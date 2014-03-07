using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;

namespace JetDev.Control
{
    public class Extenso
    {
        protected static string[] unidade = new string[10];
        protected static string[] dezena = new string[18];
        protected static string[] centena = new string[10];

        protected static string unidades(string snumero)
        {
            int numero = int.Parse(snumero);
            return unidade[numero];
        }

        protected static string dezenas(string snumero)
        {
            int numero = int.Parse(snumero);

            if (numero > 9 & numero < 21)
            {
                return dezena[numero - 10];
            }
            else if (numero >= 21 & numero <= 99)
            {
                if (Right(snumero, 1) == "0")
                {
                    return dezena[Left(snumero, 1) + 8];
                }
                else
                {
                    return dezena[int.Parse("1" + (Left(snumero, 1) - 2).ToString())] + " e " + unidade[int.Parse(Right(snumero, 1))];
                }
            }
            else
                return "";
        }

        protected static string centenas(string numero)
        {
            if (Mid(numero, 2, 2) == "00")
            {
                if (numero == "100")
                {
                    return "cem";
                }
                else
                {
                    return centena[Left(numero, 1)];
                }
            }
            else if (Mid(numero, 3, 1) == "0")
            {
                return centena[Left(numero, 1)] + " e " + dezenas(Mid(numero, 2, 2));
            }
            else if (Mid(numero, 2, 1) == "0")
            {
                return centena[Left(numero, 1)] + " e " + unidade[int.Parse(Right(numero, 1))];
            }
            else
            {
                return centena[Left(numero, 1)] + " e " + dezenas(Right(numero, 2));
            }
        }

        public static string ValorExtenso(double numero)
        {
            if (numero > 9999999.99)
            {
                return "Número fora dos padrões válidos !";
            }
            unidade[0] = "zero";
            unidade[1] = "um";
            unidade[2] = "dois";
            unidade[3] = "três";
            unidade[4] = "quatro";
            unidade[5] = "cinco";
            unidade[6] = "seis";
            unidade[7] = "sete";
            unidade[8] = "oito";
            unidade[9] = "nove";
            dezena[0] = "dez";
            dezena[1] = "onze";
            dezena[2] = "doze";
            dezena[3] = "treze";
            dezena[4] = "quatorze";
            dezena[5] = "quinze";
            dezena[6] = "dezesseis";
            dezena[7] = "dezessete";
            dezena[8] = "dezoito";
            dezena[9] = "dezenove";
            dezena[10] = "vinte";
            dezena[11] = "trinta";
            dezena[12] = "quarenta";
            dezena[13] = "cinquenta";
            dezena[14] = "sessenta";
            dezena[15] = "setenta";
            dezena[16] = "oitenta";
            dezena[17] = "noventa";
            centena[1] = "cento";
            centena[2] = "duzentos";
            centena[3] = "trezentos";
            centena[4] = "quatrocentos";
            centena[5] = "quinhentos";
            centena[6] = "seiscentos";
            centena[7] = "setecentos";
            centena[8] = "oitocentos";
            centena[9] = "novecentos";

            int inteiro = Convert.ToInt32(Math.Floor(numero));
            int tamanho;
            string ext = "";

            if (inteiro != 0)
            {
                tamanho = Len(inteiro);
            }
            else
            {
                tamanho = 0;
            }
            if (tamanho == 7)
            {
                ext = unidades(Left(inteiro.ToString(), 1).ToString());
                if (Left(inteiro.ToString(), 1) == 1)
                    ext += " milhão ";
                else
                    ext += " milhões ";

                if (Right(inteiro.ToString(), 6) == "000000")
                    ext += " de ";
                else
                    ext += " e ";

                inteiro = Int32.Parse(Right(inteiro.ToString(), 6));
            }

            if (inteiro != 0)
            {
                tamanho = Len(inteiro);
            }
            else
            {
                tamanho = 0;
            }
            if (tamanho == 1)
            {
                ext += unidades(inteiro.ToString());
            }
            else if (tamanho == 2)
            {
                ext += dezenas(inteiro.ToString());
            }
            else if (tamanho == 3)
            {
                ext += centenas(inteiro.ToString());
            }
            else if (tamanho == 4)
            {
                if (Right(inteiro.ToString(), 3) == "000")
                {
                    ext += unidades(Left(inteiro.ToString(), 1).ToString()) + " mil";
                }
                else
                {
                    if (int.Parse(Right(inteiro.ToString(), 3)) > 99)
                    {
                        ext += unidades(Left(inteiro.ToString(), 1).ToString()) + " mil e " + centenas(int.Parse((Right(inteiro.ToString(), 3))).ToString());
                    }
                    else if (int.Parse(Right(inteiro.ToString(), 3)) > 9 && int.Parse((Right(inteiro.ToString(), 3))) < 100)
                    {
                        ext += unidades(Left(inteiro.ToString(), 1).ToString()) + " mil e " + dezenas(int.Parse((Right(inteiro.ToString(), 3))).ToString());
                    }
                    else if (int.Parse(Right(inteiro.ToString(), 3)) < 10)
                    {
                        ext += unidades(Left(inteiro.ToString(), 1).ToString()) + " mil e " + unidades(int.Parse((Right(inteiro.ToString(), 3))).ToString());
                    }
                }
            }
            else if (tamanho == 5)
            {
                if (Right(inteiro.ToString(), 3) == "000")
                {
                    ext += dezenas(Left(inteiro.ToString(), 2).ToString()) + " mil ";
                }
                else
                {
                    if (int.Parse((Right(inteiro.ToString(), 3))) > 99)
                    {
                        ext += dezenas(Left(inteiro.ToString(), 2).ToString()) + " mil e " + centenas(Right(inteiro.ToString(), 3));
                    }
                    else if (int.Parse(Right(inteiro.ToString(), 3)) > 9 && int.Parse(Right(inteiro.ToString(), 3)) < 100)
                    {
                        ext += dezenas(Left(inteiro.ToString(), 2).ToString()) + " mil e " + dezenas(int.Parse((Right(inteiro.ToString(), 3))).ToString());
                    }
                    else if (int.Parse(Right(inteiro.ToString(), 3)) < 10)
                    {
                        ext += dezenas(Left(inteiro.ToString(), 2).ToString()) + " mil e " + unidades(int.Parse((Right(inteiro.ToString(), 3))).ToString());
                    }
                }
            }
            else if (tamanho == 6)
            {
                if (Right(inteiro.ToString(), 3) == "000")
                {
                    ext += centenas(Left(inteiro.ToString(), 3).ToString()) + " mil ";
                }
                else if (int.Parse(Right(inteiro.ToString(), 3)) > 99)
                {
                    ext += centenas(Left(inteiro.ToString(), 3).ToString()) + " mil e " + centenas(int.Parse((Right(inteiro.ToString(), 3))).ToString());
                }
                else if (int.Parse(Right(inteiro.ToString(), 3)) > 9 && int.Parse(Right(inteiro.ToString(), 3)) < 100)
                {
                    ext += centenas(Left(inteiro.ToString(), 3).ToString()) + " mil e " + dezenas(int.Parse((Right(inteiro.ToString(), 3))).ToString());
                }
                else if (int.Parse(Right(inteiro.ToString(), 3)) < 10)
                {
                    ext += centenas(Left(inteiro.ToString(), 3).ToString()) + " mil e " + unidades(int.Parse((Right(inteiro.ToString(), 3))).ToString());
                }
            }

            if (ext != "")
            {
                if (ext == "um")
                {
                    ext = ext + " real";
                }
                else
                {
                    ext = ext + " reais";
                }
            }
            if (numero - Convert.ToInt32(numero) != 0)
            {
                string fra;

                fra = Right(decimal.Round(Convert.ToDecimal(numero), 2).ToString(), 2);
                if (Convert.ToInt32(numero) != 0)
                {
                    if (fra.IndexOf(",") != -1)
                    {
                        int temp = (int.Parse(Right(fra, 1)) * 10);
                        fra = temp.ToString();
                    }
                    if (int.Parse(fra) >= 10)
                    {
                        ext += " e " + dezenas(fra) + " centavos";
                    }
                    else if (int.Parse(fra) < 10)
                    {

                        if (int.Parse(fra) == 1)
                            ext += " e " + unidades(fra) + " centavo";
                        else
                            ext += " e " + unidades(fra) + " centavos";
                    }
                }
                else
                {
                    if (fra.IndexOf(",") != -1)
                    {
                        int temp = (int.Parse(Right(fra, 1)) * 10);
                        fra = temp.ToString();
                    }
                    if (int.Parse(fra) >= 10)
                    {
                        ext += dezenas(fra) + " centavos";
                    }
                    else if (int.Parse(fra) < 10)
                    {
                        if (int.Parse(fra) == 1)
                            ext += unidades(fra) + " centavo";
                        else
                            ext += unidades(fra) + " centavos";
                    }
                }
            }
            return ext;
        }

        public static string NumeroExtenso(double numero)
        {
            if (numero > 9999999.99)
            {
                return "Número fora dos padrões válidos !";
            }
            unidade[0] = "zero";
            unidade[1] = "um";
            unidade[2] = "dois";
            unidade[3] = "três";
            unidade[4] = "quatro";
            unidade[5] = "cinco";
            unidade[6] = "seis";
            unidade[7] = "sete";
            unidade[8] = "oito";
            unidade[9] = "nove";
            dezena[0] = "dez";
            dezena[1] = "onze";
            dezena[2] = "doze";
            dezena[3] = "treze";
            dezena[4] = "quatorze";
            dezena[5] = "quinze";
            dezena[6] = "dezesseis";
            dezena[7] = "dezessete";
            dezena[8] = "dezoito";
            dezena[9] = "dezenove";
            dezena[10] = "vinte";
            dezena[11] = "trinta";
            dezena[12] = "quarenta";
            dezena[13] = "cinquenta";
            dezena[14] = "sessenta";
            dezena[15] = "setenta";
            dezena[16] = "oitenta";
            dezena[17] = "noventa";
            centena[1] = "cento";
            centena[2] = "duzentos";
            centena[3] = "trezentos";
            centena[4] = "quatrocentos";
            centena[5] = "quinhentos";
            centena[6] = "seiscentos";
            centena[7] = "setecentos";
            centena[8] = "oitocentos";
            centena[9] = "novecentos";

            int inteiro = Convert.ToInt32(Math.Floor(numero));
            int tamanho;
            string ext = "";

            if (inteiro != 0)
            {
                tamanho = Len(inteiro);
            }
            else
            {
                tamanho = 0;
            }
            if (tamanho == 7)
            {
                ext = unidades(Left(inteiro.ToString(), 1).ToString());
                if (Left(inteiro.ToString(), 1) == 1)
                    ext += " milhão ";
                else
                    ext += " milhões ";

                if (Right(inteiro.ToString(), 6) == "000000")
                    ext += "";
                else
                    ext += " e ";

                inteiro = Int32.Parse(Right(inteiro.ToString(), 6));
            }

            if (inteiro != 0)
            {
                tamanho = Len(inteiro);
            }
            else
            {
                tamanho = 0;
            }
            if (tamanho == 1)
            {
                ext += unidades(inteiro.ToString());
            }
            else if (tamanho == 2)
            {
                ext += dezenas(inteiro.ToString());
            }
            else if (tamanho == 3)
            {
                ext += centenas(inteiro.ToString());
            }
            else if (tamanho == 4)
            {
                if (Right(inteiro.ToString(), 3) == "000")
                {
                    ext += unidades(Left(inteiro.ToString(), 1).ToString()) + " mil";
                }
                else
                {
                    if (int.Parse(Right(inteiro.ToString(), 3)) > 99)
                    {
                        ext += unidades(Left(inteiro.ToString(), 1).ToString()) + " mil e " + centenas(int.Parse((Right(inteiro.ToString(), 3))).ToString());
                    }
                    else if (int.Parse(Right(inteiro.ToString(), 3)) > 9 && int.Parse((Right(inteiro.ToString(), 3))) < 100)
                    {
                        ext += unidades(Left(inteiro.ToString(), 1).ToString()) + " mil e " + dezenas(int.Parse((Right(inteiro.ToString(), 3))).ToString());
                    }
                    else if (int.Parse(Right(inteiro.ToString(), 3)) < 10)
                    {
                        ext += unidades(Left(inteiro.ToString(), 1).ToString()) + " mil e " + unidades(int.Parse((Right(inteiro.ToString(), 3))).ToString());
                    }
                }
            }
            else if (tamanho == 5)
            {
                if (Right(inteiro.ToString(), 3) == "000")
                {
                    ext += dezenas(Left(inteiro.ToString(), 2).ToString()) + " mil ";
                }
                else
                {
                    if (int.Parse((Right(inteiro.ToString(), 3))) > 99)
                    {
                        ext += dezenas(Left(inteiro.ToString(), 2).ToString()) + " mil e " + centenas(Right(inteiro.ToString(), 3));
                    }
                    else if (int.Parse(Right(inteiro.ToString(), 3)) > 9 && int.Parse(Right(inteiro.ToString(), 3)) < 100)
                    {
                        ext += dezenas(Left(inteiro.ToString(), 2).ToString()) + " mil e " + dezenas(int.Parse((Right(inteiro.ToString(), 3))).ToString());
                    }
                    else if (int.Parse(Right(inteiro.ToString(), 3)) < 10)
                    {
                        ext += dezenas(Left(inteiro.ToString(), 2).ToString()) + " mil e " + unidades(int.Parse((Right(inteiro.ToString(), 3))).ToString());
                    }
                }
            }
            else if (tamanho == 6)
            {
                if (Right(inteiro.ToString(), 3) == "000")
                {
                    ext += centenas(Left(inteiro.ToString(), 3).ToString()) + " mil ";
                }
                else if (int.Parse(Right(inteiro.ToString(), 3)) > 99)
                {
                    ext += centenas(Left(inteiro.ToString(), 3).ToString()) + " mil e " + centenas(int.Parse((Right(inteiro.ToString(), 3))).ToString());
                }
                else if (int.Parse(Right(inteiro.ToString(), 3)) > 9 && int.Parse(Right(inteiro.ToString(), 3)) < 100)
                {
                    ext += centenas(Left(inteiro.ToString(), 3).ToString()) + " mil e " + dezenas(int.Parse((Right(inteiro.ToString(), 3))).ToString());
                }
                else if (int.Parse(Right(inteiro.ToString(), 3)) < 10)
                {
                    ext += centenas(Left(inteiro.ToString(), 3).ToString()) + " mil e " + unidades(int.Parse((Right(inteiro.ToString(), 3))).ToString());
                }
            }

            if (ext != "")
            {
                if (ext == "um")
                {
                    ext = ext + "";
                }
                else
                {
                    ext = ext + "";
                }
            }
            if (numero - Convert.ToInt32(numero) != 0)
            {
                string fra;
                fra = Right(decimal.Round(Convert.ToDecimal(numero), 2).ToString(), 2);
                if (Convert.ToInt32(numero) != 0)
                {
                    if (fra.IndexOf(",") != -1)
                    {
                        int temp = (int.Parse(Right(fra, 1)) * 10);
                        fra = temp.ToString();
                    }
                    if (int.Parse(fra) >= 10)
                    {
                        ext += " e " + dezenas(fra) + " ";
                    }
                    else if (int.Parse(fra) < 10)
                    {

                        if (int.Parse(fra) == 1)
                            ext += " e " + unidades(fra) + " ";
                        else
                            ext += " e " + unidades(fra) + " ";
                    }
                }
                else
                {
                    if (fra.IndexOf(",") != -1)
                    {
                        int temp = (int.Parse(Right(fra, 1)) * 10);
                        fra = temp.ToString();
                    }
                    if (int.Parse(fra) >= 10)
                    {
                        ext += dezenas(fra) + " ";
                    }
                    else if (int.Parse(fra) < 10)
                    {
                        if (int.Parse(fra) == 1)
                            ext += unidades(fra) + " ";
                        else
                            ext += unidades(fra) + " ";
                    }
                }
            }
            return ext;
        }

        /***************************************************************/


        public static int Left(string MyString, int length)
        {
            string tmpstr = MyString.Substring(0, length);
            return int.Parse(tmpstr);
        }
        public static string Right(string MyString, int length)
        {
            string tmpstr = MyString.Substring(MyString.Length - length, length);
            return tmpstr;
        }
        public static string Mid(string MyString, int startIndex, int length)
        {
            string tmpstr = MyString.Substring(startIndex - 1, length);
            return tmpstr;
        }
        public static string Mid(string MyString, int startIndex)
        {
            string tmpstr = MyString.Substring(startIndex - 1);
            return tmpstr;
        }
        public static int Len(int numero)
        {
            return numero.ToString().Length;
        }
    }
}