namespace Biblioteca
{
    public class Revista : Material //Creamos clase Hija revista que hereda de la clase Material 
    {
        //Guardamos los información de revista
        private string _editorial; //Nombre de la editorial
        private int _nroEdicion; //Numero de edicion de la revista

        public string Editorial
        {
            get { return _editorial; }
            set { _editorial = value; }
        }
        public int NroEdicion
        {
            get { return _nroEdicion; }
            set { _nroEdicion = value; }
        }
        //Llamamos la constructor de material y le agegamos editorail y numero de edicion
        public Revista(string id, string nombre, string categoria, int anio, string editorial, int nroEdicion) : base(id, nombre, categoria, anio)//Constructor
        {
            _editorial = editorial;
            _nroEdicion = nroEdicion;
        }

        public override decimal CalcularCostoBase()//Implementamos el metodo de la calse padre para calcular el costo de la revista
        {
            return 800m;//Costo base: tarifa fijo de $800 para revistas
        }
        public override string ObtenerFicha()//Redefinimos la el metodo obtener ficha agregando editorial y nro de edicion
        {
            return base.ObtenerFicha() + $" | {Editorial} | Edición {NroEdicion}";
        }
    }
}
    