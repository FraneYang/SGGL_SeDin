using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace BLL
{
    public class Person_ShuntService
    {
        /// <summary>
        /// 添加分流管理
        /// </summary>
        /// <param name="Shunt"></param>
        public static void AddShunt(Model.Person_Shunt Shunt)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Person_Shunt newShunt = new Model.Person_Shunt();
            newShunt.ShuntId = Shunt.ShuntId;
            newShunt.Code = Shunt.Code;
            newShunt.ProjectId = Shunt.ProjectId;
            newShunt.State = Shunt.State;
            newShunt.CompileMan = Shunt.CompileMan;
            newShunt.CompileDate = Shunt.CompileDate;
            newShunt.SaveHandleMan = Shunt.SaveHandleMan;

            db.Person_Shunt.InsertOnSubmit(newShunt);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改分流管理
        /// </summary>
        /// <param name="Shunt"></param>
        public static void UpdateShunt(Model.Person_Shunt Shunt)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Person_Shunt newShunt = db.Person_Shunt.First(e => e.ShuntId == Shunt.ShuntId);
            newShunt.Code = Shunt.Code;
            newShunt.ProjectId = Shunt.ProjectId;
            newShunt.State = Shunt.State;
            newShunt.CompileMan = Shunt.CompileMan;
            newShunt.CompileDate = Shunt.CompileDate;
            newShunt.SaveHandleMan = Shunt.SaveHandleMan;

            db.SubmitChanges();
        }

        /// <summary>
        /// 根据分流管理Id删除一个分流管理信息
        /// </summary>
        /// <param name="ShuntId"></param>
        public static void DeleteShunt(string ShuntId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Person_Shunt Shunt = db.Person_Shunt.First(e => e.ShuntId == ShuntId);
            db.Person_Shunt.DeleteOnSubmit(Shunt);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据分流管理Id获取一个分流管理信息
        /// </summary>
        /// <param name="ShuntId"></param>
        public static Model.Person_Shunt GetShunt(string ShuntId)
        {
            return Funs.DB.Person_Shunt.FirstOrDefault(e => e.ShuntId == ShuntId);
        }

        /// <summary>
        /// 根据状态选择下一步办理类型
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public static ListItem[] GetDHandleTypeByState(string state)
        {
            if (state == Const.Shunt_Compile || state == Const.Shunt_ReCompile)
            {
                ListItem[] lis = new ListItem[1];
                lis[0] = new ListItem("审核", Const.Shunt_Audit);
                return lis;
            }
            else if (state == Const.Shunt_Audit)
            {
                ListItem[] lis = new ListItem[2];
                lis[0] = new ListItem("审批完成", Const.Shunt_Complete);
                lis[1] = new ListItem("重新编制", Const.Shunt_ReCompile);
                return lis;
            }
            else
                return null;
        }

        public static void Init(FineUIPro.DropDownList dropName, string state, bool isShowPlease)
        {
            dropName.DataValueField = "Value";
            dropName.DataTextField = "Text";
            dropName.DataSource = GetDHandleTypeByState(state);
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
    }
}
