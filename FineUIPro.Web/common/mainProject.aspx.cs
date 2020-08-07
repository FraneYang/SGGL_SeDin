using BLL;
using System;
using System.Linq;

namespace FineUIPro.Web.common
{
    public partial class mainProject : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ///项目概况
                var project = ProjectService.GetProjectByProjectId(this.CurrUser.LoginProjectId);
                if (project != null)
                {
                    this.divProjectName.InnerHtml = project.ProjectName;
                    this.divProjectAddress.InnerHtml = project.ProjectAddress;
                }
                /// 当前人工时
                this.allPersonWorkTime.InnerHtml = (Funs.DB.SitePerson_PersonInOutNumber.Where(x => x.ProjectId == this.CurrUser.LoginProjectId).Max(x => x.WorkHours) ?? 0).ToString();
                ///劳务统计
                getSitePerson();
            }
        }

        /// <summary>
        ///  劳务统计
        /// </summary>
        private void getSitePerson()
        {
            int AllCount = 0;
            int MCount = 0;
            var getAllPerson = from x in Funs.DB.SitePerson_Person
                               join y in Funs.DB.Base_WorkPost on x.WorkPostId equals y.WorkPostId
                               where x.ProjectId == this.CurrUser.LoginProjectId && x.IsUsed == true && y.PostType == Const.PostType_1
                               && x.InTime < DateTime.Now && (!x.OutTime.HasValue || x.OutTime > DateTime.Now)
                               select x;
            var getDayAll = from x in Funs.DB.SitePerson_PersonInOut
                                               where x.ProjectId == this.CurrUser.LoginProjectId && x.ChangeTime.Value.Year == DateTime.Now.Year && x.ChangeTime.Value.Month == DateTime.Now.Month
                                               && x.ChangeTime.Value.Day == DateTime.Now.Day
                                               select x;
            if (getDayAll.Count() > 0)
            {
                var getInMaxs = from x in getDayAll
                                group x by x.PersonId into g
                                select new { g.First().PersonId, ChangeTime = g.Max(x => x.ChangeTime), g.First().IsIn };
                if (getInMaxs.Count() > 0)
                {
                    AllCount = getInMaxs.Where(x => x.IsIn == true).Count();
                }

                var getMInMaxs = from x in getDayAll
                                join y in getAllPerson on x.PersonId equals y.PersonId
                                group x by x.PersonId into g
                                select new { g.First().PersonId, ChangeTime = g.Max(x => x.ChangeTime), g.First().IsIn };
                if (getInMaxs.Count() > 0)
                {
                    MCount = getMInMaxs.Where(x => x.IsIn == true).Count();
                }
            };
            ////总人数
            this.person00.InnerHtml = ((AllCount % 1000) / 100).ToString();
            this.person01.InnerHtml =((AllCount % 100) / 10).ToString();
            this.person02.InnerHtml = (AllCount % 10).ToString();
            var getManagers = from x in getAllPerson
                              join y in Funs.DB.Base_WorkPost on x.WorkPostId equals y.WorkPostId
                              where y.PostType == Const.PostType_1
                              select x;

            /////管理人数
            this.person20.InnerHtml = ((MCount % 1000) / 100).ToString();
            this.person21.InnerHtml = ((MCount % 100) / 10).ToString();
            this.person22.InnerHtml = (MCount % 10).ToString();

            /////作业人数
            int WCount = AllCount-MCount;
            this.person10.InnerHtml = ((WCount % 1000) / 100).ToString();
            this.person11.InnerHtml = ((WCount % 100) / 10).ToString();
            this.person12.InnerHtml = (WCount % 10).ToString();
        }
    }
}
