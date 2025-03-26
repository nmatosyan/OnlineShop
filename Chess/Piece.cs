
namespace Chess
{
    public abstract class Piece
    {
        public bool IsWhite { get; set; }
        public char Symbol { get; set; }

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
    }
}
