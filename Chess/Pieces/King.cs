namespace Chess.Pieces
{
    public class King : Piece
    {
        public King(bool IsWhite) : base(IsWhite)
        {
            Symbol = IsWhite ? '♚' : '♔';
        }      

        public override bool CanMove(Board board, int startX, int startY, int endX, int endY)
        {
            int dx = Math.Abs(endX - startX);
            int dy = Math.Abs(endY - startY);

            if ((dx <= 1 && dy <= 1) && (dx + dy > 0))
            {
                return board.Squares[endX, endY] == null || board.Squares[endX, endY]?.IsWhite != this.IsWhite;
            }

            return Target(board, endX, endY);

        }        
    }
}
