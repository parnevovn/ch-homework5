using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Route256.Week5.Homework.PriceCalculator.Bll.Exceptions;
using Route256.Week5.Homework.PriceCalculator.Bll.Models;
using Route256.Week5.Homework.PriceCalculator.UnitTests.Builders;
using Route256.Week5.Homework.PriceCalculator.UnitTests.Extensions;
using Route256.Week5.Homework.PriceCalculator.UnitTests.Fakers;
using Route256.Week5.Homework.TestingInfrastructure.Creators;
using Xunit;

namespace Route256.Week5.Homework.PriceCalculator.UnitTests.HandlersTests;

public class DeleteCalculationHistoryCommandHandlerTests
{
    [Fact]
    public async Task Handle_MakeAllCalls()
    {
        //arrange
        var userId = Create.RandomId();

        var command = DeleteCalculationCommandFaker.Generate()
            .WithUserId(userId);

        var deleteCalculationFilter = DeleteCalculationFilterFaker.Generate()
            .WithUserId(userId)
            .WithCalculationIds(command.CalculationIds);

        var builder = new DeleteCalculationHistoryHandlerBuilder();
        builder.CalculationService
            .SetupDeleteCalculation();

        var handler = builder.Build();
        
        //act
        var result = await handler.Handle(command, default);

        //asserts
        handler.CalculationService
            .VerifyDeleteCalculationWasCalledOnce(deleteCalculationFilter);
        
        handler.VerifyNoOtherCalls();
    }    
}