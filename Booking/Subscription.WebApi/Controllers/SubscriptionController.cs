using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Subscription.Core.Domain;
using Subscription.Core.Domain.Enums;
using Subscription.Infrastructure.Exceptions;
using Subscription.Infrastructure.Exceptions.Model.Responses;
using Subscription.Infrastructure.Exceptions.Service;
using Subscription.Service.Services.AuthenticationService;
using Subscription.Service.Services.BookService;
using Subscription.Service.Services.UserService;
using Subscription.WebApi.Models.Subscription.Response;
using Swashbuckle.AspNetCore.Annotations;


namespace Subscription.WebApi.Controllers
{
    [Authorize(Roles = "Common")]
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IUserService _userService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IExceptionService _exceptionService;

        public SubscriptionController
           (
                IBookService bookService,
                IUserService userService,
                IExceptionService exceptionService,
                IAuthenticationService authenticationService
           )
        {
            this._bookService = bookService;
            this._userService = userService;
            this._exceptionService = exceptionService;
            this._authenticationService = authenticationService;
        }

        /// <summary>
        /// Get account balance for a account id
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("Type/{type}/Subscribe")]
        [SwaggerOperation("Subscribe")]
        //[SwaggerResponse(statusCode: 200, type: typeof(BalanceResponse), description: "Success Request")]
        [SwaggerResponse(statusCode: 400, type: typeof(ErrorResponse), description: "Bad Request")]
        [SwaggerResponse(statusCode: 500, type: typeof(ErrorResponse), description: "Error Request")]
        public async Task<IActionResult> SubscribeAsync([FromRoute] SubscriptionTypeEnum type)
        {
            return await _exceptionService.HandleApiExceptionAsync<ApiException>(nameof(AccountController), nameof(SubscribeAsync), async () =>
            {
                var user = await _userService.SubscribeUserAsync(_authenticationService.GetUserEmail(), type);

                return Ok();
            });
        }

        /// <summary>
        /// Get account balance for a account id
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("Types")]
        [SwaggerOperation("Get subscription types")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<SubscriptionTypeResponse>), description: "Success Request")]
        [SwaggerResponse(statusCode: 400, type: typeof(ErrorResponse), description: "Bad Request")]
        [SwaggerResponse(statusCode: 500, type: typeof(ErrorResponse), description: "Error Request")]
        public async Task<IActionResult> GetAllSubscriptionTypesAsync()
        {
            return await _exceptionService.HandleApiExceptionAsync<ApiException>(nameof(AccountController), nameof(SubscribeAsync), async () =>
            {
                List<SubscriptionTypeResponse> response = ((SubscriptionTypeEnum[])Enum.GetValues(typeof(SubscriptionTypeEnum))).Select(c => new SubscriptionTypeResponse() { Id = (int)c, Name = c.ToString() }).ToList();

                return Ok(response);
            });
        }

        /// <summary>
        /// Get account balance for a account id
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("Books")]
        [SwaggerOperation("Gets All Books for Subscribed Users")]
       //[SwaggerResponse(statusCode: 200, type: typeof(BalanceResponse), description: "Success Request")]
        [SwaggerResponse(statusCode: 400, type: typeof(ErrorResponse), description: "Bad Request")]
        [SwaggerResponse(statusCode: 500, type: typeof(ErrorResponse), description: "Error Request")]
        public async Task<IActionResult> GetAllBooks()
        {
            return await _exceptionService.HandleApiExceptionAsync<ApiException>(nameof(AccountController), nameof(SubscribeAsync), async () =>
            {
                List<Book> booksReponse = await _bookService.GetAllBooks(_authenticationService.GetUserEmail());
                //map
                return Ok();
            });
        }
    }
}
