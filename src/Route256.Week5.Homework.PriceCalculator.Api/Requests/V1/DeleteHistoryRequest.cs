namespace Route256.Week5.Homework.PriceCalculator.Api.Requests.V1;

public record DeleteHistoryRequest(
    long UserId,
    long[] CalculationIds);