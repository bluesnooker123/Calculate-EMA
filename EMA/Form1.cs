using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace EMA
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            string targetDirectory = "../Output/Original";
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string filePath in fileEntries)
                ProcessFile(filePath);

            //string fileName = "../Output/Original/CSV-WES.csv";
            //ProcessFile(fileName);
        }

        private void ProcessFile(string filePath)
        {
            string fileName = filePath.Substring(filePath.IndexOf('\\') + 1);
            string out_path = "../Output/Adjusted/" + fileName;
                        
            string[] org_lines = System.IO.File.ReadAllLines(filePath);

            StringBuilder intermediate_result_1 = Remove_0_blank(org_lines);

            string seperater = "\r\n";
            string[] temp_stringArray = intermediate_result_1.ToString().Split(seperater.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToArray();
            StringBuilder intermediate_result_2 = Remove_duplicated_weekend_val(temp_stringArray);



            System.IO.File.WriteAllText(out_path, intermediate_result_2.ToString());


            MessageBox.Show("Save to \"" + out_path + "\" successfully!", "success");

        }
        private bool check_exist_0_blank(string line)
        {
            string[] columns = line.Split(',');
            foreach (string column in columns)
            {
                if (string.Compare(column,"0") == 0)
                    return false;
                if (string.Compare(column, "") == 0)
                    return false;
            }
            return true;
        }
        private StringBuilder Remove_0_blank(string[] lines)
        {
            StringBuilder result = new StringBuilder();
            foreach (string line in lines)
            {
                bool flag = check_exist_0_blank(line);
                if (flag)
                    result.AppendLine(line);
            }
            return result;
        }

        private StringBuilder Remove_duplicated_weekend_val(string[] stringArray)
        {
            StringBuilder result = new StringBuilder();
            var lines = new HashSet<int>();
            foreach (string line in stringArray)
            {
                string[] temp_arr = line.Split(',').ToArray();
                int hc = temp_arr[1].GetHashCode();
                if (lines.Contains(hc))
                    continue;

                if (string.Compare(temp_arr[1],"Date")!=0)
                {
                    DateTime date = Convert.ToDateTime(temp_arr[1]);
                    DayOfWeek day = date.DayOfWeek;
                    if ((day == DayOfWeek.Saturday) || (day == DayOfWeek.Sunday))
                        continue;
                }

                lines.Add(hc);
                result.AppendLine(line);
            }
            return result;
        }
    }
}
