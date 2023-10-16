namespace ClientePathBit.Models
{
    public class Seguranca
    {
        public static string CriptografarSHA512(string Senha)
        {
            System.Security.Cryptography.SHA512 cri = System.Security.Cryptography.SHA512.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(Senha);
            byte[] hash = cri.ComputeHash(inputBytes);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString(); // Retorna senha criptografada
        }
    }
}
