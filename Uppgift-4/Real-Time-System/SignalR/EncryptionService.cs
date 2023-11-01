//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Security.Cryptography;

//namespace IoTSimulator
//{
//    internal class EncryptionService
//    {
//        private readonly string encryptionKey;

//        public EncryptionService(string key)
//        {
//            encryptionKey = key;
//        }

//        public string Encrypt(string data)
//        {
//            using (Aes aesAlg = Aes.Create())
//            {
//                aesAlg.Key = Encoding.UTF8.GetBytes(encryptionKey);
//                aesAlg.IV = new byte[16]; // Initialization Vector

//                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

//                using (var memoryStream = new MemoryStream())
//                {
//                    using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
//                    {
//                        using (var streamWriter = new StreamWriter(cryptoStream))
//                        {
//                            streamWriter.Write(data);
//                        }
//                    }
//                    return Convert.ToBase64String(memoryStream.ToArray());
//                }
//            }
//        }
//    }
//}
