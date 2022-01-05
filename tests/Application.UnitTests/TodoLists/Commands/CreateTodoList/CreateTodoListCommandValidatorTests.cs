using CaWorkshop.Application.TodoLists.Commands.CreateTodoList;
using FluentAssertions;
using FluentValidation.TestHelper;
using Xunit;

namespace CaWorkshop.Application.UnitTests.TodoLists.Commands.CreateTodoList;

public class CreateTodoListCommandValidatorTests : IClassFixture<TestFixture>
{
    private readonly TestFixture _fixture;

    public CreateTodoListCommandValidatorTests(TestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void IsValid_ShouldBeTrue_WhenListTitleIsUnique()
    {
        var command = new CreateTodoListCommand
        {
            Title = "Bucket List"
        };

        var validator = new CreateTodoListCommandValidator(_fixture.Context);

        var result = validator.TestValidate(command);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void IsValid_ShouldBeFalse_WhenListTitleIsNotUnique()
    {
        var command = new CreateTodoListCommand
        {
            Title = "Todo List"
        };

        var validator = new CreateTodoListCommandValidator(_fixture.Context);

        var result = validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(r => r.Title)
            .WithErrorCode("UNIQUE_TITLE");
    }
}
