using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.DataVisualization.Charting;

namespace BLL
{
    public class JointCheckDetailService
    {
        /// <summary>
        /// 修改质量共检明细信息
        /// </summary>
        /// <param name="pauseNotice">质量共检明细实体</param>
        public static void UpdateJointCheckDetail(Model.Check_JointCheckDetail a)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Check_JointCheckDetail newJointCheckDetail = db.Check_JointCheckDetail.First(e => e.JointCheckDetailId == a.JointCheckDetailId);
            newJointCheckDetail.UnitWorkId = a.UnitWorkId;
            newJointCheckDetail.CNProfessionalCode = a.CNProfessionalCode;
            newJointCheckDetail.QuestionDef = a.QuestionDef;
            newJointCheckDetail.CheckSite = a.CheckSite;
            newJointCheckDetail.QuestionType = a.QuestionType;
            newJointCheckDetail.Standard = a.Standard;
            newJointCheckDetail.RectifyOpinion = a.RectifyOpinion;
            newJointCheckDetail.LimitDate = a.LimitDate;
            newJointCheckDetail.AttachUrl = a.AttachUrl;
            newJointCheckDetail.HandleWay = a.HandleWay;
            newJointCheckDetail.RectifyDate = a.RectifyDate;
            newJointCheckDetail.ReAttachUrl = a.ReAttachUrl;
            newJointCheckDetail.Feedback = a.Feedback;
            newJointCheckDetail.IsOK = a.IsOK;
            newJointCheckDetail.State = a.State;
            newJointCheckDetail.HandleMan = a.HandleMan;
            db.SubmitChanges();
        }
        /// <summary>
        /// 根据质量共检明细编号获取质量共检明细
        /// </summary>
        /// <param name="costCode"></param>
        public static Model.Check_JointCheckDetail GetJointCheckDetailByJointCheckDetailId(string JointCheckDetailId)
        {
            return Funs.DB.Check_JointCheckDetail.FirstOrDefault(e => e.JointCheckDetailId == JointCheckDetailId);
        }
        public static Model.Check_JointCheckDetail GetJointCheckDetailByJointCheckDetailIdForApi(string JointCheckDetailId)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                var res = db.Check_JointCheckDetail.FirstOrDefault(e => e.JointCheckDetailId == JointCheckDetailId);
                res.AttachUrl = AttachFileService.getFileUrl(res.JointCheckDetailId);
                res.ReAttachUrl = AttachFileService.getFileUrl(res.JointCheckDetailId + "r");
                return res;
            }
        }
        /// <summary>
        /// 下拉框选择(获取text val 参数必须有一个为空)
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string GetValByText(string text, string val)
        {
            string str = null;
            var itemlist = checkType();
            if (!string.IsNullOrWhiteSpace(text) && !string.IsNullOrWhiteSpace(val))
            {

            }
            else
            {
                if (!string.IsNullOrWhiteSpace(text))
                {
                    foreach (var item in itemlist)
                    {
                        if (text.Equals(item.Value))
                        {
                            str = item.Key.ToString();
                        }
                    }
                }
                if (!string.IsNullOrWhiteSpace(val))
                {
                    foreach (var item in itemlist)
                    {
                        if (val.Equals(item.Key.ToString()))
                        {
                            str = item.Value;
                        }
                    }
                }

            }



            return str;
        }
        /// <summary>
        /// 根据质量共检编号获取质量共检明细集合
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static List<Model.Check_JointCheckDetail> GetLists(string jointCheckId)
        {
            return (from x in Funs.DB.Check_JointCheckDetail where x.JointCheckId == jointCheckId orderby x.CreateDate select x).ToList();
        }

        public static List<Model.Check_JointCheckDetail> GetListsForApi(string jointCheckId)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                return (from x in db.Check_JointCheckDetail where x.JointCheckId == jointCheckId orderby x.CreateDate select x).ToList();

            }
        }
        /// <summary>
        /// 根据质量共检编号获取质量共检明细集合
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static List<Model.View_Check_JointCheckDetail> GetViewLists(string jointCheckId)
        {
            return (from x in Funs.DB.View_Check_JointCheckDetail where x.JointCheckId == jointCheckId orderby x.CreateDate select x).ToList();
        }
        public static Dictionary<string, string> checkType()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            var list = QualityQuestionTypeService.GetQualityQuestionTypeItem();
            foreach (var item in list)
            {
                dic.Add(item.Value, item.Text);
            }
            return dic;
        }
        public static void Init(FineUIPro.DropDownList dropName, bool isShowPlease)
        {
            dropName.DataValueField = "Value";
            dropName.DataTextField = "Value";
            dropName.DataSource = checkType();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        /// <summary>
        /// 根据质量共检明细主键删除一个质量共检明细信息
        /// </summary>
        /// <param name="pauseNoticeCode">质量共检明细主键</param>
        public static void DeleteJointCheckDetailByJointCheckId(string JointCheckId)
        {
            Model.SGGLDB db = Funs.DB;
            var q = (from x in db.Check_JointCheckDetail where x.JointCheckId == JointCheckId select x).ToList();
            db.Check_JointCheckDetail.DeleteAllOnSubmit(q);
            db.SubmitChanges();
        }
        /// <summary>
        /// 增加质量共检明细信息
        /// </summary>
        /// <param name="pauseNotice">质量共检明细实体</param>
        public static void AddJointCheckDetail(Model.Check_JointCheckDetail a)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Check_JointCheckDetail newJointCheckDetail = new Model.Check_JointCheckDetail();
            newJointCheckDetail.JointCheckDetailId = a.JointCheckDetailId;
            newJointCheckDetail.JointCheckId = a.JointCheckId;
            newJointCheckDetail.UnitWorkId = a.UnitWorkId;
            newJointCheckDetail.CNProfessionalCode = a.CNProfessionalCode;
            newJointCheckDetail.QuestionDef = a.QuestionDef;
            newJointCheckDetail.CheckSite = a.CheckSite;
            newJointCheckDetail.QuestionType = a.QuestionType;
            newJointCheckDetail.Standard = a.Standard;
            newJointCheckDetail.RectifyOpinion = a.RectifyOpinion;
            newJointCheckDetail.LimitDate = a.LimitDate;
            newJointCheckDetail.AttachUrl = a.AttachUrl;
            newJointCheckDetail.HandleWay = a.HandleWay;
            newJointCheckDetail.RectifyDate = a.RectifyDate;
            newJointCheckDetail.ReAttachUrl = a.ReAttachUrl;
            newJointCheckDetail.Feedback = a.Feedback;
            newJointCheckDetail.IsOK = a.IsOK;
            newJointCheckDetail.State = a.State;
            newJointCheckDetail.HandleMan = a.HandleMan;
            newJointCheckDetail.CreateDate = a.CreateDate;
            db.Check_JointCheckDetail.InsertOnSubmit(newJointCheckDetail);
            db.SubmitChanges();
        }
        public static void AddJointCheckDetailForApi(Model.Check_JointCheckDetail a)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                Model.Check_JointCheckDetail newJointCheckDetail = new Model.Check_JointCheckDetail();
                newJointCheckDetail.JointCheckDetailId = a.JointCheckDetailId;
                newJointCheckDetail.JointCheckId = a.JointCheckId;
                newJointCheckDetail.UnitWorkId = a.UnitWorkId;
                newJointCheckDetail.CNProfessionalCode = a.CNProfessionalCode;
                newJointCheckDetail.QuestionDef = a.QuestionDef;
                newJointCheckDetail.CheckSite = a.CheckSite;
                newJointCheckDetail.QuestionType = a.QuestionType;
                newJointCheckDetail.Standard = a.Standard;
                newJointCheckDetail.RectifyOpinion = a.RectifyOpinion;
                newJointCheckDetail.LimitDate = a.LimitDate;
                newJointCheckDetail.AttachUrl = a.AttachUrl;
                newJointCheckDetail.HandleWay = a.HandleWay;
                newJointCheckDetail.RectifyDate = a.RectifyDate;
                newJointCheckDetail.ReAttachUrl = a.ReAttachUrl;
                newJointCheckDetail.Feedback = a.Feedback;
                newJointCheckDetail.IsOK = a.IsOK;
                newJointCheckDetail.State = a.State;
                newJointCheckDetail.HandleMan = a.HandleMan;
                newJointCheckDetail.SaveHandleMan = a.SaveHandleMan;
                newJointCheckDetail.CreateDate = a.CreateDate;
                db.Check_JointCheckDetail.InsertOnSubmit(newJointCheckDetail);
                db.SubmitChanges();
            }
        }
        /// <summary>
        /// 根据时间段获取质量共检明细集合
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        public static List<Model.View_Check_JointCheckDetail> GetJointCheckDetailListByTime(string projectId, DateTime startTime, DateTime endTime)
        {
            return (from x in Funs.DB.View_Check_JointCheckDetail
                    where x.ProjectId == projectId && x.CheckDate >= startTime && x.CheckDate < endTime && ((x.OKDate >= startTime && x.OKDate < endTime) || x.OKDate == null)
                    select x).ToList();
        }
        /// <summary>
        /// 根据时间段获取质量共检明细集合
        /// </summary>
        /// <param name="endTime">结束时间</param>
        public static List<Model.View_Check_JointCheckDetail> GetTotalJointCheckDetailListByTime(string projectId, DateTime endTime)
        {
            return (from x in Funs.DB.View_Check_JointCheckDetail
                    where x.ProjectId == projectId && x.CheckDate < endTime && (x.OKDate < endTime || x.OKDate == null)
                    select x).ToList();
        }
        public static List<Model.View_Check_JointCheckDetail> getListDataForApi(string JointCheckId)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                IQueryable<Model.View_Check_JointCheckDetail> q = db.View_Check_JointCheckDetail;
                if (!string.IsNullOrEmpty(JointCheckId))
                {
                    q = q.Where(e => e.JointCheckId == JointCheckId);
                }


                var qres = from x in q
                           orderby x.CreateDate ascending
                           select new
                           {
                               x.JointCheckDetailId,
                               x.JointCheckId,
                               x.UnitWorkId,
                               x.CNProfessionalCode,
                               x.QuestionDef,
                               x.QuestionType,
                               x.Standard,
                               x.RectifyOpinion,
                               x.LimitDate,
                               x.AttachUrl,
                               x.HandleWay,
                               x.RectifyDate,
                               x.ReAttachUrl,
                               x.Feedback,
                               x.CheckSite,
                               x.IsOK,
                               x.UnitName,
                               x.CheckDate,
                               x.State,
                               x.CheckTypeStr,
                               x.QuestionTypeStr,
                               x.UnitWorkName,
                               x.ProfessionalName,
                               x.CreateDate,
                               x.SaveHandleMan,
                               SaveHandleManMan = (from y in db.Sys_User where y.UserId == x.SaveHandleMan select y.UserName).First()


                           };
                var list = qres.ToList();
                List<Model.View_Check_JointCheckDetail> res = new List<Model.View_Check_JointCheckDetail>();

                foreach (var item in list)
                {
                    Model.View_Check_JointCheckDetail x = new Model.View_Check_JointCheckDetail();
                    x.JointCheckDetailId = item.JointCheckDetailId;
                    x.JointCheckId = item.JointCheckId;
                    x.UnitWorkId = item.UnitWorkId;
                    x.CNProfessionalCode = item.CNProfessionalCode;
                    x.QuestionDef = item.QuestionDef;
                    x.QuestionType = item.QuestionType;
                    x.Standard = item.Standard;
                    x.RectifyOpinion = item.RectifyOpinion;
                    x.LimitDate = item.LimitDate;
                    x.AttachUrl = item.AttachUrl;
                    x.HandleWay = item.HandleWay;
                    x.RectifyDate = item.RectifyDate;
                    x.ReAttachUrl = item.ReAttachUrl;
                    x.Feedback = item.Feedback;
                    x.CheckSite = item.CheckSite;
                    x.IsOK = item.IsOK;
                    x.State = item.State;
                    x.CheckTypeStr = item.CheckTypeStr;
                    x.QuestionTypeStr = item.QuestionTypeStr;
                    x.UnitWorkName = item.UnitWorkName;
                    x.ProfessionalName = item.ProfessionalName;
                    x.CreateDate = item.CreateDate;
                    x.SaveHandleMan = item.SaveHandleMan + "$" + item.SaveHandleManMan;
                    x.AttachUrl = AttachFileService.getFileUrl(x.JointCheckDetailId);
                    x.ReAttachUrl = AttachFileService.getFileUrl(x.JointCheckDetailId + "r");
                    res.Add(x);

                }
                return res;
            }

        }
        public static void DeleteJointCheckDetailById(string id)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                var q = (from x in db.Check_JointCheckDetail where x.JointCheckDetailId == id select x).ToList();
                db.Check_JointCheckDetail.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }
        public static void UpdateJointCheckDetailForApi(Model.Check_JointCheckDetail a)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                Model.Check_JointCheckDetail newJointCheckDetail = db.Check_JointCheckDetail.FirstOrDefault(e => e.JointCheckDetailId == a.JointCheckDetailId);
                if (newJointCheckDetail != null)
                {
                    if (!string.IsNullOrEmpty(a.UnitWorkId))
                        newJointCheckDetail.UnitWorkId = a.UnitWorkId;
                    if (!string.IsNullOrEmpty(a.CNProfessionalCode))
                        newJointCheckDetail.CNProfessionalCode = a.CNProfessionalCode;
                    if (!string.IsNullOrEmpty(a.QuestionDef))
                        newJointCheckDetail.QuestionDef = a.QuestionDef;
                    if (!string.IsNullOrEmpty(a.CheckSite))
                        newJointCheckDetail.CheckSite = a.CheckSite;
                    if (!string.IsNullOrEmpty(a.QuestionType))
                        newJointCheckDetail.QuestionType = a.QuestionType;
                    if (!string.IsNullOrEmpty(a.Standard))
                        newJointCheckDetail.Standard = a.Standard;
                    if (!string.IsNullOrEmpty(a.RectifyOpinion))
                        newJointCheckDetail.RectifyOpinion = a.RectifyOpinion;
                    if (a.LimitDate.HasValue)
                        newJointCheckDetail.LimitDate = a.LimitDate;
                    if (!string.IsNullOrEmpty(a.AttachUrl))
                        newJointCheckDetail.AttachUrl = a.AttachUrl;
                    if (!string.IsNullOrEmpty(a.HandleWay))
                        newJointCheckDetail.HandleWay = a.HandleWay;
                    if (a.RectifyDate.HasValue)
                        newJointCheckDetail.RectifyDate = a.RectifyDate;
                    if (!string.IsNullOrEmpty(a.ReAttachUrl))
                        newJointCheckDetail.ReAttachUrl = a.ReAttachUrl;
                    if (!string.IsNullOrEmpty(a.State))
                        newJointCheckDetail.State = a.State;
                    if (!string.IsNullOrEmpty(a.Feedback))
                        newJointCheckDetail.Feedback = a.Feedback;
                    if (a.IsOK.HasValue)
                        newJointCheckDetail.IsOK = a.IsOK;

                    newJointCheckDetail.HandleMan = a.HandleMan;
                    newJointCheckDetail.SaveHandleMan = a.SaveHandleMan;
                    db.SubmitChanges();
                }
            }
        }
    }
}
