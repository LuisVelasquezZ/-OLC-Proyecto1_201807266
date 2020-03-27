using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thompson_Cerradura
{
    class ER
    {
        public ER(int IdER, LinkedList<Token> ListaTokens)
        {
            this.IdER = IdER;
            this.ListaTokens = ListaTokens;
        }

        public int IdER { get; set; }
        public LinkedList<Token> ListaTokens { get; set; }
    }
}
