﻿namespace ChessLogic
{
	public class Pawn : Piece
	{
		public override PieceType Type => PieceType.Pawn;
		public override Player Color { get; }
		private readonly Direction forward; //avançar
		public Pawn(Player color)
		{
			Color = color;

			if (color == Player.White)
			{
				forward = Direction.North;
			}
			else if (color == Player.Black)
			{
				forward = Direction.South;
			}

		}

		public override Piece Copy()
		{
			Pawn Copy = new Pawn(Color);
			Copy.HasMoved = HasMoved; // copy
			return Copy;
		}
		private static bool CanMoveTo(Position pos, Board board)
		{
			return Board.IsInside(pos) && board.IsEmpty(pos);

		}

		private bool CanCaptureAt(Position pos, Board board)
		{
			if (!Board.IsInside(pos) || board.IsEmpty(pos))
			{
				return false;
			}
			return board[pos].Color != Color;

		}

		private static IEnumerable<Move> PromotionMoves(Position from, Position to)
		{
			yield return new PawnPromotion(from, to, PieceType.Knight);
			yield return new PawnPromotion(from, to, PieceType.Bishop);
			yield return new PawnPromotion(from, to, PieceType.Rook);
			yield return new PawnPromotion(from, to, PieceType.Queen);




		}

		private IEnumerable<Move> ForwardMoves(Position from, Board board)
		{
			Position oneMovesPos = from + forward;
			if (CanMoveTo(oneMovesPos, board))
			{
				if (oneMovesPos.Row == 0 || oneMovesPos.Row == 7)
				{
					foreach (Move promoMove in PromotionMoves(from, oneMovesPos))
					{
						yield return promoMove;
					}
				}

				else
				{
                  yield return new NormalMove(from, oneMovesPos);
				}


				Position twoMovesPos = oneMovesPos + forward;

				if (!HasMoved && CanMoveTo(twoMovesPos, board))
				{
					yield return new DoublePawn(from, twoMovesPos);
				}
			}


		}
		private IEnumerable<Move> DiagonalMoves(Position from, Board board)
		{
			foreach (Direction dir in new Direction[] { Direction.West, Direction.East })
			{
				Position to = from + forward + dir;

				if(to == board.GetPawnSkipPosition(Color.Oponnent()))
				{
					yield return new EnPassant(from, to);
				}
				else if (CanCaptureAt(to, board))
				{
					if (to.Row == 0 || to.Row == 7)
					{
						foreach (Move promoMove in PromotionMoves(from, to))
						{
							yield return promoMove;
						}
					}

					else
					{
						yield return new NormalMove(from, to);
					}
				}
			}
		}
		public override IEnumerable<Move> GetMoves(Position from, Board board)
		{

			return ForwardMoves(from, board).Concat(DiagonalMoves(from, board));
		}

		public override bool CanCacptureOpponnentKing(Position from, Board board)
		//verifica se  tem o rei no caminho, se tiver retorna true e se não false.
		{
			return DiagonalMoves(from, board).Any(move =>
			{
				Piece piece = board[move.ToPos];
				return piece != null && piece.Type == PieceType.King;

			}

			);
		}
	}
}
