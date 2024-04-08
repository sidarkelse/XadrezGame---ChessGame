
namespace ChessLogic
{
	public enum Player
	{
		None, //seta o player no caso de vitoria ou empate
		White,
		Black

	}

	public static class PlayerExtensions
	{
		public static Player Oponnent(this Player player)
		{
			return player switch
			{
				Player.White => Player.Black,
				Player.Black => Player.White,
				_ => Player.None,
			};
		}
	}

}
