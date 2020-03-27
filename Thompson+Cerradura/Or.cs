using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thompson_Cerradura
{
    class Or:Expresion
    {
        public Or(Expresion Exp1, Expresion Exp2, int IdExpresion, int N1, int N2, int N3, int N4, int N5, int N6)
        {
            this.Exp1 = Exp1;
            this.Exp2 = Exp2;
            this.N1 = N1;
            this.N2 = N2;
            this.N3 = N3;
            this.N4 = N4;
            this.N5 = N5;
            this.N6 = N6;
            this.Arista1 = "ε";
            this.Arista2 = "ε";
            this.Arista3 = "ε";
            this.Arista4 = "ε";
            this.TipoExpresion = Expresion.Tipo.OR;
            this.IdExpresion = IdExpresion;
            this.N_primero = "N1_"+this.IdExpresion;
            this.N_ultimo = "N4_" + this.IdExpresion; 
        }
        public Expresion Exp1 { get; set; }
        public Expresion Exp2 { get; set; }
        public int N1 { get; set; }
        public int N2 { get; set; }
        public int N3 { get; set; }
        public int N4 { get; set; }
        public int N5 { get; set; }
        public int N6 { get; set; }
        public string Arista1 { get; set; }
        public string Arista2 { get; set; }
        public string Arista3 { get; set; }
        public string Arista4 { get; set; }

        public override string ObtenerDot()
        {
            string cadena = this.N_primero + " [shape = circle label =" + this.N1 + " ];\n"
                + "N4_" + this.IdExpresion + " [shape = circle label =" + this.N4 + " ];\n";
            if(this.Exp1.TipoExpresion == Expresion.Tipo.TERMINAL)
            {

                cadena += "N2_" + this.IdExpresion + " [shape = circle label =" + this.N2 + " ];\n"
                + "N3_" + this.IdExpresion + " [shape = circle label =" + this.N3 + " ];\n";
                cadena +=this.N_primero + "->" + "N2_" + this.IdExpresion + " [label = \"" + this.Arista1 + "\" ];\n"
                +"N2_" + this.IdExpresion + "->" + "N3_" + this.IdExpresion + " [label = \"" + this.Exp1.ObtenerDot() + "\" ];\n"
                +"N3_" + this.IdExpresion + "->" + "N4_" + this.IdExpresion + " [label = \"" + this.Arista2 + "\" ];\n";             

            } else
            {

                cadena += this.Exp1.ObtenerDot();
                cadena += this.N_primero + "->" + this.Exp1.N_primero + " [label = \"" + this.Arista1 + "\" ];\n"
                + this.Exp1.N_ultimo + "->" + "N4_" + this.IdExpresion + " [label = \"" + this.Arista2 + "\" ];\n";

            }

            if (this.Exp2.TipoExpresion == Expresion.Tipo.TERMINAL)
            {

                cadena += "N5_" + this.IdExpresion + " [shape = circle label =" + this.N5 + " ];\n"
                + "N6_" + this.IdExpresion + " [shape = circle label =" + this.N6 + " ];\n"
                + this.N_primero + "->" + "N5_" + this.IdExpresion + " [label = \"" + this.Arista3 + "\" ];\n"
                + "N5_" + this.IdExpresion + "->" + "N6_" + this.IdExpresion + " [label = \"" + this.Exp2.ObtenerDot() + "\" ];\n"
                + "N6_" + this.IdExpresion + "->" + "N4_" + this.IdExpresion + " [label = \"" + this.Arista4 + "\" ];";

            }
            else
            {

                cadena += this.Exp2.ObtenerDot();
                cadena += this.N_primero + "->" + this.Exp2.N_primero + " [label = \"" + this.Arista1 + "\" ];\n"
                + this.Exp2.N_ultimo + "->" + "N4_" + this.IdExpresion + " [label = \"" + this.Arista2 + "\" ];\n";

            }

            return cadena;
        }
    }
}
