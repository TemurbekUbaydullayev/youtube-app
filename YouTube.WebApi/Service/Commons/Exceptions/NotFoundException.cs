using System.Net;

namespace YouTube.WebApi.Service.Commons.Exceptions;

public class NotFoundException : HttpStatusCodeException
{
    public NotFoundException(string entityName)
    : base(HttpStatusCode.NotFound, $"{entityName} not found!")
    {
    }
}
