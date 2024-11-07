using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BingoCardGenerator
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new BingoForm());
        }
    }

    public partial class BingoForm : Form
    {
        private const int GridSize = 5;
        private Button[,] buttons = new Button[GridSize, GridSize];

        public BingoForm()
        {
            InitializeBingoCard();
            GenerateBingoCard();
        }

        private void InitializeBingoCard()
        {
            for (int i = 0; i < GridSize; i++)
            {
                for (int j = 0; j < GridSize; j++)
                {
                    buttons[i, j] = new Button
                    {
                        Width = 50,
                        Height = 50,
                        Tag = false
                    };
                    buttons[i, j].Click += Button_Click;
                    this.Controls.Add(buttons[i, j]);
                    buttons[i, j].Location = new System.Drawing.Point(j * 50, i * 50);
                }
            }

            buttons[2, 2].Text = "FREE";
            buttons[2, 2].Tag = true;
        }

        private void GenerateBingoCard()
        {
            Random random = new Random();
            for (int col = 0; col < GridSize; col++)
            {
                List<int> numbers = Enumerable.Range(col * 15 + 1, 15).OrderBy(x => random.Next()).Take(5).ToList();
                for (int row = 0; row < GridSize; row++)
                {
                    if (col == 2 && row == 2) continue;

                    buttons[row, col].Text = numbers[row].ToString();
                }
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                btn.Tag = true;
                btn.BackColor = System.Drawing.Color.LightGreen;
                CheckBingo();
            }
        }

        private void CheckBingo()
        {
            for (int i = 0; i < GridSize; i++)
            {
                if (IsBingo(GetRow(i)) || IsBingo(GetColumn(i)))
                {
                    MessageBox.Show("Bingo!");
                    return;
                }
            }
            if (IsBingo(GetDiagonal1()) || IsBingo(GetDiagonal2()))
            {
                MessageBox.Show("Bingo!");
            }
        }

        private bool IsBingo(List<Button> buttons)
        {
            return buttons.All(btn => (bool)btn.Tag);
        }

        private List<Button> GetRow(int row)
        {
            List<Button> rowButtons = new List<Button>();
            for (int col = 0; col < GridSize; col++)
            {
                rowButtons.Add(buttons[row, col]);
            }
            return rowButtons;
        }

        private List<Button> GetColumn(int col)
        {
            List<Button> colButtons = new List<Button>();
            for (int row = 0; row < GridSize; row++)
            {
                colButtons.Add(buttons[row, col]);
            }
            return colButtons;
        }

        private List<Button> GetDiagonal1()
        {
            List<Button> diag1Buttons = new List<Button>();
            for (int i = 0; i < GridSize; i++)
            {
                diag1Buttons.Add(buttons[i, i]);
            }
            return diag1Buttons;
        }

        private List<Button> GetDiagonal2()
        {
            List<Button> diag2Buttons = new List<Button>();
            for (int i = 0; i < GridSize; i++)
            {
                diag2Buttons.Add(buttons[i, GridSize - i - 1]);
            }
            return diag2Buttons;
        }
    }
}






