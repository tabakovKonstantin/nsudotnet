using System;
using System.IO;

namespace LinesCounter
{
    class Counter
    {
        private int _count;

        public int CountLines(string[] filesPath)
        {
            _count = 0;
            foreach (string filePath in filesPath)
            {
                using (StreamReader streamReader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        if (line.Contains("//"))
                        {
                            if (line.StartsWith("//"))
                            {
                                continue;
                            }
                            else
                            {
                                _count++;
                                //Console.WriteLine(line);
                            }
                        }
                        else
                        {

                            if (line.Contains("/*"))
                            {
                                if (line.StartsWith("/*"))
                                {
                                    while (true)
                                    {
                                        line = streamReader.ReadLine();
                                        if (line == null || line.Contains("*/"))
                                        {
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    _count++;
                                    //Console.WriteLine(line);

                                    while (true)
                                    {
                                        line = streamReader.ReadLine();
                                        if (line == null || line.Contains("*/"))
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                            if (line != null)
                            {
                                if (!line.Contains("*/"))
                                {
                                    _count++;
                                    //Console.WriteLine(line);
                                }
                            }
                        }

                    }
                }
            }
            return _count;
        }
    }
}
