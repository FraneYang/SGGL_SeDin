using BLL;
using System;
using System.Linq;

namespace FineUIPro.Web.HSSE.Solution
{
    public partial class ShowIndexToLaw : PageBase
    {
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            string toLawIndex = Request.Params["ToLawIndex"];
            if (!string.IsNullOrEmpty(toLawIndex))
            {
                IQueryable<Model.Law_HSSEStandardsList> q = from x in new Model.SGGLDB(Funs.ConnString).Law_HSSEStandardsList orderby x.StandardNo select x;
                if (toLawIndex == "1")
                {
                    q = q.Where(y => y.IsSelected1 == true);
                }

                if (toLawIndex == "2")
                {
                    q = q.Where(y => y.IsSelected2 == true);
                }

                if (toLawIndex == "3")
                {
                    q = q.Where(y => y.IsSelected3 == true);
                }

                if (toLawIndex == "4")
                {
                    q = q.Where(y => y.IsSelected4 == true);
                }

                if (toLawIndex == "5")
                {
                    q = q.Where(y => y.IsSelected5 == true);
                }

                if (toLawIndex == "6")
                {
                    q = q.Where(y => y.IsSelected6 == true);
                }

                if (toLawIndex == "7")
                {
                    q = q.Where(y => y.IsSelected7 == true);
                }

                if (toLawIndex == "8")
                {
                    q = q.Where(y => y.IsSelected8 == true);
                }

                if (toLawIndex == "9")
                {
                    q = q.Where(y => y.IsSelected9 == true);
                }

                if (toLawIndex == "10")
                {
                    q = q.Where(y => y.IsSelected10 == true);
                }

                if (toLawIndex == "11")
                {
                    q = q.Where(y => y.IsSelected11 == true);
                }

                if (toLawIndex == "12")
                {
                    q = q.Where(y => y.IsSelected12 == true);
                }

                if (toLawIndex == "13")
                {
                    q = q.Where(y => y.IsSelected13 == true);
                }

                if (toLawIndex == "14")
                {
                    q = q.Where(y => y.IsSelected14 == true);
                }

                if (toLawIndex == "15")
                {
                    q = q.Where(y => y.IsSelected15 == true);
                }

                if (toLawIndex == "16")
                {
                    q = q.Where(y => y.IsSelected16 == true);
                }

                if (toLawIndex == "17")
                {
                    q = q.Where(y => y.IsSelected17 == true);
                }

                if (toLawIndex == "18")
                {
                    q = q.Where(y => y.IsSelected18 == true);
                }

                if (toLawIndex == "19")
                {
                    q = q.Where(y => y.IsSelected19 == true);
                }

                if (toLawIndex == "20")
                {
                    q = q.Where(y => y.IsSelected20 == true);
                }

                if (toLawIndex == "21")
                {
                    q = q.Where(y => y.IsSelected21 == true);
                }

                if (toLawIndex == "22")
                {
                    q = q.Where(y => y.IsSelected22 == true);
                }

                if (toLawIndex == "23")
                {
                    q = q.Where(y => y.IsSelected23 == true);
                }

                if (toLawIndex == "90")
                {
                    q = q.Where(y => y.IsSelected90 == true);
                }
                this.Grid1.DataSource = q;
                this.Grid1.DataBind();
            }

            IQueryable<Model.Law_LawRegulationList> laws = from x in new Model.SGGLDB(Funs.ConnString).Law_LawRegulationList orderby x.LawRegulationCode select x;
            this.Grid2.DataSource = laws;
            this.Grid2.DataBind();
        }


        protected string ConvertLawsRegulationsType(object lawsRegulationsTypeId)
        {
            if (lawsRegulationsTypeId != null)
            {
                var lawsRegulationsType = BLL.LawsRegulationsTypeService.GetLawsRegulationsTypeById(lawsRegulationsTypeId.ToString());
                if (lawsRegulationsType!=null)
                {
                    return lawsRegulationsType.Name;
                }
            }
            return null;
        }
    }
}