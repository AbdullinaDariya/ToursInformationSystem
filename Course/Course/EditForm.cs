using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Course
{
    public partial class EditForm : Form
    {
        public int i; //номер текущего туроператора
        public bool flagEdit = false;


        public EditForm()
        {
            InitializeComponent();
        }

        private void Edit_Click(object sender, EventArgs e)
        {
            // Добавить новую строку в ComboBox
            string str = comboBox1.Text;
            if (str != String.Empty && !Form1.addrList.Contains(str))
{
                Form1.addrList.Add(str);
                bindingSource1.ResetBindings(false);
                comboBox1.Text = str;
            }
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Название туроператора должно быть введено.");
                return;
            }
            // Отредактировать запись факультета
            Form1.listTO[i].Name = textBox1.Text;
            Form1.listTO[i].Address = comboBox1.Text;
            Form1.listTO[i].Mail = textBox2.Text;
            Form1.listTO[i].Phone = maskedTextBox1.Text;
            flagEdit = true;

            Close();
        }

        private void EditForm_Load(object sender, EventArgs e)
        {
            bindingSource1.DataSource = Form1.addrList;
            comboBox1.DataSource = bindingSource1;
            textBox1.Text = Form1.listTO[i].Name;
            comboBox1.Text = Form1.listTO[i].Address;
            textBox2.Text = Form1.listTO[i].Mail;
            maskedTextBox1.Text = Form1.listTO[i].Phone;
        }
    }
}
