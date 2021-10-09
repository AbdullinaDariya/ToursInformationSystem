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
    public partial class FormTours : Form
    {
        public int to; //Индекс текущего туроператора
        public bool select;
        Color backColor; //Текущий цвет фона сетки
        public FormTours()
        {
            InitializeComponent();
        }
        //------------------------------------------------------------------
        private void FormTours_Load(object sender, EventArgs e)
        {
            Text += Form1.listTO[to].Name;
            backColor = dataGridView2.RowsDefaultCellStyle.BackColor;
            label1.Text = "Отображены ВСЕ туры туроператора"; 

            select = false; // true - на экране отображены не все туры списка;
                                 // false - на экране отображены ВСЕ туры.
        }
        //------------------------------------------
        private void DataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView2.Columns[e.ColumnIndex].Name == "price" ) //если цена 0-заменяем на пустую строку
                if (e.Value.ToString() == "0")
                e.Value = "";
        }
        //--------------------------------------------------------
        private void DataGridView2_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            // Новое значение:
            string valueNew = e.FormattedValue.ToString().Trim();

            // Имя столбца (e.ColumnIndex - индекс столбца со старым значением).
            string nameColumn = dataGridView2.Columns[e.ColumnIndex].Name;
            int vInt;
            if (nameColumn == "price" && valueNew != ""
            &&!int.TryParse(valueNew, out vInt))
            {
                MessageBox.Show("Цена должна быть числом.");
                e.Cancel = true;   // Новое значение не принято.
            }
            if (nameColumn == "price")
            {
                if (int.Parse(valueNew) < 50000 && checkBoxMin.Checked)
                // Изменить цвет фона у должника.
                dataGridView2.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Violet;
                else
                    dataGridView2.Rows[e.RowIndex].DefaultCellStyle.BackColor = backColor;
            }
        }
        //---добавить тур--------------------
        private void Add_Click(object sender, EventArgs e)
        {
            AddTourForm addForm = new AddTourForm();
            addForm.to = to; // Текущий номер туроператора
            addForm.ShowDialog();
            if (addForm.flagedit)
            {
                tourBindingSource.ResetBindings(false);
                Дешёвый();
                if (select)
                { // Если ранее была выполнена фильтрация.
                    Select_Click(null, null);

                    // Найдем последнюю отображаемую строку и перейдем на неё.
                    for (int to = dataGridView2.RowCount - 1; to >= 0; to--)
                        if (dataGridView2.Rows[to].Visible == true)
                        {
                            // Перейти на добавленного студента. Его индекс = i.
                            dataGridView2.CurrentCell = dataGridView2[1, to];
                            break;
                        }
                }
                else
                    // Перейти на добавленного студента. Он в конце списка.
                    dataGridView2.CurrentCell = dataGridView2[1, dataGridView2.RowCount - 1];
            }
         }
        
        //---------------изменить тур------------------
        private void Edit_Click(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentRow == null) return;
            EditTourForm editForm = new EditTourForm();
            editForm.to = to; // Текущий номер туроператора
            editForm.t = dataGridView2.CurrentRow.Index; // Текущий номер тура
            editForm.ShowDialog();
            if (editForm.flagedit)
                tourBindingSource.ResetCurrentItem();
            //Дешёвый();
            if ((int)dataGridView2["price", editForm.t].Value < 50000 && checkBoxMin.Checked)
            // Изменить цвет фона у тура.
                dataGridView2.Rows[editForm.t].DefaultCellStyle.BackColor =Color.Violet;

            else
                dataGridView2.Rows[editForm.t].DefaultCellStyle.BackColor = backColor;
        }
        //---------удалить тур------------------
        private void Del_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Для удаления одного или нескольких туров " +
            " выделите их, щелкнув их заголовки(слева), " +
            " и нажмите клавишу Delete.");
        }

        private void GroupBox1_Enter(object sender, EventArgs e)
        {

        }
        //-----отбор туров-----------------------
        private void Select_Click(object sender, EventArgs e)
        {
            dataGridView2.CurrentCell = null;
            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                if (TestRow(i))
                    dataGridView2.Rows[i].Visible = true;
                else
                {
                    dataGridView2.Rows[i].Visible = false;
                    select = true;
                }
            }
            if (select) label1.Text = string.Format("Внимание! Список туров отфильтрован: {0}, {1}, {2}, {3}, {4}, {5}", textBoxCountry.Text, textBoxHotel.Text, textBoxPrice1.Text,textBoxPrice2.Text, textBoxDate1.Text, textBoxDate2.Text);

            // Перенести фокус на первого отображаемого студента, если он есть.
            for (int i = 0; i < dataGridView2.RowCount; i++)
                if (dataGridView2.Rows[i].Visible == true)
                {
                    // Перейти на добавленного студента. Его индекс = i.
                    dataGridView2.CurrentCell = dataGridView2[1, i];
                    break;
                }
        }
        // Метод проверяет ячейки переданной строки
        // на одновременное равенство всем критериям.
        private bool TestRow(int t)
        {
            Tour tr = Form1.listTO[to].Tours[t];

            if (textBoxCountry.Text != "" &&
            !tr.Country.StartsWith(textBoxCountry.Text)) return false;
            if (textBoxHotel.Text != "" &&
            !tr.Hotel.StartsWith(textBoxHotel.Text)) return false;

            int price1, price2;
            int.TryParse(textBoxPrice1.Text, out price1);
            int.TryParse(textBoxPrice2.Text, out price2);
            if (price2 == 0) price2 = 100000;
            if (tr.Price < price1 || tr.Price > price2) return false;

            if (textBoxDate1.Text != "" &&
            !tr.Date1.Contains(textBoxDate1.Text)) return false;

            if (textBoxDate2.Text != "" &&
            !tr.Date2.Contains(textBoxHotel.Text)) return false;
            return true;
        }
        //-------- Выделение фоном самого дешёвого тура---------------------------
        private void Дешёвый()
        {
            
            if (checkBoxMin.Checked)
            {
                for (int Y = 0; Y < dataGridView2.RowCount; Y++)
                    // Изменить цвет фона.
                    if ((int)dataGridView2["price", Y].Value < 50000)
                    dataGridView2.Rows[Y].DefaultCellStyle.BackColor = Color.Violet;
            }
            else
                for (int Y = 0; Y < dataGridView2.RowCount; Y++)
                 // Восстановить цвет фона.
                    if ((int)dataGridView2["price", Y].Value < 50000)
                        dataGridView2.Rows[Y].DefaultCellStyle.BackColor = backColor;
        }


        //------все туры-------------------------------------
        private void All_Click(object sender, EventArgs e)
        {
            //все туры
            select = false;
            label1.Text = "Отображены ВСЕ туры туроператора" ;
            for (int i = 0; i < dataGridView2.RowCount; i++)
                dataGridView2.Rows[i].Visible = true;
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            //очистить
            textBoxCountry.Text = "";
            textBoxHotel.Text = "" ;
            textBoxPrice1.Text = "";
            textBoxPrice2.Text = "" ;
            textBoxDate1.Text = "" ;
            textBoxDate2.Text = "";
        }
        //---------Одноуровневая сортировка-----------------
        private void DataGridView2_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView2.Rows.Count == 0) return;
            int d = checkBox1.Checked == true ? -1 : 1; // Порядок сортировки.
            List <Tour>list = Form1.listTO[to].Tours;
            switch (dataGridView2.Columns[e.ColumnIndex].Name)
            {
                case "country":
                    list.Sort((a1, a2) => d*a1.Country.CompareTo(a2.Country));
                    break;
                case "hotel":
                    list.Sort((a1, a2) => d*a1.Hotel.CompareTo(a2.Hotel));
                    break;
                case "price":
                    list.Sort((a1, a2) => d*a1.Price.CompareTo(a2.Price));
                    break;
                case "date1":
                    list.Sort((a1, a2) => d* a1.Date1.CompareTo(a2.Date1));
                    break;
                case "date2":
                    list.Sort((a1, a2) => d* a1.Date2.CompareTo(a2.Date2));
                    break;
                default: // По другим столбцам сортировки не будет.
                    return;
            }
            tourBindingSource.ResetBindings(false);
            if (select) Select_Click(null, null); // Обновить фильтрацию.
            Дешёвый();
        }

        private void Sort2_Click(object sender, EventArgs e)
        {
            List <Tour>list = Form1.listTO[to].Tours;
            if (!checkBox2.Checked && !checkBox3.Checked)
                switch (comboBox1.SelectedIndex)
                {
                    case 0:
                        list = (list.OrderBy(s => s.Country).ThenBy(s => s.Hotel)).ToList();
                        break;
                    case 1:
                        list = (list.OrderBy(s => s.Country).ThenBy(s => s.Price)).ToList();
                        break;
                    case 2:
                        list = (list.OrderBy(s => s.Price).ThenBy(s =>s.Country)).ToList();
                        break;
                    case 3:
                        list = (list.OrderBy(s => s.Price).ThenBy(s =>s.Hotel)).ToList();
                        break;
                }
            else if (!checkBox2.Checked && checkBox3.Checked)
                switch (comboBox1.SelectedIndex)
                {
                    case 0:
                        list = (list.OrderBy(s => s.Country).ThenByDescending(s => s.Hotel)).ToList();
                        break;
                    case 1:
                        list = (list.OrderBy(s =>s.Country).ThenByDescending(
                        s => s.Price)).ToList();
                        break;
                    case 2:
                        list = (list.OrderBy(s =>s.Price).ThenByDescending(
                        s =>s.Country)).ToList();
                        break;
                    case 3:
                        list = (list.OrderBy(s => s.Price).ThenByDescending(
                        s =>s.Hotel)).ToList();
                        break;
                }
            else if (checkBox2.Checked && checkBox3.Checked)
                switch (comboBox1.SelectedIndex)
                {
                    case 0:
                        list = (list.OrderByDescending(s =>s.Country).ThenByDescending(
                        s => s.Hotel)).ToList();
                        break;
                    case 1:
                        list = (list.OrderByDescending(s => s.Country).ThenByDescending(
                         s => s.Price)).ToList();
                        break;
                    case 2:
                        list = (list.OrderByDescending(s => s.Price).ThenByDescending(
                        s => s.Country)).ToList();
                        break;
                    case 3:
                        list = (list.OrderByDescending(s => s.Price).ThenByDescending(
                        s => s.Hotel)).ToList();
                        break;
                    
                }
            else if (checkBox2.Checked && !checkBox3.Checked)

                switch (comboBox1.SelectedIndex)
                {
                    case 0:
                        list = (list.OrderByDescending(s => s.Country).ThenBy(
                        s => s.Hotel)).ToList();
                        break;
                    case 1:
                        list = (list.OrderByDescending(s => s.Country).ThenBy(
                        s => s.Price)).ToList();
                        break;
                    case 2:
                        list = (list.OrderByDescending(s => s.Price).ThenBy(
                        s => s.Country)).ToList();
                        break;
                    case 3:
                        list = (list.OrderByDescending(s => s.Price).ThenBy(
                        s => s.Hotel)).ToList();
                        break;

                }
            Form1.listTO[to].Tours = list;
           tourBindingSource.DataSource = list;
            if (select) Select_Click(null, null);
            Дешёвый();
        }

        private void Дешёвый_CheckedChanged(object sender, EventArgs e)
        {
            Дешёвый();
        }
    }
}
