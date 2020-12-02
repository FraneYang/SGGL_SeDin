using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    /// <summary>
    /// 检查通知单
    /// </summary>
    public static class CheckNoticeService
    {
        /// <summary>
        /// 根据主键获取检查通知单
        /// </summary>
        /// <param name="checkNoticeId"></param>
        /// <returns></returns>
        public static Model.ProjectSupervision_CheckNotice GetCheckNoticeById(string checkNoticeId)
        {
            return Funs.DB.ProjectSupervision_CheckNotice.FirstOrDefault(e => e.CheckNoticeId == checkNoticeId);
        }

        /// <summary>
        /// 添加检查通知单
        /// </summary>
        /// <param name="checkNotice"></param>
        public static void AddCheckNotice(Model.ProjectSupervision_CheckNotice checkNotice)
        {
            Model.ProjectSupervision_CheckNotice newCheckNotice = new Model.ProjectSupervision_CheckNotice();
            newCheckNotice.CheckNoticeId = checkNotice.CheckNoticeId;
            //newCheckNotice.SubjectUnitId = checkNotice.SubjectUnitId;
            newCheckNotice.SubjectUnitAdd = checkNotice.SubjectUnitAdd;
            newCheckNotice.SubjectUnitMan = checkNotice.SubjectUnitMan;
            newCheckNotice.SubjectUnitTel = checkNotice.SubjectUnitTel;
            newCheckNotice.CheckStartTime = checkNotice.CheckStartTime;
            newCheckNotice.CheckEndTime = checkNotice.CheckEndTime;
            //newCheckNotice.SubjectObject = checkNotice.SubjectObject;
            newCheckNotice.CheckTeamLeader = checkNotice.CheckTeamLeader;
            newCheckNotice.CompileMan = checkNotice.CompileMan;
            newCheckNotice.CompileDate = checkNotice.CompileDate;
            newCheckNotice.CheckTeamLeaderName = checkNotice.CheckTeamLeaderName;
            newCheckNotice.UnitId = checkNotice.UnitId;
            newCheckNotice.SexName = checkNotice.SexName;
            newCheckNotice.SubjectProjectId = checkNotice.SubjectProjectId;
            Funs.DB.ProjectSupervision_CheckNotice.InsertOnSubmit(newCheckNotice);
            Funs.DB.SubmitChanges();

            ////组长不为空时 自动将组长添加到检查组
            if (!string.IsNullOrEmpty(newCheckNotice.CheckTeamLeader))
            {
                Model.ProjectSupervision_CheckTeam newCheckTeam = new Model.ProjectSupervision_CheckTeam
                {
                    CheckTeamId = SQLHelper.GetNewID(typeof(Model.ProjectSupervision_CheckTeam))
                };
                ;
                newCheckTeam.CheckNoticeId = newCheckNotice.CheckNoticeId;
                newCheckTeam.UserId = newCheckNotice.CheckTeamLeader;
                newCheckTeam.SortIndex = 1;
                newCheckTeam.CheckPostName = "组长";
                newCheckTeam.CheckDate = newCheckNotice.CheckStartTime;
                newCheckTeam.UserName = newCheckNotice.CheckTeamLeaderName;
                newCheckTeam.UnitId = newCheckNotice.UnitId;
                newCheckTeam.SexName = newCheckNotice.SexName;
                BLL.CheckTeamService.AddCheckTeam(newCheckTeam);
            }
        }

        /// <summary>
        /// 修改检查通知单
        /// </summary>
        /// <param name="checkNotice"></param>
        public static void UpdateCheckNotice(Model.ProjectSupervision_CheckNotice checkNotice)
        {
            Model.ProjectSupervision_CheckNotice newCheckNotice = Funs.DB.ProjectSupervision_CheckNotice.FirstOrDefault(e => e.CheckNoticeId == checkNotice.CheckNoticeId);
            if (newCheckNotice != null)
            {
                //newCheckNotice.SubjectUnitId = checkNotice.SubjectUnitId;
                newCheckNotice.SubjectUnitAdd = checkNotice.SubjectUnitAdd;
                newCheckNotice.SubjectUnitMan = checkNotice.SubjectUnitMan;
                newCheckNotice.SubjectUnitTel = checkNotice.SubjectUnitTel;
                newCheckNotice.CheckStartTime = checkNotice.CheckStartTime;
                newCheckNotice.CheckEndTime = checkNotice.CheckEndTime;
                //newCheckNotice.SubjectObject = checkNotice.SubjectObject;
                newCheckNotice.CheckTeamLeader = checkNotice.CheckTeamLeader;
                newCheckNotice.CompileMan = checkNotice.CompileMan;
                newCheckNotice.CompileDate = checkNotice.CompileDate;
                newCheckNotice.CheckTeamLeaderName = checkNotice.CheckTeamLeaderName;
                newCheckNotice.UnitId = checkNotice.UnitId;
                newCheckNotice.SexName = checkNotice.SexName;
                newCheckNotice.SubjectProjectId = checkNotice.SubjectProjectId;
                Funs.DB.SubmitChanges();                
            }
        }

        /// <summary>
        /// 根据检查通知Id删除检查通知信息
        /// </summary>
        /// <param name="checkNoticeId"></param>
        public static void DeleteCheckNoticeByCheckNoticeId(string checkNoticeId)
        {
            Model.ProjectSupervision_CheckNotice checkNotice = Funs.DB.ProjectSupervision_CheckNotice.FirstOrDefault(e => e.CheckNoticeId == checkNoticeId);
            if (checkNotice != null)
            {
                //var checkFiles = from x in db.Check_CheckInfo_CheckFile where x.CheckInfoId == checkInfoId select x;
                //if (checkFiles.Count() > 0)
                //{
                //    foreach (var item in checkFiles)
                //    {
                //        BLL.CheckFileService.DeleteCheckFileByCheckFileId(item.CheckFileId);
                //    }
                //}
                var checkTeams = from x in Funs.DB.ProjectSupervision_CheckTeam where x.CheckNoticeId == checkNoticeId select x;
                if (checkTeams.Count() > 0)
                {
                    foreach (var item in checkTeams)
                    {
                        BLL.CheckTeamService.DeleteCheckTeamByCheckTeamId(item.CheckTeamId);
                    }
                }
                Funs.DB.ProjectSupervision_CheckNotice.DeleteOnSubmit(checkNotice);
                Funs.DB.SubmitChanges();
            }
        }

        /// <summary>
        /// 获取监督检查信息
        /// </summary>
        /// <param name="CheckInfoName"></param>
        /// <returns></returns>
        public static List<Model.ProjectSupervision_CheckNotice> GetCheckInfoList(string unitId, string userId, string roleId)
        {
            List<Model.ProjectSupervision_CheckNotice> checkInfoLists = new List<Model.ProjectSupervision_CheckNotice>();
            var role = BLL.RoleService.GetRoleByRoleId(roleId);
            if (userId == BLL.Const.sysglyId || (BLL.CommonService.IsMainUnitOrAdmin(unitId) && role != null))
            {
                checkInfoLists = (from x in Funs.DB.ProjectSupervision_CheckNotice orderby x.CheckStartTime select x).ToList();
            }
            else
            {
                var sysUser = BLL.UserService.GetUserByUserId(userId);
                if (sysUser != null)
                {
                    var checkInfoIdList = (from x in Funs.DB.ProjectSupervision_CheckTeam
                                           where x.UserName == sysUser.UserName && x.UnitId == sysUser.UnitId
                                           select x.CheckNoticeId).Distinct().ToList();
                    if (checkInfoIdList.Count() > 0)
                    {
                        checkInfoLists = (from x in Funs.DB.ProjectSupervision_CheckNotice
                                          where checkInfoIdList.Contains(x.CheckNoticeId)
                                          orderby x.CheckStartTime
                                          select x).ToList();
                    }

                    var checkInfoIdList1 = (from x in Funs.DB.ProjectSupervision_CheckNotice
                                            where x.CompileMan == userId
                                            orderby x.CheckStartTime
                                            select x).ToList();
                    if (checkInfoIdList1.Count() > 0)
                    {
                        if (checkInfoLists.Count() > 0)
                        {
                            checkInfoLists.AddRange(checkInfoIdList1);
                        }
                        else
                        {
                            checkInfoLists = checkInfoIdList1;
                        }
                    }

                    checkInfoLists = checkInfoLists.Distinct().ToList();
                }
            }
            return checkInfoLists;
        }

        #region 根据检查ID得到检查内容项
        /// <summary>
        /// 根据检查ID得到检查内容项
        /// </summary>
        /// <param name="checkInfoId"></param>
        /// <returns></returns>
        public static List<Model.SpCheckInfoItem> GetSpCheckInfoItemByCheckInfoId(string checkInfoId)
        {
            List<Model.SpCheckInfoItem> spCheckInfoItems = new List<Model.SpCheckInfoItem>();
            //var checkItems = BLL.ConstValue.drpConstItemList(BLL.ConstValue.Group_CheckItem);
            //if (checkItems.Count() > 0)
            //{
            //    foreach (var item in checkItems)
            //    {
                    //Model.SpCheckInfoItem spCheckInfoItem = new Model.SpCheckInfoItem
                    //{
                    //    ID = item.ID,
                    //    SortIndex = item.SortIndex,
                    //    CheckItemName = item.ConstText
                    //};
                    //if (item.ConstValue == "1")
                    //{
                    //    var table1 = Funs.DB.Check_CheckInfo_Table1.FirstOrDefault(x => x.CheckInfoId == checkInfoId);
                    //    if (table1 != null)
                    //    {
                    //        spCheckInfoItem.CheckItemId = table1.CheckItemId;
                    //        var units = BLL.UnitService.GetUnitById(table1.SubjectUnitId);
                    //        if (units != null)
                    //        {
                    //            spCheckInfoItem.CheckObject = "企业名称：" + units.UnitName;
                    //        }
                    //        spCheckInfoItem.CheckDate = table1.CheckDate;
                    //        spCheckInfoItem.CheckResult = "评定得分：" + table1.TotalLastScore.ToString() + "；评定结论：" + table1.EvaluationResult;
                    //        spCheckInfoItems.Add(spCheckInfoItem);
                    //    }
                    //}
                    //if (item.ConstValue == "2")
                    //{
                    //    var table2 = Funs.DB.Check_CheckInfo_Table2.FirstOrDefault(x => x.CheckInfoId == checkInfoId);
                    //    if (table2 != null)
                    //    {
                    //        spCheckInfoItem.CheckItemId = table2.CheckItemId;
                    //        var units = BLL.UnitService.GetUnitById(table2.SubjectUnitId);
                    //        if (units != null)
                    //        {
                    //            spCheckInfoItem.CheckObject = "企业名称：" + units.UnitName;
                    //        }
                    //        spCheckInfoItem.CheckDate = table2.CheckDate;
                    //        spCheckInfoItem.CheckResult = "评定得分：" + table2.TotalLastScore.ToString() + "；评定结论：" + table2.EvaluationResult;
                    //        spCheckInfoItems.Add(spCheckInfoItem);
                    //    }
                    //}
                    //if (item.ConstValue == "3")
                    //{
                    //    var table3 = Funs.DB.Check_CheckInfo_Table3.FirstOrDefault(x => x.CheckInfoId == checkInfoId);
                    //    if (table3 != null)
                    //    {
                    //        spCheckInfoItem.CheckItemId = table3.CheckItemId;
                    //        spCheckInfoItem.CheckObject = "项目经理部：" + table3.ProjectManagerName + "；项目名称：" + table3.ProjectName;
                    //        spCheckInfoItem.CheckDate = table3.CheckDate;
                    //        spCheckInfoItem.CheckResult = "评定得分：" + table3.TotalLastScore.ToString() + "；评定结论：" + table3.EvaluationResult;
                    //        spCheckInfoItems.Add(spCheckInfoItem);
                    //    }
                    //}
                    //if (item.ConstValue == "4")
                    //{
                    //    var table4 = Funs.DB.Check_CheckInfo_Table4.FirstOrDefault(x => x.CheckInfoId == checkInfoId);
                    //    if (table4 != null)
                    //    {
                    //        spCheckInfoItem.CheckItemId = table4.CheckItemId;
                    //        spCheckInfoItem.CheckObject = "项目经理部：" + table4.ProjectManagerName + "；项目名称：" + table4.ProjectName;
                    //        spCheckInfoItem.CheckDate = table4.CheckDate;
                    //        spCheckInfoItem.CheckResult = "评定得分：" + table4.TotalLastScore.ToString() + "；评定结论：" + table4.EvaluationResult;
                    //        spCheckInfoItems.Add(spCheckInfoItem);
                    //    }
                    //}
                    //if (item.ConstValue == "5")
                    //{
                    //    var table5 = Funs.DB.Check_CheckInfo_Table5.FirstOrDefault(x => x.CheckInfoId == checkInfoId);
                    //    if (table5 != null)
                    //    {
                    //        spCheckInfoItem.CheckItemId = table5.CheckItemId;
                    //        var units = BLL.UnitService.GetUnitById(table5.SubjectUnitId);
                    //        if (units != null)
                    //        {
                    //            spCheckInfoItem.CheckObject = "受检单位：" + units.UnitName;
                    //        }
                    //        spCheckInfoItem.CheckDate = table5.CheckDate;
                    //        var table5Item = (from x in Funs.DB.Check_CheckInfo_Table5Item where x.CheckInfoId == checkInfoId select x);
                    //        var isTable5Item = table5Item.Where(x => x.IsProject == true);
                    //        spCheckInfoItem.CheckResult = "隐患： " + table5Item.Count().ToString() + "条。立项整改： " + isTable5Item.Count().ToString() + "条。";
                    //        if (table5.IsIssued == true)
                    //        {
                    //            spCheckInfoItem.CheckResult += "已生成整改单。";
                    //        }
                    //        else
                    //        {
                    //            spCheckInfoItem.CheckResult += "整改单未生成或未发布。";
                    //        }
                    //        spCheckInfoItems.Add(spCheckInfoItem);
                    //    }
                    //}
                    //if (item.ConstValue == "6")
                    //{
                    //    var table6 = Funs.DB.Check_CheckInfo_Table6.FirstOrDefault(x => x.CheckInfoId == checkInfoId);
                    //    if (table6 != null)
                    //    {
                    //        spCheckInfoItem.CheckItemId = table6.CheckItemId;
                    //        var units = BLL.UnitService.GetUnitById(table6.SubjectUnitId);
                    //        if (units != null)
                    //        {
                    //            spCheckInfoItem.CheckObject = "受检单位：" + units.UnitName;
                    //        }
                    //        spCheckInfoItem.CheckDate = table6.CheckDate;
                    //        if (table6.TotalDeletScore.HasValue)
                    //        {
                    //            spCheckInfoItem.CheckResult = "罚分： " + table6.TotalDeletScore.ToString() + "；罚金：" + (table6.TotalDeletScore * 500).ToString() + "元。";
                    //        }
                    //        spCheckInfoItems.Add(spCheckInfoItem);
                    //    }
                    //}
                    //if (item.ConstValue == "7")
                    //{
                    //    var table7 = Funs.DB.Check_CheckInfo_Table7.FirstOrDefault(x => x.CheckInfoId == checkInfoId);
                    //    if (table7 != null)
                    //    {
                    //        spCheckInfoItem.CheckItemId = table7.CheckItemId;
                    //        var units = BLL.UnitService.GetUnitById(table7.SubjectUnitId);
                    //        if (units != null)
                    //        {
                    //            spCheckInfoItem.CheckObject = "受检单位：" + units.UnitName;
                    //        }
                    //        spCheckInfoItem.CheckDate = table7.CheckDate;
                    //        if (table7.TotalDeletScore.HasValue)
                    //        {
                    //            spCheckInfoItem.CheckResult = "罚分： " + table7.TotalDeletScore.ToString() + "；罚金：" + (table7.TotalDeletScore * 500).ToString() + "元。";
                    //        }
                    //        spCheckInfoItems.Add(spCheckInfoItem);
                    //    }
                    //}
                    //if (item.ConstValue == "8")
                    //{
                    //    var table8 = Funs.DB.Check_CheckInfo_Table8.FirstOrDefault(x => x.CheckInfoId == checkInfoId);
                    //    if (table8 != null)
                    //    {
                    //        var info = BLL.CheckInfoService.GetCheckInfoByCheckInfoId(checkInfoId);
                    //        if (info != null)
                    //        {
                    //            spCheckInfoItem.CheckItemId = table8.CheckItemId;
                    //            var units = BLL.UnitService.GetUnitById(info.SubjectUnitId);
                    //            if (units != null)
                    //            {
                    //                spCheckInfoItem.CheckObject = "受检单位：" + units.UnitName;
                    //            }
                    //            spCheckInfoItem.CheckDate = info.CheckStartTime;
                    //            spCheckInfoItem.CheckResult = table8.Values7;
                    //            spCheckInfoItems.Add(spCheckInfoItem);
                    //        }
                    //    }
                    //}
            //    }
            //}
            return spCheckInfoItems;
        }
        #endregion
    }
}
