namespace MVC2024.Models
{
	public class SerieModelo
	{
		public int ID { get; set; }
		public string nomSerie { get; set; }
		public MarcaModelo Marca { get; set; }
		public int MarcaID { get; set; } //No sería necesario, pero es bueno ponerlo

	}
}
