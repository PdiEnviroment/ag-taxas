using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using PDI.AG.Taxas.DTO;

namespace PDI.AG.Taxas.Services
{
    public interface ITaxaService       
    {
        Task ConsumirTaxas();
    }

    public class TaxasService : ITaxaService
    {
        private readonly IDistributedCache _distributedCache;

        public TaxasService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task ConsumirTaxas()
        {
            try
            {
                TaxaApiDTO? TaxasApi = await BuscarTaxasAsync();

                List<TaxaCacheDTO> TaxasColetadas = FormatarTaxas(TaxasApi);

                PersistirCache(TaxasColetadas);
            }
            catch(Exception error)
            {
                throw error;
            }
        }

        private async void PersistirCache(List<TaxaCacheDTO> taxasColetadas)
        {
            var key = Guid.NewGuid().ToString();
            var cacheSerialized = JsonConvert.SerializeObject(taxasColetadas);

            await _distributedCache.SetStringAsync(key, cacheSerialized);
        }

        private List<TaxaCacheDTO> FormatarTaxas(TaxaApiDTO? taxasApi)
        {
            List<TaxaCacheDTO> result = new List<TaxaCacheDTO>();

            if (taxasApi != null)
            {
                result.Add(taxasApi.USDBRL.ConvertToCache());
                result.Add(taxasApi.BTCBRL.ConvertToCache());
                result.Add(taxasApi.EURBRL.ConvertToCache());
            }

            return result;
        }

        private async Task<TaxaApiDTO?> BuscarTaxasAsync()
        {
            using var client = new HttpClient();
            var content = await client.GetFromJsonAsync<TaxaApiDTO>("https://economia.awesomeapi.com.br/last/USD-BRL,EUR-BRL,BTC-BRL");

            return content;
        }
    }
}
