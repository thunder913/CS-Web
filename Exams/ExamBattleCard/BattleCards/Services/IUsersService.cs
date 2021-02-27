namespace BattleCards.Services
{
    public interface IUsersService
    {
        public void AddUser(string username, string email, string password);

        public string UserExists(string username, string password);

        public bool IsUsernameTaken(string username);

        public bool IsEmailTaken(string email);
    }
}
