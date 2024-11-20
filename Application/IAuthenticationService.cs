namespace VimaV2.Services
{
    public interface IAuthenticationService
    {
        // Método para autenticar o usuário com base no token JWT
        bool Authenticate(string token);

        // Método para verificar se o usuário tem a role necessária para acessar a API
        bool Authorize(string token, string role);
    }
}
