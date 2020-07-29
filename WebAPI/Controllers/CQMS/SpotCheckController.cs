using System;
using System.Collections.Generic;
using System.Web.Http;
using BLL;
using Model;

namespace Mvc.Controllers
{
    public class SpotCheckController : ApiController
    {
        //
        // 质量共检
        // GET: /Draw/
        [HttpGet] 
        public ResponseData<List<Check_SpotCheck>> Index(string projectId, int index, int page, string startTime = null, string endTime = null,string name = null)
        {
            ResponseData<List<Check_SpotCheck>> res = new ResponseData<List<Check_SpotCheck>>();

            res.successful = true;
            res.resultValue = BLL.SpotCheckService.GetListDataForApi(name,"",startTime, endTime, projectId, index, page);
            return res;
        }
        [HttpGet]
        public ResponseData<List<Check_SpotCheck>> Search(string projectId, int index, int page, string startTime = null, string endTime = null, string name = null,string unitId=null)
        {
            ResponseData<List<Check_SpotCheck>> res = new ResponseData<List<Check_SpotCheck>>();

            res.successful = true;
            res.resultValue = BLL.SpotCheckService.GetListDataForApi(name,unitId, startTime, endTime, projectId, index, page);
            return res;
        }
       
        //
        // 质量共检
        // GET: /Draw/
        [HttpGet]
        public ResponseData<Check_SpotCheck> GetSpotCheck(string spotCheckCode)
        {
            ResponseData<Check_SpotCheck> res = new ResponseData<Check_SpotCheck>();
            Check_SpotCheck jc = BLL.SpotCheckService.GetSpotCheckForApi(spotCheckCode);
            res.successful = true;
            res.resultValue = BeanUtil.CopyOjbect<Check_SpotCheck>(jc, true);
            return res;
        }

        [HttpGet]
        public ResponseData<List<Check_SpotCheckApprove>> GetApproveBySpotCheckCode(string spotCheckCode)
        {
            ResponseData<List<Check_SpotCheckApprove>> res = new ResponseData<List<Check_SpotCheckApprove>>();
            res.successful = true;
             res.resultValue = BLL.SpotCheckApproveService.getListDataBySpotCheckCodeForApi(spotCheckCode);

            return res;

        }
        public ResponseData<Check_SpotCheckApprove> GetCurrApproveByCode(string spotCheckCode)
        {
            ResponseData<Check_SpotCheckApprove> res = new ResponseData<Check_SpotCheckApprove>();

            res.successful = true;
            res.resultValue = BeanUtil.CopyOjbect<Check_SpotCheckApprove>(BLL.SpotCheckApproveService.getCurrApproveForApi(spotCheckCode), true);
            return res;
        }
        [HttpGet]
        public ResponseData<List<SpotCheckItem>> GetSpotCheckDetail(string spotCheckCode)
        {
            ResponseData<List<SpotCheckItem>> res = new ResponseData<List<SpotCheckItem>>();
            res.successful = true;
            // res.resultValue = BLL.JointCheckDetailService.getListData(id);

            List<SpotCheckItem> spotCheckItem = new List<SpotCheckItem>();
            List<Check_SpotCheckDetail> spotCheckDetails = BLL.SpotCheckDetailService.GetSpotCheckDetailsForApi(spotCheckCode);
            foreach (Check_SpotCheckDetail d in spotCheckDetails)
            {
                SpotCheckItem item = new SpotCheckItem();
                item.SpotCheckDetail = d;
                item.controlItemAndCycle = BLL.ControlItemAndCycleService.GetControlItemAndCycleById(d.ControlItemAndCycleId) ;
                item.controlItemAndCycle = BeanUtil.CopyOjbect<WBS_ControlItemAndCycle>(item.controlItemAndCycle,true);
                item.controlItemAndCycle.AttachUrl = BLL.ControlItemAndCycleService.ConvertDetailName(item.controlItemAndCycle.ControlItemAndCycleId);
                if (item.controlItemAndCycle != null)
                {
                    item.workPackage= BLL.WorkPackageService.GetWorkPackageByWorkPackageId(item.controlItemAndCycle.WorkPackageId);
                    WBS_WorkPackage temp = BeanUtil.CopyOjbect<WBS_WorkPackage>(item.workPackage, true);
                    temp.UnitWorkId = item.workPackage.UnitWorkId;
                    item.workPackage = temp;

                }
                   if (item.workPackage != null)
                {
                   // item.CNProfessional = BLL.CNProfessionalService.GetCNProfessional(item.workPackage.CNProfessionalCode);
                    //item.CNProfessional = BeanUtil.CopyOjbect<WBS_CNProfessional>(item.CNProfessional, true);

                }
                if (item.workPackage != null)
                {
                    item.unitWork = BLL.UnitWorkService.getUnitWorkByUnitWorkId(item.workPackage.UnitWorkId.ToString()) ;
                    item.unitWork = BeanUtil.CopyOjbect<WBS_UnitWork>(item.unitWork, true);

                }

                spotCheckItem.Add(item);
            }
            
            
            res.resultValue = spotCheckItem; 




            return res;

        }


        [HttpPost]
        public ResponseData<string> AddSpotCheck([FromBody]Model.Check_SpotCheck SpotControl)
        {
            ResponseData<string> res = new ResponseData<string>();
            try
            {
                if (string.IsNullOrEmpty(SpotControl.SpotCheckCode))
                {

                    SpotControl.SpotCheckCode = BLL.SQLHelper.GetNewID(typeof(Model.Check_SpotCheck));
                    if (string.IsNullOrEmpty(SpotControl.DocCode))
                    {
                        string prefix = BLL.ProjectService.GetProjectByProjectId(SpotControl.ProjectId).ProjectCode + "-06-CM03-SJ-";
                        SpotControl.DocCode = BLL.SQLHelper.RunProcNewId("SpGetNewIncentiveCode", "dbo.Check_CheckControl", "DocCode", prefix);
                    }
                    BLL.SpotCheckService.AddSpotCheckForApi(SpotControl);
                    //BLL.AttachFileService.updateAttachFile(SpotControl.AttachUrl, SpotControl.SpotCheckCode, BLL.Const.SpotCheckMenuId);
                    SaveAttachFile(SpotControl.SpotCheckCode, BLL.Const.SpotCheckMenuId, SpotControl.AttachUrl);
                    res.resultValue = SpotControl.SpotCheckCode;
                    res.successful = true;
                }
                else
                {
                    BLL.SpotCheckService.UpdateSpotCheckForUpdateForApi(SpotControl);
                    //BLL.AttachFileService.updateAttachFile(SpotControl.AttachUrl, SpotControl.SpotCheckCode, BLL.Const.SpotCheckMenuId);
                    SaveAttachFile(SpotControl.SpotCheckCode, BLL.Const.SpotCheckMenuId, SpotControl.AttachUrl);
                    res.resultValue = SpotControl.SpotCheckCode;
                    res.successful = true;
                }
            }
            catch (Exception e)
            {
                res.resultHint = e.StackTrace;
                res.successful = false;
            }
            return res;

        }

        /// <summary>
        /// 
        /// </summary>
        public static void SaveAttachFile(string dataId, string menuId, string url)
        {
            Model.ToDoItem toDoItem = new Model.ToDoItem
            {
                MenuId = menuId,
                DataId = dataId,
                UrlStr = url,
            };
            APIUpLoadFileService.SaveAttachUrl(toDoItem);
        }

        [HttpPost]
        public ResponseData<string> AddSpotCheckDetail([FromBody]Model.Check_SpotCheckDetail SpotCheckDetail)
        {
            ResponseData<string> res = new ResponseData<string>();
            try
            {
                if (string.IsNullOrEmpty(SpotCheckDetail.SpotCheckDetailId))
                {
                    SpotCheckDetail.SpotCheckDetailId = Guid.NewGuid().ToString();
                    SpotCheckDetail.CreateDate = DateTime.Now;
                    BLL.SpotCheckDetailService.AddSpotCheckDetailForApi(SpotCheckDetail);
                    res.resultValue = SpotCheckDetail.SpotCheckDetailId;
                }
                else
                {
                    BLL.SpotCheckDetailService.UpdateSpotCheckDetailForApi(SpotCheckDetail);
                    res.resultValue = SpotCheckDetail.SpotCheckDetailId;
                }
            }
            catch (Exception e)
            {
                res.successful = false;
            }
            res.successful = true;
            return res;

        }
        [HttpGet]
        public ResponseData<string> DelSpotCheckDetail(string SpotCheckDetailId)
        {
            ResponseData<string> res = new ResponseData<string>();
            try
            {
                if (!string.IsNullOrEmpty(SpotCheckDetailId))
                {
                    BLL.SpotCheckDetailService.DeleteSpotCheckDetail(SpotCheckDetailId);
                    res.resultValue =SpotCheckDetailId;
                }
                 
            }
            catch (Exception e)
            {
                res.successful = false;
            }
            res.successful = true;
            return res;

        }
        [HttpPost]
        public ResponseData<string> AddApprove([FromBody]Model.Check_SpotCheckApprove approve)
        {
            ResponseData<string> res = new ResponseData<string>();
            try
            {
                Model.Check_SpotCheck spotCheck1 = BLL.SpotCheckService.GetSpotCheckBySpotCheckCodeForApi(approve.SpotCheckCode);

                Model.Check_SpotCheck CheckControl = new Model.Check_SpotCheck();
                CheckControl.SpotCheckCode = approve.SpotCheckCode;
                CheckControl.State = approve.ApproveType;
                BLL.SpotCheckService.UpdateSpotCheckForApi(CheckControl);


                //总包专工确认时，通知相关人员
                if (approve.ApproveType == BLL.Const.SpotCheck_Audit3 || approve.ApproveType == BLL.Const.SpotCheck_Audit4)
                {
                    if (!string.IsNullOrEmpty(spotCheck1.JointCheckMans))
                    {
                        string[] seeUsers = spotCheck1.JointCheckMans.Split(',');
                        foreach (var seeUser in seeUsers)
                        {
                            if (!string.IsNullOrEmpty(seeUser))
                            {
                                Model.Check_SpotCheckApprove approve2 = new Model.Check_SpotCheckApprove();
                                approve2.SpotCheckCode = approve.SpotCheckCode;
                                approve2.ApproveMan = seeUser;
                                approve2.ApproveType = "S";
                                approve2.Sign = "1";
                                BLL.SpotCheckApproveService.AddSpotCheckApproveForApi(approve2);
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(spotCheck1.JointCheckMans2))
                    {
                        string[] seeUsers = spotCheck1.JointCheckMans2.Split(',');
                        foreach (var seeUser in seeUsers)
                        {
                            if (!string.IsNullOrEmpty(seeUser))
                            {
                                Model.Check_SpotCheckApprove approve2 = new Model.Check_SpotCheckApprove();
                                approve2.SpotCheckCode = approve.SpotCheckCode;
                                approve2.ApproveMan = seeUser;
                                approve2.ApproveType = "S";
                                approve2.Sign = "1";
                                BLL.SpotCheckApproveService.AddSpotCheckApproveForApi(approve2);
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(spotCheck1.JointCheckMans3))
                    {
                        string[] seeUsers = spotCheck1.JointCheckMans3.Split(',');
                        foreach (var seeUser in seeUsers)
                        {
                            if (!string.IsNullOrEmpty(seeUser))
                            {
                                Model.Check_SpotCheckApprove approve2 = new Model.Check_SpotCheckApprove();
                                approve2.SpotCheckCode = approve.SpotCheckCode;
                                approve2.ApproveMan = seeUser;
                                approve2.ApproveType = "S";
                                approve2.Sign = "1";
                                BLL.SpotCheckApproveService.AddSpotCheckApproveForApi(approve2);
                            }
                        }
                    }
                    Model.Check_SpotCheckApprove ap = BLL.SpotCheckApproveService.GetComplieForApi(approve.SpotCheckCode);
                    if (ap != null)
                    {
                        Model.Check_SpotCheckApprove approve2 = new Model.Check_SpotCheckApprove();
                        approve2.SpotCheckCode = approve.SpotCheckCode;
                        approve2.ApproveMan = ap.ApproveMan;
                        approve2.ApproveType = "S";
                        approve2.Sign = "1";
                        BLL.SpotCheckApproveService.AddSpotCheckApproveForApi(approve2);
                    }
                }


                if (approve.ApproveType == BLL.Const.SpotCheck_Complete)  //审批完成时，生成分包上传交工资料的办理记录
                {
                    CheckControl.State2 = BLL.Const.SpotCheck_Audit5;   //更新主表状态
                    bool isShow = true;   //判断主表是否需要上传资料
                    var list = BLL.SpotCheckDetailService.GetOKSpotCheckDetailsForApi(CheckControl.SpotCheckCode);
                    if (list.Count == 0)   //没有合格项，则在上传资料页面不显示该主表记录
                    {
                        isShow = false;
                    }
                    else
                    {
                        bool isExitForms = false;
                        foreach (var item in list)
                        {
                            Model.WBS_ControlItemAndCycle c = BLL.ControlItemAndCycleService.GetControlItemAndCycleByIdForApi(item.ControlItemAndCycleId);
                            if (c != null)
                            {
                                if (!string.IsNullOrEmpty(c.HGForms) || !string.IsNullOrEmpty(c.SHForms))
                                {
                                    isExitForms = true;
                                    break;
                                }
                            }
                        }
                        if (!isExitForms)   //不存在有表格需上传的明细记录
                        {
                            isShow = false;
                        }
                    }
                    CheckControl.IsShow = isShow;
                    BLL.SpotCheckService.UpdateSpotCheckForApi(CheckControl);
                    foreach (var item in list)
                    {
                        //更新明细记录
                        //判断明细是否需要上传资料
                        Model.WBS_ControlItemAndCycle c = BLL.ControlItemAndCycleService.GetControlItemAndCycleByIdForApi(item.ControlItemAndCycleId);
                        if (c != null)
                        {
                            if (string.IsNullOrEmpty(c.HGForms) && string.IsNullOrEmpty(c.SHForms))
                            {
                                item.IsShow = false;
                                item.IsDataOK = "2";    //资料情况为不需要
                            }
                            else
                            {
                                item.IsShow = true;
                            }
                        }
                        item.State = BLL.Const.SpotCheck_Audit5;
                        item.HandleMan = spotCheck1.CreateMan;
                        BLL.SpotCheckDetailService.UpdateSpotCheckDetailForApi(item);
                        if (item.IsShow == true)
                        {
                            //新增待办记录
                            Model.Check_SpotCheckApprove approve2 = new Model.Check_SpotCheckApprove();
                            approve2.SpotCheckCode = CheckControl.SpotCheckCode;
                            approve2.ApproveMan = spotCheck1.CreateMan;
                            approve2.ApproveType = BLL.Const.SpotCheck_Audit5;
                            approve2.Sign = "2";
                            approve2.SpotCheckDetailId = item.SpotCheckDetailId;
                            BLL.SpotCheckApproveService.AddSpotCheckApproveForApi(approve2);
                        }
                    }
                }
                res.resultValue = BLL.SpotCheckApproveService.AddSpotCheckApproveForApi(approve);

            }
            catch (Exception e)
            {
                res.successful = false;
            }
            res.successful = true;
            return res;

        }
        [HttpPost]
        public ResponseData<string> UpdateApprove([FromBody]Model.Check_SpotCheckApprove approve)
        {
            ResponseData<string> res = new ResponseData<string>();
            try
            {
                //Model.Check_JointCheckApprove approve1 = BLL.JointCheckApproveService.GetJointCheckApproveByJointCheckId(approve.JointCheckId);
                approve.ApproveDate = DateTime.Now;
                //approve1.ApproveIdea = approve.ApproveIdea;
                //approve1.IsAgree = approve.IsAgree;
                //  approve1.AttachUrl = approve.AttachUrl;
                BLL.SpotCheckApproveService.UpdateSpotCheckApproveForApi(approve);

            }
            catch (Exception e)
            {
                res.successful = false;
            }
            res.successful = true;
            return res;

        }

        [HttpGet]
        public   ResponseData<List<TreeNode>> getWBSTree(string projectId)
        {
            ResponseData<List<TreeNode>> res = new ResponseData<List<TreeNode>>();
            List<TreeNode> resList = new List<TreeNode>();
            res.successful = true;
            res.resultValue = resList;
            TreeNode t1 = new TreeNode();
            t1.ID = "1";
            t1.Depth = 1;
            t1.Title = "建筑工程";
            t1.Type = "projectType";
            resList.Add(t1);
            List<Model.WBS_UnitWork> works =  BLL.UnitWorkService.GetUnitWorkListByPidForApi(projectId, "1");
            foreach(var item  in works)
            {
                TreeNode tw = new TreeNode();
                tw.ID = item.UnitWorkId;
                tw.Title =item.UnitWorkName;
                tw.Depth = 2;
                tw.Type = "unitWork";
                if (t1.child == null)
                {
                    t1.child = new List<TreeNode>();
                }
                t1.child.Add(tw);
                List<Model.WBS_WorkPackage> packages = BLL.WorkPackageService.GetApproveWorkPackages1sByUnitWorkId(item.UnitWorkId);
                foreach(var wp in packages)
                {
                    if(tw.child == null)
                    {
                        tw.child = new List<TreeNode>();
                    }
                    tw.child.Add(GetTreeNode(wp,3));
                }
            }
            TreeNode t2 = new TreeNode();
            t2.ID = "2";
            t2.Title = "安装工程";
            t2.Type = "projectType";
            t2.Depth = 1;
            resList.Add(t2);
            List<Model.WBS_UnitWork> works2 = BLL.UnitWorkService.GetUnitWorkListByPidForApi(projectId, "2");
            foreach (var item in works2)
            {
                TreeNode tw = new TreeNode();
                tw.ID = item.UnitWorkId;
                tw.Title = item.UnitWorkName;
                tw.Depth = 2;
                tw.Type = "unitWork";
                if (t2.child == null)
                {
                    t2.child = new List<TreeNode>();
                }
                t2.child.Add(tw);
                List<Model.WBS_WorkPackage> packages = BLL.WorkPackageService.GetApproveWorkPackages1sByUnitWorkId(item.UnitWorkId);
                foreach (var wp in packages)
                {
                    if (tw.child == null)
                    {
                        tw.child = new List<TreeNode>();
                    }
                    tw.child.Add(GetTreeNode(wp,3));
                }
            }
            return res;

        }

        private TreeNode GetTreeNode(WBS_WorkPackage package,int depth)
        {
            TreeNode node = new TreeNode();
            node.Title = package.PackageContent;
            node.ID = package.WorkPackageId;
            node.Type = "package";
            node.Depth = depth;
            List<Model.WBS_WorkPackage> packages = BLL.WorkPackageService.GetAllApproveWorkPackagesBySuperWorkPackageId(package.WorkPackageId);
            foreach (var wp in packages)
            {
                if (node.child == null)
                {
                    node.child = new List<TreeNode>();
                }
                node.child.Add(GetTreeNode(wp, depth+1));
            }
            return node;

        }
        [HttpGet]
        public ResponseData<string> see(string dataId, string userId)
        {
            ResponseData<string> res = new ResponseData<string>();
            res.successful = true;
            BLL.SpotCheckApproveService.See(dataId, userId);
            return res;
        }
    }
}
