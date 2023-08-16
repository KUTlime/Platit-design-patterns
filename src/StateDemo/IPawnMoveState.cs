public interface IPawnMoveState
{
    bool CanMove(Coordinate coordinate);

    void Move(Coordinate coordinate);
}
