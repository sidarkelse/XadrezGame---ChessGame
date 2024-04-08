using ChessLogic;
using System.Windows;
using System.Windows.Controls;

namespace ChessUI
{
	/// <summary>
	/// Interaction logic for GameOverMenu.xaml
	/// </summary>
	public partial class GameOverMenu : UserControl
	{
		public event Action<Option> OptionSelected;
		public GameOverMenu(GameState gameState)
		{
			InitializeComponent();

			GameResult gameresult = gameState.GameResult;
			WinnerText.Text = GetWinnerText(gameresult.Winner);
			ReasonText.Text = GetReasonText(gameresult.Reason, gameState.CurrentPlayer);
		}

		private static  string GetWinnerText(Player winner)
		{
			return winner switch
			{
				Player.White => "WHITE WINS!",
				Player.Black => "BlACK WINS!",
				 _ => "IT'S A DRAW",
			};
		}
		private  static string PlayerString(Player player)
		{
			return player switch
			{
				Player.White => "WHITE",
				Player.Black => "BLACK",
				_ => ""
			};
		}

		private static string GetReasonText(EndReason reason, Player currentPlayer)
		{
			return reason switch
			{ EndReason.Stalemate => $"STALMATE - {PlayerString(currentPlayer)} CAN'T MOVE",
			  EndReason.CheckMate =>$"CHECKMATE - {PlayerString(currentPlayer)} CAN'T MOVE",
			  EndReason.FiftyMoveRule => "FIFTY-MOVE RULE",
		      EndReason.InsufficientMaterial => "INSUFFICIENT MATERIAL",
			  EndReason.ThreefoldRepetition => "THREEFOLD REDEPTITION",
			  _=> ""
			
			};

		}
		private void Restart_Click(object sender, RoutedEventArgs e)
		{
			OptionSelected?.Invoke(Option.Restart);

		}

		private void Exit_Click(object sender, RoutedEventArgs e)
		{
			OptionSelected?.Invoke(Option.Exit);

		}
	}
}
