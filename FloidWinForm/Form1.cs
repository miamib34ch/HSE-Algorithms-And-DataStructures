using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FloidWinForm
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_Enter(object sender, EventArgs e)
        {

        }

        TextBox[,] arr;
        static double[,] D_new;
        static int[,] T_new;
        static double[,] D_old;
        static int[,] T_old;

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out int result))
            {
                arr = new TextBox[result,result];
                tableLayoutPanel1.Controls.Clear();
                tableLayoutPanel1.ColumnCount = result;
                tableLayoutPanel1.RowCount = result;
                for (int i = 0; i < result; i++)
                {
                    for (int j = 0; j < result; j++)
                    {
                        arr[i, j] = new TextBox();
                        arr[i, j].Font = textBox1.Font;
                        tableLayoutPanel1.Controls.Add(arr[i,j], i, j);
                        if (i == j)
                        {
                            arr[i, j].Text = "0";
                            arr[i, j].Enabled = false;                        
                        }     
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                label3.Text = "";
                D_old = new double[int.Parse(textBox1.Text), int.Parse(textBox1.Text)];
                T_old = new int[int.Parse(textBox1.Text), int.Parse(textBox1.Text)];
                for (int i = 0; i < int.Parse(textBox1.Text); i++)
                {
                    for (int j = 0; j < int.Parse(textBox1.Text); j++)
                    {
                        T_old[i, j] = j + 1;
                        if (arr[j, i].Text == "-")
                        {
                            D_old[i, j] = double.PositiveInfinity;
                        }
                        else
                        {
                            if (double.TryParse(arr[j, i].Text, out double res))
                            {
                                D_old[i, j] = res;
                            }
                        }
                    }
                }
                Floid(int.Parse(textBox1.Text), ref label3, int.Parse(textBox2.Text), int.Parse(textBox3.Text));
            }
            catch
            {
                label3.Text = "Ошибка";
            }
        }

        public static void Floid(int x, ref Label lb3, int tx2, int tx3)
        {
            D_new = new double[x, x];
            T_new = new int[x, x];
            int k = 0;
            do
            {
                for (int i = 0; i < x; i++)
                {
                    for (int j = 0; j < x; j++)
                    {
                        if (D_old[i, j] - (D_old[i, k] + D_old[k, j]) >= 0)
                            D_new[i, j] = D_old[i, k] + D_old[k, j];
                        else
                            D_new[i, j] = D_old[i, j];
                        

                        if (D_old[i, j] == D_new[i, j])
                            T_new[i, j] = T_old[i, j];
                        else
                            T_new[i, j] = T_old[i, k];

                        if (i == j && D_new[i, j] < 0)
                            lb3.Text = "Ошибка, отрицательный цикл";
                    }
                }
                for (int i = 0; i < x; i++)
                {
                    for (int j = 0; j < x; j++)
                    {
                        D_old[i, j] = D_new[i, j];
                        T_old[i, j] = T_new[i, j];
                    }
                }
                k++;
            } while (k != x);
            if (lb3.Text != "Ошибка, отрицательный цикл")
            {
                if (D_new[tx2 - 1, tx3 - 1] != double.PositiveInfinity)
                {
                    lb3.Text = $"Минимальный путь из {tx2} в {tx3} равен {D_new[tx2 - 1, tx3 - 1]}\n";
                    bool go = true;
                    do
                    {
                        lb3.Text += $"v{tx2} ";
                        if (T_new[tx2 - 1, tx3 - 1] == tx3)
                        {
                            lb3.Text += $"-> v{tx3}";
                            go = false;
                        }
                        else
                        {
                            tx2 = T_new[tx2 - 1, tx3 - 1];
                            lb3.Text += "-> ";
                        }
                    } while (go);
                }
                else
                    lb3.Text = "Пути нет";
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
