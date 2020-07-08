using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Caching;
using System.Web.UI.WebControls;

namespace BLL
{
    public class CNProfessionalService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 获取实体集合
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_CNProfessional> GetList()
        {
            var q = (from x in new Model.SGGLDB(Funs.ConnString).Base_CNProfessional orderby x.SortIndex select x).ToList();
            return q;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="?"></param>
        public static void AddCNProfessional(Model.Base_CNProfessional cNProfessional)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Base_CNProfessional newCNProfessional = new Model.Base_CNProfessional
            {
                CNProfessionalId = cNProfessional.CNProfessionalId,
                CNProfessionalCode = cNProfessional.CNProfessionalCode,
                ProfessionalName = cNProfessional.ProfessionalName,
                SortIndex = cNProfessional.SortIndex
            };

            db.Base_CNProfessional.InsertOnSubmit(newCNProfessional);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="teamGroup"></param>
        public static void UpdateCNProfessional(Model.Base_CNProfessional cNProfessional)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Base_CNProfessional newCNProfessional = db.Base_CNProfessional.FirstOrDefault(e => e.CNProfessionalId == cNProfessional.CNProfessionalId);
            if (newCNProfessional != null)
            {
                newCNProfessional.CNProfessionalCode = cNProfessional.CNProfessionalCode;
                newCNProfessional.ProfessionalName = cNProfessional.ProfessionalName;
                newCNProfessional.SortIndex = cNProfessional.SortIndex;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="cNProfessionalId"></param>
        public static void DeleteCNProfessionalById(string cNProfessionalId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Base_CNProfessional cNProfessional = db.Base_CNProfessional.FirstOrDefault(e => e.CNProfessionalId == cNProfessionalId);
            {
                db.Base_CNProfessional.DeleteOnSubmit(cNProfessional);
                db.SubmitChanges();
            }
        }
        public static void InitCNProfessionalDownList(FineUIPro.DropDownList dropName, bool isShowPlease)
        {
            dropName.DataValueField = "Value";
            dropName.DataTextField = "Text";
            dropName.DataSource = GetCNProfessionalItem();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        /// <summary>
        /// 专业下拉框
        /// </summary>
        /// <param name="dropName"></param>
        /// <param name="projectId"></param>
        /// <param name="isShowPlease"></param>
        public static void InitCNProfessional(FineUIPro.DropDownList dropName, bool isShowPlease)
        {
            dropName.DataValueField = "Text";
            dropName.DataTextField = "Text";
            dropName.DataSource = GetCNProfessionalItem();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        /// <summary>
        /// 获取专业集合
        /// </summary>
        /// <returns></returns>
        public static ListItem[] GetCNProfessionalItem()
        {
            var q = (from x in new Model.SGGLDB(Funs.ConnString).Base_CNProfessional orderby x.SortIndex select x).ToList();
            ListItem[] list = new ListItem[q.Count()];
            for (int i = 0; i < q.Count(); i++)
            {
                list[i] = new ListItem(q[i].ProfessionalName ?? "", q[i].CNProfessionalId);
            }
            return list;
        }

        /// <summary>
        /// 获取一个专业信息
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public static Model.Base_CNProfessional GetCNProfessional(string CNProfessionalId)
        {
            return new Model.SGGLDB(Funs.ConnString).Base_CNProfessional.FirstOrDefault(e => e.CNProfessionalId == CNProfessionalId);
        }


        public static string GetCNProfessionalNameByCode(string cnProfessionalCode)
        {
            string res = "";
            if (!string.IsNullOrEmpty(cnProfessionalCode))
            {
                string[] codes = cnProfessionalCode.Split(',');
                var list = new Model.SGGLDB(Funs.ConnString).Base_CNProfessional.Where(x => codes.Contains(x.CNProfessionalId));
                foreach (var item in list)
                {
                    res += item.ProfessionalName + ",";
                }
                if (!String.IsNullOrEmpty(res))
                {
                    return res.Substring(0, res.Length - 1);
                }
            }
            return res;
        }
    }
}
