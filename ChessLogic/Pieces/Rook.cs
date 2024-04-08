﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
	public  class Rook : Piece
	{
		public override PieceType Type => PieceType.Rook;
		public override Player Color { get; }

		private static readonly Direction[] dirs = new Direction[]
		{
			Direction.North,
			Direction.South,
			Direction.East,
			Direction.West,
		};
		public Rook(Player color) 
		
		{
			Color = color;
		}

		public override Piece Copy()
		{
           Rook Copy = new Rook(Color);
			Copy.HasMoved  = HasMoved;
			return Copy;
		}

		public override IEnumerable<Move>GetMoves(Position from, Board board)
		{
			return MovePositionInDirs(from, board,dirs).Select(to => new NormalMove(from,to));
		}

	}
}
