using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Util.Validators.DtoValidators
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class CpfValidatorCustomAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var cpf = value as string;
            if (string.IsNullOrWhiteSpace(cpf))
                return false;

            return CpfValidator.Validar(cpf);
        }

        public override string FormatErrorMessage(string name)
        {
            return string.IsNullOrEmpty(ErrorMessage)
                ? $"{name} inválido. Informe um CPF com 11 dígitos válidos."
                : ErrorMessage;
        }
    }
}
