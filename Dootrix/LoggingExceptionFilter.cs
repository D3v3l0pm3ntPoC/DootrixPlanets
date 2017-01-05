using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace Dootrix.Planets.Api
{
    public class LoggingExceptionFilter : ExceptionFilterAttribute
    {
        /// <summary>
        /// Raises the exception event.
        /// </summary>
        /// <param name="actionExecutedContext">The context for the action.</param>
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnException(actionExecutedContext);
            actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(HttpStatusCode.InternalServerError, actionExecutedContext.Exception.Message);

            //Raise error notification events analytic data or just log to file on disk (probably to be consumed by a backend processor that knows what to do with notification) 
            //Not enough time to implement this....
            //SystemEvents.RaiseExceptionNotification(
            //    actionExecutedContext.ActionContext.ControllerContext.ControllerDescriptor.ControllerType.Assembly.GetName().Name,
            //    actionExecutedContext.Exception);
        }
    }
}