using CIoTD.Security;

namespace CIoTD.Infrastructure
{
    public static class UserRepository
    {
        public static User Get(string username, string password)
        {
            // ToDo: aqui pode ser incluída futuramente a recuperação do usuário e senha
            // a partir de um banco de dados para fazer uma checagem prévia se usuário
            // é válido ou não antes de gerar o token
            //
            // Exemplo com usuário hardcoding
            // var users = new List<User>
            // {
            //    new User { Id = 1, Username = "root", Password = "admin", Role = "owner" }
            // };
            // return users.FirstOrDefault(x => x.Username == username &&
            //                                x.Password == password);
            return new User { Id = 1, Username = username, Password = password, Role = "" };
        }
    }
}
