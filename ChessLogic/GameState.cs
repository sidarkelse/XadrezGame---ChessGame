using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
	public class GameState
	{

		public Board Board { get; }
		public Player CurrentPlayer { get; private set; }
		public GameResult GameResult { get; private set; } = null;

		private int noCaptureOrPawnMoves = 0;

		private string stateString;

		private readonly Dictionary<string, int>stateHistory = new Dictionary<string, int>();	


		public GameState(Player player, Board board)
		{
			CurrentPlayer = player;
			Board = board;


		    stateString = new StateString(CurrentPlayer,board).ToString();
			stateHistory[stateString] = 1;
		}

		public IEnumerable<Move> LegalMovesForPieces(Position pos)
		{
			if (Board.IsEmpty(pos) || Board[pos].Color != CurrentPlayer)
			{
				return Enumerable.Empty<Move>();
			}

			Piece piece = Board[pos];
			IEnumerable<Move> moveCandidates = piece.GetMoves(pos, Board);
			return moveCandidates.Where(move => move.IsLegal(Board));
		}

		public void MakeMove(Move move)
		{
			Board.SetPawnSkipPromotion(CurrentPlayer, null);
			bool capturePawn = move.Execute(Board);
			if (capturePawn)
			{
				noCaptureOrPawnMoves = 0;
				stateHistory.Clear();
			}
			else
			{
				noCaptureOrPawnMoves ++;
			}
			CurrentPlayer = CurrentPlayer.Oponnent();
			UpdateStateString();
			CheckForGameOver();

		}

		public IEnumerable<Move> AllLegalMovesFor(Player player)// retorna uma coleção de todos movimentos legais que o player pode fazer
		{
			IEnumerable<Move> moveCandidates = Board.PiecePositionsFor(player).SelectMany(pos =>
			{
				Piece piece = Board[pos];
				return piece.GetMoves(pos, Board);
			});

			return moveCandidates.Where(move => move.IsLegal(Board));
		}

		private void CheckForGameOver()
		{
			if (!AllLegalMovesFor(CurrentPlayer).Any())
			{
				if (Board.IsInCheck(CurrentPlayer))
				{
					GameResult = GameResult.Win(CurrentPlayer.Oponnent());
				}
				else
				{
					GameResult = GameResult.Draw(EndReason.Stalemate);
				}

			}
			else if(Board.InsufficientMaterial())
			{
				GameResult = GameResult.Draw(EndReason.InsufficientMaterial);
			}
			else if (FiftyMoveRule())
			{
				GameResult = GameResult.Draw(EndReason.FiftyMoveRule);
			}
			else if(ThreefoldRepetetion())
			{
				GameResult = GameResult.Draw(EndReason.ThreefoldRepetition);
			}
		}

		public bool IsGameOver()
		{
			return GameResult != null;

		}

		private bool FiftyMoveRule()
		{
			int fullMoves = noCaptureOrPawnMoves / 2;
			return fullMoves == 50;

		}

		private void UpdateStateString()
		{
			stateString = new StateString(CurrentPlayer, Board).ToString();

			if(!stateHistory.ContainsKey(stateString))
			{
				stateHistory[stateString] = 1;
			}
			else
			{
				stateHistory[stateString]++;
			}
		}
		private bool ThreefoldRepetetion()
		{
			return stateHistory[stateString] == 3;
		}
	}

}