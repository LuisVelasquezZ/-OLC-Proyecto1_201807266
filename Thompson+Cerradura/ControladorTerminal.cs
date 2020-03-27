using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thompson_Cerradura
{
    class ControladorTerminal
    {
        private static ControladorTerminal instancia;
        public ControladorTerminal()
        {

        }

        public static ControladorTerminal getInstancia()
        {
            if(instancia == null)
            {
                instancia = new ControladorTerminal();
            }
            return instancia;
        }

        public Terminal generarTerm(string arista, int idExpresion)
        {
            Terminal nuevo = new Terminal(arista, idExpresion);
            return nuevo;
        }
    }
}
