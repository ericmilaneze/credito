using System;
using MongoDB.Bson.Serialization;

namespace Credito.Framework.MongoDB
{
    public record Idade
    {
        private int _valor;

        internal Idade(int valor) =>
            _valor = valor;

        internal int ToInt() =>
            _valor;

        public static Idade FromInt(int valor) =>
            new Idade(valor);

        public static implicit operator Idade(int valor) => 
            FromInt(valor);
    }
}