using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Police
{
    public partial class Police_Main : Form
    {
        public Police_Main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Police_Chart policeChart = new Police_Chart("1");
            policeChart.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Police_Chart policeChart = new Police_Chart("2");
            policeChart.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Police_Chart policeChart = new Police_Chart("3");
            policeChart.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Police_Chart policeChart = new Police_Chart("4");
            policeChart.Show();
        }

        private void Police_Main_Load(object sender, EventArgs e)
        {

            skinEngine1.SkinFile = Application.StartupPath + @"\MP10.ssk";

        }
    }
}
