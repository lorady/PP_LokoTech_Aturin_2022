using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AturinKozlovNegmatov_Praktika2022_LokoTeh.Entities
{
    public partial class Поезда
    {
        public string imagepath { get { return AppDomain.CurrentDomain.BaseDirectory + Photo; } }
    }
}
