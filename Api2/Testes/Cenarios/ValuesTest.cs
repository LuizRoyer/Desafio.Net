using FluentAssertions;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Testes.Fixtures;
using Xunit;

namespace Testes.Cenarios
{
    public class ValuesTest
    {
        private readonly TestContext _testContext;
        public ValuesTest()
        {
            _testContext = new TestContext();
        }
               
        [Fact]
        public async Task<object> ValidarIntegracao()
        {
            HttpClient client = new HttpClient();
            Uri urlBase = new Uri($"https://localhost:44392/api/");

            HttpResponseMessage response = client.GetAsync($"{urlBase}api2/CalcularTaxa?valorInicial=100&meses=5").Result;
            if (response.IsSuccessStatusCode)
            {
               var result = await response.Content.ReadAsStringAsync();
                return   response.StatusCode.Should().Be(HttpStatusCode.OK);
            }
            else
            {
                throw new Exception($"ERRO-{response.Content.ReadAsStringAsync().Result}");
            }
        }

        [Fact]
        public async Task  CapturarRetornoResponse()
        {
           
            var response = await _testContext.Client.GetAsync($"/api2/CalcularJuros?valorInicial=100&meses=5");
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);         
                  }

        [Fact]
        public async Task CapturarIdValoresResponse()
        {          
            var response = await _testContext.Client.GetAsync($"/api2/CalcularJuros?valorInicial=100&meses=5");
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task CapturarErroResponse()
        {

            var response = await _testContext.Client.GetAsync($"/api2/CalcularJuros?valorInicial=100&meses=XXX");
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task CapturarErroFormatacapDados()
        {
            var response = await _testContext.Client.GetAsync($"/api2/CalcularJuros?valorInicial=xx&meses=5.5");
            response.EnsureSuccessStatusCode();
            response.Content.Headers.ContentType.ToString().Should().Be("text/plain; charset=utf-8");
        }
    }
}
