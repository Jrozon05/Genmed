using System;
using System.IO;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace genmed_api.Utils.Extensions
{
    public static class Extensiones
    {
        #region Functions
        public static string Encrypt(this string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        public static string Decrypt(this string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
        
        public static bool validarUserName(this string nombreUsuario)
        {
            string pattern = @"(?=[A-Za-z0-9])(?!._-]{1})[A-Za-z0-9._-]{3,15}$";
            Match m = Regex.Match(nombreUsuario, pattern, RegexOptions.IgnoreCase);
            
            if(!m.Success)
            {
                return false;
            }

            return true;
        }

        public static bool validarClave(this string clave)
        {
            string pattern = "^(?=.*[a-z])(?=.*[A-Z])(?=.*)(?=.*[^a-zA-Z]).{5,15}$";
            Match m = Regex.Match(clave, pattern, RegexOptions.IgnoreCase);

            if(!m.Success)
            {
                return false;
            }

            return true;
        }

        public static bool validarEmail(this string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);

                return true;
            }
            catch (FormatException) 
            {
                return false;
            }
        }
        
        public static bool validarNombreApellido(this string nombreApellido)
        {
            string pattern = "^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$";
            Match m = Regex.Match(nombreApellido, pattern);
            
            if(!m.Success)
            {
                return false;
            }

            return true;
        }

        public static bool validarPosicion(this string posicion)
        {
            string pattern = "[a-zA-Z]";
            Match m = Regex.Match(posicion, pattern);
            
            if(!m.Success)
            {
                return false;
            }

            return true;
        }
    
        #endregion
    }
}