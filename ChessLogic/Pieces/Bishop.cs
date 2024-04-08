﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
	public class Bishop : Piece
	{
		 public override  PieceType Type => PieceType.Bishop;
		 public  override Player Color { get; }

		private static readonly Direction[] dirs = new Direction[]
		{
			 Direction.NorthWest,
			 Direction.NorthEast,
			 Direction.SouthWest,
			 Direction.SouthEast,


		};
		public Bishop(Player color)
		
		{
			Color = color;
		}

		public override Piece Copy() //garante que ao criar uma copia do bispo, Esses atributos sejam preservados
		{
			Bishop copy = new Bishop(Color); 
			copy.HasMoved = HasMoved;
			return copy;
		}

		public override  IEnumerable<Move>GetMoves(Position from, Board board)
		{
			return MovePositionInDirs(from, board, dirs).Select(to => new NormalMove(from, to));
		}
	}
}
