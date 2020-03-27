using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thompson_Cerradura
{
    class And: Expresion
    {
        public And(Expresion Exp1, Expresion Exp2, int IdExpresion, int N1, int N2, int N3)
        {
            this.Exp1 = Exp1;
            this.Exp2 = Exp2;
            this.N1 = N1;
            this.N2 = N2;
            this.N3 = N3;
            this.TipoExpresion = Expresion.Tipo.AND;
            this.IdExpresion = IdExpresion;
            this.N_primero = "N1_" + this.IdExpresion;
            this.N_ultimo = "N3_" + + this.IdExpresion;
        }

        public Expresion Exp1 { get; set; }
        public Expresion Exp2 { get; set; }
        public int N1 { get; set; }
        public int N2 { get; set; }
        public int N3 { get; set; }

        public override string ObtenerDot()
        {
            string cadena = ""; 
            if (this.Exp1.TipoExpresion == Expresion.Tipo.TERMINAL & this.Exp2.TipoExpresion ==  Expresion.Tipo.TERMINAL)
            {
                cadena += this.N_primero + " [shape = circle label =" + this.N1 + " ];\n"
                + "N2_" + this.IdExpresion + " [shape = circle label =" + this.N2 + " ];\n"
                + "N3_" + this.IdExpresion + " [shape = circle label =" + this.N3 + " ];\n"
                + this.N_primero+ "->" + "N2_" + this.IdExpresion + " [label = \"" + this.Exp1.ObtenerDot() + "\" ];\n"
                + "N2_" + this.IdExpresion + "->" + "N3_" + this.IdExpresion + " [label = \"" + this.Exp2.ObtenerDot() + "\" ];\n";

            }
            else if (this.Exp1.TipoExpresion != Expresion.Tipo.TERMINAL & this.Exp2.TipoExpresion == Expresion.Tipo.TERMINAL)
            {

                cadena += this.Exp1.ObtenerDot();
                this.N_primero = this.Exp1.N_primero;
                cadena += "N3_" + this.IdExpresion + " [shape = circle label =" + this.N3 + " ];\n"
                + this.Exp1.N_ultimo + "->" + "N3_" + this.IdExpresion + " [label = \"" + this.Exp2.ObtenerDot() + "\" ];\n";
                

            }else if (this.Exp1.TipoExpresion == Expresion.Tipo.TERMINAL & this.Exp2.TipoExpresion != Expresion.Tipo.TERMINAL)
            {
                cadena += this.N_primero + " [shape = circle label =" + this.N1 + " ];\n"
                + this.Exp2.ObtenerDot()
                + this.N_primero + "->" + this.Exp2.N_primero + " [label = \"" + this.Exp1.ObtenerDot() + "\" ];\n";
                this.N_ultimo = this.Exp2.N_ultimo;
            } else
            {
                cadena += this.Exp1.ObtenerDot();
                this.Exp2.N_primero = this.Exp1.N_ultimo;
                cadena += this.Exp2.ObtenerDot();
                this.N_ultimo = this.Exp2.N_ultimo;
            }
                return cadena;
        }
    }
}
