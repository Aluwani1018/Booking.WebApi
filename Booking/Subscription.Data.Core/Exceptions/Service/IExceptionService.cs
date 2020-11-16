using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Subscription.Infrastructure.Exceptions.Service
{
    public interface IExceptionService
    {
        Task<IActionResult> HandleApiExceptionAsync<TException>(string controller, string method, Func<Task<IActionResult>> function) where TException : ApiException;
    }
}
