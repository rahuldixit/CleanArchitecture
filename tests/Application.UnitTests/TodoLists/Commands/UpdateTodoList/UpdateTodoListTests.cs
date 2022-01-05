using System.Threading;
using System.Threading.Tasks;
using CaWorkshop.Application.Common.Exceptions;
using CaWorkshop.Application.TodoLists.Commands.UpdateTodoList;
using CaWorkshop.Infrastructure.Data;
using FluentAssertions;
using Xunit;

namespace CaWorkshop.Application.UnitTests.TodoLists.Commands.UpdateTodoList;

public class UpdateTodoListTests : TestFixture
{
    [Fact]
    public async Task UpdateTodoListShouldThrowNotFoundForInvalidId()
    {
        var command = new UpdateTodoListCommand
        {
            Id = 99,
            Title = "List that does not exist"
        };

        var handler = new UpdateTodoListCommandHandler(Context);

        await FluentActions.Invoking(() =>
            handler.Handle(command, CancellationToken.None))
            .Should().ThrowAsync<NotFoundException>();
    }
}
