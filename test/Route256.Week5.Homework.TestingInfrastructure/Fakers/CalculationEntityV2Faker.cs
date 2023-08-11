using AutoBogus;
using Bogus;
using Route256.Week5.Homework.PriceCalculator.Dal.Entities;

namespace Route256.Week5.Homework.TestingInfrastructure.Fakers;

public static class CalculationEntityV2Faker
{
    private static readonly object Lock = new();

    private static readonly Faker<CalculationEntityV2> Faker = new AutoFaker<CalculationEntityV2>()
        .RuleFor(x => x.Id, f => f.Random.Long(0L))
        .RuleFor(x => x.UserId, f => f.Random.Long(0L))
        .RuleFor(x => x.Price, f => f.Random.Decimal())
        .RuleFor(x => x.TotalVolume, f => f.Random.Double())
        .RuleFor(x => x.TotalWeight, f => f.Random.Double())
        .RuleFor(x => x.GoodIds, f => Enumerable.Range(1, 10).Select(_ => f.Random.Long(1, 100)).ToArray());

    public static CalculationEntityV2[] Generate(int count = 1)
    {
        lock (Lock)
        {
            return Enumerable.Repeat(Faker.Generate(), count)
                .ToArray();
        }
    }

    public static CalculationEntityV2 WithId(
        this CalculationEntityV2 src, 
        long id)
    {
        return src with { Id = id };
    }
    
    public static CalculationEntityV2 WithUserId(
        this CalculationEntityV2 src, 
        long userId)
    {
        return src with { UserId = userId };
    }
    
    public static CalculationEntityV2 WithTotalVolume(
        this CalculationEntityV2 src, 
        double totalVolume)
    {
        return src with { TotalVolume = totalVolume };
    }
    
    public static CalculationEntityV2 WithTotalWeight(
        this CalculationEntityV2 src, 
        double totalWeight)
    {
        return src with { TotalWeight = totalWeight };
    }
    
    public static CalculationEntityV2 WithPrice(
        this CalculationEntityV2 src, 
        decimal price)
    {
        return src with { Price = price };
    }
    
    public static CalculationEntityV2 WithAt(
        this CalculationEntityV2 src, 
        DateTimeOffset at)
    {
        return src with { At = at };
    }
}