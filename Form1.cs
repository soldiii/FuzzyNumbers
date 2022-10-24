using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab1_II
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private double a3_down, a3_up;
        private double[] a_down, a_up, b_down, b_up;
        private NumericUpDown[] c_down, c_up;

        private bool IsFigureConvex(bool flag)
        {
            if (flag)
                for(var i = 0; i < a_down.Length - 1; i++)
                {
                    if (a_down[i] > a_down[i + 1] || a_up[i] < a_up[i + 1])
                    {
                        MessageBox.Show("Условие выпуклости для функции принадлежности переменной A не выполняется");
                        return false;
                    }
                }
            else
                for (var i = 0; i < b_down.Length - 1; i++)
                {
                    if (b_down[i] > b_down[i + 1] || b_up[i] < b_up[i + 1])
                    {
                        MessageBox.Show("Условие выпуклости для функции принадлежности переменной B не выполняется");
                        return false;
                    }
                }
            return true;
        }

        private bool Getting_Values()
        {
            a3_down = ((0.25 * ((double)(a4_down.Value) - (double)(a2_down.Value))) / 0.5) + (double)(a2_down.Value);
            a3_up = ((0.25 * ((double)(a4_up.Value) - (double)(a2_up.Value))) / 0.5) + (double)(a2_up.Value);
            a_down = new double[4] { (double)(a1_down.Value), (double)(a2_down.Value), a3_down, (double)(a4_down.Value) };
            a_up = new double[4] { (double)(a1_up.Value), (double)(a2_up.Value), a3_up, (double)(a4_up.Value) };
            if (!IsFigureConvex(true)) return false;
            b_down = new double[4] { (double)(b1_down.Value), (double)(b2_down.Value), (double)(b3_down.Value), (double)(b4_down.Value) };
            b_up = new double[4] { (double)(b1_up.Value), (double)(b2_up.Value), (double)(b3_up.Value), (double)(b4_up.Value) };
            if (!IsFigureConvex(false)) return false;
            c_down = new NumericUpDown[4] { c1_down, c2_down, c3_down, c4_down };
            c_up = new NumericUpDown[4] { c1_up, c2_up, c3_up, c4_up };
            return true;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (!Getting_Values()) return;
            for (var i = 0;i < a_down.Length; ++i)
            {
                c_down[i].Value = (decimal)(a_down[i] + b_down[i]);
                c_up[i].Value = (decimal)(a_up[i] + b_up[i]);
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (!Getting_Values()) return;
            for (var i = 0; i < a_down.Length; ++i)
            {
                c_down[i].Value = (decimal)(a_down[i] - b_up[i]);
                c_up[i].Value = (decimal)(a_up[i] - b_down[i]);
            }
        }
        private void Button3_Click(object sender, EventArgs e)
        {
            if (!Getting_Values()) return;
            for (var i = 0; i < a_down.Length; ++i)
            {
                c_down[i].Value = (decimal)(a_down[i] * b_down[i]);
                c_up[i].Value = (decimal)(a_up[i] * b_up[i]);
            }
        }
        private void Button4_Click(object sender, EventArgs e)
        {
            if (!Getting_Values()) return;
            for (var i = 0; i < b_down.Length; ++i)
            {
                if ((b_down[i] == 0) || (b_up[i] == 0))
                {
                    MessageBox.Show("В переменной B есть числа равные 0");
                    return;
                }
            }
            for (var i = 0; i < b_down.Length; ++i)
            {
                    c_down[i].Value = (decimal)(a_down[i] / b_up[i]);
                    c_up[i].Value = (decimal)(a_up[i] / b_down[i]);
                
            }
        }   
        private void Button5_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            if (!Getting_Values()) return;
            double[] y = new double[4] { 0, 0.5, 0.75, 1};
            for (var i = 0; i < y.Length; ++i)
            {
                chart1.Series[0].Points.AddXY(a_down[i], y[i]);
            }
            for (var i = y.Length - 1; i >= 0; --i)
            {
                chart1.Series[0].Points.AddXY(a_up[i], y[i]);
            }

        }

        private void Button6_Click(object sender, EventArgs e)
        {
            chart1.Series[1].Points.Clear();
            if (!Getting_Values()) return;
            double[] y = new double[4] { 0, 0.5, 0.75, 1};
            for (var i = 0; i < y.Length; ++i)
            {
                chart1.Series[1].Points.AddXY(b_down[i], y[i]);
            }
            for (var i = y.Length - 1; i >= 0; --i)
            {
                chart1.Series[1].Points.AddXY(b_up[i], y[i]);
            }

        }
        private void Button7_Click(object sender, EventArgs e)
        {
            chart1.Series[2].Points.Clear();
            double[] y = new double[8] { 0, 0.5, 0.75, 1, 1, 0.75, 0.5, 0 };
            double[] x = new double[8] { (double)(c1_down.Value), (double)(c2_down.Value), (double)(c3_down.Value), (double)(c4_down.Value), (double)(c4_up.Value), (double)(c3_up.Value), (double)(c2_up.Value), (double)(c1_up.Value) };
            for (var i = 0; i < y.Length; ++i)
            {
                chart1.Series[2].Points.AddXY(x[i], y[i]);
            }
        }
        private void button8_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            chart1.Series[2].Points.Clear();
        }
        private void button9_Click(object sender, EventArgs e)
        {
            if (!Getting_Values()) return;
            int ka = a_up.Length;
            int kb = b_up.Length;
            double result_a = 0;
            double result_b = 0; 

            for (var i = 0; i < ka; ++i)
            {
                result_a += a_up[i] + a_down[i];
            }
            for (var i = 0; i < kb; ++i)
            {
                result_b += b_up[i] + b_down[i];
            }
            result_a /= ka;
            result_b /= kb;

            if (result_a > result_b)
                MessageBox.Show("Нечеткие числа не равны: число A больше числа Б");
            else if(result_a < result_b)
                MessageBox.Show("Нечеткие числа не равны: число A меньше числа Б");
            else
                MessageBox.Show("Нечеткие числа равны");

        }

    }
}
