using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thompson_Cerradura
{
    class ControladorOr
    {
        private static ControladorOr instancia;
        public ControladorOr() { }
        public static ControladorOr getInstancia()
        {
            if(instancia == null)
            {
                instancia = new ControladorOr();
            }
            return instancia;
        }


        public Or generarOr(int primero, int idExpresion, Expresion exp1, Expresion exp2) {
            Or nuevo = new Or(exp1,exp2,idExpresion,primero, primero+1, primero+2, primero+3, primero+4,primero+5);
                return nuevo;
        }
    }
}
