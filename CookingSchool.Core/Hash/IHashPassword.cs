namespace Cooking_School.Core.Hash
{
    public interface IHashPassword
    {
        void createHashPassword(string password, out byte[] passswordHash, out byte[] passwordSlot);

        bool verifyPassword(string password, byte[] passwordHash,byte[] passwordSlot);

    }
}
