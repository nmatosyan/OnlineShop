namespace Chess.Pieces
{
    public class King : Piece
    {
        Board board = new Board();
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
                return board.Squares[endX, endY] == null || board.Squares[endX, endY].IsWhite != this.IsWhite;
            }

            return Target(board, endX, endY);

        }

        //public bool IsKingInCheck(bool isWhite)
        //{            
        //    int kingX = -1, kingY = -1;
        //    for (int x = 0; x < 8; x++)
        //    {
        //        for (int y = 0; y < 8; y++)
        //        {
        //            var piece = board.Squares[x, y];
        //            if (piece != null && piece is King && piece.IsWhite == isWhite)
        //            {
        //                kingX = x;
        //                kingY = y;
        //            }
        //        }
        //    }

        //    for (int x = 0; x < 8; x++)
        //    {
        //        for (int y = 0; y < 8; y++)
        //        {
        //            var enemyPiece = board.Squares[x, y];
        //            if (enemyPiece != null && enemyPiece.IsWhite != isWhite)
        //            {
        //                if (enemyPiece.CanMove(board, x, y, kingX, kingY))
        //                    return true;
        //            }
        //        }
        //    }
        //    return false;
        //}

        //public bool HasAnyLegalMove(bool isWhite)
        //{
        //    for (int x = 0; x < 8; x++)
        //    {
        //        for (int y = 0; y < 8; y++)
        //        {
        //            var piece = board.Squares[x, y];
        //            if (piece != null && piece.IsWhite == isWhite)
        //            {
        //                for (int newX = 0; newX < 8; newX++)
        //                {
        //                    for (int newY = 0; newY < 8; newY++)
        //                    {
        //                        if (piece.CanMove(board, x, y, newX, newY))
        //                        {
        //                            var saved = board.Squares[newX, newY];
        //                            board.Squares[newX, newY] = piece;
        //                            board.Squares[x, y] = null;
        //                            bool check = IsKingInCheck(isWhite);
                                    
        //                            board.Squares[x, y] = piece;
        //                            board.Squares[newX, newY] = saved;

        //                            if (!check)
        //                                return true; 
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    return false; 
        //}
    }
}
