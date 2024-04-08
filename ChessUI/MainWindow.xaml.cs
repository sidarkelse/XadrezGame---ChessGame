using ChessLogic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
namespace ChessUI
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private readonly Image[,] PieceImages = new Image[8, 8];
		private readonly Rectangle[,] highlights = new Rectangle[8, 8]; //destacar as possíveis posições para onde uma peça pode se mover
		private readonly Dictionary<Position, Move> moveCache = new Dictionary<Position, Move>();


		private GameState gameState;
		private Position selectdPos = null;
		public MainWindow()
		{
			InitializeComponent();
			InitializeBoard();

			gameState = new GameState(Player.White, Board.Initial());
			DrawBoard(gameState.Board);// inicializar tabuleiro original sem peças movidas

			SetCursor(gameState.CurrentPlayer);

		}
		private void InitializeBoard() //Inicialização das peças no tabuleiro
		{
			for (int r = 0; r < 8; r++)
			{
				for (int c = 0; c < 8; c++)
				{
					Image image = new Image();
					PieceImages[r, c] = image;
					PieceGrid.Children.Add(image);

					Rectangle highlight = new Rectangle();
					highlights[r, c] = highlight;
					HighLightGrid.Children.Add(highlight);
				}
			}
		}
		private void DrawBoard(Board board) //atualiza o tabuleiro após uma jogada
		{
			for (int r = 0; r < 8; r++)
			{
				for (int c = 0; c < 8; c++)
				{
					Piece piece = board[r, c];
					PieceImages[r, c].Source = Images.GetImage(piece);

				}
			}
		}

		private void BoardGrid_MouseDown(object sender, MouseButtonEventArgs e) //quando clica em qualquer lugar no Tabuleiro
		{

			if (IsMenuOnScreen())
			{
				return;
			}

			Point point = e.GetPosition(BoardGrid);
			Position pos = ToSquarePosition(point);

			if (selectdPos == null)
			{
				OnFromPositionSelected(pos);

			}
			else
			{
				OnToPositionSelected(pos);
			}

		}

		private Position ToSquarePosition(Point point)
		{
			double squareSize = BoardGrid.ActualHeight / 8;

			int row = (int)(point.Y / squareSize);
			int col = (int)(point.X / squareSize);
			return new Position(row, col);
		}

		private void OnFromPositionSelected(Position pos)
		{
			IEnumerable<Move> moves = gameState.LegalMovesForPieces(pos);

			if (moves.Any())
			{
				selectdPos = pos;
				CacheMoves(moves);
				ShowHighLights();

			}
		}
		private void OnToPositionSelected(Position pos)
		{
			selectdPos = null;
			HideHighlight();

			if (moveCache.TryGetValue(pos, out Move move))
			{
				if (move.Type == MoveType.PawnPromotion)
				{
					HandlePromotion(move.FromPos, move.ToPos);
				}
				else
				{
					HandleMove(move);

				}

			}
		}
		private void HandlePromotion(Position from, Position  to)
		{
			PieceImages[to.Row, to.Column].Source = Images.GetImage(gameState.CurrentPlayer, PieceType.Pawn);
			PieceImages[from.Row, from.Column].Source = null;

			PromotionMenu promMenu = new PromotionMenu(gameState.CurrentPlayer);
			MenuContainer.Content = promMenu;

			promMenu.PieceSelected += type =>
			{
				MenuContainer.Content = null;
				Move promMove = new PawnPromotion(from, to, type);
				HandleMove(promMove);

			};
		}

		private void HandleMove(Move move) //diz para o jogo como executar o movimento
		{

			gameState.MakeMove(move);
			DrawBoard(gameState.Board);
			SetCursor(gameState.CurrentPlayer);

			if (gameState.IsGameOver())
			{
				ShowGameOver();
			}


		}

		private void CacheMoves(IEnumerable<Move> moves) // responsável por armazenar os movimentos válidos disponíveis para a peça selecionada
		{
			moveCache.Clear();

			foreach (Move move in moves)
			{
				moveCache[move.ToPos] = move;

			}
		}
		private void ShowHighLights() // responsável por preencher esses retângulos com uma cor
		{

			Color color = Color.FromArgb(150, 125, 255, 000);
			foreach (Position to in moveCache.Keys)
			{
				highlights[to.Row, to.Column].Fill = new SolidColorBrush(color);

			}
		}
		private void HideHighlight()// é usada para limpar os retângulos destacados, removendo a cor 
		{
			foreach (Position to in moveCache.Keys)
			{
				highlights[to.Row, to.Column].Fill = Brushes.Transparent;
			}
		}
		private void SetCursor(Player player)
		{
			if (player == Player.White)
			{
				Cursor = ChessCursors.WhiteCursor;
			}
			else
			{
				Cursor = ChessCursors.BalckCursor;

			}
		}

		private bool IsMenuOnScreen()
		{
			return MenuContainer.Content != null;

		}

		private void ShowGameOver()
		{
			GameOverMenu gameOverMenu = new GameOverMenu(gameState);
			MenuContainer.Content = gameOverMenu;

			gameOverMenu.OptionSelected += option =>
			{
				if (option == Option.Restart)
				{
					MenuContainer.Content = null;
					RestartGame();
				}
				else
				{
					Application.Current.Shutdown();
				}
			};
		}
		private void RestartGame()
		{
			selectdPos = null;
			HideHighlight();
			moveCache.Clear();
			gameState = new GameState(Player.White, Board.Initial());
			DrawBoard(gameState.Board);
			SetCursor(gameState.CurrentPlayer);

		}

		private void Window_KeyDown(object sender, KeyEventArgs e)
		{
			if(!IsMenuOnScreen()&& e.Key == Key.Escape)
			{
				ShowPauseMenu();
			}
		
		}
		private void ShowPauseMenu()
		{
			PauseMenu pauseMenu = new PauseMenu();
			MenuContainer.Content = pauseMenu;

			pauseMenu.OptionSelected += option =>
			{
				MenuContainer.Content = null;
				if (option == Option.Restart)
				{
					RestartGame();
				}
			};
		}
	}
}