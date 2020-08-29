using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace BLL
{
    public class CheckControlService
    {
        public static Model.SGGLDB db = Funs.DB;
        /// <summary>
        /// 根据质量检查与控制Id删除一个质量检查与控制信息
        /// </summary>
        /// <param name="CheckControlId"></param>
        public static void DeleteCheckControl(string CheckControlId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Check_CheckControl CheckControl = db.Check_CheckControl.First(e => e.CheckControlCode == CheckControlId);
            db.Check_CheckControl.DeleteOnSubmit(CheckControl);
            db.SubmitChanges();
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
        /// <summary>
        /// 添加质量检查与控制
        /// </summary>
        /// <param name="CheckControl"></param>
        public static void AddCheckControl(Model.Check_CheckControl CheckControl)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Check_CheckControl newCheckControl = new Model.Check_CheckControl();
            newCheckControl.CheckControlCode = CheckControl.CheckControlCode;
            newCheckControl.ProposeUnitId = CheckControl.ProposeUnitId;
            newCheckControl.DocCode = CheckControl.DocCode;
            newCheckControl.ProjectId = CheckControl.ProjectId;
            newCheckControl.UnitWorkId = CheckControl.UnitWorkId;
            newCheckControl.UnitId = CheckControl.UnitId;
            newCheckControl.CheckDate = CheckControl.CheckDate;
            newCheckControl.CheckMan = CheckControl.CheckMan;
            newCheckControl.IsSubmit = CheckControl.IsSubmit;
            newCheckControl.SubmitMan = CheckControl.SubmitMan;
            newCheckControl.CNProfessionalCode = CheckControl.CNProfessionalCode;
            newCheckControl.QuestionType = CheckControl.QuestionType;
            newCheckControl.CheckSite = CheckControl.CheckSite;
            newCheckControl.QuestionDef = CheckControl.QuestionDef;
            newCheckControl.LimitDate = CheckControl.LimitDate;
            newCheckControl.RectifyOpinion = CheckControl.RectifyOpinion;
            newCheckControl.AttachUrl = CheckControl.AttachUrl;
            newCheckControl.HandleWay = CheckControl.HandleWay;
            newCheckControl.RectifyDate = CheckControl.RectifyDate;
            newCheckControl.ReAttachUrl = CheckControl.ReAttachUrl;
            newCheckControl.State = CheckControl.State;
            newCheckControl.SaveHandleMan = CheckControl.SaveHandleMan;

            db.Check_CheckControl.InsertOnSubmit(newCheckControl);
            db.SubmitChanges();
        }
        public static void AddCheckControlForApi(Model.Check_CheckControl CheckControl)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                Model.Check_CheckControl newCheckControl = new Model.Check_CheckControl();
                newCheckControl.CheckControlCode = CheckControl.CheckControlCode;
                newCheckControl.ProposeUnitId = CheckControl.ProposeUnitId;
                newCheckControl.DocCode = CheckControl.DocCode;
                newCheckControl.ProjectId = CheckControl.ProjectId;
                newCheckControl.UnitWorkId = CheckControl.UnitWorkId;
                newCheckControl.UnitId = CheckControl.UnitId;
                newCheckControl.CheckDate = CheckControl.CheckDate;
                newCheckControl.CheckMan = CheckControl.CheckMan;
                newCheckControl.IsSubmit = CheckControl.IsSubmit;
                newCheckControl.SubmitMan = CheckControl.SubmitMan;
                newCheckControl.CNProfessionalCode = CheckControl.CNProfessionalCode;
                newCheckControl.QuestionType = CheckControl.QuestionType;
                newCheckControl.CheckSite = CheckControl.CheckSite;
                newCheckControl.QuestionDef = CheckControl.QuestionDef;
                newCheckControl.LimitDate = CheckControl.LimitDate;
                newCheckControl.RectifyOpinion = CheckControl.RectifyOpinion;
                newCheckControl.AttachUrl = CheckControl.AttachUrl;
                newCheckControl.HandleWay = CheckControl.HandleWay;
                newCheckControl.RectifyDate = CheckControl.RectifyDate;
                newCheckControl.ReAttachUrl = CheckControl.ReAttachUrl;
                newCheckControl.State = CheckControl.State;
                newCheckControl.SaveHandleMan = CheckControl.SaveHandleMan;

                db.Check_CheckControl.InsertOnSubmit(newCheckControl);
                db.SubmitChanges();
            }
        }
        /// <summary>
        /// 根据质量检查与控制Id获取一个质量检查与控制信息
        /// </summary>
        /// <param name="CheckControlDetailId"></param>
        public static Model.Check_CheckControl GetCheckControl(string CheckControlCode)
        {
            return Funs.DB.Check_CheckControl.FirstOrDefault(e => e.CheckControlCode == CheckControlCode);
        }
        public static Model.Check_CheckControl GetCheckControlForApi(string CheckControlCode)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                Model.Check_CheckControl x = db.Check_CheckControl.FirstOrDefault(e => e.CheckControlCode == CheckControlCode);
                x.AttachUrl = AttachFileService.getFileUrl(x.CheckControlCode);
                x.ReAttachUrl = AttachFileService.getFileUrl(x.CheckControlCode + "r");
                x.QuestionType = x.QuestionType + "$" + BLL.QualityQuestionTypeService.GetQualityQuestionType(x.QuestionType).QualityQuestionType;
                var unit = UnitService.GetUnitByUnitId(x.UnitId);
                x.UnitId = x.UnitId + "$" + unit.UnitName;
                var ppunit = UnitService.GetUnitByUnitId(x.ProposeUnitId);
                var punit = ProjectUnitService.GetProjectUnitByUnitIdProjectId(x.ProjectId, x.ProposeUnitId);
                if (punit != null)
                {
                    x.ProposeUnitId = x.ProposeUnitId + "$" + ppunit.UnitName + "$" + punit.UnitType;
                }
                else
                {
                    x.ProposeUnitId = x.ProposeUnitId + "$" + "$";

                }
                Sys_User checkMen = UserService.GetUserByUserId(x.CheckMan);

                x.CheckMan = (checkMen != null ? checkMen.UserName : "") + "$" + UnitWorkService.GetNameById(x.UnitWorkId) + "$" + ConvertManAndID(x.CheckControlCode);

                x.CNProfessionalCode = x.CNProfessionalCode + "$" + CNProfessionalService.GetCNProfessionalNameByCode(x.CNProfessionalCode);
                return x;
            }

        }
        /// <summary>
        /// 根据状态选择下一步办理类型
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public static ListItem[] GetDHandleTypeByState(string state)
        {
            if (state == Const.CheckControl_Compile || state == Const.CheckControl_ReCompile)  //无是否同意
            {
                ListItem[] lis = new ListItem[2];
                lis[0] = new ListItem("总包负责人审核", Const.CheckControl_Audit1);
                lis[1] = new ListItem("分包专业工程师回复", Const.CheckControl_Audit2);
                return lis;
            }
            else if (state == Const.CheckControl_Audit1)//有是否同意
            {
                ListItem[] lis = new ListItem[2];
                lis[0] = new ListItem("分包专业工程师回复", Const.CheckControl_Audit2);//是 加载
                lis[1] = new ListItem("重新编制", Const.CheckControl_ReCompile);//否加载
                return lis;
            }
            else if (state == Const.CheckControl_Audit2 || state == Const.CheckControl_ReCompile2)//无是否同意
            {
                ListItem[] lis = new ListItem[2];
                lis[0] = new ListItem("分包负责人审批", Const.CheckControl_Audit3);
                lis[1] = new ListItem("总包专业工程师确认", Const.CheckControl_Audit4);
                return lis;
            }
            else if (state == Const.CheckControl_Audit3)//有是否同意
            {
                ListItem[] lis = new ListItem[2];
                lis[0] = new ListItem("总包专业工程师确认", Const.CheckControl_Audit4);//是 加载
                lis[1] = new ListItem("分包专业工程师重新回复", Const.CheckControl_ReCompile2);//否加载
                return lis;
            }
            else if (state == Const.CheckControl_Audit4)//有是否同意
            {
                ListItem[] lis = new ListItem[3];
                lis[0] = new ListItem("总包负责人确认", Const.CheckControl_Audit5);//是 加载
                lis[1] = new ListItem("审批完成", Const.CheckControl_Complete);//是 加载
                lis[2] = new ListItem("分包专业工程师重新回复", Const.CheckControl_ReCompile2);//否加载
                return lis;
            }
            else if (state == Const.CheckControl_Audit5)//有是否同意
            {
                ListItem[] lis = new ListItem[2];
                lis[0] = new ListItem("审批完成", Const.CheckControl_Complete);//是 加载
                lis[1] = new ListItem("分包专业工程师重新回复", Const.CheckControl_ReCompile2);//否加载
                return lis;
            }
            else
                return null;
        }
        /// <summary>
        /// 修改质量检查与控制
        /// </summary>
        /// <param name="CheckControl"></param>
        public static void UpdateCheckControl(Model.Check_CheckControl CheckControl)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Check_CheckControl newCheckControl = db.Check_CheckControl.First(e => e.CheckControlCode == CheckControl.CheckControlCode);
            newCheckControl.DocCode = CheckControl.DocCode;
            newCheckControl.ProposeUnitId = CheckControl.ProposeUnitId;
            newCheckControl.UnitWorkId = CheckControl.UnitWorkId;
            newCheckControl.UnitId = CheckControl.UnitId;
            newCheckControl.CheckDate = CheckControl.CheckDate;
            newCheckControl.IsSubmit = CheckControl.IsSubmit;
            newCheckControl.SubmitMan = CheckControl.SubmitMan;
            newCheckControl.IsOK = CheckControl.IsOK;
            newCheckControl.CNProfessionalCode = CheckControl.CNProfessionalCode;
            newCheckControl.QuestionType = CheckControl.QuestionType;
            newCheckControl.CheckSite = CheckControl.CheckSite;
            newCheckControl.QuestionDef = CheckControl.QuestionDef;
            newCheckControl.LimitDate = CheckControl.LimitDate;
            newCheckControl.RectifyOpinion = CheckControl.RectifyOpinion;
            newCheckControl.AttachUrl = CheckControl.AttachUrl;
            newCheckControl.HandleWay = CheckControl.HandleWay;
            newCheckControl.RectifyDate = CheckControl.RectifyDate;
            newCheckControl.ReAttachUrl = CheckControl.ReAttachUrl;
            newCheckControl.State = CheckControl.State;
            newCheckControl.SaveHandleMan = CheckControl.SaveHandleMan;

            db.SubmitChanges();
        }
        public static List<Model.Check_CheckControl> GetListDataForApi(string state, string name, string projectId, int index, int page)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                IQueryable<Model.Check_CheckControl> q = db.Check_CheckControl;
                List<string> ids = new List<string>();
                if (!string.IsNullOrEmpty(name))
                {
                    var qunit = from u in Funs.DB.Base_Unit
                                where u.UnitName.Contains(name)
                                select u.UnitId;
                    ids = qunit.ToList();
                    q = q.Where(e => ids.Contains(e.UnitId));
                }


                if (!string.IsNullOrEmpty(projectId))
                {
                    q = q.Where(e => e.ProjectId == projectId);
                }
                switch (state)
                {
                    case "1": // 未整改
                        q = q.Where(e => e.State != BLL.Const.CheckControl_Audit4
                                        && e.State != BLL.Const.CheckControl_Audit5
                                        && e.State != BLL.Const.CheckControl_Complete
                                        && e.LimitDate > DateTime.Now);
                        break;
                    case "2": // 待确认 5/6
                        q = q.Where(e => e.State == BLL.Const.CheckControl_Audit4 || e.State == BLL.Const.CheckControl_Audit5);
                        break;
                    case "3": // 已闭环 7
                        q = q.Where(e => e.State == BLL.Const.CheckControl_Complete);
                        break;
                    case "4":  // 超期未整改
                        q = q.Where(e => e.State != BLL.Const.CheckControl_Audit4
                                        && e.State != BLL.Const.CheckControl_Audit5
                                        && e.State != BLL.Const.CheckControl_Complete
                                        && e.LimitDate < DateTime.Now);
                        break;
                }
                var qq1 = from x in q
                          orderby x.DocCode descending
                          select new
                          {
                              x.CheckControlCode,
                              x.DocCode,
                              x.UnitId,
                              x.ProposeUnitId,
                              x.UnitWorkId,
                              x.CheckDate,
                              x.State,
                              x.CheckSite,
                              x.IsSubmit,
                              x.AttachUrl,
                              x.QuestionDef,
                              x.QuestionType,
                              x.RectifyOpinion,
                              x.LimitDate,
                              x.CNProfessionalCode,

                              CNProfessionalName = (from y in db.Base_CNProfessional where y.CNProfessionalId == x.CNProfessionalCode select y.ProfessionalName).First(),
                              UnitWork = (from y in db.WBS_UnitWork where y.UnitWorkId == x.UnitWorkId select y.UnitWorkCode + "-" + y.UnitWorkName + (y.ProjectType == "1" ? "(建筑)" : "(安装)")).First(),
                              CheckMan = (from y in db.Sys_User where y.UserId == x.CheckMan select y.UserName).First()

                          };
                var list = qq1.Skip(index * page).Take(page).ToList();

                List<Model.Check_CheckControl> listRes = new List<Model.Check_CheckControl>();
                for (int i = 0; i < list.Count; i++)
                {
                    Model.Check_CheckControl x = new Model.Check_CheckControl();
                    x.CheckControlCode = list[i].CheckControlCode;
                    x.DocCode = list[i].DocCode;
                    x.UnitWorkId = list[i].UnitWorkId;
                    x.CheckDate = list[i].CheckDate;
                    x.UnitId = list[i].UnitId + "$" + UnitService.GetUnitNameByUnitId(list[i].UnitId);
                    var punit = ProjectUnitService.GetProjectUnitByUnitIdProjectId(projectId, list[i].ProposeUnitId);
                    string unitType = string.Empty;
                    if (punit != null)
                    {
                        unitType = punit.UnitType;
                    }
                    x.ProposeUnitId = list[i].ProposeUnitId + "$" + UnitService.GetUnitNameByUnitId(list[i].ProposeUnitId) + "$" + unitType;
                    x.CheckMan = list[i].CheckMan + "$" + list[i].UnitWork + "$" + ConvertManAndID(list[i].CheckControlCode);
                    x.State = list[i].State;
                    x.CheckSite = list[i].CheckSite;
                    x.IsSubmit = list[i].IsSubmit;
                    x.AttachUrl = list[i].AttachUrl;
                    x.QuestionDef = list[i].QuestionDef;
                    if (!string.IsNullOrEmpty(list[i].QuestionType))
                    {
                        x.QuestionType = list[i].QuestionType + "$" + BLL.QualityQuestionTypeService.GetQualityQuestionType(list[i].QuestionType).QualityQuestionType;
                    }
                    x.RectifyOpinion = list[i].RectifyOpinion;
                    x.LimitDate = list[i].LimitDate;
                    x.CNProfessionalCode = list[i].CNProfessionalCode + "$" + list[i].CNProfessionalName;
                    x.AttachUrl = AttachFileService.getFileUrl(x.CheckControlCode);
                    x.ReAttachUrl = AttachFileService.getFileUrl(x.CheckControlCode + "r");
                    listRes.Add(x);
                }
                return listRes;
            }
        }
        public static string GetListCountStr(string projectId)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                IQueryable<Model.Check_CheckControl> q = db.Check_CheckControl;

                if (!string.IsNullOrEmpty(projectId))
                {
                    q = q.Where(e => e.ProjectId == projectId);
                }

                var i = q.Where(e => e.State != BLL.Const.CheckControl_Audit4
                                        && e.State != BLL.Const.CheckControl_Audit5
                                        && e.State != BLL.Const.CheckControl_Complete
                                        && e.LimitDate > DateTime.Now).ToList();
                string weiZhengLeng = i.Count.ToString(); // 未整改

                var j = q.Where(e => e.State == BLL.Const.CheckControl_Audit4 || e.State == BLL.Const.CheckControl_Audit5).ToList();
                string weiRengLeng = j.Count.ToString(); // 未确认

                string len = weiZhengLeng + "$" + weiRengLeng;
                return len;
            }
        }
        public static List<Model.Check_CheckControl> GetListDataForApi(string unitId, string unitWork, string problemType, string professional, string state, string dateA, string dateZ, string projectId, int index, int page)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                IQueryable<Model.Check_CheckControl> q = db.Check_CheckControl;

                if (!string.IsNullOrEmpty(state) && "undefined" != state)
                {
                    switch (state)
                    {
                        case "未确认":
                            q = q.Where(e => "5,6".Split(',').Contains(e.State));
                            break;
                        case "已闭环":
                            q = q.Where(e => "7" == e.State);
                            break;
                        case "未整改":
                            q = q.Where(e => "0,1,2,3,4,8".Split(',').Contains(e.State));
                            q = q.Where(e => e.LimitDate >= DateTime.Now);
                            break;
                        case "超期未整改":
                            q = q.Where(e => e.LimitDate < DateTime.Now);
                            q = q.Where(e => "0,1,2,3,4,8".Split(',').Contains(e.State));
                            break;
                    }

                }

                if (!string.IsNullOrEmpty(unitWork) && "undefined" != unitWork)
                {
                    q = q.Where(e => e.UnitWorkId == unitWork);
                }
                if (!string.IsNullOrEmpty(problemType) && "undefined" != problemType)
                {
                    q = q.Where(e => e.QuestionType == problemType);
                }

                if (!string.IsNullOrEmpty(professional) && "undefined" != professional)
                {
                    q = q.Where(e => e.CNProfessionalCode == professional);
                }
                if (!string.IsNullOrEmpty(dateA) && "undefined" != dateA)
                {
                    DateTime date = DateTime.ParseExact(dateA, "yyyy-MM-dd", new CultureInfo("zh-CN", true));
                    q = q.Where(e => e.CheckDate >= date);
                }
                if (!string.IsNullOrEmpty(dateZ) && "undefined" != dateZ)
                {
                    DateTime date = DateTime.ParseExact(dateZ + "23:59:59", "yyyy-MM-ddHH:mm:ss", new CultureInfo("zh-CN", true));
                    q = q.Where(e => e.CheckDate <= date);
                }
                if (!string.IsNullOrEmpty(unitId) && "undefined" != unitId)
                {
                    q = q.Where(e => e.UnitId == unitId);
                }
                if (!string.IsNullOrEmpty(projectId) && "undefined" != projectId)
                {
                    q = q.Where(e => e.ProjectId == projectId);
                }

                var qq1 = from x in q
                          orderby x.DocCode descending
                          select new
                          {
                              x.CheckControlCode,
                              x.DocCode,
                              x.UnitId,
                              x.ProposeUnitId,
                              x.UnitWorkId,
                              x.CheckDate,
                              x.State,
                              x.CheckSite,
                              x.IsSubmit,
                              x.AttachUrl,
                              x.QuestionDef,
                              x.QuestionType,
                              x.RectifyOpinion,
                              x.LimitDate,
                              x.CNProfessionalCode,

                              CNProfessionalName = (from y in db.Base_CNProfessional where y.CNProfessionalId == x.CNProfessionalCode select y.ProfessionalName).First(),
                              UnitWork = (from y in db.WBS_UnitWork where y.UnitWorkId == x.UnitWorkId select y.UnitWorkCode + "-" + y.UnitWorkName + (y.ProjectType == "1" ? "(建筑)" : "(安装)")).First(),
                              CheckMan = (from y in db.Sys_User where y.UserId == x.CheckMan select y.UserName).First()

                          };
                var list = qq1.Skip(index * page).Take(page).ToList();

                List<Model.Check_CheckControl> listRes = new List<Model.Check_CheckControl>();
                for (int i = 0; i < list.Count; i++)
                {
                    Model.Check_CheckControl x = new Model.Check_CheckControl();
                    x.CheckControlCode = list[i].CheckControlCode;
                    x.DocCode = list[i].DocCode;
                    x.UnitWorkId = list[i].UnitWorkId;
                    x.CheckDate = list[i].CheckDate;
                    x.UnitId = list[i].UnitId + "$" + UnitService.GetUnitNameByUnitId(list[i].UnitId);
                    x.ProposeUnitId = list[i].ProposeUnitId + "$" + UnitService.GetUnitNameByUnitId(list[i].ProposeUnitId);
                    x.CheckMan = list[i].CheckMan + "$" + list[i].UnitWork + "$" + ConvertManAndID(list[i].CheckControlCode);
                    x.State = list[i].State;
                    x.CheckSite = list[i].CheckSite;
                    x.IsSubmit = list[i].IsSubmit;
                    x.AttachUrl = list[i].AttachUrl;
                    x.QuestionDef = list[i].QuestionDef;
                    if (!string.IsNullOrEmpty(list[i].QuestionType))
                    {
                        x.QuestionType = list[i].QuestionType + "$" + BLL.QualityQuestionTypeService.GetQualityQuestionType(list[i].QuestionType).QualityQuestionType;
                    }
                    x.RectifyOpinion = list[i].RectifyOpinion;
                    x.LimitDate = list[i].LimitDate;
                    x.CNProfessionalCode = list[i].CNProfessionalCode + "$" + list[i].CNProfessionalName;
                    x.AttachUrl = AttachFileService.getFileUrl(x.CheckControlCode);
                    x.ReAttachUrl = AttachFileService.getFileUrl(x.CheckControlCode + "r");
                    listRes.Add(x);
                }
                return listRes;
            }
        }
        public static string ConvertMan(object CheckControlCode)
        {
            if (CheckControlCode != null)
            {
                Model.Check_CheckControlApprove a = BLL.CheckControlApproveService.GetCheckControlApproveByCheckControlId(CheckControlCode.ToString());
                if (a != null)
                {
                    if (a.ApproveMan != null)
                    {
                        return BLL.UserService.GetUserByUserId(a.ApproveMan).UserName;
                    }
                }
                else
                {
                    return "";
                }
            }
            return "";
        }
        public static string ConvertManAndID(object CheckControlCode)
        {
            if (CheckControlCode != null)
            {
                Model.Check_CheckControlApprove a = BLL.CheckControlApproveService.GetCheckControlApproveByCheckControlId(CheckControlCode.ToString());
                if (a != null)
                {
                    if (a.ApproveMan != null)
                    {
                        var user = BLL.UserService.GetUserByUserId(a.ApproveMan);
                        return user.UserName + "$" + user.UserId;
                    }
                }
                else
                {
                    return "";
                }
            }
            return "";
        }
        public static void UpdateCheckControlForApi(Model.Check_CheckControl CheckControl)
        {

            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                Model.Check_CheckControl newCheckControl = db.Check_CheckControl.First(e => e.CheckControlCode == CheckControl.CheckControlCode);
                if (!string.IsNullOrEmpty(CheckControl.DocCode))
                    newCheckControl.DocCode = CheckControl.DocCode;
                if (!string.IsNullOrEmpty(CheckControl.UnitWorkId))
                    newCheckControl.UnitWorkId = CheckControl.UnitWorkId;
                if (!string.IsNullOrEmpty(CheckControl.UnitId))
                    newCheckControl.UnitId = CheckControl.UnitId;
                if (CheckControl.CheckDate.HasValue)
                    newCheckControl.CheckDate = CheckControl.CheckDate;
                if (CheckControl.IsSubmit.HasValue)
                    newCheckControl.IsSubmit = CheckControl.IsSubmit;
                if (!string.IsNullOrEmpty(CheckControl.SubmitMan))
                    newCheckControl.SubmitMan = CheckControl.SubmitMan;
                if (CheckControl.IsOK.HasValue)
                    newCheckControl.IsOK = CheckControl.IsOK;
                if (!string.IsNullOrEmpty(CheckControl.CNProfessionalCode))
                    newCheckControl.CNProfessionalCode = CheckControl.CNProfessionalCode;
                if (!string.IsNullOrEmpty(CheckControl.QuestionType))
                    newCheckControl.QuestionType = CheckControl.QuestionType;
                if (!string.IsNullOrEmpty(CheckControl.CheckSite))
                    newCheckControl.CheckSite = CheckControl.CheckSite;
                if (!string.IsNullOrEmpty(CheckControl.QuestionDef))
                    newCheckControl.QuestionDef = CheckControl.QuestionDef;
                if (CheckControl.LimitDate.HasValue)
                    newCheckControl.LimitDate = CheckControl.LimitDate;
                if (!string.IsNullOrEmpty(CheckControl.RectifyOpinion))
                    newCheckControl.RectifyOpinion = CheckControl.RectifyOpinion;
                if (!string.IsNullOrEmpty(CheckControl.AttachUrl))
                    newCheckControl.AttachUrl = CheckControl.AttachUrl;
                if (!string.IsNullOrEmpty(CheckControl.HandleWay))
                    newCheckControl.HandleWay = CheckControl.HandleWay;
                if (CheckControl.RectifyDate.HasValue)
                    newCheckControl.RectifyDate = CheckControl.RectifyDate;
                if (!string.IsNullOrEmpty(CheckControl.ReAttachUrl))
                    newCheckControl.ReAttachUrl = CheckControl.ReAttachUrl;
                if (!string.IsNullOrEmpty(CheckControl.State))
                    newCheckControl.State = CheckControl.State;
                if (!string.IsNullOrEmpty(CheckControl.ProposeUnitId))
                    newCheckControl.ProposeUnitId = CheckControl.ProposeUnitId;
                db.SubmitChanges();
            }
        }
        public static int GetListCount(string projectId)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                IQueryable<Model.Check_CheckControl> q = db.Check_CheckControl;
                if (!string.IsNullOrEmpty(projectId))
                {
                    q = q.Where(e => e.ProjectId == projectId);
                }

                return q.Count();
            }

        }
    }
}
