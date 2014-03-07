using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization;

namespace JetDev.Control.WHMCS
{
    public class Cliente
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string CPF_CNPJ { get; set; }
        public string Endereço { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}