using System.Text.RegularExpressions;

namespace Api.Util.Validators
{
    public static class PhoneValidator
    {
        public static bool ValidarCelularBr(string phoneNumber, out string numeroFormatado)
        {
            numeroFormatado = null;

            if (string.IsNullOrWhiteSpace(phoneNumber))
                return false;

            // Remove todos os caracteres não numéricos
            string digitsOnly = Regex.Replace(phoneNumber, @"[^\d]", "");

            // Validações básicas
            if (digitsOnly.Length != 11)
                return false;

            if (digitsOnly[2] != '9')
                return false;

            // Validação de DDDs brasileiros válidos
            var dddsValidos = new[] {
            11, 12, 13, 14, 15, 16, 17, 18, 19,
            21, 22, 24, 27, 28,
            31, 32, 33, 34, 35, 37, 38,
            41, 42, 43, 44, 45, 46, 47, 48, 49,
            51, 53, 54, 55,
            61, 62, 63, 64, 65, 66, 67, 68, 69,
            71, 73, 74, 75, 77, 79,
            81, 82, 83, 84, 85, 86, 87, 88, 89,
            91, 92, 93, 94, 95, 96, 97, 98, 99
        };

            int ddd;
            if (!int.TryParse(digitsOnly.Substring(0, 2), out ddd) || !dddsValidos.Contains(ddd))
                return false;

            var numeroSemDDD = digitsOnly.Substring(2);

            // Verifica se o restante é composto por todos os dígitos iguais
            if (numeroSemDDD.Distinct().Count() == 1)
                return false;

            // Verifica padrões inválidos no final
            if (numeroSemDDD.EndsWith("0000") || numeroSemDDD.EndsWith("1234") || numeroSemDDD.EndsWith("4321"))
                return false;

            numeroFormatado = digitsOnly;
            return true;
        }
    }

}
