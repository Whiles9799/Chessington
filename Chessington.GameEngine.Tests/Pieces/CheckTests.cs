using Chessington.GameEngine.Pieces;
using FluentAssertions;
using NUnit.Framework;

namespace Chessington.GameEngine.Tests.Pieces
{
    [TestFixture]
    public class CheckTests
    {
        [Test]
        public void Kings_CannotWalkIntoACheck()
        {
            var board = new Board();
            var king = new King(Player.White);
            board.AddPiece(Square.At(4, 4), king);
            var rook = new Rook(Player.Black);
            board.AddPiece(Square.At(0, 3), rook);

            var moves = king.GetAvailableMoves(board);
            moves.Should().NotContain(Square.At(4,3));
        }
    }
}