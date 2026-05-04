namespace Biblioteca
{
    public class AgregarRegistro
    {
        public void AgregarNuevoMaterial(List<Material> registros, Material nuevoMaterial)//Metodo creado para agregar un nuevo material
        {
            Console.WriteLine("\n=== Paso 3: Agregar Nuevo Registro ===");//Nombre del paso y lo que hace

            AgregarRegistro agregarRegistro = new AgregarRegistro();//Creacion del objeto agregar registro
            
            agregarRegistro.AgregarNuevoMaterial(registros, new Libro("M007", "Don Quijote", "Libro", 1605, "Cervantes, M.", 863));//Llamamos al metodo para agregar un nuevo registro con sus datos
            
            Console.WriteLine("Nuevo material agregado. total de materiales: " + registros.Count);//Mensaje de que se agrego el registro y la camtidad de materiales
            
            foreach (var material in registros) //Recorremos y mostramos la ficha de cada registro
            {
                Console.WriteLine(material.ObtenerFicha());
            }
        }
    }
}