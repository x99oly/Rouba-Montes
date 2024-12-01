﻿using System;

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
            return $"{Numero}{Naipe}";
        }

        public override int GetHashCode()
        {
            return Numero.GetHashCode();
        }

        /// <summary>
        /// Verifica se duas cartas são iguais com base em seu número 
        /// ** Naipe ignorado segundo regras do jogo **
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>True se Numero for igual</returns>
        /// <returns>False se entidade for nula</returns>
        /// <returns>False se a própria instância da classe for nula</returns>
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
