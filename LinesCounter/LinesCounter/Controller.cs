using System;
using System.IO;

namespace LinesCounter
{
    class Controller
    {
        private Parser _parser;
        private Counter _counter;

        private int _countAllLines = 0;

        public Controller(string[] args)
        {
            try
            {
                _parser = new Parser(args); 
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            } 

            _counter = new Counter();
            string[] filesPath = Directory.GetFiles(Directory.GetCurrentDirectory(), _parser.GetExtentionFile(), SearchOption.AllDirectories );
            _countAllLines += _counter.CountLines(filesPath);

            Console.WriteLine(_countAllLines);
        } 

    }
}
