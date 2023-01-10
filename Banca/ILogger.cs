using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banca

{
    public interface ILogger
    {
        public void Log(String message);

        public List<String> GetActions();
        public int GetNumberOfCalls();
    }
}
