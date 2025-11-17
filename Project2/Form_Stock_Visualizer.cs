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
        int simulationIndex = 0; //index for timer simulation
        public Form_Stock_Visualizer()
        {
            InitializeComponent();
        }
        public Form_Stock_Visualizer(string tickerFile, DateTime startDate, DateTime endDate) : this() //new form constructor that takes file path, start date, and end date
        {
           
            Text = Path.GetFileName(tickerFile); //changes window name to name of selected file without showing entire absolute path
            
            list_Candlesticks = loadTicker(tickerFile);// load the candlesticks from the passed file
            // set date pickers (guarded by try/catch in case start/end are out of control)
            dateTimePicker_start.Value = startDate;
            dateTimePicker_end.Value = endDate;

            refreshDisplay(); //call refresh display to actually show the data

        }

        private void button_fileSelect_Click(object sender, EventArgs e) //if the file select button is clicked, this function will run
        {
            openFileDialog_selector.ShowDialog(); // Show the dialog, save the result (ok or cancel), in dr
        }

        private void button_simulate_Click(object sender, EventArgs e) //if the simulate button is clicked, this function will run
        {
            refreshDisplay(); //refresh the display to ensure the filtered list is up to date
            list_SimulatedCandlesticks.Clear(); //clear the simulated list
            simulationIndex = 0; //reset the index
            chart_OHLCV.DataSource = list_SimulatedCandlesticks; //bind the simulated list to the chart
            timer_Simulation.Start(); //start the timer

        }

        private void hScrollBar_Speed_Scroll(object sender, ScrollEventArgs e) //if the scroll bar is moved, this function will run
        {
            int Speed = Math.Max(1, 1000 - hScrollBar_Speed.Value); //invert speed so higher value is faster
            textBox_Speed.Text = Speed.ToString(); //update the speed text box when the scroll bar is moved
        }
        private void textBox_Speed_TextChanged(object sender, EventArgs e) //if the text box is changed, update the speed
        {
            timer_Simulation.Interval = int.Parse(textBox_Speed.Text); //update the timer interval when the text box is changed
        }

        private void timer_Simulation_Tick(object sender, EventArgs e) //if the timer ticks, this function will run
        {
            list_SimulatedCandlesticks.Add(list_FilteredCandlesticks[simulationIndex]); //add the next candlestick to the simulated list
            normalizeChart(list_SimulatedCandlesticks); //normalize the chart data
            displayStock(list_SimulatedCandlesticks); //display the simulated list
            simulationIndex++; //increment the index
            if (simulationIndex >= list_FilteredCandlesticks.Count) //if the index is greater than or equal to the number of filtered candlesticks
            {
                timer_Simulation.Stop(); //stop the timer
            }
        }

        private void button_refresh_Click(object sender, EventArgs e) //if the refresh button is clicked, this function will run
        {
            refreshDisplay(); //call function to refresh the display
        }

        private void openFileDialog_selector_fileOK(object sender, CancelEventArgs e) //if a file is selected, this function will run, and call other necessary functions
        {   //https://learn.microsoft.com/en-us/dotnet/api/system.io.path?view=net-9.0
            Text = Path.GetFileName(openFileDialog_selector.FileName); //changes window name to name of selected file without showing entire absolute path
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

        void filterCandlesticksByDate() //void function to call the filter function and save the result to the global variable
        {
            list_FilteredCandlesticks = filterCandlesticksByDate(list_Candlesticks, dateTimePicker_start.Value, dateTimePicker_end.Value); //filter the list of candlesticks by the dates selected in the date pickers
        }
        List<SmartCandlestick> filterCandlesticksByDate(List<SmartCandlestick> listOfCandlesticks, DateTime startDate, DateTime endDate) //function to filter the list of candlesticks by date
        {
            return listOfCandlesticks.Where(c => c.date >= startDate && c.date <= endDate).ToList(); //LINQ (similar to sql) query to only load candlesticks where date is between start and end date
        }

        void normalizeChart() //void function to call the normalize function
        {
            normalizeChart(list_FilteredCandlesticks); //normalize the chart data
        }
        void normalizeChart(List<SmartCandlestick> list_Candlesticks) //function to use the y axis chart to its full potential
        {
            if (list_Candlesticks.Count == 0) return; //if the list is empty, return
            decimal minPrice = list_Candlesticks.Min(c => c.low); //find the lowest price in the list of candlesticks
            decimal maxPrice = list_Candlesticks.Max(c => c.high); //find the highest price in the list of candlesticks
            chart_OHLCV.ChartAreas[0].AxisY.Minimum = (double)minPrice * 0.98; //set the minimum value of the y-axis to 98% of the lowest price
            chart_OHLCV.ChartAreas[0].AxisY.Maximum = (double)maxPrice * 1.02; //set the maximum value of the y-axis to 102% of the highest price
        }

        void displayStock() //void function to call the display function
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
            chart_OHLCV.Titles.Add(Text + " From Dates: " + dateTimePicker_start.Value + " -- " + dateTimePicker_end.Value); //changes window name to name of selected file 

            // https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.datavisualization.charting.title?view=netframework-4.8.1
            chart_OHLCV.Titles.Add(new Title("Volume", Docking.Bottom)); // Add "Volume" title at the bottom :] ((using the string, docking constructor)) 
            this.TopMost = true; //bring the form to the front after refresh
        }
    }
}
