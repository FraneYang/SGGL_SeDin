using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;

namespace FineUIPro.Web.WeldingProcess.WeldingManage
{
    /// <summary>
    /// GetWdldingDailyItem 的摘要说明
    /// </summary>
    public class GetWdldingDailyItem : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string rowId = context.Request.QueryString["WeldingDailyId"];
            JArray ja = new JArray();
            var viewWeldlines = (from x in BLL.Funs.DB.View_HJGL_WeldJoint where x.WeldingDailyId == rowId orderby x.PipelineCode, x.WeldJointCode select x).ToList();
            for (int i = 0; i < viewWeldlines.Count; i++)
            {
                JArray jaItem = new JArray
                {
                    i + 1,
                    viewWeldlines[i].PipelineCode,
                    viewWeldlines[i].WeldJointCode,
                    viewWeldlines[i].CoverWelderCode,
                    viewWeldlines[i].BackingWelderCode,
                    viewWeldlines[i].WeldTypeCode,
                    viewWeldlines[i].WeldingLocationCode,
                    viewWeldlines[i].JointAttribute,
                    viewWeldlines[i].Size,
                    viewWeldlines[i].Dia,
                    viewWeldlines[i].Thickness,
                    viewWeldlines[i].WeldingMethodCode
                };
                ja.Add(jaItem);
            }

            context.Response.ContentType = "text/plain";
            context.Response.Write(ja.ToString(Newtonsoft.Json.Formatting.None));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}