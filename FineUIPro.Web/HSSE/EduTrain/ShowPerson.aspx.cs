using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FineUIPro.Web.HSSE.EduTrain
{
    public partial class ShowPerson : PageBase
    {
        #region  定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string TrainingId
        {
            get
            {
                return (string)ViewState["TrainingId"];
            }
            set
            {
                ViewState["TrainingId"] = value;
            }
        }

        ///// <summary>
        ///// GV被选择项列表
        ///// </summary>
        //public List<string> ItemSelectedList
        //{
        //    get
        //    {
        //        return (List<string>)ViewState["ItemSelectedList"];
        //    }
        //    set
        //    {
        //        ViewState["ItemSelectedList"] = value;
        //    }
        //}

        /// <summary>
        /// 项目id
        /// </summary>
        public string ProjectId
        {
            get
            {
                return (string)ViewState["ProjectId"];
            }
            set
            {
                ViewState["ProjectId"] = value;
            }
        }
        #endregion

        #region  页面加载
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ProjectId = this.CurrUser.LoginProjectId;
                if (!string.IsNullOrEmpty(Request.Params["projectId"]) && Request.Params["projectId"] != this.ProjectId)
                {
                    this.ProjectId = Request.Params["projectId"];
                } 
                //单位               
                BLL.UnitService.InitUnitDropDownList(this.drpUnit, this.ProjectId, true);
                this.TrainingId = Request.Params["TrainingId"];              
                // 绑定表格
                BindGrid();
            }
        }
        #endregion

        #region  保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string personId = Grid1.DataKeys[rowIndex][0].ToString();
                    var trainRecordDetail = BLL.EduTrain_TrainRecordDetailService.GetTrainDetailByPersonIdTrainingId(this.TrainingId, personId);
                    if (trainRecordDetail == null && !string.IsNullOrEmpty(personId))
                    {
                        Model.EduTrain_TrainRecordDetail detail = new Model.EduTrain_TrainRecordDetail
                        {
                            TrainingId = this.TrainingId,
                            PersonId = personId,
                            CheckResult = true,
                            CheckScore = 100
                        };

                        detail.CheckScore = null;
                        BLL.EduTrain_TrainRecordDetailService.AddTrainDetail(detail);
                    }
                }
                ///更新培训人数
                var train = BLL.EduTrain_TrainRecordService.GetTrainingByTrainingId(this.TrainingId);
                if (train != null)
                {
                    var item = BLL.EduTrain_TrainRecordDetailService.GetTrainRecordDetailByTrainingId(this.TrainingId);
                    train.TrainPersonNum = item.Count();
                    BLL.EduTrain_TrainRecordService.UpdateTraining(train);
                }
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
            else
            {
                Alert.ShowInParent("请至少选择一条记录！");
                return;
            }

        }
        #endregion

        #region  绑定数据
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {           
            string type = Request.Params["TrainTypeId"];
            var trainType = BLL.TrainTypeService.GetTrainTypeById(type);
            if (trainType != null)
            {
                string strSql = @"SELECT viewPerSon.PersonId,viewPerSon.UnitName,viewPerSon.PersonName,viewPerSon.CardNo,viewPerSon.SexName,WorkPostName,viewPerSon.IdentityCard,TeamGroupName,WorkAreaName,InTime "
                    + @" FROM View_SitePerson_Person AS viewPerSon   "
                    //+ @" LEFT JOIN EduTrain_TrainRecordDetail AS TrainRecordDetail ON viewPerSon.PersonId=TrainRecordDetail.PersonId "
                    //+ @" LEFT JOIN EduTrain_TrainRecord AS TrainRecord ON TrainRecord.TrainingId=TrainRecordDetail.TrainingId "
                    + @" WHERE viewPerSon.ProjectId='" + this.ProjectId + "' AND viewPerSon.IsUsed='1' ";
                   
                List<SqlParameter> listStr = new List<SqlParameter>();
                if (BLL.EduTrain_TrainRecordDetailService.GetTrainRecordDetailByTrainingId(this.TrainingId).Count() > 0)
                {
                    strSql += @" AND (viewPerSon.PersonId NOT IN (SELECT PersonId FROM EduTrain_TrainRecord AS R 
                                LEFT JOIN EduTrain_TrainRecordDetail AS D ON R.TrainingId = D.TrainingId 
                                WHERE R.ProjectId='" + this.ProjectId + "' AND R.TrainingId = '" + this.TrainingId + "'))";
                }

                if (!trainType.IsRepeat.HasValue || trainType.IsRepeat == false)
                {
                    strSql += @" AND viewPerSon.PersonId NOT IN 
                                    (SELECT PersonId FROM EduTrain_TrainRecord AS R 
                                     LEFT JOIN EduTrain_TrainRecordDetail AS D ON R.TrainingId = D.TrainingId 
                                     WHERE R.ProjectId='" + this.ProjectId + "' AND R.TrainTypeId ='" + type + "' AND D.CheckResult = 1)";
                }
                
                if (this.drpUnit.SelectedValue != BLL.Const._Null)
                {
                    strSql += " AND viewPerSon.UnitId=@UnitId";
                    listStr.Add(new SqlParameter("@UnitId", this.drpUnit.SelectedValue));
                }
                if (!string.IsNullOrEmpty(this.txtPersonName.Text.Trim()))
                {
                    strSql += " AND viewPerSon.PersonName LIKE @PersonName";
                    listStr.Add(new SqlParameter("@PersonName", "%" + this.txtPersonName.Text.Trim() + "%"));
                }

                if (!string.IsNullOrEmpty(this.txtCardNo.Text.Trim()))
                {
                    strSql += " AND viewPerSon.CardNo LIKE @CardNo";
                    listStr.Add(new SqlParameter("@CardNo", "%" + this.txtCardNo.Text.Trim() + "%"));
                }
                if (!string.IsNullOrEmpty(this.txtWorkPostName.Text.Trim()))
                {
                    strSql += " AND viewPerSon.WorkPostName LIKE @WorkPostName";
                    listStr.Add(new SqlParameter("@WorkPostName", "%" + this.txtWorkPostName.Text.Trim() + "%"));
                }
                if (ckPostType2.Checked)
                {
                    strSql += " AND viewPerSon.PostType = @PostType";
                    listStr.Add(new SqlParameter("@PostType", BLL.Const.PostType_2));
                }
                if (ckIsHsse.Checked)
                {
                    strSql += " AND IsHsse = @IsHsse";
                    listStr.Add(new SqlParameter("@IsHsse", true));
                }
                strSql += " group by viewPerSon.PersonId,viewPerSon.UnitName,viewPerSon.PersonName,viewPerSon.CardNo,viewPerSon.Sex,WorkPostName,viewPerSon.IdentityCard,TeamGroupName,WorkAreaName,InTime,SexName";
                SqlParameter[] parameter = listStr.ToArray();
                DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

                Grid1.RecordCount = tb.Rows.Count;
                var table1 = this.GetPagedDataTable(Grid1, tb);
                Grid1.DataSource = table1;
                Grid1.DataBind();
            }
        }                 
        #endregion

        #region 排序
        /// <summary>
        /// Grid1排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {           
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
            BindGrid();
        }
        #endregion

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {            
            this.BindGrid();
        }   
    }
}