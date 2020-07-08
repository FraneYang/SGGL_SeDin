using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace BLL
{
    public class DrawService
    {
        /// <summary>
        /// 获取施工图纸信息
        /// </summary>
        /// <param name="UnitWorkId"></param>
        /// <returns></returns>
        public static Model.Check_Draw GetDrawByDrawId(string DrawId)
        {
            return new Model.SGGLDB(Funs.ConnString).Check_Draw.FirstOrDefault(e => e.DrawId == DrawId);
        }
        /// <summary>
        /// 添加施工图纸信息
        /// </summary>
        /// <param name="WPQ"></param>
        public static void AddCheckDraw(Model.Check_Draw Draw)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Check_Draw newDraw = new Model.Check_Draw();
            newDraw.DrawId = Draw.DrawId;
            newDraw.ProjectId = Draw.ProjectId;
            newDraw.DrawCode = Draw.DrawCode;
            newDraw.DrawName = Draw.DrawName;
            newDraw.MainItem = Draw.MainItem;
            newDraw.DesignCN = Draw.DesignCN;
            newDraw.Edition = Draw.Edition;
            newDraw.AcceptDate = Draw.AcceptDate;
            newDraw.CompileMan = Draw.CompileMan;
            newDraw.CompileDate = Draw.CompileDate;
            newDraw.IsInvalid = Draw.IsInvalid;
            newDraw.Recover = Draw.Recover;
            db.Check_Draw.InsertOnSubmit(newDraw);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改施工图纸信息
        /// </summary>
        /// <param name="WPQ"></param>
        public static void UpdateCheckDraw(Model.Check_Draw Draw)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Check_Draw newDraw = db.Check_Draw.FirstOrDefault(e => e.DrawId == Draw.DrawId);
            if (newDraw != null)
            {
                newDraw.DrawId = Draw.DrawId;
                newDraw.ProjectId = Draw.ProjectId;
                newDraw.DrawCode = Draw.DrawCode;
                newDraw.DrawName = Draw.DrawName;
                newDraw.MainItem = Draw.MainItem;
                newDraw.DesignCN = Draw.DesignCN;
                newDraw.Edition = Draw.Edition;
                newDraw.AcceptDate = Draw.AcceptDate;
                newDraw.CompileMan = Draw.CompileMan;
                newDraw.CompileDate = Draw.CompileDate;
                newDraw.IsInvalid = Draw.IsInvalid;
                newDraw.Recover = Draw.Recover;
                db.SubmitChanges();
            }
        }
        /// <summary>
        /// 根据主键删除施工图纸信息
        /// </summary>
        /// <param name="checkerId"></param>
        public static void DeleteDrawById(string DrawId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Check_Draw Draw = db.Check_Draw.FirstOrDefault(e => e.DrawId == DrawId);
            if (Draw != null)
            {
                db.Check_Draw.DeleteOnSubmit(Draw);
                db.SubmitChanges();
            }
        }
        /// <summary>
        /// 获取主项下拉框
        /// </summary>
        /// <param name="projectId">项目Id</param>
        /// <returns></returns>
        public static void InitMainItemDropDownList(FineUIPro.DropDownList dropName, string projectId)
        {
            var q = (from x in new Model.SGGLDB(Funs.ConnString).ProjectData_MainItem where x.ProjectId == projectId orderby x.MainItemCode select x).ToList();
            dropName.DataValueField = "MainItemId";
            dropName.DataTextField = "MainItemName";
            dropName.DataSource = q;
            dropName.DataBind();
            Funs.FineUIPleaseSelect(dropName);
        }

        /// <summary>
        /// 获取设计专业下拉框
        /// </summary>
        /// <param name="dropName"></param>
        public static void InitDesignCNNameDropDownList(FineUIPro.DropDownList dropName)
        {
            var q = (from x in new Model.SGGLDB(Funs.ConnString).Base_DesignProfessional orderby x.DesignProfessionalCode select x).ToList();
            ListItem[] list = new ListItem[q.Count()];
            dropName.DataValueField = "DesignProfessionalId";
            dropName.DataTextField = "ProfessionalName";
            dropName.DataSource = q;
            dropName.DataBind();
            Funs.FineUIPleaseSelect(dropName);
        }
        public static List<Model.Check_Draw> GetDrawByProjectIdForApi(string name, string projectId, int index, int page)
        {
            List<string> codes = new List<string>();

            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                if (!string.IsNullOrEmpty(name))
                {
                    var qcn = from y in db.Base_DesignProfessional where y.ProfessionalName.Contains(name) select y.DesignProfessionalId;
                    codes = qcn.ToList();
                }
                var q = from x in db.Check_Draw
                        where x.ProjectId == projectId && (name == "" || x.DrawName.Contains(name) || x.DrawCode.Contains(name) || codes.Contains(x.DesignCN))
                        select new
                        {
                            x.DrawId,
                            x.ProjectId,
                            x.DrawCode,
                            x.DrawName,
                            x.MainItem,
                            x.DesignCN,
                            MainItemName = (from y in db.ProjectData_MainItem where y.MainItemId == x.MainItem select y.MainItemName).First(),
                            DesignCNName = (from y in db.Base_DesignProfessional where y.DesignProfessionalId == x.DesignCN select y.ProfessionalName).First(),
                            x.Edition,
                            x.AcceptDate,
                            x.CompileMan,
                            x.CompileDate,
                            x.IsInvalid,
                            x.Recover
                        };
                var list = q.Skip(index * page).Take(page).ToList();
                List<Model.Check_Draw> listRes = new List<Model.Check_Draw>();
                for (int i = 0; i < list.Count; i++)
                {
                    Model.Check_Draw x = new Model.Check_Draw();
                    x.DrawId = list[i].DrawId;
                    x.ProjectId = list[i].ProjectId;
                    x.DrawCode = list[i].DrawCode;
                    x.DrawName = list[i].DrawName;
                    x.MainItem = list[i].MainItem + "$" + list[i].MainItemName;
                    x.DesignCN = list[i].DesignCN + "$" + list[i].DesignCNName;
                    x.Edition = list[i].Edition;
                    x.AcceptDate = list[i].AcceptDate;
                    x.CompileMan = list[i].CompileMan;
                    x.IsInvalid = list[i].IsInvalid;
                    x.Recover = list[i].Recover;
                    x.CompileDate = list[i].CompileDate;
                    listRes.Add(x);
                }
                return listRes;
            }
        }
    }
}
