using Microsoft.AspNetCore.Mvc;
using System.Runtime.Serialization;

namespace Route256.Week5.Homework.PriceCalculator.Bll.Exceptions;

public class OneOrManyCalculationsBelongsToAnotherUserException : Exception
{
    public OneOrManyCalculationsBelongsToAnotherUserException() : base(typeof(OneOrManyCalculationsBelongsToAnotherUserException).Name)
    {
    }

    public OneOrManyCalculationsBelongsToAnotherUserException(string? message) : base($"{typeof(OneOrManyCalculationsBelongsToAnotherUserException).Name} {message}")
    {
    }
}