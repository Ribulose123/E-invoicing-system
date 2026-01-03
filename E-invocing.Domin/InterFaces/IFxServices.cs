

namespace E_invocing.Domin.InterFaces
{
    public interface IFxServices
    {
        Task <decimal> GetExchangeRateAsync(string fromCurrency, string toCurrency); 
    }
}
