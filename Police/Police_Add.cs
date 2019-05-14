using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace Police
{
    public partial class Police_Add : Form
    {
        string main_type = "";
        DataGridView mainChartView;
        string imgUrl = "";
        public Police_Add(string type,DataGridView chartView)
        {
            InitializeComponent();
            main_type = type;
            mainChartView = chartView;
        }

 
     

        private void button2_Click(object sender, EventArgs e)
        {
            // 连接数据库
            OleDbConnection sqlcon = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=police.mdb");//建立数据库连接  
            sqlcon.Open();

            string sql = "INSERT INTO police (name, [number], create_time, validity_time, photo, beizhu, chart_type)VALUES (@name,@number,@create_time,@validity_time,@photo,@beizhu,@chart_type)";

            OleDbCommand com = new OleDbCommand();

            com = sqlcon.CreateCommand();
            com.CommandText = sql;
            //配参

            string name = nameText.Text.ToString();
            string number = numberText.Text.ToString();
            string createTime = createTimePicker.Text.ToString();
            string validityTime = youxiaoqiTimePicker.Text.ToString();
            string photo = imgUrl.ToString();
            string beizhu = beizhuText.Text.ToString();


            com.Parameters.Add(new OleDbParameter("@name", name));
            com.Parameters.Add(new OleDbParameter("@number", number));
            com.Parameters.Add(new OleDbParameter("@create_time", createTime));
            com.Parameters.Add(new OleDbParameter("@validity_time", validityTime));

            com.Parameters.Add(new OleDbParameter("@photo", photo));
            com.Parameters.Add(new OleDbParameter("@beizhu", beizhu));
            com.Parameters.Add(new OleDbParameter("@chart_type", main_type));

            com.ExecuteNonQuery();
            sqlcon.Close();

            MessageBox.Show("内容添加成功");
            reloadCHartView();
        }

        private void uploaderButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            fileDialog.Title = "请选择文件";
            fileDialog.Filter = "图像文件 (*.bmp;*.ico;*.gif;*.jpeg;*.jpg;*.png)|*.bmp;*.ico;*.gif;*.jpeg;*.jpg;*.png"; //设置要选择的文件的类型
            fileDialog.Multiselect = true;//多选设置
            var result = new List<string>();
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                string file = fileDialog.FileName;//返回文件的完整路径  

                string targetPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Path.GetFileName(file));
                bool isrewrite = true;//是否覆盖已存在的同名文件
                File.Copy(file, targetPath, isrewrite);
                imgUrl = file;
                uploaderText.Text = imgUrl;
            }
        }



        // 刷新
        private void reloadCHartView (){
            OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=police.mdb");//建立数据库连接  
            OleDbCommand cmd = new OleDbCommand("select * from police where chart_type = '" + main_type + "'", conn);//执行数据连接  
            DataSet ds = new DataSet();
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            da.Fill(ds);

            mainChartView.DataSource = ds.Tables[0];//数据源  
            mainChartView.AutoGenerateColumns = false;//不自动  
            conn.Close();//关闭数据库连接  
        }

        private void Police_Add_Load_1(object sender, EventArgs e)
        {
            skinEngine1.SkinFile = Application.StartupPath + @"\MP10.ssk";

        }







        #region 上传
  
        #endregion
    }
}
