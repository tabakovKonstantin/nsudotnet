using System;
using System.IO;
using System.Text;

namespace Enigma
{
    class Parser
    {
        private string _pathToInputFile;
        private string _pathToOutputFile;
        private string _pathToKeyFile;

        private bool _encrypt;

        private AlgorithmType _algorithmType;

        private string _extentionKeyFile = ".txt";

        public Parser(string[] args)
        {
            ParsParam(args);
        }

        private void ParsParam(string[] args)
        {

            switch (args[2])
            {
                case "aes":
                    {
                        _algorithmType = AlgorithmType.AES;
                        break;
                    }

                case "des":
                    {
                        _algorithmType = AlgorithmType.DES;
                        break;
                    }

                case "rc2":
                    {
                        _algorithmType = AlgorithmType.RC2;
                        break;
                    }
                case "rijndael":
                    {
                        _algorithmType = AlgorithmType.Rijndael;
                        break;
                    }
                default:
                    {
                        throw new Exception("The parameter " + args[2] + " is incorrect.");
                        break;
                    }
            }

            if (File.Exists(args[1]))
            {
                _pathToInputFile = args[1];
            }
            else
            {
                throw new Exception("File " + args[1] + " does not exist.");
            }

            switch (args[0])
            {
                case "encrypt":
                    {
                        _encrypt = true;

                        if (args.Length != 4)
                        {
                            throw new Exception("С параметрами поебень");
                        }

                        if (!File.Exists(args[3]))
                        {
                            _pathToOutputFile = args[3];
                        }
                        else
                        {
                            throw new Exception("File " + args[3] + " exist.");
                        }

                        string nameInputFile = Path.GetFileNameWithoutExtension(_pathToInputFile);
                        StringBuilder stringBuilder = new StringBuilder();
                        stringBuilder.Append(Path.GetPathRoot(_pathToInputFile));
                        stringBuilder.Append(nameInputFile);
                        stringBuilder.Append(".key");
                        stringBuilder.Append(_extentionKeyFile);

                        if (!File.Exists(stringBuilder.ToString()))
                        {
                            _pathToKeyFile = stringBuilder.ToString();
                        }
                        else
                        {
                            throw new Exception("File " + args[3] + " exist.");
                        }
                        
                        break;
                    }

                case "decrypt":
                    {
                        _encrypt = false;

                        if (args.Length != 5)
                        {
                            throw new Exception("С параметрами поебень");
                        }

                        if (!File.Exists(args[3]) || !File.Exists(args[4]))
                        {
                            _pathToKeyFile = args[3];
                            _pathToOutputFile = args[4];
                        }
                        else
                        {
                            throw new Exception("File " + args[3] + "or" + args[4] + " exist.");
                        }

                        break;
                    }

                default:
                    {
                        throw new Exception("The parameter " + args[0] + " is incorrect.");
                        break;
                    }
            }
        }

        public string GetPathInputFile()
        {
            return _pathToInputFile;
        }

        public string GetPathOutputFile()
        {
            return _pathToOutputFile;
        }

        public string GetPathKeyFile()
        {
            return _pathToKeyFile;
        }

        public AlgorithmType GetAlgorithmType()
        {
            return _algorithmType;
        }

        public bool IsEncrypt()
        {
            return _encrypt;
        }
    }
}
