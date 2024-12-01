using System;

namespace RoubaMontes.Aid.Search.ArvoreAvl
{
    public class AvlThree<T> where T : class, IComparable<T>
    {
        public Node<T>? Raiz { get; private set; }
        public int DirecaoDoBalanceamento { get; private set; } = 1;

        public AvlThree(T no)
        {
            Raiz = new Node<T>(no);
        }

        public void AdicionarNo(T novoNo)
        {
            Raiz = AdicionarNoRecursivo(Raiz, novoNo);
        }

        private Node<T> AdicionarNoRecursivo(Node<T>? root, T novoNo)
        {
            if (root == null) return new Node<T>(novoNo);

            int res = Direcao(root, novoNo);

            if (res == 0) throw new ArgumentException("Nó já existe na árvore.");

            if (res == 1)
            {
                root.NoDir = AdicionarNoRecursivo(root.NoDir, novoNo);
            }
            else
            {
                root.NoEsq = AdicionarNoRecursivo(root.NoEsq, novoNo);
            }

            // Aqui você pode implementar a lógica de balanceamento se necessário
            return root;
        }

        private void RotacionarEsquerda(Node<T> raiz)
        {
            if (raiz.NoDir == null) return;

            Node<T> no = raiz.NoDir;
            raiz.NoDir = no.NoEsq;
            no.NoEsq = raiz;

            // Atualizar a raiz da árvore após a rotação
            if (raiz == Raiz)
            {
                Raiz = no;
            }
        }

        public void DefinirDirecaoDOBalanceamento(int parameter)
        {
            if (parameter == -1 || parameter == 1)
            {
                DirecaoDoBalanceamento = parameter;
            }
            else
            {
                throw new ArgumentException("Direção do balanceamento deve ser -1 ou 1.");
            }
        }

        private int Direcao(Node<T> raiz, T novoNo)
        {
            int res = raiz.CompareTo(novoNo);
            return res * DirecaoDoBalanceamento;
        }
    }
}
