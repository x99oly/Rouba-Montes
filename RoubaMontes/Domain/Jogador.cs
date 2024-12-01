namespace RoubaMontes.Domain
{
    public class Jogador
    {
        public string Nome { get; private set; }
        public Monte? MonteDeCartas { get; private set; }
        public int PosicaoNaUltimaPartida { get; private set; }
        public int TamanhoDoMonteNaUltimaPartida { get; private set; }

        public Jogador(string nome)
        {
            Nome = nome;
        }

        public void ComprarCarta(Carta carta)
        {
            if (MonteDeCartas == null) MonteDeCartas = new Monte(carta);

            else MonteDeCartas.AdicionarCarta(carta);
        }
        // Talvez seja melhor trocar o dicionário ----- Tuplas???
        public bool SelecionarMonte(Dictionary<Carta, Monte> montes, Carta cartaDaVez)
        {
            if (montes.TryGetValue(cartaDaVez, out Monte? novoMonte))
            {
                novoMonte.VincularJogador(this);

                if (MonteDeCartas == null)
                {
                    MonteDeCartas = novoMonte;
                }
                else
                {
                    while (novoMonte.MonteDeCartas.Count > 0)
                    {
                        MonteDeCartas.AdicionarMonte(novoMonte);
                    }
                }

                montes.Remove(cartaDaVez);
                ComprarCarta(cartaDaVez);
                montes.Add(UltimaCarta(), MonteDeCartas);
                return true;
            }
            return false;
        }

        public Carta? UltimaCarta()
        {
            if (MonteDeCartas == null) return null;

            try
            {
                return MonteDeCartas.UltimaCarta();
            }
            catch (ArgumentNullException e)
            {
                throw new ArgumentNullException(e.Message);
            }
        }

        public Carta? DescartarUltimaCarta()
        {
            if (MonteDeCartas == null) return null;

            try
            {
                return MonteDeCartas.DescartarUltimaCarta();
            }
            catch (ArgumentNullException e)
            {
                throw new ArgumentNullException(e.Message);
            }
        }

        public override string ToString()
        {
            if (MonteDeCartas == null)
                return $"Jogador: {Nome} com total de 0 cartas";

            return $"Jogador: {Nome} com total de {MonteDeCartas.MonteDeCartas.Count()} cartas";
        }

    }
}