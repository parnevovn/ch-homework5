﻿namespace Route256.Week5.Homework.PriceCalculator.Bll.Models
{
    public record DeleteCalculationFilter(
        long UserId,
        long[] CalculationIds);
}
