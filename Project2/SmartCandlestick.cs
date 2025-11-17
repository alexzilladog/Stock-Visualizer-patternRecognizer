using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    public class SmartCandlestick : aCandlestick //derive SmartCandlestick from aCandlestick
    {
        public SmartCandlestick() : base() //default constructor
        {
            computeProperties(); //compute properties after initializing base class (will be default values, as it uses base default constructor)
        }

        public SmartCandlestick(DateTime date, decimal open, decimal high, decimal low, decimal close, double volume) : base("", "", date, open, close, high, low, volume) 
            //call base constructor with parameters ("", "" for ticker and period, which are not used)
        {
            computeProperties(); //compute properties after initializing base class
        }

        public SmartCandlestick(string Line): base(Line) //call base constructor with line parameter
        {
            computeProperties(); //compute properties after initializing base class

        }

        public SmartCandlestick(aCandlestick candle) :base(candle) //call base constructor with aCandlestick parameter
        {
            computeProperties(); //compute properties after initializing base class
        }

        public SmartCandlestick(SmartCandlestick candle) : base(candle) //call base constructor with SmartCandlestick parameter
        {
            isBullish = candle.isBullish; //copy all properties from the passed SmartCandlestick
            isBearish = candle.isBearish;
            isNeutral = candle.isNeutral;
            range = candle.range;
            bodyRange = candle.bodyRange;
            upperWickRange = candle.upperWickRange;
            lowerWickRange = candle.lowerWickRange;
            topOfBody = candle.topOfBody;
            bottomOfBody = candle.bottomOfBody;
        }

        public void computeProperties() //find the properties of the candlestick
        {
            range = high - low; //total range of candlestick
            bodyRange = Math.Abs(close - open); //range of body (abs so that it's always positive)
            topOfBody = Math.Max(open, close); //top of body
            bottomOfBody = Math.Min(open, close); //bottom of body
            upperWickRange = high - topOfBody; //range of upper wick (above body)
            lowerWickRange = bottomOfBody - low; //range of lower wick (below body)
            
            
            isBullish = close > open; //is bullish if close is greater than open
            isBearish = open > close; //is bearish if open is greater than close
            isNeutral = open == close; //is neutral if open equals close
        }


        //bool values (default false)
        public Boolean isBullish; 
        public Boolean isBearish;
        public Boolean isNeutral;

        //decimal values (default 0)
        public Decimal range;
        public Decimal bodyRange;
        public Decimal upperWickRange;
        public Decimal lowerWickRange;
        public Decimal topOfBody;
        public Decimal bottomOfBody;
    }
}
