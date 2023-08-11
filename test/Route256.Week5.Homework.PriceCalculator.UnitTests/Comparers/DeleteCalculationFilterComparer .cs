using System;
using System.Collections.Generic;
using System.Linq;
using Route256.Week5.Homework.PriceCalculator.Bll.Models;

namespace Route256.Week5.Homework.PriceCalculator.UnitTests.Comparers;

public class DeleteCalculationFilterComparer : IEqualityComparer<DeleteCalculationFilter>
{
    public bool Equals(DeleteCalculationFilter? x, DeleteCalculationFilter? y)
    {
        if (x! is null && y is null)
        {
            return true;
        }

        if ((x is null && y is not null) || (x is not null && y is null))
        {
            return false;
        }

        return x!.UserId == y!.UserId
            && x.CalculationIds.SequenceEqual(y.CalculationIds);
    }

    public int GetHashCode(DeleteCalculationFilter obj)
    {
        return HashCode.Combine(
            obj.UserId,  
            obj.CalculationIds);
    }
}
