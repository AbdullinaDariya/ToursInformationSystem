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
    public partial class AddTourForm : Form
    {
        public int to; //номер текущего туроператора
        public bool flagedit = false; //для отслеживания изменений
        public AddTourForm()
        {
            InitializeComponent();
        }

        private void Label6_Click(object sender, EventArgs e)
        {

        }

        private void AddTourForm_Load(object sender, EventArgs e)
        {
            label1.Text += Form1.listTO[to].Name;
            //DateTime dt = DateTime.Now;
           // dateTimePicker1.Value = new DateTime(dt.Year, dt.Month, dt.Day);
            dateTimePicker2.Value = dateTimePicker1.Value;
        }

        private void Add_Click(object sender, EventArgs e)
        {
            int r = 0;
            if (string.IsNullOrWhiteSpace(textBox1.Text)) // Если поле пустое
            {
                MessageBox.Show("Страна должна быть введена");
                textBox1.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(textBox2.Text)) //Если поле пустое
            {
                MessageBox.Show("Отель должен быть введен");
                textBox2.Focus();
                return;

            }
            if (textBox3.Text != "" && !int.TryParse(textBox3.Text, out r))
            {
                MessageBox.Show("Цена должна быть числом");
                textBox3.Focus();
                return;
            }

            // Проверяем поля даты.
            if (dateTimePicker1.Value > dateTimePicker2.Value)
            {
                MessageBox.Show("Дата начала поездки не может быть позже даты окончания поездки");
                return;
            }

            Form1.listTO[to].Tours.Add(new Tour(textBox1.Text, //добавление
            textBox2.Text, r, dateTimePicker1.Value.ToShortDateString(), dateTimePicker2.Value.ToShortDateString()));
            flagedit = true;  //изменения выполнены

            if (MessageBox.Show("Тур в страну " + textBox1.Text +
              " добавлен. \n\n Добавить следующий тур?",
                "Добавить или выйти ? ",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                DialogResult.No)
                Close();
            textBox1.Focus(); //перемещаем на первое поле ввода



        }

        private void Label5_Click(object sender, EventArgs e)
        {

        }

        private void TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
