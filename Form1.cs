using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;
using Empolyeee;
using System.IO;

namespace formDay3
{
    public partial class Form1 : Form
    {
        string fileName;
        public static BindingList<Employee> empList = new BindingList<Employee>();
        public Form1()
        {
            InitializeComponent();
            this.textBoxName.DataBindings.Add("Text", empList, "Name");
            this.textBoxID.DataBindings.Add("Text", empList, "Id");
            this.textBoxSalary.DataBindings.Add("Text", empList, "Salary");

            this.dataGridView1.DataSource = empList;
        }

        private void ButtonAddEmployee_Click(object sender, EventArgs e)
        {
            AddForm f = new AddForm();
            f.ShowDialog();
        }

        private void ButtonNext_Click(object sender, EventArgs e)
        {
            this.BindingContext[empList].Position++;
        }

        private void ButtonFirstEmployee_Click(object sender, EventArgs e)
        {
            this.BindingContext[empList].Position = 0;

        }

        private void ButtonPrevious_Click(object sender, EventArgs e)
        {
            this.BindingContext[empList].Position--;

        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            if (empList.Count > 0)
            {
                empList.Remove(this.BindingContext[empList].Current as Employee);
            }
        }


        private void ButtonEditEmp_Click(object sender, EventArgs e)
        {
            if (this.textBoxID.Enabled == false)
            {
                this.textBoxID.Enabled = this.textBoxName.Enabled = this.textBoxSalary.Enabled = true;
            }
            else
            {
                this.textBoxID.Enabled = this.textBoxName.Enabled = this.textBoxSalary.Enabled = false;
            }

        }

        private void RadioButtonName_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonName.Checked)
                this.radioButtonDscending.Enabled = this.radioButtonAscending.Enabled = true;
        }

        private void RadioButtonSalary_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonSalary.Checked)
                this.radioButtonDscending.Enabled = this.radioButtonAscending.Enabled = true;
        }


        private void ButtonSort_Click(object sender, EventArgs e)
        {
            if (radioButtonSortId.Checked)
            {
                if (this.radioButtonAscending.Checked)
                {
                    empList = new BindingList<Employee>(empList.OrderBy(x => x.Id).ToList());

                }
                else
                {
                    empList = new BindingList<Employee>(empList.OrderByDescending(x => x.Id).ToList());
                }
                dataGridView1.DataSource = empList;
            }
            else if (this.radioButtonSalary.Checked)
            {
                if (this.radioButtonAscending.Checked)
                {
                    empList = new BindingList<Employee>(empList.OrderBy(x => x.Salary).ToList());
                }
                else
                {
                    empList = new BindingList<Employee>(empList.OrderByDescending(x => x.Salary).ToList());
                }
                dataGridView1.DataSource = empList;
            }
            else if (this.radioButtonName.Checked)
            {
                if (this.radioButtonAscending.Checked)
                {
                    empList = new BindingList<Employee>(empList.OrderBy(x => x.Name).ToList());
                }
                else
                {
                    empList = new BindingList<Employee>(empList.OrderByDescending(x => x.Name).ToList());
                }
                dataGridView1.DataSource = empList;
            }
        }

        private void RadioButtonSortId_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonSortId.Checked)
                this.radioButtonDscending.Enabled = this.radioButtonAscending.Enabled = true;
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog s = new SaveFileDialog();
            
            if (s.ShowDialog() == DialogResult.OK)
            {
                TextWriter writer = new StreamWriter(s.FileName);

                for (int i = 0; i < this.dataGridView1.Rows.Count-1; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        writer.Write(dataGridView1.Rows[i].Cells[j].Value.ToString() + " ");
                    }
                    writer.WriteLine();
                }
                writer.Close();
            }
        }

        private void ButtonLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.Filter = "*.rtf|*.txt";

            if (o.ShowDialog() == DialogResult.OK)
            {
                StreamReader r = new StreamReader(o.FileName);
                string rowMany = r.ReadToEnd();
                string[] data = rowMany.Split();
                List<String> w = new List<string>();
                foreach (string item in data)
                {
                    if (item != "")
                        w.Add(item);
                }
                int rows = 0;
                foreach (char item in rowMany)
                {
                    if (item == '\r')
                    {
                        rows++;
                    }
                }
                int x = 0;
                for (int i = 0; i < rows; i++)
                {
                    empList.Add(new Employee());
                    for (int j = 0; j < 3; j++)
                    {
                        this.dataGridView1.Rows[i].Cells[j].Value = w[x];
                        x++;

                    }
                }              
                r.Close();
            }
        }

        private void GroupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void GroupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ButtonPrint_Click(object sender, EventArgs e)
        {
            try
            {
                PrintDocument pd = new PrintDocument();
                pd.DefaultPageSettings.PaperSize = new PaperSize("A4", 827, 1170);
                pd.PrintPage += PrintDocument1_PrintPage;
                pd.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while printing", ex.ToString());
            }
        }

        private void PrintDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            Bitmap bm = new Bitmap(this.dataGridView1.Width, this.dataGridView1.Height);
            this.dataGridView1.DrawToBitmap(bm, new Rectangle(0, 0, this.dataGridView1.Width, this.dataGridView1.Height));
            e.Graphics.DrawImage(bm, 250, 100);
        }
    }
}
