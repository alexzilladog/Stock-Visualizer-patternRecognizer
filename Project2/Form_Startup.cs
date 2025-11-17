using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project2
{
    public partial class Form_Startup : Form
    {
        public Form_Startup()
        {
            InitializeComponent();
        }

        private void button_LoadTickers_Click(object sender, EventArgs e) //when load tickers button is clicked
        {
            openFileDialog_selector.ShowDialog(); //show file dialog to select files
        }

        private void openFileDialog_selector_FileOk(object sender, CancelEventArgs e) //when files are selected and submitted
        {
            int numFiles = openFileDialog_selector.FileNames.Length; //get number of selected files
            DateTime startDate = dateTimePicker_Start.Value; //get start date from date picker
            DateTime endDate = dateTimePicker_End.Value; //get end date from date picker

            //go thru all files
            for (int i=0; i < numFiles; i++)
            {
                //create a new form with the file, start, and end dates as arguments (with new constructor)
                Form newStockForm = new Form_Stock_Visualizer(openFileDialog_selector.FileNames[i], startDate, endDate); //FileNames[i] gets the full path of each file

                newStockForm.Show(); //show the new form
            }
        }
    }
}
