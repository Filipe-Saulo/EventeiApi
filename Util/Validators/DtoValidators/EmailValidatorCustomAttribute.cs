using System;
using System.ComponentModel.DataAnnotations;


namespace Api.Util.Validators.DtoValidators
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class EmailValidatorCustomAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
                return false;

            return EmailValidator.Validar(value.ToString());
        }

        public override string FormatErrorMessage(string name)
        {
            return string.IsNullOrEmpty(ErrorMessage)
                ? $"{name} inválido. Informe um e-mail no formato correto."
                : ErrorMessage;
        }
    }
}
