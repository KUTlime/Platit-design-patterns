public class UnderregulatedState : IState
{
    public IState Regulate() => new OverregulatedState();
}
