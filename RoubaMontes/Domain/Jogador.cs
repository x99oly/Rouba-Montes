namespace RoubaMontes.Domain
{
    public class Jogador
    {
        public string Nome { get; private set; }
        public Monte MonteDeCartas { get; private set; }
        public int PosicaoNaUltimaPartida { get; private set; }
        public int TamanhoDoMonteNaUltimaPartida { get; private set; }

        public Jogador(string nome)
        {
            Nome = nome;
            MonteDeCartas = new Monte();
        }

        public void ComprarCarta(Carta carta)
        {
            if (MonteDeCartas == null) MonteDeCartas = new Monte(carta);

            else MonteDeCartas.AdicionarCarta(carta);
        }
        // Talvez seja melhor trocar o dicionário ----- Tuplas???
        public bool SelecionarMonte(Dictionary<Carta, Monte> montes, Carta carta)
        {
            if (montes.TryGetValue(carta, out Monte? novoMonte))
            {
                novoMonte.VincularJogador(this);
                while (novoMonte.MonteDeCartas.Count > 0)
                {
                    MonteDeCartas.AdicionarMonte(novoMonte);                    
                }
                montes.Remove(carta);
                return true;
            }
            return false;
        }

        public Carta UltimaCarta()
        {
            return MonteDeCartas.UltimaCarta();
        }

        public Carta DescartarUltimaCarta()
        {
            return MonteDeCartas.DescartarUltimaCarta();
        }

        public override string ToString()
        {
            if (MonteDeCartas == null)
                return $"Jogador: {Nome} com total de 0 cartas";

            return $"Jogador: {Nome} com total de {MonteDeCartas.MonteDeCartas.Count()} cartas";
        }

    }
}