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
    public partial class AddForm : Form
    {
        public bool flagEdit = false;
        public AddForm()
        {
            InitializeComponent();
        }

        private void Add_Click(object sender, EventArgs e)
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
            // Создать новый объект и добавить его в список
            List <Tour> listT = new List<Tour> ();

            Form1.listTO.Add(new Touroperator(textBox1.Text, maskedTextBox1.Text, textBox2.Text, comboBox1.Text, listT));
            textBox1.Focus();
            flagEdit = true;
            MessageBox.Show("Туроператор добавлен."
            +"\n\nДобавьте следующее или закройте окно.");
        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void AddForm_Load(object sender, EventArgs e)
        {
            bindingSource1.DataSource = Form1.addrList;
            //comboBox1.DataSource = bindingSource1;
        }

        private void ComboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Delete)
            {
                Form1.addrList.Remove(comboBox1.Text);
                bindingSource1.ResetBindings(false);
                if (Form1.addrList.Count > 0) comboBox1.Text = " " + Form1.addrList[0];
            }
        }
    }
}
