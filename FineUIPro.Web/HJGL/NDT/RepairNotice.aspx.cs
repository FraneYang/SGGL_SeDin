using System;
using System.Collections.Generic;
using System.Linq;
using BLL;

namespace FineUIPro.Web.HJGL.NDT
{
    public partial class RepairNotice : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string ndeItemId = Request.Params["NDEItemID"];
            var q = BLL.Batch_NDEItemService.GetNDEItemViewById(ndeItemId);
            txtPipeCode.Text = q.PipelineCode;
            txtWeldJointCode.Text = q.WeldJointCode;
            txtWelder.Text = q.WelderCode;
            txtJudgeGrade.Text = q.JudgeGrade;
            txtRepairLocation.Text = q.RepairLocation;
            txtCheckDefects.Text = BLL.Base_DefectService.GetDefectNameStrByDefectIdStr(q.CheckDefects);

            var repair = BLL.RepairRecordService.GetRepairRecordByNdeItemId(ndeItemId);
            if (repair == null)
            {
                var mark = from x in new Model.SGGLDB(Funs.ConnString).HJGL_RepairRecord
                           where x.WeldJointId == q.WeldJointId && x.DetectionTypeId == q.DetectionTypeId
                           orderby x.RepairMark descending
                           select x;
                if (mark.Count() == 0)
                {
                    txtRepairMark.Text = "R1";
                }
                else
                {
                    string m = mark.First().RepairMark;
                    string first = m.Substring(0, 1);
                    string last = m.Substring(1, 1);
                    int n = Convert.ToInt32(last) + 1;
                    txtRepairMark.Text = first + n.ToString();
                }
            }
            else
            {
                txtRepairMark.Text = "R1";
                if (!string.IsNullOrEmpty(repair.PhotoUrl))
                {
                    imgPhoto.ImageUrl = repair.PhotoUrl;
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string ndeItemId = Request.Params["NDEItemID"];
            var q = BLL.Batch_NDEItemService.GetNDEItemViewById(ndeItemId);
            var repair = BLL.RepairRecordService.GetRepairRecordByNdeItemId(ndeItemId);
            Model.HJGL_RepairRecord newItem = new Model.HJGL_RepairRecord();

            if (repair == null)
            {
                newItem.RepairRecordId = SQLHelper.GetNewID(typeof(Model.HJGL_RepairRecord));

                string code = q.TrustBatchCode;
                if (code.Substring(code.Length - 2, 1) == "R")
                {
                    string first = code.Substring(0, code.Length - 1);
                    string last = code.Substring(code.Length - 1);
                    int n = Convert.ToInt32(last) + 1;
                    newItem.RepairRecordCode = first + n.ToString();
                }
                else
                {
                    newItem.RepairRecordCode = q.TrustBatchCode + "R1";
                }
                newItem.ProjectId = q.ProjectId;
                newItem.UnitId = q.UnitId;
                newItem.UnitWorkId = q.UnitWorkId;
                newItem.NoticeDate = DateTime.Now;
                newItem.NDEItemID = ndeItemId;
                newItem.WeldJointId = q.WeldJointId;
                newItem.DetectionTypeId = q.DetectionTypeId;
                newItem.WelderId = q.BackingWelderId;
                newItem.RepairLocation = q.RepairLocation;
                newItem.CheckDefects = txtCheckDefects.Text;
                newItem.RepairMark = txtRepairMark.Text;
                newItem.PhotoUrl = imgPhoto.ImageUrl;
                BLL.RepairRecordService.AddRepairRecord(newItem);
            }
            else
            {
                repair.CheckDefects = txtCheckDefects.Text;
                repair.RepairMark = txtRepairMark.Text;
                repair.PhotoUrl = imgPhoto.ImageUrl;
                BLL.RepairRecordService.UpdateRepairRecord(repair);
            }

            ShowNotify("生成返修通知单！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        #region  上传电子签名图片
        /// <summary>
        /// 上传照片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void filePhoto_FileSelected(object sender, EventArgs e)
        {
            if (filePhoto.HasFile)
            {
                string fileName = filePhoto.ShortFileName;
                if (!ValidateFileType(fileName))
                {
                    ShowNotify("无效的文件类型！");
                    return;
                }
                fileName = fileName.Replace(":", "_").Replace(" ", "_").Replace("\\", "_").Replace("/", "_");
                fileName = DateTime.Now.Ticks.ToString() + "_" + fileName;
                filePhoto.SaveAs(Server.MapPath("~/upload/" + fileName));
                imgPhoto.ImageUrl = "~/upload/" + fileName;
                // 清空文件上传组件
                filePhoto.Reset();
            }
        }
        #endregion 
    }
}