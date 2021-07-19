using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public static class APITestPackageService
    {
        #region 获取试压包号
        /// <summary>
        /// 获取试压包号
        /// </summary>
        /// <param name="unitWorkId">单位工程</param>
        /// <param name="isFinish">是否完成</param>
        /// <param name="testPackageNo">试压包号</param>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getTestPackageNo(string unitWorkId, bool isFinish, string testPackageNo)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var dataList = from x in db.PTP_TestPackage
                               where x.UnitWorkId == unitWorkId
                               select x;

                if (!string.IsNullOrEmpty(testPackageNo))
                {
                    dataList = dataList.Where(e => e.TestPackageNo.Contains(testPackageNo));
                }

                if (isFinish == true)
                {
                    dataList = dataList.Where(e => e.AduditDate.HasValue == true);
                }
                else
                {
                    dataList = dataList.Where(e => e.AduditDate.HasValue == false);
                }

                var getDataLists = (from x in dataList
                                    orderby x.TestPackageNo
                                    select new Model.BaseInfoItem
                                    {
                                        BaseInfoId = x.PTP_ID,
                                        BaseInfoCode = x.TestPackageNo,
                                    }).ToList();
                return getDataLists;
            }
        }
        #endregion

        #region 获取尾项检查试压包号
        /// <summary>
        /// 获取尾项检查试压包号
        /// </summary>
        /// <param name="unitWorkId">单位工程</param>
        /// <param name="isFinish">是否完成</param>
        /// <param name="testPackageNo">试压包号</param>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getItemEndCheckTestPackageNo(string unitWorkId, string testPackageNo)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var dataList = from x in db.PTP_TestPackage
                               where x.UnitWorkId == unitWorkId && x.AduditDate.HasValue
                               select x;

                if (!string.IsNullOrEmpty(testPackageNo))
                {
                    dataList = dataList.Where(e => e.TestPackageNo.Contains(testPackageNo));
                }

                var getDataLists = (from x in dataList
                                    orderby x.TestPackageNo
                                    select new Model.BaseInfoItem
                                    {
                                        BaseInfoId = x.PTP_ID,
                                        BaseInfoCode = x.TestPackageNo,
                                    }).ToList();
                return getDataLists;
            }
        }
        #endregion

        #region
        public static List<Model.View_PTP_ItemEndCheckList> getItemEndCheckList(string pTP_ID, int index, int page)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                IQueryable<Model.View_PTP_ItemEndCheckList> q = db.View_PTP_ItemEndCheckList;
                List<string> ids = new List<string>();
                if (!string.IsNullOrEmpty(pTP_ID))
                {
                    q = q.Where(e => e.PTP_ID == pTP_ID);
                }

                var qq1 = from x in q
                          orderby x.CompileDate descending
                          select new
                          {
                              x.ItemEndCheckListId,
                              x.PTP_ID,
                              x.CompileMan,
                              x.CompileDate,
                              x.State,
                              x.AIsOK,
                              x.BIsOK,
                              x.AOKState,
                              x.CompileManName,
                              x.TestPackageNo,
                              x.TestPackageName,
                              AIsOKStr = BLL.ItemEndCheckListService.ConvertAState(x.ItemEndCheckListId),
                              BIsOKStr = BLL.ItemEndCheckListService.ConvertBState(x.ItemEndCheckListId),
                              x.StateStr,
                              AuditManName = BLL.ItemEndCheckListService.ConvertMan(x.ItemEndCheckListId)
                          };
                var list = qq1.Skip(index * page).Take(page).ToList();

                List<Model.View_PTP_ItemEndCheckList> listRes = new List<Model.View_PTP_ItemEndCheckList>();
                for (int i = 0; i < list.Count; i++)
                {
                    Model.View_PTP_ItemEndCheckList x = new Model.View_PTP_ItemEndCheckList();
                    x.ItemEndCheckListId = list[i].ItemEndCheckListId;
                    x.PTP_ID = list[i].PTP_ID;
                    x.CompileMan = list[i].CompileMan;
                    x.CompileDate = list[i].CompileDate;
                    x.State = list[i].State;
                    x.AIsOK = list[i].AIsOK;
                    x.BIsOK = list[i].BIsOK;
                    x.AOKState = list[i].AOKState;
                    x.CompileManName = list[i].CompileManName;
                    x.TestPackageNo = list[i].TestPackageNo;
                    x.TestPackageName = list[i].TestPackageName;
                    x.AIsOKStr = list[i].AIsOKStr;
                    x.BIsOKStr = list[i].BIsOKStr;
                    x.StateStr = list[i].StateStr;
                    x.AuditManName = list[i].AuditManName;
                    listRes.Add(x);
                }
                return listRes;
            }
        }

        public static List<Model.HJGL_Pipeline> getItemEndCheckPipeline(string pTP_ID)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                IQueryable<Model.HJGL_Pipeline> q = from x in db.HJGL_Pipeline
                                                    join y in db.PTP_PipelineList
                                                    on x.PipelineId equals y.PipelineId
                                                    where y.PTP_ID == pTP_ID
                                                    select x;
                List<string> ids = new List<string>();

                var list = (from x in q
                            orderby x.PipelineCode
                            select new
                            {
                                x.PipelineId,
                                x.PipelineCode,
                            }).ToList();
                List<Model.HJGL_Pipeline> listRes = new List<Model.HJGL_Pipeline>();
                for (int i = 0; i < list.Count; i++)
                {
                    Model.HJGL_Pipeline x = new Model.HJGL_Pipeline();
                    x.PipelineId = list[i].PipelineId;
                    x.PipelineCode = list[i].PipelineCode;
                    listRes.Add(x);
                }
                return listRes;
            }
        }

        public static List<Model.BaseInfoItem> getHandleType(string state)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var list = BLL.TestPackageEditService.GetHandleTypeByState(state);
                List<Model.BaseInfoItem> dataList = new List<Model.BaseInfoItem>();
                foreach (var item in list)
                {
                    Model.BaseInfoItem b = new Model.BaseInfoItem();
                    b.BaseInfoId = item.Value;
                    b.BaseInfoName = item.Text;
                    dataList.Add(b);
                }

                return dataList;
            }
        }

        public static List<Model.BaseInfoItem> getHandleMan(string state, string projectId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                List<Model.Sys_User> users = new List<Model.Sys_User>();
                if (state == BLL.Const.TestPackage_Compile)
                {
                    users = BLL.UserService.GetUserListByProjectIdAndUnitType(projectId, BLL.Const.ProjectUnitType_2);
                }
                else if (state == BLL.Const.TestPackage_Audit1 || state == BLL.Const.TestPackage_ReAudit2)
                {
                    users = BLL.UserService.GetUserListByProjectIdAndUnitType(projectId, BLL.Const.ProjectUnitType_1);
                }
                else if (state == BLL.Const.TestPackage_Audit2)
                {
                    users = BLL.UserService.GetUserListByProjectIdAndUnitType(projectId, BLL.Const.ProjectUnitType_3);
                }
                else if (state == BLL.Const.TestPackage_Audit3)
                {

                }
                List<Model.BaseInfoItem> dataList = new List<Model.BaseInfoItem>();
                foreach (var item in users)
                {
                    Model.BaseInfoItem b = new Model.BaseInfoItem();
                    b.BaseInfoId = item.UserId;
                    b.BaseInfoName = item.UserName;
                    dataList.Add(b);
                }

                return dataList;
            }
        }

        public static List<Model.BaseInfoItem> getChangeStateHandleMan(string state, string projectId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                List<Model.Sys_User> users = new List<Model.Sys_User>();
                if (state == BLL.Const.TestPackage_ReAudit2)
                {
                    users = BLL.UserService.GetUserListByProjectIdAndUnitType(projectId, BLL.Const.ProjectUnitType_2);
                }
                else if (state == BLL.Const.TestPackage_Audit3)
                {
                    users = BLL.UserService.GetUserListByProjectIdAndUnitType(projectId, BLL.Const.ProjectUnitType_3);
                }
                List<Model.BaseInfoItem> dataList = new List<Model.BaseInfoItem>();
                foreach (var item in users)
                {
                    Model.BaseInfoItem b = new Model.BaseInfoItem();
                    b.BaseInfoId = item.UserId;
                    b.BaseInfoName = item.UserName;
                    dataList.Add(b);
                }

                return dataList;
            }
        }
        #endregion

        #region 根据试压包ID获取明细
        /// <summary>
        /// 根据试压包ID获取明细
        /// </summary>
        /// <param name="ptp_Id"></param>
        /// <returns></returns>
        public static List<Model.TestPackageItem> GetTestPackageDetail(string ptp_Id)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataLists = (from x in db.View_PTP_TestPackageAudit
                                    where x.PTP_ID == ptp_Id
                                    orderby x.PipelineCode
                                    select new Model.TestPackageItem
                                    {
                                        PipelineCode = x.PipelineCode,
                                        WeldJointCount = x.WeldJointCount,
                                        WeldJointCountT = x.WeldJointCountT,
                                        CountS = x.CountS,
                                        CountU = x.CountU,
                                        NDTR_Name = x.NDTR_Name,
                                        Ratio = x.Ratio

                                    }).ToList();

                return getDataLists;
            }
        }
        #endregion

        #region  获取具备试压条件的试压包提醒
        /// <summary>
        /// 获取具备试压条件的试压包提醒
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> GetCanTestPackageWarning(string projectId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                List<Model.BaseInfoItem> canTest = new List<Model.BaseInfoItem>();
                // 获取项目中未完成的试压包
                var testPackage = from x in db.PTP_TestPackage
                                  where x.ProjectId == projectId && !x.FinishDate.HasValue
                                  select x;

                foreach (var t in testPackage)
                {
                    string strSql = @"SELECT ProjectId,PTP_ID,WeldJointCount,WeldJointCountT,CountU 
                              FROM dbo.View_PTP_TestPackageAudit
                             WHERE PTP_ID=@PTP_ID";

                    List<SqlParameter> listStr = new List<SqlParameter>();
                    listStr.Add(new SqlParameter("@PTP_ID", t.PTP_ID));
                    SqlParameter[] parameter = listStr.ToArray();
                    DataTable dt = SQLHelper.GetDataTableRunText(strSql, parameter);

                    if (IsCanTest(dt))
                    {

                        Model.BaseInfoItem item = new Model.BaseInfoItem();
                        item.BaseInfoId = t.PTP_ID;
                        item.BaseInfoCode = "具备试压条件：" + t.TestPackageNo;
                        canTest.Add(item);
                    }
                }
                return canTest;
            }
        }


        private static bool IsCanTest(DataTable dt)
        {
            bool isPass = true;
            foreach (DataRow row in dt.Rows)
            {
                int totalJoint = Convert.ToInt32(row["WeldJointCount"]);
                int compJoint = Convert.ToInt32(row["WeldJointCountT"]);
                int noPassJoint = Convert.ToInt32(row["CountU"]);

                if (totalJoint != compJoint || noPassJoint != 0)
                {
                    isPass = false;
                    break;
                }
            }

            return isPass;
        }
        #endregion

        public static Model.View_PTP_ItemEndCheckList GetViewItemEndCheckList(string itemEndCheckListId)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                var res = db.View_PTP_ItemEndCheckList.FirstOrDefault(e => e.ItemEndCheckListId == itemEndCheckListId);
                res.PTP_ID = res.PTP_ID;
                res.CompileMan = res.CompileMan;
                res.CompileDate = res.CompileDate;
                res.State = res.State;
                res.AIsOK = res.AIsOK;
                res.BIsOK = res.BIsOK;
                res.AOKState = res.AOKState;
                res.TestPackageNo = res.TestPackageNo;
                res.TestPackageName = res.TestPackageName;
                return res;
            }
        }

        public static Model.PTP_ItemEndCheckList GetItemEndCheckList(string itemEndCheckListId)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                var res = db.PTP_ItemEndCheckList.FirstOrDefault(e => e.ItemEndCheckListId == itemEndCheckListId);
                res.PTP_ID = res.PTP_ID;
                res.CompileMan = res.CompileMan;
                res.CompileDate = res.CompileDate;
                res.State = res.State;
                res.AIsOK = res.AIsOK;
                res.BIsOK = res.BIsOK;
                res.AOKState = res.AOKState;
                return res;
            }
        }

        public static List<Model.View_PTP_ItemEndCheck> getItemEndCheckDetail(string itemEndCheckListId)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                IQueryable<Model.View_PTP_ItemEndCheck> q = db.View_PTP_ItemEndCheck;
                if (!string.IsNullOrEmpty(itemEndCheckListId))
                {
                    q = q.Where(e => e.ItemEndCheckListId == itemEndCheckListId);
                }
                var qres = from x in q
                           orderby x.PipelineCode ascending
                           select new
                           {
                               x.ItemCheckId,
                               x.PipelineId,
                               x.Content,
                               x.ItemType,
                               x.Result,
                               x.ItemEndCheckListId,
                               x.Remark,
                               x.PipelineCode,
                           };
                var list = qres.ToList();
                List<Model.View_PTP_ItemEndCheck> res = new List<Model.View_PTP_ItemEndCheck>();

                foreach (var item in list)
                {
                    Model.View_PTP_ItemEndCheck x = new Model.View_PTP_ItemEndCheck();
                    x.ItemCheckId = item.ItemCheckId;
                    x.PipelineId = item.PipelineId;
                    x.Content = item.Content;
                    x.ItemType = item.ItemType;
                    x.Result = item.Result;
                    x.ItemEndCheckListId = item.ItemEndCheckListId;
                    x.Remark = item.Remark;
                    x.PipelineCode = item.PipelineCode;
                    res.Add(x);
                }
                return res;
            }
        }

        public static List<Model.View_PTP_TestPackageApprove> getTestPackageApprove(string itemEndCheckListId)
        {
            using (var db = new Model.SGGLDB(Funs.ConnString))
            {
                IQueryable<Model.View_PTP_TestPackageApprove> q = db.View_PTP_TestPackageApprove;
                if (!string.IsNullOrEmpty(itemEndCheckListId))
                {
                    q = q.Where(e => e.ItemEndCheckListId == itemEndCheckListId);
                }
                var qres = from x in q
                           where x.ApproveDate != null
                           orderby x.ADate ascending
                           select new
                           {
                               x.ApproveId,
                               x.ApproveDate,
                               x.Opinion,
                               x.ApproveMan,
                               x.ApproveType,
                               x.ItemEndCheckListId,
                               x.ApproveManName,
                               x.StateStr,
                           };
                var list = qres.ToList();
                List<Model.View_PTP_TestPackageApprove> res = new List<Model.View_PTP_TestPackageApprove>();

                foreach (var item in list)
                {
                    Model.View_PTP_TestPackageApprove x = new Model.View_PTP_TestPackageApprove();
                    x.ApproveId = item.ApproveId;
                    x.ApproveDate = item.ApproveDate;
                    x.Opinion = item.Opinion;
                    x.ApproveMan = item.ApproveMan;
                    x.ApproveType = item.ApproveType;
                    x.ItemEndCheckListId = item.ItemEndCheckListId;
                    x.ApproveManName = item.ApproveManName;
                    x.StateStr = item.StateStr;
                    res.Add(x);
                }
                return res;
            }
        }

        #region 保存ItemEndCheckList
        /// <summary>
        /// 保存ItemEndCheckList
        /// </summary>
        /// <param name="newItem">尾项记录</param>
        /// <returns></returns>
        public static string SaveItemEndCheckList(Model.ItemEndCheckList newItem)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                string message = string.Empty;
                string saveType = newItem.SaveOrSubmit;
                string oldState = newItem.OldState;
                string currUserId = newItem.CurrUserId;
                Model.PTP_ItemEndCheckList newItemEndCheckList = new Model.PTP_ItemEndCheckList
                {
                    ItemEndCheckListId = newItem.ItemEndCheckListId,
                    PTP_ID = newItem.PTP_ID,
                    CompileMan = newItem.CompileMan,
                    CompileDate = newItem.CompileDate,
                    State = newItem.State,
                    AIsOK = newItem.AIsOK,
                    BIsOK = newItem.BIsOK,
                    AOKState = newItem.AOKState,
                };
                if (saveType == "save")
                {
                    newItemEndCheckList.State = oldState;
                }
                var updateItemEndCheckList = db.PTP_ItemEndCheckList.FirstOrDefault(x => x.ItemEndCheckListId == newItem.ItemEndCheckListId);
                if (updateItemEndCheckList == null)
                {
                    newItemEndCheckList.ItemEndCheckListId = SQLHelper.GetNewID();
                    db.PTP_ItemEndCheckList.InsertOnSubmit(newItemEndCheckList);
                    db.SubmitChanges();
                    Model.PTP_TestPackageApprove approve1 = new Model.PTP_TestPackageApprove();
                    approve1.ApproveId = SQLHelper.GetNewID(typeof(Model.PTP_TestPackageApprove));
                    if (saveType == "submit")
                    {
                        approve1.ApproveDate = DateTime.Now;
                    }
                    approve1.ApproveMan = currUserId;
                    approve1.ApproveType = BLL.Const.TestPackage_Compile;
                    approve1.ItemEndCheckListId = newItemEndCheckList.ItemEndCheckListId;
                    BLL.TestPackageApproveService.AddTestPackageApprove(approve1);
                }
                else
                {
                    Model.PTP_TestPackageApprove approve1 = BLL.TestPackageApproveService.GetTestPackageApproveById(newItemEndCheckList.ItemEndCheckListId);
                    if (approve1 != null && saveType == "submit")
                    {
                        approve1.ApproveDate = DateTime.Now;
                        approve1.Opinion = newItem.TestPackageApproveItems[0].Opinion;
                        BLL.TestPackageApproveService.UpdateTestPackageApprove(approve1);
                    }
                    var getItemEndCheck = BLL.AItemEndCheckService.GetItemEndCheckByItemEndCheckListId(newItemEndCheckList.ItemEndCheckListId);
                    if (newItemEndCheckList.State == Const.TestPackage_Complete)
                    {
                        bool b = true;   //B项是否全部整改完成
                        var BItems = getItemEndCheck.Where(x => x.ItemType == "B");
                        foreach (var item in BItems)
                        {
                            if (item.Result != "合格")
                            {
                                b = false;
                            }
                        }
                        if (b)
                        {
                            newItemEndCheckList.State = Const.TestPackage_Complete;
                        }
                        else
                        {
                            newItemEndCheckList.State = BLL.Const.TestPackage_ReAudit2;
                            var approve2 = db.PTP_TestPackageApprove.FirstOrDefault(x => x.ItemEndCheckListId == newItemEndCheckList.ItemEndCheckListId && x.ApproveType == BLL.Const.TestPackage_Audit1);
                            Model.PTP_TestPackageApprove approveR = new Model.PTP_TestPackageApprove();
                            approveR.ApproveId = SQLHelper.GetNewID(typeof(Model.PTP_TestPackageApprove));
                            if (approve2 != null)
                            {
                                approveR.ApproveMan = approve2.ApproveMan;
                            }
                            approveR.ApproveType = BLL.Const.TestPackage_ReAudit2;
                            approveR.ItemEndCheckListId = newItemEndCheckList.ItemEndCheckListId;
                            BLL.TestPackageApproveService.AddTestPackageApprove(approveR);
                        }
                    }
                    ItemEndCheckListService.UpdateItemEndCheckList(newItemEndCheckList);
                    //// 删除检查明细项
                    AItemEndCheckService.DeleteAllItemEndCheckByID(newItemEndCheckList.ItemEndCheckListId);
                }
                if (newItem.ItemEndCheckItems != null && newItem.ItemEndCheckItems.Count() > 0)
                {
                    bool flag = true;
                    foreach (var item in newItem.ItemEndCheckItems)
                    {
                        if (item.Content != "/")
                        {
                            Model.PTP_ItemEndCheck newItemEndCheck = new Model.PTP_ItemEndCheck();
                            newItemEndCheck.ItemCheckId = SQLHelper.GetNewID();
                            newItemEndCheck.PipelineId = item.PipelineId;
                            newItemEndCheck.Content = item.Content;
                            newItemEndCheck.ItemType = item.ItemType;
                            newItemEndCheck.Result = item.Result;
                            newItemEndCheck.ItemEndCheckListId = newItemEndCheckList.ItemEndCheckListId;
                            newItemEndCheck.Remark = item.Remark;
                            BLL.AItemEndCheckService.AddAItemEndCheckForApi(newItemEndCheck);
                        }
                        else
                        {
                            Model.PTP_ItemEndCheck newItemEndCheck = new Model.PTP_ItemEndCheck();
                            newItemEndCheck.ItemCheckId = SQLHelper.GetNewID();
                            newItemEndCheck.ItemEndCheckListId = item.ItemEndCheckListId;
                            newItemEndCheck.PipelineId = item.PipelineId;
                            newItemEndCheck.Content = "/";
                            newItemEndCheck.ItemType = "/";
                            newItemEndCheck.Remark = item.Remark;
                            AItemEndCheckService.AddAItemEndCheck(newItemEndCheck);
                        }
                        if (item.ItemType == "A" && item.Result != "合格")
                        {
                            flag = false;
                        }
                    }
                    if (flag)
                    {
                        newItemEndCheckList.AOKState = true;
                    }
                    else
                    {
                        newItemEndCheckList.AOKState = null;
                    }
                    ItemEndCheckListService.UpdateItemEndCheckList(newItemEndCheckList);
                }
                if (newItem.TestPackageApproveItems != null && newItem.TestPackageApproveItems.Count() > 0)
                {
                    if (saveType == "submit")
                    {
                        if (newItem.State != Const.TestPackage_Complete)
                        {
                            foreach (var item in newItem.TestPackageApproveItems)
                            {
                                Model.PTP_TestPackageApprove newTestPackageApprove = new Model.PTP_TestPackageApprove();
                                newTestPackageApprove.ApproveId = SQLHelper.GetNewID();
                                newTestPackageApprove.Opinion = item.Opinion;
                                if(!string.IsNullOrEmpty(item.ApproveMan))
                                { 
                                newTestPackageApprove.ApproveMan = item.ApproveMan;
                                    }
                                newTestPackageApprove.ApproveType = item.ApproveType;
                                newTestPackageApprove.ItemEndCheckListId = newItemEndCheckList.ItemEndCheckListId;
                                BLL.TestPackageApproveService.AddTestPackageApproveForApi(newTestPackageApprove);
                            }
                        }
                    }
                }
                return message;
            }
        }
        #endregion
    }
}
