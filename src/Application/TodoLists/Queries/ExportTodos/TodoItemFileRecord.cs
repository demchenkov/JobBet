using JobBet.Application.Common.Mappings;
using JobBet.Domain.Entities;

namespace JobBet.Application.TodoLists.Queries.ExportTodos;

public class TodoItemRecord : IMapFrom<TodoItem>
{
    public string? Title { get; set; }

    public bool Done { get; set; }
}
