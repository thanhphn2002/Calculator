using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace _2051012108
{
    public partial class Form3 : Form
    {
        String bieuThuc, res;

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
            } 
            else
                lbRes.Text += userClick;
        }

        private String calculate()
        {
            DataTable dt = new DataTable();
            bieuThuc = bieuThuc.Replace('x', '*');

            try
            {
                var temp = checked(dt.Compute(bieuThuc, ""));
                res = String.Format("{0:0.###}", temp);
            
                if (res.Equals("∞"))
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
    }
}
