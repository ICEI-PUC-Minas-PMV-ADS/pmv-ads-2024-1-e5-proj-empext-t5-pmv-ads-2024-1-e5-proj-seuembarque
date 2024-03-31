using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace Puc.Diario.Infra
{
    public static class Utils
    {

        #region Converter

        /// <summary>
        /// Converte object em tipo Bolean
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ToBool(object value)
        {
            try
            {
                return Convert.ToBoolean(value);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Converte string em Int32 e retorna zero em caso de erro
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Int32</returns>
        public static int ToInt32(string value)
        {
            try
            {
                return Convert.ToInt32(value);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Converte string em decimal e retorna zero em caso de erro
        /// </summary>
        /// <param name="value"></param>
        /// <returns>decimal</returns>
        public static decimal ToDecimal(string value)
        {
            try
            {
                return decimal.Round(Convert.ToDecimal(value), 2);
            }
            catch
            {
                return Convert.ToDecimal(0.00);
            }
        }

        /// <summary>
        /// Converte string em double e retorna zero em caso de erro
        /// </summary>
        /// <param name="value"></param>
        /// <returns>double</returns>
        public static double ToDouble(string value)
        {
            try
            {
                return Convert.ToDouble(value);
            }
            catch
            {
                return Convert.ToDouble(0.00);
            }
        }

        /// <summary>
        /// Converte string em Int64 e retorna zero em caso de erro
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Int64</returns>
        public static long ToInt64(string value)
        {
            try
            {
                return Convert.ToInt64(value);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Converte string em string e retorna string vazio em caso de erro
        /// </summary>
        /// <param name="value"></param>
        /// <returns>string</returns>
        public static string ToString(object value)
        {
            try
            {
                return Convert.ToString(value).Trim();
            }
            catch
            {
                return string.Empty;
            }
        }

        public static DateTime ToDateTime(object value)
        {
            try
            {
                return Convert.ToDateTime(value, CultureInfo.GetCultureInfo("pt-BR"));
            }
            catch
            {
                return new DateTime(1, 1, 1);
            }
        }

        #endregion

        #region Formatação Formulario

        /// <summary>
        /// Formata o CPF ou CNPJ do Cedente ou do Sacado no formato: 000.000.000-00, 00.000.000/0001-00 respectivamente.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>string formatada</returns>
        public static string FormataCPFCPPJ(string value)
        {

            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;

            if (value.Trim().Length < 11)
                return string.Empty;
            else
            {
                if (value.Trim().Length == 11)
                    return FormataCPF(value);
                else if (value.Trim().Length == 14)
                    return FormataCNPJ(value);
            }

            throw new Exception(string.Format("O CPF ou CNPJ: {0} é inválido.", value));
        }

        /// <summary>
        /// Formata o número do CPF 92074286520 para 920.742.865-20
        /// </summary>
        /// <param name="cpf">Sequencia numérica de 11 dígitos. Exemplo: 00000000000</param>
        /// <returns>CPF formatado</returns>
        public static string FormataCPF(string cpf)
        {
            try
            {
                return string.Format(
                    "{0}.{1}.{2}-{3}", cpf.Substring(0, 3), cpf.Substring(3, 3), cpf.Substring(6, 3),
                    cpf.Substring(9, 2));
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Formata o CNPJ. Exemplo 00.316.449/0001-63
        /// </summary>
        /// <param name="cnpj">Sequencia numérica de 14 dígitos. Exemplo: 00000000000000</param>
        /// <returns>CNPJ formatado</returns>
        public static string FormataCNPJ(string cnpj)
        {
            try
            {
                return string.Format(
                    "{0}.{1}.{2}/{3}-{4}", cnpj.Substring(0, 2), cnpj.Substring(2, 3), cnpj.Substring(5, 3),
                    cnpj.Substring(8, 4), cnpj.Substring(12, 2));
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Formato o CEP em 00.000-000
        /// </summary>
        /// <param name="cep">Sequencia numérica de 8 dígitos. Exemplo: 00000000</param>
        /// <returns>CEP formatado</returns>
        public static string FormataCEP(string cep)
        {
            try
            {
                return string.Format("{0}{1}-{2}", cep.Substring(0, 2), cep.Substring(2, 3), cep.Substring(5, 3));
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Formata agência e conta
        /// </summary>
        /// <param name="agencia">Código da agência</param>
        /// <param name="digitoAgencia">Dígito verificador da agência. Pode ser vazio.</param>
        /// <param name="conta">Código da conta</param>
        /// <param name="digitoConta">dígito verificador da conta. Pode ser vazio.</param>
        /// <returns>Agência e conta formatadas</returns>
        public static string FormataAgenciaConta(string agencia, string digitoAgencia, string conta, string digitoConta)
        {
            string agenciaConta = string.Empty;
            try
            {
                agenciaConta = agencia;
                if (digitoAgencia != string.Empty)
                    agenciaConta += "-" + digitoAgencia;

                agenciaConta += "/" + conta;
                if (digitoConta != string.Empty)
                    agenciaConta += "-" + digitoConta;

                return agenciaConta;
            }
            catch
            {
                return string.Empty;
            }
        }

        #endregion

        #region Extrair de Arquivo txt

        /// <summary>
        /// Extrai uma string conforme texto, inicio e fim informado.
        /// </summary>
        /// <param name="linha">string linha</param>
        /// <param name="de">int inicio</param>
        /// <param name="ate">int fim</param>
        /// <returns>string</returns>
        public static string ExtrairDaPosicao(string linha, int de, int ate)
        {
            try
            {
                int inicio = de - 1;
                int tamanho = ate - inicio;
                int totalLinha = inicio + tamanho;

                if (linha.Length >= totalLinha)
                {
                    return linha.Substring(inicio, tamanho);
                }

            }
            catch (Exception)
            {
                return string.Empty;
            }

            return string.Empty;
        }

        /// <summary>
        /// Extrai uma string conforme texto, inicio e fim informado e retorna um Int32
        /// </summary>
        /// <param name="linha">string linha</param>
        /// <param name="de">int inicio</param>
        /// <param name="ate">int fim</param>
        /// <returns>Int32</returns>
        public static int ExtrairInt32DaPosicao(string linha, int de, int ate)
        {
            return int.Parse(ExtrairDaPosicao(linha, de, ate));
        }

        /// <summary>
        /// Extrai uma string conforme texto, inicio e fim informado e retorna um Int64
        /// </summary>
        /// <param name="linha">string linha</param>
        /// <param name="de">int inicio</param>
        /// <param name="ate">int fim</param>
        /// <returns>Int64</returns>
        public static long ExtrairInt64DaPosicao(string linha, int de, int ate)
        {
            return long.Parse(ExtrairDaPosicao(linha, de, ate));
        }

        /// <summary>
        /// Extrai uma string conforme texto, inicio e fim informado e retorna um DateTime
        /// </summary>
        /// <param name="linha">string linha</param>
        /// <param name="de">int inicio</param>
        /// <param name="ate">int fim</param>
        /// <returns>DateTime</returns>
        public static DateTime ExtrairDataDaPosicao(string linha, int de, int ate)
        {
            string valor = ExtrairDaPosicao(linha, de, ate);
            return DateTime.ParseExact(valor, "ddMMyyyy", null);
        }

        #endregion

        #region Boleto Bancario


        /// <summary>
        ///  Indentifica tipo de documento
        ///       01 - CPF | 02 - CNPJ
        ///       Autor: Janiel Madureira Oliveira
        /// </summary>
        /// <param name="inscricao"></param>
        /// <returns></returns>
        public static string IdentificaTipoInscricaoSacado(string inscricao)
        {
            //Variaveis
            string tipo = string.Empty;
            //Tratamento
            inscricao = inscricao.Replace(".", "").Replace("-", "").Replace("/", "");
            //Verifica tipo
            if (inscricao.Length == 11)
            {
                tipo = "01"; //CPF
            }
            else if (inscricao.Length == 14)
            {
                tipo = "02"; // CNPJ
            }

            //Retorno
            return tipo;
        }

        /// <summary>
        /// Função para verificação de erro de digitação do barcode.
        /// </summary>
        /// <param name="codigoBarra"></param>
        /// <returns>verdadeiro ou falso</returns>
        public static bool verificaErroCodigoBarra(string codigoBarra)
        {
            //Verifica se o número digitado não está vazio ou é nulo e se está entre 2 e 13.
            if (string.IsNullOrEmpty(codigoBarra) || !(codigoBarra.Length == 44))
                return false;
            else
            {
                //Verifica se o número digitado é realmente um número.
                foreach (char caracter in codigoBarra
                ) //Com o foreach passamos caracter por caracter para dentro de uma 
                    if (!char.IsNumber(caracter)) //variável "caracter" e verificamos se ela é um número.
                        return false; //Caso ache algo diferente de número retorna falso.      
                return true; //Sem erros, retorna verdadeiro.
            }
        }

        /// <summary>
        /// Função do algoritmo em si, onde verifica o código verificador do barcode.
        /// </summary>
        /// <param name="codigoBarra"></param>
        /// <returns>verdadeiro ou falso</returns>
        public static bool verificaDigitoCodigoBarra(string codigoBarra)
        {
            int dvGeral = ToInt32(codigoBarra.Substring(4, 1));
            string codBar = codigoBarra.Substring(0, 3) + codigoBarra.Substring(5);
            int digito = 0;

            string digitoUm = codigoBarra.Substring(0, 1);
            if (digitoUm == "8")
            {
                //Concessionaria
                codBar = codigoBarra.Substring(0, 3) + codigoBarra.Substring(4);
                dvGeral = ToInt32(codigoBarra.Substring(3, 1));

                digito = Mod10(codBar);
                if (digito == dvGeral)
                    return true;

                digito = Mod10(codigoBarra);
                if (digito == dvGeral)
                    return true;

                digito = Mod11(codBar);
                if (digito == dvGeral)
                    return true;

                digito = Mod11(codigoBarra);
                if (digito == dvGeral)
                    return true;

                digito = Mod11Peso2a9(codBar);
                if (digito == dvGeral)
                    return true;

                digito = Mod11Peso2a9(codigoBarra);
                if (digito == dvGeral)
                    return true;
            }
            else
            {
                //Boleto Bancario
                codBar = codigoBarra.Substring(0, 4) + codigoBarra.Substring(5);
                dvGeral = ToInt32(codigoBarra.Substring(4, 1));
                digito = Mod11Peso2a9(codBar);
            }

            bool retorno = digito == dvGeral;
            return retorno;
        }


        public static string GerarValorBoleto(string valor, int tamanho)
        {
            string vl = valor.Replace(".", "").Replace(",", "");
            while (vl.Length < tamanho)
            {
                vl = "0" + vl;
                vl = vl.Trim().Replace(" ", string.Empty);
            }

            return vl;
        }

        //public static string GerarCodigoBarrasCorreto(string codbarra, string vencimento, string valor)
        //{
        //    string retorno = string.Empty;
        //    if (!string.IsNullOrWhiteSpace(codbarra))
        //    {
        //        var codigoBarra = LinhaDigitavalEmCodigo(codbarra);
        //        if (codigoBarra.Length < 44)
        //        {
        //            string linha = codigoBarra.Substring(33);
        //            Int64 zero = ToInt64(linha);
        //            if (zero == 0)
        //            {
        //                var fatorVencimento = GerarFatorVencimento(vencimento);
        //                var valorDoc = GerarValorBoleto(valor, 10);
        //                linha = codigoBarra.Substring(0, 33);
        //                var novoCod = string.Concat(linha, fatorVencimento, valorDoc);
        //                retorno = LinhaDigitavalEmCodigo(novoCod);
        //            }
        //        }
        //        else
        //        {
        //            string linha = codigoBarra.Substring(5, 14);
        //            string inicio = codigoBarra.Substring(0, 5);
        //            string fim = codigoBarra.Substring(19);
        //            Int64 zero = ToInt64(linha);
        //            if (zero == 0)
        //            {
        //                var fatorVencimento = GerarFatorVencimento(vencimento);
        //                var valorDoc = GerarValorBoleto(valor, 10);
        //                linha = codigoBarra.Substring(0, 33);
        //                var novoCod = string.Concat(inicio, fatorVencimento, valorDoc, fim);
        //                codigoBarra = LinhaDigitavalEmCodigo(novoCod);
        //                if (codigoBarra.Length == 44)
        //                    retorno = codigoBarra;
        //            }
        //            else
        //            {
        //                if (codigoBarra.Length == 44)
        //                    retorno = codigoBarra;
        //            }
        //            retorno = codigoBarra;
        //        }
        //    }

        //    try
        //    {
        //        var codBar = retorno.Substring(0, 4) + retorno.Substring(5);
        //        var dvGeral = Utils.ToInt32(retorno.Substring(4, 1));
        //        string banco = retorno.Substring(0, 3);
        //        int digito = 0;
        //        if (banco == "104")
        //        {
        //            digito = Utils.Mod11Base9(codBar);
        //        }
        //        else
        //        {
        //            digito = Utils.Mod11Peso2a9(codBar);
        //        }





        //        string Codbarra = string.Concat(retorno.Substring(0, 4), digito,
        //            retorno.Substring(5));

        //        return Codbarra;
        //    }
        //    catch (Exception)
        //    {
        //        return retorno;
        //    }
        //    //return retorno;
        //}

        /// <summary>
        /// Alinha o texto a esquerda e preenche com o tamanho total e texto informado.
        /// </summary>
        /// <param name="texto"></param>
        /// <param name="tamnaho"></param>
        /// <param name="preencher"></param>
        /// <returns></returns>
        public static string AlinharEsquerda(string texto, int tamnaho, string preencher)
        {
            while (texto.Length < tamnaho)
            {
                texto = preencher + texto;
            }

            return texto;
        }

        /// <summary>
        /// Alinha o texto a direita e preenche com o tamanho total e texto informado.
        /// </summary>
        /// <param name="texto"></param>
        /// <param name="tamnaho"></param>
        /// <param name="preencher"></param>
        /// <returns></returns>
        public static string AlinharDireita(string texto, int tamnaho, string preencher)
        {
            while (texto.Length < tamnaho)
            {
                texto = texto + preencher;
            }

            return texto;
        }

        //Função do algoritmo em si, onde verifica o código verificador do barcode.
        public static bool verificaDigitoArrecadacao(string codigoBarra)
        {
            //Declaração de variáveis
            int i, somaTotal, somaPar = 0, somaImpar = 0, multiplo, digito;
            for (i = 0; i < codigoBarra.Length - 1; i++) //Laço para percorrer a String flexível ao seu tamanho.
            {
                if ((i + 1) % 2 == 0) //Verificação da posíção do número, se é par ou ímpar.
                    somaPar += (codigoBarra[i] - 48) * 3; //Caso Par, multiplica-se por 3.
                else somaImpar += (codigoBarra[i] - 48) * 1; //Caso Impar, multiplica-se por 1.
            }

            somaTotal = somaPar + somaImpar; // Soma de todos resultados.
            if (somaTotal % 10 != 0 && somaTotal > 10)
                //Algoritmo para encontrar o múltiplo de 10 mais perto, igual ou maior.
                multiplo = somaTotal / 10 + 1;
            else if (somaTotal < 10)
                multiplo = 1;
            else multiplo = somaTotal / 10; //fim
            digito = multiplo * 10 - somaTotal; //Diminui-se do múltiplo o valor da soma total.
            int valorVerifica = codigoBarra[codigoBarra.Length - 1] - 48;
            if (digito != valorVerifica) //Verifica-se o dígito é igual ao 13º número do barcode.
                return false; //Caso não, retorna falso.
            return true; //Caso igual, retorna verdadeiro.

        }


        #endregion

        //Observação importante: Ao tentar utilizar diversos tipos de conversão e não obter sucesso, me basiei na tabela
        //ascii para ter o valor (-48), ao usar os valores de char, nos momentos que precisei comparar int com string.

        #region Mod

        /// <summary>
        /// Retorna digito verificador do calculo.
        /// </summary>
        /// <param name="seq"></param>
        /// <returns>Int32</returns>
        public static int Mod10(string seq)
        {
            /* Variáveis
             * -------------
             * d - Dígito
             * s - Soma
             * p - Peso
             * b - Base
             * r - Resto
             */

            int d, s = 0, p = 2, r;

            for (int i = seq.Length; i > 0; i--)
            {
                r = Convert.ToInt32(Mid(seq, i, 1)) * p;

                if (r > 9)
                    r = r / 10 + r % 10;

                s += r;

                if (p == 2)
                    p = 1;
                else
                    p = p + 1;
            }

            d = (10 - s % 10) % 10;
            return d;
        }

        /// <summary>
        /// Retorna digito verificador do calculo.
        /// </summary>
        /// <param name="seq"></param>
        /// <returns>Int32</returns>
        public static int Mod11Peso2a9(string seq)
        {
            /* Variáveis
             * -------------
             * d - Dígito
             * s - Soma
             * p - Peso
             * b - Base
             * r - Resto
             */

            int d, r, s = 0, p = 2, b = 9;
            string n;

            for (int i = seq.Length; i > 0; i--)
            {
                n = Mid(seq, i, 1);

                s = s + Convert.ToInt32(n) * p;

                if (p < b)
                    p = p + 1;
                else
                    p = 2;
            }

            r = s * 10 % 11;

            if (r == 0 || r == 1 || r == 10)
                d = 1;
            else
                d = r;

            return d;

        }

        /// <summary>
        /// Retorna digito verificador do calculo.
        /// </summary>
        /// <param name="seq"></param>
        /// <returns>Int32</returns>
        public static int Mod11(string seq, int b)
        {
            /* Variáveis
             * -------------
             * d - Dígito
             * s - Soma
             * p - Peso
             * b - Base
             * r - Resto
             */

            int d, s = 0, p = 2;


            for (int i = seq.Length; i > 0; i--)
            {
                s = s + Convert.ToInt32(Mid(seq, i, 1)) * p;
                if (p == b)
                    p = 2;
                else
                    p = p + 1;
            }

            d = 11 - s % 11;


            if (d > 9 || d == 0 || d == 1)
                d = 1;

            return d;
        }

        /// <summary>
        /// Retorna digito verificador do calculo.
        /// </summary>
        /// <param name="seq"></param>
        /// <returns>Int32</returns>
        public static int Mod11Base9(string seq)
        {
            /* Variáveis
             * -------------
             * d - Dígito
             * s - Soma
             * p - Peso
             * b - Base
             * r - Resto
             */

            int d, s = 0, p = 2, b = 9;


            for (int i = seq.Length - 1; i >= 0; i--)
            {
                string aux = Convert.ToString(seq[i]);
                s += Convert.ToInt32(aux) * p;
                if (p >= b)
                    p = 2;
                else
                    p = p + 1;
            }

            if (s < 11)
            {
                d = 11 - s;
                return d;
            }
            else
            {
                d = 11 - s % 11;
                if (d > 9 || d == 0)
                    d = 0;

                return d;
            }
        }

        /// <summary>
        /// Retorna digito verificador do calculo.
        /// </summary>
        /// <param name="seq"></param>
        /// <returns>Int32</returns>
        public static int Mod11(string seq, int lim, int flag)
        {
            int mult = 0;
            int total = 0;
            int pos = 1;
            //int res = 0;
            int ndig = 0;
            int nresto = 0;
            string num = string.Empty;

            mult = 1 + seq.Length % (lim - 1);

            if (mult == 1)
                mult = lim;


            while (pos <= seq.Length)
            {
                num = Mid(seq, pos, 1);
                total += Convert.ToInt32(num) * mult;

                mult -= 1;
                if (mult == 1)
                    mult = lim;

                pos += 1;
            }

            nresto = total % 11;
            if (flag == 1)
                return nresto;
            else
            {
                if (nresto == 0 || nresto == 1 || nresto == 10)
                    ndig = 1;
                else
                    ndig = 11 - nresto;

                return ndig;
            }
        }

        /// <summary>
        /// Retorna digito verificador do calculo.
        /// </summary>
        /// <param name="seq"></param>
        /// <returns>Int32</returns>
        public static int Mult10Mod11(string seq, int lim, int flag)
        {
            int mult = 0;
            int total = 0;
            int pos = 1;
            int ndig = 0;
            int nresto = 0;
            string num = string.Empty;

            mult = 1 + seq.Length % (lim - 1);

            if (mult == 1)
                mult = lim;

            while (pos <= seq.Length)
            {
                num = Mid(seq, pos, 1);
                total += Convert.ToInt32(num) * mult;

                mult -= 1;
                if (mult == 1)
                    mult = lim;

                pos += 1;
            }

            nresto = total * 10 % 11;

            if (flag == 1)
                return nresto;
            else
            {
                if (nresto == 0 || nresto == 1 || nresto == 10)
                    ndig = 1;
                else
                    ndig = nresto;

                return ndig;
            }
        }


        /// <summary>
        /// Retorna digito verificador do calculo.
        /// </summary>
        /// <param name="seq"></param>
        /// <returns>Int32</returns>
        public static string Mid(string str, int start, int? length = null)
        {
            if (!length.HasValue)
                return str.Substring(start - 1);
            else
                return str.Substring(start - 1, length.Value);
        }

        public static string Left(string s, int length)
        {
            return s.Substring(0, length);
        }

        public static string Right(string str, int length)
        {
            return str.Substring(str.Length - length);
        }


        /// <summary>
        /// Retorna digito verificador do calculo.
        /// </summary>
        /// <param name="seq"></param>
        /// <returns>Int32</returns>
        public static int Mod11(string seq)
        {
            /* Variáveis
             * -------------
             * d - Dígito
             * s - Soma
             * p - Peso
             * b - Base
             * r - Resto
             */

            int d, s = 0, p = 2, b = 9;

            for (int i = 0; i < seq.Length; i++)
            {
                s = s + Convert.ToInt32(seq[i]) * p;
                if (p < b)
                    p = p + 1;
                else
                    p = 2;
            }

            d = 11 - s % 11;
            if (d > 9)
                d = 0;
            return d;
        }

        /// <summary>
        /// Retorna digito verificador do calculo.
        /// </summary>
        /// <param name="seq"></param>
        /// <returns>Int32</returns>
        public static int Mod11_Unic(string seq)
        {
            /* Variáveis
             * -------------
             * d - Dígito
             * s - Soma
             * p - Peso
             * b - Base
             * r - Resto
             */

            int d, s = 0, p = 2, b = 9;

            for (int i = 0; i < seq.Length; i++)
            {
                s = s + Convert.ToInt32(seq[i]) * p;
                if (p < b)
                    p = p + 1;
                else
                    p = 2;
            }

            d = 11 - s % 11;
            if (d > 9)
                d = 0;
            return d;
        }

        #endregion

        #region Manipulação de String

        /// <summary>
        /// retorna um array de strings de tamanho variável com os dados da linha (pode ser usado para qualquer leitura de arquivos de retorno || remessa)
        /// os dados no string pattern correspondem a intervalos fechados na matemática ex: [2-19] (fechado de 2 a 19)
        /// </summary>
        /// <param name="linha">string de onde os dados serão extraídos. por exemplo, uma linha de um arquivo de retorno</param>
        /// <param name="pattern">obrigatóriamente é necessário numero PAR de valores NUMÉRICOS no string pattern. ex: 1-1,2-19</param>
        /// <returns>um array de strings de tamanho variável contendo os dados lidos na linha: string[]</returns>
        /// <example>
        /// string[] dados = getDados(sLine, "1-1,2-394,395-400");
        /// </example>
        public static string[] GetDados(string linha, string pattern)
        {
            // separa os números
            pattern = pattern.Replace('-', ',');
            string[] coord = pattern.Split(',');

            //cria objeto para armazenágem, buffer.
            string[] dados = new string[coord.Length / 2];

            //pega os números de 2 em 2 e preenche o array
            int x = 0;
            for (int i = 0; i < coord.Length; i += 2)
            {
                dados[x] = linha.Substring(
                    Convert.ToInt32(coord[i]) - 1, Convert.ToInt32(coord[i + 1]) - Convert.ToInt32(coord[i]) + 1);
                //arg[x] = linha.Substring(Convert.ToInt32(coord[i]), Convert.ToInt32(coord[i + 1]));
                x++;
            }

            //retorna os dados
            return dados;
        }

        /// <summary>
        /// Remove Mascara CNPJ
        /// </summary>
        /// <param name="cnpj"></param>
        /// <returns>string</returns>
        public static string RemoveMascaraCNPJ(string cnpj)
        {
            string retorno =
                cnpj.Replace(".", "")
                    .Replace(".", "")
                    .Replace(".", "")
                    .Replace(".", "")
                    .Replace("/", "")
                    .Replace("-", "");
            return retorno;
        }

        /// <summary>
        /// Substitui Caracteres Especiais
        /// </summary>
        /// <param name="strline"></param>
        /// <returns>string</returns>
        public static string SubstituiCaracteresEspeciais(string strline)
        {
            try
            {
                strline = strline.Replace("ã", "a");
                strline = strline.Replace('Ã', 'A');
                strline = strline.Replace('â', 'a');
                strline = strline.Replace('Â', 'A');
                strline = strline.Replace('á', 'a');
                strline = strline.Replace('Á', 'A');
                strline = strline.Replace('à', 'a');
                strline = strline.Replace('À', 'A');
                strline = strline.Replace('ç', 'c');
                strline = strline.Replace('Ç', 'C');
                strline = strline.Replace('é', 'e');
                strline = strline.Replace('É', 'E');
                strline = strline.Replace('Ê', 'E');
                strline = strline.Replace('ê', 'e');
                strline = strline.Replace('õ', 'o');
                strline = strline.Replace('Õ', 'O');
                strline = strline.Replace('ó', 'o');
                strline = strline.Replace('Ó', 'O');
                strline = strline.Replace('ô', 'o');
                strline = strline.Replace('Ô', 'O');
                strline = strline.Replace('ú', 'u');
                strline = strline.Replace('Ú', 'U');
                strline = strline.Replace('ü', 'u');
                strline = strline.Replace('Ü', 'U');
                strline = strline.Replace('í', 'i');
                strline = strline.Replace('Í', 'I');
                strline = strline.Replace('ª', 'a');
                strline = strline.Replace('º', 'o');
                strline = strline.Replace('°', 'o');
                strline = strline.Replace('&', 'e');

                strline = strline.Replace('!', new char());
                strline = strline.Replace('@', new char());
                strline = strline.Replace('#', new char());
                strline = strline.Replace('$', new char());
                strline = strline.Replace('%', new char());
                strline = strline.Replace('¨', new char());
                strline = strline.Replace('&', new char());
                strline = strline.Replace('*', new char());
                strline = strline.Replace('(', new char());
                strline = strline.Replace(')', new char());
                strline = strline.Replace('-', new char());
                strline = strline.Replace('+', new char());
                strline = strline.Replace('=', new char());
                strline = strline.Replace('[', new char());
                strline = strline.Replace(']', new char());
                strline = strline.Replace('{', new char());
                strline = strline.Replace('}', new char());
                strline = strline.Replace(';', new char());
                strline = strline.Replace(':', new char());
                strline = strline.Replace(':', new char());
                strline = strline.Replace('"', new char());

                char[] buffer = new char[strline.Length];
                int idx = 0;

                foreach (char c in strline)
                {
                    if (c >= '0' && c <= '9' || c >= 'A' && c <= 'Z'
                                               || c >= 'a' && c <= 'z' || c == '.' || c == '_')
                    {
                        buffer[idx] = c;
                        idx++;
                    }
                }

                return new string(buffer, 0, idx);
            }
            catch (Exception ex)
            {
                Exception tmpEx = new Exception("Erro ao formatar string.", ex);
                throw tmpEx;
            }
        }

        /// <summary>
        /// Converter Decimal em String
        /// </summary>
        /// <param name="valorParametro"></param>
        /// <returns>string</returns>
        public static string ConverterDecimalString(decimal valorParametro)
        {
            string valor = string.Empty;
            try
            {
                var strValor = decimal.Round(Convert.ToDecimal(valorParametro), 2);
                valor = strValor.ToString().Replace(".", ",");
                var vl = valor.Split(',');
                string esq = vl.FirstOrDefault();
                string direita = string.Empty;

                if (vl.Count() > 1)
                {
                    direita = vl.LastOrDefault();
                }

                if (direita.Length == 1)
                    direita = direita + "0";
                if (direita.Length == 0)
                    direita = direita + "00";

                valor = string.Empty;
                valor = esq + direita;
            }
            catch (Exception)
            {
                valor = string.Empty;
            }

            return valor;
        }

        /// <summary>
        /// Retorna uma string de data no formato Japones yyyy/MM/dd
        /// </summary>
        /// <param name="dia">Datetime</param>
        /// <returns>string</returns>
        public static string DataFormatoJapones(DateTime dia)
        {
            return dia == null ? string.Empty : dia.ToString("yyyy/MM/dd");
        }

        /// <summary>
        /// Retorna uma string de data no formato Americano yyyy/MM/dd
        /// </summary>
        /// <param name="dia">Datetime</param>
        /// <returns>string</returns>
        public static string DataFormatoAmericano(DateTime dia)
        {
            return dia == null ? string.Empty : dia.ToString("MM/dd/yyyy");
        }

        public static string DataFormatoBR(DateTime dia)
        {
            return dia == null ? string.Empty : dia.ToString("dd/MM/yyyy");
        }
        /// <summary>
        /// Gera string criptografada somente de ida.
        /// </summary>
        /// <param name="texto"></param>
        /// <returns>string</returns>
        public static string Criptografar(string texto)
        {
            MD5CryptoServiceProvider _Provider = new MD5CryptoServiceProvider();
            byte[] data = Encoding.ASCII.GetBytes(texto);
            data = _Provider.ComputeHash(data);
            StringBuilder _SbData = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                _SbData.Append(data[i]);
            }

            return _SbData.ToString();
        }

        /// <summary>
        /// Criptogra a Ida
        /// </summary>
        /// <param name="valor"></param>
        /// <returns>string</returns>
        public static string Base64Criptografar(string valor)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(valor);
            return Convert.ToBase64String(plainTextBytes);
        }

        /// <summary>
        /// Descriptografa a Ida
        /// </summary>
        /// <param name="valor"></param>
        /// <returns>string</returns>
        public static string Base64Descriptografar(string valor)
        {
            var base64EncodedBytes = Convert.FromBase64String(valor);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }


        /// <summary>
        /// Gera uma string randomica de numero e texto com tamanho Baseada no numero informado
        /// </summary>
        /// <param name="maxSize"></param>
        /// <returns>string</returns>
        public static string GerarNumericUniqueKey(int maxSize)
        {
            //maxSize += 10;
            var dataReg = DateTime.Now;
            char[] chars = new char[62];
            string datastring = dataReg.Year + dataReg.Month + dataReg.Day + dataReg.Hour + dataReg.Minute +
                                dataReg.Millisecond +
                                "1234567890";
            chars = datastring.ToCharArray();
            byte[] data = new byte[1];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetNonZeroBytes(data);
                data = new byte[maxSize];
                crypto.GetNonZeroBytes(data);
            }

            StringBuilder result = new StringBuilder(maxSize);
            foreach (byte b in data)
            {
                result.Append(chars[b % chars.Length]);
            }

            string strKey = result.ToString().ToUpper();

            if (maxSize < 30)
                return strKey.Substring(0, maxSize);

            string retorno = ExtrairDaPosicao(strKey, 1, 8) + "-" + ExtrairDaPosicao(strKey, 9, 12) + "-" +
                             ExtrairDaPosicao(strKey, 13, 16) + "-" + ExtrairDaPosicao(strKey, 17, 20) +
                             "-" +
                             ExtrairDaPosicao(strKey, 21, 32);
            return retorno;
        }

        /// <summary>
        /// Gera uma string randomica somente de texto com tamanho Baseada no numero informado
        /// </summary>
        /// <param name="maxSize"></param>
        /// <returns>string</returns>
        public static string GerarStringUniqueKey(int maxSize)
        {
            maxSize += 10;
            var dataReg = DateTime.Now;
            char[] chars = new char[62];
            string datastring = dataReg.Year + dataReg.Month + dataReg.Day + dataReg.Hour + dataReg.Minute +
                                dataReg.Millisecond +
                                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            chars = datastring.ToCharArray();
            byte[] data = new byte[1];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetNonZeroBytes(data);
                data = new byte[maxSize];
                crypto.GetNonZeroBytes(data);
            }

            StringBuilder result = new StringBuilder(maxSize);
            foreach (byte b in data)
            {
                result.Append(chars[b % chars.Length]);
            }

            string strKey = result.ToString().ToUpper();

            if (maxSize < 32)
                return strKey;

            string retorno = ExtrairDaPosicao(strKey, 1, 8) + "-" + ExtrairDaPosicao(strKey, 9, 12) + "-" +
                             ExtrairDaPosicao(strKey, 13, 16) + "-" + ExtrairDaPosicao(strKey, 17, 20) +
                             "-" +
                             ExtrairDaPosicao(strKey, 21, 32);
            return retorno;
        }

        /// <summary>
        /// Retorna uma string com a acentuação grafica removida.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string RemoveAcento(string value)
        {
            string normalizedString = value.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < normalizedString.Length; i++)
            {
                char c = normalizedString[i];
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    stringBuilder.Append(c);
            }

            return stringBuilder.ToString();
        }


        /// <summary>
        /// Retorna Codigo de Barras baseado na linha digitavel informada.
        /// </summary>
        /// <param name="barra"></param>
        /// <returns></returns>
        public static string LinhaDigitavalEmCodigo(string barra)
        {
            string codigoDeBarras = string.Empty;
            try
            {

                var codigobarra = string.Empty;
                if (!string.IsNullOrWhiteSpace(barra))
                {
                    var Codbarra =
                        barra.Replace(".", "")
                            .Replace(".", "")
                            .Replace(".", "")
                            .Replace(".", "")
                            .Replace(" ", "")
                            .Replace(" ", "")
                            .Replace(" ", "")
                            .Replace(" ", "");
                    codigobarra = Codbarra;
                    if (codigobarra.Length > 44)
                    {

                        if (codigobarra.Substring(0, 1) == "8")
                        {
                            var barra1 = ExtrairDaPosicao(Codbarra, 1, 11);
                            var barra2 = ExtrairDaPosicao(Codbarra, 13, 23);
                            var barra3 = ExtrairDaPosicao(Codbarra, 25, 35);
                            var barra4 = ExtrairDaPosicao(Codbarra, 37, 47);
                            codigobarra = string.Concat(barra1, barra2, barra3, barra4);
                        }
                        else
                        {
                            codigobarra = Codbarra.Substring(0, 4)
                                          + Codbarra.Substring(32, 15)
                                          + Codbarra.Substring(4, 5)
                                          + Codbarra.Substring(10, 10)
                                          + Codbarra.Substring(21, 10);
                        }


                    }

                    codigoDeBarras = codigobarra;
                }
            }
            catch (Exception ex)
            {
                codigoDeBarras = ex.Message;
            }


            return codigoDeBarras;
        }

        /// <summary>
        /// Formata o campo de acordo com o tipo e o tamanho 
        /// </summary>        
        public static string FitStringLength(string SringToBeFit, int maxLength, int minLength, char FitChar,
            int maxStartPosition, bool maxTest, bool minTest, bool isNumber)
        {
            try
            {
                string result = "";

                if (maxTest == true)
                {
                    // max
                    if (SringToBeFit.Length > maxLength)
                    {
                        result += SringToBeFit.Substring(maxStartPosition, maxLength);
                    }
                }

                if (minTest == true)
                {
                    // min
                    if (SringToBeFit.Length <= minLength)
                    {
                        if (isNumber == true)
                        {
                            result += new string(FitChar, minLength - SringToBeFit.Length) + SringToBeFit;
                        }
                        else
                        {
                            result += SringToBeFit + new string(FitChar, minLength - SringToBeFit.Length);
                        }
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                Exception tmpEx = new Exception("Problemas ao Formatar a string. String = " + SringToBeFit, ex);
                throw tmpEx;
            }
        }



        #endregion

        #region Validação



        /* Exemplo de Leitura de um arquivo de remessa
        private void button1_Click(object sender, EventArgs e)
        { //ler arquivo de texto
            StreamReader objReader = new StreamReader("C:\\Documents and Settings\\uis\\Desktop\\bancos\\CED006877211081.REM");
            string sLine = "";
            string[] dados;
        
            while (sLine != null)
            {
                sLine = objReader.ReadLine();
                if (sLine != null){
                    dados = getDados(sLine, "1-1,2-394,395-400");
                    // adicionar os dados a um string
                    textBox1.Text += " posição:<" + dados[2] + ">";
                    // poderia ser
                    //new boleto_dados(dados[0],dados[1],dados[2]);
                }
            }
            objReader.Close();
        }
        */

        /// <summary>
        /// Retorna veradadeiro ou falso se a string é ou não um numero.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNumber(string value)
        {
            Regex objNotNumberPattern = new Regex("[^0-9.-]");
            Regex objTwoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
            Regex objTwoMinusPattern = new Regex("[0-9]*[-][0-9]*[-][0-9]*");
            string strValidRealPattern = "^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$";
            string strValidIntegerPattern = "^([-]|[0-9])[0-9]*$";
            Regex objNumberPattern = new Regex("(" + strValidRealPattern + ")|(" + strValidIntegerPattern + ")");

            return !objNotNumberPattern.IsMatch(value) &&
                   !objTwoDotPattern.IsMatch(value) &&
                   !objTwoMinusPattern.IsMatch(value) &&
                   objNumberPattern.IsMatch(value);
        }

        /// <summary>
        /// Retorna veradadeiro ou falso se a string é ou não uma data valida.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool DataValida(DateTime dateTime)
        {
            if (dateTime.ToString("dd/MM/yyyy") == "01/01/1900" | dateTime.ToString("dd/MM/yyyy") == "01/01/0001")
                return false;
            else
                return true;
        }



        /// <summary>
        /// Retorna veradadeiro ou falso se a string é ou não um cep.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ValidarCep(string cep)
        {
            if (cep.Length == 8)
            {
                cep = cep.Substring(0, 5) + "-" + cep.Substring(5, 3);
                //txt.Text = cep;
            }

            return Regex.IsMatch(cep, "[0-9]{5}-[0-9]{3}");
        }

        /// <summary>
        /// Retorna veradadeiro ou falso se a string é ou não um cpf valido.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ValidaCpf(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;

            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "").Replace("_", "");

            if (string.IsNullOrWhiteSpace(cpf))
                return false;

            if (cpf.Length != 11)
                return false;

            // Caso coloque todos os numeros iguais
            switch (cpf)
            {
                case "11111111111":
                    return false;
                case "00000000000":
                    return false;
                case "22222222222":
                    return false;
                case "33333333333":
                    return false;
                case "44444444444":
                    return false;
                case "55555555555":
                    return false;
                case "66666666666":
                    return false;
                case "77777777777":
                    return false;
                case "88888888888":
                    return false;
                case "99999999999":
                    return false;
            }


            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();

            tempCpf = tempCpf + digito;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            bool retorno = false;
            retorno = cpf.EndsWith(digito);
            return retorno;
        }

        /// <summary>
        /// Retorna veradadeiro ou falso se a string é ou não um cnpj valido.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ValidaCnpj(string cnpj)
        {

            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            int soma;

            int resto;

            string digito;

            string tempCnpj;

            cnpj = cnpj.Trim();

            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

            if (cnpj.Length != 14)
                return false;

            // Caso coloque todos os numeros iguais
            switch (cnpj)
            {
                case "11111111111111":
                    return false;
                case "00000000000000":
                    return false;
                case "22222222222222":
                    return false;
                case "33333333333333":
                    return false;
                case "44444444444444":
                    return false;
                case "55555555555555":
                    return false;
                case "66666666666666":
                    return false;
                case "77777777777777":
                    return false;
                case "88888888888888":
                    return false;
                case "99999999999999":
                    return false;
            }

            tempCnpj = cnpj.Substring(0, 12);

            soma = 0;

            for (int i = 0; i < 12; i++)

                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            resto = soma % 11;

            if (resto < 2)

                resto = 0;

            else

                resto = 11 - resto;

            digito = resto.ToString();

            tempCnpj = tempCnpj + digito;

            soma = 0;

            for (int i = 0; i < 13; i++)

                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = soma % 11;

            if (resto < 2)

                resto = 0;

            else

                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cnpj.EndsWith(digito);

        }


        /// <summary>
        /// Retorna veradadeiro ou falso se a string é ou não um email valido.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ValidarEmail(string email)
        {
            Regex rg = new Regex(
                @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");

            if (rg.IsMatch(email))
                return true;
            else
            {
                return false;
            }
        }


        public static DateTime AjustarDataGoogle(DateTime dt)
        {
            var hoje = DateTime.Now;
            if (dt.Year > hoje.Year)
                dt = dt.AddYears(-1);
            else
            {
                if (dt.Month > hoje.Month)
                    dt = dt.AddMonths(-1);
            }

            if (dt.Month > hoje.Month)
                dt = dt.AddMonths(-1);

            return dt;
        }

        public static DateTime AjustarDataGoogle(DateTime hoje, DateTime dt)
        {
            if (dt.Year > hoje.Year)
                dt = dt.AddYears(-1);
            else
            {
                if (dt.Month > hoje.Month)
                    dt = dt.AddMonths(-1);
            }

            if (dt.Month > hoje.Month)
                dt = dt.AddMonths(-1);

            return dt;
        }

        #endregion

        #region ticketStatus
        public static string CheckHubspotStatusName(string statusTicket)
        {
            var statusName = "";

            switch (statusTicket)
            {
                case "101268888":
                    statusName = "Closed";
                    break;
                case "1":
                    statusName = "In Development";
                    break;
                case "2":
                    statusName = "In Validation";
                    break;
                case "3":
                    statusName = "In Deployment";
                    break;
                case "4":
                    statusName = "Client Feedback";
                    break;
                default:
                    statusName = statusTicket;
                    break;
            }

            return statusName;
        }
        #endregion

        #region hubspotUser
            public static string GetHubspotUserName(string userId)
            {
                var userName = "";

                switch (userId)
                {
                    case "465285487":
                    userName = "Paulo Melo";
                        break;
                    case "465285488":
                    userName = "Fernando Cruz";
                        break;
                    case "465285489":
                    userName = "Fernando Arruda";
                        break;
                    case "465285490":
                    userName = "Sema DEMIR";
                        break;
                    case "465285491":
                    userName = "Mellisa Wilson";
                        break;
                    case "465285492":
                    userName = "Renato Basso";
                        break;
                    case "465285494":
                    userName = "Alexandre Witkowicz";
                        break;
                    case "465285495":
                    userName = "Omar Orra";
                        break;
                    case "465285496":
                    userName = "Gabriel Serra";
                        break;
                    case "465285497":
                        userName = "Neil Webers";
                        break;
                    case "465285498":
                        userName = "Yannick BORDEREAU";
                        break;
                    case "465354115":
                        userName = "Alexandre Alves Dias";
                        break;
                    case "559295585":
                        userName = "Bruno Mazzinghy";
                        break;
                    default:
                        userName = userId;
                        break;
                }

                return userName;
            }
        #endregion

        #region loginTxt
            public static bool CheckLogin(string login, string password)
        {
            string fixedLogin = "hubspot-integration@solvace.com";
            string fixedPassword = "pdElec5!@";
            if (login == fixedLogin && password == fixedPassword)            
                return true;            
                        
            return false;
            
        }
        #endregion

        #region UtilsDiario
        public static string ConvertAnoNivel(string txt)
        {
            var txtConverted = "";
            switch (txt)
            {
                case "primeiro_em":
                    txtConverted = "1° Ano";
                    break;
                case "segundo_em":
                    txtConverted = "2° Ano";
                    break;
                case "terceiro_em":
                    txtConverted = "3° Ano";
                    break;
                case "primeiro":
                    txtConverted = "1° Ano";
                    break;
                case "segundo":
                    txtConverted = "2° Ano";
                    break;
                case "terceiro":
                    txtConverted = "3° Ano";
                    break;
                case "quarto":
                    txtConverted = "4° Ano";
                    break;
                case "quinto":
                    txtConverted = "5° Ano";
                    break;
                case "sexto":
                    txtConverted = "6° Ano";
                    break;
                case "setimo":
                    txtConverted = "7° Ano";
                    break;
                case "oitavo":
                    txtConverted = "8° Ano";
                    break;
                case "nono":
                    txtConverted = "9° Ano";
                    break;
                case "ef":
                    txtConverted = "Ensino Fundamental";
                    break;
                case "em":
                    txtConverted = "Ensino Médio";
                    break;
                default:
                    txtConverted = txt;
                    break;
            }
            return txtConverted;
        }

        public static string NomeDaMateria(string txt)
        {
            var txtConverted = "";
            switch (txt)
            {
                case "portugues":
                    txtConverted = "Português";
                    break;
                case "ingles":
                    txtConverted = "Inglês";
                    break;
                case "artes":
                    txtConverted = "Artes";
                    break;
                case "matematica":
                    txtConverted = "Matemática";
                    break;
                case "ciencias":
                    txtConverted = "Ciências";
                    break;
                case "educacao_fisica":
                    txtConverted = "Educação Fisíca";
                    break;
                case "ensino_religioso":
                    txtConverted = "Ensino Religioso";
                    break;
                case "historia":
                    txtConverted = "História";
                    break;
                case "geografia":
                    txtConverted = "Geografia";
                    break;
                default:
                    txtConverted = txt;
                    break;
            }
            return txtConverted;
        }

        public static string NomeDoBimestre(string txt)
        {
            var txtConverted = "";
            switch (txt)
            {
                case "primeiro":
                    txtConverted = "1° Bimestre";
                    break;
                case "segundo":
                    txtConverted = "2° Bimestre";
                    break;
                case "terceiro":
                    txtConverted = "3° Bimestre";
                    break;
                case "quarto":
                    txtConverted = "4° Bimestre";
                    break;                
                default:
                    txtConverted = txt;
                    break;
            }
            return txtConverted;
        }

        public static string ConvertTurma(int txt)
        {
            dynamic txtConverted;
            switch (txt)
            {
                case 1:
                    txtConverted = "Turma 1";
                    break;
                case 2:
                    txtConverted = "Turma 2";
                    break;
                case 3:
                    txtConverted = "Turma 3";
                    break;
                case 4:
                    txtConverted = "Turma 4";
                    break;            
                default:
                    txtConverted = txt;
                    break;
            }
            return txtConverted;
        }
        //public static dynamic ComboAno()
        //{
        //    List<SelectListItem> combo = new List<SelectListItem>
        //    {
        //        new SelectListItem { Value = "primeiro", Text = "1° Ano" },
        //        new SelectListItem { Value = "segundo", Text = "2° Ano" },
        //        new SelectListItem { Value = "terceiro", Text = "3° Ano" },
        //        new SelectListItem { Value = "quarto", Text = "4° Ano" },
        //        new SelectListItem { Value = "quinto", Text = "5° Ano" },
        //        new SelectListItem { Value = "sexto", Text = "6° Ano" },
        //        new SelectListItem { Value = "setimo", Text = "7° Ano" },
        //        new SelectListItem { Value = "oitavo", Text = "8° Ano" },
        //        new SelectListItem { Value = "nono", Text = "9° Ano" },
        //        // Adicione quantos itens desejar
        //    };         

        //    return combo;
        //}

        //public static dynamic ComboNivel()
        //{
        //    List<SelectListItem> combo = new List<SelectListItem>
        //        {
        //            new SelectListItem { Value = "em", Text = "Ensino Médio" },
        //            new SelectListItem { Value = "ef", Text = "Ensino Fundamental" }
        //        };

        //    return combo;
        //}

        //public static dynamic ComboTurma()
        //{
        //    List<SelectListItem> combo = new List<SelectListItem>
        //        {
        //            new SelectListItem { Value = "1", Text = "Turma 1" },
        //            new SelectListItem { Value = "2", Text = "Turma 2" },
        //            new SelectListItem{ Value = "3", Text = "Turma 3" },
        //            new SelectListItem { Value = "4", Text = "Turma 4" },
        //        };

        //    return combo;
        //} 
        //public static dynamic ComboBimestre()
        //{
        //    List<SelectListItem> combo = new List<SelectListItem>
        //        {
        //            new SelectListItem { Value = "1", Text = "1° Bimestre" },
        //            new SelectListItem { Value = "2", Text = "2° Bimestre" },
        //            new SelectListItem{ Value = "3", Text = "3° Bimestre" },
        //            new SelectListItem { Value = "4", Text = "4° Bimestre" },
        //        };

        //    return combo;
        //}

        //public static dynamic ComboMateria()
        //{
        //    List<SelectListItem> combo = new List<SelectListItem>
        //        {
        //            new SelectListItem { Value = "1", Text = "Português" },
        //            new SelectListItem { Value = "2", Text = "Inglês" },
        //            new SelectListItem{ Value = "3", Text = "Artes" },
        //            new SelectListItem { Value = "4", Text = "Matemática" },
        //            new SelectListItem { Value = "5", Text = "Ciências" },
        //            new SelectListItem { Value = "6", Text = "Educação Fisíca" },
        //            new SelectListItem { Value = "7", Text = "Ensino Religioso" },
        //            new SelectListItem { Value = "8", Text = "História" },
        //            new SelectListItem { Value = "9", Text = "Geografia" },
        //        };

        //    return combo;
        //}
        #endregion

    }
}
