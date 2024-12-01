using System;
using Xunit;
using RoubaMontes.Aid.Search.ArvoreAvl;
using RoubaMontes.Domain;

namespace RoubaMontes.Tests
{
    public class AvlThreeTests
    {
        [Fact]
        public void CriarAvlTree_CriaArvoreCorretamente()
        {
            // Arrange
            Carta carta = new Carta(1, '*');

            // Act
            AvlThree<Carta> arvore = new AvlThree<Carta>(carta);

            // Assert
            Assert.NotNull(arvore.Raiz);
            Assert.Equal(carta, arvore.Raiz.Raiz);
            Assert.Null(arvore.Raiz.NoEsq);
            Assert.Null(arvore.Raiz.NoDir);
        }

        [Fact]
        public void AdicionarNo_AdicionaNoCorretamente()
        {
            // Arrange
            Carta carta1 = new Carta(1, '*');
            Carta carta2 = new Carta(2, '*');
            Carta carta3 = new Carta(3, '*');
            AvlThree<Carta> arvore = new AvlThree<Carta>(carta1);

            // Act
            arvore.AdicionarNo(carta2);
            arvore.AdicionarNo(carta3);

            // Assert
            Assert.NotNull(arvore.Raiz.NoDir);
            Assert.Equal(carta2, arvore.Raiz.NoDir.Raiz);
            Assert.NotNull(arvore.Raiz.NoDir.NoDir);
            Assert.Equal(carta3, arvore.Raiz.NoDir.NoDir.Raiz);
        }

        [Fact]
        public void AdicionarNo_LancaExcecaoParaNoDuplicado()
        {
            // Arrange
            Carta carta1 = new Carta(1, '*');
            AvlThree<Carta> arvore = new AvlThree<Carta>(carta1);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => arvore.AdicionarNo(carta1));
        }
    }
}
