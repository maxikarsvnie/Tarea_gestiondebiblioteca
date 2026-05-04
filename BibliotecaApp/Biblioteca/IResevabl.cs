namespace Biblioteca
{
    public interface IPrestable
    {
        bool EstaDisponible { get; } //chequeamos si el material esta disponible para ser prestado
        void Prestar(string detalle);//Metodo para prestar el material
        void Devolver();//Metodo para devolver el material
        string ObtenerEstado();//Metodo que indica si el material esta disponible para prestarse
    }
}   