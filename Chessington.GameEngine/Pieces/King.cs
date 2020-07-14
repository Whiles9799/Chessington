using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class King : Piece
    {
        public King(Player player)
            : base(player)
        {
        }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            var currentPos = board.FindPiece(this);
            var currentRow = currentPos.Row;
            var currentCol = currentPos.Col;
            List<Square> availableMoves = new List<Square>();

            foreach (var rowDiff in Enumerable.Range(-1, 3))
            {
                foreach (var colDiff in Enumerable.Range(-1, 3))
                {
                    if (board.CanMoveOrTake(Square.At(currentRow + rowDiff, currentCol + colDiff),this))
                    {
                        availableMoves.Add(Square.At(currentRow + rowDiff, currentCol + colDiff));
                    }
                }
            }

            availableMoves.RemoveAll(x => x == currentPos);

            return availableMoves;
        }
    }
}