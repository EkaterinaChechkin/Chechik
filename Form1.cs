using Microsoft.SolverFoundation.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Рассчет_Чечкина
{
    public partial class Form1 : Form
    {
        Splav st = new Splav();

        public Form1()
        {
            InitializeComponent();
        }

        
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void bt_Ras_Click(object sender, EventArgs e)
        {
           if (
            #region---
            (tb_Cu1.Text == "") ||
            (tb_Cu2.Text == "") ||
            (tb_Sn1.Text == "") ||
            (tb_Sn2.Text == "") ||
            (tb_Zn1.Text == "") ||
            (tb_Zn2.Text == "") ||
            (tb_St1.Text == "") ||
            (tb_St2.Text == ""))
            #endregion---
            {
                Grafick.Parent = null;
                MessageBox.Show("Не все поля заполнены!", "Ошибка");
                return;
            }
           else
            {

                Grafick.Parent = tabControl1;

                chart1.Series[0].Points.Clear();
                chart1.Series[1].Points.Clear();

                st.Cu1 = Double.Parse(tb_Cu1.Text);
                st.Cu2 = Double.Parse(tb_Cu2.Text);
                st.Sn1 = Double.Parse(tb_Sn1.Text);
                st.Sn2 = Double.Parse(tb_Sn1.Text);
                st.Zn1 = Double.Parse(tb_Zn1.Text);
                st.Zn2 = Double.Parse(tb_Zn2.Text);
                st.St1 = Double.Parse(tb_St1.Text);
                st.St2 = Double.Parse(tb_St2.Text);
                

                List<SolverRow> solverList = new List<SolverRow>();
                solverList.Add(new SolverRow { xId = 1, Koef = st.Ras_Sp1 });
                solverList.Add(new SolverRow { xId = 2, Koef = st.Ras_Sp2 });


                SolverContext context = SolverContext.GetContext();
                Model model = context.CreateModel();
                Set users = new Set(Domain.Any, "users");

                Parameter Koef = new Parameter(Domain.Real, "Koef", users);
                Koef.SetBinding(solverList, "Koef", "xId");
                model.AddParameter(Koef);

                Decision choose = new Decision(Domain.RealNonnegative, "choose", users);
                model.AddDecisions(choose);
                model.AddGoal("goal", GoalKind.Minimize, Model.Sum(Model.ForEach(users, xId => choose[xId] * Koef[xId])));



                model.AddConstraint("Ogran1", Model.Sum(Model.ForEach(users, xId =>  (st.Cu1* st.Ras_Sp1 + st.Cu2 * st.Ras_Sp2)*0.01 ))<=2 );
                model.AddConstraint("Ogran2", Model.Sum(Model.ForEach(users, xId => (st.Sn1 * st.Ras_Sp1 + st.Sn2 * st.Ras_Sp2)*0.01)) <= 1000);
                model.AddConstraint("Ogran3", Model.Sum(Model.ForEach(users, xId => (st.Zn1 * st.Ras_Sp1 + st.Zn2 * st.Ras_Sp2) * 0.01)) <=12.8 );


                try
                {
                    Solution solution = context.Solve();
                    Report report = solution.GetReport();

                    String reportStr = "";

                    for (int i = 0; i < solverList.Count; i++)
                    {
                        reportStr += "Значение X" + (i + 1).ToString() + ": " + choose.GetDouble(solverList[i].xId) + "\n";
                    }
                    reportStr += "\n" + report.ToString();
                }

                catch (Exception ex)
                {
                    MessageBox.Show("При решении задачи оптимизации возникла исключительная ситуация.");
                }

                double Ras_Sp1 = Math.Round(choose.GetDouble(solverList[0].xId), 3);
                double Ras_Sp2 = Math.Round(choose.GetDouble(solverList[1].xId), 3);
              

                this.chart1.Series[0].Points.AddXY("", st.Ras_Sp1);
                this.chart1.Series[1].Points.AddXY("", st.Ras_Sp2);

                dataGridView1.Rows.Add(st.Ras_Sp1,st.Ras_Sp2,st.Ras_O_St);







            }
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            Grafick.Parent = null;
            tb_Cu1.Text = "10";
            st.Cu1 = 10d;
            tb_Cu2.Text = "10";
            st.Cu2 = 10d;
            tb_Sn1.Text = "10";
            st.Sn1 = 10d;
            tb_Sn2.Text = "30";
            st.Sn2 = 30d;
            tb_Zn1.Text = "80";
            st.Zn1 = 80d;
            tb_Zn2.Text = "60";
            st.Zn2 = 60d;
            tb_St1.Text = "4";
            st.St1 = 4d;
            tb_St2.Text = "6";
            st.St2 = 6d;

            st.Ras_Sp1 = 10.5d;
            st.Ras_Sp2 = 6.5d;
            st.Ras_O_St = 81d;

            tb_Kl_sp1.Text = "10.5";
            tb_Kl_sp2.Text = "6.5";
            tb_Ob.Text = "81";
        }
    }
}
