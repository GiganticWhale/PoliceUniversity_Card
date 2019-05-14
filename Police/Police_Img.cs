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

namespace Police
{
    public partial class Police_Img : Form
    {
        string mainImgUrl = "";
        public Police_Img( string imgUrl)
        {
            InitializeComponent();
            mainImgUrl = imgUrl;
          
                Image img = Image.FromFile(imgUrl);
                if (img != null)
                {
                    this.pictureBox1.Image = img;
                }
          
           
        }

        public bool urlHasUse() {
           
            if (Directory.Exists(mainImgUrl))//判断是否存在
        {
            return true;
        }
        else
        {
            return false;
        }

        }


        private void Police_Img_Load(object sender, EventArgs e)
        {
            skinEngine1.SkinFile = Application.StartupPath + @"\MP10.ssk";
        }

    
    }
}
