namespace FineUIPro.Web.BaseInfo
{
    using BLL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;

    public partial class TrainTypeEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 培训类型id
        /// </summary>
        public string TrainTypeId
        {
            get
            {
                return (string)ViewState["TrainTypeId"];
            }
            set
            {
                ViewState["TrainTypeId"] = value;
            }
        }
        #endregion

        /// <summary>
        /// LEC评价编辑页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.TrainTypeId = Request.Params["TrainTypeId"];
                this.InitTreeMenu();
                this.BindGrid();
            }
        }

        #region 绑定试题类型树
        /// <summary>
        /// 初始化树
        /// </summary>
        private void InitTreeMenu()
        {
            this.tvTestTraining.Nodes.Clear();
            var getTestTrainings = from x in Funs.DB.Training_TestTraining select x;
            if (getTestTrainings.Count() > 0)
            {
                var rootT = getTestTrainings.Where(x => x.SupTrainingId == "0").OrderBy(x => x.TrainingCode);
                foreach (var item in rootT)
                {
                    TreeNode rootNode = new TreeNode
                    {
                        Text = item.TrainingName,
                        NodeID = item.TrainingId,
                        Expanded = true
                    };

                    this.tvTestTraining.Nodes.Add(rootNode);
                    BoundTree(rootNode.Nodes, rootNode.NodeID, getTestTrainings.ToList());
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="parentId"></param>
        private void BoundTree(TreeNodeCollection nodes, string parentId, List<Model.Training_TestTraining> getTestTrainings)
        {
            var dt = getTestTrainings.Where(x => x.SupTrainingId == parentId).OrderBy(x => x.TrainingCode);
            if (dt.Count() > 0)
            {
                TreeNode tn = null;
                foreach (var dr in dt)
                {
                    string name = dr.TrainingName;
                    if (!string.IsNullOrEmpty(dr.TrainingCode))
                    {
                        name = "[" + dr.TrainingCode + "]" + dr.TrainingName;
                    }
                    tn = new TreeNode
                    {

                        Text = name,
                        NodeID = dr.TrainingId,
                        EnableClickEvent = true,
                        ToolTip = dr.TrainingName
                    };
                    nodes.Add(tn);
                    ///是否存在下级节点
                    var sup = Funs.DB.Training_TestTraining.FirstOrDefault(x => x.SupTrainingId == tn.NodeID);
                    if (sup != null)
                    {
                        BoundTree(tn.Nodes, tn.NodeID, getTestTrainings.ToList());
                    }
                }
            }
        }
        #endregion

        #region 绑定数据
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            if (!string.IsNullOrEmpty(this.TrainTypeId))
            {
                string strSql = @"SELECT item.TrainTypeItemId,item.TrainTypeId,item.TrainingId,testT.TrainingCode,testT.TrainingName,item.SCount,item.MCount,item.JCount"
                        + @" FROM Base_TrainTypeItem AS item "
                        + @" LEFT JOIN Training_TestTraining AS testT ON item.TrainingId=testT.TrainingId"
                        + @" WHERE item.TrainTypeId=@TrainTypeId ";
                List<SqlParameter> listStr = new List<SqlParameter>
                {
                    new SqlParameter("@TrainTypeId", this.TrainTypeId)
                };
                SqlParameter[] parameter = listStr.ToArray();
                DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
                Grid1.DataSource = tb;
                Grid1.DataBind();
            }
            else
            {
                Grid1.DataSource = null;
                Grid1.DataBind();
            }
        }
        #endregion

        #region 新增明细
        /// <summary>
        /// 新增LEC评价
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            this.reSetTable();
        }

        /// <summary>
        /// 重置
        /// </summary>
        private void reSetTable()
        {
            this.drpTestTraining.Value = string.Empty;
            this.txtSCount.Text = string.Empty;
            this.txtMCount.Text = string.Empty;
            this.txtJCount.Text = string.Empty;
        }
        #endregion

        #region 删除明细
        /// <summary>
        /// 删除LEC评价
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDelete_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {              
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    BLL.TrainTypeService.DeleteTrainTypeItemById(rowID);
                }
            }
            this.BindGrid();
            
            this.reSetTable();
            this.ShowNotify("删除数据成功!", MessageBoxIcon.Success);
        }
        #endregion

        #region 数据编辑
        /// <summary>
        /// Grid行双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            this.EditData();
        }

        /// <summary>
        /// 右键编辑事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuEdit_Click(object sender, EventArgs e)
        {
            this.EditData();
        }

        /// <summary>
        /// 编辑数据方法
        /// </summary>
        private void EditData()
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInParent("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string Id = Grid1.SelectedRowID;
            var trainTypeItem = TrainTypeService.GetTrainTypeItemById(Id);
            if (trainTypeItem != null)
            {               
                this.drpTestTraining.Value = trainTypeItem.TrainingId;
                this.txtSCount.Text = trainTypeItem.SCount.ToString();
                this.txtMCount.Text = trainTypeItem.MCount.ToString();
                this.txtJCount.Text = trainTypeItem.JCount.ToString();
            }
        }
        #endregion
        
        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, FineUIPro.GridSortEventArgs e)
        {
            BindGrid();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.drpTestTraining.Value))
            {
                Model.Base_TrainTypeItem newItem = new Model.Base_TrainTypeItem()
                {                   
                    TrainTypeId=this.TrainTypeId,
                    TrainingId=this.drpTestTraining.Value,
                    SCount=Funs.GetNewIntOrZero(this.txtSCount.Text),
                    MCount = Funs.GetNewIntOrZero(this.txtMCount.Text),
                    JCount = Funs.GetNewIntOrZero(this.txtJCount.Text),
                };

                var deleteItem = Funs.DB.Base_TrainTypeItem.FirstOrDefault(x => x.TrainTypeId == newItem.TrainTypeId && x.TrainingId == newItem.TrainingId);
                if (deleteItem != null)
                {
                    TrainTypeService.DeleteTrainTypeItemById(deleteItem.TrainTypeItemId);
                }

                TrainTypeService.AddTrainTypeItem(newItem);                
                this.BindGrid();
                this.reSetTable();

                this.ShowNotify("保存成功!", MessageBoxIcon.Success);
            }
        }
    }
}