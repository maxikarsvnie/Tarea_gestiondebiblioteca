namespace Biblioteca
{
    public class Libro : Material, IPrestable //Creamos la clase hija Libro herada de Material y IPrestable
    {
        //Datos del libro
        private string _autor; //Guardamos el uator del lubro
        private int _numPaginas; //Guardamos la cantidad de paginas
        public bool EstaDisponible { get; private set; } = true; //Propiedad que indica si el libro esta disponible

        public string Autor
        { 
            get { return _autor; } 
            set { _autor = value; }
        }
        public int NumeroPaginas
        {
            get { return _numPaginas; }
            set { _numPaginas = value; }
        }

        public Libro(string id, string nombre, string categoria, int anio, string autor, int numPaginas) : base(id, nombre, categoria, anio)//Constructor
        {
            _autor = autor;
            _numPaginas = numPaginas;
        }

        public override decimal CalcularCostoBase()//Usamos el metodo de la clase padre para calcular el costo base
        {
            return 1500 + (NumeroPaginas * 0.5m);//Costo base: tarifa fija $1500 + $0.5 por página
        }

        public override string ObtenerFicha()//redefinimos la clase obtener ficha de la clase padre
        {
            return base.ObtenerFicha() + $" | {_autor} | {_numPaginas} Pags";//Devuelve todos sus datos, incluyendo autor y paginas
        }

        public void AccionPropia()//Solo devuelve en mensaje dentro del Console
        {
            Console.WriteLine("El libro se puede prestar.");
        }

        public void Prestar(string detalle)//Implementamos el metrodo IPrestable
        {
            if (!EstaDisponible)//Usamos para modificar el estado del libro a "no disponible"
            {
               Console.WriteLine("El libro esta disponible para prestar.");
                return;
            }
                EstaDisponible = false;
                Console.WriteLine("El libro no esta " + detalle);
        }
        public void Devolver()//Implementamos el metodo Iprestable
        {
            EstaDisponible = true;//Sirve para cambiar el estado a "Disponible"
            Console.WriteLine("El libro ha sido devuelto.");
        }
        public string ObtenerEstado()//Implementacion de IPrestable
        {
            return EstaDisponible ? "Disponible para prestar" : "No disponible para prestar";//Devisa el estado de esta disponible y devuelve el texto segun corresponde
        }
    }
}