using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    public class Recognizer_Marubozu : Recognizer
    {
        public Recognizer_Marubozu() : base("Marubozu", 1) //constructs with Marubozu name and size 1
        {
        }

        public override bool Recognize(List<SmartCandlestick> givenCandlesticks) //override Recognize abstract method
        {
            
            if (givenCandlesticks.Count == 1) //ensure exactly one candlestick is passed
            {
                SmartCandlestick candlestick = givenCandlesticks[0]; //candlestick used in function == candlestick passed in
                decimal tol = candlestick.range * 0.05m; //5% tolerance based on the range of the candlestick
                // Check for Bullish Marubozu
                if (candlestick.isBullish) //if the candlestick is bullish
                {
                    if (candlestick.open <= (candlestick.low + tol) && candlestick.close >= (candlestick.high - tol)) //if open is within tolerance of low and close is within tolerance of high
                    {
                        //patternIndices.Add(0); //add index 0 to patternIndices if the single candlestick is a bullish marubozu
                        return true; //return true if bullish marubozu is found
                    }
                }
                // Check for Bearish Marubozu
                if (candlestick.isBearish) //if the candlestick is bearish
                {
                    if (candlestick.open >= candlestick.high - tol  && candlestick.close  <= candlestick.low + tol) //if open is within tolerance of high and close is within tolerance of low
                    {
                        return true; //return true if bearish marubozu is found
                    }
                }
            }
            return false; //no harami pattern, do nothing
        }
    }
}
