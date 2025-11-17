using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    public class aCandlestick
    {
        
        public string ticker { get; set; }
        public string period { get; set; }

        public DateTime date { get; set; } //get allows you to read data in var, 
        public decimal open { get; set; } //set allow you to set data in the var
        public decimal close { get; set; }
        public decimal high { get; set; }
        public decimal low { get; set; }
        public double volume { get; set; }


        public static char[] delimeters = { ',', '"' }; //delimiters available only to instance that calls them 
        //default constructor
        public aCandlestick() { }

        public aCandlestick(string ticker, string period, DateTime date, decimal open, decimal close, decimal high, decimal low, double volume)
        {
            this.ticker = ticker;
            this.period = period;
            this.date = date; //sets all values to values passed in
            this.open = open;
            this.close = close;
            this.high = high;
            this.low = low;
            this.volume = volume;
        }

        //create a clone of a candlestick (copy constructor)
        public aCandlestick(aCandlestick otherCandlestick)
        {
            this.ticker = otherCandlestick.ticker;
            this.period = otherCandlestick.period;
            this.date = otherCandlestick.date; //sets all values to values passed in by clone
            this.open = otherCandlestick.open;
            this.close = otherCandlestick.close;
            this.high = otherCandlestick.high;
            this.low = otherCandlestick.low;
            this.volume = otherCandlestick.volume;
        }

        public aCandlestick(String line)
        {
            CultureInfo provider = CultureInfo.InvariantCulture; //ensures . is used for decimal point
            String[] strings = line.Split(delimeters, StringSplitOptions.RemoveEmptyEntries); //split each line into pieces based on delimiters

            // date of candlestick
            String ticker = strings[0]; //first item in array is ticker
            String period = strings[1]; //second item in array is period

            String datestring = strings[2]; //third item in array is date

            date = DateTime.ParseExact(datestring, "yyyy-MM-dd", provider); //convert string to date
            open = Decimal.Parse(strings[3], provider); //second item in array is open price
            high = Decimal.Parse(strings[4], provider); //third item in array is high price
            low = Decimal.Parse(strings[5], provider); //fourth item in array is low price
            close = Decimal.Parse(strings[6], provider); //fifth item in array is close price
            volume = Double.Parse(strings[7], provider); //sixth item in array is volume
            //there is a seventh item in the csv, sector, but we don't need it
        }


    }
}