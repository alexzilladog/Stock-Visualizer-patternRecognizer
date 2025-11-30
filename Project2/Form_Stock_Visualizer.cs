using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Project2
{
    public partial class Form_Stock_Visualizer : Form
    {
        private List<SmartCandlestick> list_Candlesticks = new List<SmartCandlestick>(); //set a "global" variable for a List of candlesticks (necessry for refresh display)
        private List<SmartCandlestick> list_FilteredCandlesticks = new List<SmartCandlestick>(); //set a "global" variable for a List of filtered candlesticks (necessry for refresh display)
        private List<SmartCandlestick> list_SimulatedCandlesticks = new List<SmartCandlestick>(); //set a "global" variable for a List of simulated candlesticks (necessry for timer to work)
        private List<SmartCandlestick> list_Doji = new List<SmartCandlestick>(); //list of doji candlesticks
        private List<SmartCandlestick> list_Hammer = new List<SmartCandlestick>(); //list of hammer candlesticks
        private List<SmartCandlestick> list_InvertedHammer = new List<SmartCandlestick>(); //list of inverted hammer candlesticks
        private List<SmartCandlestick> list_Marubozu = new List<SmartCandlestick>(); //list of marubozu candlesticks
        private List<SmartCandlestick> list_Engulfing = new List<SmartCandlestick>(); //list of engulfing candlesticks
        private List<SmartCandlestick> list_Harami = new List<SmartCandlestick>(); //list of harami candlesticks

        List<Recognizer> list_Recognizers = new List<Recognizer>(); //list of pattern recognizers
        
        int simulationIndex = 0; //index for timer simulation
        public Form_Stock_Visualizer()
        {
            InitializeComponent();
        }
        public Form_Stock_Visualizer(string tickerFile, DateTime startDate, DateTime endDate) : this() //new form constructor that takes file path, start date, and end date
        {
           
            Text = Path.GetFileName(tickerFile); //changes currentCandlestickname to name of selected file without showing entire absolute path
            
            list_Candlesticks = loadTicker(tickerFile);// load the candlesticks from the passed file
            // set date pickers (guarded by try/catch in case start/end are out of control)
            dateTimePicker_start.Value = startDate;
            dateTimePicker_end.Value = endDate;

            refreshDisplay(); //call refresh display to actually show the data

        }

        void initializeRecognizers()
        {
             //add engulfing recognizer
            list_Recognizers.Add(new Recognizer_Doji()); //add doji recognizer
            list_Recognizers.Add(new Recognizer_Hammer()); //add hammer recognizer
            list_Recognizers.Add(new Recognizer_InvertedHammer()); //add inverted hammer recognizer
            list_Recognizers.Add(new Recognizer_Marubozu()); //add shooting star recognizer
            list_Recognizers.Add(new Recognizer_Engulfing()); //add engulfing recognizer
            list_Recognizers.Add(new Recognizer_Harami()); //add harami recognizer
            
            //clear previous pattern lists
            list_Doji.Clear();
            list_Engulfing.Clear();
            list_Hammer.Clear();
            list_InvertedHammer.Clear();
            list_Marubozu.Clear();
            list_Harami.Clear();


        }

        //button functions below
        private void button_fileSelect_Click(object sender, EventArgs e) //if the file select button is clicked, this function will run
        {
            openFileDialog_selector.ShowDialog(); // Show the dialog, save the result (ok or cancel), in dr
        }

        private void button_simulate_Click(object sender, EventArgs e) //if the simulate button is clicked, this function will run
        {
            refreshDisplay(); //refresh the display to ensure the filtered list is up to date
            list_SimulatedCandlesticks.Clear(); //clear the simulated list
            initializeRecognizers(); //initialize the recognizers
            simulationIndex = 0; //reset the index
            chart_OHLCV.DataSource = list_SimulatedCandlesticks; //bind the simulated list to the chart
            
            timer_Simulation.Start(); //start the timer

        }

        private void button_refresh_Click(object sender, EventArgs e) //if the refresh button is clicked, this function will run
        {
            refreshDisplay(); //call function to refresh the display
        }

        private void hScrollBar_Speed_Scroll(object sender, ScrollEventArgs e) //if the scroll bar is moved, this function will run
        {
            int Speed = Math.Max(1, 2000 - hScrollBar_Speed.Value); //invert speed so higher value is faster
            textBox_Speed.Text = Speed.ToString(); //update the speed text box when the scroll bar is moved
        }
        private void textBox_Speed_TextChanged(object sender, EventArgs e) //if the text box is changed, update the speed
        {

            try
            {
                int textBoxValue = int.Parse(textBox_Speed.Text); //get the value from the text box
                if (textBoxValue > 0) //if the value is greater than 0
                {  //ensure the value is at least 1
                    timer_Simulation.Interval = textBoxValue; //update the timer interval when the text box is changed
                    hScrollBar_Speed.Value = Math.Max(1, 2000 - textBoxValue); //update the scroll bar value when the text box is changed
                }
            }
            catch
            {
                return; //if the text box is not a valid integer, do nothing
            }
        }

        private void timer_Simulation_Tick(object sender, EventArgs e) //if the timer ticks, this function will run
        {
            list_SimulatedCandlesticks.Add(list_FilteredCandlesticks[simulationIndex]); //add the next candlestick to the simulated list
            normalizeChart(list_SimulatedCandlesticks); //normalize the chart data
            displayStock(list_SimulatedCandlesticks); //display the simulated list
            for (int i = 0; i < list_Recognizers.Count; i++) {
                //list_Recognizers[i].recognize(list_SimulatedCandlesticks[simulationIndex]);
            }
            simulationIndex++; //increment the index
            for (int s = 0; s < list_Recognizers.Count; s++)
            { //for each recognizer in the list
                int needed = list_Recognizers[s].patternSize;
                // only attempt recognition when enough candles are available
                if (simulationIndex >= needed)
                {
                    int start = simulationIndex - needed; //calculate the start index for the current window of candlesticks
                    List<SmartCandlestick> currentCandlestick= list_FilteredCandlesticks.GetRange(start, needed); //get the current widnow of  (1 or 2) candlesticks as a list

                    if (list_Recognizers[s].Recognize(currentCandlestick)) //
                    { //if the recognizer recognizes the pattern in the current currentCandlestick
                        string name = list_Recognizers[s].patternName; //get the name of the pattern

                        if (name == "Doji") //if the pattern is doji
                        {
                            list_Doji.Add(currentCandlestick[needed - 1]); //add the current candlestick to the doji list
                        }
                        else if (name == "Hammer") //if the pattern is hammer
                        {
                            list_Hammer.Add(currentCandlestick[needed - 1]); //add the current candlestick to the hammer list
                        }
                        else if (name == "Inverted Hammer") //if the pattern is inverted hammer
                        {
                            list_InvertedHammer.Add(currentCandlestick[needed - 1]); //add the current candlestick to the inverted hammer list
                        }
                        else if (name == "Marubozu") //if the pattern is marubozu
                        {
                            list_Marubozu.Add(currentCandlestick[needed - 1]); //add the current candlestick to the marubozu list
                        }
                        else if (name == "Engulfing") //if the pattern is engulfing
                        {
                            // add current then previous (preserve previous behavior)
                            list_Engulfing.Add(currentCandlestick[needed - 1]);
                            list_Engulfing.Add(currentCandlestick[needed - 2]);
                        }
                        else if (name == "Harami")
                        {
                            // add current then previous (preserve previous behavior)
                            list_Harami.Add(currentCandlestick[needed - 1]);
                            list_Harami.Add(currentCandlestick[needed - 2]);
                        }
                    }
                }
            }
            if (simulationIndex >= list_FilteredCandlesticks.Count) //if the index is greater than or equal to the number of filtered candlesticks
            {
                timer_Simulation.Stop(); //stop the timer
                comboBox_selectPattern.Enabled = true; //enable the pattern selection combo box
            }
        }

        private void comboBox_selectPattern_SelectedIndexChanged(object sender, EventArgs e) //if the selected pattern is changed, this function will run
        {
            if (comboBox_selectPattern.SelectedItem.ToString() == "Doji") //if the selected pattern is doji
            {
                normalizeChart(list_Doji); //normalize the chart data
                displayStock(list_Doji); //display the doji list
            }
            else if (comboBox_selectPattern.SelectedItem.ToString() == "Hammer") //if the selected pattern is hammer
            {
                normalizeChart(list_Hammer); //normalize the chart data
                displayStock(list_Hammer); //display the hammer list
            }
            else if (comboBox_selectPattern.SelectedItem.ToString() == "Inverted Hammer") //if the selected pattern is inverted hammer
            {
                normalizeChart(list_InvertedHammer); //normalize the chart data
                displayStock(list_InvertedHammer); //display the inverted hammer list
            }
            else if (comboBox_selectPattern.SelectedItem.ToString() == "Marubozu") //if the selected pattern is marubozu
            {
                normalizeChart(list_Marubozu); //normalize the chart data
                displayStock(list_Marubozu); //display the marubozu list
            }
            else if (comboBox_selectPattern.SelectedItem.ToString() == "Engulfing") //if the selected pattern is engulfing
            {
                normalizeChart(list_Engulfing); //normalize the chart data
                displayStock(list_Engulfing); //display the engulfing list
            }
            else if (comboBox_selectPattern.SelectedItem.ToString() == "Harami") //if the selected pattern is harami
            {
                normalizeChart(list_Harami); //normalize the chart data
                displayStock(list_Harami); //display the harami list
            }
        }
        private void openFileDialog_selector_fileOK(object sender, CancelEventArgs e) //if a file is selected, this function will run, and call other necessary functions
        {   //https://learn.microsoft.com/en-us/dotnet/api/system.io.path?view=net-9.0
            Text = Path.GetFileName(openFileDialog_selector.FileName); //changes currentCandlestickname to name of selected file without showing entire absolute path
            loadTicker(); //call function to read the csv file
            refreshDisplay(); //call function to refresh the display
        }

        void loadTicker()
        {
            list_Candlesticks = loadTicker(openFileDialog_selector.FileName); //call function to read the csv file
        }

        private List<SmartCandlestick> loadTicker(String tickerFile) //function to read the csv file and return a list of candlesticks
        {
            var list_Candlesticks = new List<SmartCandlestick>(); //initialize the list of candlesticks
            using (StreamReader csvReader = new StreamReader(tickerFile)) //csvReader is an object of type streamreader, which reads the file passed from function
            {
                string text = csvReader.ReadToEnd(); //read the entire file
                string[] lines = text.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries); //remove blank lines

                for (int i = 1; i < lines.Length; i++) // start at line after header
                {
                    string line = lines[i].Trim(); //remove whitespace
                    SmartCandlestick candlestick = new SmartCandlestick(line); // send the whole line to the constructor of aCandlestick, which will seperate the attributes accordingly (by position)
                    list_Candlesticks.Add(candlestick); // add the new object to the list
                }
                button_Refresh.Enabled = true; //enable the refresh button now that candlesticks are loaded up
                if (list_Candlesticks.Count > 0 && list_Candlesticks[0].date > list_Candlesticks[list_Candlesticks.Count - 1].date) //check if the list is in descending order
                {
                    list_Candlesticks.Reverse(); //if it is, reverse the list to be in ascending order
                }
                else if (list_Candlesticks.Count == 0) //if no candlesticks are present 
                {
                    MessageBox.Show("Invalid Stock File Passed to loadTicker (empty list of candles)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); //show an error message
                    button_Refresh.Enabled = false; //disable refresh button if the list of candles is not loaded
                }
            }
            return list_Candlesticks; //return my list of ordered and instantiated aCandlestick objects
        }

        void filterCandlesticksByDate() //default function to call the filter function if no args are passed
        {
            list_FilteredCandlesticks = filterCandlesticksByDate(list_Candlesticks, dateTimePicker_start.Value, dateTimePicker_end.Value); //filter the list of candlesticks by the dates selected in the date pickers
        }
        List<SmartCandlestick> filterCandlesticksByDate(List<SmartCandlestick> listOfCandlesticks, DateTime startDate, DateTime endDate) //function to filter the list of candlesticks by date
        {
            return listOfCandlesticks.Where(candlestick => candlestick.date >= startDate && candlestick.date <= endDate).ToList(); //LINQ (similar to sql) query to only load candlesticks where date is between start and end date
        }

        void normalizeChart() //defualt function to call the normalize function if no args are passed
        {
            normalizeChart(list_FilteredCandlesticks); //normalize the chart data
        }
        void normalizeChart(List<SmartCandlestick> list_Candlesticks) //function to use the y axis chart to its full potential
        {
            if (list_Candlesticks.Count == 0) return; //if the list is empty, return
            decimal minPrice = list_Candlesticks.Min(candlestick => candlestick.low); //find the lowest price in the list of candlesticks
            decimal maxPrice = list_Candlesticks.Max(candlestick => candlestick.high); //find the highest price in the list of candlesticks
            chart_OHLCV.ChartAreas[0].AxisY.Minimum = (double)minPrice * 0.98; //set the minimum value of the y-axis to 98% of the lowest price
            chart_OHLCV.ChartAreas[0].AxisY.Maximum = (double)maxPrice * 1.02; //set the maximum value of the y-axis to 102% of the highest price
        }

        void displayStock() //default function to call the display function if no args are passed
        {
            displayStock(list_FilteredCandlesticks); //call function to display the stock data in the chart and data grid view
        }
        void displayStock(List<SmartCandlestick> list_Candlesticks) //function to display the stock data in the chart and data grid view
        {
            //initialize binding source
            chart_OHLCV.DataSource = list_Candlesticks; //bind the list of candlesticks to the chart
            chart_OHLCV.DataBind(); //bind the data to the chart
        }
        private void refreshDisplay() //refresh the display after the date or file is changed, now without loading the file again!
        {
            list_FilteredCandlesticks = filterCandlesticksByDate(list_Candlesticks, dateTimePicker_start.Value, dateTimePicker_end.Value); //filter the list of candlesticks by the dates selected in the date pickers
            normalizeChart(list_FilteredCandlesticks); //normalize the chart data
            chart_OHLCV.DataSource = list_FilteredCandlesticks; //bind the list of candlesticks to the chart
            displayStock(list_FilteredCandlesticks); //display the stock data in the chart and data grid view
                                                     //https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.datavisualization.charting.chart.titles?view=netframework-4.8.1
            chart_OHLCV.Titles.Clear(); //clear any previous titles (in case of using another csv file) NECESSARY TO AVOID MULTIPLE TITLES
            chart_OHLCV.Titles.Add(Text + " From Dates: " + dateTimePicker_start.Value + " -- " + dateTimePicker_end.Value); //changes currentCandlestickname to name of selected file 

            // https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.datavisualization.charting.title?view=netframework-4.8.1
            chart_OHLCV.Titles.Add(new Title("Volume", Docking.Bottom)); // Add "Volume" title at the bottom :] ((using the string, docking constructor)) 
            this.TopMost = true; //bring the form to the front after refresh
        }

        
    }
}
