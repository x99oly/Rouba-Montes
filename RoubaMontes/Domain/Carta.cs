using System;

namespace RoubaMontes.Domain
{
    public class Carta : IComparable<Carta>
    {
        public char Naipe { get; private set; }
        public int Numero { get; private set; }

        public Carta(int numero, char naipe)
        {
            Numero = numero;
            Naipe = naipe;
        }

        public override string ToString()
        {
            return $"{Numero:D2}{Naipe}";
        }

        public override int GetHashCode()
        {
            return Numero.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            if (this == null) return false;

            var other = obj as Carta;

            if (other == null) return false;

            return Numero == other.Numero;
        }

        public int CompareTo(Carta? other)
        {
            if (other == null) return 1;

            var carta = other as Carta;

            if (carta == null) return 1;

            if (other.Equals(carta)) return 0;

            return carta.Numero > this.Numero ? 1 : -1;
        }
    } 
}
