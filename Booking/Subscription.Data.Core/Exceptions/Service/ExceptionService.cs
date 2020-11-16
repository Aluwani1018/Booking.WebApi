using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Subscription.Infrastructure.Exceptions.Model.Enum;
using Subscription.Infrastructure.Exceptions.Model.Responses;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Subscription.Infrastructure.Exceptions.Service
{
    public class ExceptionService : IExceptionService
    {
        private readonly ILogger<ExceptionService> _logger;
        private readonly JsonSerializerSettings _jsonSerializerSettings;

        public ExceptionService
            (
                ILogger<ExceptionService> logger,
                JsonSerializerSettings jsonSerializerSettings
            )
        {
            this._logger = logger;
            this._jsonSerializerSettings = jsonSerializerSettings;
        }

        public async Task<IActionResult> HandleApiExceptionAsync<TException>(string controller, string method, Func<Task<IActionResult>> function) where TException : ApiException
        {
            ErrorResponse errorResponse = new ErrorResponse((int)ErrorEnum.GeneralError, nameof(ErrorEnum.GeneralError));

            try
            {
                return await function();
            }
            catch (TException ex)
            {
                return ExceptionHandlerMapping(ex, controller, method, errorResponse);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Unmapped Api Exception Received ({0} - {1}): {2}", controller, method, ex.Message);
                return new ObjectResult(errorResponse)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }

        private IActionResult ExceptionHandlerMapping<TException>(TException ex, string controller, string method, ErrorResponse errorResponse) where TException : ApiException
        {
            if (ex.ErrorContentType != null)
            {
                try
                {
                    ErrorResponse internalErrorResponse = (ErrorResponse)JsonConvert.DeserializeObject(ex.ErrorContent?.ToString(), typeof(ErrorResponse), this._jsonSerializerSettings);
                    Array enums = Enum.GetValues(typeof(ErrorEnum));

                    foreach (ErrorEnum enumValue in enums)
                    {
                        try
                        {
                            if ((ErrorEnum)internalErrorResponse.ErrorCode == (ErrorEnum)enumValue)
                            {
                                HandleExceptionLogging((ErrorEnum)enumValue, () => _logger.LogError(ex, "{0} Api Exception ({1} - {2}) : {3} : {4}.", enumValue.ToString(), controller, method, ex.Message, ex.ErrorContent));
                                return MapErrorToResponse((ErrorEnum)enumValue, enumValue.ToString());
                            }
                        }
                        catch (System.Exception)
                        {

                            if (string.Equals(Regex.Replace(internalErrorResponse.ErrorMessage, @"(\\|\/|\s|-|\.)", string.Empty), enumValue.ToString(), StringComparison.InvariantCultureIgnoreCase))
                            {
                                HandleExceptionLogging((ErrorEnum)enumValue, () => _logger.LogError(ex, "{0} Api Exception ({1} - {2}) : {3} : {4}.", enumValue.ToString(), controller, method, ex.Message, ex.ErrorContent));
                                return MapErrorToResponse((ErrorEnum)enumValue, enumValue.ToString());
                            }
                        }
                    }
                }
                catch (JsonReaderException)
                {
                    _logger.LogError(ex, "Unmapped Api Exception ({0} - {1}) (Could Not Map To 'ErrorResponse'): {2}", controller, method, ex.Message);
                    return MapErrorToResponse(ErrorEnum.GeneralError, nameof(ErrorEnum.GeneralError));
                }

                _logger.LogError(ex, "Unmapped Api Exception Received ({0} - {1}): {2}", controller, method, ex.Message);
                return MapErrorToResponse(ErrorEnum.GeneralError, nameof(ErrorEnum.GeneralError));
            }

            _logger.LogError(ex, "Unmapped Api Exception Received ({0} - {1}): {2}", controller, method, ex.Message);
            return MapErrorToResponse(ErrorEnum.GeneralError, nameof(ErrorEnum.GeneralError));
        }

        private static IActionResult MapErrorToResponse(ErrorEnum error, string errorName)
        {
            return new ObjectResult(new ErrorResponse((int)error, errorName))
            {
                StatusCode = (int)HttpStatusCode.BadRequest
            };
        }

        private static void HandleExceptionLogging(ErrorEnum errorEnum, Action function)
        {
            function();
        }
    }
}
