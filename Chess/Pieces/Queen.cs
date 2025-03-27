namespace Chess.Pieces;

public class Queen : Piece
{
    public Queen (bool IsWhite ) : base (IsWhite)
    {
        Symbol = IsWhite ? '♛' : '♕';
    }

    public override bool CanMove(Board board, int startX, int startY, int endX, int endY)
    {
        Bishop bishop = new Bishop(IsWhite);
        if (bishop.CanMove(board, startX, startY, endX, endY))
            return true;

        Rook rook = new Rook(IsWhite);
        if (rook.CanMove(board, startX, startY, endX, endY))
            return true;

        return Target(board, endX, endY);
    }
}
