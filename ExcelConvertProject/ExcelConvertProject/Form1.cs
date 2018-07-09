using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExcelConvertProject.Scripts.Excel;

namespace ExcelConvertProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {            
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = System.Environment.CurrentDirectory;
            openFileDialog.Filter = "excel files (*.xlsx)|*.xlsx";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //System.IO.StreamReader sr = new
                //   System.IO.StreamReader(openFileDialog.FileName);
                //MessageBox.Show(sr.ReadToEnd());
                //sr.Close();            
                FileTextBox.Text = openFileDialog.FileName;
                ExcelManager.Instance.ReadExcel(openFileDialog.FileName);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //SaveFileDialog saveFileDialog = new SaveFileDialog();

            //saveFileDialog.InitialDirectory = "c:\\";
            //saveFileDialog.Filter = "text files (*.txt)|*.txt";
            //saveFileDialog.FilterIndex = 2;
            //saveFileDialog.RestoreDirectory = true;

            //if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{   
            //    ExcelManager.Instance.SaveByte(saveFileDialog.FileName);
            //}

            ExcelManager.Instance.CreateFile();
        }
    }
}
