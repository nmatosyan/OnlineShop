using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.Pieces;

namespace Chess
{
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
                             
            }
        }
    }
}
