using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thompson_Cerradura
{
    class Token
    {
        public enum Nombre
        {
            Comentario,
            Comentario_Multilinea,
            Reservada_CONJ,
            Dos_Puntos,
            Identificador,
            Punto_Y_Coma,
            Asignador,
            Conjunto,
            Punto,
            Or,
            Asterisco,
            Comilla_Doble,
            Cadena,
            Signo_Cierre_Interrogacion,
            Mas,
            Coma,
            Todo,
            Salto_de_Linea,
            Comilla_Simple,
            Tabulacion,
            LLave_IZQ,
            LLave_DER,
            Error
        }

        public Token(int IdToken, Nombre NombreToken, String Valor, int Fila, int Columna)
        {
            this.IdToken = IdToken;
            this.NombreToken = NombreToken;
            this.Valor = Valor;
            this.Fila = Fila;
            this.Columna = Columna;
        }

        public int IdToken { get; set; }
        public Nombre NombreToken { get; set; }
        public String Valor { get; set; }
        public int Fila { get; set; }
        public int Columna { get; set; }
    }
}
