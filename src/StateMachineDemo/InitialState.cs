public class InitialState : IState
{
    public IState Regulate() => new UnderregulatedState();
}
