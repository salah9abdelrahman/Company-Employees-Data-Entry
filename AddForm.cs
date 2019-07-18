using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Empolyeee;

namespace formDay3
{
    public partial class AddForm : Form
    {
       public Employee emp = new Employee();

        public AddForm()
        {
            InitializeComponent();
            this.textBoxName.DataBindings.Add("Text", emp, "Name");
            this.textBoxID.DataBindings.Add("Text", emp,"Id");
            this.textBoxSalary.DataBindings.Add("Text", emp,"Salary");
        }

        private void TextBoxName_TextChanged(object sender, EventArgs e)
        {
               
            this.buttonSubmitEmployee.Enabled = false;
            if (!(string.IsNullOrEmpty(this.textBoxName.Text)))
            {
                this.buttonSubmitEmployee.Enabled = true;
            }
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            if (this.textBoxID.Text.Length > 0)
            {
                try
                {
                    emp.Id = int.Parse(this.textBoxID.Text);
                    errorProvider1.SetError(this.textBoxID, "");

                }
                catch (FormatException)
                {

                    errorProvider1.SetError(this.textBoxID, "only numbers");
                }
            }
                
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (this.textBoxSalary.TextLength > 0)
            {
                try
                {
                    emp.Salary = float.Parse(this.textBoxSalary.Text);
                    errorProvider1.SetError(this.textBoxSalary, "");
                }
                catch (FormatException)
                {

                    errorProvider1.SetError(this.textBoxSalary, "only numbers");
                }
            }
        }

        private void ButtonSubmitEmployee_Click(object sender, EventArgs e)
        {
            Form1.empList.Add(emp);
            this.Close();
        }
    }
}
