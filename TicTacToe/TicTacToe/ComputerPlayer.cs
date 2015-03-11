using System;

namespace TicTacToe
{
    /// <summary>
    /// A computer tic-tac-toe player
    /// </summary>
    public class ComputerPlayer
    {
        Random rand = new Random();

        /// <summary>
        /// Constructor
        /// </summary>
        public ComputerPlayer()
        {
        }

        /// <summary>
        /// Takes a turn, placing an O in the best square
        /// </summary>
        /// <param name="board">the board</param>
        public void TakeTurn(Board board)
        {
            // place O in a random open square
            BoardSquare square = board.GetSquare(rand.Next(3), rand.Next(3));
            while (!square.Empty)
            {
                square = board.GetSquare(rand.Next(3), rand.Next(3));
            }
            square.Contents = SquareContents.O;
        }
    }
}
