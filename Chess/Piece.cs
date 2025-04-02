using Chess.Pieces;

namespace Chess;

public abstract class Piece
{
    public bool IsWhite { get; set; }
    public char Symbol { get; set; }
    public bool HasMoved { get; set; } = false;

    public Piece(bool isWhite)
    {
        IsWhite = isWhite;
    }

    public abstract bool CanMove(Board board, int startX, int startY, int endX, int endY);

    public bool Target(Board board, int endX, int endY)
    {
        if (endX < 0 || endX > 7 || endY < 0 || endY > 7)
            return false;

        var targetPiece = board.Squares[endX, endY];

        if (targetPiece == null)
            return true;

        return targetPiece.IsWhite != this.IsWhite;
    }

    public bool CanCastle(int startX, int endX, int startY, int endY, Board board)
    {
        if (this.HasMoved)
            return false;

        if (startX != endX)
            return false;

        if (endY == startY + 2)
        {
            Piece? rook = board.Squares[startX, 7];
            if (rook is Rook && !rook.HasMoved &&
                board.Squares[startX, 5] == null && 
                board.Squares[startX, 6] == null)
            {
                return true;
            }
        }

        if (endY == startY - 2)
        {
            Piece? rook = board.Squares[startX, 0];
            if (rook is Rook && !rook.HasMoved &&
                board.Squares[startX, 1] == null &&
                board.Squares[startX, 2] == null &&
                board.Squares[startX, 3] == null)
            {
                return true;
            }
        }

        return false;
    }
}
