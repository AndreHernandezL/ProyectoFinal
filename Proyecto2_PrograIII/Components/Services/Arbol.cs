using System.Text;

namespace Proyecto2_PrograIII.Components.Services
{
    public class Arbol
    {
        public Nodo? NodoRaiz { get; set; }
        int cantidadNodos = 0;
        public bool contrario;

        public Arbol()
        {
            NodoRaiz = null;
            contrario = false;
        }

        public Arbol(bool contrario)
        {
            NodoRaiz = null;
            this.contrario = contrario;
        }

        bool EstaVacio()
        {
            return NodoRaiz == null;
        }

        public void Insertar(Nodo nodo, int Dato)
        {

            if (EstaVacio())
            {
                Nodo NuevoNodo = new Nodo(Dato);
                NodoRaiz = NuevoNodo;
            }
            else if (Dato > Convert.ToInt32(nodo.Dato))
            {
                if (nodo.RamaDerecha == null)
                {
                    Nodo NuevoNodo = new Nodo(Dato);
                    nodo.RamaDerecha = NuevoNodo;
                }
                else
                {
                    Insertar(nodo.RamaDerecha, Dato);
                }
            }
            else if (Dato < Convert.ToInt32(nodo.Dato))
            {
                if (nodo.RamaIzquierda == null)
                {
                    Nodo newNodo = new Nodo(Dato);
                    nodo.RamaIzquierda = newNodo;
                }
                else
                {
                    Insertar(nodo.RamaIzquierda, Dato);
                }
            }
            cantidadNodos++;
        }

        public void InsertarEspejo(Nodo nodo, int Dato)
        {

            if (EstaVacio())
            {
                Nodo NuevoNodo = new Nodo(Dato);
                NodoRaiz = NuevoNodo;
            }
            else if (Dato < Convert.ToInt32(nodo.Dato))
            {
                if (nodo.RamaDerecha == null)
                {
                    Nodo NuevoNodo = new Nodo(Dato);
                    nodo.RamaDerecha = NuevoNodo;
                }
                else
                {
                    InsertarEspejo(nodo.RamaDerecha, Dato);
                }
            }
            else if (Dato > Convert.ToInt32(nodo.Dato))
            {
                if (nodo.RamaIzquierda == null)
                {
                    Nodo newNodo = new Nodo(Dato);
                    nodo.RamaIzquierda = newNodo;
                }
                else
                {
                    InsertarEspejo(nodo.RamaIzquierda, Dato);
                }
            }
            cantidadNodos++;
        }


        public Nodo Busqueda(Nodo nodo, int dato)
        {
            if (nodo == null)
            {
                return null;
            }
            else if (dato > Convert.ToInt32(nodo.Dato))
            {
                return Busqueda(nodo.RamaDerecha, dato);
            }
            else if (dato < Convert.ToInt32(nodo.Dato))
            {
                return Busqueda(nodo.RamaIzquierda, dato);
            }
            else
            {
                return nodo;
            }
        }

        public void EliminarTodasLasHojas()
        {
            NodoRaiz = EliminarHojas(NodoRaiz);
        }

        public Nodo EliminarHojas(Nodo nodo)
        {
            if (nodo == null)
                return null;

            if (nodo.RamaIzquierda == null && nodo.RamaDerecha == null)
                return null; // Es una hoja, eliminarla

            nodo.RamaIzquierda = EliminarHojas(nodo.RamaIzquierda);
            nodo.RamaDerecha = EliminarHojas(nodo.RamaDerecha);

            return nodo;
        }

        public Nodo sucesor(Nodo nodo)
        {
            if (nodo.RamaIzquierda != null)
            {
                return sucesor(nodo.RamaIzquierda);
            }
            else
            {
                return nodo;
            }
        }

        public Nodo Padre(Nodo nodo, int dato)
        {

            if (nodo.RamaIzquierda != null && nodo.RamaIzquierda.Dato.Equals(dato))
            {
                return nodo;
            }
            if (nodo.RamaDerecha != null && nodo.RamaDerecha.Dato.Equals(dato))
            {
                return nodo;
            }
            else
            {
                if (nodo.RamaDerecha != null || nodo.RamaIzquierda != null)
                {
                    if (dato < Convert.ToInt32(nodo.Dato))
                    {
                        return Padre(nodo.RamaIzquierda, dato);
                    }
                    else
                    {
                        return Padre(nodo.RamaDerecha, dato);
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        public string NodosInternos(Nodo nodo)
        {
            if (nodo == null)
            {
                return "";
            }

            string resultado = "";

            // Recorre rama izquierda
            resultado += NodosInternos(nodo.RamaIzquierda);

            // Nodo interno: tiene al menos un hijo (izquierdo o derecho)
            if (nodo.RamaIzquierda != null || nodo.RamaDerecha != null)
            {
                resultado += $"{nodo.Dato} ";
            }

            // Recorre rama derecha
            resultado += NodosInternos(nodo.RamaDerecha);

            return resultado;
        }

        public string NodosExternos(Nodo nodo)
        {
            if (nodo == null)
            {
                return "";
            }

            string resultado = "";

            // Recorre rama izquierda
            resultado += NodosExternos(nodo.RamaIzquierda);

            // Nodo hoja: no tiene hijos
            if (nodo.RamaIzquierda == null && nodo.RamaDerecha == null)
            {
                resultado += $"{nodo.Dato} ";
            }

            // Recorre rama derecha
            resultado += NodosExternos(nodo.RamaDerecha);

            return resultado;
        }

        public int ObtenerGrado(Nodo nodo)
        {
            if (nodo == null)
                return 0;

            int grado = 0;

            if (nodo.RamaIzquierda != null) grado++;
            if (nodo.RamaDerecha != null) grado++;

            return grado;
        }

        public int ObtenerNivel(Nodo nodo, int valorBuscado, int nivelActual = 1)
        {
            if (nodo == null)
                return -1; // No encontrado

            if (Convert.ToInt32(nodo.Dato) == valorBuscado)
                return nivelActual;

            // Buscar en la izquierda
            int nivelIzq = ObtenerNivel(nodo.RamaIzquierda, valorBuscado, nivelActual + 1);
            if (nivelIzq != -1)
                return nivelIzq;

            // Buscar en la derecha
            return ObtenerNivel(nodo.RamaDerecha, valorBuscado, nivelActual + 1);
        }


        public int LongitudCaminoInterno(Nodo nodo, int nivel = 1)
        {
            if (nodo == null)
                return 0;

            // Si el nodo tiene al menos un hijo, cuenta como interno
            //bool esInterno = nodo.RamaIzquierda != null || nodo.RamaDerecha != null;

            int suma = nivel;

            suma += LongitudCaminoInterno(nodo.RamaIzquierda, nivel + 1);
            suma += LongitudCaminoInterno(nodo.RamaDerecha, nivel + 1);

            return suma;
        }

        public int LongitudCaminoExterno(Nodo nodo, int nivel = 1)
        {
            if (nodo == null)
            {
                // Si llegamos a un hijo nulo, cuenta como nodo externo
                return nivel;
            }

            int suma = 0;
            suma += LongitudCaminoExterno(nodo.RamaIzquierda, nivel + 1);
            suma += LongitudCaminoExterno(nodo.RamaDerecha, nivel + 1);

            return suma;

        }

     

        private void GenerarDotRecursivo(Nodo nodo, StringBuilder sb)
        {
            if (nodo == null) return;

            // Si tiene hijo izquierdo, conectar
            if (nodo.RamaIzquierda != null)
            {
                sb.AppendLine($"  {nodo.Dato} -> {nodo.RamaIzquierda.Dato};");
                GenerarDotRecursivo(nodo.RamaIzquierda, sb);
            }
            else if (nodo.RamaDerecha != null)
            {
                // No tiene hijo izquierdo, pero sí derecho => agregar invisible a la izquierda
                string nullNodeId = $"null_{nodo.Dato}_L";
                sb.AppendLine($"  {nodo.Dato} -> {nullNodeId} [style=invis];");
                sb.AppendLine($"  {nullNodeId} [style=invis];");
            }

            // Si tiene hijo derecho, conectar
            if (nodo.RamaDerecha != null)
            {
                sb.AppendLine($"  {nodo.Dato} -> {nodo.RamaDerecha.Dato};");
                GenerarDotRecursivo(nodo.RamaDerecha, sb);
            }
            else if (nodo.RamaIzquierda != null)
            {
                // No tiene hijo derecho, pero sí izquierdo => agregar invisible a la derecha
                string nullNodeId = $"null_{nodo.Dato}_R";
                sb.AppendLine($"  {nodo.Dato} -> {nullNodeId} [style=invis];");
                sb.AppendLine($"  {nullNodeId} [style=invis];");
            }

            // Si es hoja (sin hijos), solo agregarlo para asegurar visibilidad
            if (nodo.RamaIzquierda == null && nodo.RamaDerecha == null)
            {
                sb.AppendLine($"  {nodo.Dato};");
            }
        }

        public string GenerarDot()
        {
            var sb = new StringBuilder();
            sb.AppendLine("digraph G {");
            sb.AppendLine("node [shape=circle];");
            GenerarDotRecursivo(NodoRaiz, sb);
            sb.AppendLine("}");
            return sb.ToString();
        }

    }
}
