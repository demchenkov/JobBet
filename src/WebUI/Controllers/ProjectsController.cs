using JobBet.Application.Common.Models;
using JobBet.Application.Projects.Commands.CreateProject;
using JobBet.Application.Projects.Commands.DeleteProject;
using JobBet.Application.Projects.Commands.SetProjectExecutor;
using JobBet.Application.Projects.Commands.UpdateProject;
using JobBet.Application.Projects.Queries.GetProjectById;
using JobBet.Application.Projects.Queries.GetProjectsWithPagination;
using Microsoft.AspNetCore.Mvc;

namespace JobBet.WebUI.Controllers;

// [Authorize]
public class ProjectsController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PaginatedList<ProjectDto>>> GetProjectsWithPagination([FromQuery] GetProjectsQuery query)
    {
        return await Mediator.Send(query);
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProjectDetailsDto>> GetProjectById(int id)
    {
        return await Mediator.Send(new GetProjectByIdQuery { Id = id });
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create([FromBody] CreateProjectCommand command)
    {
        return await Mediator.Send(command);
    }
    
    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, UpdateProjectCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }
    
        await Mediator.Send(command);
    
        return NoContent();
    }

    [HttpPut("{id:int}/executor")]
    public async Task<ActionResult> ChooseExecutor(int id, SetProjectExecutorCommand command)
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
        await Mediator.Send(new DeleteProjectCommand { Id = id });
    
        return NoContent();
    }
}