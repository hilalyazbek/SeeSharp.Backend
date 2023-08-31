using System;
namespace SeeSharp.Application.Common.Models;

public class Result
{
    public bool Succeeded { get; init; }

    public string[] Errors { get; init; }

    internal Result(bool succeeded, IEnumerable<string> errors)
    {
        Succeeded = succeeded;
        Errors = errors.ToArray();
    }

    public static Result Success()
    {
        return new Result(true, Array.Empty<string>());
    }

    public static Result Failure(IEnumerable<string> errors)
    {
        return new Result(false, errors);
    }
}

