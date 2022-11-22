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

namespace WindowsFormsApp47
{
    public partial class Form1 : Form
    {
        string path = Directory.GetCurrentDirectory() + "/Файлы со штрафами/Штрафы.txt";

        public Form1()
        {
            InitializeComponent();
            Filling();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int ind = dataGridView1.CurrentRow.Index;
            dataGridView1[6, ind].Value = "true";
            Delete(ind);
        }

        public void Filling()
        {
            dataGridView1.Rows.Add();
            int count = File.ReadAllLines(path).Length;
            for(int i = 0; i < count-1; i++)
                dataGridView1.Rows.Add();

            string s = "";
            using (StreamReader sr = new StreamReader(path))
            {
                for (int i = 0; i < count; i++)
                {
                    s = Convert.ToString(sr.ReadLine());
                    string[] words = s.Split(';');
                    for(int j = 0; j < words.Length; j++)
                    {
                        dataGridView1[j, i].Value = words[j];
                    }
                }
            }
        }

        public void Delete(int ind)
        {
            bool prov = true;
            string idCar = dataGridView1[1, ind].Value.ToString();
            for(int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (dataGridView1[1, i].Value.ToString() == idCar)
                {
                    if (dataGridView1[6, i].Value.ToString() == "false")
                    {
                        prov = false;
                        break;
                    }
                }
            }
            if(prov)
            {
                for (int i = dataGridView1.RowCount - 1; i >= 0; i--)
                {
                    if (dataGridView1[1, i].Value.ToString() == idCar)
                    {
                        dataGridView1.Rows.RemoveAt(i);
                    }
                }
                dataGridView1.Refresh();
            }
            using (StreamWriter sw = new StreamWriter(path))
            {
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    for (int j = 0; j < dataGridView1.ColumnCount - 1; j++)
                        sw.Write(dataGridView1[j, i].Value + ";");
                    sw.WriteLine(dataGridView1[6, i].Value);
                }
            }
        }
    }
}
