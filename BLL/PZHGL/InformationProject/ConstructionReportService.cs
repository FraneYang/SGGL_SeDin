using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace BLL
{
    /// <summary>
    /// 总承包商施工报告
    /// </summary>
    public static class ConstructionReportService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取总承包商施工报告
        /// </summary>
        /// <param name="ConstructionReportId"></param>
        /// <returns></returns>
        public static Model.ZHGL_ConstructionReport GetConstructionReportById(string ConstructionReportId)
        {
            return Funs.DB.ZHGL_ConstructionReport.FirstOrDefault(e => e.ConstructionReportId == ConstructionReportId);
        }

        /// <summary>
        /// 添加总承包商施工报告
        /// </summary>
        /// <param name="ConstructionReport"></param>
        public static void AddConstructionReport(Model.ZHGL_ConstructionReport ConstructionReport)
        {
            Model.SGGLDB db = Funs.DB;
            Model.ZHGL_ConstructionReport newConstructionReport = new Model.ZHGL_ConstructionReport
            {
                ConstructionReportId = ConstructionReport.ConstructionReportId,
                ProjectId = ConstructionReport.ProjectId,
                Code = ConstructionReport.Code,
                FileType = ConstructionReport.FileType,
                Content = ConstructionReport.Content,
                CompileMan = ConstructionReport.CompileMan,
                CompileDate = ConstructionReport.CompileDate,
                State = ConstructionReport.State,
            };
            db.ZHGL_ConstructionReport.InsertOnSubmit(newConstructionReport);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改总承包商施工报告
        /// </summary>
        /// <param name="ConstructionReport"></param>
        public static void UpdateConstructionReport(Model.ZHGL_ConstructionReport ConstructionReport)
        {
            Model.SGGLDB db = Funs.DB;
            Model.ZHGL_ConstructionReport newConstructionReport = db.ZHGL_ConstructionReport.FirstOrDefault(e => e.ConstructionReportId == ConstructionReport.ConstructionReportId);
            if (newConstructionReport != null)
            {
                newConstructionReport.Code = ConstructionReport.Code;
                newConstructionReport.Content = ConstructionReport.Content;
                newConstructionReport.State = ConstructionReport.State;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除总承包商施工报告
        /// </summary>
        /// <param name="ConstructionReportId"></param>
        public static void DeleteConstructionReportById(string ConstructionReportId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.ZHGL_ConstructionReport ConstructionReport = db.ZHGL_ConstructionReport.FirstOrDefault(e => e.ConstructionReportId == ConstructionReportId);
            if (ConstructionReport != null)
            {
                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(ConstructionReport.ConstructionReportId);
                db.ZHGL_ConstructionReport.DeleteOnSubmit(ConstructionReport);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 把状态转换代号为文字形式
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public static string ConvertState(object state)
        {
            if (state != null)
            {
                if (state.ToString() == BLL.Const.ConstructionReport_ReCompile)
                {
                    return "重新编制";
                }
                else if (state.ToString() == BLL.Const.ConstructionReport_Compile)
                {
                    return "编制";
                }
                else if (state.ToString() == BLL.Const.ConstructionReport_Audit1)
                {
                    return "施工经理批准";
                }
                else if (state.ToString() == BLL.Const.ConstructionReport_Complete)
                {
                    return "审批完成";
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 获取文件类型
        /// </summary>
        /// <returns></returns>
        public static ListItem[] GetFileTypeList()
        {
            ListItem[] lis = new ListItem[5];
            lis[0] = new ListItem("施工周报告", "1");
            lis[1] = new ListItem("施工月报告", "2");
            lis[2] = new ListItem("施工开工报告", "3");
            lis[3] = new ListItem("施工完工报告", "4");
            lis[4] = new ListItem("施工专题报告", "5");
            return lis;
        }

        /// <summary>
        /// 获取文件类型
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public static string ConvertFileType(object FileType)
        {
            string name = string.Empty;
            if (FileType != null)
            {
                var fileType = GetFileTypeList().FirstOrDefault(x => x.Value == FileType.ToString());
                if (fileType != null)
                {
                    name = fileType.Text;
                }
            }
            return name;
        }

        //<summary>
        //获取办理人姓名
        //</summary>
        //<param name="state"></param>
        //<returns></returns>
        public static string ConvertMan(object ConstructionReportId)
        {
            if (ConstructionReportId != null)
            {
                Model.ZHGL_ConstructionReportApprove a = ConstructionReportApproveService.GetConstructionReportApproveByConstructionReportId(ConstructionReportId.ToString());
                if (a != null)
                {
                    if (a.ApproveMan != null)
                    {
                        return UserService.GetUserByUserId(a.ApproveMan).UserName;
                    }
                }
                else
                {
                    return "";
                }
            }
            return "";
        }

        public static void InitHandleType(FineUIPro.DropDownList dropName, bool isShowPlease, string state)
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

        /// <summary>
        /// 根据状态选择下一步办理类型
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public static ListItem[] GetDHandleTypeByState(string state)
        {
            if (state == Const.ConstructionReport_Compile || state == Const.ConstructionReport_ReCompile)  //无是否同意
            {
                ListItem[] lis = new ListItem[1];
                lis[0] = new ListItem("施工经理批准", Const.ConstructionReport_Audit1);
                return lis;
            }
            else if (state == Const.ConstructionReport_Audit1)   //有是否同意
            {
                ListItem[] lis = new ListItem[2];
                lis[0] = new ListItem("审批完成", Const.ConstructionReport_Complete);//是 加载
                lis[1] = new ListItem("重新编制", Const.ConstructionReport_ReCompile);//否加载
                return lis;
            }
            else
                return null;
        }
    }
}
