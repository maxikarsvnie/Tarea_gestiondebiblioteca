namespace Biblioteca
{
    public class EliminarRegistro//Creamos esta clase para borrar materiales de la lista
    {
        public void EliminarPorNombre(List<Material> registros, string nombre)
        {
            registros.RemoveAll(m => m.Nombre == nombre); //Busca todo los materiales que coincidan con el nombrey los elimina de la lista
            if (registros.Any(m => m.Nombre == nombre))//Revisamos si existe algun material con el nombre
            {
                Console.WriteLine($"No se pudo eliminar el material con nombre '{nombre}', porque no existe.");//Si aun aparece es poque no se pudo eliminar, seguramente esta mal escrito
            }
            else
            {
                Console.WriteLine($"Material con nombre '{nombre}' eliminado exitosamente.");//Si ya no existe es porque se elimino correctamente
            }
        }
    }
}