using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_invocing.Domin.InterFaces
{
    public interface ITaxService
    {
        Task<decimal>GetTaxRateAsync(string countryCode);
    }
}
