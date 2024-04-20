using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ChessLogic
{
	public abstract class Move
	{
		public abstract MoveType Type { get; } //Movimentos normais
		public abstract Position FromPos{ get; } //Retorna ao movimento inicial

		public abstract Position ToPos { get; } //Posição final da jogada

		public abstract bool Execute(Board board);

		public virtual bool IsLegal(Board board)
		{
			Player player = board[FromPos].Color;
			Board boardCopy = board.Copy();
			Execute(boardCopy);
			return !boardCopy.IsInCheck(player);
		}

        public string ToJson() => JsonConvert.SerializeObject(this);
        public static T Parse<T>(string json) where T : Move => JsonConvert.DeserializeObject<T>(json);
    }

    public class MoveData
    {
        public MoveData(){ }
        public MoveData(Position from, Position to, MoveType type) => (FromPos, ToPos, Type) = (from, to, type);

        public MoveType Type { get; set; }
        public Position FromPos { get; set; }
        public Position ToPos { get; set; }
    }
}
