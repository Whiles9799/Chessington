using System;
using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public abstract class Piece
    {
        public bool HasMoved { get; set; }
        
        protected Piece(Player player)
        {
            Player = player;
        }

        public Player Player { get; private set; }

        public abstract IEnumerable<Square> GetAvailableMoves(Board board);

        public void MoveTo(Board board, Square newSquare)
        {
            var currentSquare = board.FindPiece(this);
            board.MovePiece(currentSquare, newSquare);
            HasMoved = true;
        }

        public IEnumerable<Square> GetDiagonalMoves(Board board)
        {
            List<Square> availableMoves = new List<Square>();

            availableMoves.AddRange(ExploreInOneDirection(board, x =>  Square.At(x.Row+1, x.Col+1)));
            availableMoves.AddRange(ExploreInOneDirection(board, x =>  Square.At(x.Row+1, x.Col-1)));
            availableMoves.AddRange(ExploreInOneDirection(board, x =>  Square.At(x.Row-1, x.Col+1)));
            availableMoves.AddRange(ExploreInOneDirection(board, x =>  Square.At(x.Row-1, x.Col-1)));

            return availableMoves;
        }

        public IEnumerable<Square> GetLateralMoves(Board board)
        {
            List<Square> availableMoves = new List<Square>();

            availableMoves.AddRange(ExploreInOneDirection(board, x =>  Square.At(x.Row+1, x.Col)));
            availableMoves.AddRange(ExploreInOneDirection(board, x =>  Square.At(x.Row-1, x.Col)));
            availableMoves.AddRange(ExploreInOneDirection(board, x =>  Square.At(x.Row, x.Col+1)));
            availableMoves.AddRange(ExploreInOneDirection(board, x =>  Square.At(x.Row, x.Col-1)));

            return availableMoves;
        }

        public IEnumerable<Square> ExploreInOneDirection(Board board, Func<Square, Square> iterator)
        {
            var nextSquare = iterator(board.FindPiece(this));
            while (board.CanMoveOrTake(nextSquare, this))
            {
                yield return nextSquare;
                if (board.IsOccupied(nextSquare))
                {
                    break;
                }
                nextSquare = iterator(nextSquare);
            } 
        }
    }
}