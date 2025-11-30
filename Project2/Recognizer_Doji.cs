using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    public class Recognizer_Doji : Recognizer
    {
        public Recognizer_Doji() : base("Doji", 1) //constructs with Doji name and size 1
        {

        }

        public override bool Recognize(List<SmartCandlestick> givenCandlesticks) //override Recognize abstract method
        {
            if (givenCandlesticks.Count == 1) // ensure exactly one candlestick is passed
            {
                decimal bodyToRangeRatio = givenCandlesticks[0].bodyRange / givenCandlesticks[0].range; //calculate body to range ratio
                if (bodyToRangeRatio <= 0.1m) //if body to range ratio is less than or equal to 10%
                {
                    //patternIndices.Add( //unimplemented
                    return true;
                }
            }
            return false; //if no doji pattern found do nothing
        }
    }
}