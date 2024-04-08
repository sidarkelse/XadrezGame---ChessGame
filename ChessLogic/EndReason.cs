namespace ChessLogic
{
	public enum  EndReason
	{
		CheckMate,//cheque mate
		Stalemate, //afogamento
		FiftyMoveRule, //50lances = empate
		InsufficientMaterial, //empate por material insuficiente
		ThreefoldRepetition, //só poode repetir a jogada 3veze, após isso = empate

	}
}
