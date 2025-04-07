namespace Chess.Pieces;

public class Bishop : Piece
{       
    
    public Bishop(bool isWhite) : base(isWhite)
    {
        
        Symbol = isWhite ? '♝' : '♗';
    }

    public static bool IsInsideBoard(int x, int y)
    {
        return x >= 0 && x < 8 && y >= 0 && y < 8;
    }

    public override bool CanMove(Board board, int startX, int startY, int endX, int endY)
    {
        if (!IsInsideBoard(endX, endY))
            return false;

        int deltaX = Math.Abs(endX - startX);
        int deltaY = Math.Abs(endY - startY);

        if (deltaX != deltaY)
            return false;

        int stepX = (endX > startX) ? 1 : -1;
        int stepY = (endY > startY) ? 1 : -1;

        int x = startX + stepX;
        int y = startY + stepY;

        while (x != endX || y != endY)
        {
            if (!IsInsideBoard(x, y))
                return false;

            if (board.Squares[x, y] != null)
                return false;

            x += stepX;
            y += stepY;
        }

        var target = board.Squares[endX, endY];
        return target == null || target.IsWhite != this.IsWhite;
    }
}
