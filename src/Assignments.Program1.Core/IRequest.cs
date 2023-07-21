namespace Assignments.Program1.Core;

public interface IRequest
{
    public string Key { get; }

    public string Value { get; }

    public bool Encrypted { get; }
}
