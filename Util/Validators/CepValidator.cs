using Microsoft.DotNet.Scaffolding.Shared;
using MySqlX.XDevAPI;
using System.ComponentModel.Design;
using System.Text.RegularExpressions;
using static Mysqlx.Notice.Warning.Types;
using static System.Net.Mime.MediaTypeNames;

namespace Api.Util.Validators
{
    public static class CepValidator
    {
        public static bool ValidarCep(int? cep)
        {
            // Verifica se tem valor
            if (!cep.HasValue)
                return false;

            // Converte para string com zeros à esquerda
            string cepStr = cep.Value.ToString("D8"); // Formata com 8 dígitos

            // Verifica se tem exatamente 8 dígitos
            if (cepStr.Length != 8)
                return false;

            // Verifica se não são todos dígitos iguais (ex: 00000000)
            if (cepStr.Distinct().Count() == 1)
                return false;

            // Verifica alguns CEPs inválidos conhecidos
            if (cepStr.StartsWith("000") || cepStr.StartsWith("999"))
                return false;

            // Verifica se o CEP está em um intervalo válido
            int cepNum = cep.Value;
            if (cepNum < 1000000 || cepNum > 99999999)
                return false;

            return true;
        }

        // SUGESTAO DE IMPLEMENTAÇÂO        

        //private static ICepService _cepService;

        //public static void Configure(ICepService cepService)
        //{
        //    _cepService = cepService;
        //}

        //public static async Task<bool> ValidarCepComApi(int? cep, string estado, string cidade, string bairro)
        //{
        //    if (!cep.HasValue) return false;

        //    string cepStr = cep.Value.ToString("D8");
        //    var endereco = await _cepService.BuscarEnderecoPorCep(cepStr);

        //    if (endereco == null) return false;

        //    // Valida consistência com outros campos do endereço
        //    return endereco.Uf.Equals(estado, StringComparison.OrdinalIgnoreCase) &&
        //           endereco.Localidade.Equals(cidade, StringComparison.OrdinalIgnoreCase) &&
        //           (string.IsNullOrEmpty(endereco.Bairro) ||
        //            endereco.Bairro.Equals(bairro, StringComparison.OrdinalIgnoreCase));
        //}
        // -------------------------------------------------------------------------------
        //ABAIXO ADICIONAR EM PROGRAM.CS CASO FOR UTILIZAR ESSE SUGESTAO

        // Registra o serviço de CEP
        //builder.Services.AddHttpClient<ICepService, SerproCepService>(client => 
        //{
        //    client.DefaultRequestHeaders.Add("Accept", "application/json");
        //    client.BaseAddress = new Uri("https://h-apigateway.conectagov.estaleiro.serpro.gov.br/api-cep/v1/");
        //    client.DefaultRequestHeaders.Add("x-api-key", builder.Configuration["Serpro:ApiKey"]);
        //});

        //// Configura o validador
        //builder.Services.AddSingleton(provider => 
        //{
        //    var cepService = provider.GetRequiredService<ICepService>();
        //CepValidator.Configure(cepService);
        //return CepValidator;
        //});
        //-----------------------------------------------------------------------------------------------
        //ABAIXO COLOCAR EM appsettings.json ----> A KEY DA API É SOLICITADA EM 
        //"Serpro": {
        //  "ApiKey": "sua-chave-aqui"
        //  }
        //
        //
        //Acesse o Portal do ConectaGov:

        //URL: https://conectagov.estaleiro.serpro.gov.br/

        //Este é o ambiente oficial para integrar com APIs do governo

        //Cadastro no Portal:

        //Clique em "Cadastre-se" (se for sua primeira vez)

        //Você precisará de:

        //Conta Gov.br(nível prata ou ouro)

        //Dados da sua organização

        //Solicitação de Acesso:

        //Após login, acesse "Área de Desenvolvimento"

        //Busque pelo serviço "API Consulta CEP"

        //Clique em "Solicitar Acesso"
    }
}

