using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoubaMontes.Aid.Search.ArvoreAvl
{
    public class Node<T> where T : class, IComparable<T>
    {
        public T Raiz {  get; private set; }

        public Node<T>? NoEsq { get; set; }

        public Node<T>? NoDir { get; set; }

        public Node (T no)
        {
            Raiz = no;
            NoEsq = null;
            NoDir = null;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;

            var node = obj as Node<T>;
            if (node == null) return false;

            return this.Raiz.Equals(node.Raiz);
        }

        public int CompareTo(T other) {

            if (other == null) return 1;

            return Raiz.CompareTo(other);
        }

    }
}
