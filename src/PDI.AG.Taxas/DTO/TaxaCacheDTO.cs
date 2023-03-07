namespace PDI.AG.Taxas.DTO
{
    public class TaxaCacheDTO
    {
        public string? codigo { get; set; }
        public string? nome { get; set; }
        public decimal taxaCompra { get; set; }
        public decimal taxaVenda { get; set; }
        public DateTime createDate { get; set; }        
    }
}
