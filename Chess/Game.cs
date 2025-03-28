using Chess.CHessGame;
using Chess.Pieces;

namespace Chess;

public class Game
{
    private Board board = new Board();    
    
    private bool isWhiteTurn = true;

    public void Start()
    {
        board.Initialize();

        while (true)
        {
            Console.Clear();
            board.Print();
            Console.WriteLine($"{(isWhiteTurn ? "White" : "Black")}'s turn. Enter move (e.g. e2 e4): ");
            string allMoves = "";
            var input = Console.ReadLine();
            if (input == null)
            {
                continue;
            }

            var parts = input.Split(' ');
            if (parts.Length == 2)
            {
                if (board.MovePiece(parts[0], parts[1]))
                {
                    isWhiteTurn = !isWhiteTurn;
                }
                else
                {
                    Console.WriteLine("Try again");
                    Console.ReadKey();
                }
            }
             
           

            //if (board.IsKingInCheck(true))
            //{
            //    if (board.HasAnyLegalMove(true))
            //    {
            //        Console.WriteLine("Check!");
            //    }

            //    else
            //    {
            //        Console.WriteLine("Mat! Game over.");
            //    }
            //}

            //if (board.IsKingInCheck(false) && board.HasAnyLegalMove(false))
            //{
            //    Console.WriteLine("Pat! Draw.");
            //}

        }
    }
}
