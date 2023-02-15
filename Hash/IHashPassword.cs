namespace Cooking_School_ASP.NET.Hash
{
    public interface IHashPassword
    {
        void createHashPassword(string password, out byte[] passswordHash, out byte[] passwordSlot);

        bool verifyPassword(string password, byte[] passwordHash,byte[] passwordSlot);

    }
}
