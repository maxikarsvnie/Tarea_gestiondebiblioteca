namespace Biblioteca
{
    public record Prestamo (
    string IdPrestamo, //Id del pretamo
    string IdMaterial, //Codigo del material prestado
    string NombreSocio, //Nombre de la persona que saco el prestamo
    string Motivo, //Motivo del prestamo
    Decimal multa, //Multa asociada al prestamo
    DateTime Fecha //Fecha del prestamo
    );//Se usa recor ya que es una clase que sirve para guardar texto de manera simple y autimatica
}