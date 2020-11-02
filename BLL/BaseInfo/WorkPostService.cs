using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace BLL
{
    public static class WorkPostService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static Model.Base_WorkPost GetWorkPostById(string workPostId)
        {
            return Funs.DB.Base_WorkPost.FirstOrDefault(e => e.WorkPostId == workPostId);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="?"></param>
        public static void AddWorkPost(Model.Base_WorkPost workPost)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_WorkPost newWorkPost = new Model.Base_WorkPost
            {
                WorkPostId = workPost.WorkPostId,
                WorkPostCode = workPost.WorkPostCode,
                WorkPostName = workPost.WorkPostName,
                PostType = workPost.PostType,
                IsHsse = workPost.IsHsse,
                CNCodes = workPost.CNCodes,
                Remark = workPost.Remark
            };

            db.Base_WorkPost.InsertOnSubmit(newWorkPost);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="teamGroup"></param>
        public static void UpdateWorkPost(Model.Base_WorkPost workPost)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_WorkPost newWorkPost = db.Base_WorkPost.FirstOrDefault(e => e.WorkPostId == workPost.WorkPostId);
            if (newWorkPost != null)
            {
                newWorkPost.WorkPostCode = workPost.WorkPostCode;
                newWorkPost.WorkPostName = workPost.WorkPostName;
                newWorkPost.PostType = workPost.PostType;
                newWorkPost.IsHsse = workPost.IsHsse;
                newWorkPost.CNCodes = workPost.CNCodes;
                newWorkPost.Remark = workPost.Remark;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="workPostId"></param>
        public static void DeleteWorkPostById(string workPostId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_WorkPost workPost = db.Base_WorkPost.FirstOrDefault(e => e.WorkPostId == workPostId);
            {
                db.Base_WorkPost.DeleteOnSubmit(workPost);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 获取类别下拉项
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_WorkPost> GetWorkPostList()
        {
            var list = (from x in Funs.DB.Base_WorkPost orderby x.WorkPostCode select x).ToList();
            return list;
        }

        /// <summary>
        /// 获取类别下拉项
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_WorkPost> GetWorkPostListByType(string postType)
        {
            var list = (from x in Funs.DB.Base_WorkPost where x.PostType == postType orderby x.WorkPostCode select x).ToList();
            return list;
        }

        /// <summary>
        /// 获取本部岗位下拉项
        /// </summary>
        /// <returns></returns>
        public static ListItem[] GetMainWorkPostList()
        {
            ListItem[] lis = new ListItem[11];
            lis[0] = new ListItem("施工经理", "1");
            lis[1] = new ListItem("安全经理", "2");
            lis[2] = new ListItem("质量经理", "3");
            lis[3] = new ListItem("试车经理", "4");
            lis[4] = new ListItem("施工专业工程师", "5");
            lis[5] = new ListItem("安全专业工程师", "6");
            lis[6] = new ListItem("质量专业工程师", "7");
            lis[7] = new ListItem("试车专业工程师", "8");
            lis[8] = new ListItem("本部综合管理工程师", "9");
            lis[9] = new ListItem("本部合同管理工程师", "10");
            lis[10] = new ListItem("本部安全质量工程师", "11");
            return lis;
        }

        #region 表下拉框
        /// <summary>
        ///  表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitWorkPostDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease)
        {
            dropName.DataValueField = "WorkPostId";
            dropName.DataTextField = "WorkPostName";
            dropName.DataSource = BLL.WorkPostService.GetWorkPostList();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }

        /// <summary>
        ///  表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitWorkPostNameDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease)
        {
            dropName.DataValueField = "WorkPostName";
            dropName.DataTextField = "WorkPostName";
            dropName.DataSource = BLL.WorkPostService.GetWorkPostList();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }

        /// <summary>
        ///  表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitWorkPostNameByTypeDropDownList(FineUIPro.DropDownList dropName, string postType, bool isShowPlease)
        {
            dropName.DataValueField = "WorkPostName";
            dropName.DataTextField = "WorkPostName";
            dropName.DataSource = BLL.WorkPostService.GetWorkPostListByType(postType);
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }

        /// <summary>
        ///  表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitWorkPostNameByTypeDropDownList2(FineUIPro.DropDownList dropName, string postType, bool isShowPlease)
        {
            dropName.DataValueField = "WorkPostId";
            dropName.DataTextField = "WorkPostName";
            dropName.DataSource = BLL.WorkPostService.GetWorkPostListByType(postType);
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }

        /// <summary>
        ///  表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitMainWorkPostDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease)
        {
            dropName.DataValueField = "Value";
            dropName.DataTextField = "Text";
            dropName.DataSource = BLL.WorkPostService.GetMainWorkPostList();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        #endregion

        #region 根据岗位ID得到岗位名称
        /// <summary>
        /// 根据岗位ID得到岗位名称
        /// </summary>
        /// <param name="workPostId"></param>
        /// <returns></returns>
        public static string getWorkPostNameById(string workPostId)
        {
            string workPostName = string.Empty;
            if (!string.IsNullOrEmpty(workPostId))
            {
                var q = GetWorkPostById(workPostId);
                if (q != null)
                {
                    workPostName = q.WorkPostName;
                }
            }

            return workPostName;
        }
        #endregion

        #region 根据多岗位ID得到岗位名称字符串
        /// <summary>
        /// 根据多岗位ID得到岗位名称字符串
        /// </summary>
        /// <param name="bigType"></param>
        /// <returns></returns>
        public static string getWorkPostNamesWorkPostIds(object workPostIds)
        {
            string workPostName = string.Empty;
            if (workPostIds != null)
            {
                string[] ids = workPostIds.ToString().Split(',');
                foreach (string id in ids)
                {
                    var q = GetWorkPostById(id);
                    if (q != null)
                    {
                        workPostName += q.WorkPostName + ",";
                    }
                }
                if (workPostName != string.Empty)
                {
                    workPostName = workPostName.Substring(0, workPostName.Length - 1); ;
                }
            }

            return workPostName;
        }
        #endregion
    }
}