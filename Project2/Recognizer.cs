//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Project2
//{
//    public abstract class Recognizer
//    {
//        string patternName;
//        int patternSize;
//        public List<int> patternIndices = new List<int>();

//        public Recognizer(string patternName, int patternSize)
//        {
//            this.patternName = patternName;
//            this.patternSize = patternSize;
//        }

//        public bool Recognizer(List<SmartCandlestick> givenCandlesticks)
//        {

//            this.patternName = "DefaultPattern";
//            this.patternSize = 1;
//        }

//        //public abstract bool recognizer(List<SmartCandlestick> givenCandlesticks);

//        //public abstract bool recognizer();

//        //public abstract bool recognizer(string patternName, int patternSize);

//        public List<int> patternIndices = new List<int>();
//    }
//}



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
        int patternSize; //pattern size
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
