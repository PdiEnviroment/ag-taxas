namespace PDI.AG.Taxas.DTO
{
    public class TaxaApiDTO
    {
        public Taxa? USDBRL { get; set; }
        public Taxa? EURBRL { get; set; }
        public Taxa? BTCBRL { get; set; }
    }

    public class Taxa
    {
        public string code { get; set; }
        public string codein { get; set; }
        public string name { get; set; }
        public string high { get; set; }
        public string low { get; set; }
        public string varBid { get; set; }
        public string pctChange { get; set; }
        public string bid { get; set; }
        public string ask { get; set; }
        public string timestamp { get; set; }
        public string create_date { get; set; }

        public Taxa(string code, string codein, string name, string high, string low, string varBid, string pctChange, string bid, string ask, string timestamp, string create_date)
        {
            this.code = code;
            this.codein = codein;
            this.name = name;
            this.high = high;
            this.low = low;
            this.varBid = varBid;
            this.pctChange = pctChange;
            this.bid = bid;
            this.ask = ask;
            this.timestamp = timestamp;
            this.create_date = create_date;
        }

        public TaxaCacheDTO ConvertToCache()
        {
            return new TaxaCacheDTO()
            {
                codigo = this.code,
                createDate = Convert.ToDateTime(this.create_date),
                nome = this.name,
                taxaCompra = Convert.ToDecimal(this.ask),
                taxaVenda = Convert.ToDecimal(this.bid)
            };
        }
    }

}
