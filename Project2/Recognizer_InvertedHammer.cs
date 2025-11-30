using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    public class Recognizer_InvertedHammer : Recognizer
    {
        public Recognizer_InvertedHammer() : base("Inverted Hammer", 1) //constructs with inverted hammer name and size 1
        {
        }

        public override bool Recognize(List<SmartCandlestick> givenCandlesticks) //override Recognize abstract method
        {
            if (givenCandlesticks.Count != 1) return false; // ensure exactly one candlestick is passed

            SmartCandlestick candlestick = givenCandlesticks[0]; // get the single candlestick as candlestick

            // guard against invalid candles
            if (candlestick.range <= 0 || candlestick.bodyRange <= 0) return false;

            // ratios and thresholds
            decimal bodyToRange = candlestick.bodyRange / candlestick.range; // body relative to total range
            decimal upperWickToBody = candlestick.upperWickRange / candlestick.bodyRange; // lower wick relative to body
            const decimal MaxBodyToRange = 0.30m;     // body <= 30% of range
            const decimal MinUpperWickToBody = 2.0m;  // lower wick >= 2x body
            const decimal MaxLowerWickToRange = 0.10m; // upper wick <= 10% of range

            // require long lower wick, small body and small upper wick
            bool longUpperWick = upperWickToBody >= MinUpperWickToBody; // long upper wick
            bool smallBody = bodyToRange <= MaxBodyToRange; // bodytorange  ratio is small
            bool smallLowerWick = candlestick.lowerWickRange <= (candlestick.range * MaxLowerWickToRange); // small lower wick has lower wick range less than 10% of total range

            if (smallBody && longUpperWick && smallLowerWick)
            {
                // Typical hammer is bullish reversal, but the candle can be bullish or bearish.
                // If you want to accept only bullish hammers, add: if (!candlestick.isBullish) return false;
                return true;
            }

            return false; //if no inverted hammer pattern found do nothing
        } 
    }
}