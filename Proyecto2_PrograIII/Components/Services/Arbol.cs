using System.Text;

namespace Proyecto2_PrograIII.Components.Services
{
    public class Arbol
    {
        public Nodo? NodoRaiz { get; set; }
        int cantidadNodos = 0;

        public Arbol()
        {
            NodoRaiz = null;
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

        public void Eliminar(int dato)
        {
            Nodo nodoEliminar = Busqueda(NodoRaiz, dato);

            if (nodoEliminar == null)
            {
                Console.WriteLine("No se encontro el dato Buscado.");
                return;
            }

            if (cantidadNodos == 1)
            {
                NodoRaiz = null;
                return;
            }

            //Eliminamos si es una hoja
            if (nodoEliminar.RamaIzquierda == null && nodoEliminar.RamaDerecha == null)
            {
                Nodo NodoPadre = Padre(NodoRaiz, dato);

                if (NodoPadre.RamaDerecha != null && NodoPadre.RamaDerecha.Dato.Equals(dato))
                {
                    NodoPadre.RamaDerecha = null;
                }
                else if (NodoPadre.RamaIzquierda != null && NodoPadre.RamaIzquierda.Dato.Equals(dato))
                {
                    NodoPadre.RamaIzquierda = null;
                }
            }

            //Si detectamos que tiene 1 hijos
            else if ((nodoEliminar.RamaIzquierda != null && nodoEliminar.RamaDerecha == null) ||
               (nodoEliminar.RamaDerecha != null && nodoEliminar.RamaIzquierda == null))
            {
                Nodo NodoPadre = Padre(NodoRaiz, dato);


                if (NodoPadre.RamaIzquierda != null && NodoPadre.RamaIzquierda.Dato.Equals(dato))
                {
                    if (nodoEliminar.RamaIzquierda != null)
                    {
                        NodoPadre.RamaIzquierda = nodoEliminar.RamaIzquierda;
                    }
                    else
                    {
                        NodoPadre.RamaIzquierda = nodoEliminar.RamaDerecha;
                    }
                    nodoEliminar = null;
                }

                if (NodoPadre.RamaDerecha != null && NodoPadre.RamaDerecha.Dato.Equals(dato))
                {
                    if (nodoEliminar.RamaIzquierda != null)
                    {
                        NodoPadre.RamaDerecha = nodoEliminar.RamaIzquierda;
                    }
                    else
                    {
                        NodoPadre.RamaDerecha = nodoEliminar.RamaDerecha;
                    }
                    nodoEliminar = null;
                }
            }


            //Si detectamos que tiene 2 hijos
            else if (nodoEliminar.RamaDerecha != null && nodoEliminar.RamaIzquierda != null)
            {
                Nodo Sucesor = sucesor(nodoEliminar.RamaDerecha);
                Nodo PadreSucesor = Padre(NodoRaiz, Convert.ToInt32(Sucesor.Dato));

                nodoEliminar.Dato = Sucesor.Dato;

                if (Sucesor.RamaDerecha != null)
                {
                    PadreSucesor.RamaDerecha = Sucesor.RamaDerecha;
                    Sucesor = null;
                }
                else if (nodoEliminar == PadreSucesor)
                {
                    PadreSucesor.RamaDerecha = null;
                }
                else
                {
                    PadreSucesor.RamaIzquierda = null;
                }
            }

            //Eliminnar si tiene un solo hijo

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
                resultado += $"[{nodo.Dato}] ";
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
                resultado += $"[{nodo.Dato}] ";
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

        public int ObtenerNivel(Nodo nodo, int valorBuscado, int nivelActual = 0)
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

        /*private void GenerarDotRecursivo(Nodo nodo, StringBuilder sb)
        {
            if (nodo == null) return;

            if (nodo.RamaIzquierda != null)
            {
                sb.AppendLine($"  {nodo.Dato} -> {nodo.RamaIzquierda.Dato};");
                GenerarDotRecursivo(nodo.RamaIzquierda, sb);
            }
            if (nodo.RamaDerecha != null)
            {
                sb.AppendLine($"{nodo.Dato} -> {nodo.RamaDerecha.Dato};");
                GenerarDotRecursivo(nodo.RamaDerecha, sb);
            }

            // Si es hoja, asegurarse de que aparezca en el gráfico
            if (nodo.RamaIzquierda== null && nodo.RamaDerecha== null)
            {
                sb.AppendLine($"{nodo.Dato};");
            }
        }*/

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

        public void RecorridoInOrden(Nodo nodo)
        {
            if (nodo == null)
            {
                return;
            }
            else
            {
                RecorridoInOrden(nodo.RamaIzquierda);
                Console.WriteLine($"{nodo.Dato}");
                RecorridoInOrden(nodo.RamaDerecha);
            }
        }

        public void RecorridoPreOrden(Nodo nodo)
        {
            if (nodo == null)
            {
                return;
            }
            else
            {
                Console.WriteLine($"{nodo.Dato}");
                RecorridoPreOrden(nodo.RamaIzquierda);
                RecorridoPreOrden(nodo.RamaDerecha);
            }
        }

        public void RecorridoPostOrden(Nodo nodo)
        {
            if (nodo == null)
            {
                return;
            }
            else
            {
                RecorridoPostOrden(nodo.RamaIzquierda);
                RecorridoPostOrden(nodo.RamaDerecha);
                Console.WriteLine($"{nodo.Dato}");
            }
        }
    }
}
