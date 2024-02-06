using Microsoft.AspNetCore.Mvc;
using static Store.BusinessLayer.Response;

namespace Store.Extentions
{
    public static class ResponseExtention
    {
        public static IActionResult ToHttpResult<T>(this IResult<T> response, int? codes = null)
        {
            if (response.Success)
            {
                if (response.Data == null)
                    return new ObjectResult(response) { StatusCode = StatusCodes.Status204NoContent };
                else if (codes == null)
                    return new ObjectResult(response) { StatusCode = StatusCodes.Status200OK };
                else if (codes == 201)
                    return new ObjectResult(response) { StatusCode = StatusCodes.Status201Created };
                else
                    return new ObjectResult(response) { StatusCode = StatusCodes.Status200OK };
            }

            return new ObjectResult(response) { StatusCode = StatusCodes.Status500InternalServerError, };
        }
    }
}
