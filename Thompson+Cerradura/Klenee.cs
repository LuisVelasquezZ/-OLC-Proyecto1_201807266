using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thompson_Cerradura
{
    class Klenee : Expresion
    {
        public Klenee(Expresion Exp1, int IdExpresion, int N1, int N2, int N3, int N4)
        {
            this.Exp1 = Exp1;
            this.N1 = N1;
            this.N2 = N2;
            this.N3 = N3;
            this.N4 = N4;
            this.Arista1 = "ε";
            this.Arista2 = "ε";
            this.Arista3 = "ε";
            this.Arista4 = "ε";
            this.TipoExpresion = Expresion.Tipo.KLEENE;
            this.IdExpresion = IdExpresion;
            this.N_primero = "N1_" + this.IdExpresion;
            this.N_ultimo = "N4_" + +this.IdExpresion;
        }

        public Expresion Exp1 { get; set; }
        public int N1 { get; set; }
        public int N2 { get; set; }
        public int N3 { get; set; }
        public int N4 { get; set; }
        public string Arista1 { get; set; }
        public string Arista2 { get; set; }
        public string Arista3 { get; set; }
        public string Arista4 { get; set; }

        public override string ObtenerDot()
        {
            string cadena = this.N_primero + " [shape = circle label =" + this.N1 + " ];\n"
                + "N4_" + this.IdExpresion + " [shape = circle label =" + this.N4 + " ];\n";
            if (this.Exp1.TipoExpresion == Expresion.Tipo.TERMINAL)
            {
                cadena += "N2_" + this.IdExpresion + " [shape = circle label =" + this.N2 + " ];\n"
                + "N3_" + this.IdExpresion + " [shape = circle label =" + this.N3 + " ];\n"
                + this.N_primero + "->" + "N2_" + this.IdExpresion + " [label = \"" + this.Arista1 + "\" ];\n"
                + "N2_" + this.IdExpresion + "->" + "N3_" + this.IdExpresion + " [label = \"" + this.Exp1.ObtenerDot() + "\" ];\n"
                + "N3_" + this.IdExpresion + "->" + "N4_" + this.IdExpresion + " [label = \"" + this.Arista2 + "\" ];\n"
                + "N3_" + this.IdExpresion + "->" + "N2_" + this.IdExpresion + " [label = \"" + this.Arista3 + "\" ];\n";

            }
            else
            {

                cadena += this.Exp1.ObtenerDot();
                cadena += this.N_primero + "->" + this.Exp1.N_primero + " [label = \"" + this.Arista1 + "\" ];\n"
                + this.Exp1.N_ultimo + "->" + "N4_" + this.IdExpresion + " [label = \"" + this.Arista2 + "\" ];\n"
                + this.Exp1.N_ultimo+ "->" + this.Exp1.N_primero + " [label = \"" + this.Arista3 + "\" ];\n";

            }
            cadena +="N4_" + this.IdExpresion + "->" + this.N_primero + " [label = \"" + this.Arista4 + "\" ];\n";

            return cadena;
        }
    }

    
}
