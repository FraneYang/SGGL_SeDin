using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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