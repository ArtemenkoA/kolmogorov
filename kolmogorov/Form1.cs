using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kolmogorov
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int n = Convert.ToInt32(textBox1.Text);
            table.RowCount = n;
            table.ColumnCount = n;
            foreach (DataGridViewColumn column in table.Columns)
                column.Width = 40;
                
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int n = Convert.ToInt32(textBox1.Text);
            double[,] matrix = new double[n, n];
            double[,] matrix_slau = new double[n, n];

            double answer=0;
            double[] Answers = new double[n];
            double[] FreeCoef = new double[n];
            double[] Times = new double[n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    matrix[i, j] = Convert.ToDouble(table.Rows[i].Cells[j].Value);
                }
            }

            matrix_slau[0, 0] = -matrix[0, 1];
            matrix_slau[0, 1] = matrix[1, 0];
           
            for (int i = 1; i < n-2; i++)
            {
                for (int j = 1; j < n-2; j++)
                {
                    matrix_slau[i,j-1] = matrix[i-1,j];
                    matrix_slau[i,j] = -(matrix[i, j-1] + matrix[i,j+1]);
                    matrix_slau[i,j+1] = matrix[i+1,j];
                    j++;
                }
            }

            matrix_slau[n - 2, n-2] = matrix[n - 2, n-1];
            matrix_slau[n - 2, n - 1] = -matrix[n-1, n - 2];


            for (int i = 0; i < n; i++)
            {
                Answers[i] = 0;
                FreeCoef[i] = 0;
                matrix_slau[n-1, i] = 1;
            }

            FreeCoef[n-1] = 1;

            Gauss(matrix_slau, FreeCoef, answer, Answers, n);

            for (int i = 0; i < n; i++)
            {
                double sum_l = 0;
                for (int j = 0; j < n; j++)
                {
                     sum_l = sum_l + matrix[i, j];
                }
                Times[i] = -(Answers[i] - 1) / sum_l;
                listBox1.Items.Add("P" + (i + 1) + ":  " + Answers[i] + "   t=  " + Times[i]);
            }
        }

        void Gauss(double[,] matrix, double[] FreeCoef, double answer, double[] Answers, int n)
        {
            for (int k = 0; k < n - 1; k++)
            {
                for (int i = k + 1; i < n; i++)
                {
                    for (int j = k + 1; j < n; j++)
                    {
                        matrix[i, j] = matrix[i, j] - matrix[k, j] * (matrix[i, k] / matrix[k, k]);
                    }
                    FreeCoef[i] = FreeCoef[i] - FreeCoef[k] * matrix[i, k] / matrix[k, k];
                }
            }

            for (int k = n - 1; k >= 0; k--)
            {
                answer = 0;
                for (int j = k + 1; j < n; j++)
                    answer += matrix[k, j] * Answers[j];
                Answers[k] = (FreeCoef[k] - answer) / matrix[k, k];
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }
        private void Form1_Load(object sender, EventArgs e) { }
        private void pictureBox1_Click(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
    }
}