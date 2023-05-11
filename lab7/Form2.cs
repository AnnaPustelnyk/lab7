using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace lab7
{
    public partial class Form2 : Form
    {
        private List<Tuple<int, int>> selectedCells;
        private Form1 form1;
        private bool gameEnded;
        private int secondsLeft, number1, number2;
        public Form2(Form1 form1)
        {
            InitializeComponent();
            this.form1 = form1;
            number2 = int.Parse(this.form1.textBox2.Text) * 2;
            dataGridView1.ReadOnly = true;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {


        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gameEnded) return;

            if (number2 == 0)
            {
                timer1.Stop();
                MessageBox.Show("You win!");
                gameEnded = true;
                return;
            }
            DataGridView dataGridView = (DataGridView)sender;
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView.RowCount && e.ColumnIndex >= 0 && e.ColumnIndex < dataGridView.ColumnCount)
            {
                Tuple<int, int> cell = Tuple.Create(e.RowIndex, e.ColumnIndex);
                if (selectedCells.Contains(cell))
                {
                    dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Green;
                    dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.SelectionBackColor = Color.Green;
                    number2--;
                }
                else
                {
                    dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Red;
                    dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.SelectionBackColor = Color.Red;
                }
            }
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            secondsLeft--;
            if (secondsLeft == 0)
            {
                timer1.Stop();
                MessageBox.Show("You lose!");
                gameEnded = true;
                return;
            }

            if (number2 == 0)
            {
                timer1.Stop();
                MessageBox.Show("You win!");
                gameEnded = true;
                return;
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Random random = new Random();

            secondsLeft = int.Parse(form1.textBox2.Text) * 3;
            timer1.Interval = 1000;
            timer1.Tick += Timer_Tick;
            timer1.Start();
            gameEnded = false;

            dataGridView1.RowHeadersVisible = false;
            dataGridView1.ColumnHeadersVisible = false;

            if (int.TryParse(form1.textBox1.Text, out number1))
            {
                dataGridView1.RowCount = number1;
                dataGridView1.ColumnCount = number1;

                for (int i = 0; i < number1; i++)
                {
                    dataGridView1.Columns[i].Width = 50;
                    dataGridView1.Rows[i].Height = 50;
                }
                dataGridView1.Size = new Size(50 * number1 + 5, 50 * number1 + 5);
                dataGridView1.Location = new Point(10, 10);

            }

            selectedCells = new List<Tuple<int, int>>();

            int cellsToSelect = int.Parse(form1.textBox2.Text);
            while (selectedCells.Count < cellsToSelect)
            {
                int row = random.Next(number1);
                int col = random.Next(number1);
                if (!selectedCells.Contains(Tuple.Create(row, col)))
                {
                    selectedCells.Add(Tuple.Create(row, col));
                }
            }

            dataGridView1.CellClick += dataGridView1_CellClick;
            dataGridView1.ClearSelection();
            this.Controls.Add(dataGridView1);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
    }
}
