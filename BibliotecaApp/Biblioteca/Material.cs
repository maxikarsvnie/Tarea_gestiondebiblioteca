namespace Biblioteca
{
    public abstract class Material //Clase Material abstracta para usarla como padre de Libro y revista
    {
        private string _nombre; //Giarda el nombre del material
        private string _idMaterial;//Guarda el ID
        private string _categoria; //Guarda la cartegoria
        public int Anio { get; set;} //Guardamos el año y se puede leer y modificar
            //encapsulamiento
        public string Nombre//Se utiliza solo el get para que se pueda leer pero no modificar el nombre
        {
            get { return _nombre; }
        }
        public string Categoria
        {
            get { return _categoria; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))//If para comprobar qe categoria tenga datos si no sa error
                {
                    throw new ArgumentException("La categoría no puede estar vacía.");
                }
                _categoria = value;
            }
        }

        protected Material(string id, string nombre, string categoria, int anio)//Constructor: Lo usamos para crear un nuevo material
        {
            _idMaterial = id;
            _nombre = nombre;
            _categoria = categoria; 
            Anio = anio; 
        }
        abstract public decimal CalcularCostoBase();//Metodo abstracto que hace que cada clase hija defina la forma de calcular el costo base

        public virtual string ObtenerFicha()//Metodo virtual, lo usamos para devolver una ficha de material en un texto
        {
            return $"{_idMaterial} | {_nombre} | {_categoria} | {Anio}";
        }
    }
}