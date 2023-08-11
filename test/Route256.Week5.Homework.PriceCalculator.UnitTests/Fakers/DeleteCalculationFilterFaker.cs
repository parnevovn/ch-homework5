using AutoBogus;
using Bogus;
using Route256.Week5.Homework.PriceCalculator.Bll.Models;
using System.Linq;

namespace Route256.Week5.Homework.PriceCalculator.UnitTests.Fakers;

public static class DeleteCalculationFilterFaker
{
    private static readonly object Lock = new();
    
    private static readonly Faker<DeleteCalculationFilter> Faker = new AutoFaker<DeleteCalculationFilter>()
        .RuleFor(x => x.UserId, f => f.Random.Long(0L))
        .RuleFor(x => x.CalculationIds, f => Enumerable.Range(1, 10).Select(_ => f.Random.Long(1, 100)).ToArray());
    
    public static DeleteCalculationFilter Generate()
    {
        lock (Lock)
        {
            return Faker.Generate();
        }
    }
    
    public static DeleteCalculationFilter WithUserId(
        this DeleteCalculationFilter src, 
        long userId)
    {
        return src with { UserId = userId };
    }

    public static DeleteCalculationFilter WithCalculationIds(
        this DeleteCalculationFilter src,
        long[] calculationIds)
    {
        return src with { CalculationIds = calculationIds };
    }
}