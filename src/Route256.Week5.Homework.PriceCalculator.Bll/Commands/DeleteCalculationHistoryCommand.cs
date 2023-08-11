using MediatR;
using Route256.Week5.Homework.PriceCalculator.Bll.Models;
using Route256.Week5.Homework.PriceCalculator.Bll.Services.Interfaces;

namespace Route256.Week5.Homework.PriceCalculator.Bll.Commands;

public record DeleteCalculationHistoryCommand(
    long UserId,
    long[] CalculationIds)
    : IRequest<DeleteHistoryResult>;

public class DeleteCalculationHistoryQueryHandler
    : IRequestHandler<DeleteCalculationHistoryCommand, DeleteHistoryResult>
{
    private readonly ICalculationService _calculationService;

    public DeleteCalculationHistoryQueryHandler(
        ICalculationService calculationService)
    {
        _calculationService = calculationService;
    }

    public async Task<DeleteHistoryResult> Handle(
        DeleteCalculationHistoryCommand request,
        CancellationToken cancellationToken)
    {
        var query = new DeleteCalculationFilter(
            request.UserId,
            request.CalculationIds);

        await _calculationService.DeleteCalculations(query, cancellationToken);

        return new DeleteHistoryResult();
    }
}