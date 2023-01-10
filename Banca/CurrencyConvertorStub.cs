using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banca
{
    public class CurrencyConvertorStub : ICurrencyConvertor
    {
        float euroRate = 4.94F;
        float HRKRate = 0.65F;
        public CurrencyConvertorStub(float _rateEurRon, float _rateHRKRon)
        {
            euroRate = _rateEurRon;
            HRKRate = _rateHRKRon;
        }

        public float EuroToRon(float ValueInEur)
        {
            return ValueInEur * euroRate;
        }
        public float RonToEuro(float ValueInRon)
        {
            return ValueInRon / euroRate;
        }
        public float HRKToRon(float ValueInHRK)
        {
            return ValueInHRK * HRKRate;
        }
        public float RonToHRK(float ValueInRon)
        {
            return ValueInRon / HRKRate;
        }
    }
}
