namespace Chess.Pieces;

public class Rook : Piece
{
    public Rook(bool isWhite) : base(isWhite)
    {
        Symbol = isWhite ? '♜' : '♖';
    }

    public override bool CanMove(Board board, int startX, int startY, int endX, int endY)
    {
        if (startX != endX && startY != endY)
            return false;

        int stepX = startX == endX ? 0 : (endX > startX ? 1 : -1);
        int stepY = startY == endY ? 0 : (endY > startY ? 1 : -1);       

        int x = startX + stepX;
        int y = startY + stepY;

       while (x != endX || y != endY)
        {
            if (board.Squares[x,y] != null)
                return false;
            x += stepX;
            y += stepY;
        }

       return board.Squares[endX, endY] == null || board.Squares[endX, endY].IsWhite != this.IsWhite;

    }
}
