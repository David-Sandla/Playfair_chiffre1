namespace Playfair_Chiffre_Lib{
    
    public interface IChiffre{
        string Encrypt(string msg);
        string Decrypt(string msg);
    }
}