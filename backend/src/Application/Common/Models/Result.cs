namespace KwikDeploy.Application.Common.Models;

public class Result<T>
{
    public Result(T id)
    {
        Value = id;
    }

    public T? Value { get; set; }
}