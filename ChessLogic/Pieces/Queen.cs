

using System.Reflection;

namespace ChessLogic
{
	public class Queen : Piece
	{

		public override PieceType Type => PieceType.Queen;
		public override Player Color { get; }

		private static readonly Direction[] dirs = new Direction[]
		{
			Direction.North,
			Direction.South,
			Direction.East,
			Direction.West,
			Direction.NorthEast,
			Direction.NorthWest,
			Direction.SouthWest,
			Direction.SouthEast

		};

		public Queen(Player color)
		{
			Color = color;
		}

		public override Piece Copy()
		{
			Queen Copy = new Queen(Color);
			Copy.HasMoved = HasMoved;
			return Copy;

		}
		public override IEnumerable<Move> GetMoves(Position from, Board board)
		{
			return MovePositionInDirs(from, board, dirs).Select(to => new NormalMove(from, to));// retorna uma coleção de movimentos normais- De posição de origem para posição de destino
																								//Calcula todas as posições possíveis para mover a peça a partir da posição de origem (from) em direções específicas (dirs).
		}
	}
}
