using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    public class Knight : Piece
    {
        public Knight(bool IsWhite) : base(IsWhite)
        {
            Symbol = IsWhite ? '♞' : '♘';
        }

        public override bool CanMove(Board board, int startX, int startY, int endX, int endY)
        {
            int dx = Math.Abs(endX - startX);
            int dy = Math.Abs(endY - startY);

            if ((dx == 2 && dy == 1) || (dx == 1 && dy == 2))
            {
                return board.Squares[endX, endY]== null || board.Squares[endX, endY].IsWhite != this.IsWhite;
            }          

            return Target(board, endX, endY);
        }
    }
}
