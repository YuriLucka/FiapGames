using FiapCloudGames.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FiapCloudGames.Domain.ValueObjects
{
    public sealed class Email
    {
        public string Value { get; }

        public Email(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("E-mail não pode ser vazio.");

            if (!Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new DomainException("Formato de e-mail inválido.");

            Value = value.ToLowerInvariant();
        }

        public override string ToString() => Value;
        public override bool Equals(object? obj) => obj is Email other && Value == other.Value;
        public override int GetHashCode() => Value.GetHashCode();
    }
}
