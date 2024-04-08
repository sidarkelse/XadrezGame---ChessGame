namespace ChessLogic
{
	public  class GameResult
	{
	
		public Player Winner { get; }
		public EndReason Reason { get; }

		public GameResult(Player winner, EndReason reason)
		{
			Winner = winner;
			Reason = reason;
		}

		public static GameResult Win(Player winner)
		{
			return new GameResult(winner, EndReason.CheckMate);
		}

		public static GameResult Draw(EndReason reason)
		{
			return new GameResult(Player.None, reason);
		}
	}
}
