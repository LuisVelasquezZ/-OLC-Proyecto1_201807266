using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thompson_Cerradura
{
    class Thompson
    {
        private static Thompson instancia;
        LinkedList<Token> ers = new LinkedList<Token>();
        LinkedList<String> conjuntos = new LinkedList<string>();
        LinkedList<String> lexemas = new LinkedList<string>();
        LinkedList<ER> expresionesr = new LinkedList<ER>();
        LinkedList<Expresion> expres = new LinkedList<Expresion>();
        int idExpresion = 0;
        int idExpresionRegular = 1;
        int idNodo = 0;
        public Thompson()
        {

        }
        public static Thompson getInstancia()
        {
            if(instancia == null)
            {
                instancia = new Thompson();
            }
            return instancia;
        }

        public void clasificador(LinkedList<Token> tokens)
        {
            
            bool dpts = false;
            bool id = false;
            bool asig = false;
            bool pyc = false;
            bool lleno = false;
            foreach(Token t in tokens)
            {
                if (t.NombreToken == Token.Nombre.Conjunto)
                {
                    conjuntos.AddLast(t.Valor);
                } 
            }

            foreach (Token t in tokens)
            {
                if (t.NombreToken == Token.Nombre.Dos_Puntos)
                {
                    dpts = true;

                } else if (t.NombreToken == Token.Nombre.Identificador)
                {
                    id = true;
                }
                else if (t.NombreToken == Token.Nombre.Asignador)
                {
                    asig = true;
                }
                else if (t.NombreToken == Token.Nombre.Punto_Y_Coma)
                {
                    pyc = false;
                    dpts = false;
                    id = false;
                    asig = false;
                    if (lleno)
                    {
                        expresionesr.AddLast(new ER(idExpresionRegular++, ers));
                        ers.Clear();
                        lleno = false;
                        dpts = false;
                        id = false;
                        pyc = false;
                        asig = false;
                    }
                }
                else if (id & asig & !pyc & !dpts)
                {
                    ers.AddLast(t);
                    lleno = true;
                }
            }

            foreach (Token t in tokens)
            {
                if (t.NombreToken == Token.Nombre.Dos_Puntos)
                {
                    dpts = true;
                }
                else if (t.NombreToken == Token.Nombre.Identificador)
                {
                    id = true;
                }
                
                else if (t.NombreToken == Token.Nombre.Punto_Y_Coma)
                {
                    dpts = false;
                    id = false;
                }
                else if (id & dpts & t.NombreToken == Token.Nombre.Cadena)
                {
                    lexemas.AddLast(t.Valor);
                    id = false;
                    dpts = false;
                    pyc = false;
                }
            }

        }

        public void generarExpresiones(LinkedList<Token> tokens)
        {
            clasificador(tokens);
            int indice = 0;
            Expresion argu1 = null;
            Expresion argu2 = null;
            foreach(Token tok in ers)
            {
                Console.WriteLine("Expresion Regular: " + tok.Valor);
            }
            foreach(ER exp in expresionesr)
            {
                for(int i = exp.ListaTokens.Count; i > 0; i--)
                {
                    Token t = exp.ListaTokens.ElementAt(i);
                    if(t.NombreToken == Token.Nombre.Cadena & exp.ListaTokens.ElementAt(i - 1).NombreToken == Token.Nombre.Cadena & exp.ListaTokens.ElementAt(i - 2).NombreToken != Token.Nombre.Cadena)
                    {

                    }
                    if(t.NombreToken == Token.Nombre.Cadena)
                    {
                        if( argu1 == null)
                        {
                            if(exp.ListaTokens.ElementAt(i-1).NombreToken != Token.Nombre.Cadena)
                            {
                                if(exp.ListaTokens.ElementAt(i - 1).NombreToken != Token.Nombre.Asterisco)
                                {
                                    argu1 = new Klenee(new Terminal(t.Valor.Trim('"'), idExpresion++), idExpresion++, idNodo+ 1 , idNodo+2, idNodo+3, idNodo+4);
                                    idNodo = idNodo + 4;
                                } else if(exp.ListaTokens.ElementAt(i - 1).NombreToken != Token.Nombre.Signo_Cierre_Interrogacion)
                                {
                                    argu1 = new Or(new Terminal(t.Valor.Trim('"'), idExpresion++), new Terminal("ε", idExpresion++), idExpresion++, idNodo + 1, idNodo + 2, idNodo + 3, idNodo + 4, idNodo + 5, idNodo + 6);
                                    idNodo = idNodo + 6;
                                } else if(exp.ListaTokens.ElementAt(i - 1).NombreToken != Token.Nombre.Mas)
                                {
                                    Expresion cerr = new Klenee(new Terminal(t.Valor.Trim('"'), idExpresion++), idExpresion++, idNodo + 1, idNodo + 2, idNodo + 3, idNodo + 4);
                                    idNodo = idNodo + 4;
                                    argu1 = new And(cerr, new Terminal(t.Valor.Trim('"'), idExpresion++), idExpresion++, idNodo + 1, idNodo + 2, idNodo + 3);
                                    idNodo = idNodo + 3;
                                }
                            } else
                            {
                                argu1 = new Terminal(t.Valor.Trim('"'), idExpresion++);
                            }
                        } else if(argu2 == null)
                        {
                            argu2 = new Terminal(t.Valor.Trim('"'), idExpresion++);
                        }
                    } else if(t.NombreToken == Token.Nombre.Punto)
                    {

                    }
                }
            }
            
        }

        public void generarGrafos()
        {
            Terminal exp1 = new Terminal("A", idExpresion++);
            Terminal exp2 = new Terminal("B", idExpresion++);
            Or exp3 = ControladorOr.getInstancia().generarOr(4, idExpresion++, exp1, exp2);
            Terminal exp4 = new Terminal("z", idExpresion++);
            Terminal exp5 = new Terminal("y", idExpresion++);
            Or exp6 = ControladorOr.getInstancia().generarOr(exp3.N6 + 1, idExpresion++, exp4, exp5);
            Or exp7 = ControladorOr.getInstancia().generarOr(exp6.N6+1, idExpresion++, exp3, exp6);
            Terminal exp9 = new Terminal("1", idExpresion++);
            Terminal exp10 = new Terminal("2", idExpresion++);
            Or exp11 = ControladorOr.getInstancia().generarOr(exp7.N6 + 1, idExpresion++, exp9, exp10);
            And exp8 = new And(exp1, exp2, idExpresion++, 1, 2, 3);
            And exp12 = ControladorAnd.getInstancia().generarAnd(exp11.N6+1, idExpresion++, exp4, exp5);
            And exp13 = ControladorAnd.getInstancia().generarAnd(exp12.N3+ 1, idExpresion++, exp8, exp12);
            Klenee exp14 = new Klenee(exp1, idExpresion++, 1, 2, 3, 4);
            Klenee exp15 = new Klenee(exp7, idExpresion++, 5,6,7,8);
            string cadena = "digraph G{\n"
                + "rankdir=LR;\n"
                + exp15.ObtenerDot()
                +"\n}";

            if (File.Exists(@"imagen.txt"))
            {
                File.Delete(@"imagen.txt");
            }
            if (File.Exists(@"imagen.png"))
            {
                File.Delete(@"imagen.png");
            }
            String rdot = @"imagen.txt";
            String rpng = @"imagen.png";
            System.IO.File.WriteAllText(rdot, cadena);
            String comandoDot = "dot -Tpng " + "imagen.txt " + "-o " + "imagen.png";
            var comando = string.Format(comandoDot);
            /*ProcessStartInfo procStart = new ProcessStartInfo("cmd",  comandoDot);
            Process proc = new Process();
            proc.StartInfo = procStart;
            proc.Start();
            /*var proc = new System.Diagnostics.Process();
            proc.StartInfo = procStart;
            proc.Start();
            proc.WaitForExit();*/
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = false;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            cmd.StandardInput.WriteLine(comandoDot);
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
        }

    }
}
