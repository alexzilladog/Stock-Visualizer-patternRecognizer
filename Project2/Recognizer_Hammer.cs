using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    public class Recognizer_Hammer : Recognizer
    {
        public Recognizer_Hammer() : base("Hammer", 1) //constructor with pattern name and size
        {
        }

        public override bool Recognize(List<SmartCandlestick> givenCandlesticks) // override Recognize abstract method
        {
            if (givenCandlesticks.Count != 1) return false; // ensure exactly one candlestick is passed

            SmartCandlestick candlestick = givenCandlesticks[0]; // get the single candlestick

            // guard against invalid candles
            if (candlestick.range <= 0 || candlestick.bodyRange <= 0) return false; // prevent division by zero

            // ratios and thresholds
            decimal bodyToRange = candlestick.bodyRange / candlestick.range; // body relative to total range
            decimal lowerWickToBody = candlestick.lowerWickRange / candlestick.bodyRange; // lower wick relative to body
            const decimal MaxBodyToRange = 0.30m; // body <= 30% of range
            const decimal MinLowerWickToBody = 2.0m; // lower wick >= 2x body
            const decimal MaxUpperWickToRange = 0.10m; // upper wick <= 10% of range

            // require long lower wick, small body and small upper wick
            bool longLowerWick = lowerWickToBody >= MinLowerWickToBody; // long lower wick
            bool smallBody = bodyToRange <= MaxBodyToRange; // bodytorange  ratio is small
            bool smallUpperWick = candlestick.upperWickRange <= (candlestick.range * MaxUpperWickToRange); // small upper wick has upper wick range less than 10% of total range

            if (smallBody && longLowerWick && smallUpperWick)
            {
                // Typical hammer is bullish reversal, but the candle can be bullish or bearish.
                // If you want to accept only bullish hammers, add: if (!candlestick.isBullish) return false;
                return true;
            }

            return false;
        }
    }
}
