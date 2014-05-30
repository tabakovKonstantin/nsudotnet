using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Enigma
{
    class Crypt
    {
        private Stream _inputStream;
        private Stream _outputStream;
        private Stream _keyStream;

        private SymmetricAlgorithm _algorithm;

        private byte[] _key;
        private byte[] _iv;

        public Crypt(Stream inputStream, Stream outputStream, Stream keyStream)
        {
            _inputStream = inputStream;
            _outputStream = outputStream;
            _keyStream = keyStream;
        }

        public void Encrypt()
        {
            CheckAlgorithmNull();

            _key = _algorithm.Key;
            _iv = _algorithm.IV;

            WriteKeyToStream();

            ICryptoTransform encryptor = _algorithm.CreateEncryptor(_key, _iv);

            using (CryptoStream cryptoStream = new CryptoStream(_outputStream, encryptor, CryptoStreamMode.Write))
            {
                _inputStream.CopyTo(cryptoStream);
            }
 
        }

        public void Decrypt()
        {
            CheckAlgorithmNull();
            
            _algorithm.Padding = PaddingMode.None;

            ReadKeyToStream();
            
            ICryptoTransform decryptor = _algorithm.CreateDecryptor(_key, _iv);

            using (CryptoStream cryptoStream = new CryptoStream(_inputStream, decryptor, CryptoStreamMode.Read))
            {
                cryptoStream.CopyTo(_outputStream);
            }
            
        }

        private void WriteKeyToStream()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(Convert.ToBase64String(_iv));
            stringBuilder.AppendLine(Convert.ToBase64String(_key));

            string IVandKey = stringBuilder.ToString();

            using (StreamWriter streamWriter = new StreamWriter(_keyStream))
            {
                streamWriter.Write(IVandKey);
            }

        }

        private void ReadKeyToStream()
        {
            string IVLine;
            string KeyLine;

            using (StreamReader streamReader = new StreamReader(_keyStream))
            {
                IVLine = streamReader.ReadLine();
                KeyLine = streamReader.ReadLine();
            }

            _iv = Convert.FromBase64String(IVLine.TrimEnd(new Char[] { ' ' }));
            _key = Convert.FromBase64String(KeyLine.TrimEnd(new Char[] { ' ' }));

            if (_key == null || _key.Length <= 0)
                throw new ArgumentNullException("Все пиздец c ключем");
            if (_iv == null || _iv.Length <= 0)
                throw new ArgumentNullException("Все пиздец c вектором");
        }

        private void CheckAlgorithmNull()
        {
            if (_algorithm == null)
            {
                throw new Exception("Не установлен алгорит");
            }
        }

        public void SetInputStream(Stream inputStream)
        {
            _inputStream = inputStream;
        }

        public void SetOutputStream(Stream outputStream)
        {
            _outputStream = outputStream;
        }

        public void SetKeyStream(Stream keyStream)
        {
            _keyStream = keyStream;
        }

        public void SetAlgorithm(SymmetricAlgorithm algorithm)
        {
            _algorithm = algorithm;
        }
    }
}
