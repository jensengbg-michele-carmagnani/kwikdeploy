using Microsoft.AspNetCore.Identity;

namespace KwikDeploy.Application.Common.Exceptions;

public class IdentityException : Exception
{
    public IdentityException(IEnumerable<IdentityError> identityErrors)
    {
        IdentityErrors = identityErrors;
    }

    public IEnumerable<IdentityError> IdentityErrors { get; }
}
