using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using Model;

namespace BLL
{
    public static class ConstValue
    {

        #region 常量表下拉框
        /// <summary>
        /// 常量表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitConstValueDropDownList(FineUIPro.DropDownList dropName, string groupId, bool isShowPlease)
        {
            dropName.DataValueField = "ConstValue";
            dropName.DataTextField = "ConstText";
            dropName.DataSource = ConstValue.drpConstItemList(groupId);
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }

        /// <summary>
        /// 常量表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitConstValueRadioButtonList(FineUIPro.RadioButtonList rblName, string groupId, string selectValue)
        {
            rblName.DataValueField = "ConstValue";
            rblName.DataTextField = "ConstText";
            rblName.DataSource = ConstValue.drpConstItemList(groupId);
            rblName.DataBind();
            if (!string.IsNullOrEmpty(selectValue))
            {
                rblName.SelectedValue = selectValue;
            }
        }

        public static void InitConstNameDropDownList(FineUIPro.DropDownList dropName, string groupId, bool isShowPlease)
        {
            dropName.DataValueField = "ConstText";
            dropName.DataTextField = "ConstText";
            dropName.DataSource = ConstValue.drpConstItemList(groupId);
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }

        /// <summary>
        /// 常量表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitConstValueCheckBoxList(FineUIPro.CheckBoxList rblName, string groupId)
        {
            rblName.DataValueField = "ConstValue";
            rblName.DataTextField = "ConstText";
            rblName.DataSource = ConstValue.drpConstItemList(groupId);
            rblName.DataBind();
        }
        #endregion

        /// <summary>
        /// 获取常量下拉框 根据常量组id
        /// </summary>
        /// <param name="groupId">常量组id</param>
        /// <returns>常量集合</returns>
        public static List<Sys_Const> drpConstItemList(string groupId)
        {
            var list = (from x in Funs.DB.Sys_Const
                        where x.GroupId == groupId
                        orderby x.SortIndex
                        select x).ToList();
            return list;
        }

        /// <summary>
        /// 根据值、组ID获取常量信息
        /// </summary>
        /// <param name="constValue"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static Sys_Const GetConstByConstValueAndGroupId(string constValue, string groupId)
        {
            return Funs.DB.Sys_Const.FirstOrDefault(e => e.ConstValue == constValue && e.GroupId == groupId);
        }

        #region 根据多ID得到名称字符串
        /// <summary>
        /// 根据多ID得到名称字符串
        /// </summary>
        /// <param name="bigType"></param>
        /// <returns></returns>
        public static string getConstTextsConstValues(object constValues, string groupId)
        {
            string name = string.Empty;
            if (constValues != null)
            {
                string[] ids = constValues.ToString().Split(',');
                foreach (string id in ids)
                {
                    var q = GetConstByConstValueAndGroupId(id, groupId);
                    if (q != null)
                    {
                        name += q.ConstText + ",";
                    }
                }
                if (name != string.Empty)
                {
                    name = name.Substring(0, name.Length - 1); ;
                }
            }

            return name;
        }
        #endregion

        #region 常量组
        /// <summary>
        /// 系统环境设置 组id
        /// </summary>
        public const string Group_SysSet = "SysSet";
        /// <summary>
        /// 是/否 组id
        /// </summary>
        public const string Group_0001 = "0001";
        /// <summary>
        /// 性别：男/女 组id
        /// </summary>
        public const string Group_0002 = "0002";
        /// <summary>
        /// 婚姻状况 组id
        /// </summary>
        public const string Group_0003 = "0003";
        /// <summary>
        /// 文化程度 组id
        /// </summary>
        public const string Group_0004 = "0004";
        /// <summary>
        /// 民族 组id
        /// </summary>
        public const string Group_0005 = "0005";
        /// <summary>
        /// 辅助方法 组id
        /// </summary>
        public const string Group_0006 = "0006";
        /// <summary>
        /// 危险级别 组id
        /// </summary>
        public const string Group_0007 = "0007";
        /// <summary>
        /// 年度 组id
        /// </summary>
        public const string Group_0008 = "0008";
        /// <summary>
        /// 月份 组id
        /// </summary>
        public const string Group_0009 = "0009";
        /// <summary>
        /// 上半年/下半年 组id
        /// </summary>
        public const string Group_0010 = "0010";
        /// <summary>
        /// 季度 组id
        /// </summary>
        public const string Group_0011 = "0011";        
        /// <summary>
        /// 事故类型 组id
        /// </summary>
        public const string Group_0012 = "0012";
        /// <summary>
        /// 角色类型 组id
        /// </summary>
        public const string Group_0013 = "0013";
        /// <summary>
        /// 按钮常量 组id
        /// </summary>
        public const string Group_MenuButton = "MenuButton";
        ///// <summary>
        ///// 单位项目类型 组id
        ///// </summary>
        //public const string Group_UnitClass = "UnitClass";
        /// <summary>
        /// 项目类型：组ID
        /// </summary>
        public const string Group_ProjectType = "ProjectType";
        /// <summary>
        /// 报表类型：组ID
        /// </summary>
        public const string Group_ReportType = "ReportType";
        /// <summary>
        /// 资源上传审核状态：未审核，未通过，已通过 
        /// </summary>
        public const string Group_UploadResources = "UploadResources";        
        /// <summary>
        /// 图表类型：组id
        /// </summary>
        public const string Group_ChartType = "ChartType";        
        /// <summary>
        /// 资源上报状态：组id
        /// </summary>
        public const string Group_UpState = "UpState";
        /// <summary>
        /// 在线督查：组id
        /// </summary>
        public const string Group_sysMenu = "sysMenu";
        /// <summary>
        /// 报表设计：组id
        /// </summary>
        public const string Group_Report = "Report";
        /// <summary>
        /// 监督评价报告：组id
        /// </summary>
        public const string Group_CheckRectType = "CheckRectType";
        /// <summary>
        /// -是否可自动执行：组id
        /// </summary>
        public const string Group_Synchronization = "Synchronization";
        /// <summary>
        /// -菜单类型：组id
        /// </summary>
        public const string Group_MenuType = "MenuType";        
        /// <summary>
        /// -项目单位类型：组id
        /// </summary>
        public const string Group_ProjectUnitType = "ProjectUnitType";
        /// <summary>
        /// -标牌类型：组id
        /// </summary>
        public const string Group_SignType = "SignType";
        /// <summary>
        /// 天气 组id
        /// </summary>
        public const string Group_Weather = "Weather";
        /// <summary>
        /// 处理措施 组id
        /// </summary>
        public const string Group_HandleStep = "HandleStep";
        /// <summary>
        /// 危险源对应工作阶段 组id
        /// </summary>
        public const string Group_WorkStage = "WorkStage";
        /// <summary>
        /// 环境危险源类型 组id
        /// </summary>
        public const string Group_EnvironmentalType = "EnvironmentalType";
        /// <summary>
        /// 环境危险源小类型 组id
        /// </summary>
        public const string Group_EnvironmentalSmallType = "EnvironmentalSmallType";
        /// <summary>
        /// 危险性较大的工程清单类型 组id
        /// </summary>
        public const string Group_LargerHazardType = "LargerHazardType";
        /// <summary>
        /// 应急演练类型 组id
        /// </summary>
        public const string Group_DrillRecordType = "DrillRecordType";
        /// <summary>
        /// 伤害情况 组id
        /// </summary>
        public const string Group_Accident = "Accident";
        /// <summary>
        /// 事故调查报告类型 组id
        /// </summary>
        public const string Group_AccidentReport = "AccidentReport";
        /// <summary>
        /// 奖励/处罚 组ID
        /// </summary>
        public const string Group_RewardOrPunish = "RewardOrPunish";
        /// <summary>
        /// 违规名称 组ID
        /// </summary>
        public const string Group_ViolationName = "ViolationName";
        /// <summary>
        /// 违规类型 组ID
        /// </summary>
        public const string Group_ViolationType = "ViolationType";
        /// <summary>
        /// 违规类型（其它） 组ID
        /// </summary>
        public const string Group_ViolationTypeOther = "ViolationTypeOther";
        /// <summary>
        /// 岗位类型 组id
        /// </summary>
        public const string Group_PostType = "PostType";
        /// <summary>
        /// -人工时月报是否按平均数取值：组id
        /// </summary>
        public const string Group_IsMonthReportGetAVG = "IsMonthReportGetAVG";
        /// <summary>
        /// -是否固定流程取值：组id
        /// </summary>
        public const string Group_MenuFlowOperate = "MenuFlowOperate";
        /// <summary>
        /// -是否与博晟数据提取 ：组id
        /// </summary>
        public const string Group_ChangeData = "ChangeData";
        /// <summary>
        /// 标准规范对应HSSE方案 组ID
        /// </summary>
        public const string Group_CNProfessional = "CNProfessional";
        /// <summary>
        /// 方案审查类型 组ID
        /// </summary>
        public const string Group_InvestigateType = "InvestigateType";
        /// <summary>
        /// -管理月报冻结日期：组id
        /// </summary>
        public const string Group_MonthReportFreezeDay = "MonthReportFreezeDay";
        /// <summary>
        /// -企业安全管理资料考核类型：组id
        /// </summary>
        public const string Group_SafetyDataCheckType = "SafetyDataCheckType";
        /// <summary>
        /// -事故报告登记：组id
        /// </summary>
        public const string Group_AccidentReportRegistration = "AccidentReportRegistration";
        /// <summary>
        /// -事故调查处理报告：组id
        /// </summary>
        public const string Group_AccidentInvestigationProcessingReport = "AccidentInvestigationProcessingReport";
        /// <summary>
        /// -奖励类型：组id
        /// </summary>
        public const string Group_RewardType = "RewardType";
        /// <summary>
        /// -违规人员处理措施：组id
        /// </summary>
        public const string Group_ViolationPersonHandleStep = "ViolationPersonHandleStep";
        /// <summary>
        /// -五环管理月报内容项：组id
        /// </summary>
        public const string Group_MonthReportCItem = "MonthReportCItem";
        /// <summary>
        /// -东华管理月报内容项：组id
        /// </summary>
        public const string Group_MonthReportDItem = "MonthReportDItem";
        /// <summary>
        /// 隐患级别 组id
        /// </summary>
        public const string Group_HiddenDangerLevel = "HiddenDangerLevel";
        /// <summary>
        /// 作业票 组id
        /// </summary>
        public const string Group_LicenseType = "LicenseType";
        /// <summary>
        /// 安全数据（月报）汇总 组id
        /// </summary>
        public const string Group_HSEData = "HSEData";
        /// <summary>
        /// 本部菜单
        /// </summary>
        public const string Group_MenuType_S = "MenuType_S";
        /// <summary>
        /// 项目菜单
        /// </summary>
        public const string Group_MenuType_P = "MenuType_P";
        #endregion
    }
}