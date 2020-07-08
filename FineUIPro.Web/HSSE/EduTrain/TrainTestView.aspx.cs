using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FineUIPro.Web.HSSE.EduTrain
{
    public partial class TrainTestView : PageBase
    {
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               // ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                // 绑定表格
                BindGrid();
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string trainDetailId = Request.Params["TrainDetailId"];
            var trainRecordDetail = BLL.EduTrain_TrainRecordDetailService.GetTrainDetailByTrainDetailId(trainDetailId);
            if (trainRecordDetail != null)
            {               
                var person = BLL.PersonService.GetPersonById(trainRecordDetail.PersonId);
                var trainRecord = BLL.EduTrain_TrainRecordService.GetTrainingByTrainingId(trainRecordDetail.TrainingId);   
                if (person != null && trainRecord != null)
                {
                    List<Model.View_EduTrain_TrainTest> viewList = new List<Model.View_EduTrain_TrainTest>();
                    var trainTest = from x in new Model.SGGLDB(Funs.ConnString).View_EduTrain_TrainTest
                                    where x.TrainingId == trainRecord.TrainingId
                                    orderby x.COrder
                                    select x;
                    var trainPersonRecord = new Model.SGGLDB(Funs.ConnString).EduTrain_TrainPersonRecord.FirstOrDefault(x => x.RecordId == trainRecord.FromRecordId && x.IdentifyId == person.IdentityCard);
                    if (trainPersonRecord != null && !string.IsNullOrEmpty(trainPersonRecord.Answers))
                    {
                        List<string> listAnswers = Funs.GetStrListByStr(trainPersonRecord.Answers, '|');
                        foreach(var item in trainTest)
                        {
                            if (listAnswers.Count() > item.COrder)
                            {
                                item.Description = listAnswers[item.COrder.Value];
                            }

                            viewList.Add(item);
                        }
                    }

                    Grid1.DataSource = viewList;
                    Grid1.DataBind();
                }
            }

            for (int i = 0; i < Grid1.Rows.Count; i++)
            {
                if (Grid1.Rows[i].Values[2].ToString() != Grid1.Rows[i].Values[3].ToString())
                {
                    Grid1.Rows[i].RowCssClass = "Red";
                }
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            this.BindGrid();
        }


        #region 导出按钮
        /// 导出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOut_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            string filename = Funs.GetNewFileName();
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("考试试题" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.Grid1.PageSize = this.Grid1.Rows.Count();
            BindGrid();
            Response.Write(GetGridTableHtml(Grid1));
            Response.End();
        }
        #endregion
    }
}