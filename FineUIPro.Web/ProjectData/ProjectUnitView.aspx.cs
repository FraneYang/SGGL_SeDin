using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Model;

namespace FineUIPro.Web.ProjectData
{
    public partial class ProjectUnitView : PageBase
    {
        /// <summary>
        /// 定义项
        /// </summary>
        public string ProjectUnitId
        {
            get
            {
                return (string)ViewState["ProjectUnitId"];
            }
            set
            {
                ViewState["ProjectUnitId"] = value;
            }
        }

        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();                
                this.ProjectUnitId = Request.QueryString["ProjectUnitId"];
                if (!String.IsNullOrEmpty(this.ProjectUnitId))
                {
                    var projectUnit = BLL.ProjectUnitService.GetProjectUnitById(this.ProjectUnitId);
                    if (projectUnit != null)
                    {
                        var project = BLL.ProjectService.GetProjectByProjectId(projectUnit.ProjectId);
                        if (project != null)
                        {
                            this.lbProjectName.Text = project.ProjectName;
                        }
                        var unit = BLL.UnitService.GetUnitByUnitId(projectUnit.UnitId);
                        if (unit != null)
                        {
                            this.lbUnitName.Text = unit.UnitName;
                        }
                        this.txtInTime.Text = string.Format("{0:yyyy-MM-dd}", projectUnit.InTime);
                        this.txtOutTime.Text = string.Format("{0:yyyy-MM-dd}", projectUnit.OutTime);
                        var unitType = BLL.ConstValue.drpConstItemList(ConstValue.Group_ProjectUnitType).FirstOrDefault(x => x.ConstValue == projectUnit.UnitType);
                        if (unitType != null)
                        {
                            this.drpUnitType.Text = unitType.ConstText;
                        }                      
                    }
                }
            }
        }                    
    }
}