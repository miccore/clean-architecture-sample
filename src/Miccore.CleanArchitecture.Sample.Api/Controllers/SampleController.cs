using System.Net;
using Asp.Versioning;
using MediatR;
using Miccore.CleanArchitecture.Sample.Api.Validators.Sample;
using Miccore.CleanArchitecture.Sample.Application.Commands.Sample;
using Miccore.CleanArchitecture.Sample.Application.Queries.Sample;
using Miccore.CleanArchitecture.Sample.Application.Responses.Sample;
using Miccore.CleanArchitecture.Sample.Core.Exceptions;
using Miccore.Pagination.Model;
using Miccore.Pagination.Service;
using Microsoft.AspNetCore.Mvc;

namespace Miccore.CleanArchitecture.Sample.Api.Controllers
{
    /// <summary>
    /// sample api controller
    /// </summary>

    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class SampleController : BaseController
    {
        private readonly IMediator _mediator;

        public SampleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// create sample
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(template: "", Name = nameof(CreateSample))]
        public async Task<ActionResult<SampleResponse>> CreateSample([FromBody] CreateSampleCommand command)
        {
            try
            {
                // validate command
                var validator = new CreateSampleValidator();
                var validate = validator.Validate(command);
                if(!validate.IsValid){
                    throw new ValidatorException(validate.ToString());
                }

                // call command
                var created = await _mediator.Send(command);

                // return response
                return HandleCreatedResponse(created);

            }
            // not found exception
            catch (NotFoundException notFound)
            {
                return HandleErrorResponse(HttpStatusCode.NotFound, notFound.Message);
            }
            // invalid data validation exception
            catch (ValidatorException invalid)
            {
                return HandleErrorResponse(HttpStatusCode.BadRequest, invalid.Message);
            }
            // general exception
            catch (Exception ex)
            {
                return HandleErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// delete sample
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]    
        [HttpDelete(template: "{id}", Name = nameof(DeleteSample))]
        public async Task<IActionResult> DeleteSample(int id)
        {
            // delete existing movie
            try
            {
                // create command
                DeleteSampleCommand command = new DeleteSampleCommand(id);

                // call command
                var deleted = await _mediator.Send(command);

                // return response
                return HandleDeletedResponse();
            }
            // not found exception
            catch (NotFoundException notFound)
            {
                return HandleErrorResponse(HttpStatusCode.NotFound, notFound.Message);
            }
            // general exception
            catch (Exception ex)
            {
                return HandleErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// get all sample paginate
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet(template: "", Name = nameof(GetAllSamples))]
        public async Task<ActionResult<PaginationModel<SampleResponse>>> GetAllSamples([FromQuery] PaginationQuery query)
        {
            try
            {
                // create query
                GetAllSampleQuery sampleQuery = new GetAllSampleQuery(query);

                // call query
                var samples = await _mediator.Send(sampleQuery);

                // return date if not paginate
                if(!query.paginate) return HandleSuccessResponse(samples);

                // add next and previous links
                samples.AddRouteLink(Url.RouteUrl(nameof(GetAllSamples)), query);

                // return response
                return HandleSuccessResponse(samples);

            }
            // general exception
            catch (Exception ex)
            {
                return HandleErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// get sample by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet(template: "{id}", Name = nameof(GetSampleById))]
        public async Task<ActionResult<SampleResponse>> GetSampleById(int id)
        {
            try
            {
                // create query
                GetSampleByIdQuery sampleQuery = new GetSampleByIdQuery(id);

                // call query
                var sample = await _mediator.Send(sampleQuery);

                // return response
                return HandleSuccessResponse(sample);

            }
            // not found exception
            catch (NotFoundException notFound)
            {
                return HandleErrorResponse(HttpStatusCode.NotFound, notFound.Message);
            }
            // general exception
            catch (Exception ex)
            {
                return HandleErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        /// <summary>
        /// update sample
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut(template: "", Name = nameof(UpdateSample))]
        public async Task<ActionResult<SampleResponse>> UpdateSample([FromBody] UpdateSampleCommand command)
        {
            try
            {
                // validate command
                var validator = new UpdateSampleValidator();
                var validate = validator.Validate(command);
                if(!validate.IsValid){
                    throw new ValidatorException(validate.ToString());
                }

                // call command
                var updated = await _mediator.Send(command);

                // return response
                return HandleSuccessResponse(updated);

            }
            // not found exception
            catch (NotFoundException notFound)
            {
                return HandleErrorResponse(HttpStatusCode.NotFound, notFound.Message);
            }
            // invalid data validation exception
            catch (ValidatorException invalid)
            {
                return HandleErrorResponse(HttpStatusCode.BadRequest, invalid.Message);
            }
            // general exception
            catch (Exception ex)
            {
                return HandleErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}