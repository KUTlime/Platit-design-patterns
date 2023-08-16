public class InFieldPawnMoveState : IPawnMoveState
{
    // Check field ahead
    // Check pawns diagonally
    // return result.
    public bool CanMove(Coordinate coordinate) => true;

    // Maybe move, maybe not.
    public void Move(Coordinate coordinate)
    {
    }
}
