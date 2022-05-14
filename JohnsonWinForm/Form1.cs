using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Johnson
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void label4_Click(object sender, EventArgs e)
        {

        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        TextBox[,] arr;
        double[,] mas;

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out int result))
            {
                arr = new TextBox[2, result];
                tableLayoutPanel1.Controls.Clear();
                tableLayoutPanel1.ColumnCount = result;
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < result; j++)
                    {
                        arr[i, j] = new TextBox();
                        tableLayoutPanel1.Controls.Add(arr[i, j], j, i);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label5.Text = "";
            label6.Text = "";
            label7.Text = "";
            try
            {
                mas = new double[2, int.Parse(textBox1.Text)];
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < int.Parse(textBox1.Text); j++)
                    {
                        mas[i, j] = double.Parse(arr[i, j].Text);
                    }
                }
                Johnson(mas,ref label5,ref label6, ref label7);
            }
            catch
            {
                label5.Text = "Ошибка";
            }
        }

        public static void Johnson(double[,] mas, ref Label label5, ref Label label6, ref Label label7)
        {
            for (int j = 0; j < mas.Length/2; j++)
            {
                if (mas[0, j] <= mas[1, j])
                    label5.Text += $"{j+1}";
                else
                    label6.Text += $"{j+1}";
            }                                                       //делю на две группы
            double[,] first = new double[2, label5.Text.Length];
            double[,] second = new double[2, label6.Text.Length];
            int countf = 0;
            int counts = 0;
            for (int j = 0; j < mas.Length / 2; j++)            //продолжаю делить: преобразую в массивы
            {
                if (mas[0, j] <= mas[1, j])
                {
                    first[0, countf] = j+1;
                    first[1, countf] = mas[0, j];
                    countf++;
                }
                else
                {
                    second[0, counts] = j + 1;
                    second[1, counts] = mas[1, j];
                    counts++;
                }
            }
            for (int i = 0; i < first.Length / 2; i++)      //сортировка пузырьком первого массива по возрастанию
            {
                for (int j = 0; j < first.Length / 2 - 1; j++)
                {
                    if (first[1,j] > first [1,j+1])
                    {
                        double tmp0 = first[0, j];
                        double tmp1 = first[1, j];
                        first[0, j] = first[0, j + 1];
                        first[1, j] = first[1, j + 1];
                        first[0, j + 1] = tmp0;
                        first[1, j + 1] = tmp1; 
                    }
                }
            }
            for (int i = 0; i < second.Length / 2; i++)     //сортировка пузырьком второг массива по убыванию
            {
                for (int j = 0; j < second.Length / 2 - 1; j++)
                {
                    if (second[1, j] < second[1, j + 1])
                    {
                        double tmp0 = second[0, j];
                        double tmp1 = second[1, j];
                        second[0, j] = second[0, j + 1];
                        second[1, j] = second[1, j + 1];
                        second[0, j + 1] = tmp0;
                        second[1, j + 1] = tmp1;
                    }
                }
            }
            label5.Text = "Оптимальный порядок выполнения заданий:";        //вывод оптимального порядка заданий
            label6.Text = "";
            for (int j = 0; j < first.Length / 2; j++)
            {
                label5.Text += $" {first[0,j]}";
            }
            for (int j = 0; j < second.Length / 2; j++)
            {
                label5.Text += $" {second[0, j]}";
            }
           
            
            double[,] mas2 = new double[3, mas.Length / 2];             //объединение массивов в один
            for (int i = 0; i < first.Length / 2; i++)
            {
                mas2[0, i] = mas[0, (int)first[0,i] - 1];
                mas2[1, i] = mas[1, (int)first[0,i] - 1];
                mas2[2, i] = first[0, i];
            }
            for (int i = 0; i < second.Length / 2; i++)
            {
                mas2[0, i+ first.Length / 2] = mas[0, (int)second[0, i] - 1];
                mas2[1, i + first.Length / 2] = mas[1, (int)second[0, i] - 1];
                mas2[2, i + first.Length / 2] = second[0, i];
            }


            double sum1 = 0;
            double sum2 = 0;


            for (int i = 0; i < mas.Length / 2; i++)        //подсчёт времени и определение простоев
            {
                if (i == 0)
                {
                    sum1 += mas2[0, i];
                    sum2 += mas2[0, i];
                    label6.Text += $"Простой на втором конвеере: {mas2[0, i]} ч, между началом работы и заданием {mas2[2,i]}"; 
                }
                else
                {
                    sum1 += mas2[0, i];
                    sum2 += mas2[1, i - 1];
                    if (sum1 > sum2)
                    {
                        label6.Text += $"\nПростой на втором конвеере: {sum1 - sum2} ч, между заданиями {mas2[2,i-1]} и {mas2[2,i]}";
                        sum2 += sum1 - sum2;                            
                    }
                }
            }
            sum2 += mas2[1, mas.Length / 2 - 1];
            label6.Text += $"\nПростой на первом конвеере: {sum2 - sum1} ч, между последним заданием и концом работы";
            label7.Text += $"\nВремя работы: {sum2}";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
