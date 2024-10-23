using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

class Program
{
    static Random random = new Random();
    static List<int> drawnNumbers = new List<int>();

    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        Form form = new Form();
        form.Text = "Bingo";

        Button drawButton = new Button();
        drawButton.Text = "数字抽選ボタン";
        drawButton.Dock = DockStyle.Top;
        drawButton.Height = 40;

        ListBox historyListBox = new ListBox();
        historyListBox.Dock = DockStyle.Fill;

        drawButton.Click += (sender, e) =>
        {
            if (drawnNumbers.Count >= 75)
            {
                MessageBox.Show("すべての数字が出されました。");
                return;
            }

            int number;
            do
            {
                number = random.Next(1, 76);
            } while (drawnNumbers.Contains(number));

            drawnNumbers.Add(number);
            historyListBox.Items.Add($"数字: {number}");

            MessageBox.Show($"抽選結果: {number}");
        };

        form.Controls.Add(historyListBox);
        form.Controls.Add(drawButton);

        Application.Run(form);
    }
}
