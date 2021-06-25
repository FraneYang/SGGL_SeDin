namespace BLL
{
    using System.Collections.Generic;
    using System.Data;
    using System.Text;
    using System.Collections;
    using System.Linq;

    /// <summary>
    /// 通用方法类。
    /// </summary>
    public static class PrinterDocService
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void PrinterDocMethod(string menuId, string id, string name)
        {
            System.Web.HttpContext.Current.Response.ClearContent();
            string htmlStr = string.Empty;
            if (menuId == (Const.ProjectRectifyNoticesMenuId + "#1"))
            {
                htmlStr = GetRectifyNoticesTableHtml(id);
            }
           else if (menuId == (Const.ProjectRectifyNoticesMenuId + "#2"))
            {
                htmlStr = GetRectifyNoticesTableHtml2(id);
            }
            else if (menuId == Const.ProjectManagerMonth_SeDinMenuId)
            {
                htmlStr = GetMonthReportHtml(id);
            }
            else if (menuId == Const.ProjectTestRecordMenuId)
            {
                htmlStr = GetTestRecordHtml(id);
            }
            else if (menuId == Const.SendCardMenuId)
            {
                htmlStr = GetSendCardHtml(id);
            }
            if (!string.IsNullOrEmpty(htmlStr))
            {
                string filename = name + Funs.GetNewFileName();
                System.Web.HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" 
                    + System.Web.HttpUtility.UrlEncode(filename, Encoding.UTF8) + ".doc");
                System.Web.HttpContext.Current.Response.ContentType = "application/word";
                System.Web.HttpContext.Current.Response.ContentEncoding = Encoding.UTF8;
                System.Web.HttpContext.Current.Response.Write(htmlStr);
                System.Web.HttpContext.Current.Response.End();
            }
        }

        #region 隐患整改通知单
        /// <summary>
        /// 导出方法
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public static string GetRectifyNoticesTableHtml(string rectifyNoticesId)
        {
            StringBuilder sb = new StringBuilder();
            var getRectifyNotices = RectifyNoticesService.GetRectifyNoticesById(rectifyNoticesId);
            if (getRectifyNotices != null)
            {
                sb.Append("<meta http-equiv=\"content-type\" content=\"application/word; charset=UTF-8\"/>");
                sb.Append("<table width=\"100% \" cellspacing=\"0\" rules=\"all\" border=\"1\" style=\"border-collapse:collapse;font-size: 11pt;\">");
                sb.Append("<tr style=\"height: 35px\">");
                sb.AppendFormat("<td align=\"center\" colspan=\"4\"  style=\"width: 100%; font-size: 12pt; font-weight: bold;border-right: none;border-left: none;border-bottom: none;border-top: none; \">{0}</td> ", "安全隐患整改通知单");
                sb.Append("</tr>");

                sb.Append("<tr style=\"height: 35px\">");
                sb.AppendFormat("<td align=\"center\"  colspan=\"3\" style=\"border-right: none;border-left: none;border-top: none; \">{0}</td> ", "项目名称：" + BLL.ProjectService.GetProjectNameByProjectId(getRectifyNotices.ProjectId));
                sb.AppendFormat("<td align=\"center\" style=\"border-right: none;border-left: none;border-top: none; \">{0}</td> ", "编号：" + getRectifyNotices.RectifyNoticesCode);
                sb.Append("</tr>");

                sb.Append("<tr style=\"height: 35px\">");
                sb.AppendFormat("<td align=\"center\"  style=\"width: 18%; \">{0}</td> ", "受检单位名称");
                sb.AppendFormat("<td align=\"left\"  style=\"width: 32%; \">{0}</td> ", UnitService.GetUnitNameByUnitId(getRectifyNotices.UnitId));
                sb.AppendFormat("<td align=\"center\"  style=\"width: 18%; \">{0}</td> ", "单位工程名称");
                sb.AppendFormat("<td align=\"left\"  style=\"width: 32%; \">{0}</td> ", UnitWorkService.GetUnitWorkName(getRectifyNotices.WorkAreaId));
                sb.Append("</tr>");

                sb.Append("<tr style=\"height: 35px\">");
                sb.AppendFormat("<td align=\"center\" >{0}</td> ", "检查人员");
                sb.AppendFormat("<td align=\"left\"  colspan=\"3\">{0}</td> ", UserService.getUserNamesUserIds(getRectifyNotices.CheckManIds));
                sb.Append("</tr>");

                sb.Append("<tr style=\"height: 35px\">");
                sb.AppendFormat("<td align=\"center\" >{0}</td> ", "检查日期");
                sb.AppendFormat("<td align=\"left\"  colspan=\"3\">{0}</td> ", string.Format("{0:yyyy-MM-dd}", getRectifyNotices.CheckedDate));
                sb.Append("</tr>");

                sb.Append("<tr style=\"height: 35px\">");
                sb.AppendFormat("<td align=\"center\" >{0}</td> ", "隐患类别");
                if (getRectifyNotices.HiddenHazardType == "3")
                {
                    sb.AppendFormat("<td align=\"left\"  colspan=\"3\">{0}</td> ", "☐一般    ☐较大   √重大");
                }
                else if (getRectifyNotices.HiddenHazardType == "2")
                {
                    sb.AppendFormat("<td align=\"left\"  colspan=\"3\">{0}</td> ", "☐一般   √较大   ☐重大");
                }
                else
                {
                    sb.AppendFormat("<td align=\"left\"  colspan=\"3\">{0}</td> ", "√一般   ☐较大   ☐重大");
                }
                sb.Append("</tr>");

                sb.Append("<tr style=\"height: 35px\">");
                sb.AppendFormat("<td align=\"left\"  colspan=\"4\" style=\"border-bottom: none;\" >{0}</td> ", "贵公司存在的安全隐患及整改要求如下：");
                sb.Append("</tr>");

                var getItem = from x in Funs.DB.Check_RectifyNoticesItem
                              where x.RectifyNoticesId == rectifyNoticesId
                              orderby x.RectifyNoticesItemId
                              select x;
                int i = 1;
                foreach (var item in getItem)
                {
                    string contStr = "&nbsp; &nbsp;" + i.ToString() + "." + item.WrongContent + "（详见附图" + i.ToString() + "），整改要求：" + item.Requirement;
                    if (item.LimitTime.HasValue)
                    {
                        contStr += " 整改期限" + string.Format("{0:yyyy-MM-dd HH:mm:ss}", item.LimitTime);
                    }

                    sb.Append("<tr style=\"height: 35px\">");
                    sb.AppendFormat("<td align=\"left\"  colspan=\"4\" style=\"border-top: none;border-bottom: none;\"  >{0}</td> ", contStr);
                    sb.Append("</tr>");
                    i++;
                }

                sb.Append("<tr style=\"height: 35px\">");
                sb.AppendFormat("<td align=\"left\"  colspan=\"4\" style=\"border-top: none;border-bottom: none;\"  >{0}</td> ", "");
                sb.Append("</tr>");

                sb.Append("<tr style=\"height: 35px\">");
                sb.AppendFormat("<td align=\"right\"  colspan=\"4\" style=\"border-top: none;border-bottom: none;\"  >{0}</td> ", "总包单位项目安全经理：" + UserService.getSignatureName(getRectifyNotices.SignPerson) + "&nbsp; &nbsp;");
                sb.Append("</tr>");

                sb.Append("<tr style=\"height: 35px\">");
                sb.AppendFormat("<td align=\"right\"  colspan=\"4\" style=\"border-top: none;\">{0}</td> ", "日期：" + string.Format("{0:yyyy-MM-dd HH:mm:ss}", getRectifyNotices.SignDate) + "&nbsp; &nbsp;");
                sb.Append("</tr>");

                sb.Append("<tr style=\"height: 35px\">");
                sb.AppendFormat("<td align=\"center\" rowspan=\"3\">{0}</td> ", "抄送：");
                if (string.IsNullOrEmpty(getRectifyNotices.ProfessionalEngineerId))
                {
                    sb.AppendFormat("<td align=\"left\"   colspan=\"2\">{0}</td> ", "☐专业工程师：");
                    sb.AppendFormat("<td align=\"left\" >{0}</td> ", "日期：");
                }
                else
                {
                    sb.AppendFormat("<td align=\"left\"   colspan=\"2\">{0}</td> ", "√专业工程师：" + UserService.getSignatureName(getRectifyNotices.ProfessionalEngineerId));
                    sb.AppendFormat("<td align=\"left\"   >{0}</td> ", "日期：" + string.Format("{0:yyyy-MM-dd HH:mm:ss}", getRectifyNotices.ProfessionalEngineerTime1));
                }
                sb.Append("</tr>");

                sb.Append("<tr style=\"height: 35px\">");
                if (string.IsNullOrEmpty(getRectifyNotices.ConstructionManagerId))
                {
                    sb.AppendFormat("<td align=\"left\"   colspan=\"2\">{0}</td> ", "☐施工经理：");
                    sb.AppendFormat("<td align=\"left\" >{0}</td> ", "日期：");
                }
                else
                {
                    sb.AppendFormat("<td align=\"left\"   colspan=\"2\">{0}</td> ", "√施工经理：" + UserService.getSignatureName(getRectifyNotices.ConstructionManagerId));
                    sb.AppendFormat("<td align=\"left\"   >{0}</td> ", "日期：" + string.Format("{0:yyyy-MM-dd HH:mm:ss}", getRectifyNotices.ConstructionManagerTime1));
                }
                sb.Append("</tr>");

                sb.Append("<tr style=\"height: 35px\">");
                if (string.IsNullOrEmpty(getRectifyNotices.ProjectManagerId))
                {
                    sb.AppendFormat("<td align=\"left\"   colspan=\"2\">{0}</td> ", "☐项目经理：");
                    sb.AppendFormat("<td align=\"left\" >{0}</td> ", "日期：");
                }
                else
                {
                    sb.AppendFormat("<td align=\"left\"   colspan=\"2\">{0}</td> ", "√项目经理：" + UserService.getSignatureName(getRectifyNotices.ProjectManagerId));
                    sb.AppendFormat("<td align=\"left\"   >{0}</td> ", "日期：" + string.Format("{0:yyyy-MM-dd HH:mm:ss}", getRectifyNotices.ProjectManagerTime1));
                }
                sb.Append("</tr>");

                sb.Append("<tr style=\"height: 35px\">");
                sb.AppendFormat("<td align=\"center\" >{0}</td> ", "接收人");
                sb.AppendFormat("<td align=\"left\" >{0}</td> ", UserService.getSignatureName(getRectifyNotices.DutyPersonId));
                sb.AppendFormat("<td align=\"center\" >{0}</td> ", "接收日期");
                sb.AppendFormat("<td align=\"left\" >{0}</td> ", string.Format("{0:yyyy-MM-dd HH:mm:ss}", getRectifyNotices.DutyPersonTime));
                sb.Append("</tr>");

                sb.Append("<tr style=\"height: 35px\">");
                sb.AppendFormat("<td align=\"left\"  colspan=\"4\" style=\"border-bottom: none;\"  >{0}</td> ", "注： 1.本表由总包单位项目安全经理签发，一般隐患抄送专业工程师监督；较大隐患抄送施工经理监督；重大隐患需要由项目经理签字并报公司施工管理部备案。");
                sb.Append("</tr>");

                sb.Append("<tr style=\"height: 35px\">");
                sb.AppendFormat("<td align=\"left\" colspan=\"4\" style=\"border-top: none;\">{0}</td> ", "&nbsp; &nbsp;2.本表一式2份，签发单位和接收单位各一份。");
                sb.Append("</tr>");

                sb.Append("<tr style=\"height: 35px\">");
                sb.AppendFormat("<td align=\"left\" colspan=\"4\"  style=\"width: 100%; font-size: 11pt; font-weight: bold;border-right: none;border-left: none;border-bottom: none;border-top: none; \">{0}</td> ", "附图：");
                sb.Append("</tr>");

                sb.Append("<tr style=\"height: 20px\">");
                sb.AppendFormat("<td align=\"left\"  colspan=\"4\" >{0}</td> ", "整改前的照片");
                sb.Append("</tr>");

                int j = 1;
                foreach (var item in getItem)
                {
                    var att = AttachFileService.GetAttachFile(item.RectifyNoticesItemId.ToString() + "#1", BLL.Const.ProjectRectifyNoticesMenuId);
                    if (att != null && !string.IsNullOrEmpty(att.AttachUrl))
                    {
                        string imgStr = string.Empty;
                        List<string> listStr = Funs.GetStrListByStr(att.AttachUrl, ',');
                        foreach (var urlItem in listStr)
                        {
                            imgStr += "<img width='100' height='100' src='" + (Funs.SGGLUrl + urlItem).Replace('\\', '/') + "'></img>&nbsp;&nbsp;";
                        }
                        sb.Append("<tr>");
                        sb.AppendFormat("<td align=\"center\"  colspan=\"4\"  style=\"border-bottom: none;\">{0}</td> ", imgStr);
                        sb.Append("</tr>");
                        sb.Append("<tr style=\"height: 20px\">");
                        sb.AppendFormat("<td align=\"center\"  colspan=\"4\" style=\"border-top: none;\">{0}</td> ", "图 " + j.ToString());
                        sb.Append("</tr>");

                        j++;
                    }
                }
                sb.Append("</table>");
            }
            return sb.ToString();
        }
        #endregion

        #region 安全隐患整改反馈单
        /// <summary>
        /// 导出方法
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public static string GetRectifyNoticesTableHtml2(string rectifyNoticesId)
        {
            StringBuilder sb = new StringBuilder();
            var getRectifyNotices = RectifyNoticesService.GetRectifyNoticesById(rectifyNoticesId);
            if (getRectifyNotices != null)
            {
                sb.Append("<meta http-equiv=\"content-type\" content=\"application/word; charset=UTF-8\"/>");
                sb.Append("<table width=\"100% \" cellspacing=\"0\" rules=\"all\" border=\"1\" style=\"border-collapse:collapse;font-size: 11pt;\">");
                sb.Append("<tr style=\"height: 35px\">");
                sb.AppendFormat("<td align=\"center\" colspan=\"4\"  style=\"width: 100%; font-size: 12pt; font-weight: bold;border-right: none;border-left: none;border-bottom: none;border-top: none; \">{0}</td> ", "安全隐患整改反馈单");
                sb.Append("</tr>");

                sb.Append("<tr style=\"height: 35px\">");
                sb.AppendFormat("<td align=\"center\"  colspan=\"3\" style=\"border-right: none;border-left: none;border-top: none; \">{0}</td> ", "项目名称：" + BLL.ProjectService.GetProjectNameByProjectId(getRectifyNotices.ProjectId));
                sb.AppendFormat("<td align=\"center\" style=\"border-right: none;border-left: none;border-top: none; \">{0}</td> ", "编号：" + getRectifyNotices.RectifyNoticesCode);
                sb.Append("</tr>");

                sb.Append("<tr style=\"height: 35px\">");
                sb.AppendFormat("<td align=\"center\"  style=\"width: 18%; \">{0}</td> ", "受检单位名称");
                sb.AppendFormat("<td align=\"left\"  style=\"width: 32%; \">{0}</td> ", UnitService.GetUnitNameByUnitId(getRectifyNotices.UnitId));
                sb.AppendFormat("<td align=\"center\"  style=\"width: 18%; \">{0}</td> ", "单位工程名称");
                sb.AppendFormat("<td align=\"left\"  style=\"width: 32%; \">{0}</td> ", UnitWorkService.GetUnitWorkName(getRectifyNotices.WorkAreaId));
                sb.Append("</tr>");

                sb.Append("<tr style=\"height: 35px\">");
                sb.AppendFormat("<td align=\"center\" >{0}</td> ", "检查人员");
                sb.AppendFormat("<td align=\"left\"  colspan=\"3\">{0}</td> ", UserService.getUserNamesUserIds(getRectifyNotices.CheckManIds));
                sb.Append("</tr>");

                sb.Append("<tr style=\"height: 35px\">");
                sb.AppendFormat("<td align=\"center\" >{0}</td> ", "检查日期");
                sb.AppendFormat("<td align=\"left\"  colspan=\"3\">{0}</td> ", string.Format("{0:yyyy-MM-dd}", getRectifyNotices.CheckedDate));
                sb.Append("</tr>");

                sb.Append("<tr style=\"height: 35px\">");
                sb.AppendFormat("<td align=\"center\" >{0}</td> ", "隐患类别");
                if (getRectifyNotices.HiddenHazardType == "3")
                {
                    sb.AppendFormat("<td align=\"left\"  colspan=\"3\">{0}</td> ", "☐一般    ☐较大   √重大");
                }
                else if (getRectifyNotices.HiddenHazardType == "2")
                {
                    sb.AppendFormat("<td align=\"left\"  colspan=\"3\">{0}</td> ", "☐一般   √较大   ☐重大");
                }
                else
                {
                    sb.AppendFormat("<td align=\"left\"  colspan=\"3\">{0}</td> ", "√一般   ☐较大   ☐重大");
                }
                sb.Append("</tr>");

                sb.Append("<tr style=\"height: 35px\">");
                sb.AppendFormat("<td align=\"left\"  colspan=\"4\" style=\"border-bottom: none;\" >{0}</td> ", "&nbsp; &nbsp;我单位接到编号为 " + getRectifyNotices.RectifyNoticesCode + "  的安全隐患整改通知单后，现已按要求完成了整改，具体整改情况如下：");
                sb.Append("</tr>");

                var getItem = from x in Funs.DB.Check_RectifyNoticesItem
                              where x.RectifyNoticesId == rectifyNoticesId
                              orderby x.RectifyNoticesItemId
                              select x;
                int i = 1;
                foreach (var item in getItem)
                {
                    string contStr = "&nbsp; &nbsp;" + i.ToString() + "." + item.RectifyResults + "（详见附图" + i.ToString() + "）。是否合格：" + (item.IsRectify.HasValue ? (item.IsRectify == true ? "合格" : "不合格") : "");
                    sb.Append("<tr style=\"height: 35px\">");
                    sb.AppendFormat("<td align=\"left\"  colspan=\"4\" style=\"border-top: none;border-bottom: none;\"  >{0}</td> ", contStr);
                    sb.Append("</tr>");
                    i++;
                }

                sb.Append("<tr style=\"height: 35px\">");
                sb.AppendFormat("<td align=\"left\"  colspan=\"4\" style=\"border-top: none;border-bottom: none;\"  >{0}</td> ", "");
                sb.Append("</tr>");

                sb.Append("<tr style=\"height: 35px\">");
                sb.AppendFormat("<td align=\"right\"  colspan=\"4\" style=\"border-top: none;border-bottom: none;\"  >{0}</td> ", "施工单位项目安全经理：" + UserService.getSignatureName(getRectifyNotices.DutyPersonId) + "&nbsp; &nbsp;"
                    + "日期：" + string.Format("{0:yyyy-MM-dd HH:mm:ss}", getRectifyNotices.CompleteDate) + "&nbsp; &nbsp;");
                sb.Append("</tr>");

                sb.Append("<tr style=\"height: 35px\">");
                sb.AppendFormat("<td align=\"right\"  colspan=\"4\" style=\"border-top: none;\"  >{0}</td> ", "施工单位项目负责人：" + UserService.getSignatureName(getRectifyNotices.UnitHeadManId) + "&nbsp; &nbsp;"
                    + "日期：" + string.Format("{0:yyyy-MM-dd HH:mm:ss}", getRectifyNotices.UnitHeadManDate) + "&nbsp; &nbsp;");
                sb.Append("</tr>");

                sb.Append("<tr style=\"height: 35px\">");
                sb.AppendFormat("<td align=\"left\"  colspan=\"4\" style=\"border-bottom: none;\"  >{0}</td> ", "总包单位复查意见：");
                sb.Append("</tr>");

                sb.Append("<tr style=\"height: 35px\">");
                sb.AppendFormat("<td align=\"left\"  colspan=\"4\" style=\"border-top: none;border-bottom: none;\"  >{0}</td> ", "&nbsp; &nbsp;" + getRectifyNotices.ReCheckOpinion);
                sb.Append("</tr>");

                sb.Append("<tr style=\"height: 35px\">");
                sb.AppendFormat("<td align=\"left\"  colspan=\"4\" style=\"border-top: none;border-bottom: none;\"  >{0}</td> ", "");
                sb.Append("</tr>");

                sb.Append("<tr style=\"height: 35px\">");
                sb.AppendFormat("<td align=\"right\"  colspan=\"4\" style=\"border-top: none;border-bottom: none;\"  >{0}</td> ", "安全经理/安全工程师：" + UserService.getSignatureName(getRectifyNotices.CheckPerson) + "&nbsp; &nbsp;");
                sb.Append("</tr>");

                sb.Append("<tr style=\"height: 35px\">");
                sb.AppendFormat("<td align=\"right\"  colspan=\"4\" style=\"border-top: none;\">{0}</td> ", "日期：" + string.Format("{0:yyyy-MM-dd HH:mm:ss}", getRectifyNotices.ReCheckDate) + "&nbsp; &nbsp;");
                sb.Append("</tr>");

                sb.Append("<tr style=\"height: 35px\">");
                sb.AppendFormat("<td align=\"center\" rowspan=\"3\">{0}</td> ", "抄送：");
                if (string.IsNullOrEmpty(getRectifyNotices.ProfessionalEngineerId))
                {
                    sb.AppendFormat("<td align=\"left\"   colspan=\"2\">{0}</td> ", "☐专业工程师：");
                    sb.AppendFormat("<td align=\"left\" >{0}</td> ", "日期：");
                }
                else
                {
                    sb.AppendFormat("<td align=\"left\"   colspan=\"2\">{0}</td> ", "√专业工程师：" + UserService.getSignatureName(getRectifyNotices.ProfessionalEngineerId));
                    sb.AppendFormat("<td align=\"left\"   >{0}</td> ", "日期：" + string.Format("{0:yyyy-MM-dd HH:mm:ss}", getRectifyNotices.ProfessionalEngineerTime2));
                }
                sb.Append("</tr>");

                sb.Append("<tr style=\"height: 35px\">");
                if (string.IsNullOrEmpty(getRectifyNotices.ConstructionManagerId))
                {
                    sb.AppendFormat("<td align=\"left\"   colspan=\"2\">{0}</td> ", "☐施工经理：");
                    sb.AppendFormat("<td align=\"left\" >{0}</td> ", "日期：");
                }
                else
                {
                    sb.AppendFormat("<td align=\"left\"   colspan=\"2\">{0}</td> ", "√施工经理：" + UserService.getSignatureName(getRectifyNotices.ConstructionManagerId));
                    sb.AppendFormat("<td align=\"left\"   >{0}</td> ", "日期：" + string.Format("{0:yyyy-MM-dd HH:mm:ss}", getRectifyNotices.ConstructionManagerTime2));
                }
                sb.Append("</tr>");

                sb.Append("<tr style=\"height: 35px\">");
                if (string.IsNullOrEmpty(getRectifyNotices.ProjectManagerId))
                {
                    sb.AppendFormat("<td align=\"left\"   colspan=\"2\">{0}</td> ", "☐项目经理：");
                    sb.AppendFormat("<td align=\"left\" >{0}</td> ", "日期：");
                }
                else
                {
                    sb.AppendFormat("<td align=\"left\"   colspan=\"2\">{0}</td> ", "√项目经理：" + UserService.getSignatureName(getRectifyNotices.ProjectManagerId));
                    sb.AppendFormat("<td align=\"left\"   >{0}</td> ", "日期：" + string.Format("{0:yyyy-MM-dd HH:mm:ss}", getRectifyNotices.ProjectManagerTime2));
                }
                sb.Append("</tr>");

                sb.Append("<tr style=\"height: 35px\">");
                sb.AppendFormat("<td align=\"left\" colspan=\"4\"  style=\"width: 100%; font-size: 11pt; font-weight: bold;border-right: none;border-left: none;border-bottom: none;border-top: none; \">{0}</td> ", "附图：");
                sb.Append("</tr>");

                sb.Append("<tr style=\"height: 20px\">");
                sb.AppendFormat("<td align=\"left\"  colspan=\"4\" >{0}</td> ", "整改后的照片");
                sb.Append("</tr>");

                int j = 1;
                foreach (var item in getItem)
                {
                    var att = AttachFileService.GetAttachFile(item.RectifyNoticesItemId.ToString() + "#2", BLL.Const.ProjectRectifyNoticesMenuId);
                    if (att != null && !string.IsNullOrEmpty(att.AttachUrl))
                    {
                        string imgStr = string.Empty;
                        List<string> listStr = Funs.GetStrListByStr(att.AttachUrl, ',');
                        foreach (var urlItem in listStr)
                        {
                            imgStr += "<img width='100' height='100' src='" + (Funs.SGGLUrl + urlItem).Replace('\\', '/') + "'></img>&nbsp;&nbsp;";
                        }
                        sb.Append("<tr>");
                        sb.AppendFormat("<td align=\"center\"  colspan=\"4\"  style=\"border-bottom: none;\">{0}</td> ", imgStr);
                        sb.Append("</tr>");
                        sb.Append("<tr style=\"height: 20px\">");
                        sb.AppendFormat("<td align=\"center\"  colspan=\"4\" style=\"border-top: none;\">{0}</td> ", "图 " + j.ToString());
                        sb.Append("</tr>");
                        j++;
                    }
                }

                sb.Append("</table>");
            }
            return sb.ToString();
        }
        #endregion

        #region 安全月报
        /// <summary>
        /// 导出方法
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public static string GetMonthReportHtml(string monthReportId)
        {
            StringBuilder sb = new StringBuilder();
            var getMonthReport = Funs.DB.SeDin_MonthReport.FirstOrDefault(x => x.MonthReportId == monthReportId);
            if (getMonthReport != null)
            {
                sb.Append("<meta http-equiv=\"content-type\" content=\"application/word; charset=UTF-8\"/>");
                sb.Append("<table width=\"100% \" cellspacing=\"0\" rules=\"all\" border=\"0\" style=\"border-collapse:collapse;font-size: 10.5pt;\">");
                sb.Append("<tr style=\"height: 120px\">");
                //sb.AppendFormat("<td align=\"center\" style=\"width: 100%; font-size: 12pt; font-weight: bold;border-right: none;border-left: none;border-bottom: none;border-top: none; \">{0}</td> ", "安全隐患整改反馈单");
                sb.AppendFormat("<td align=\"center\"  style=\"width: 100%;border-right: none;border-left: none;border-bottom: none;border-top: none;\">{0}</td> "
                    , "<img width='350' height='70' src='" + (Funs.SGGLUrl + "/Images/SUBimages/sedinlogo.png").Replace('\\', '/') + "'></img>");
                sb.Append("</tr>");

                sb.Append("<tr style=\"height: 100px\">");
                sb.AppendFormat("<td align=\"center\" style=\"width: 100%; font-size: 14pt; font-weight: bold;border-right: none;border-left: none;border-bottom: none;border-top: none; \">{0}</td> ", "文件编号：");
                sb.Append("</tr>");

                sb.Append("<tr style=\"height: 100px\">");
                sb.AppendFormat("<td align=\"center\" style=\"width: 100%; font-size: 22pt; border-right: none;border-left: none;border-bottom: none;border-top: none; \">{0}</td> "
                    , ProjectService.GetProjectNameByProjectId(getMonthReport.ProjectId));
                sb.Append("</tr>");

                sb.Append("<tr style=\"height: 150px\">");
                sb.AppendFormat("<td align=\"center\" style=\"width: 100%; font-size: 36pt; font-weight: bold;border-right: none;border-left: none;border-bottom: none;border-top: none; \">{0}</td> "
                    , "HSE月报告");
                sb.Append("</tr>");

                sb.Append("<tr style=\"height: 150px\">");
                string dateStr = "报告期"+string.Format("{0:yyyy-MM-dd}",getMonthReport.StartDate)+" 至 "+ string.Format("{0:yyyy-MM-dd}", getMonthReport.EndDate);
                sb.AppendFormat("<td align=\"center\" style=\"width: 100%; font-size: 14pt; border-right: none;border-left: none;border-bottom: none;border-top: none; \">{0}</td> "
                    , dateStr);
                sb.Append("</tr>");

                sb.Append("<tr style=\"height: 70px\">");              
                sb.AppendFormat("<td align=\"center\" style=\"width: 100%; border-right: none;border-left: none;border-bottom: none;border-top: none; \">{0}</td> " , "");
                sb.Append("</tr>");

                sb.Append("<tr style=\"height: 35px\">");
                sb.AppendFormat("<td align=\"center\" style=\"width: 100%; font-size: 16pt;border-right: none;border-left: none;border-bottom: none;border-top: none; \">{0}</td> ", "编制：" + UserService.GetUserNameByUserId(getMonthReport.CompileManId));                
                sb.Append("</tr>");
                sb.Append("<tr style=\"height: 35px\">");
                sb.AppendFormat("<td align=\"center\" style=\"width: 100%; font-size: 16pt;border-right: none;border-left: none;border-bottom: none;border-top: none; \">{0}</td> ", "审核：" + UserService.GetUserNameByUserId(getMonthReport.AuditManId));
                sb.Append("</tr>");
                sb.Append("<tr style=\"height: 35px\">");
                sb.AppendFormat("<td align=\"center\" style=\"width: 100%; font-size: 16pt;border-right: none;border-left: none;border-bottom: none;border-top: none; \">{0}</td> ", "批准：" + UserService.GetUserNameByUserId(getMonthReport.ApprovalManId));
                sb.Append("</tr>");

                sb.Append("<tr style=\"height: 100px\">");
                sb.AppendFormat("<td align=\"center\" style=\"width: 100%; border-right: none;border-left: none;border-bottom: none;border-top: none; \">{0}</td> ", "");
                sb.Append("</tr>");

                sb.Append("<tr style=\"height: 45px\">");
                sb.AppendFormat("<td align=\"center\" style=\"width: 100%; font-size: 16pt;font-weight: bold;border-right: none;border-left: none;border-bottom: none;border-top: none; \">{0}</td> ", "项目现场HSE月报");
                sb.Append("</tr>");

                sb.Append("<tr style=\"height: 20px\">");
                sb.AppendFormat("<td align=\"right\" style=\"width: 100%; font-size: 10.5pt;border-right: none;border-left: none;border-bottom: none;border-top: none; \">{0}</td> "
                    , "报告截止日期：" + string.Format("{0:yyyy-MM-dd}", getMonthReport.DueDate));
                sb.Append("</tr>");

                sb.Append("<tr>");
                sb.Append("<td  style=\"width: 100%; \">");

                #region 1、项目信息
                var getMonthReport1 = Funs.DB.SeDin_MonthReport1.FirstOrDefault(x => x.MonthReportId == monthReportId);
                if (getMonthReport1 != null)
                {
                    sb.Append("<table width=\"100% \" cellspacing=\"0\" rules=\"all\" border=\"1\" style=\"border-collapse:collapse;font-size: 10.5pt;\">");
                    sb.Append("<tr style=\"height: 30px\">");
                    sb.AppendFormat("<td align=\"left\" colspan=\"6\"  style=\"width: 100%;font-weight: bold;\">{0}</td> "
                        , "1、项目信息：");
                    sb.Append("</tr>");

                    sb.Append("<tr style=\"height: 20px\">");
                    sb.AppendFormat("<td align=\"center\" style=\"width: 18%;\">{0}</td> "
                        , "项目编号");
                    sb.AppendFormat("<td align=\"left\" colspan=\"2\"  style=\"width: 17%;\">{0}</td> "
                    , getMonthReport1.ProjectCode);
                    sb.AppendFormat("<td align=\"center\" style=\"width: 15%;\">{0}</td> "
                      , "项目名称");
                    sb.AppendFormat("<td align=\"left\" colspan=\"2\"  style=\"width: 50%;\">{0}</td> "
                    , getMonthReport1.ProjectName);
                    sb.Append("</tr>");

                    sb.Append("<tr style=\"height: 20px\">");
                    sb.AppendFormat("<td align=\"center\" >{0}</td> "
                        , "项目类型");
                    sb.AppendFormat("<td align=\"left\" colspan=\"2\" >{0}</td> "
                    , getMonthReport1.ProjectType);
                    sb.AppendFormat("<td align=\"center\">{0}</td> "
                      , "合同工期");
                    sb.AppendFormat("<td align=\"left\" colspan=\"2\"  >{0}</td> "
                    , string.Format("{0:yyyy-MM-dd}", getMonthReport1.StartDate)+" 至 " + string.Format("{0:yyyy-MM-dd}", getMonthReport1.EndDate));
                    sb.Append("</tr>");

                    sb.Append("<tr style=\"height: 20px\">");
                    sb.AppendFormat("<td align=\"center\" colspan=\"2\" style=\"width: 25%;\">{0}</td> "
                        , "项目经理及联系方式");
                    sb.AppendFormat("<td align=\"left\" colspan=\"2\" style=\"width: 25%;\">{0}</td> "
                    , getMonthReport1.ProjectManager);
                    sb.AppendFormat("<td align=\"center\"  style=\"width: 25%;\">{0}</td> "
                      , "安全经理及联系方式");
                    sb.AppendFormat("<td align=\"left\" style=\"width: 25%;\">{0}</td> "
                   , getMonthReport1.HsseManager);
                    sb.Append("</tr>");
                    
                    sb.Append("<tr style=\"height: 20px\">");
                    sb.AppendFormat("<td align=\"center\" colspan=\"2\" >{0}</td> "
                        , "合同额");
                    sb.AppendFormat("<td align=\"left\" colspan=\"4\" >{0}</td> "
                    , getMonthReport1.ContractAmount);
                    sb.Append("</tr>");

                    sb.Append("<tr style=\"height: 20px\">");
                    sb.AppendFormat("<td align=\"center\" colspan=\"2\" >{0}</td> "
                        , "所处的施工阶段");
                    sb.AppendFormat("<td align=\"left\" colspan=\"4\" >{0}</td> "
                    , getMonthReport1.ConstructionStage);
                    sb.Append("</tr>");

                    sb.Append("<tr style=\"height: 20px\">");
                    sb.AppendFormat("<td align=\"center\" colspan=\"2\" >{0}</td> "
                        , "项目所在地");
                    sb.AppendFormat("<td align=\"left\" colspan=\"4\" >{0}</td> "
                    , getMonthReport1.ProjectAddress);
                    sb.Append("</tr>");

                    sb.Append("</table>");
                }
                #endregion

                sb.Append("<tr style=\"height: 20px\">");
                sb.AppendFormat("<td align=\"center\" style=\"width: 100%; border-right: none;border-left: none;border-bottom: none;border-top: none; \">{0}</td> ", "");
                sb.Append("</tr>");
                #region 2、项目安全工时统计
                var getMonthReport2 = Funs.DB.SeDin_MonthReport2.FirstOrDefault(x => x.MonthReportId == monthReportId);
                if (getMonthReport2 != null)
                {
                    sb.Append("<table width=\"100% \" cellspacing=\"0\" rules=\"all\" border=\"1\" style=\"border-collapse:collapse;font-size: 10.5pt;\">");
                    sb.Append("<tr style=\"height: 30px\">");
                    sb.AppendFormat("<td align=\"left\" colspan=\"6\"  style=\"width: 100%;font-weight: bold;\">{0}</td> "
                        , "2、项目安全工时统计：");
                    sb.Append("</tr>");

                    sb.Append("<tr style=\"height: 20px\">");
                    sb.AppendFormat("<td align=\"center\" style=\"width: 20%;\">{0}</td> "
                        , "当月安全人工时");
                    sb.AppendFormat("<td align=\"left\" style=\"width: 11%;\">{0}</td> "
                    , getMonthReport2.MonthWorkTime);
                    sb.AppendFormat("<td align=\"center\" style=\"width: 20%;\">{0}</td> "
                        , "年度累计安全人工时");
                    sb.AppendFormat("<td align=\"left\" style=\"width: 11%;\">{0}</td> "
                    , getMonthReport2.YearWorkTime);
                    sb.AppendFormat("<td align=\"center\" style=\"width: 20%;\">{0}</td> "
                        , "项目累计安全人工时");
                    sb.AppendFormat("<td align=\"left\" style=\"width: 11%;\">{0}</td> "
                    , getMonthReport2.ProjectWorkTime);
                    sb.Append("</tr>");

                    sb.Append("<tr style=\"height: 20px\">");
                    sb.AppendFormat("<td align=\"center\" >{0}</td> "
                        , "总损失工时");
                    sb.AppendFormat("<td align=\"left\" >{0}</td> "
                    , getMonthReport2.TotalLostTime);
                    sb.AppendFormat("<td align=\"center\" >{0}</td> "
                        , "百万工时损失率");
                    sb.AppendFormat("<td align=\"left\" >{0}</td> "
                    , getMonthReport2.MillionLossRate);
                    sb.AppendFormat("<td align=\"center\" >{0}</td> "
                        , "工时统计准确率");
                    sb.AppendFormat("<td align=\"left\" >{0}</td> "
                    , getMonthReport2.TimeAccuracyRate);
                    sb.Append("</tr>");

                    sb.Append("<tr style=\"height: 20px\">");
                    sb.AppendFormat("<td align=\"left\" colspan=\"6\" >{0}</td> "
                    , "本项目自"+string.Format("{0:yyyy-MM-dd}", getMonthReport2.StartDate)+" 至 " + string.Format("{0:yyyy-MM-dd}", getMonthReport2.EndDate)
                    +"安全生产"+ getMonthReport2 .SafeWorkTime .ToString()+ "人工时，无可记录事故");
                    sb.Append("</tr>");

                    sb.Append("</table>");
                }
                #endregion

                sb.Append("<tr style=\"height: 20px\">");
                sb.AppendFormat("<td align=\"center\" style=\"width: 100%; border-right: none;border-left: none;border-bottom: none;border-top: none; \">{0}</td> ", "");
                sb.Append("</tr>");
                #region 3、项目HSE事故、事件统计
                sb.Append("<table width=\"100% \" cellspacing=\"0\" rules=\"all\" border=\"1\" style=\"border-collapse:collapse;font-size: 10.5pt;\">");
                sb.Append("<tr style=\"height: 30px\">");
                sb.AppendFormat("<td align=\"left\" colspan=\"10\"  style=\"width: 100%;font-weight: bold;\">{0}</td> "
                    , "3、项目HSE事故、事件统计：");
                sb.Append("</tr>");

                sb.Append("<tr style=\"height: 20px\">");
                sb.AppendFormat("<td align=\"center\" style=\"width: 20%;\" colspan=\"2\">{0}</td> ", "事故类型");
                sb.AppendFormat("<td align=\"left\" style=\"width: 10%;\">{0}</td> ", "次数本月");
                sb.AppendFormat("<td align=\"left\" style=\"width: 10%;\">{0}</td> ", "次数累计");
                sb.AppendFormat("<td align=\"left\" style=\"width: 10%;\">{0}</td> ", "损失工时（本月）");
                sb.AppendFormat("<td align=\"left\" style=\"width: 10%;\">{0}</td> ", "损失工时（累计）");
                sb.AppendFormat("<td align=\"left\" style=\"width: 10%;\">{0}</td> ", "经济损失（本月）");
                sb.AppendFormat("<td align=\"left\" style=\"width: 10%;\">{0}</td> ", "经济损失（累计）");
                sb.AppendFormat("<td align=\"left\" style=\"width: 10%;\">{0}</td> ", "人数当月");
                sb.AppendFormat("<td align=\"left\" style=\"width: 10%;\">{0}</td> ", "人数累计");
                sb.Append("</tr>");
                var getMonthReport3 = from x in Funs.DB.SeDin_MonthReport3
                                      where x.MonthReportId == monthReportId
                                      orderby x.SortIndex
                                      select x;
                if (getMonthReport3.Count() > 0)
                {
                    foreach (var item in getMonthReport3)
                    {
                        sb.Append("<tr style=\"height: 20px\">");
                        if (!string.IsNullOrEmpty(item.BigType) && item.SortIndex ==1)
                        {
                            sb.AppendFormat("<td align=\"center\" style=\"width: 10%;\" rowspan=\"4\">{0}</td> ", item.BigType);
                           
                        }
                        if (item.SortIndex > 4)
                        {
                            sb.AppendFormat("<td align=\"left\" style=\"width: 10%;\"  colspan=\"2\">{0}</td> ", item.AccidentType);
                        }
                        else
                        {
                            sb.AppendFormat("<td align=\"left\" style=\"width: 10%;\">{0}</td> ", item.AccidentType);
                        }                         
                        sb.AppendFormat("<td align=\"left\">{0}</td> ", item.MonthTimes);
                        sb.AppendFormat("<td align=\"left\">{0}</td> ", item.TotalTimes);
                        sb.AppendFormat("<td align=\"left\">{0}</td> ", item.MonthLossTime);
                        sb.AppendFormat("<td align=\"left\">{0}</td> ", item.TotalLossTime);
                        sb.AppendFormat("<td align=\"left\">{0}</td> ", item.MonthMoney);
                        sb.AppendFormat("<td align=\"left\">{0}</td> ", item.TotalMoney);
                        sb.AppendFormat("<td align=\"left\">{0}</td> ", item.MonthPersons);
                        sb.AppendFormat("<td align=\"left\">{0}</td> ", item.TotalPersons);

                        sb.Append("</tr>");
                    }

                    sb.Append("<tr style=\"height: 20px\">");
                    sb.AppendFormat("<td align=\"center\" colspan=\"10\" >{0}</td> ", "事故综述（含未遂事故、事件）");
                    sb.Append("</tr>");

                    sb.Append("<tr style=\"height: 60px\">");
                    sb.AppendFormat("<td align=\"left\" colspan=\"10\" >{0}</td> ", getMonthReport.AccidentsSummary);
                    sb.Append("</tr>");

                    sb.Append("</table>");
                }
                #endregion

                sb.Append("<tr style=\"height: 20px\">");
                sb.AppendFormat("<td align=\"center\" style=\"width: 100%; border-right: none;border-left: none;border-bottom: none;border-top: none; \">{0}</td> ", "");
                sb.Append("</tr>");
                #region 4、本月人员投入情况
                sb.Append("<table width=\"100% \" cellspacing=\"0\" rules=\"all\" border=\"1\" style=\"border-collapse:collapse;font-size: 10.5pt;\">");
                sb.Append("<tr style=\"height: 30px\">");
                sb.AppendFormat("<td align=\"left\" colspan=\"6\"  style=\"width: 100%;font-weight: bold;\">{0}</td> "
                    , "4、本月人员投入情况：");
                sb.Append("</tr>");

                sb.Append("<tr style=\"height: 20px\">");
                sb.AppendFormat("<td align=\"center\" style=\"width: 40%;\" rowspan=\"2\">{0}</td> ", "单位名称");
                sb.AppendFormat("<td align=\"center\"  colspan=\"2\">{0}</td> ", "管理人员");
                sb.AppendFormat("<td align=\"center\"  colspan=\"2\">{0}</td> ", "作业人员");
                sb.AppendFormat("<td align=\"center\" style=\"width: 25%;\" rowspan=\"2\">{0}</td> ", "合计");
                sb.Append("</tr>");

                sb.Append("<tr style=\"height: 20px\">");
                sb.AppendFormat("<td align=\"center\" style=\"width: 12%;\">{0}</td> ", "安全管理");
                sb.AppendFormat("<td align=\"center\" style=\"width: 12%;\">{0}</td> ", "其他管理");
                sb.AppendFormat("<td align=\"center\" style=\"width: 12%;\" >{0}</td> ", "特种作业");
                sb.AppendFormat("<td align=\"center\" style=\"width: 12%;\">{0}</td> ", "一般作业");
                sb.Append("</tr>");
                var getMonthReport4 = from x in Funs.DB.SeDin_MonthReport4
                                      where x.MonthReportId == monthReportId
                                      orderby x.UnitName
                                      select x;
                if (getMonthReport4.Count() > 0)
                {
                    foreach (var item in getMonthReport4)
                    {
                        sb.Append("<tr style=\"height: 20px\">");                      
                        sb.AppendFormat("<td align=\"center\">{0}</td> ", item.UnitName);
                        sb.AppendFormat("<td align=\"left\">{0}</td> ", item.SafeManangerNum);
                        sb.AppendFormat("<td align=\"left\">{0}</td> ", item.OtherManangerNum);
                        sb.AppendFormat("<td align=\"left\">{0}</td> ", item.SpecialWorkerNum);
                        sb.AppendFormat("<td align=\"left\">{0}</td> ", item.GeneralWorkerNum);
                        sb.AppendFormat("<td align=\"left\">{0}</td> ", item.TotalNum);
                        sb.Append("</tr>");
                    }

                    sb.Append("<tr style=\"height: 20px\">");
                    sb.AppendFormat("<td align=\"center\">{0}</td> ", "合计");
                    sb.AppendFormat("<td align=\"left\">{0}</td> ", getMonthReport4.Sum(x=>x.SafeManangerNum) ?? 0);
                    sb.AppendFormat("<td align=\"left\">{0}</td> ", getMonthReport4.Sum(x => x.OtherManangerNum) ?? 0);
                    sb.AppendFormat("<td align=\"left\">{0}</td> ", getMonthReport4.Sum(x => x.SpecialWorkerNum) ?? 0);
                    sb.AppendFormat("<td align=\"left\">{0}</td> ", getMonthReport4.Sum(x => x.GeneralWorkerNum) ?? 0);
                    sb.AppendFormat("<td align=\"left\">{0}</td> ", getMonthReport4.Sum(x => x.TotalNum) ?? 0);
                    sb.Append("</tr>");

                    sb.Append("</table>");
                }
                #endregion
                #region 4、赛鼎公司人员信息统计
                sb.Append("<table width=\"100% \" cellspacing=\"0\" rules=\"all\" border=\"1\" style=\"border-collapse:collapse;font-size: 10.5pt;\">");
                sb.Append("<tr style=\"height: 20px\">");
                sb.AppendFormat("<td align=\"center\" colspan=\"5\"  style=\"width: 100%;border-bottom: none;border-top: none;\">{0}</td> "
                    , "赛鼎公司人员信息统计");
                sb.Append("</tr>");

                sb.Append("<tr style=\"height: 20px\">");
                sb.AppendFormat("<td align=\"center\" style=\"width: 20%;\" >{0}</td> ", "项目现场正式员工总数");
                sb.AppendFormat("<td align=\"center\" style=\"width: 20%;\" >{0}</td> ", "项目现场外聘人员总数");
                sb.AppendFormat("<td align=\"center\" style=\"width: 20%;\" >{0}</td> ", "项目现场外籍人员总数");
                sb.AppendFormat("<td align=\"center\" style=\"width: 20%;\" >{0}</td> ", "项目现场HSE管理人员总数");
                sb.AppendFormat("<td align=\"center\" style=\"width: 20%;\" >{0}</td> ", "项目现场员工总数（含外聘）");
                sb.Append("</tr>");
                int count1 = 0, count2 = 0, count3 = 0, count4 = 0, count5 = 0;
                var getMonthReport4Other = Funs.DB.SeDin_MonthReport4Other.FirstOrDefault(x => x.MonthReportId == monthReportId);              
                if (getMonthReport4Other != null)
                {
                    count1 = getMonthReport4Other.FormalNum ?? 0;
                    count2 = getMonthReport4Other.OutsideNum ?? 0;
                    count3 = getMonthReport4Other.ForeignNum ?? 0;
                    count4 = getMonthReport4Other.ManagerNum ?? 0;
                    count5 = getMonthReport4Other.TotalNum ?? 0;
                }

                sb.Append("<tr style=\"height: 20px\">");
                sb.AppendFormat("<td align=\"center\">{0}</td> ", count1);
                sb.AppendFormat("<td align=\"center\">{0}</td> ", count2);
                sb.AppendFormat("<td align=\"center\">{0}</td> ", count3);
                sb.AppendFormat("<td align=\"center\">{0}</td> ", count4);
                sb.AppendFormat("<td align=\"center\">{0}</td> ", count5);
                sb.Append("</tr>");

                sb.Append("</table>");
                #endregion

                sb.Append("<tr style=\"height: 20px\">");
                sb.AppendFormat("<td align=\"center\" style=\"width: 100%; border-right: none;border-left: none;border-bottom: none;border-top: none; \">{0}</td> ", "");
                sb.Append("</tr>");
                #region 5、本月大型、特种设备投入情况
                sb.Append("<table width=\"100% \" cellspacing=\"0\" rules=\"all\" border=\"1\" style=\"border-collapse:collapse;font-size: 10.5pt;\">");
                sb.Append("<tr style=\"height: 30px\">");
                sb.AppendFormat("<td align=\"left\" colspan=\"13\"  style=\"width: 100%;font-weight: bold;\">{0}</td> "
                    , "5、本月大型、特种设备投入情况：");
                sb.Append("</tr>");
                sb.Append("<tr style=\"height: 20px\">");
                sb.AppendFormat("<td align=\"center\" style=\"width: 16%;\" rowspan=\"2\">{0}</td> ", "单位名称");
                sb.AppendFormat("<td align=\"center\"  colspan=\"6\">{0}</td> ", "特种设备");
                sb.AppendFormat("<td align=\"center\"  colspan=\"4\">{0}</td> ", "大型机具设备");
                sb.AppendFormat("<td align=\"center\" >{0}</td> ", "特殊机具设备");
                sb.AppendFormat("<td align=\"center\" style=\"width: 7%;\" rowspan=\"2\">{0}</td> ", "合计");
                sb.Append("</tr>");

                sb.Append("<tr style=\"height: 20px\">");
                sb.AppendFormat("<td align=\"center\" style=\"width: 7%;\">{0}</td> ", "汽车吊");
                sb.AppendFormat("<td align=\"center\" style=\"width: 7%;\">{0}</td> ", "履带吊");
                sb.AppendFormat("<td align=\"center\" style=\"width: 7%;\" >{0}</td> ", "塔吊");
                sb.AppendFormat("<td align=\"center\" style=\"width: 7%;\">{0}</td> ", "门式起重机");
                sb.AppendFormat("<td align=\"center\" style=\"width: 7%;\">{0}</td> ", "升降机");
                sb.AppendFormat("<td align=\"center\" style=\"width: 7%;\">{0}</td> ", "叉车");
                sb.AppendFormat("<td align=\"center\" style=\"width: 7%;\">{0}</td> ", "挖掘机");
                sb.AppendFormat("<td align=\"center\" style=\"width: 7%;\">{0}</td> ", "装载机");
                sb.AppendFormat("<td align=\"center\" style=\"width: 7%;\">{0}</td> ", "拖板车");
                sb.AppendFormat("<td align=\"center\" style=\"width: 7%;\">{0}</td> ", "桩机");
                sb.AppendFormat("<td align=\"center\" style=\"width: 7%;\">{0}</td> ", "吊篮");
                sb.Append("</tr>");
                var getMonthReport5 = from x in Funs.DB.SeDin_MonthReport5
                                      where x.MonthReportId == monthReportId
                                      orderby x.UnitName
                                      select x;
                if (getMonthReport5.Count() > 0)
                {
                    foreach (var item in getMonthReport5)
                    {
                        sb.Append("<tr style=\"height: 20px\">");
                        sb.AppendFormat("<td align=\"center\">{0}</td> ", item.UnitName);
                        sb.AppendFormat("<td align=\"left\">{0}</td> ", item.T01);
                        sb.AppendFormat("<td align=\"left\">{0}</td> ", item.T02);
                        sb.AppendFormat("<td align=\"left\">{0}</td> ", item.T03);
                        sb.AppendFormat("<td align=\"left\">{0}</td> ", item.T04);
                        sb.AppendFormat("<td align=\"left\">{0}</td> ", item.T05);
                        sb.AppendFormat("<td align=\"left\">{0}</td> ", item.T06);

                        sb.AppendFormat("<td align=\"left\">{0}</td> ", item.D01);
                        sb.AppendFormat("<td align=\"left\">{0}</td> ", item.D02);
                        sb.AppendFormat("<td align=\"left\">{0}</td> ", item.D03);
                        sb.AppendFormat("<td align=\"left\">{0}</td> ", item.D04);
                        sb.AppendFormat("<td align=\"left\">{0}</td> ", item.S01);

                        sb.AppendFormat("<td align=\"left\">{0}</td> ", (item.T01 ?? 0) + (item.T02 ?? 0) + (item.T03 ?? 0) + (item.T04 ?? 0) + (item.T05 ?? 0) + (item.T06 ?? 0)
                            + (item.D01 ?? 0) + (item.D02 ?? 0) + (item.D03 ?? 0) + (item.D04 ?? 0) + (item.S01 ?? 0));
                        sb.Append("</tr>");
                    }

                    sb.Append("<tr style=\"height: 20px\">");
                    sb.AppendFormat("<td align=\"center\">{0}</td> ", "合计");
                    int sumt01 = getMonthReport5.Sum(x => x.T01) ?? 0;
                    int sumt02 = getMonthReport5.Sum(x => x.T02) ?? 0;
                    int sumt03 = getMonthReport5.Sum(x => x.T03) ?? 0;
                    int sumt04 = getMonthReport5.Sum(x => x.T04) ?? 0;
                    int sumt05 = getMonthReport5.Sum(x => x.T05) ?? 0;
                    int sumt06 = getMonthReport5.Sum(x => x.T06) ?? 0;
                    int sumd01 = getMonthReport5.Sum(x => x.D01) ?? 0;
                    int sumd02 = getMonthReport5.Sum(x => x.D02) ?? 0;
                    int sumd03 = getMonthReport5.Sum(x => x.D03) ?? 0;
                    int sumd04 = getMonthReport5.Sum(x => x.D04) ?? 0;
                    int sums01 = getMonthReport5.Sum(x => x.S01) ?? 0;
                    sb.AppendFormat("<td align=\"left\">{0}</td> ", sumt01);
                    sb.AppendFormat("<td align=\"left\">{0}</td> ", sumt02);
                    sb.AppendFormat("<td align=\"left\">{0}</td> ", sumt03);
                    sb.AppendFormat("<td align=\"left\">{0}</td> ", sumt04);
                    sb.AppendFormat("<td align=\"left\">{0}</td> ", sumt05);
                    sb.AppendFormat("<td align=\"left\">{0}</td> ", sumt06);
                    sb.AppendFormat("<td align=\"left\">{0}</td> ", sumd01);
                    sb.AppendFormat("<td align=\"left\">{0}</td> ", sumd02);
                    sb.AppendFormat("<td align=\"left\">{0}</td> ", sumd03);
                    sb.AppendFormat("<td align=\"left\">{0}</td> ", sumd04);
                    sb.AppendFormat("<td align=\"left\">{0}</td> ", sums01);
                    sb.AppendFormat("<td align=\"left\">{0}</td> ", sumt02+ sumt03+ sumt01+ sumt05+ sumt06+ sumd01+ sumd02 + sumd03 + sumd04 + sums01);
                    sb.Append("</tr>");

                    sb.Append("</table>");
                }
                #endregion

                sb.Append("<tr style=\"height: 20px\">");
                sb.AppendFormat("<td align=\"center\" style=\"width: 100%; border-right: none;border-left: none;border-bottom: none;border-top: none; \">{0}</td> ", "");
                sb.Append("</tr>");
                #region 6、安全生产费用投入情况
                sb.Append("<table width=\"100% \" cellspacing=\"0\" rules=\"all\" border=\"1\" style=\"border-collapse:collapse;font-size: 10.5pt;\">");
                sb.Append("<tr style=\"height: 30px\">");
                sb.AppendFormat("<td align=\"left\" colspan=\"7\"  style=\"width: 100%;font-weight: bold;\">{0}</td> "
                    , "6、安全生产费用投入情况：");
                sb.Append("</tr>");
                sb.Append("<tr style=\"height: 20px\">");
                sb.AppendFormat("<td align=\"center\" style=\"width: 16%;\">{0}</td> ", "");
                sb.AppendFormat("<td align=\"center\" style=\"width: 14%;\">{0}</td> ", "安全防护投入");
                sb.AppendFormat("<td align=\"center\" style=\"width: 14%;\">{0}</td> ", "劳动保护及职业健康投入");
                sb.AppendFormat("<td align=\"center\" style=\"width: 14%;\">{0}</td> ", "安全技术进步投入");
                sb.AppendFormat("<td align=\"center\" style=\"width: 14%;\">{0}</td> ", "安全教育培训投入");
                sb.AppendFormat("<td align=\"center\" style=\"width: 14%;\">{0}</td> ", "合计");
                sb.AppendFormat("<td align=\"center\" style=\"width: 14%;\">{0}</td> ", "完成合同额");
                sb.Append("</tr>");
                var getMonthReport6 = Funs.DB.SeDin_MonthReport6.FirstOrDefault(x => x.MonthReportId == monthReportId);
                if (getMonthReport6 != null)
                {
                    sb.Append("<tr style=\"height: 20px\">");
                    sb.AppendFormat("<td align=\"center\">{0}</td> ","本月");
                    sb.AppendFormat("<td align=\"left\">{0}</td> ", getMonthReport6.SafetyMonth);
                    sb.AppendFormat("<td align=\"left\">{0}</td> ", getMonthReport6.LaborMonth);
                    sb.AppendFormat("<td align=\"left\">{0}</td> ", getMonthReport6.ProgressMonth);
                    sb.AppendFormat("<td align=\"left\">{0}</td> ", getMonthReport6.EducationMonth);
                    sb.AppendFormat("<td align=\"left\">{0}</td> ", getMonthReport6.SumMonth);
                    sb.AppendFormat("<td align=\"left\">{0}</td> ", getMonthReport6.ContractMonth);
                    sb.Append("</tr>");
                    sb.Append("<tr style=\"height: 20px\">");
                    sb.AppendFormat("<td align=\"center\">{0}</td> ", "年度累计");
                    sb.AppendFormat("<td align=\"left\">{0}</td> ", getMonthReport6.SafetyYear);
                    sb.AppendFormat("<td align=\"left\">{0}</td> ", getMonthReport6.LaborYear);
                    sb.AppendFormat("<td align=\"left\">{0}</td> ", getMonthReport6.ProgressYear);
                    sb.AppendFormat("<td align=\"left\">{0}</td> ", getMonthReport6.EducationYear);
                    sb.AppendFormat("<td align=\"left\">{0}</td> ", getMonthReport6.SumYear);
                    sb.AppendFormat("<td align=\"left\">{0}</td> ", getMonthReport6.ContractYear);
                    sb.Append("</tr>");
                    sb.Append("<tr style=\"height: 20px\">");
                    sb.AppendFormat("<td align=\"center\">{0}</td> ", "项目累计");
                    sb.AppendFormat("<td align=\"left\">{0}</td> ", getMonthReport6.SafetyTotal);
                    sb.AppendFormat("<td align=\"left\">{0}</td> ", getMonthReport6.LaborTotal);
                    sb.AppendFormat("<td align=\"left\">{0}</td> ", getMonthReport6.ProgressTotal);
                    sb.AppendFormat("<td align=\"left\">{0}</td> ", getMonthReport6.EducationTotal);
                    sb.AppendFormat("<td align=\"left\">{0}</td> ", getMonthReport6.SumTotal);
                    sb.AppendFormat("<td align=\"left\">{0}</td> ", getMonthReport6.ContractTotal);
                    sb.Append("</tr>");
                    sb.Append("<tr style=\"height: 20px\">");
                    sb.AppendFormat("<td align=\"center\" colspan=\"3\">{0}</td> ", "工程造价占比");
                    sb.AppendFormat("<td align=\"left\" colspan=\"4\">{0}</td> ", getMonthReport6.ConstructionCost);
                    sb.Append("</tr>");
                    sb.Append("</table>");
                }
                #endregion

                sb.Append("<tr style=\"height: 20px\">");
                sb.AppendFormat("<td align=\"center\" style=\"width: 100%; border-right: none;border-left: none;border-bottom: none;border-top: none; \">{0}</td> ", "");
                sb.Append("</tr>");
                #region 7、项目HSE培训统计
                sb.Append("<table width=\"100% \" cellspacing=\"0\" rules=\"all\" border=\"1\" style=\"border-collapse:collapse;font-size: 10.5pt;\">");
                sb.Append("<tr style=\"height: 30px\">");
                sb.AppendFormat("<td align=\"left\" colspan=\"7\"  style=\"width: 100%;font-weight: bold;\">{0}</td> "
                    , "7、项目HSE培训统计：");
                sb.Append("</tr>");
                sb.Append("<tr style=\"height: 20px\">");
                sb.AppendFormat("<td align=\"center\" style=\"width: 16%;\" rowspan=\"2\">{0}</td> ", "培训课程类型");
                sb.AppendFormat("<td align=\"center\" colspan=\"3\">{0}</td> ", "次数");
                sb.AppendFormat("<td align=\"center\" colspan=\"3\">{0}</td> ", "参加人次");
                sb.Append("</tr>");
                sb.Append("<tr style=\"height: 20px\">");
                sb.AppendFormat("<td align=\"center\" >{0}</td> ", "本月");
                sb.AppendFormat("<td align=\"center\" >{0}</td> ", "本年度");
                sb.AppendFormat("<td align=\"center\" >{0}</td> ", "项目累计");
                sb.AppendFormat("<td align=\"center\" >{0}</td> ", "本月");
                sb.AppendFormat("<td align=\"center\" >{0}</td> ", "本年度");
                sb.AppendFormat("<td align=\"center\" >{0}</td> ", "项目累计");
                sb.Append("</tr>");

                var getMonthReport7 = Funs.DB.SeDin_MonthReport7.FirstOrDefault(x => x.MonthReportId == monthReportId);
                if (getMonthReport7 != null)
                {
                    sb.Append("<tr style=\"height: 20px\">");
                    sb.AppendFormat("<td align=\"center\">{0}</td> ", "专项安全培训");
                    sb.AppendFormat("<td align=\"left\" style=\"width: 14%;\">{0}</td> ", getMonthReport7.SpecialMontNum);
                    sb.AppendFormat("<td align=\"left\" style=\"width: 14%;\">{0}</td> ", getMonthReport7.SpecialYearNum);
                    sb.AppendFormat("<td align=\"left\" style=\"width: 14%;\">{0}</td> ", getMonthReport7.SpecialTotalNum);
                    sb.AppendFormat("<td align=\"left\" style=\"width: 14%;\">{0}</td> ", getMonthReport7.SpecialMontPerson);
                    sb.AppendFormat("<td align=\"left\" style=\"width: 14%;\">{0}</td> ", getMonthReport7.SpecialYearPerson);
                    sb.AppendFormat("<td align=\"left\" style=\"width: 14%;\">{0}</td> ", getMonthReport7.SpecialTotalPerson);
                    sb.Append("</tr>");
                    sb.Append("<tr style=\"height: 20px\">");
                    sb.AppendFormat("<td align=\"center\">{0}</td> ", "员工入场安全培训");
                    sb.AppendFormat("<td align=\"left\">{0}</td> ", getMonthReport7.EmployeeMontNum);
                    sb.AppendFormat("<td align=\"left\">{0}</td> ", getMonthReport7.EmployeeYearNum);
                    sb.AppendFormat("<td align=\"left\">{0}</td> ", getMonthReport7.EmployeeTotalNum);
                    sb.AppendFormat("<td align=\"left\">{0}</td> ", getMonthReport7.EmployeeMontPerson);
                    sb.AppendFormat("<td align=\"left\">{0}</td> ", getMonthReport7.EmployeeYearPerson);
                    sb.AppendFormat("<td align=\"left\">{0}</td> ", getMonthReport7.EmployeeTotalPerson);
                    sb.Append("</tr>");
                    sb.Append("</table>");
                }
                #endregion

                sb.Append("<tr style=\"height: 20px\">");
                sb.AppendFormat("<td align=\"center\" style=\"width: 100%; border-right: none;border-left: none;border-bottom: none;border-top: none; \">{0}</td> ", "");
                sb.Append("</tr>");
                #region 8、项目HSE会议统计
                sb.Append("<table width=\"100% \" cellspacing=\"0\" rules=\"all\" border=\"1\" style=\"border-collapse:collapse;font-size: 10.5pt;\">");
                sb.Append("<tr style=\"height: 30px\">");
                sb.AppendFormat("<td align=\"left\" colspan=\"4\"  style=\"width: 100%;font-weight: bold;\">{0}</td> "
                    , "8、项目HSE会议统计：");
                sb.Append("</tr>");
                sb.Append("<tr style=\"height: 20px\">");
                sb.AppendFormat("<td align=\"center\" style=\"width: 25%;\">{0}</td> ", "会议类型");
                sb.AppendFormat("<td align=\"center\" style=\"width: 25%;\">{0}</td> ", "次数（本月）");
                sb.AppendFormat("<td align=\"center\" style=\"width: 25%;\">{0}</td> ", "次数（累计）");
                sb.AppendFormat("<td align=\"center\" style=\"width: 25%;\">{0}</td> ", "参会人次（本月）");
                sb.Append("</tr>");
                var getMonthReport8 = Funs.DB.SeDin_MonthReport8.FirstOrDefault(x => x.MonthReportId == monthReportId);
                if (getMonthReport8 != null)
                {
                    sb.Append("<tr style=\"height: 20px\">");
                    sb.AppendFormat("<td align=\"center\">{0}</td> ", "周例会");
                    sb.AppendFormat("<td align=\"left\">{0}</td> ", getMonthReport8.WeekMontNum);
                    sb.AppendFormat("<td align=\"left\">{0}</td> ", getMonthReport8.WeekTotalNum);
                    sb.AppendFormat("<td align=\"left\">{0}</td> ", getMonthReport8.WeekMontPerson);
                    sb.Append("</tr>");

                    sb.Append("<tr style=\"height: 20px\">");
                    sb.AppendFormat("<td align=\"center\">{0}</td> ", "月例会（安委会）");
                    sb.AppendFormat("<td align=\"left\">{0}</td> ", getMonthReport8.MonthMontNum);
                    sb.AppendFormat("<td align=\"left\">{0}</td> ", getMonthReport8.MonthTotalNum);
                    sb.AppendFormat("<td align=\"left\">{0}</td> ", getMonthReport8.MonthMontPerson);
                    sb.Append("</tr>");

                    sb.Append("<tr style=\"height: 20px\">");
                    sb.AppendFormat("<td align=\"center\">{0}</td> ", "专题会议");
                    sb.AppendFormat("<td align=\"left\">{0}</td> ", getMonthReport8.SpecialMontNum);
                    sb.AppendFormat("<td align=\"left\">{0}</td> ", getMonthReport8.SpecialTotalNum);
                    sb.AppendFormat("<td align=\"left\">{0}</td> ", getMonthReport8.SpecialMontPerson);
                    sb.Append("</tr>");
                    sb.Append("<tr style=\"height: 20px\">");
                    sb.AppendFormat("<td align=\"center\" colspan=\"4\"  style=\"width: 100%;font-weight: bold;\">{0}</td> "
                        , "班前会");
                    sb.Append("</tr>");

                    sb.Append("<tr style=\"height: 20px\">");
                    sb.AppendFormat("<td align=\"center\">{0}</td> ", "单位名称");
                    sb.AppendFormat("<td align=\"center\">{0}</td> ", "班组名称");
                    sb.AppendFormat("<td align=\"center\">{0}</td> ", "会议次数（本月）");
                    sb.AppendFormat("<td align=\"center\">{0}</td> ", "参会人数累计（本月）");
                    sb.Append("</tr>");
                    var get8Items = from x in Funs.DB.SeDin_MonthReport8Item
                                    where x.MonthReportId == monthReportId
                                    orderby x.UnitName,x.TeamName
                                    select x;
                    foreach (var item in get8Items)
                    {
                        sb.Append("<tr style=\"height: 20px\">");
                        sb.AppendFormat("<td align=\"center\">{0}</td> ", item.UnitName);
                        sb.AppendFormat("<td align=\"center\">{0}</td> ", item.TeamName);
                        sb.AppendFormat("<td align=\"center\">{0}</td> ", item.ClassNum);
                        sb.AppendFormat("<td align=\"center\">{0}</td> ", item.ClassPersonNum);
                        sb.Append("</tr>");
                    }
                    sb.Append("</table>");
                }
                #endregion

                sb.Append("<tr style=\"height: 20px\">");
                sb.AppendFormat("<td align=\"center\" style=\"width: 100%; border-right: none;border-left: none;border-bottom: none;border-top: none; \">{0}</td> ", "");
                sb.Append("</tr>");
                #region 9、项目HSE检查统计
                sb.Append("<table width=\"100% \" cellspacing=\"0\" rules=\"all\" border=\"1\" style=\"border-collapse:collapse;font-size: 10.5pt;\">");
                sb.Append("<tr style=\"height: 30px\">");
                sb.AppendFormat("<td align=\"left\" colspan=\"4\"  style=\"width: 100%;font-weight: bold;\">{0}</td> "
                    , "9、项目HSE检查统计：");
                sb.Append("</tr>");
                sb.Append("<tr style=\"height: 20px\">");
                sb.AppendFormat("<td align=\"center\" style=\"width: 25%;\">{0}</td> ", "检查类型");
                sb.AppendFormat("<td align=\"center\" style=\"width: 25%;\">{0}</td> ", "次数（本月）");
                sb.AppendFormat("<td align=\"center\" style=\"width: 25%;\">{0}</td> ", "次数（本年度累计）");
                sb.AppendFormat("<td align=\"center\" style=\"width: 25%;\">{0}</td> ", "次数（项目总累计）");
                sb.Append("</tr>");
                var getMonthReport9 = Funs.DB.SeDin_MonthReport9.FirstOrDefault(x => x.MonthReportId == monthReportId);
                if (getMonthReport9 != null)
                {
                    sb.Append("<tr style=\"height: 20px\">");
                    sb.AppendFormat("<td align=\"center\">{0}</td> ", "日常巡检");
                    sb.AppendFormat("<td align=\"left\">{0}</td> ", getMonthReport9.DailyMonth);
                    sb.AppendFormat("<td align=\"left\">{0}</td> ", getMonthReport9.DailyYear);
                    sb.AppendFormat("<td align=\"left\">{0}</td> ", getMonthReport9.DailyTotal);
                    sb.Append("</tr>");

                    sb.Append("<tr style=\"height: 20px\">");
                    sb.AppendFormat("<td align=\"center\">{0}</td> ", "周联合检查");
                    sb.AppendFormat("<td align=\"left\">{0}</td> ", getMonthReport9.WeekMonth);
                    sb.AppendFormat("<td align=\"left\">{0}</td> ", getMonthReport9.WeekYear);
                    sb.AppendFormat("<td align=\"left\">{0}</td> ", getMonthReport9.WeekTotal);
                    sb.Append("</tr>");

                    sb.Append("<tr style=\"height: 20px\">");
                    sb.AppendFormat("<td align=\"center\">{0}</td> ", "专项检查");
                    sb.AppendFormat("<td align=\"left\">{0}</td> ", getMonthReport9.SpecialMonth);
                    sb.AppendFormat("<td align=\"left\">{0}</td> ", getMonthReport9.SpecialYear);
                    sb.AppendFormat("<td align=\"left\">{0}</td> ", getMonthReport9.SpecialTotal);
                    sb.Append("</tr>");

                    sb.Append("<tr style=\"height: 20px\">");
                    sb.AppendFormat("<td align=\"center\">{0}</td> ", "月综合HSE检查");
                    sb.AppendFormat("<td align=\"left\">{0}</td> ", getMonthReport9.MonthlyMonth);
                    sb.AppendFormat("<td align=\"left\">{0}</td> ", getMonthReport9.MonthlyYear);
                    sb.AppendFormat("<td align=\"left\">{0}</td> ", getMonthReport9.MonthlyTotal);
                    sb.Append("</tr>");

                    sb.Append("<tr style=\"height: 20px\">");
                    sb.AppendFormat("<td align=\"center\" colspan=\"4\"  style=\"width: 100%;font-weight: bold;\">{0}</td> "
                        , "专项检查");
                    sb.Append("</tr>");

                    sb.Append("<tr style=\"height: 20px\">");
                    sb.AppendFormat("<td align=\"center\">{0}</td> ", "类型");
                    sb.AppendFormat("<td align=\"center\">{0}</td> ", "检查次数（本月）");
                    sb.AppendFormat("<td align=\"center\">{0}</td> ", "次数（本年度累计）");
                    sb.AppendFormat("<td align=\"center\">{0}</td> ", "次数（项目总累计）");
                    sb.Append("</tr>");
                    var get9ItemSpecials = from x in Funs.DB.SeDin_MonthReport9Item_Special
                                    where x.MonthReportId == monthReportId
                                    orderby x.TypeName
                                    select x;
                    foreach (var item in get9ItemSpecials)
                    {
                        sb.Append("<tr style=\"height: 20px\">");
                        sb.AppendFormat("<td align=\"center\">{0}</td> ", item.TypeName);
                        sb.AppendFormat("<td align=\"center\">{0}</td> ", item.CheckMonth);
                        sb.AppendFormat("<td align=\"center\">{0}</td> ", item.CheckYear);
                        sb.AppendFormat("<td align=\"center\">{0}</td> ", item.CheckTotal);
                        sb.Append("</tr>");
                    }

                    sb.Append("<tr style=\"height: 20px\">");
                    sb.AppendFormat("<td align=\"center\" colspan=\"4\"  style=\"width: 100%;font-weight: bold;border-bottom: none;\">{0}</td> "
                        , "隐患整改单");
                    sb.Append("</tr>");
                    sb.Append("<tr style=\"height: 20px\">");
                    sb.Append("<td align=\"center\" colspan=\"4\"  style=\"width: 100%;\">");
                    sb.Append("<table width=\"100% \" cellspacing=\"0\" rules=\"all\" border=\"1\" style=\"border-collapse:collapse;font-size: 10.5pt;\">");
                    sb.Append("<tr style=\"height: 20px\">");
                    sb.AppendFormat("<td align=\"center\"  rowspan=\"2\" style=\"width: 16%;border-left: none;border-top: none; \">{0}</td> ", "单位名称");
                    sb.AppendFormat("<td align=\"center\" colspan=\"3\"  style=\"width: 21%;border-top: none; \">{0}</td> ", "下发数量（本月）");
                    sb.AppendFormat("<td align=\"center\" colspan=\"3\"  style=\"width: 21%;border-top: none; \">{0}</td> ", "整改完成数量（本月）");
                    sb.AppendFormat("<td align=\"center\" colspan=\"3\"  style=\"width: 21%;border-top: none; \">{0}</td> ", "下发数量（累计）");
                    sb.AppendFormat("<td align=\"center\" colspan=\"3\"  style=\"width: 21%;border-top: none; border-right: none;\">{0}</td> ", "整改完成数量（累计）");
                    sb.Append("</tr>");
                    sb.Append("<tr style=\"height: 20px\">");
                    sb.AppendFormat("<td align=\"center\" style=\"width: 7%;border-left: none;border-top: none; \">{0}</td> ", "一般");
                    sb.AppendFormat("<td align=\"center\" style=\"width: 7%;border-top: none; \">{0}</td> ", "较大");
                    sb.AppendFormat("<td align=\"center\" style=\"width: 7%;border-top: none; \">{0}</td> ", "重大");
                    sb.AppendFormat("<td align=\"center\" style=\"width: 7%;border-top: none; \">{0}</td> ", "一般");
                    sb.AppendFormat("<td align=\"center\" style=\"width: 7%;border-top: none; \">{0}</td> ", "较大");
                    sb.AppendFormat("<td align=\"center\" style=\"width: 7%;border-top: none; \">{0}</td> ", "重大");
                    sb.AppendFormat("<td align=\"center\" style=\"width: 7%;border-top: none; \">{0}</td> ", "一般");
                    sb.AppendFormat("<td align=\"center\" style=\"width: 7%;border-top: none; \">{0}</td> ", "较大");
                    sb.AppendFormat("<td align=\"center\" style=\"width: 7%;border-top: none; \">{0}</td> ", "重大");
                    sb.AppendFormat("<td align=\"center\" style=\"width: 7%;border-top: none; \">{0}</td> ", "一般");
                    sb.AppendFormat("<td align=\"center\" style=\"width: 7%;border-top: none; \">{0}</td> ", "较大");
                    sb.AppendFormat("<td align=\"center\" style=\"width: 7%;border-top: none; border-right: none;\">{0}</td> ", "重大");
                    sb.Append("</tr>");
                    var get9ItemRectifications = from x in Funs.DB.SeDin_MonthReport9Item_Rectification
                                           where x.MonthReportId == monthReportId
                                           orderby x.UnitName
                                                 select x;
                    foreach (var itemr in get9ItemRectifications)
                    {
                        sb.Append("<tr style=\"height: 20px\">");
                        sb.AppendFormat("<td align=\"center\" style=\"border-left: none;border-bottom: none;\">{0}</td> ", itemr.UnitName);
                        sb.AppendFormat("<td align=\"center\" style=\"border-bottom: none;\">{0}</td> ", itemr.IssuedMonth ?? 0);
                        sb.AppendFormat("<td align=\"center\" style=\"border-bottom: none;\">{0}</td> ", itemr.IssuedMonthLarge ?? 0);
                        sb.AppendFormat("<td align=\"center\" style=\"border-bottom: none;\">{0}</td> ", itemr.IssuedMonthSerious ?? 0);
                        sb.AppendFormat("<td align=\"center\" style=\"border-bottom: none;\">{0}</td> ", itemr.RectificationMoth ?? 0);
                        sb.AppendFormat("<td align=\"center\" style=\"border-bottom: none;\">{0}</td> ", itemr.RectificationMothLarge ?? 0);
                        sb.AppendFormat("<td align=\"center\" style=\"border-bottom: none;\">{0}</td> ", itemr.RectificationMothSerious ?? 0);
                        sb.AppendFormat("<td align=\"center\" style=\"border-bottom: none;\">{0}</td> ", itemr.IssuedTotal ?? 0);
                        sb.AppendFormat("<td align=\"center\" style=\"border-bottom: none;\">{0}</td> ", itemr.IssuedTotalLarge ?? 0);
                        sb.AppendFormat("<td align=\"center\" style=\"border-bottom: none;\">{0}</td> ", itemr.IssuedTotalSerious ?? 0);
                        sb.AppendFormat("<td align=\"center\" style=\"border-bottom: none;\">{0}</td> ", itemr.RectificationTotal ?? 0);
                        sb.AppendFormat("<td align=\"center\" style=\"border-bottom: none;\">{0}</td> ", itemr.RectificationTotalLarge ?? 0);
                        sb.AppendFormat("<td align=\"center\" style=\"border-right: none;border-bottom: none;\">{0}</td> ", itemr.RectificationTotalSerious ?? 0);
                        sb.Append("</tr>");
                    }
                    sb.Append("</table>");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr style=\"height: 20px\">");
                    sb.AppendFormat("<td align=\"center\" colspan=\"4\"  style=\"width: 100%;font-weight: bold;border-bottom: none;border-top: none;\">{0}</td> "
                        , "停工令");
                    sb.Append("</tr>");
                    sb.Append("<tr style=\"height: 20px\">");
                    sb.Append("<td align=\"center\" colspan=\"4\"  style=\"width: 100%;\">");
                    sb.Append("<table width=\"100% \" cellspacing=\"0\" rules=\"all\" border=\"1\" style=\"border-collapse:collapse;font-size: 10.5pt;\">");
                    sb.Append("<tr style=\"height: 20px\">");
                    sb.AppendFormat("<td align=\"center\" style=\"width: 20%;border-left: none;border-top: none; \">{0}</td> ", "单位名称");
                    sb.AppendFormat("<td align=\"center\" style=\"width: 20%;border-top: none;\">{0}</td> ", "下发数量（本月）");
                    sb.AppendFormat("<td align=\"center\" style=\"width: 20%;border-top: none;\">{0}</td> ", "停工天数（本月）");
                    sb.AppendFormat("<td align=\"center\" style=\"width: 20%;border-top: none;\">{0}</td> ", "下发数量（累计）");
                    sb.AppendFormat("<td align=\"center\" style=\"width: 20%;border-top: none; border-right: none;\">{0}</td> ", "停工天数（累计）");
                    sb.Append("</tr>");
                    var get9ItemStoppages = from x in Funs.DB.SeDin_MonthReport9Item_Stoppage
                                                 where x.MonthReportId == monthReportId
                                                 orderby x.UnitName
                                                 select x;
                    foreach (var items in get9ItemStoppages)
                    {
                        sb.Append("<tr style=\"height: 20px\">");
                        sb.AppendFormat("<td align=\"center\" style=\"border-left: none;border-bottom: none;\">{0}</td> ", items.UnitName);
                        sb.AppendFormat("<td align=\"center\" style=\"border-bottom: none;\">{0}</td> ", items.IssuedMonth);
                        sb.AppendFormat("<td align=\"center\" style=\"border-bottom: none;\">{0}</td> ", items.StoppageMonth);
                        sb.AppendFormat("<td align=\"center\" style=\"border-bottom: none;\">{0}</td> ", items.IssuedTotal);
                        sb.AppendFormat("<td align=\"center\" style=\"border-right: none;border-bottom: none;\">{0}</td> ", items.StoppageTotal);
                        sb.Append("</tr>");
                    }
                    sb.Append("</table>");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                    sb.Append("</table>");
                }
                #endregion

                sb.Append("<tr style=\"height: 20px\">");
                sb.AppendFormat("<td align=\"center\" style=\"width: 100%; border-right: none;border-left: none;border-bottom: none;border-top: none; \">{0}</td> ", "");
                sb.Append("</tr>");
                #region 10、项目奖惩情况统计
                sb.Append("<table width=\"100% \" cellspacing=\"0\" rules=\"all\" border=\"1\" style=\"border-collapse:collapse;font-size: 10.5pt;\">");
                sb.Append("<tr style=\"height: 30px\">");
                sb.AppendFormat("<td align=\"left\" colspan=\"6\"  style=\"width: 100%;font-weight: bold;\">{0}</td> "
                    , "10、项目奖惩情况统计：");
                sb.Append("</tr>");
                sb.Append("<tr style=\"height: 20px\">");
                sb.AppendFormat("<td align=\"center\" style=\"width: 15%;\">{0}</td> ", "类型");
                sb.AppendFormat("<td align=\"center\" style=\"width: 25%;\">{0}</td> ", "内容");
                sb.AppendFormat("<td align=\"center\" style=\"width: 15%;\">{0}</td> ", "次数（本月）");
                sb.AppendFormat("<td align=\"center\" style=\"width: 15%;\">{0}</td> ", "次数（累计）");
                sb.AppendFormat("<td align=\"center\" style=\"width: 15%;\">{0}</td> ", "金额（本月）");
                sb.AppendFormat("<td align=\"center\" style=\"width: 15%;\">{0}</td> ", "金额（累计）");
                sb.Append("</tr>");
                var getMonthReport10 = Funs.DB.SeDin_MonthReport10.FirstOrDefault(x => x.MonthReportId == monthReportId);
                if (getMonthReport10 != null)
                {
                    sb.Append("<tr style=\"height: 20px\">");
                    sb.AppendFormat("<td align=\"center\" rowspan=\"3\">{0}</td> ", "奖励");
                    sb.AppendFormat("<td align=\"left\" >{0}</td> ", "安全工时奖");
                    sb.AppendFormat("<td align=\"left\" >{0}</td> ", getMonthReport10.SafeMonthNum);
                    sb.AppendFormat("<td align=\"left\" >{0}</td> ", getMonthReport10.SafeTotalNum);
                    sb.AppendFormat("<td align=\"left\" >{0}</td> ", getMonthReport10.SafeMonthMoney);
                    sb.AppendFormat("<td align=\"left\" >{0}</td> ", getMonthReport10.SafeTotalMoney);
                    sb.Append("</tr>");

                    sb.Append("<tr style=\"height: 20px\">");
                    sb.AppendFormat("<td align=\"left\" >{0}</td> ", "HSE绩效考核奖励");
                    sb.AppendFormat("<td align=\"left\" >{0}</td> ", getMonthReport10.HseMonthNum);
                    sb.AppendFormat("<td align=\"left\" >{0}</td> ", getMonthReport10.HseTotalNum);
                    sb.AppendFormat("<td align=\"left\" >{0}</td> ", getMonthReport10.HseMonthMoney);
                    sb.AppendFormat("<td align=\"left\" >{0}</td> ", getMonthReport10.HseTotalMoney);
                    sb.Append("</tr>");

                    sb.Append("<tr style=\"height: 20px\">");
                    sb.AppendFormat("<td align=\"left\" >{0}</td> ", "安全生产先进个人奖");
                    sb.AppendFormat("<td align=\"left\" >{0}</td> ", getMonthReport10.ProduceMonthNum);
                    sb.AppendFormat("<td align=\"left\" >{0}</td> ", getMonthReport10.ProduceTotalNum);
                    sb.AppendFormat("<td align=\"left\" >{0}</td> ", getMonthReport10.ProduceMonthMoney);
                    sb.AppendFormat("<td align=\"left\" >{0}</td> ", getMonthReport10.ProduceTotalMoney);
                    sb.Append("</tr>");

                    sb.Append("<tr style=\"height: 20px\">");
                    sb.AppendFormat("<td align=\"center\" rowspan=\"3\">{0}</td> ", "处罚");
                    sb.AppendFormat("<td align=\"left\" >{0}</td> ", "事故责任处罚");
                    sb.AppendFormat("<td align=\"left\" >{0}</td> ", getMonthReport10.AccidentMonthNum);
                    sb.AppendFormat("<td align=\"left\" >{0}</td> ", getMonthReport10.AccidentTotalNum);
                    sb.AppendFormat("<td align=\"left\" >{0}</td> ", getMonthReport10.AccidentMonthMoney);
                    sb.AppendFormat("<td align=\"left\" >{0}</td> ", getMonthReport10.AccidentTotalMoney);
                    sb.Append("</tr>");

                    sb.Append("<tr style=\"height: 20px\">");
                    sb.AppendFormat("<td align=\"left\" >{0}</td> ", "违章处罚");
                    sb.AppendFormat("<td align=\"left\" >{0}</td> ", getMonthReport10.ViolationMonthNum);
                    sb.AppendFormat("<td align=\"left\" >{0}</td> ", getMonthReport10.ViolationTotalNum);
                    sb.AppendFormat("<td align=\"left\" >{0}</td> ", getMonthReport10.ViolationMonthMoney);
                    sb.AppendFormat("<td align=\"left\" >{0}</td> ", getMonthReport10.ViolationTotalMoney);
                    sb.Append("</tr>");

                    sb.Append("<tr style=\"height: 20px\">");
                    sb.AppendFormat("<td align=\"left\" >{0}</td> ", "安全管理处罚");
                    sb.AppendFormat("<td align=\"left\" >{0}</td> ", getMonthReport10.ManageMonthNum);
                    sb.AppendFormat("<td align=\"left\" >{0}</td> ", getMonthReport10.ManageTotalNum);
                    sb.AppendFormat("<td align=\"left\" >{0}</td> ", getMonthReport10.ManageMonthMoney);
                    sb.AppendFormat("<td align=\"left\" >{0}</td> ", getMonthReport10.ManageTotalMoney);
                    sb.Append("</tr>");
                    sb.Append("</table>");
                }
                #endregion

                sb.Append("<tr style=\"height: 20px\">");
                sb.AppendFormat("<td align=\"center\" style=\"width: 100%; border-right: none;border-left: none;border-bottom: none;border-top: none; \">{0}</td> ", "");
                sb.Append("</tr>");
                #region 11、项目危大工程施工情况
                sb.Append("<table width=\"100% \" cellspacing=\"0\" rules=\"all\" border=\"1\" style=\"border-collapse:collapse;font-size: 10.5pt;\">");
                sb.Append("<tr style=\"height: 30px\">");
                sb.AppendFormat("<td align=\"left\" colspan=\"4\"  style=\"width: 100%;font-weight: bold;\">{0}</td> "
                    , "11、项目危大工程施工情况：");
                sb.Append("</tr>");

                sb.Append("<tr style=\"height: 20px\">");
                sb.AppendFormat("<td align=\"center\" style=\"width: 25%;\">{0}</td> ", "类别");
                sb.AppendFormat("<td align=\"center\" style=\"width: 25%;\">{0}</td> ", "本月正在施工");
                sb.AppendFormat("<td align=\"center\" style=\"width: 25%;\">{0}</td> ", "已完工");
                sb.AppendFormat("<td align=\"center\" style=\"width: 25%;\">{0}</td> ", "下月施工计划");
                sb.Append("</tr>");
                var getMonthReport11= Funs.DB.SeDin_MonthReport11.FirstOrDefault(x => x.MonthReportId == monthReportId);
                if (getMonthReport11 != null)
                {
                    sb.Append("<tr style=\"height: 20px\">");
                    sb.AppendFormat("<td align=\"left\" >{0}</td> ", "危险性较大分部分项工程");
                    sb.AppendFormat("<td align=\"left\" >{0}</td> ", getMonthReport11.RiskWorkNum);
                    sb.AppendFormat("<td align=\"left\" >{0}</td> ", getMonthReport11.RiskFinishedNum);
                    sb.AppendFormat("<td align=\"left\" >{0}</td> ", getMonthReport11.RiskWorkNext);
                    sb.Append("</tr>");

                    sb.Append("<tr style=\"height: 20px\">");
                    sb.AppendFormat("<td align=\"left\" >{0}</td> ", "超过一定规模危大工程");
                    sb.AppendFormat("<td align=\"left\" >{0}</td> ", getMonthReport11.LargeWorkNum);
                    sb.AppendFormat("<td align=\"left\" >{0}</td> ", getMonthReport11.LargeFinishedNum);
                    sb.AppendFormat("<td align=\"left\" >{0}</td> ", getMonthReport11.LargeWorkNext);
                    sb.Append("</tr>");
                    sb.Append("</table>");
                }
                #endregion

                sb.Append("<tr style=\"height: 20px\">");
                sb.AppendFormat("<td align=\"center\" style=\"width: 100%; border-right: none;border-left: none;border-bottom: none;border-top: none; \">{0}</td> ", "");
                sb.Append("</tr>");
                #region 12、项目应急演练情况
                sb.Append("<table width=\"100% \" cellspacing=\"0\" rules=\"all\" border=\"1\" style=\"border-collapse:collapse;font-size: 10.5pt;\">");
                sb.Append("<tr style=\"height: 30px\">");
                sb.AppendFormat("<td align=\"left\" colspan=\"7\"  style=\"width: 100%;font-weight: bold;\">{0}</td> "
                    , "12、项目应急演练情况：");
                sb.Append("</tr>");
                sb.Append("<tr style=\"height: 20px\">");
                sb.AppendFormat("<td align=\"center\" colspan=\"2\">{0}</td> ", "类别");
                sb.AppendFormat("<td align=\"center\" >{0}</td> ", "直接投入");
                sb.AppendFormat("<td align=\"center\" >{0}</td> ", "参演人数");
                sb.AppendFormat("<td align=\"center\" >{0}</td> ", "本月次数");
                sb.AppendFormat("<td align=\"center\" >{0}</td> ", "项目累计次数");
                sb.AppendFormat("<td align=\"center\" >{0}</td> ", "下月计划");
                sb.Append("</tr>");
                var getMonthReport12 = Funs.DB.SeDin_MonthReport12.FirstOrDefault(x => x.MonthReportId == monthReportId);
                if (getMonthReport12 != null)
                {
                    sb.Append("<tr style=\"height: 20px\">");
                    sb.AppendFormat("<td align=\"left\" style=\"width: 14%;\" rowspan=\"2\">{0}</td> ", "综合演练");
                    sb.AppendFormat("<td align=\"left\" style=\"width: 14%;\">{0}</td> ", "现场演练");
                    sb.AppendFormat("<td align=\"left\" style=\"width: 14%;\">{0}</td> ", getMonthReport12.MultipleSiteInput);
                    sb.AppendFormat("<td align=\"left\" style=\"width: 14%;\">{0}</td> ", getMonthReport12.MultipleSitePerson);
                    sb.AppendFormat("<td align=\"left\" style=\"width: 14%;\">{0}</td> ", getMonthReport12.MultipleSiteNum);
                    sb.AppendFormat("<td align=\"left\" style=\"width: 15%;\">{0}</td> ", getMonthReport12.MultipleSiteTotalNum);
                    sb.AppendFormat("<td align=\"left\" style=\"width: 15%;\">{0}</td> ", getMonthReport12.MultipleSiteNext);
                    sb.Append("</tr>");

                    sb.Append("<tr style=\"height: 20px\">");
                    sb.AppendFormat("<td align=\"left\" style=\"width: 14%;\">{0}</td> ", "桌面演练");
                    sb.AppendFormat("<td align=\"left\" style=\"width: 14%;\">{0}</td> ", getMonthReport12.MultipleDesktopInput);
                    sb.AppendFormat("<td align=\"left\" style=\"width: 14%;\">{0}</td> ", getMonthReport12.MultipleDesktopPerson);
                    sb.AppendFormat("<td align=\"left\" style=\"width: 14%;\">{0}</td> ", getMonthReport12.MultipleDesktopNum);
                    sb.AppendFormat("<td align=\"left\" style=\"width: 15%;\">{0}</td> ", getMonthReport12.MultipleDesktopTotalNum);
                    sb.AppendFormat("<td align=\"left\" style=\"width: 15%;\">{0}</td> ", getMonthReport12.MultipleDesktopNext);
                    sb.Append("</tr>");

                    sb.Append("<tr style=\"height: 20px\">");
                    sb.AppendFormat("<td align=\"left\" style=\"width: 14%;\" rowspan=\"2\">{0}</td> ", "单项演练");
                    sb.AppendFormat("<td align=\"left\" style=\"width: 14%;\">{0}</td> ", "现场演练");
                    sb.AppendFormat("<td align=\"left\" style=\"width: 14%;\">{0}</td> ", getMonthReport12.SingleSiteInput);
                    sb.AppendFormat("<td align=\"left\" style=\"width: 14%;\">{0}</td> ", getMonthReport12.SingleSitePerson);
                    sb.AppendFormat("<td align=\"left\" style=\"width: 14%;\">{0}</td> ", getMonthReport12.SingleSiteNum);
                    sb.AppendFormat("<td align=\"left\" style=\"width: 15%;\">{0}</td> ", getMonthReport12.SingleSiteTotalNum);
                    sb.AppendFormat("<td align=\"left\" style=\"width: 15%;\">{0}</td> ", getMonthReport12.SingleSiteNext);
                    sb.Append("</tr>");

                    sb.Append("<tr style=\"height: 20px\">");
                    sb.AppendFormat("<td align=\"left\" style=\"width: 14%;\">{0}</td> ", "桌面演练");
                    sb.AppendFormat("<td align=\"left\" style=\"width: 14%;\">{0}</td> ", getMonthReport12.SingleDesktopInput);
                    sb.AppendFormat("<td align=\"left\" style=\"width: 14%;\">{0}</td> ", getMonthReport12.SingleDesktopPerson);
                    sb.AppendFormat("<td align=\"left\" style=\"width: 14%;\">{0}</td> ", getMonthReport12.SingleDesktopNum);
                    sb.AppendFormat("<td align=\"left\" style=\"width: 15%;\">{0}</td> ", getMonthReport12.SingleDesktopTotalNum);
                    sb.AppendFormat("<td align=\"left\" style=\"width: 15%;\">{0}</td> ", getMonthReport12.SingleDesktopNext);
                    sb.Append("</tr>");
                    sb.Append("</table>");
                }
                #endregion

                sb.Append("<tr style=\"height: 20px\">");
                sb.AppendFormat("<td align=\"center\" style=\"width: 100%; border-right: none;border-left: none;border-bottom: none;border-top: none; \">{0}</td> ", "");
                sb.Append("</tr>");

                sb.Append("<table width=\"100% \" cellspacing=\"0\" rules=\"all\" border=\"1\" style=\"border-collapse:collapse;font-size: 10.5pt;\">");
                sb.Append("<tr style=\"height: 30px\">");
                sb.AppendFormat("<td align=\"left\" style=\"width: 100%;font-weight: bold;\">{0}</td> "
                    , "13、本月HSE活动综述：");
                sb.Append("</tr>");

                sb.Append("<tr style=\"height: 20px\">");
                sb.AppendFormat("<td align=\"left\" style=\"width: 100%;\">{0}</td> ", getMonthReport.ThisSummary ?? "");
                sb.Append("</tr>");
                sb.Append("</table>");

                sb.Append("<tr style=\"height: 60px\">");
                sb.AppendFormat("<td align=\"center\" style=\"width: 100%; border-right: none;border-left: none;border-bottom: none;border-top: none; \">{0}</td> ", "");
                sb.Append("</tr>");

                sb.Append("<table width=\"100% \" cellspacing=\"0\" rules=\"all\" border=\"1\" style=\"border-collapse:collapse;font-size: 10.5pt;\">");
                sb.Append("<tr style=\"height: 30px\">");
                sb.AppendFormat("<td align=\"left\" style=\"width: 100%;font-weight: bold;\">{0}</td> "
                    , "14、下月HSE工作计划：");
                sb.Append("</tr>");
                sb.Append("<tr style=\"height: 60px\">");
                sb.AppendFormat("<td align=\"left\" style=\"width: 100%;\">{0}</td> ", getMonthReport.NextPlan ?? "");
                sb.Append("</tr>");
                sb.Append("</table>");

                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("</table>");
            }
            return sb.ToString();
        }
        #endregion

        #region 试卷
        /// <summary>
        /// 导出方法
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public static string GetTestRecordHtml(string testRecordId)
        {
            Model.SGGLDB db = Funs.DB;
            StringBuilder sb = new StringBuilder();
            var getTestRecord = TestRecordService.GetTestRecordById(testRecordId);
            if (getTestRecord != null)
            {
                var getTestItems = from x in Funs.DB.Training_TestRecordItem
                                     where x.TestRecordId == testRecordId 
                                     select x;
                sb.Append("<meta http-equiv=\"content-type\" content=\"application/word; charset=UTF-8\"/>");
                sb.Append("<table width=\"100% \" cellspacing=\"0\" rules=\"all\" border=\"0\" style=\"border-collapse:collapse;font-size: 12pt;\">");
                sb.Append("<tr style=\"height: 35px\">");
                sb.AppendFormat("<td align=\"center\"  style=\"width: 100%; font-size: 12pt; font-weight: bold;\">{0}</td> ", "赛鼎工程有限公司" + ProjectService.GetProjectNameByProjectId(getTestRecord.ProjectId));
                sb.Append("</tr>");
                sb.Append("<tr style=\"height:35px\">");
                var getTrainTypeName = (from x in db.Training_TestPlan 
                                        join z in db.Training_Plan on x.PlanId equals z.PlanId
                                        join t in db.Base_TrainType on z.TrainTypeId equals t.TrainTypeId
                                        where x.TestPlanId == getTestRecord.TestPlanId
                                        select t.TrainTypeName).FirstOrDefault();
                sb.AppendFormat("<td align=\"center\"  style=\"width: 100%; font-size: 12pt; font-weight: bold;\">{0}</td> ", getTrainTypeName ?? "" + "培训试题");
                sb.Append("</tr>");
                sb.Append("</table>");

                sb.Append("<table width=\"100% \" cellspacing=\"0\" rules=\"all\" border=\"0\" style=\"border-collapse:collapse;font-size: 10.5pt;\">");
                sb.Append("<tr style=\"height: 35px\">");                
                string unitName = "";
                string workPostName = "";
                string testName = "";
                string idCard = "";
                var person = Funs.DB.SitePerson_Person.FirstOrDefault(e => e.PersonId == getTestRecord.TestManId);
                if (person != null)
                {
                    unitName = BLL.UnitService.GetUnitNameByUnitId(person.UnitId);
                    workPostName = WorkPostService.getWorkPostNamesWorkPostIds(person.WorkPostId);
                    testName = person.PersonName;
                    idCard = person.IdentityCard;
                }
                sb.AppendFormat("<td align=\"left\"  style=\"width:53%; \">{0}</td> ", "单位名称："+ unitName);
                sb.AppendFormat("<td align=\"left\" style=\"width:25%;\">{0}</td> ", "工种/职务："+ workPostName);                
                sb.AppendFormat("<td align=\"left\"  style=\"width:22%;\">{0}</td> ", "日期：" + string.Format("{0:yyyy-MM-dd}", getTestRecord.TestStartTime));
                
                sb.Append("</tr>");
                sb.Append("</table>");

                sb.Append("<table width=\"100% \" cellspacing=\"0\" rules=\"all\" border=\"0\" style=\"border-collapse:collapse;font-size: 10.5pt;\">");
                sb.Append("<tr style=\"height: 35px\">");
                sb.AppendFormat("<td align=\"left\"  style=\"width:25%; \">{0}</td> ", "姓名：" + testName);
                sb.AppendFormat("<td align=\"left\" style=\"width:50%;\">{0}</td> ", "身份证号：" + idCard);
                sb.AppendFormat("<td align=\"left\"  style=\"width:25%;\">{0}</td> ", "分数：" + (getTestRecord.TestScores ?? 0).ToString());
                sb.Append("</tr>");
                sb.Append("</table>");

                sb.Append("<table width=\"100% \" cellspacing=\"0\" rules=\"all\" border=\"0\" style=\"border-collapse:collapse;font-size: 10.5pt;\">");
                ///单项选择题
                sb.Append("<tr style=\"height: 30px\">");
                sb.AppendFormat("<td align=\"left\"  style=\"width:100%;  font-weight: bold;\">{0}</td> ", "一、单项选择题 (每题2分，共50分)");
                sb.Append("</tr>");
                var getSingleItem = getTestItems.Where(x=> x.TestType == "1").ToList();
                if (getSingleItem.Count > 0)
                {
                    int num = 1;
                    foreach (var item in getSingleItem)
                    {
                        sb.Append("<tr style=\"height: 30px\">");
                      string  Avstracts = item.Abstracts.Replace("　", "").Replace(" ", "").Replace("（", "(").Replace("）", ")").Replace("()", "(" + item.SelectedItem + ")");

                        sb.AppendFormat("<td align=\"left\"  style=\"width:100%;\">{0}</td> ", num + "、" + Avstracts);
                        sb.Append("</tr>");
                        sb.Append("<tr style=\"height: 30px\">");
                        string str = string.Empty;
                        if (!string.IsNullOrEmpty(item.AItem))
                        {
                            str += "A." + item.AItem;
                        }
                        if (!string.IsNullOrEmpty(item.BItem))
                        {
                            str += "&nbsp;&nbsp;B." + item.BItem;
                        }
                        if (!string.IsNullOrEmpty(item.CItem))
                        {
                            str += "&nbsp;&nbsp;C." + item.CItem;
                        }
                        if (!string.IsNullOrEmpty(item.DItem))
                        {
                            str += "&nbsp;&nbsp;D." + item.DItem;

                        }
                        sb.AppendFormat("<td  align=\"left\" style=\"width:100%; \">{0}</td> ", str);
                        sb.Append("</tr>");
                        num++;
                    }
                }

                ///多项选择题
                sb.Append("<tr style=\"height: 30px\">");
                sb.AppendFormat("<td align=\"left\"  style=\"width: 100%; font-weight: bold; \">{0}</td> ", "二、多项选择题 (每题3分，共30分)");
                sb.Append("</tr>");

                var getMultipleItem = getTestItems.Where(x => x.TestType == "2").ToList();
                if (getMultipleItem.Count > 0)
                {
                    int num = 1;
                    foreach (var item in getMultipleItem)
                    {
                       string  Avstracts = item.Abstracts.Replace("　", "").Replace(" ", "").Replace("（", "(").Replace("）", ")").Replace("()", "(" + item.SelectedItem + ")");
                        sb.AppendFormat("<td align=\"left\" style=\"width:100%; \">{0}</td> ", num + "、" + Avstracts);
                        sb.Append("</tr>");
                        sb.Append("<tr style=\"height:30px\">");
                        string str = string.Empty;
                        if (!string.IsNullOrEmpty(item.AItem))
                        {
                            str += "A." + item.AItem;
                        }
                        if (!string.IsNullOrEmpty(item.BItem))
                        {
                            str += "&nbsp;&nbsp;B." + item.BItem;
                        }
                        if (!string.IsNullOrEmpty(item.CItem))
                        {
                            str += "&nbsp;&nbsp;C." + item.CItem;
                        }
                        if (!string.IsNullOrEmpty(item.DItem))
                        {
                            str += "&nbsp;&nbsp;D." + item.DItem;
                        }

                        sb.AppendFormat("<td  align=\"left\" style=\"width: 100%; \">{0}</td> ", str);
                        sb.Append("</tr>");
                        num++;
                    }
                }

                ///判断题
                sb.Append("<tr style=\"height: 30px\">");
                sb.AppendFormat("<td align=\"left\"   style=\"width: 100%;  font-weight: bold;\">{0}</td> ", "三、判断题 (每题1分，共20分)");
                sb.Append("</tr>");
                var getIsTrueItem = getTestItems.Where(x => x.TestType == "3").ToList();
                if (getIsTrueItem.Count > 0)
                {
                    int num = 1;
                    foreach (var item in getIsTrueItem)
                    {
                        sb.Append("<tr style=\"height: 30px\">");
                        var Avstracts = item.Abstracts;
                        if (Avstracts.IndexOf("(") > -1)
                        {
                            Avstracts = Avstracts.Replace("(", "（" + item.SelectedItem == "（A" ? "（√" : "（×");
                        }
                        else
                        {
                            if (Avstracts.IndexOf("（") > -1)
                                Avstracts = Avstracts.Replace("（", "（" + item.SelectedItem == "（A" ? "（√" : "（×");
                        }
                        sb.AppendFormat("<td  align=\"left\" style=\"width: 100%; \">{0}</td> ", num + "、" + Avstracts);
                        sb.Append("</tr>");
                        num++;
                    }
                }
                sb.Append("</table>");

                sb.Append("<table width=\"100% \" cellspacing=\"0\" rules=\"all\" border=\"0\" style=\"border-collapse:collapse;font-size: 10.5pt;\">");
                var attachFile = Funs.DB.AttachFile.FirstOrDefault(x => x.ToKeyId == testRecordId);
                if (attachFile != null && !string.IsNullOrEmpty(attachFile.AttachUrl))
                {
                    List<string> listUrl = Funs.GetStrListByStr(attachFile.AttachUrl, ',');
                    int count = listUrl.Count();
                    sb.Append("<tr>");
                    if (count > 0)
                    {
                        string imgStr0 = "<img width='100' height='100' src='" + (Funs.SGGLUrl + listUrl[0]).Replace('\\', '/') + "'></img>&nbsp;&nbsp;";
                        string imgStr1 = "<img width='100' height='100' src='" + (Funs.SGGLUrl + listUrl[0]).Replace('\\', '/') + "'></img>&nbsp;&nbsp;";
                        if (count >= 2)
                        {
                            int cout2 = count / 2;
                            imgStr1 = "<img width='100' height='100' src='" + (Funs.SGGLUrl + listUrl[cout2]).Replace('\\', '/') + "'></img>&nbsp;&nbsp;";
                        }
                        string imgStr2 = "<img width='100' height='100' src='" + (Funs.SGGLUrl + listUrl[count - 1]).Replace('\\', '/') + "'></img>&nbsp;&nbsp;";
                        sb.AppendFormat("<td align=\"center\"   style=\"width: 30%; \">{0}</td> ", imgStr0);
                        sb.AppendFormat("<td align=\"center\"   style=\"width: 30%; \">{0}</td> ", imgStr1);
                        sb.AppendFormat("<td align=\"center\"   style=\"width: 30%; \">{0}</td> ", imgStr2);
                    }
                    else
                    {
                        sb.AppendFormat("<td align=\"center\"   style=\"width: 30%; \">{0}</td> ", "");
                    }                   
                    sb.Append("</tr>");
                }

                sb.Append("</table>");
                ///图片

            }
            return sb.ToString();
        }
        #endregion

        #region 人员上岗证
        /// <summary>
        /// 导出方法
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public static string GetSendCardHtml(string personIds)
        {
            Model.SGGLDB db = Funs.DB;
            StringBuilder sb = new StringBuilder();
            sb.Append("<meta http-equiv=\"content-type\" content=\"application/word; charset=UTF-8\"/>");
            sb.Append("<table width=\"100% \" cellspacing=\"0\" rules=\"all\" border=\"0\" style=\"border-collapse:collapse;\">");
            List<string> pList = Funs.GetStrListByStr(personIds, ',');
            if (pList.Count() > 0)
            {
                string imgStrUrl = "<img width='60' height='50' src='" + (Funs.SGGLUrl + "Images/SUBimages/CNCEC.png").Replace('\\', '/') + "'></img>";
                for (int pageIndex = 1; pageIndex * 2 <= pList.Count() + 1; pageIndex++)
                {
                    string projectName = "";
                    string unitName1 = "";
                    string unitName2 = "";
                    string workName1 = "";
                    string workName2 = "";
                    string personName1 = "";
                    string personName2 = "";
                    string cardNo1 = "";
                    string cardNo2 = "";
                    string photoUrl1 = "";
                    string photoUrl2 = "";
                    string QRUrl1 = "";
                    string QRUrl2 = "";
                    var getDataList = pList.Skip(2 * (pageIndex - 1)).Take(2).ToList();
                    int i = 0;
                    foreach (var item in getDataList)
                    {
                        var getPerson = PersonService.GetPersonById(item);
                        if (getPerson != null)
                        {
                            string qrurl = string.Empty;
                            if (!string.IsNullOrEmpty(getPerson.QRCodeAttachUrl) && CreateQRCodeService.isHaveImage(getPerson.QRCodeAttachUrl))
                            {
                                qrurl= getPerson.QRCodeAttachUrl;
                            }
                            else
                            {
                                qrurl = CreateQRCodeService.CreateCode_Simple(getPerson.IdentityCard);
                                getPerson.QRCodeAttachUrl = qrurl;
                                db.SubmitChanges();
                            }

                            projectName = ProjectService.GetShortNameByProjectId(getPerson.ProjectId);
                            if (i == 0)
                            {
                                unitName1 = UnitService.GetUnitNameByUnitId(getPerson.UnitId);
                                workName1 = WorkPostService.getWorkPostNameById(getPerson.WorkPostId);
                                personName1 = getPerson.PersonName;
                                cardNo1 = getPerson.CardNo;
                                photoUrl1 = getPerson.PhotoUrl; 
                                QRUrl1 = qrurl;
                            }
                            else
                            {
                                unitName2 = UnitService.GetUnitNameByUnitId(getPerson.UnitId);
                                workName2 = WorkPostService.getWorkPostNameById(getPerson.WorkPostId);
                                personName2 = getPerson.PersonName;
                                cardNo2 = getPerson.CardNo;
                                photoUrl2 = getPerson.PhotoUrl;
                                QRUrl2 = qrurl;
                            }
                            i++;
                        }
                    }
          
                    sb.Append("<tr >");
                    sb.Append("<td align=\"left\" style=\"width: 49%;\" >");
                    sb.Append("<table width=\"100% \" cellspacing=\"0\" rules=\"all\" border=\"0\" style=\"border-collapse:collapse;\">");
                    sb.Append("<tr style=\"height: 40px\">");
                    sb.AppendFormat("<td align=\"center\"  style=\"width: 25%;\" rowspan=\"2\">{0}</td> ", imgStrUrl);
                    sb.AppendFormat("<td align=\"center\"  style=\"width: 50%;font-size: 11pt;font-weight: bold;\">{0}</td> ", "赛鼎工程有限公司");
                    string imgStrQRUrl1 = "<img width='60' height='50' src='" + (Funs.SGGLUrl + QRUrl1).Replace('\\', '/') + "'></img>";
                    sb.AppendFormat("<td align=\"center\"  style=\"width: 25%;\" rowspan=\"2\">{0}</td> ", imgStrQRUrl1);
                    sb.Append("</tr>");
                    sb.Append("<tr style=\"height: 30px\"  valign=\"top\">");
                    sb.AppendFormat("<td align=\"center\"  style=\" font-size: 9pt; font-weight: bold;\">{0}</td> ", projectName);
                    sb.Append("</tr>");
                    sb.Append("</table>");

                    sb.Append("<table width=\"100% \" cellspacing=\"0\" rules=\"all\" border=\"0\" style=\"border-collapse:collapse;font-size: 9pt;background-color:#5b9bd5\">");
                    sb.Append("<tr style=\"height: 25PX\">");
                    sb.AppendFormat("<td align=\"right\"  style=\"width: 20%;\" >{0}</td> ", "单位：");
                    sb.AppendFormat("<td align=\"left\"  style=\"width: 55%;\" >{0}</td> ",  unitName1);
                    string imgStrphotoUrl1 = "<img width='85' height='110' src='" + (Funs.SGGLUrl + photoUrl1).Replace('\\', '/') + "'></img>";
                        sb.AppendFormat("<td align=\"center\"  style=\"width: 25%;\" rowspan=\"5\">{0}</td> ", imgStrphotoUrl1);
                    sb.Append("</tr>");
                    sb.Append("<tr style=\"height: 25PX\">");
                    sb.AppendFormat("<td align=\"right\" >{0}</td> ", "岗位：" );
                    sb.AppendFormat("<td align=\"left\" >{0}</td> ",  workName1);
                    sb.Append("</tr>");
                    sb.Append("<tr style=\"height: 25PX\">");
                    sb.AppendFormat("<td align=\"right\" >{0}</td> ", "姓名：");
                    sb.AppendFormat("<td align=\"left\" >{0}</td> ", personName1);
                    sb.Append("</tr>");
                    sb.Append("<tr style=\"height: 25PX\">");
                    sb.AppendFormat("<td align=\"right\" >{0}</td> ", "编号：");
                    sb.AppendFormat("<td align=\"left\" >{0}</td> ",  cardNo1);
                    sb.Append("</tr>");
                    sb.Append("<tr style=\"height: 25PX\">");
                    sb.AppendFormat("<td align=\"right\" >{0}</td> ", "");
                    sb.AppendFormat("<td align=\"left\" >{0}</td> ", "");
                    sb.Append("</tr>");
                    sb.Append("</table>");
                    sb.Append("</td >");

                    sb.AppendFormat("<td align=\"center\"  style=\"width: 2%;\">{0}</td> ", "");

                    sb.Append("<td align=\"right\" style=\"width: 49%;\">");
                    if (!string.IsNullOrEmpty(personName2))
                    {
                        sb.Append("<table width=\"100% \" cellspacing=\"0\" rules=\"all\" border=\"0\" style=\"border-collapse:collapse;\">");
                        sb.Append("<tr style=\"height: 40px\">");
                        sb.AppendFormat("<td align=\"center\"  style=\"width: 25%;\" rowspan=\"2\">{0}</td> ", imgStrUrl);
                        sb.AppendFormat("<td align=\"center\"  style=\"width: 50%;font-size: 11pt;font-weight: bold;\">{0}</td> ", "赛鼎工程有限公司");
                        string imgStrQRUrl2 = "<img width='60' height='50' src='" + (Funs.SGGLUrl + QRUrl2).Replace('\\', '/') + "'></img>";
                        sb.AppendFormat("<td align=\"center\"  style=\"width: 25%;\" rowspan=\"2\">{0}</td> ", imgStrQRUrl2);
                        sb.Append("</tr>");
                        sb.Append("<tr style=\"height: 30px\">");
                        sb.AppendFormat("<td align=\"center\" valign=\"top\" style=\" font-size: 9pt; font-weight: bold;\">{0}</td> ", projectName);
                        sb.Append("</tr>");
                        sb.Append("</table>");

                        sb.Append("<table width=\"100% \" cellspacing=\"0\" rules=\"all\" border=\"0\" style=\"border-collapse:collapse;font-size: 9pt;background-color:#5b9bd5;\">");
                        sb.Append("<tr style=\"height: 25PX\">");
                        sb.AppendFormat("<td align=\"right\"  style=\"width: 20%;\" >{0}</td> ", "单位：");
                        sb.AppendFormat("<td align=\"left\"  style=\"width: 55%;\" >{0}</td> ", unitName2);
                        string imgStrphotoUrl2 = "<img width='85' height='110' src='" + (Funs.SGGLUrl + photoUrl2).Replace('\\', '/') + "'></img>";
                        sb.AppendFormat("<td align=\"center\"  style=\"width: 25%;\" rowspan=\"5\">{0}</td> ", imgStrphotoUrl2);
                        sb.Append("</tr>");

                        sb.Append("<tr style=\"height: 25PX\">");
                        sb.AppendFormat("<td align=\"right\" >{0}</td> ", "岗位：");
                        sb.AppendFormat("<td align=\"left\" >{0}</td> ", workName2);
                        sb.Append("</tr>");
                        sb.Append("<tr style=\"height: 25PX\">");
                        sb.AppendFormat("<td align=\"right\" >{0}</td> ", "姓名：");
                        sb.AppendFormat("<td align=\"left\" >{0}</td> ", personName2);
                        sb.Append("</tr>");
                        sb.Append("<tr style=\"height: 25PX\">");
                        sb.AppendFormat("<td align=\"right\" >{0}</td> ", "编号：");
                        sb.AppendFormat("<td align=\"left\" >{0}</td> ", cardNo2);
                        sb.Append("</tr>");
                        sb.Append("<tr style=\"height: 25PX\">");
                        sb.AppendFormat("<td align=\"right\" >{0}</td> ", "");
                        sb.AppendFormat("<td align=\"left\" >{0}</td> ", "");
                        sb.Append("</tr>");

                        sb.Append("</table>");
                    }
                    sb.Append("</td >");
                    sb.Append("</tr>");

                    sb.Append("<tr  style=\"height:15px\">");
                    sb.AppendFormat("<td align=\"right\"  style=\"width: 100%\" colspan=\"3\">{0}</td> ", "");
                    sb.Append("<tr >");
                }
            }
            sb.Append("</table>");
            return sb.ToString();
        }
        #endregion
    }
}