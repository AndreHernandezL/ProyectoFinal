namespace Proyecto2_PrograIII.Components.Services
{
    public class Nodo
    {
        public Nodo? RamaDerecha { get; set; }
        public Nodo? RamaIzquierda { get; set; }
        public object? Dato { get; set; }

        public Nodo(int dato)
        {
            RamaDerecha = null;
            RamaIzquierda = null;
            Dato = dato;
        }

        public Nodo()
        {
            RamaDerecha = null;
            RamaIzquierda = null;
            Dato = null;
        }
    }
}
