using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
	public class Knight : Piece
	{
		

		public override PieceType Type => PieceType.Knight;
		public override Player Color { get; }

		public Knight(Player color)
		{
			Color = color;
		}


		public override Piece Copy()
		{
			Knight Copy = new Knight(Color);
			Copy.HasMoved = HasMoved;
			return Copy;
		}

		private static IEnumerable<Position> PotetionalPosition(Position from )
		{
			foreach(Direction vDir in new Direction[] {Direction.North, Direction.South} ) 
			{
				foreach(Direction hDir in new Direction[] {Direction.West, Direction.East} )
				{
					yield return from + 2 * vDir + hDir;
					yield return from + 2 * hDir + vDir;

				}
			}
		}
		private IEnumerable<Position>MovePosition(Position from, Board board)
		{
			return PotetionalPosition(from).Where(pos => Board.IsInside(pos)
			&& (board.IsEmpty(pos) || board[pos].Color != Color));
		}

		public override IEnumerable<Move> GetMoves(Position from , Board board)
		{
			return MovePosition(from, board).Select(to => new NormalMove(from, to));
		}
	}

}
