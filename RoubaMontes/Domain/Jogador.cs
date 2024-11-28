namespace RoubaMontes.Domain
{
    public class Jogador
    {
        public string Nome { get; private set; }
        public Queue<Carta> Monte { get; private set; }
        public int PosicaoNaUltimaPartida { get; private set; }
        public int TamanhoDoMonteNaUltimaPartida { get; private set; }

        public Jogador() { }

        public Jogador(string nome)
        {
            Nome = nome;
            Monte = new Queue<Carta>();
        }

        public void ComprarCarta(Carta carta)
        {
            TestarAutoNulidade();

            if (carta == null) throw new ArgumentNullException($"Carta não pode ser nula");
            Monte.Enqueue(carta);
        }

        private void TestarAutoNulidade()
        {
            if (string.IsNullOrEmpty(Nome)) throw new ArgumentNullException("Jogador instânciado, mas não iniciado: ");
        }
    }
}