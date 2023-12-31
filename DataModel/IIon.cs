namespace DataModel
{
    public interface IIon
    {
        public int Id { get; init; }
        public string? Name { get; set; }
        public string? Symbol { get; set; }
        public double Concentration { get; set; }

        public List<Product> Products { get; }
    }
}