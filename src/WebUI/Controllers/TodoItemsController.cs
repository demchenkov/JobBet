using JobBet.Application.Common.Models;
using JobBet.Application.TodoItems.Commands.CreateTodoItem;
using JobBet.Application.TodoItems.Commands.DeleteTodoItem;
using JobBet.Application.TodoItems.Commands.UpdateTodoItem;
using JobBet.Application.TodoItems.Commands.UpdateTodoItemDetail;
using JobBet.Application.TodoItems.Queries.GetTodoItemsWithPagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobBet.WebUI.Controllers;

[Authorize]
public class TodoItemsController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PaginatedList<TodoItemBriefDto>>> GetTodoItemsWithPagination([FromQuery] GetTodoItemsWithPaginationQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateTodoItemCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, UpdateTodoItemCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpPut("[action]")]
    public async Task<ActionResult> UpdateItemDetails(int id, UpdateTodoItemDetailCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteTodoItemCommand { Id = id });

        return NoContent();
    }
}
