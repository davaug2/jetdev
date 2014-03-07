using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Xml.Linq;

namespace JetDev.Control.WHMCS
{/*
    public static class WHMCS
    {
        public static XDocument Obter(string acao, string entrada)
        {

            var WHMCS_USERNAME = "admin";
            var WHMCS_PASSWORD = "asmare@1";
            var WHMCS_CLIENTID = "";
            var WHMCS_ACCESSKEY = "KjOFfJn9AL9pSDYqI2cEacF4QRI8QcbPLYrBGoisc97PpvKHU8wxMihjDGnuywE2";
            var WHMCS_AUTHKEY = "jPR30f976fc57b75de78NAwVWH";
            var WHMCS_URL = "http://atendimento.jetdev.com.br/";
            var WHMCS_URLAPI = WHMCS_URL + "includes/api.php";

            var data = string.Format("action={1}&accesskey={2}&username={3}&password={4}&responsetype=xml&{5}", WHMCS_CLIENTID, acao, WHMCS_ACCESSKEY, WHMCS_USERNAME, GetMd5Hash(WHMCS_PASSWORD), entrada);
            var byteArray = Encoding.UTF8.GetBytes(data);

            var webRequest = WebRequest.Create(WHMCS_URLAPI);
            webRequest.Method = "POST";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.ContentLength = byteArray.Length;
            Stream dataStream = webRequest.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            var response = webRequest.GetResponse();

            dataStream = response.GetResponseStream();
            var result = new StreamReader(dataStream).ReadToEnd();
            dataStream.Close();
            response.Close();

            return XDocument.Parse(result);
        }

        static string GetMd5Hash(string input)
        {
            byte[] data = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
                sBuilder.Append(data[i].ToString("x2"));
            return sBuilder.ToString();
        }

        public static List<Cliente> ListarClientes()
        {
            var xd = Obter("getclients", string.Empty);
            var xmlClients = xd.Root.Elements("clients").Elements("client").ToArray();
            var clients = new List<Cliente>();
            foreach (var item in xmlClients)
            {
                clients.Add(new Cliente()
                {
                    Codigo = item.Element("id").Value,
                    Nome = string.Format("{0} {1}", item.Element("firstname").Value, item.Element("lastname").Value),
                    Email = item.Element("email").Value
                });
            }
            return clients;
        }

        public static List<Fatura> Faturas(string codigo)
        {
            var xd = Obter("getinvoices", "userid=" + codigo);
            var xmlClients = xd.Root.Elements("invoices").Elements("invoice").ToArray();
            var entidade = new List<Fatura>();
            foreach (var item in xmlClients)
            {
                entidade.Add(new Fatura()
                {
                    Codigo = item.Element("id").Value,
                    DataVencimento = item.Element("duedate").Value,
                    DataPagamento = item.Element("datepaid").Value,
                    Situacao = item.Element("status").Value,                    
                });
            }
            return entidade;
        }


        public static List<Produto> ListarProdutos()
        {
            var xd = Obter("getproducts", string.Empty);
            var xmlClients = xd.Root.Elements("products").Elements("product").ToArray();
            //var precos = new whmcsEntities().tblpricings.Where(i => i.type == "product").ToList();
            var entidade = new List<Produto>();
            //foreach (var item in xmlClients)
            //{
            //    entidade.Add(new Produto()
            //    {
            //        Codigo = item.Element("pid").Value,
            //        Nome = item.Element("name").Value,
            //        Descricao = item.Element("description").Value,
            //        Preco = precos.Where(i => i.relid.ToString() == item.Element("pid").Value).Select(i=> i.monthly).FirstOrDefault()
            //    });
            //}
            return entidade;
        }
    }
    */
}