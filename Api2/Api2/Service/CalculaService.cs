using System;
using System.Net.Http;

namespace Api2.Service
{
    public class CalculaService
    {
        public string CalcularTaxa(string valorInicial, string meses)
        {
            string retorno;
            try
            {
                retorno = ValidarParametros(valorInicial, meses);

                if (string.IsNullOrWhiteSpace(retorno))
                {
                    double valorFinal = Convert.ToDouble(valorInicial);
                    
                    for (int tempo = 0; tempo < Convert.ToInt32(meses); tempo++)
                        valorFinal *= 1 + BuscarTaxaJuros();
                                       
                    return valorFinal.ToString("0.00");
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Erro {e.Message}");
            }
            return retorno;
        }

        /// <summary>
        /// Metodo criado para validar se os parametros são validos para o Calculo de Juros
        /// </summary>
        private string ValidarParametros(string valor, string tempo)
        {
            if (string.IsNullOrWhiteSpace(valor))
                return "Campo Valor Inicial Obrigatorio";

            if (string.IsNullOrWhiteSpace(tempo))
                return "Campo Messes Obrigatorio";

            if (double.TryParse(valor, out double valorValido) == false)
                return "Confira o campo Valor Inicial,  campo Númerico";

            if (Int32.TryParse(tempo, out  int messes) == false)
                return "Confira o campo Meses,  Campo númerico sem casas decimais";

            return string.Empty;
        }

        /// <summary>
        /// Metodo para buscar o valor da Taxa de Juros na Api1
        /// </summary>
        /// <returns></returns>
        public Double BuscarTaxaJuros()
        {
            var result = "";
            HttpClient client = new HttpClient();
            Uri urlBase = new Uri($"https://localhost:44392/api/");

            HttpResponseMessage response = client.GetAsync($"{urlBase}Api/TaxaJuros").Result;
            if (response.IsSuccessStatusCode)
            {
                result = response.Content.ReadAsStringAsync().Result;
                return Convert.ToDouble(result);
            }
            else
            {
                throw new Exception($"ERRO-{response.Content.ReadAsStringAsync().Result}");
            }
        }
    }
}
