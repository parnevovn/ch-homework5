using Moq;
using Route256.Week5.Homework.PriceCalculator.Bll.Services.Interfaces;
using Route256.Week5.Homework.PriceCalculator.UnitTests.Stubs;

namespace Route256.Week5.Homework.PriceCalculator.UnitTests.Builders;

public class DeleteCalculationHistoryHandlerBuilder
{
    public Mock<ICalculationService> CalculationService;
    
    public DeleteCalculationHistoryHandlerBuilder()
    {
        CalculationService = new Mock<ICalculationService>();
    }
    
    public DeleteCalculationHistoryCommandHandlerStub Build()
    {
        return new DeleteCalculationHistoryCommandHandlerStub(
            CalculationService);
    }
}