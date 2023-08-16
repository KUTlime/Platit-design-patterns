public class OverregulatedState : IState
{
    // Do some system regulation by altering it internal state and return new state
    // that reflects the altered state.
    public IState Regulate() => new UnderregulatedState();
}
