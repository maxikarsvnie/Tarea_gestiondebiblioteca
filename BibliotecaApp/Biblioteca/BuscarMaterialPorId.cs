using Biblioteca;

namespace BibliotecaApp
{
    public static class Utilidades
    {
        public static Material BuscarMaterialPorId(List<Material> lista, string id, int indice = 0)//Busca materiales dentro de la lista usando su ID
        {
            if (indice >= lista.Count) return null; // caso base: si el indice llego al final del la lista, significa que no se encontro

            if (lista[indice].ObtenerFicha().StartsWith(id)) return lista[indice];//Devuelve la ficha del material buscado por el Id

            return BuscarMaterialPorId(lista, id, indice + 1); // recursión, Si no conincide se llama al metodo asi mismo para avanzar con el siguiente hasta encontrarlo o llegar al final
        }
    }
}
