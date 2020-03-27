using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thompson_Cerradura
{
    abstract class Expresion
    {
        public enum Tipo
        {
            OR,
            AND,
            KLEENE,
            TERMINAL
        }

        public int IdExpresion { get; set; }
        public Tipo TipoExpresion { get; set; }
        public int Contador { get; set; }
        public string N_primero { get; set; }
        public string N_ultimo { get; set; }
        public abstract string ObtenerDot();
    }

}
