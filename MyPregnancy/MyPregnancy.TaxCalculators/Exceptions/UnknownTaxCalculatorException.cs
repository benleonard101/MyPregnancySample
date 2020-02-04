namespace MyPregnancy.TaxCalculators.Exceptions
{
    using MyPregnancy.Common;
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class UnknownTaxCalculatorException : Exception
    {
        public UnknownTaxCalculatorException(Enums.Calculator calculator, Exception innerException)
            : base($"Could not find Tax Calculator for ({calculator}).", innerException)
        {

        }
        public UnknownTaxCalculatorException()
        {
        }

        public UnknownTaxCalculatorException(string message) : base(message)
        {
        }

        public UnknownTaxCalculatorException(string message, Exception inner) : base(message, inner)
        {
        }

        protected UnknownTaxCalculatorException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
