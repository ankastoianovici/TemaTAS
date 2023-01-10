using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banca
{
    public interface ICurrencyConvertor
    {
        public float RonToEuro(float ron);
        public float EuroToRon(float euro);
        public float RonToHRK(float ron);
        public float HRKToRon(float HRK);
    }
}
