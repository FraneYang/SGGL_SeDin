using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
   public static class DepartService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 根据主键获取信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static Model.Base_Depart GetDepartById(string departId)
        {
            return new Model.SGGLDB(Funs.ConnString).Base_Depart.FirstOrDefault(e => e.DepartId == departId);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="?"></param>
        public static void AddDepart(Model.Base_Depart depart)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Base_Depart newDepart = new Model.Base_Depart
            {
                DepartId = depart.DepartId,
                DepartCode = depart.DepartCode,
                DepartName = depart.DepartName,
                Remark = depart.Remark
            };

            db.Base_Depart.InsertOnSubmit(newDepart);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="teamGroup"></param>
        public static void UpdateDepart(Model.Base_Depart depart)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Base_Depart newDepart = db.Base_Depart.FirstOrDefault(e => e.DepartId == depart.DepartId);
            if (newDepart != null)
            {
                newDepart.DepartCode = depart.DepartCode;
                newDepart.DepartName = depart.DepartName;
                newDepart.Remark = depart.Remark;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="departId"></param>
        public static void DeleteDepartById(string departId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Base_Depart depart = db.Base_Depart.FirstOrDefault(e => e.DepartId == departId);
            {
                db.Base_Depart.DeleteOnSubmit(depart);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 获取类别下拉项
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_Depart> GetDepartList()
        {
            var list = (from x in new Model.SGGLDB(Funs.ConnString).Base_Depart orderby x.DepartCode select x).ToList();
            return list;
        }

        /// <summary>
        /// 根据主键获取信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static string getDepartNameById(string departId)
        {
            string name = string.Empty;
            var dep= new Model.SGGLDB(Funs.ConnString).Base_Depart.FirstOrDefault(e => e.DepartId == departId);
            if (dep != null)
            {
                name = dep.DepartName;
            }
            return name;
        }

        #region 表下拉框
        /// <summary>
        ///  表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitDepartDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease)
        {
            dropName.DataValueField = "DepartId";
            dropName.DataTextField = "DepartName";
            dropName.DataSource = BLL.DepartService.GetDepartList();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        #endregion
    }
}