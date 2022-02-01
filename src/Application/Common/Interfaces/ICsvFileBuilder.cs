using JobBet.Application.TodoLists.Queries.ExportTodos;

namespace JobBet.Application.Common.Interfaces;

public interface ICsvFileBuilder
{
    byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
}
