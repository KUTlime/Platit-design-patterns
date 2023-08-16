public class Pawn
{
    private IPawnMoveState _pawnMoveState = new InitialPawnMoveState();

    public void MovePawnTwoFieldsAhead(char x)
    {
        _pawnMoveState.Move(new Coordinate(x, 4));
        _pawnMoveState = new InFieldPawnMoveState();
    }

    public void MovePawnOneFieldsAhead(char x)
    {
        _pawnMoveState.Move(new Coordinate(x, 3));
        _pawnMoveState = new InFieldPawnMoveState();
    }
}
