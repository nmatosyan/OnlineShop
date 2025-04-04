using System.Net.NetworkInformation;
using Chess.CHessGame;
using Chess.Pieces;

namespace Chess;

public class Board
{
    public Piece?[,] Squares { get; set; } = new Piece?[8,8]; 

    public void Initialize()
    {
        Squares[0, 0] = new Rook(false);
        Squares[0, 1] = new Knight(false);
        Squares[0, 2] = new Bishop(false);
        Squares[0, 3] = new Queen(false);
        Squares[0, 4] = new King(false);
        Squares[0, 5] = new Bishop(false);
        Squares[0, 6] = new Knight(false);
        Squares[0, 7] = new Rook(false);
        for (int i = 0; i < 8; i++)
            Squares[1, i] = new Pawn(false);

        Squares[7, 0] = new Rook(true);
        Squares[7, 1] = new Knight(true);
        Squares[7, 2] = new Bishop(true);
        Squares[7, 3] = new Queen(true);
        Squares[7, 4] = new King(true);
        Squares[7, 5] = new Bishop(true);
        Squares[7, 6] = new Knight(true);
        Squares[7, 7] = new Rook(true);
        for (int i = 0; i < 8; i++)
            Squares[6, i] = new Pawn(true);

        for (int row = 2; row <= 5; row++)
            for (int col = 0; col < 8; col++)
                Squares[row, col] = null;
    }

    public void Print()
    {        
        Console.SetCursorPosition(0, 0);
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("    A   B   C   D   E   F   G   H");
        Console.WriteLine("  |---+---+---+---+---+---+---+---|");

        for (int row = 0; row < 8; row++)
        {
            Console.Write($"{8 - row} |");

            for (int col = 0; col < 8; col++)
            {                
                var piece = Squares[row, col];
                if (piece == null)                
                    Console.Write("   |");               
                else                               
                    Console.Write($" {piece?.Symbol} |");                           
            }

            Console.WriteLine($" {8 - row}");
            Console.WriteLine("  |---+---+---+---+---+---+---+---|");
        }

        Console.WriteLine("    A   B   C   D   E   F   G   H");             
    }

    public static (int row, int col) ParsePosition(string position)
    {
        int col = position[0] - 'a';
        int row = 8 - (position[1] - '0');
        return (row, col);
    }

    public bool MovePiece(string from, string to, bool IsWhite)
    {
        var (startX, startY) = ParsePosition(from);
        var (endX, endY) = ParsePosition(to);

        var piece = Squares[startX, startY];
        if (piece == null)
        {
            Console.WriteLine("There is no figure in the initial position.");
            return false;
        }

        if (piece.CanMove(this, startX, startY, endX, endY))
        {
            Squares[endX, endY] = piece;
            Squares[startX, startY] = null;
            return true;
        }

        if (piece is King king && king.CanCastle(startX, endX, startY, endY, this))
        {
            bool isShortCastle = (endY == startY + 2);

            Squares[endX, endY] = piece;
            Squares[startX, startY] = null;

            if (isShortCastle)
            {
                Squares[startX, 5] = Squares[startX, 7];
                Squares[startX, 7] = null;
            }

            else
            {
                Squares[startX, 3] = Squares[startX, 7];
                Squares[startX, 7] = null;
            }

            Console.WriteLine("Castle complited!");
            return true;
        }

        else
        {
            Console.WriteLine("Invalid move");
            return false;
        }
    }

    public bool IsKingInCheck(bool isWhite)
    {
        int kingX = -1, kingY = -1;
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                var piece = Squares[x, y];
                if (piece != null && piece is King && piece.IsWhite == isWhite)
                {
                    kingX = x;
                    kingY = y;
                }
            }
        }

        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                var enemyPiece = Squares[x, y];
                if (enemyPiece != null && enemyPiece.IsWhite != isWhite)
                {
                    if (enemyPiece.CanMove(this, x, y, kingX, kingY))
                        return true;
                }
            }
        }

        return false;
    }

    public bool HasAnyLegalMove(bool isWhite)
    {
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                var piece = Squares[x, y];
                if (piece != null && piece.IsWhite == isWhite)
                {
                    for (int newX = 0; newX < 8; newX++)
                    {
                        for (int newY = 0; newY < 8; newY++)
                        {
                            if (piece.CanMove(this, x, y, newX, newY))
                            {
                                var saved = Squares[newX, newY];
                                Squares[newX, newY] = piece;
                                Squares[x, y] = null;
                                bool check = IsKingInCheck(isWhite);

                                Squares[x, y] = piece;
                                Squares[newX, newY] = saved;

                                if (!check)
                                    return true;
                            }
                        }
                    }
                }
            }
        }

        return false;
    }
}
