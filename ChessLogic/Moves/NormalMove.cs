using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
	public class NormalMove : Move
	{
	

		public override MoveType Type => MoveType.Normal;
		public override Position FromPos { get; }
		public override Position ToPos { get; }

		public NormalMove(Position from, Position to)
		{
			FromPos = from;
			ToPos = to;
		}

		public override bool Execute(Board board)
		{
			Piece piece = board[FromPos]; //Armazena a peça na posição de origem.
			bool capture =!board.IsEmpty(ToPos);
			board[ToPos] = piece; // Move a peça para a posição de destino.
			board[FromPos] = null; //Remove a peça da posição de origem.
			piece.HasMoved = true; //Marca a peça como movida

			return capture || piece.Type == PieceType.Pawn;

		}
	}
}
