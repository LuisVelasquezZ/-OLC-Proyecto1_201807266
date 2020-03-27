using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thompson_Cerradura
{
    class Lexico
    {
        private int idTokenTabla = 1;
        private int idToken = 1;
        private int idError = 1;
        private LinkedList<Token> tablaTokens = new LinkedList<Token>();
        private LinkedList<Token> listaErrores = new LinkedList<Token>();
        private LinkedList<Token> listaTokens = new LinkedList<Token>();
        private int estado = 0;
        private int fila = 1;
        private int columna = 0;
        private string aux = "";
        private static Lexico instancia;
        public static Lexico getInstancia()
        {
            if(instancia == null)
            {
                instancia = new Lexico();
            }
            return instancia;
        }

        public Lexico()
        {
            tablaTokens.Clear();
            idTokenTabla = 1;
            tablaTokens.AddLast(new Token(idTokenTabla, Token.Nombre.Asignador, "->", 0, 0));
            idTokenTabla++;
            tablaTokens.AddLast(new Token(idTokenTabla, Token.Nombre.Asterisco, "*", 0, 0));
            idTokenTabla++;
            tablaTokens.AddLast(new Token(idTokenTabla, Token.Nombre.Cadena, "", 0, 0));
            idTokenTabla++;
            tablaTokens.AddLast(new Token(idTokenTabla, Token.Nombre.Coma, ",", 0, 0));
            idTokenTabla++;
            tablaTokens.AddLast(new Token(idTokenTabla, Token.Nombre.Comentario, "", 0, 0));
            idTokenTabla++;
            tablaTokens.AddLast(new Token(idTokenTabla, Token.Nombre.Comentario_Multilinea, "", 0, 0));
            idTokenTabla++;
            tablaTokens.AddLast(new Token(idTokenTabla, Token.Nombre.Comilla_Simple, "", 0, 0));
            idTokenTabla++;
            tablaTokens.AddLast(new Token(idTokenTabla, Token.Nombre.Conjunto, "", 0, 0));
            idTokenTabla++;
            tablaTokens.AddLast(new Token(idTokenTabla, Token.Nombre.Dos_Puntos, ":", 0, 0));
            idTokenTabla++;
            tablaTokens.AddLast(new Token(idTokenTabla, Token.Nombre.Identificador, "", 0, 0));
            idTokenTabla++;
            tablaTokens.AddLast(new Token(idTokenTabla, Token.Nombre.LLave_DER, "}", 0, 0));
            idTokenTabla++;
            tablaTokens.AddLast(new Token(idTokenTabla, Token.Nombre.LLave_IZQ, "{", 0, 0));
            idTokenTabla++;
            tablaTokens.AddLast(new Token(idTokenTabla, Token.Nombre.Mas, "+", 0, 0));
            idTokenTabla++;            
            tablaTokens.AddLast(new Token(idTokenTabla, Token.Nombre.Or, "|", 0, 0));
            idTokenTabla++;
            tablaTokens.AddLast(new Token(idTokenTabla, Token.Nombre.Punto, ".", 0, 0));
            idTokenTabla++;
            tablaTokens.AddLast(new Token(idTokenTabla, Token.Nombre.Punto_Y_Coma, ";", 0, 0));
            idTokenTabla++;
            tablaTokens.AddLast(new Token(idTokenTabla, Token.Nombre.Reservada_CONJ, "CONJ", 0, 0));
            idTokenTabla++;
            tablaTokens.AddLast(new Token(idTokenTabla, Token.Nombre.Salto_de_Linea, "", 0, 0));
            idTokenTabla++;
            tablaTokens.AddLast(new Token(idTokenTabla, Token.Nombre.Signo_Cierre_Interrogacion, "?", 0, 0));
            idTokenTabla++;
            tablaTokens.AddLast(new Token(idTokenTabla, Token.Nombre.Tabulacion, "", 0, 0));
            idTokenTabla++;
            tablaTokens.AddLast(new Token(idTokenTabla, Token.Nombre.Todo, "", 0, 0));
            idTokenTabla++;
        }


        public LinkedList<Token> GetTokens(String entrada)
        {
            analizar(entrada);
            return listaTokens;
        }

        public LinkedList<Token> GetErrores()
        {
            return listaErrores;
        }

        public void analizar(String entrada)
        {
            listaErrores.Clear();
            listaTokens.Clear();
            idToken = 0;
            idError = 0;
            entrada += "#";
            estado = 0;
            char c;
            for(int i = 0; i < entrada.Length; i++)
            {
                columna++;
                c = entrada.ElementAt(i);
                switch (estado)
                {
                    //Estado inicial
                    case 0:
                        bool encontrado = false;
                        //Busca  c en lista de tokens
                        foreach (Token t in tablaTokens)
                        {
                            if (t.Valor.Length == 1)
                            {
                                if (c == t.Valor.ElementAt(0))
                                {
                                    listaTokens.AddLast(new Token(idToken, t.NombreToken, t.Valor, fila, columna));
                                    idToken++;
                                    estado = 0;
                                    encontrado = true;
                                    break;
                                }
                            }
                        }
                        //El  c no es ningun token reconocido
                        if (!encontrado)
                        {
                            //El  c es "/"
                            if (c == '/')
                            {
                                aux += c;
                                estado = 1;
                            }
                            else if (c == '<')
                            {
                                aux += c;
                                estado = 3;
                            }
                            else if (c == 'C')
                            {
                                if (entrada.ElementAt(i + 1) == 'O' && entrada.ElementAt(i + 2) == 'N' && entrada.ElementAt(i + 3) == 'J')
                                {
                                    listaTokens.AddLast(new Token(idToken, Token.Nombre.Reservada_CONJ, "CONJ", fila, columna));
                                    idToken++;
                                    i = i + 3;
                                    estado = 0;
                                }
                                else
                                {
                                    aux += c;
                                    estado = 5;
                                }
                            }
                            else if (Char.IsLetter(c))
                            {
                                aux += c;
                                estado = 5;
                            }
                            else if (c == '-')
                            {
                                if (entrada.ElementAt(i + 1) == '>')
                                {
                                    listaTokens.AddLast(new Token(idToken, Token.Nombre.Asignador, "->", fila, columna));
                                    idToken++;
                                    estado = 0;
                                    i = i + 1;
                                    aux = "";
                                }
                                else
                                {
                                    aux += c;
                                    estado = 6;
                                }

                            }
                            else if (c == '"')
                            {
                                aux += c;
                                estado = 9;
                            }
                            else if (c == '[')
                            {
                                aux += c;
                                if (entrada.ElementAt(i + 1) == ':')
                                {
                                    aux += entrada.ElementAt(i + 1);
                                    i++;
                                    estado = 10;
                                }
                                else
                                {
                                    estado = 6;
                                }

                            }
                            else if (Char.IsWhiteSpace(c))
                            {

                            }
                            else if (c == '\\') {
                                if(entrada.ElementAt(i+ 1) == 'n')
                                {
                                    aux = "\\n";
                                    if (i+2 < entrada.Length )
                                    {
                                        if(entrada.ElementAt(i + 2) == ',')
                                        {
                                            aux += ",";
                                            i = i + 2;
                                            estado = 8;
                                        } else
                                        {
                                            listaTokens.AddLast(new Token(idToken, Token.Nombre.Salto_de_Linea, "\\n", fila, columna));
                                            idToken++;
                                            estado = 0;
                                            i = i + 1;
                                            aux = "";
                                        }
                                    } else
                                    {
                                        listaTokens.AddLast(new Token(idToken, Token.Nombre.Salto_de_Linea, "\\n", fila, columna));
                                        idToken++;
                                        estado = 0;
                                        i = i + 1;
                                        aux = "";
                                    }
                                    
                                    

                                }
                                else if (entrada.ElementAt(i + 1) == '\'')
                                {
                                    aux = "\\'";
                                    if (i + 2 < entrada.Length)
                                    {
                                        if (entrada.ElementAt(i + 2) == ',')
                                        {
                                            aux += ",";
                                            i = i + 2;
                                            estado = 8;
                                        }
                                        else
                                        {
                                            listaTokens.AddLast(new Token(idToken, Token.Nombre.Comilla_Simple, "\\'", fila, columna));
                                            idToken++;
                                            estado = 0;
                                            i = i + 1;
                                            aux = "";
                                        }
                                    }
                                    else
                                    {
                                        listaTokens.AddLast(new Token(idToken, Token.Nombre.Comilla_Simple, "\\'", fila, columna));
                                        idToken++;
                                        estado = 0;
                                        i = i + 1;
                                        aux = "";
                                    }
                                    

                                }
                                else if (entrada.ElementAt(i + 1)  == '"')
                                {
                                    aux = "\\\"";
                                    if (i + 2 < entrada.Length)
                                    {
                                        if (entrada.ElementAt(i + 2) == ',')
                                        {
                                            aux += ",";
                                            i = i + 2;
                                            estado = 8;
                                        }
                                        else
                                        {
                                            listaTokens.AddLast(new Token(idToken, Token.Nombre.Comilla_Doble, "\\\"", fila, columna));
                                            idToken++;
                                            estado = 0;
                                            i = i + 1;
                                            aux = "";
                                        }
                                    }
                                    else
                                    {
                                        listaTokens.AddLast(new Token(idToken, Token.Nombre.Comilla_Doble, "\\\"", fila, columna));
                                        idToken++;
                                        estado = 0;
                                        i = i + 1;
                                        aux = "";
                                    }
                                    

                                }
                                else if (entrada.ElementAt(i + 1) == 't')
                                {
                                    aux = "\\t";
                                    if (i + 2 < entrada.Length)
                                    {
                                        if (entrada.ElementAt(i + 2) == ',')
                                        {
                                            aux += ",";
                                            i = i + 2;
                                            estado = 8;
                                        }
                                        else
                                        {
                                            listaTokens.AddLast(new Token(idToken, Token.Nombre.Tabulacion, "\\t", fila, columna));
                                            idToken++;
                                            estado = 0;
                                            i = i + 1;
                                            aux = "";
                                        }
                                    }
                                    else
                                    {
                                        listaTokens.AddLast(new Token(idToken, Token.Nombre.Tabulacion, "\\t", fila, columna));
                                        idToken++;
                                        estado = 0;
                                        i = i + 1;
                                        aux = "";
                                    }
                                    

                                } else
                                {
                                    aux += c;
                                    estado = 6;
                                }
                            }
                            else if (Char.IsControl(c))
                            {
                                if (c == '\n')
                                {
                                    fila++;
                                    columna = 0;
                                }
                            }
                            else if ((int)c >= 33 & (int)c <= 125)
                            {
                                aux += c;
                                estado = 6;
                            }
                            else if (c == '#')
                            {
                                if (i == entrada.Length - 1)
                                {
                                    Console.WriteLine("Analisis Finalizado");
                                }
                            }
                            else
                            {
                                aux += c;
                                listaErrores.AddLast(new Token(idError, Token.Nombre.Error, aux, fila, columna));
                                idError++;
                                estado = 0;
                                aux = "";
                            }
                        }
                        break;
                    // El c anterior es "/"
                    case 1:
                        //EL c es "/"
                        if (c == '/')
                        {
                            aux += c;
                            estado = 2;
                        }
                        else if (c == '#') {
                            if (i == entrada.Length - 1)
                            {
                                Console.WriteLine("Analisis Finalizado");
                            }
                            else
                            {
                                aux += c;
                            }
                            listaErrores.AddLast(new Token(idError, Token.Nombre.Error, aux, fila, columna));
                            idError++;
                            estado = 0;
                            aux = "";
                        }
                        else
                        {
                            aux += c;
                            estado = 6;
                            i--;
                        }
                        break;
                    case 2:
                        if (c != '\n')
                        {
                            aux += c;
                            estado = 2;
                        } else
                        {
                            aux += c;
                            listaTokens.AddLast(new Token(idToken, Token.Nombre.Comentario, aux, fila, columna));
                            idToken++;
                            fila++;
                            columna = 0;
                            aux = "";
                            estado = 0;
                        }
                        break;
                    case 3:
                        if (c == '!')
                        {
                            aux += c;
                            estado = 4;
                        }
                        else if (c == '#')
                        {
                            if (i == entrada.Length - 1)
                            {
                                Console.WriteLine("Analisis Finalizado");
                            } else
                            {
                                aux += c;
                            }
                            listaErrores.AddLast(new Token(idError, Token.Nombre.Error, aux, fila, columna));
                            idError++;
                            estado = 0;
                            aux = "";
                        }
                        else
                        {
                            aux += c;
                            estado = 6;
                            i--;
                        }
                        break;
                    case 4:
                        string segmento = entrada.Substring(i);
                        bool cierre = false;
                        for (int j = 0; j < segmento.Length; j++)
                        {
                            char caracter = segmento.ElementAt(j);
                            if (caracter == '!')
                            {
                                if (segmento.ElementAt(j + 1) == '>')
                                {
                                    cierre = true;
                                    break;
                                }
                            }
                        }
                        if (cierre)
                        {
                            if (c != '!')
                            {
                                aux += c;
                                estado = 4;
                                if (c == '\n')
                                {
                                    fila++;
                                    columna = 0;
                                }
                            } else
                            {
                                aux += c;
                                aux += entrada.ElementAt(i + 1);
                                listaTokens.AddLast(new Token(idToken, Token.Nombre.Comentario_Multilinea, aux, fila, columna));
                                idToken++;
                                estado = 0;
                                aux = "";
                                i++;
                            }
                        } else
                        {
                            if (c == '\n') {
                                aux += c;
                                listaErrores.AddLast(new Token(idError, Token.Nombre.Error, aux, fila, columna));
                                idError++;
                                estado = 0;
                                aux = "";
                                fila++;
                                columna = 0;
                            }
                            else if (c == '#')
                            {
                                if (i == entrada.Length - 1)
                                {
                                    Console.WriteLine("Analisis Finalizado");
                                }
                                else
                                {
                                    aux += c;
                                }
                                listaErrores.AddLast(new Token(idError, Token.Nombre.Error, aux, fila, columna));
                                idError++;
                                estado = 0;
                                aux = "";
                            }
                            else {
                                aux += c;
                                estado = 4;
                            }
                        }
                        break;
                    case 5:
                        if (Char.IsDigit(c) | Char.IsLetter(c))
                        {
                            aux += c;
                            estado = 5;
                        } else if (c == ' ')
                        {
                            aux += c;
                            listaTokens.AddLast(new Token(idToken, Token.Nombre.Identificador, aux, fila, columna));
                            idToken++;
                            estado = 0;
                            aux = "";
                        }
                        else if (c == '#')
                        {
                            if (i == entrada.Length - 1)
                            {
                                listaTokens.AddLast(new Token(idToken, Token.Nombre.Identificador, aux, fila, columna));
                                idToken++;
                                estado = 0;
                                aux = "";
                                Console.WriteLine("Analisis Finalizado");
                            }
                            else
                            {
                                listaTokens.AddLast(new Token(idToken, Token.Nombre.Identificador, aux, fila, columna));
                                idToken++;
                                estado = 0;
                                aux = "";
                                i--;
                            }
                        } else
                        {
                            if (c == '~')
                            {
                                aux += c;
                                estado = 7;
                            }
                            else if (c == ',')
                            {
                                aux += c;
                                estado = 8;
                            } else
                            {
                                listaTokens.AddLast(new Token(idToken, Token.Nombre.Identificador, aux, fila, columna));
                                idToken++;
                                estado = 0;
                                aux = "";
                                i--;
                            }
                        }
                        break;
                    case 6:
                        if (c == '~')
                        {
                            aux += c;
                            estado = 7;
                        } else if (c == ',')
                        {
                            aux += c;
                            estado = 8;
                        }
                        else if (c == '#')
                        {
                            if (i == entrada.Length - 1)
                            {
                                listaErrores.AddLast(new Token(idError, Token.Nombre.Error, aux, fila, columna));
                                idError++;
                                estado = 0;
                                aux = "";
                                Console.WriteLine("Analisis Finalizado");
                            }
                            else
                            {
                                listaErrores.AddLast(new Token(idError, Token.Nombre.Error, aux, fila, columna));
                                idToken++;
                                estado = 0;
                                aux = "";
                            }
                        } else
                        {
                            aux += c;
                            listaErrores.AddLast(new Token(idError, Token.Nombre.Error, aux, fila, columna));
                            idToken++;
                            estado = 0;
                            aux = "";
                        }
                        break;
                    case 7:
                        if ((int)c >= 33 & (int)c <= 125)
                        {
                            aux += c;
                            listaTokens.AddLast(new Token(idToken, Token.Nombre.Conjunto, aux, fila, columna));
                            idToken++;
                            estado = 0;
                            aux = "";
                        } else
                        {
                            listaErrores.AddLast(new Token(idError, Token.Nombre.Error, aux, fila, columna));
                            idError++;
                            estado = 0;
                            aux = "";
                            i--;
                        }
                        break;
                    case 8:
                        if (c == '\\'){
                            bool especial = false;
                            if (entrada.ElementAt(i + 1) == 'n')
                            {
                                aux += "\\n";
                                i = i + 1;
                                especial = true;
                            }
                            else if (entrada.ElementAt(i + 1) == '\'')
                            {
                                aux += "\\'";
                                i = i + 1;
                                especial = true;
                            }
                            else if (entrada.ElementAt(i + 1) == '"')
                            {
                                aux += "\\\"";
                                i = i + 1;
                                especial = true;
                            }
                            else if (entrada.ElementAt(i + 1) == 't')
                            {
                                aux += "\\t";
                                i = i + 1;
                                especial = true;
                            } else
                            {
                                if (entrada.ElementAt(i + 1) == ',')
                                {

                                    estado = 6;
                                }
                                else
                                {
                                    listaTokens.AddLast(new Token(idToken, Token.Nombre.Conjunto, aux, fila, columna));
                                    idToken++;
                                    estado = 0;
                                    aux = "";
                                }
                            }
                            if(especial)
                            {
                                if (entrada.ElementAt(i + 1) == ',')
                                {
                                    estado = 6;
                                }
                                else
                                {
                                    listaTokens.AddLast(new Token(idToken, Token.Nombre.Conjunto, aux, fila, columna));
                                    idToken++;
                                    estado = 0;
                                    aux = "";
                                }
                            }
                        }
                        else if ((int)c >= 33 & (int)c <= 125)
                        {
                            aux += c;
                            if (entrada.ElementAt(i + 1) == ',')
                            {
                                estado = 6;
                            }
                            else
                            {
                                listaTokens.AddLast(new Token(idToken, Token.Nombre.Conjunto, aux, fila, columna));
                                idToken++;
                                estado = 0;
                                aux = "";
                            }
                        }
                        else
                        {
                            listaErrores.AddLast(new Token(idError, Token.Nombre.Error, aux, fila, columna));
                            idError++;
                            estado = 0;
                            aux = "";
                            i--;
                        }
                        break;
                    case 9:
                        string segmento2 = entrada.Substring(i);
                        bool cierre2 = false;
                        for (int j = 0; j < segmento2.Length; j++)
                        {
                            char caracter = segmento2.ElementAt(j);
                            if (caracter == '"')
                            {
                                cierre2 = true;
                                break;
                            }
                        }
                        if (cierre2)
                        {
                            if (c != '"')
                            {
                                aux += c;
                                estado = 9;
                                if (c == '\n')
                                {
                                    fila++;
                                    columna = 0;
                                }
                            }
                            else
                            {
                                aux += c;
                                listaTokens.AddLast(new Token(idToken, Token.Nombre.Cadena, aux, fila, columna));
                                idToken++;
                                estado = 0;
                                aux = "";
                            }
                        }
                        else
                        {
                            if (c == '\n')
                            {
                                aux += c;
                                listaErrores.AddLast(new Token(idError, Token.Nombre.Error, aux, fila, columna));
                                idError++;
                                estado = 0;
                                aux = "";
                                fila++;
                                columna = 0;
                            }
                            else if (c == '#')
                            {
                                if (i == entrada.Length - 1)
                                {
                                    Console.WriteLine("Analisis Finalizado");
                                }
                                else
                                {
                                    aux += c;
                                }
                                listaErrores.AddLast(new Token(idError, Token.Nombre.Error, aux, fila, columna));
                                idError++;
                                estado = 0;
                                aux = "";
                            }
                            else
                            {
                                aux += c;
                                estado = 9;
                            }
                        }
                        break;
                    case 10:
                        string segmento3 = entrada.Substring(i);
                        bool cierre3 = false;
                        for (int j = 0; j < segmento3.Length; j++)
                        {
                            char caracter = segmento3.ElementAt(j);
                            if (caracter == ':')
                            {
                                if (segmento3.ElementAt(j + 1) == ']')
                                {
                                    cierre3 = true;
                                    break;
                                }
                            }
                        }
                        if (cierre3)
                        {
                            if (c != ':')
                            {
                                aux += c;
                                estado = 10;
                                if (c == '\n')
                                {
                                    fila++;
                                    columna = 0;
                                }
                            }
                            else
                            {
                                aux += c;
                                aux += entrada.ElementAt(i + 1);
                                listaTokens.AddLast(new Token(idToken, Token.Nombre.Comentario_Multilinea, aux, fila, columna));
                                idToken++;
                                estado = 0;
                                aux = "";
                                i++;
                            }
                        }
                        else
                        {
                            if (c == '\n')
                            {
                                aux += c;
                                listaErrores.AddLast(new Token(idError, Token.Nombre.Error, aux, fila, columna));
                                idError++;
                                estado = 0;
                                aux = "";
                                fila++;
                                columna = 0;
                            }
                            else if (c == '#')
                            {
                                if (i == entrada.Length - 1)
                                {
                                    Console.WriteLine("Analisis Finalizado");
                                }
                                else
                                {
                                    aux += c;
                                }
                                listaErrores.AddLast(new Token(idError, Token.Nombre.Error, aux, fila, columna));
                                idError++;
                                estado = 0;
                                aux = "";
                            }
                            else
                            {
                                aux += c;
                                estado = 4;
                            }
                        }
                        break;
                }
            }
        }
    }
}
