using System;
using System.Collections.Generic;
using System.Text;
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

        public string CompararArboles(Arbol arbolA, Arbol arbolB)
        {
            var sb = new StringBuilder();

            int nodosA = ContarNodos(arbolA.NodoRaiz);
            int nodosB = ContarNodos(arbolB.NodoRaiz);

            int nivelesA = Altura(arbolA.NodoRaiz);
            int nivelesB = Altura(arbolB.NodoRaiz);

            bool similares = nodosA == nodosB;
            bool equivalentes = nivelesA == nivelesB;

            if (similares)
                sb.AppendLine("✓ Son similares: tienen la misma cantidad de nodos.|");
            else
                sb.AppendLine("✗ No son similares: tienen diferente cantidad de nodos.|");

            if (equivalentes)
                sb.AppendLine("✓ Son equivalentes: tienen la misma cantidad de niveles.|");
            else
                sb.AppendLine("✗ No son equivalentes: tienen diferente cantidad de niveles.|");

            if (!similares || !equivalentes)
                sb.AppendLine("✗ Son distintos: no tienen la misma cantidad de nodos y niveles.|");
            else
                sb.AppendLine("✓ Son iguales: tiene la misma cantidad de nodos o niveles.|");

            return sb.ToString();
        }

        public int ContarNodos(Nodo nodo)
        {
            if (nodo == null) return 0;
            return 1 + ContarNodos(nodo.RamaIzquierda) + ContarNodos(nodo.RamaDerecha);
        }

        public int Altura(Nodo nodo)
        {
            if (nodo == null) return 0;
            return 1 + Math.Max(Altura(nodo.RamaIzquierda), Altura(nodo.RamaDerecha));
        }

        public void CambioRamas(string nombreArbolito)
        {
            var arbolito = Arboles[nombreArbolito];
            Arbol NewArbol;

            if (arbolito.contrario)
            {
                NewArbol = new Arbol();
                RecorridoPreOrdenEspejo(arbolito.NodoRaiz, NewArbol);
            }
            else
            {
                NewArbol = new Arbol(true);
                RecorridoPreOrden(arbolito.NodoRaiz, NewArbol);
            }
            
            Arboles[nombreArbolito] = NewArbol;

        }

        public void RecorridoPreOrden(Nodo nodo, Arbol newArbol)
        {
            if (nodo == null)
            {
                return;
            }
            else
            {
                newArbol.InsertarEspejo(newArbol.NodoRaiz, Convert.ToInt32(nodo.Dato));
                RecorridoPreOrden(nodo.RamaIzquierda, newArbol);
                RecorridoPreOrden(nodo.RamaDerecha, newArbol);
            }
        }

        public void RecorridoPreOrdenEspejo(Nodo nodo, Arbol newArbol)
        {
            if (nodo == null)
            {
                return;
            }
            else
            {
                newArbol.Insertar(newArbol.NodoRaiz, Convert.ToInt32(nodo.Dato));
                RecorridoPreOrdenEspejo(nodo.RamaDerecha, newArbol);
                RecorridoPreOrdenEspejo(nodo.RamaIzquierda, newArbol);
            }
        }
    }
}
