using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Route256.Week5.Homework.PriceCalculator.Bll.Exceptions;
using System.Net;

namespace Route256.Week5.Homework.PriceCalculator.Api.ActionFilters;

public sealed class ExceptionFilterAttribute: Attribute, IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        switch (context.Exception)
        {
            case ValidationException exception:
                HandlerBadRequest(context, exception);
                return;

            case OneOrManyCalculationsBelongsToAnotherUserException:
            case OneOrManyCalculationsNotFoundException:
                HandlerIncorrectData(context, context.Exception);
                return;

            default:
                HandlerInternalError(context);
                return;
        }
    }

    private static void HandlerInternalError(ExceptionContext context)
    {
        var jsonResult = new JsonResult(new ErrorResponse(
            HttpStatusCode.InternalServerError, 
            "Возникла ошибка, уже чиним"));
        jsonResult.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Result = jsonResult;
    }

    private static void HandlerBadRequest(ExceptionContext context, ValidationException exception)
    {
        var jsonResult = new JsonResult(
            new ErrorResponse(
                HttpStatusCode.BadRequest, 
                exception.Message));
        
        jsonResult.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Result = jsonResult;
    }

    private static void HandlerIncorrectData(ExceptionContext context, Exception exception)
    {
        HttpStatusCode httpStatusCode;

        switch (exception)
        {
            case OneOrManyCalculationsBelongsToAnotherUserException:
                httpStatusCode = HttpStatusCode.Forbidden;
                break;

            case OneOrManyCalculationsNotFoundException:
                httpStatusCode = HttpStatusCode.BadRequest;
                break;

            default:
                httpStatusCode = HttpStatusCode.BadRequest;
                break;
        }

        var jsonResult = new JsonResult($"{(int)httpStatusCode} {exception.Message}");

        jsonResult.StatusCode = (int)httpStatusCode;

        context.Result = jsonResult;
    }
}