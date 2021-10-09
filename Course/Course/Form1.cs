using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.Diagnostics;

namespace Course
{
    public partial class Form1 : Form
    {
        FileStream fs;
        XmlSerializer xs;

        public Form1()
        {
            InitializeComponent();
        }

        public static List<Touroperator> listTO = new List<Touroperator>();
        public List<Tour> listT;
        public static List<string> addrList = new List<string>();
        private void Form1_Load(object sender, EventArgs e)
        {
            string[] addrArr = new string[] { "Новослободская,9","Овчинниковский переулок,55","Тверская,25", "Академика Янгеля,42", "Сиреневый бульвар,4" };
            addrList.AddRange(addrArr);
            ((DataGridViewComboBoxColumn)dataGridView1.Columns["address"]). DataSource = addrList;

            if (File.Exists("Туроператор_туры.xml"))
            {
                // Восстановим из файла сериализованный граф объектов.
                fs = new FileStream("Туроператор_туры.xml", FileMode.Open);
                xs = new XmlSerializer(typeof(List<Touroperator>));
                listTO = (List<Touroperator>)xs.Deserialize(fs);
                fs.Close();
            }
            else
            {
                //Оператор Арт-Тур------------------------------------------------------
                listT = new List<Tour>();
                listT.Add(new Tour("Франция", "GoldenIce", 60008, "01.02.2020", "14.02.2020"));
                listT.Add(new Tour("Италия", "SkyStars", 90000, "01.02.2020", "14.02.2020"));
                listT.Add(new Tour("Сербия", "Twice", 40000, "01.02.2020", "14.02.2020"));
                listT.Add(new Tour("Турция", "Big Dream", 50000, "01.02.202-", "14.02.2020"));
                listTO.Add(new Touroperator("Art Tour", "+79746779367", "arttour@mail.ru", "Новослободская,9", listT));

                //Оператор Coral Travel-----------------------------------
                listT = new List<Tour>();
                listT.Add(new Tour("Испания", "Sunny day", 100000, "01.02.2020", "14.02.2020"));
                listT.Add(new Tour("США", "USA village", 50000, "01.02.2020", "14.02.2020"));
                listT.Add(new Tour("Нидерланды", "Medisson", 30000, "01.02.2020", "14.02.2020"));
                listT.Add(new Tour("Турция", "FiveStars", 40000, "01.02.2020", "14.02.2020"));
                listT.Add(new Tour("Турция", "ThreeStars", 20000, "01.02.2020", "14.02.2020"));
                listT.Add(new Tour("Турция", "FiveStars", 40000, "02.02.2020", "15.02.2020"));
                listT.Add(new Tour("Турция", "FiveStars", 40000, "03.02.2020", "16.02.2020"));
                listTO.Add(new Touroperator("Coral Travel", "+79775734562", "coraltravel@mail.ru", "Овчинниковский переулок,55", listT));

                //Оператор Tez Tour---------------------------------------------------------
                listT = new List<Tour>();
                listT.Add(new Tour("Франция", "Paris love", 80000, "01.02.2020", "14.02.2020"));
                listT.Add(new Tour("Эстония", "Happy holiday", 90000, "01.02.2020", "14.02.2020"));
                listT.Add(new Tour("Чехия", "NiceChoice", 70000, "01.02.2020", "14.02.2020"));
                listTO.Add(new Touroperator("Tez Tour", "+79335779321", "teztour@mail.ru", "Тверская,25", listT));
            }
            touroperatorBindingSource.DataSource = listTO;
        }

        //-----------------------------------------------------------------------------------------------
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow == null
            || dataGridView1.CurrentRow.Index ==
            dataGridView1.RowCount - 1) // Новая строка
                return;
            FormTours formT = new FormTours();
            // Выполнить привязку сетки 2 к студентам текущего факультета.
            formT.to = dataGridView1.CurrentRow.Index;
            formT.tourBindingSource.DataSource = listTO[formT.to].Tours;

            formT.ShowDialog();
            touroperatorBindingSource.ResetCurrentItem(); // Для обновления количества студентов.
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            // Создание потока
            fs = new FileStream("Туроператор_туры.xml", FileMode.Create);
            XmlSerializer xs = new XmlSerializer(typeof(List<Touroperator>));

            // Сохраним объект в XML-файле
            xs.Serialize(fs, listTO);

            fs.Close();
            Close();

        }

        private void Add_Click(object sender, EventArgs e)
        {
            AddForm addForm = new AddForm();
            addForm.ShowDialog();

            if (addForm.flagEdit)
                touroperatorBindingSource.ResetBindings(false);
        }

        private void Del_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Для удаления одного или нескольких туроператоров выделите их, щелкнув их заголовки(слева), и нажмите клавишу Delete");
        }

        private void Edit_Click(object sender, EventArgs e)
        {
            EditForm editForm = new EditForm();
            editForm.i = dataGridView1.CurrentRow.Index;
            editForm.ShowDialog();
            if (editForm.flagEdit)

                touroperatorBindingSource.ResetCurrentItem();
        }
        //------------------------------------------------------------------------
        private void Show_Click(object sender, EventArgs e)
        {
            dataGridView1_CellDoubleClick(null, null);
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ToolStripTextBox1_Click(object sender, EventArgs e)
        {
            Process p = Process.Start("NotePad.exe", "Справка\\manual.txt");
        }

        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Проект разработала студентка группы ПИ18-1 Абдуллина Дария Сергеевна",
                "Об авторе", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ToolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void MenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
