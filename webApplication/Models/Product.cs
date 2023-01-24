namespace webApplication.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string NazwaProduktu { get; set; }
        public string? Model { get; set; }
        public int Ilosc { get; set; }
        public double Cena { get; set; }
        public string? Rodzaj { get; set; }
    }
}
