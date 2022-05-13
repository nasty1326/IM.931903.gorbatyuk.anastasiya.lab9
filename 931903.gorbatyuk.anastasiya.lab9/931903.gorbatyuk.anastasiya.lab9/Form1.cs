using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _931903.gorbatyuk.anastasiya.lab9
{
    public partial class Form1 : Form
    {
        double[] pfreq = new double[5] { 0, 0, 0, 0, 0 };
        double prob5;
        const double HiIdeal = 9.488;
        public Form1()
        {
            InitializeComponent();

            ndProb1.ValueChanged += new System.EventHandler(this.ndProb1_ValueChanged);
            ndProb2.ValueChanged += new System.EventHandler(this.ndProb2_ValueChanged);
            ndProb3.ValueChanged += new System.EventHandler(this.ndProb3_ValueChanged);
            ndProb4.ValueChanged += new System.EventHandler(this.ndProb4_ValueChanged);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            int number = (int)ndNumExp.Value;
            int[] events = new int[5] { 0, 0, 0, 0, 0 };
            double[] freq = new double[5] { 0, 0, 0, 0, 0 };

            Random rnd = new Random();

            for (int i = 0; i < number; i++)
            {

                double s = rnd.NextDouble();


                if ((0 <= s) && (s < pfreq[0]))
                {
                    events[0] = events[0] + 1;
                }

                if ((pfreq[0] <= s) && (s < (pfreq[0] + pfreq[1])))
                {
                    events[1] = events[1] + 1;
                }

                if (((pfreq[0] + pfreq[1]) <= s) && (s < (pfreq[0] + pfreq[1] + pfreq[2])))
                {
                    events[2] = events[2] + 1;
                }

                if (((pfreq[0] + pfreq[1] + pfreq[2]) <= s) && (s < (pfreq[0] + pfreq[1] + pfreq[2] + pfreq[3])))
                {
                    events[3] = events[3] + 1;
                }

                if (((pfreq[0] + pfreq[1] + pfreq[2] + pfreq[3]) <= s) && (s < (pfreq[0] + pfreq[1] + pfreq[2] + pfreq[3] + pfreq[4])))
                {
                    events[4] = events[4] + 1;
                }

            }
            

            for (int i = 0; i < 5; i++)
            {
                freq[i] = (double)events[i] / number;
                chart1.Series[0].Points.AddXY(i, freq[i]);
            }

            double teoMean = 0; 
            double teoVar=0; 
            double statMean=0; 
            double statVar=0; 
            double ErMean=0; 
            double ErVar=0; 
            double Hi=0;

            //теор мат ожидание
            for (int i=0;i<5;i++)
            {
                teoMean = teoMean + (double)(pfreq[i] * (i+1));
            }

            lbTerMean.Text= Math.Round(teoMean, 3).ToString();

            // теор дисперсия
            double tmxq = 0;
            for (int i = 0; i < 5; i++)
            {
                tmxq = tmxq + (double)(pfreq[i] * (i + 1) * (i + 1));
            }
            teoVar = tmxq - teoMean * teoMean;

            lbTerVar.Text = Math.Round(teoVar, 3).ToString();


            //стат мат ожидание
            for (int i = 0; i < 5; i++)
            {
                statMean = statMean + (double)(freq[i] * (i + 1));
            }

            lbStatMean.Text = Math.Round(statMean, 3).ToString();

            //стат дисперсия
            double smxq = 0;
            for (int i = 0; i < 5; i++)
            {
                smxq = smxq + (double)(freq[i] * (i + 1) * (i + 1));
            }
            statVar = smxq - statVar * statVar;

            lbStatVar.Text = Math.Round(statVar, 3).ToString();

            // относительная погрешность мат. ожидания
            double abErMean = Math.Abs(statMean - teoMean);
            ErMean = abErMean / Math.Abs(teoMean);

            lbErMean.Text = Math.Round(ErMean, 3).ToString();

            // относительная погрешность мат. ожидания
            double abErVar = Math.Abs(statVar - teoVar);
            ErVar = abErVar / Math.Abs(teoVar);

            lbErVar.Text = Math.Round(ErVar, 3).ToString();

            //Хи-квадрат
            for (int i = 0; i < 5; i++)
            {
                double c;
                c = Math.Abs(freq[i] - pfreq[i]);
                if (c == 0)
                {
                    c = 1;
                }
                Hi = Hi + (double)(c * c) / pfreq[i];

            }
            lbHi.Text = Math.Round(Hi, 3).ToString();

            if (Hi > HiIdeal)
            {
                lbZnac.Text = ">";
                lbTF.Text = "true";
                lbTF.ForeColor = Color.Red;
            }
            else
            {
                lbZnac.Text = "<";
                lbTF.Text = "false";
                lbTF.ForeColor = Color.Green;
            }
        }



        private void ndProb1_ValueChanged(Object sender, EventArgs e)
        {
            pfreq[0] = (double)ndProb1.Value;
            prob5 = 1 - (pfreq[0] + pfreq[1] + pfreq[2] + pfreq[3]);
            lbProb5.Text = Math.Round(prob5, 3).ToString();
            if (prob5 < 0)
            {
                lbProb5.ForeColor = Color.Red;
                button3.Enabled = false;
            }
            else
            {
                lbProb5.ForeColor = Color.Black;
                button3.Enabled = true;
            }
            pfreq[4] = prob5;
        }

        private void ndProb2_ValueChanged(Object sender, EventArgs e)
        {
            pfreq[1] = (double)ndProb2.Value;
            prob5 = 1 - (pfreq[0] + pfreq[1] + pfreq[2] + pfreq[3]);
            lbProb5.Text = Math.Round(prob5, 3).ToString();
            if (prob5 < 0)
            {
                lbProb5.ForeColor = Color.Red;
                button3.Enabled = false;
            }
            else
            {
                lbProb5.ForeColor = Color.Black;
                button3.Enabled = true;
            }
            pfreq[4] = prob5;
        }

        private void ndProb3_ValueChanged(Object sender, EventArgs e)
        {
            pfreq[2] = (double)ndProb3.Value;
            prob5 = 1 - (pfreq[0] + pfreq[1] + pfreq[2] + pfreq[3]);
            lbProb5.Text = Math.Round(prob5, 3).ToString();
            if (prob5 < 0)
            {
                lbProb5.ForeColor = Color.Red;
                button3.Enabled = false;
            }
            else
            {
                lbProb5.ForeColor = Color.Black;
                button3.Enabled = true;
            }
            pfreq[4] = prob5;
        }

        private void ndProb4_ValueChanged(Object sender, EventArgs e)
        {
            pfreq[3] = (double)ndProb4.Value;
            prob5 = 1 - (pfreq[0] + pfreq[1] + pfreq[2] + pfreq[3]);
            lbProb5.Text = Math.Round(prob5, 3).ToString();
            if (prob5 < 0)
            {
                lbProb5.ForeColor = Color.Red;
                button3.Enabled = false;
            }
            else
            {
                lbProb5.ForeColor = Color.Black;
                button3.Enabled = true;
            }
            pfreq[4] = prob5;
        }
    }
}
//В большестве случаев, чем больше число эксперементов, тем меньше значение Хи-квадрат. Следовательно статистический результат более точно отражает теоритический результат. 
