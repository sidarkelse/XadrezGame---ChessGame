namespace ChessLogic.Helpers
{
    public class Utils
    {
        public static Move GetMoveInstance(MoveData moveData)
        {
            return moveData.Type switch
            {
                MoveType.Normal => new NormalMove(moveData.FromPos, moveData.ToPos),
                MoveType.CastleKS or MoveType.CastleQs => new Castle(moveData.Type, moveData.FromPos),
                MoveType.DoublePawn => new DoublePawn(moveData.FromPos, moveData.ToPos),
                MoveType.EnPassant => new EnPassant(moveData.FromPos, moveData.ToPos),
                MoveType.PawnPromotion => new PawnPromotion(moveData.FromPos, moveData.ToPos, PieceType.Pawn),
                _ => null
            };
        }
    }
}
