using PE2A_WF_Student.Models;
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

namespace PE2A_WF_Student
{
    public partial class HistoryForm : Form
    {
        private List<History> histories = new List<History>();
        public HistoryForm()
        {
            InitializeComponent();
            ShowHistory();
        }
        public void ShowHistory()
        {
            try
            {
                String[] readAllLines = File.ReadAllLines(Util.ExecutablePath() + @"\csvFile.csv", Encoding.UTF8);
                if (readAllLines[1] != null)
                {
                    for (int i = 1; i < readAllLines.Length; i++)
                    {
                        String[] split = readAllLines[i].Split(',');
                        var historyObj = new History()
                        {
                            No = int.Parse(split[0]),
                            StudentCode = split[1],
                            PracticalName = split[2],
                            Point = split[3],
                            PracticalDate = split[4]
                        };
                        histories.Add(historyObj);
                    }
                    this.dgvHistory.DataSource = histories;
                }
            }
            catch (Exception ex)
            {
                Util.LogException("ShowHistory", ex.Message);
            }



        }

    }
}
