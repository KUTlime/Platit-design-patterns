namespace Assignments.Program1.Core;

public static class StringExtensions
{
    public static string GetRandomString(this string input) => new Random().Next().ToString() + input;
}
