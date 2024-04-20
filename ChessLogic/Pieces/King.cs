namespace ChessLogic
{
	public  class King : Piece
	{
		
		public override PieceType Type => PieceType.King;
		public override Player Color  {get;}

		public static readonly Direction[] dirs = new Direction[]
		{
			Direction.North,
			Direction.South,
			Direction.East,
			Direction.West,
			Direction.NorthEast,
			Direction.SouthEast,
			Direction.SouthWest,
			Direction.NorthWest,
		};

		public King(Player color)
		{
			Color = color;
		}

		private static bool IsUnmovedRook(Position pos, Board board)//verifica se a torre não foi movida
		{
			if(board.IsEmpty(pos))
			{
				return false;
			}
			Piece piece = board[pos];
			return piece.Type == PieceType.Rook && !piece.HasMoved;
		}

		private static bool AllEmpty(IEnumerable<Position>positions,Board board)
		{
			return positions.All(pos => board.IsEmpty(pos));

		}
		private bool CanCastleKingSide(Position from,Board board)//Verifica se o rei não foi movido
		{
			if(HasMoved)
			{
				return false;	
			}
			Position rookPos = new Position(from.Row, 7);
			Position[] betweenPositions = new Position[] { new(from.Row, 5), new(from.Row, 6) };

			return IsUnmovedRook(rookPos,board)&& AllEmpty(betweenPositions, board);

		}
		private bool canCastleQueenSide(Position from, Board board)//Verifica se o roque pro lado da rainha é valido
		{
			if(HasMoved)
			{
				return false;	
			}
			Position rookPos = new Position(from.Row, 0);
			Position[] betweenPositions = new Position[] { new(from.Row, 1), new(from.Row, 2), new(from.Row,3)};

			return IsUnmovedRook(rookPos, board) && AllEmpty(betweenPositions, board);
		}

		public override Piece Copy()
		{
			King Copy = new King(Color);
			Copy.HasMoved = HasMoved;
			return Copy;

		}

		private IEnumerable<Position> MovePositions(Position from, Board board)
		{
			foreach (Direction dir in dirs) 
			{
				Position to = from + dir;
				if(!Board.IsInside(to))
				{
					continue;
				}
				if(board.IsEmpty(to) || board[to].Color != Color)
				{
					yield return to;
				}
			}
		}
		public override IEnumerable<Move>GetMoves(Position from , Board board) 
		{
			foreach(Position to in MovePositions(from, board))
			{
				yield return new NormalMove(from, to);
			}

			if(CanCastleKingSide(from, board))
			{
				yield return new Castle(MoveType.CastleKS, from);

			}
			if(canCastleQueenSide(from, board))
			{
				yield return new Castle(MoveType.CastleQs, from);
			}
		}

		public override bool CanCacptureOpponnentKing(Position from, Board board)
		{
			return MovePositions(from, board).Any(to =>
			{
				Piece piece = board[to];
				return piece != null && piece.Type == PieceType.King;

			});
		}
	}
}
