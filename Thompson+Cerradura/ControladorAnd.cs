using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thompson_Cerradura
{
    class ControladorAnd
    {
        private static ControladorAnd instancia;
        public ControladorAnd()
        {

        }
        public static ControladorAnd getInstancia()
        {
            if(instancia == null)
            {
                instancia = new ControladorAnd();
            }
            return instancia;
        }

        public And generarAnd(int primero, int idExpresion, Expresion exp1, Expresion exp2)
        {
            And nuevo = new And(exp1, exp2, idExpresion, primero, primero + 1, primero + 2);
            return nuevo;
        }
    }
}
