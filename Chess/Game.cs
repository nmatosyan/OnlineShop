using Chess.ChessGame;
using Chess.CHessGame;
using Chess.Pieces;

namespace Chess;

public class Game
{
    private Board board = new Board();

    private bool isWhiteTurn = true;

    public void Start()
    {
        //using (var db = new ChessDbContext())
        //{
        //    var games = db.ChessGames.ToList();
        //    foreach (var game in games)
        //    {
        //        Console.WriteLine($"{game.Moves} {game.Date}");
        //    }
        //}

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
                if (board.MovePiece(parts[0], parts[1], isWhiteTurn))
                {
                    isWhiteTurn = !isWhiteTurn;
                }
                else
                {
                    Console.WriteLine("Try again");
                    Console.ReadKey();
                }
            }

            using (var db = new ChessDbContext())
            {
                var game = new ChessGameDb { Moves = allMoves.Trim(), Date = DateTime.UtcNow };
                db.ChessGames.Add(game);
                db.SaveChanges();
                Console.WriteLine("Партия сохранена в базу данных!");
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
