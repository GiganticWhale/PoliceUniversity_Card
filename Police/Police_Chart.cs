using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Police
{

    public partial class Police_Chart : Form
    {
        string mainType = "";
        public Police_Chart(string type)
        {
            InitializeComponent();
            mainType = type;
        }

        private void Police_Chart_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“mainDataSet.police”中。您可以根据需要移动或删除它。
            // this.policeTableAdapter.Fill(this.mainDataSet.police);

            skinEngine1.SkinFile = Application.StartupPath + @"\MP10.ssk";


            OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=police.mdb");//建立数据库连接  
            OleDbCommand cmd = new OleDbCommand("select * from police where chart_type = '" + mainType + "'", conn);//执行数据连接  
            DataSet ds = new DataSet();
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            da.Fill(ds);

            this.dataGridView1.DataSource = ds.Tables[0];//数据源  
            this.dataGridView1.AutoGenerateColumns = false;//不自动  
            conn.Close();//关闭数据库连接  
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Police_Add police_Add = new Police_Add(mainType, this.dataGridView1);
            police_Add.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string userId = this.dataGridView1.SelectedCells[0].Value.ToString();
            OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=\"police.mdb\"");//建立数据库连接  

            try
            {
                bool DeleteResult = DataDelete(dataGridView1, conn);
                if (DeleteResult)
                {
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                conn.Close();
            }
        }



        #region 删除
        public bool DataDelete(DataGridView dataGridView1, OleDbConnection conn)
        {
            bool result = false;
            DialogResult dr = MessageBox.Show("确定要删除这条记录吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                //int iCount = dataGridView1.SelectedRows.Count;
                Int32 selectedRowCount = dataGridView1.Rows.GetRowCount(DataGridViewElementStates.Selected);
                conn.Open();
                //if (iCount < 1)
                if (selectedRowCount < 1)
                {
                    MessageBox.Show("删除失败！");
                }
                else
                {
                    for (int i = dataGridView1.Rows.Count - 1; i >= 0; i--)
                    {
                        if (dataGridView1.Rows[i].Selected)
                        {
                            string sqlDel = "delete * from police where id=@id";
                            OleDbCommand comd = new OleDbCommand(sqlDel, conn);
                            comd.Parameters.AddWithValue("@id", dataGridView1.Rows[i].Cells[0].Value);
                            int rtnCode = comd.ExecuteNonQuery();
                            dataGridView1.Rows.Remove(dataGridView1.Rows[i]);
                        }

                    }
                }
                result = true;
            }
            return result;
        }
        #endregion

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
              if (e.RowIndex >= 0)
            {
                 DataGridViewColumn column = dataGridView1.Columns[e.ColumnIndex];
                 if (column is DataGridViewButtonColumn)
                 {
                     //这里可以编写你需要的任意关于按钮事件的操作~
                     string info = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                     if (info.Length > 0) {
                         System.Diagnostics.Process.Start("ExpLore", info);

                         //Police_Img policeImg = new Police_Img(info);
                         //policeImg.Show();
                     }
                 }
         　
             }
        }
    }
}
