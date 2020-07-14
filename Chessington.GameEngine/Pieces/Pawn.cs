using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Pawn : Piece
    {
        
        public Pawn(Player player) 
            : base(player) { }


        public void PawnPromote(Board board, Square currentSquare, Square newSquare)
        {
            board.RemovePiece(currentSquare);
            board.AddPiece(newSquare, new Queen(board.CurrentPlayer));
        }
        
        public IEnumerable<Square> PawnMove(Board board, int direction)
        {
            var currentPos = board.FindPiece(this);
            var currentRow = currentPos.Row;
            var currentCol = currentPos.Col;

            if (board.CanMoveTo(Square.At(currentRow + direction, currentCol)))
            {
                yield return Square.At(currentRow + direction, currentCol);

                if (!HasMoved &&
                    board.CanMoveTo(Square.At(currentRow + 2 * direction, currentCol)))
                {
                    yield return Square.At(currentRow + 2 * direction, currentCol);
                }
            }
        }

        public IEnumerable<Square> PawnTake(Board board, int direction)
        {
            var currentPos = board.FindPiece(this);
            var currentRow = currentPos.Row;
            var currentCol = currentPos.Col;

            var diagonallyRight = Square.At(currentRow + direction, currentCol + 1);
            var diagonallyLeft = Square.At(currentRow + direction, currentCol - 1);
            if (board.CanMoveOrTake(diagonallyRight, this) && board.IsOccupied(diagonallyRight))
            {
                yield return diagonallyRight;
            }
            if (board.CanMoveOrTake(diagonallyLeft, this) && board.IsOccupied(diagonallyLeft))
            {
                yield return diagonallyLeft;
            }
        }
        
        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            if (Player == Player.White)
            {
                return PawnMove(board, -1).Concat(PawnTake(board, -1));
            }
            else
            {
                return PawnMove(board, 1).Concat(PawnTake(board, 1));
            }
        }
    }
}