using AutoBogus;
using Bogus;
using Route256.Week5.Homework.PriceCalculator.Bll.Commands;
using Route256.Week5.Homework.PriceCalculator.Bll.Models;
using System.Linq;

namespace Route256.Week5.Homework.PriceCalculator.UnitTests.Fakers;

public static class DeleteCalculationCommandFaker
{
    private static readonly object Lock = new();
    
    private static readonly Faker<DeleteCalculationHistoryCommand> Faker = new AutoFaker<DeleteCalculationHistoryCommand>()
        .RuleFor(x => x.UserId, f => f.Random.Long(0L))
        .RuleFor(x => x.CalculationIds, f => Enumerable.Range(1, 10).Select(_ => f.Random.Long(1, 100)).ToArray());
    
    public static DeleteCalculationHistoryCommand Generate()
    {
        lock (Lock)
        {
            return Faker.Generate();
        }
    }
    
    public static DeleteCalculationHistoryCommand WithUserId(
        this DeleteCalculationHistoryCommand src, 
        long userId)
    {
        return src with { UserId = userId };
    }

    public static DeleteCalculationHistoryCommand WithCalculationIds(
        this DeleteCalculationHistoryCommand src,
        long[] calculationIds)
    {
        return src with { CalculationIds = calculationIds };
    }
}