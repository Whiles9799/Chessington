using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Pawn : Piece
    {
        
        public Pawn(Player player) 
            : base(player) { }

        public IEnumerable<Square> PawnMove(Board board, int direction, int startingRow)
        {
            var currentPos = board.FindPiece(this);
            var currentRow = currentPos.Row;
            var currentCol = currentPos.Col;

            if (!Square.At(currentRow + direction, currentCol).IsOccupied(board))
            {
                yield return Square.At(currentRow + direction, currentCol);

                if (currentRow == startingRow &&
                    !Square.At(currentRow + 2 * direction, currentCol).IsOccupied(board))
                {
                    yield return Square.At(currentRow + 2 * direction, currentCol);
                }
            }
        }
        
        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            if (Player == Player.White)
            {
                return PawnMove(board, -1, GameSettings.BoardSize - 2);
            }
            else
            {
                return PawnMove(board, 1, 1);
            }
        }
    }
}