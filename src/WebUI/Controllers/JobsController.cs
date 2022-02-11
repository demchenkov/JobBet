using JobBet.Application.Common.Models;
using JobBet.Application.Jobs.Commands.CreateJob;
using JobBet.Application.Jobs.Commands.DeleteJob;
using JobBet.Application.Jobs.Commands.SetJobExecutor;
using JobBet.Application.Jobs.Commands.UpdateJob;
using JobBet.Application.Jobs.Queries.GetJobById;
using JobBet.Application.Jobs.Queries.GetJobsWithPagination;
using Microsoft.AspNetCore.Mvc;

namespace JobBet.WebUI.Controllers;

// [Authorize]
public class JobsController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PaginatedList<JobDto>>> GetJobsWithPagination([FromQuery] GetJobsQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<JobDetailsDto>> GetJobById(int id)
    {
        return await Mediator.Send(new GetJobByIdQuery {Id = id});
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create([FromBody] CreateJobCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, UpdateJobCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpPatch("{id:int}")]
    public async Task<ActionResult> ChooseExecutor(int id, SetJobExecutorCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteJobCommand {Id = id});

        return NoContent();
    }
}