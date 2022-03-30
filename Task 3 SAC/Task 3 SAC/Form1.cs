using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task_3_SAC
{
    public partial class Form1 : Form
    {
        List<Sale> sales = new List<Sale>();
        string filter = "";
        BindingSource bs = new BindingSource();
        public Form1()
        {
            InitializeComponent();
            LoadCSV();
            bs.DataSource = sales;
            dgvSales.DataSource = bs;
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void LoadCSV()
        {
            // Locate the CSV file
            string filePath = @"C:\Users\Hung\Documents\2022\Software Development\Task3_Shop_Data.csv";
            List<string> lines = new List<string>();
            lines = File.ReadAllLines(filePath).ToList();
            foreach (string line in lines)
            {
                // Create an empty object of the sale type
                List<string> textbooks = line.Split(',').ToList();
                Sale s = new Sale();
                // Initialising all its properties
                s.Textbook = textbooks[0];
                s.Subject = textbooks[1];
                s.PurchasedPrice = double.Parse(textbooks[4]);
                s.SalePrice = textbooks[5];
                s.Rating = textbooks[6];
                // Adds the object to the sales list
                sales.Add(s); 
            }
        }

        private List<Sale> Search(string target, string filter)
        {
            target = target.ToLower();
            filter = filter.ToLower();

            List<Sale> results = new List<Sale>();
            foreach(Sale s in sales)
            {
                // Compares the rating property of the sale object
                if(filter == "rating")
                {
                    if (String.Equals(s.Rating, target))
                    {
                        results.Add(s);
                    }
                }
                // Compares the subject property of the sale object
                else if(filter == "subject")
                {
                    if (s.Subject.ToLower().Contains(target))
                    {
                        results.Add(s);
                    }
                }
                // Compares the textbook property of the sale object
                else if(filter == "textbook")
                {
                    if (s.Textbook.ToLower().Contains(target))
                    {
                        results.Add(s);
                    }
                }
                
                else
                {
                    // error
                }
            }
            return results;
        }

        private void comFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            filter = comFilter.Text;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            // Stores the result returned from the search function in the list
            List<Sale> r = Search(txtSearch.Text, filter);
            bs.DataSource = r;
            dgvSales.DataSource = r;
            // Set the reset binding to false so the data type does not change
            bs.ResetBindings(false);

        }
    }
}
