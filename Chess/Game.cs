using Chess.ChessGame;
using Chess.CHessGame;
using Chess.Pieces;
using Microsoft.EntityFrameworkCore;

namespace Chess;

public class Game
{
    private Board board = new Board();
    
    private bool isWhiteTurn = true;

    public void Start()
    {       
        board.Initialize();

        Console.Write("Enter White player name: ");
        string? whiteName = Console.ReadLine();

        Console.Write("Enter Black player name: ");
        string? blackName = Console.ReadLine();

        while (true)
        {
            int whiteId, blackId;

            using (var db = new ChessDbContext())
            {
                var whitePlayer = db.Users.FirstOrDefault(u => u.Name == whiteName)
                                  ?? new User { Name = whiteName };

                var blackPlayer = db.Users.FirstOrDefault(u => u.Name == blackName)
                                  ?? new User { Name = blackName };

                if (whitePlayer.UserId == 0) db.Users.Add(whitePlayer);
                if (blackPlayer.UserId == 0) db.Users.Add(blackPlayer);

                db.SaveChanges();

                whiteId = whitePlayer.UserId;
                blackId = blackPlayer.UserId;
            }

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
                var game = new ChessGameDb
                {
                    Moves = allMoves.Trim(),
                    Date = DateTime.UtcNow,
                    WhitePlayerId = whiteId,
                    BlackPlayerId = blackId
                };
                db.ChessGames.Add(game);
                db.SaveChanges();
            }

            using (var db = new ChessDbContext())
            {
                var games = db.ChessGames
                              .Include(g => g.WhitePlayer)
                              .Include(g => g.BlackPlayer)
                              .ToList();

                foreach (var game in games)
                {
                    Console.WriteLine($"Game #{game.Id}: {game.Moves} ({game.Date}) - White: {game.WhitePlayer?.Name}, Black: {game.BlackPlayer?.Name}");
                }
            }

            var moveText = $"{parts[0]} {parts[1]}"; 
            allMoves += moveText + " ";

            using (var db = new ChessDbContext())
            {                
                var lastGame = db.ChessGames.OrderByDescending(g => g.Id).FirstOrDefault();
                if (lastGame != null)
                {
                    var move = new MoveDb
                    {
                        Move = moveText,
                        MoveNumber = lastGame.MoveList.Count + 1,
                        GameId = lastGame.Id
                    };

                    db.Moves.Add(move);
                    db.SaveChanges();
                }
            }

            if (board.IsKingInCheck(true))
            {
                if (board.HasAnyLegalMove(true))
                {
                    Console.WriteLine("Check!");
                }

                else
                {
                    Console.WriteLine("Mat! Game over.");
                }
            }

            if (board.IsKingInCheck(false) && board.HasAnyLegalMove(false))
            {
                Console.WriteLine("Pat! Draw.");
            }
        }    
    }
}
