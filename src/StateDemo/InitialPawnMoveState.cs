public class InitialPawnMoveState : IPawnMoveState
{
    public bool CanMove(Coordinate coordinate) => true;

    // Move One or Two fields forward
    public void Move(Coordinate coordinate)
    {
    }
}
