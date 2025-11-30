using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    public class Recognizer_Harami : Recognizer
    {
        public Recognizer_Harami() : base("Harami", 2) //constructs with Harami name and size 2
        {
        }
        public override bool Recognize(List<SmartCandlestick> givenCandlesticks) //override Recognize abstract method
        {
            if (givenCandlesticks.Count != 2) return false; // ensure exactly two candlesticks are passed
            SmartCandlestick firstCandle = givenCandlesticks[0]; // first candlestick
            SmartCandlestick secondCandle = givenCandlesticks[1]; // second candlestick
            // Check for Bullish Harami
            if (firstCandle.isBearish && secondCandle.isBullish) // first candle bearish, second bullish
            {
                if (secondCandle.open > firstCandle.close && secondCandle.close < firstCandle.open) // second candle body is within first candle body
                {
                    //patternIndices.Add(0); // add index 0 to patternIndices if bullish harami is found
                    return true;
                }
            }
            // Check for Bearish Harami
            if (firstCandle.isBullish && secondCandle.isBearish) // first candle bullish, second bearish
            {
                if (secondCandle.open < firstCandle.close && secondCandle.close > firstCandle.open) // second candle body is within first candle body
                {
                    //patternIndices.Add(0); // add index 0 to patternIndices if bearish harami is found
                    return true;
                }
            }
            return false; // no harami pattern found
        }
    }
}
