using System;
using System.Collections.Generic;
using System.Linq;
using AutoBogus;
using Bogus;
using Bogus.DataSets;
using Route256.Week5.Homework.PriceCalculator.Bll.Models;
using Route256.Week5.Homework.PriceCalculator.Dal.Entities;
using Route256.Week5.Homework.PriceCalculator.Dal.Models;

namespace Route256.Week5.Homework.PriceCalculator.UnitTests.Fakers;

public static class DeleteHistoryQueryModelFaker
{
    private static readonly object Lock = new();

    private static readonly Faker<DeleteHistoryQueryModel> Faker = new AutoFaker<DeleteHistoryQueryModel>()
        .RuleFor(x => x.UserId, f => f.Random.Long(0L))
        .RuleFor(x => x.CalculationIds, f => Enumerable.Range(1, 10).Select(_ => f.Random.Long(1, 100)).ToArray());

    public static DeleteHistoryQueryModel Generate()
    {
        lock (Lock)
        {
            return Faker.Generate();
        }
    }

    public static DeleteHistoryQueryModel WithIds(
        this DeleteHistoryQueryModel src,
        long[] calculationIds)
    {
        return src with { CalculationIds = calculationIds };
    }

    public static DeleteHistoryQueryModel WithUserId(
        this DeleteHistoryQueryModel src,
        long userId)
    {
        return src with { UserId = userId };
    }
}
