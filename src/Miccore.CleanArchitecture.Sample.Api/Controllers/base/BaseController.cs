using System.Net;
using Miccore.CleanArchitecture.Sample.Core.ApiModels;
using Microsoft.AspNetCore.Mvc;

namespace Miccore.CleanArchitecture.Sample.Api.Controllers
{
    /// <summary>
    /// base controller
    /// </summary>
    public class BaseController : ControllerBase
    {   
        /// <summary>
        /// return success response with 200 by default
        /// </summary>
        /// <param name="data"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        protected ActionResult HandleSuccessResponse(object data, HttpStatusCode status = HttpStatusCode.OK)
        {
            var response = new ApiResponse();
            response.Data = data;
            return StatusCode((int)status, response);
        }

        /// <summary>
        /// return success response with 201 by default
        /// </summary>
        /// <param name="data"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        protected ActionResult HandleCreatedResponse(object data, HttpStatusCode status = HttpStatusCode.Created)
        {
            var response = new ApiResponse();
            response.Data = data;
            return StatusCode((int)status, response);
        }

        /// <summary>
        /// error response with message and status code
        /// </summary>
        /// <param name="httpStatus"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        protected ActionResult HandleErrorResponse(HttpStatusCode httpStatus, string message)
        {
            var response = new ApiResponse()
            {
                Errors = new List<ApiError>
                {
                    new ApiError
                    {
                        Code = (int)httpStatus,
                        Message = message,
                    },
                },
            };

            return StatusCode((int)httpStatus, response);
        }

        /// <summary>
        /// handle deleted response with 204 no content
        /// </summary>
        /// <returns></returns>
        protected ActionResult HandleDeletedResponse()
        {
            return StatusCode((int)HttpStatusCode.NoContent);
        }
    }
}