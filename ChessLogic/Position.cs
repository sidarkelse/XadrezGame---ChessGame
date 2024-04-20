namespace ChessLogic
{
	public class Position
	{
		public int Row { get; set; } //Linha
		public int Column { get; set; } //Coluna

		public Position(int row, int column)
		{
			Row = row;
			Column = column;
		}

		public Player SquareColor() //Determina a Cor das casas, Na soma das coordenadas da linha (Row) e a coluna (Column)

		{
			if((Row + Column) % 2 == 0) // se a soma for par a cor é branca
			{
				return Player.White;
			}
			return Player.Black; // se não a cor é preta

		}

		public override bool Equals(object obj) //Determina Igualdade das posições
		{
			return obj is Position position &&
				   Row == position.Row &&
				   Column == position.Column;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Row, Column);
		}

		public static bool operator ==(Position left, Position right) //Comparando a igualdade-
		{
			return EqualityComparer<Position>.Default.Equals(left, right); //Retorna true se left for = right - Se for diferente = False
		}

		public static bool operator !=(Position left, Position right) //Comparando a desigualdade
		{
			return !(left == right); //-inverte o resultado, left + right = true/ =False -- Se left != right = True
		}

		public static Position operator +(Position pos, Direction dir)
		{
			return new Position(pos.Row + dir.RowDelta, pos.Column + dir.ColumnDelta);

		}

	}
}
