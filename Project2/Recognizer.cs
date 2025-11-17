using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    public abstract class Recognizer
    {
        string patternName;
        int patternSize;

        public Recognizer(string patternName, int patternSize)
        {
            this.patternName = patternName;
            this.patternSize = patternSize;
        }

        public Recognizer()
        {
            this.patternName = "DefaultPattern";
            this.patternSize = 1;
        }

        public abstract bool recognizer(List<SmartCandlestick> givenCandlesticks);

        public abstract bool recognizer();

        public abstract bool recognizer(string patternName, int patternSize);
    }
}
