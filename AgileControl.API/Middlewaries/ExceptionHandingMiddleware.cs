using AgileControl.API.Models.Exceptions;
using AgileControl.API.Models.Responses;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace AgileControl.API.Middlewaries;

public class ExceptionHandingMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException ex)
        {
            var code = HttpStatusCode.BadRequest;
            var errorMessage = ex.Message;
            await HandleExceptionAsync(context, code, errorMessage);
        }
        catch (ArgumentException ex)
        {
            var code = HttpStatusCode.BadRequest;
            var errorMessage = ex.Message;
            await HandleExceptionAsync(context, code, errorMessage);
        }
        catch (BadRequestException ex)
        {

            var code = HttpStatusCode.BadRequest;
            var errorMessage = ex.Message;
            await HandleExceptionAsync(context, code, errorMessage);
        }
        catch (NotFoundException ex)
        {
            var code = HttpStatusCode.BadRequest;
            var errorMessage = ex.Message;
            await HandleExceptionAsync(context, code, errorMessage);
        }
        catch (Exception)
        {
            var code = HttpStatusCode.InternalServerError;
            var errorMessage = "Internal server error.";
            await HandleExceptionAsync(context, code, errorMessage);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, HttpStatusCode code, string errorMessage)
    {
        context.Response.StatusCode = (int)code;
        context.Response.ContentType = "application/json";

        var response = new Response<object>
        {
            Data = null,
            Error = new Error
            {
                Message = errorMessage,
                ErrorCode = code
            }
        };


        return context.Response.WriteAsJsonAsync(response);
    }
}