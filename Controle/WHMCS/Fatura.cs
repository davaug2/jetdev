using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JetDev.Control.WHMCS
{
    public class Fatura
    {
        public string Codigo { get; set; }

        public string DataVencimento { get; set; }

        public string DataPagamento { get; set; }

        public string Situacao { get; set; }
    }
}
