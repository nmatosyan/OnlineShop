namespace Chess.Pieces
{
    public class Pawn : Piece
    {
        public Pawn(bool IsWhite) : base(IsWhite)
        {
            Symbol = IsWhite ? '♟' : '♙';
        }

        public override bool CanMove(Board board, int startX, int startY, int endX, int endY)
        {
            int direction = IsWhite ? -1 : 1;
            int startrow = IsWhite ? 6 : 1;

            if (startY == endY && board.Squares[endX, endY] == null)
            {
                if (endX == startX + direction)
                    return true;

                if (startX == startrow && endX == startX + 2 * direction && board.Squares[startX + direction, startY] == null)
                    return true;

            }           

            if (Math.Abs(endY - startY) == 1 && endX == startX + direction)
            {
                if (board.Squares[endX, endY] != null && board.Squares[endX, endY].IsWhite != this.IsWhite)
                    return true;
            }

            return false;
        }
    }
}
