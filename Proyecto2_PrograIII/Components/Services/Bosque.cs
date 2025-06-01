using System;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace Proyecto2_PrograIII.Components.Services
{
    public class Bosque
    {
        public Dictionary<string, Arbol> Arboles;
        private int indice;

        public Bosque()
        {
            Arboles = new Dictionary<string, Arbol>();
            indice = 1;
        }

        public string nombreNuevo()
        {
            int actual = indice;
            indice++; // Para el siguiente nombre

            string nombre = "";

            while (actual > 0)
            {
                actual--; // Ajuste porque A = 0
                nombre = (char)('A' + (actual % 26)) + nombre;
                actual /= 26;
            }

            return nombre;
        }

    }
}
