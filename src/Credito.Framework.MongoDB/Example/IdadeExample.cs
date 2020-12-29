namespace Credito.Framework.MongoDB.Example
{
    public record IdadeExample
    {
        private int _valor;

        internal IdadeExample(int valor) =>
            _valor = valor;

        internal int ToInt() =>
            _valor;

        public static IdadeExample FromInt(int valor) =>
            new IdadeExample(valor);

        public static implicit operator IdadeExample(int valor) => 
            FromInt(valor);
    }
}