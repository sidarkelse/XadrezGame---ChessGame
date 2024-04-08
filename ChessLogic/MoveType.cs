namespace ChessLogic
{
	public enum MoveType
	{
		Normal, //Movimento normal
		CastleKS,//Roque lado do rei
		CastleQs,//Roque lado da rainha
		DoublePawn, //Peão avança 2 casas
		EnPassant, //peão pode tomar na vertical
		PawnPromotion //Promação de peão
	}
}
