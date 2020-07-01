using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
   public static class PostTitleService
   {
       public static Model.SGGLDB db = Funs.DB;

       /// <summary>
       /// 根据主键获取信息
       /// </summary>
       /// <param name="groupId"></param>
       /// <returns></returns>
       public static Model.Base_PostTitle GetPostTitleById(string postTitleId)
       {
           return Funs.DB.Base_PostTitle.FirstOrDefault(e => e.PostTitleId == postTitleId);
       }

       /// <summary>
       /// 根据名称获取信息
       /// </summary>
       /// <param name="groupId"></param>
       /// <returns></returns>
       public static Model.Base_PostTitle GetPostTitleByName(string postTitleName)
       {
           return Funs.DB.Base_PostTitle.FirstOrDefault(e => e.PostTitleName == postTitleName);
       }

       /// <summary>
       /// 添加职称信息
       /// </summary>
       /// <param name="?"></param>
       public static void AddPostTitle(Model.Base_PostTitle postTitle)
       {
           Model.SGGLDB db = Funs.DB;
            Model.Base_PostTitle newPostTitle = new Model.Base_PostTitle
            {
                PostTitleId = postTitle.PostTitleId,
                PostTitleCode = postTitle.PostTitleCode,
                PostTitleName = postTitle.PostTitleName,
                Remark = postTitle.Remark
            };

            db.Base_PostTitle.InsertOnSubmit(newPostTitle);
           db.SubmitChanges();
       }

       /// <summary>
       /// 修改职称信息
       /// </summary>
       /// <param name="teamGroup"></param>
       public static void UpdatePostTitle(Model.Base_PostTitle postTitle)
       {
           Model.SGGLDB db = Funs.DB;
           Model.Base_PostTitle newPostTitle = db.Base_PostTitle.FirstOrDefault(e => e.PostTitleId == postTitle.PostTitleId);
           if (newPostTitle != null)
           {
               newPostTitle.PostTitleCode = postTitle.PostTitleCode;
               newPostTitle.PostTitleName = postTitle.PostTitleName;
               newPostTitle.Remark = postTitle.Remark;
               db.SubmitChanges();
           }
       }

       /// <summary>
       /// 根据职称主键删除对应职称信息
       /// </summary>
       /// <param name="postTitleId"></param>
       public static void DeletePostTitleById(string postTitleId)
       {
           Model.SGGLDB db = Funs.DB;
           Model.Base_PostTitle postTitle = db.Base_PostTitle.FirstOrDefault(e => e.PostTitleId == postTitleId);
           {
               db.Base_PostTitle.DeleteOnSubmit(postTitle);
               db.SubmitChanges();
           }
       }

       /// <summary>
       /// 获取类别下拉项
       /// </summary>
       /// <returns></returns>
       public static List<Model.Base_PostTitle> GetPostTitleList()
       {
           var list = (from x in Funs.DB.Base_PostTitle orderby x.PostTitleCode select x).ToList();
           return list;
       }

       /// <summary>
       /// 获取职称下拉选项
       /// </summary>
       /// <returns></returns>
       public static List<Model.Base_PostTitle> GetPostTitleDropDownList()
       {
           var list = (from x in Funs.DB.Base_PostTitle orderby x.PostTitleCode select x).ToList();           
           return list;
       }

       #region 表下拉框
       /// <summary>
       ///  表下拉框
       /// </summary>
       /// <param name="dropName">下拉框名字</param>
       /// <param name="isShowPlease">是否显示请选择</param>
       public static void InitPostTitleDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease)
       {
           dropName.DataValueField = "PostTitleId";
           dropName.DataTextField = "PostTitleName";
           dropName.DataSource = BLL.PostTitleService.GetPostTitleList();
           dropName.DataBind();
           if (isShowPlease)
           {
               Funs.FineUIPleaseSelect(dropName);
           }
       }
       #endregion
   }
}