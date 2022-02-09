using JobBet.Application.Common.Models;
using JobBet.Application.Clients.Queries.GetClientById;
using JobBet.Application.Clients.Queries.GetClientsWithPagination;
using Microsoft.AspNetCore.Mvc;

namespace JobBet.WebUI.Controllers;

// [Authorize]
public class ClientsController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PaginatedList<ClientDto>>> GetClientsWithPagination([FromQuery] GetClientsQuery query)
    {
        return await Mediator.Send(query);
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ClientDetailsDto>> GetClientById(int id)
    {
        return await Mediator.Send(new GetClientByIdQuery { Id = id });
    }
}