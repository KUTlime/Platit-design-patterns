namespace Assignments.Program1.Core;

public record Request(string Key, string Value, bool Encrypted) : IRequest;
