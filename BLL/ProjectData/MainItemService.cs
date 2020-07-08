using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace BLL
{
    public class MainItemService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 记录数
        /// </summary>
        private static int count
        {
            get;
            set;
        }

        /// <summary>
        /// 定义变量
        /// </summary>
        private static IQueryable<Model.ProjectData_MainItem> qq = from x in db.ProjectData_MainItem select x;


        /// <summary>
        /// 获取列表数
        /// </summary>
        /// <returns></returns>
        public static int getListCount(string searchItem, string searchValue, string projectId)
        {
            return count;
        }


        /// <summary>
        /// 根据主项和单位工程对应关系Id获取一个主项和单位工程对应关系信息
        /// </summary>
        /// <param name="mainItemId">主项和单位工程对应关系Id</param>
        /// <returns>一个主项和单位工程对应关系实体</returns>
        public static Model.ProjectData_MainItem GetMainItemByMainItemId(string mainItemId)
        {
            return new Model.SGGLDB(Funs.ConnString).ProjectData_MainItem.First(x => x.MainItemId == mainItemId);
        }

        /// <summary>
        /// 是否存在主项和单位工程对应关系
        /// </summary>
        /// <param name="postName"></param>
        /// <returns>true-存在，false-不存在</returns>
        public static bool IsExistMainItem(string mainItemCode, string projectId)
        {
            var q = from x in new Model.SGGLDB(Funs.ConnString).ProjectData_MainItem where x.MainItemCode == mainItemCode && x.ProjectId == projectId select x;
            if (q.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 增加主项和单位工程对应关系信息
        /// </summary>
        /// <param name="mainItemToUnitWork">主项和单位工程对应关系实体</param>
        public static void AddMainItem(Model.ProjectData_MainItem mainItemToUnitWork)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            string newKeyID = SQLHelper.GetNewID(typeof(Model.ProjectData_MainItem));
            Model.ProjectData_MainItem newMainItem = new Model.ProjectData_MainItem();
            newMainItem.MainItemId = newKeyID;
            newMainItem.ProjectId = mainItemToUnitWork.ProjectId;
            newMainItem.MainItemCode = mainItemToUnitWork.MainItemCode;
            newMainItem.MainItemName = mainItemToUnitWork.MainItemName;
            newMainItem.UnitWorkIds = mainItemToUnitWork.UnitWorkIds;
            newMainItem.Remark = mainItemToUnitWork.Remark;

            db.ProjectData_MainItem.InsertOnSubmit(newMainItem);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改主项和单位工程对应关系信息
        /// </summary>
        /// <param name="mainItemToUnitWork">主项和单位工程对应关系实体</param>
        public static void UpdateMainItem(Model.ProjectData_MainItem mainItemToUnitWork)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.ProjectData_MainItem newMainItem = db.ProjectData_MainItem.First(e => e.MainItemId == mainItemToUnitWork.MainItemId);
            newMainItem.MainItemCode = mainItemToUnitWork.MainItemCode;
            newMainItem.MainItemName = mainItemToUnitWork.MainItemName;
            newMainItem.UnitWorkIds = mainItemToUnitWork.UnitWorkIds;
            newMainItem.Remark = mainItemToUnitWork.Remark;

            db.SubmitChanges();
        }

        /// <summary>
        /// 根据主项和单位工程对应关系Id删除一个主项和单位工程对应关系信息
        /// </summary>
        /// <param name="mainItemId">主项和单位工程对应关系Id</param>
        public static void DeleteMainItemByMainItemId(string mainItemId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.ProjectData_MainItem mainItemToUnitWork = db.ProjectData_MainItem.First(e => e.MainItemId == mainItemId);

            db.ProjectData_MainItem.DeleteOnSubmit(mainItemToUnitWork);
            db.SubmitChanges();
        }

        /// <summary>
        /// 获取主项名称项
        /// </summary>
        /// <param name="projectId">项目Id</param>
        /// <returns></returns>
        public static ListItem[] GetMainItemList(string projectId)
        {
            var q = (from x in new Model.SGGLDB(Funs.ConnString).ProjectData_MainItem where x.ProjectId == projectId orderby x.MainItemCode select x).ToList();
            ListItem[] item = new ListItem[q.Count()];
            for (int i = 0; i < q.Count(); i++)
            {
                item[i] = new ListItem((q[i].MainItemCode + "-" + q[i].MainItemName) ?? "", q[i].MainItemId.ToString());
            }
            return item;
        }
        /// <summary>
        /// 主项名称下拉框
        /// </summary>
        /// <param name="dropName"></param>
        /// <param name="projectId"></param>
        /// <param name="isShowPlease"></param>
        public static void InitMainItemDownList(FineUIPro.DropDownList dropName, string projectId, bool isShowPlease)
        {
            dropName.DataValueField = "Value";
            dropName.DataTextField = "Text";
            dropName.DataSource = GetMainItemList(projectId);
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        /// <summary>
        /// 根据项目id 获取 
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="name"></param>
        /// <param name="unitWorks"></param>
        /// <returns></returns>
        public static List<Model.ProjectData_MainItem> GetMainItemList(string projectId, string name, string unitWorks)
        {
            var q = (from x in new Model.SGGLDB(Funs.ConnString).ProjectData_MainItem
                     where x.ProjectId == projectId && (name == "" || x.MainItemName.Contains(name)) && (unitWorks == "" || x.UnitWorkIds == unitWorks)
                     orderby x.MainItemCode
                     select x).ToList();
            List<Model.ProjectData_MainItem> res = new List<Model.ProjectData_MainItem>();
            for (int i = 0; i < q.Count(); i++)
            {
                Model.ProjectData_MainItem w = new Model.ProjectData_MainItem();
                w.MainItemCode = q[i].MainItemCode;
                w.MainItemId = q[i].MainItemId;
                w.MainItemName = q[i].MainItemName;
                res.Add(w);
            }
            return res;
        }
    }
}
