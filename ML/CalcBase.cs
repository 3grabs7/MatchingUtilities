using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public abstract class CalcBase
    {
        public CalcBase(string path, char separator)
        {
            TestDataFilePath = path;
            SeparatorChar = separator;
        }
        public string TestDataFilePath;
        public char SeparatorChar;
    }
}
