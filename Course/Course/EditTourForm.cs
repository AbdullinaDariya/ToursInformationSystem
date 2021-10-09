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
    public partial class EditTourForm : Form
    {
        public int to, t;
        public bool flagedit = false;
        public EditTourForm()
        {
            InitializeComponent();
        }
        
        private void EditTourForm_Load(object sender, EventArgs e)
        {

            label1.Text += Form1.listTO[to].Name;
            Tour tr = Form1.listTO[to].Tours[t];

            textBox1.Text = tr.Country;
            textBox2.Text = tr.Hotel;
            textBox3.Text = tr.Price.ToString();
            textBox4.Text = tr.Date1;
            textBox5.Text = tr.Date2;

        }

       

        //--------принять изменения-------------
        private void Accept_Click(object sender, EventArgs e)
        {
            int pr = 0;
            if (string.IsNullOrWhiteSpace(textBox1.Text)) // Если поле пустое
            {
                MessageBox.Show("Страна должна быть введена");
                textBox1.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Отель должен быть введен");
                textBox2.Focus();
                return;
            }
            if (!int.TryParse(textBox3.Text, out pr))
            {
                MessageBox.Show("Цена должна быть числом");
                textBox3.Focus();
                return;
            }

          

            Tour tr = Form1.listTO[to].Tours[t];
            tr.Country = textBox1.Text;
            tr.Hotel = textBox2.Text;
            tr.Price = pr;
            tr.Date1 = textBox4.Text;
            tr.Date2 = textBox5.Text;
            flagedit = true;
            Close();
        }
    }
}

    


