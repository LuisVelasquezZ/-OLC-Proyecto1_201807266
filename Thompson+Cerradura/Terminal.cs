using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thompson_Cerradura
{
    class Terminal: Expresion
    {
        public Terminal(string Arista, int IdExpresion)
        {
            this.Arista = Arista;
            this.IdExpresion = IdExpresion;
            this.TipoExpresion = Expresion.Tipo.TERMINAL;
        }
        public string Arista { get; set; }

        public override string ObtenerDot()
        {
            string cadena = this.Arista;
            return cadena;
        }

    }
}
