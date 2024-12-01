using NuGet.Frameworks;
using RoubaMontes.Domain;
using System.Reflection;

namespace RoubaMontes.Tests
{
    public class BaralhoTests
    {
        [Fact]
        public void teste_CriaInstanciaComNumeroCorretoDeCartas()
        {
            // Arrange
            int totalDeJogadores = 4;

            // Act
            Baralho baralho = new Baralho(totalDeJogadores);
            int totalDeCartas = baralho.Cartas.Length;
            int quantidadeDeCartasPorJogador = (int)typeof(Baralho)
                .GetField("_quantidadeDeCartasPorJogador", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(baralho);

            // Assert
            Assert.Equal(totalDeJogadores * quantidadeDeCartasPorJogador, totalDeCartas);
        }

        [Fact]
        public void teste_RetirarCarta()
        {
            // Arrange
            Baralho baralho = new Baralho(2);
            int ultimaCartaAntes = baralho.posicaoDaUltimaCarta;

            // Act
            var jogadorDaVez = new Jogador("teste");
            baralho.RetirarCarta(jogadorDaVez);

            // Assert
            Assert.NotEqual(baralho.Cartas[ultimaCartaAntes], baralho.Cartas[baralho.posicaoDaUltimaCarta]);
        }

        [Fact]
        public void teste_Embaralhar()
        {
            // Arrange
            Baralho original = new Baralho(2);
            var cartasAntes = original.Cartas.Clone() as Carta[];

            // Act
            MethodInfo embaralhar = typeof(Baralho).GetMethod("Embaralhar", BindingFlags.NonPublic | BindingFlags.Instance);
            embaralhar?.Invoke(original, null);

            // Assert
            Assert.False(cartasAntes.SequenceEqual(original.Cartas));
        }

        [Fact]
        public void teste_InstanciarBaralho()
        {
            // Arrange
            Baralho baralho = new Baralho(2);
            MethodInfo instanciarBaralho = typeof(Baralho).GetMethod("InstancirBaralho", BindingFlags.NonPublic | BindingFlags.Instance);

            // Act
            instanciarBaralho?.Invoke(baralho, new object[] { baralho.Cartas.Length });

            // Assert
            Assert.NotNull(baralho.Cartas);
            Assert.Equal(baralho.Cartas.Length, baralho.Cartas.Count());
        }

        [Fact]
        public void teste_ParseBaralhoParaString()
        {
            // Arrange
            Baralho baralho = new Baralho(2);
            MethodInfo parseParaString = typeof(Baralho).GetMethod("ParseBaralhoParaString", BindingFlags.NonPublic | BindingFlags.Instance);

            // Act
            string resultado = (string)parseParaString?.Invoke(baralho, null);

            // Assert
            Assert.NotEmpty(resultado);
            Assert.Contains(",", resultado);
        }

        [Fact]
        public void teste_FisherYatesShuffle()
        {
            // Arrange
            Baralho original = new Baralho(2);
            var cartasAntes = original.Cartas.Clone() as Carta[];

            // Act
            MethodInfo fisherYates = typeof(Baralho).GetMethod("Fisher_YatesShuffle", BindingFlags.NonPublic | BindingFlags.Instance);
            fisherYates?.Invoke(original, new object[] { original.Cartas.Length - 1 });

            // Assert
            Assert.False(cartasAntes.SequenceEqual(original.Cartas));
        }
    }
}
