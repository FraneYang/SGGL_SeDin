using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    /// <summary>
    /// 施工方案
    /// </summary>
    public static class APIConstructSolutionService
    {
        #region 根据constructSolutionId获取施工方案
        /// <summary>
        ///  根据 constructSolutionId获取施工方案
        /// </summary>
        /// <param name="constructSolutionId"></param>
        /// <returns></returns>
        public static Model.ConstructSolutionItem getConstructSolutionByConstructSolutionId(string constructSolutionId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getInfo = from x in db.Solution_ConstructSolution
                              where x.ConstructSolutionId == constructSolutionId
                              select new Model.ConstructSolutionItem
                              {
                                  ConstructSolutionId = x.ConstructSolutionId,
                                  ProjectId = x.ProjectId,
                                  ConstructSolutionCode = x.ConstructSolutionCode,
                                  ConstructSolutionName = x.ConstructSolutionName,
                                  VersionNo = x.VersionNo,
                                  UnitId = x.UnitId,
                                  UnitName = db.Base_Unit.First(u => u.UnitId == x.UnitId).UnitName,
                                  InvestigateType = x.InvestigateType,
                                  InvestigateTypeName = db.Sys_Const.FirstOrDefault(c => c.GroupId == ConstValue.Group_InvestigateType && c.ConstValue == x.InvestigateType).ConstText,
                                  SolutinTypeId = x.SolutinType,
                                  SolutinTypeName = db.Sys_Const.FirstOrDefault(c => c.GroupId == ConstValue.Group_CNProfessional && c.ConstValue == x.SolutinType).ConstText,
                                  FileContents = System.Web.HttpUtility.HtmlDecode(x.FileContents),
                                  CompileManId = x.CompileMan,
                                  CompileManName = db.Sys_User.First(u => u.UserId == x.CompileMan).UserName,
                                  CompileDate = string.Format("{0:yyyy-MM-dd}", x.CompileDate),
                                  States = x.States,
                                  AttachUrl = db.AttachFile.FirstOrDefault(z => z.ToKeyId == x.ConstructSolutionId).AttachUrl.Replace('\\', '/'),
                              };
                return getInfo.FirstOrDefault();
            }
        }
        #endregion        

        #region 获取施工方案列表信息
        /// <summary>
        /// 获取施工方案列表信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="unitId"></param>
        /// <param name="strParam"></param>
        /// <returns></returns>
        public static List<Model.ConstructSolutionItem> getConstructSolutionList(string projectId, string unitId, string strParam, string states)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getConstructSolution = from x in db.Solution_ConstructSolution
                                           where x.ProjectId == projectId && (x.UnitId == unitId || unitId == null)
                                          && (strParam == null || x.ConstructSolutionName.Contains(strParam))
                                          && (states == null || (states == "0" && (x.States == "0" || x.States == null)) || (states == "1" && (x.States == "1" || x.States == "2")))
                                           orderby x.ConstructSolutionCode descending
                                           select new Model.ConstructSolutionItem
                                           {
                                               ConstructSolutionId = x.ConstructSolutionId,
                                               ProjectId = x.ProjectId,
                                               ConstructSolutionCode = x.ConstructSolutionCode,
                                               ConstructSolutionName = x.ConstructSolutionName,
                                               VersionNo = x.VersionNo,
                                               UnitId = x.UnitId,
                                               UnitName = db.Base_Unit.First(u => u.UnitId == x.UnitId).UnitName,
                                               InvestigateType = x.InvestigateType,
                                               InvestigateTypeName = db.Sys_Const.FirstOrDefault(c => c.GroupId == ConstValue.Group_InvestigateType && c.ConstValue == x.InvestigateType).ConstText,
                                               SolutinTypeId = x.SolutinType,
                                               SolutinTypeName = db.Sys_Const.FirstOrDefault(c => c.GroupId == ConstValue.Group_CNProfessional && c.ConstValue == x.SolutinType).ConstText,
                                               FileContents = x.FileContents,
                                               CompileManId = x.CompileMan,
                                               CompileManName = db.Sys_User.First(u => u.UserId == x.CompileMan).UserName,
                                               CompileDate = string.Format("{0:yyyy-MM-dd}", x.CompileDate),
                                               States = x.States,
                                               AttachUrl = db.AttachFile.FirstOrDefault(z => z.ToKeyId == x.ConstructSolutionId).AttachUrl.Replace('\\', '/'),
                                           };
                return getConstructSolution.ToList();
            }
        }
        #endregion        

        #region 保存Solution_ConstructSolution
        /// <summary>
        /// 保存Solution_ConstructSolution
        /// </summary>
        /// <param name="newItem">施工方案</param>
        /// <returns></returns>
        public static void SaveConstructSolution(Model.ConstructSolutionItem newItem)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Solution_ConstructSolution newConstructSolution = new Model.Solution_ConstructSolution
            {
                ConstructSolutionId = newItem.ConstructSolutionId,
                ProjectId = newItem.ProjectId,
                ConstructSolutionCode = newItem.ConstructSolutionCode,
                ConstructSolutionName = newItem.ConstructSolutionName,
                VersionNo = newItem.VersionNo,
                UnitId = newItem.UnitId,
                InvestigateType = newItem.InvestigateType,
                SolutinType = newItem.SolutinTypeId,
                FileContents = System.Web.HttpUtility.HtmlEncode(newItem.FileContents),
                CompileMan = newItem.CompileManId,
                CompileManName =UserService.GetUserNameByUserId( newItem.CompileManId),
                States =Const.State_2,
            };
            if (newItem.States != "1")
            {
                newConstructSolution.States = Const.State_0;
            }

            var updateConstructSolution = db.Solution_ConstructSolution.FirstOrDefault(x => x.ConstructSolutionId == newItem.ConstructSolutionId);
            if (updateConstructSolution == null)
            {
                newConstructSolution.CompileDate = DateTime.Now;
                newConstructSolution.ConstructSolutionId = SQLHelper.GetNewID();
                newConstructSolution.ConstructSolutionCode = CodeRecordsService.ReturnCodeByMenuIdProjectId(Const.ProjectConstructSolutionMenuId, newConstructSolution.ProjectId, newConstructSolution.UnitId);
                ConstructSolutionService.AddConstructSolution(newConstructSolution);
            }
            else
            {
                ConstructSolutionService.UpdateConstructSolution(newConstructSolution);
            }
            if (newConstructSolution.States == "1")
            {
                CommonService.btnSaveData(newConstructSolution.ProjectId, Const.ProjectConstructSolutionMenuId, newConstructSolution.ConstructSolutionId, newConstructSolution.CompileMan, true, newConstructSolution.CompileManName, "../Solution/ConstructSolutionView.aspx?ConstructSolutionId={0}");
            }
            if (!string.IsNullOrEmpty(newItem.AttachUrl))
            {
                ////保存附件
                UploadFileService.SaveAttachUrl(UploadFileService.GetSourceByAttachUrl(newItem.AttachUrl, 10, null), newItem.AttachUrl, Const.ProjectConstructSolutionMenuId, newConstructSolution.ConstructSolutionId);
            }
            else
            {
                CommonService.DeleteAttachFileById(newConstructSolution.ConstructSolutionId);
            }
        }
        #endregion
    }
}
