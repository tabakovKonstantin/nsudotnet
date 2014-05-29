using System;
using System.IO;
using System.Security.Cryptography;

namespace Enigma
{
    class Controller
    {
        private Parser _parser;
        private Crypt _crypt;
        private FactoryCrypt _factoryCrypt;

        private Stream _inputStream;
        private Stream _outputStream;
        private Stream _keyStream;

        public Controller(string[] args)
        {
            try
            {
                _parser = new Parser(args);

                using (_inputStream = new FileStream(_parser.GetPathInputFile(), FileMode.Open, FileAccess.Read))
                {
                    using (_outputStream = new FileStream(_parser.GetPathOutputFile(), FileMode.Create, FileAccess.Write))
                    {
                        using (_keyStream = new FileStream(_parser.GetPathKeyFile(), FileMode.OpenOrCreate, FileAccess.ReadWrite))
                        {
                            _crypt = new Crypt(_inputStream, _outputStream, _keyStream);

                            _factoryCrypt = new FactoryCrypt();

                            _crypt.SetAlgorithm(_factoryCrypt.CreateObject(_parser.GetAlgorithmType()));

                            if (_parser.IsEncrypt())
                            {
                                _crypt.Encrypt();
                            }
                            else
                            {
                                _crypt.Decrypt();

                            }
                        } 
                    }
                    
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
          
        }

    }
}
