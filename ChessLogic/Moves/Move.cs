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
	public abstract partial class Move
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

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
        public static Move Parse(string json)
        {
            return JsonConvert.DeserializeObject<MoveData>(json);
        }
    }

    public class MoveData : Move
    {
		public MoveData() { }

        public override MoveType Type { get; }
        public override Position FromPos { get; }
        public override Position ToPos { get; }

        public override bool Execute(Board board)
        {
            return false;
        }
    }
}
