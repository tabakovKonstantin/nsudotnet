using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Enigma
{
    class FactoryCrypt
    {
        private Dictionary<AlgorithmType, Type> _typeCrypt = new Dictionary<AlgorithmType, Type>();

        public FactoryCrypt()
        {
            RegisterType(AlgorithmType.AES, typeof(AesCryptoServiceProvider));
            RegisterType(AlgorithmType.DES, typeof(TripleDESCryptoServiceProvider));
            RegisterType(AlgorithmType.RC2, typeof(RC2CryptoServiceProvider));
            RegisterType(AlgorithmType.Rijndael, typeof(RijndaelManaged));
        }

        private void RegisterType(AlgorithmType typeName, Type type)
        {
            _typeCrypt.Add(typeName, type);
        }

        public SymmetricAlgorithm CreateObject(AlgorithmType typeName)
        {
            Type type = null;

            if (!_typeCrypt.TryGetValue(typeName, out type))
            {
                return null;
            }
                
            return (SymmetricAlgorithm)Activator.CreateInstance(type);
        }
    }
}
