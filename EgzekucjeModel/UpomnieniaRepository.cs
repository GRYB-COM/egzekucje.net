namespace Egzekucje.NET
{
    public interface UpomnieniaRepository
    {
        Upomnienie Pobierz(long idUpomnienia);
        void UsunPoIdOsoby(long idOsoby);
        Upomnienie Zapisz(Upomnienie upomnienie);
    }
}