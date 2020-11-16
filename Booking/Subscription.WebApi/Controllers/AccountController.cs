using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Subscription.Core.Domain;
using Subscription.Infrastructure.Exceptions;
using Subscription.Infrastructure.Exceptions.Model.Responses;
using Subscription.Infrastructure.Exceptions.Service;
using Subscription.Core.Security.Tokens;
using Subscription.Service.Services.AuthenticationService;
using Subscription.Service.Services.UserService;
using Subscription.WebApi.Models.Account.Requests;
using Subscription.WebApi.Models.Authentication.Response;
using Swashbuckle.AspNetCore.Annotations;

namespace Subscription.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IExceptionService _exceptionService;
        private readonly IAuthenticationService _authenticationService;

        public AccountController
           (
                IMapper mapper,
                IUserService userService,
                IExceptionService exceptionService,
                IAuthenticationService authenticationService
           )
        {
            this._mapper = mapper;
            this._userService = userService;
            this._exceptionService = exceptionService;
            this._authenticationService = authenticationService;
        }

        /// <summary>
        /// Register an account
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("Register")]
        [SwaggerOperation("Register an account")]
        [SwaggerResponse(statusCode: 200, type: typeof(AccessToken), description: "Success Request")]
        [SwaggerResponse(statusCode: 400, type: typeof(ErrorResponse), description: "Bad Request")]
        [SwaggerResponse(statusCode: 500, type: typeof(ErrorResponse), description: "Error Request")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest request)
        {
            return await _exceptionService.HandleApiExceptionAsync<ApiException>(nameof(AccountController), nameof(RegisterAsync), async () =>
            {
                User user = _mapper.Map<RegisterRequest, User>(request);

                User userRespose = await _userService.CreateUserAsync(user, request.Password);
                return Ok();
            });
        }
    }
}
