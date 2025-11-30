using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    public abstract class Recognizer
    {
        public string patternName; //pattern name
        public int patternSize; //pattern size
        public List<int> patternIndices = new List<int>(); //list of pattern indices

        public Recognizer(string patternName, int patternSize) //constructor with pattern name and size
        {
            this.patternName = patternName; //set pattern name
            this.patternSize = patternSize; //set pattern size
        }

        // Implementations must provide recognition logic for a list of candlesticks.
        public abstract bool Recognize(List<SmartCandlestick> givenCandlesticks);

        //public abstract bool recognizer(List<SmartCandlestick> givenCandlesticks);

        //public abstract bool recognizer();

        //public abstract bool recognizer(string patternName, int patternSize);

        // (Removed duplicate declaration of patternIndices)
    }
}
