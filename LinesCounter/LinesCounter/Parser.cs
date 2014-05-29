using System;

namespace LinesCounter
{
    class Parser
    {
        private string _extentionFile;

        public Parser(string[] args)
        {

            parsParam(args);
        }

        private void parsParam(string[] args)
        {
            if (args.Length > 1)
            {
                throw new Exception("дохуя");
            }

            _extentionFile = args[0];

        }

        public string GetExtentionFile()
        {
            return _extentionFile;
        }
    }
}
