namespace Chess.Pieces;

public class Bishop : Piece
{       
    
    public Bishop(bool isWhite) : base(isWhite)
    {
        
        Symbol = isWhite ? '♝' : '♗';
    }

    public override bool CanMove(Board board, int startX, int startY, int endX, int endY)
    {
        int deltaX = Math.Abs(endX - startX);
        int deltaY = Math.Abs(endY - startY);

        if (deltaX != deltaY)
            return false;

        int stepX = (endX > startX) ? 1 : -1;
        int stepY = (endY > startY) ? 1 : -1;

        int x = startX + stepX;
        int y = startY + stepY;

        while (x != endX && y != endY)
        {
            if (board.Squares[x, y] != null)
                return false;

            x += stepX;
            y += stepY;
        }

        if (board.Squares[endX, endY] == null || board.Squares[endX, endY].IsWhite != this.IsWhite)
            return true;

        return Target(board, endX, endY);
    }
}
