using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Subscription.WebApi.Models.Authentication.Requests;
using Subscription.Infrastructure.Exceptions.Service;
using Subscription.Infrastructure.Exceptions.Model.Responses;
using Subscription.Infrastructure.Exceptions;
using Subscription.Service.Services.AuthenticationService;
using Subscription.Core.Security.Tokens;
using Subscription.WebApi.Models.Authentication.Response;
using AutoMapper;

namespace Subscription.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IExceptionService _exceptionService;
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController
           (
                IMapper mapper,
                IExceptionService exceptionService,
                IAuthenticationService authenticationService
           )
        {
            this._mapper = mapper;
            this._exceptionService = exceptionService;
            this._authenticationService = authenticationService;
        }

        /// <summary>
        /// User Log in
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("LogIn")]
        [SwaggerOperation("Log in")]
        [SwaggerResponse(statusCode: 200, type: typeof(LogInResponse), description: "Success Request")]
        [SwaggerResponse(statusCode: 400, type: typeof(ErrorResponse), description: "Bad Request")]
        [SwaggerResponse(statusCode: 500, type: typeof(ErrorResponse), description: "Error Request")]
        public async Task<IActionResult> LogInAsync([FromBody] LogInRequest request)
        {
            return await _exceptionService.HandleApiExceptionAsync<ApiException>(nameof(AuthenticationController), nameof(LogInAsync), async () =>
            {
                AccessToken accessTokenResponse = await _authenticationService.LogInAsync(request.Email, request.Password);

                LogInResponse logInResponse = _mapper.Map<AccessToken, LogInResponse>(accessTokenResponse);
                
                return Ok(logInResponse);
            });
        }

        /// <summary>
        /// User Log in
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("Token/Refresh")]
        [SwaggerOperation("Refresh Token")]
        [SwaggerResponse(statusCode: 200, type: typeof(LogInResponse), description: "Success Request")]
        [SwaggerResponse(statusCode: 400, type: typeof(ErrorResponse), description: "Bad Request")]
        [SwaggerResponse(statusCode: 500, type: typeof(ErrorResponse), description: "Error Request")]
        public async Task<IActionResult> RefreshTokenAsync([FromBody] RefreshTokenRequest request)
        {
            return await _exceptionService.HandleApiExceptionAsync<ApiException>(nameof(AuthenticationController), nameof(RefreshTokenAsync), async () =>
            {
                AccessToken response = await _authenticationService.RefreshTokenAsync(request.Token, request.UserEmail);

                LogInResponse logInResponse = _mapper.Map<AccessToken, LogInResponse>(response);
                return Ok(logInResponse);
            });
        }

        /// <summary>
        /// User Log in
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("Token/Revoke")]
        [SwaggerOperation("Revoke Token")]
        [SwaggerResponse(statusCode: 200, description: "Success Request")]
        [SwaggerResponse(statusCode: 400, type: typeof(ErrorResponse), description: "Bad Request")]
        [SwaggerResponse(statusCode: 500, type: typeof(ErrorResponse), description: "Error Request")]
        public async Task<IActionResult> RevokeToken([FromBody] RevokeTokenRequest request)
        {
            return await _exceptionService.HandleApiExceptionAsync<ApiException>(nameof(AuthenticationController), nameof(RevokeToken), async () =>
            {
                _authenticationService.RevokeRefreshToken(request.RefeshToken);
                return Ok();
            });
        }
    }
}
