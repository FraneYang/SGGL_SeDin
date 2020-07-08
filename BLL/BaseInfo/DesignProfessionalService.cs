using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Caching;
using System.Web.UI.WebControls;

namespace BLL
{
    public class DesignProfessionalService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 获取实体集合
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_DesignProfessional> GetList()
        {
            var q = (from x in new Model.SGGLDB(Funs.ConnString).Base_DesignProfessional orderby x.SortIndex select x).ToList();
            return q;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="?"></param>
        public static void AddDesignProfessional(Model.Base_DesignProfessional designProfessional)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Base_DesignProfessional newDesignProfessional = new Model.Base_DesignProfessional
            {
                DesignProfessionalId = designProfessional.DesignProfessionalId,
                DesignProfessionalCode = designProfessional.DesignProfessionalCode,
                ProfessionalName = designProfessional.ProfessionalName,
                SortIndex = designProfessional.SortIndex
            };

            db.Base_DesignProfessional.InsertOnSubmit(newDesignProfessional);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="teamGroup"></param>
        public static void UpdateDesignProfessional(Model.Base_DesignProfessional designProfessional)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Base_DesignProfessional newDesignProfessional = db.Base_DesignProfessional.FirstOrDefault(e => e.DesignProfessionalId == designProfessional.DesignProfessionalId);
            if (newDesignProfessional != null)
            {
                newDesignProfessional.DesignProfessionalCode = designProfessional.DesignProfessionalCode;
                newDesignProfessional.ProfessionalName = designProfessional.ProfessionalName;
                newDesignProfessional.SortIndex = designProfessional.SortIndex;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="designProfessionalId"></param>
        public static void DeleteDesignProfessionalById(string designProfessionalId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Base_DesignProfessional designProfessional = db.Base_DesignProfessional.FirstOrDefault(e => e.DesignProfessionalId == designProfessionalId);
            {
                db.Base_DesignProfessional.DeleteOnSubmit(designProfessional);
                db.SubmitChanges();
            }
        }
        public static void InitDesignProfessionalDownList(FineUIPro.DropDownList dropName, bool isShowPlease)
        {
            dropName.DataValueField = "Value";
            dropName.DataTextField = "Text";
            dropName.DataSource = GetDesignProfessionalItem();
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
        public static void InitDesignProfessional(FineUIPro.DropDownList dropName, string projectId, bool isShowPlease)
        {
            dropName.DataValueField = "Text";
            dropName.DataTextField = "Text";
            dropName.DataSource = GetDesignProfessionalItem();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        /// <summary>
        /// 获取设计专业集合
        /// </summary>
        /// <returns></returns>
        public static ListItem[] GetDesignProfessionalItem()
        {
            var q = (from x in new Model.SGGLDB(Funs.ConnString).Base_DesignProfessional orderby x.SortIndex select x).ToList();
            ListItem[] list = new ListItem[q.Count()];
            for (int i = 0; i < q.Count(); i++)
            {
                list[i] = new ListItem(q[i].ProfessionalName ?? "", q[i].DesignProfessionalId);
            }
            return list;
        }
        /// <summary>
        /// 获取一个专业大项信息
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public static Model.Base_DesignProfessional GetDesignProfessional(string DesignProfessionalId)
        {
            return new Model.SGGLDB(Funs.ConnString).Base_DesignProfessional.FirstOrDefault(e => e.DesignProfessionalId == DesignProfessionalId);
        }
        public static string GetDesignProfessionalNameByCode(string cnProfessionalCode)
        {
            string res = "";
            if (!string.IsNullOrEmpty(cnProfessionalCode))
            {
                string[] codes = cnProfessionalCode.Split(',');
                var list = new Model.SGGLDB(Funs.ConnString).Base_DesignProfessional.Where(x => codes.Contains(x.DesignProfessionalCode));
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
