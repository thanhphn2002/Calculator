using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2051012108
{
    public partial class Form3 : Form
    {
        String bieuThuc, res;
        bool equalClick = false;

        public Form3()
        {
            InitializeComponent();
        }

        private void bt0_Click(object sender, EventArgs e)
        {
            object userClick = (sender as Button).Text;

            if (userClick.Equals("Ac"))
                lbRes.Text = "";
            else if (userClick.Equals("Del"))
            {
                if (lbRes.Text.Length > 0)
                    lbRes.Text = lbRes.Text.Remove(lbRes.Text.Length - 1);
            }
            else if (userClick.Equals("="))
            {
                bieuThuc = lbRes.Text;
                lbRes.Text = calculate();

                if (lbRes.Text.Equals("MATH ERROR") || lbRes.Text.Equals("OVER FLOW"))
                {
                    delay(700);
                    lbRes.Text = "";
                }
                else
                    equalClick = true;
            }
            else
            {
                if (equalClick)
                {
                    lbRes.Text = "";
                    equalClick = false;
                }

                lbRes.Text += userClick;
            }
        }

        private String calculate()
        {
            DataTable dt = new DataTable();
            bieuThuc = bieuThuc.Replace('x', '*');

            try
            {
                var temp = checked(dt.Compute(bieuThuc, ""));
                res = String.Format("{0:0.###}", temp);

                if (res.Equals("∞") || res.Equals("-∞") || res.Equals("NaN"))
                    throw new DivideByZeroException("MATH ERROR");
            }
            catch (System.Data.SyntaxErrorException)
            {
                res = "MATH ERROR";
            }
            catch (System.Data.EvaluateException)
            {
                res = "MATH ERROR";
            }
            catch (DivideByZeroException ex)
            {
                res = ex.Message;
            }
            catch (OverflowException)
            {
                res = "OVER FLOW";
            }

            return res;
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Are you sure to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Stop) == DialogResult.No)
                e.Cancel = true;
        }

        private void delay(int timeDelay)
        {
            // Using Thread Sleep
            Thread.Sleep(timeDelay);

            // Using Task Delay
            Task.Delay(timeDelay).Wait();
        }
    }
}
