using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public static class Const
    {        
        #region 查询字段：系统设置
        /// <summary>
        /// 系统管理员ID
        /// </summary>
        public const string sysglyId = "AF17168B-87BD-4GLY-1111-F0A0A1158F9B";

        /// <summary>
        /// 系统管理员ID
        /// </summary>
        public const string hfnbdId = "C4A62EC0-E5D3-4EBF-A5FA-E56AA89633C0";

        /// <summary>
        /// 公共临时账户ID
        /// </summary>
        public const string sedinId = "A8547452-751F-41B7-8E4B-5862EA4E9411";
        
        /// <summary>
        /// null 字符串
        /// </summary>
        public const string _Null = "null";

        /// <summary>
        /// all 字符串
        /// </summary>
        public const string _ALL = "all";

        /// <summary>
        /// 是 字符串
        /// </summary>
        public const string _True = "True";

        /// <summary>
        ///  否 字符串
        /// </summary>
        public const string _False = "False";

        /// <summary>
        /// 默认用户密码
        /// </summary>
        public const string Password = "123";

        /// <summary>
        /// 入场培训类型ID
        /// </summary>
        public const string EntryTrainTypeId = "8920c9cc-fa92-49b2-9493-775a55da27bb";
        
        /// <summary>
        /// 微信订阅模板ID
        /// </summary>
        public const string WX_TemplateID = "hKFrcahyXEjGSrLM4qBPpjXAkFldnrqxU_mARd1IF4o";
        #endregion

        #region 按钮描述
        /// <summary>
        /// 添加
        /// </summary>
        public const string BtnAdd = "增加";
        /// <summary>
        /// 修改
        /// </summary>
        public const string BtnModify = "修改";
        /// <summary>
        /// 删除
        /// </summary>
        public const string BtnDelete = "删除";
        /// <summary>
        /// 保存
        /// </summary>
        public const string BtnSave = "保存";
        /// <summary>
        /// 提交
        /// </summary>
        public const string BtnSubmit = "提交";

        /// <summary>
        /// 查看
        /// </summary>
        public const string BtnSee = "查看";

        /// <summary>
        /// 保存并上报
        /// </summary>
        public const string BtnSaveUp = "保存并上报";
        /// <summary>
        /// 打印
        /// </summary>
        public const string BtnPrint = "打印";
        /// <summary>
        /// 上传资源
        /// </summary>
        public const string BtnUploadResources = "上传资源";
        /// <summary>
        /// 下载
        /// </summary>
        public const string BtnDownload = "下载";
        /// <summary>
        /// 选择
        /// </summary>
        public const string BtnSelect = "选择";
        /// <summary>
        /// 审核
        /// </summary>
        public const string BtnAuditing = "审核";
        /// <summary>
        /// 导入
        /// </summary>
        public const string BtnIn = "导入";
        /// <summary>
        /// 导出
        /// </summary>
        public const string BtnOut = "导出";
        /// <summary>
        /// 统计分析
        /// </summary>
        public const string BtnAnalyse = "统计";
        /// <summary>
        /// 数据同步
        /// </summary>
        public const string BtnSyn = "同步";
        /// <summary>
        /// 发布
        /// </summary>
        public const string BtnIssuance = "发布";
        /// <summary>
        /// 确认
        /// </summary>
        public const string BtnConfirm = "确认";
        /// <summary>
        /// 提示
        /// </summary>
        public const string BtnToolTip = "提示";
        /// <summary>
        /// 响应
        /// </summary>
        public const string BtnResponse = "响应";
        /// <summary>
        /// 发卡
        /// </summary>
        public const string BtnSendCard = "发卡";
        /// <summary>
        /// 发卡
        /// </summary>
        public const string BtnQuery = "查询";
        /// <summary>
        /// 登录
        /// </summary>
        public const string BtnLogin = "登录";
        /// <summary>
        /// 生成委托单
        /// </summary>
        public const string BtnGenerate = "生成";
        /// <summary>
        /// 审核
        /// </summary>
        public const string BtnPointAudit = "点口审核";
        /// <summary>
        /// 打开重新点口 
        /// </summary>
        public const string BtnOpenResetPoint = "打开重新点口";
        /// <summary>
        /// 生成返修通知单
        /// </summary>
        public const string BtnRepairNotice = "生成返修通知单";
        /// <summary>
        /// 取消审核
        /// </summary>
        public const string BtnCancelAuditing = "取消审核";
        /// <summary>
        /// 试压包完成
        /// </summary>
        public const string BtnTestComplete = "试压包完成";
        #endregion

        #region 定义常量
        /// <summary>
        /// 赛鼎单位id
        /// </summary>
        public const string UnitId_SEDIN = "d72a27c9-4ba9-41c5-ab0b-c010409f20f2";
        /// <summary>
        /// 赛鼎
        /// </summary>
        public const string AppID_SEDIN = "wxee9d14366730ae44";
        /// <summary>
        /// 赛鼎
        /// </summary>
        public const string AppSecret_SEDIN = "8a83ad8658a3bb717191b77827a925e5";

        #region 项目状态
        /// <summary>
        /// 施工中
        /// </summary>
        public const string ProjectState_1 = "1";
        /// <summary>
        /// 暂停中
        /// </summary>
        public const string ProjectState_2 = "2";
        /// <summary>
        /// 已完工
        /// </summary>
        public const string ProjectState_3 = "3";
        #endregion
       
        #region 项目单位类型
        /// <summary>
        /// 总包
        /// </summary>
        public const string ProjectUnitType_1 = "1";
        /// <summary>
        /// 施工分包
        /// </summary>
        public const string ProjectUnitType_2 = "2";
        /// <summary>
        /// 监理
        /// </summary>
        public const string ProjectUnitType_3 = "3"; 
        /// <summary>
        /// 业主
        /// </summary>
        public const string ProjectUnitType_4 = "4"; 
        /// <summary>
        /// 检测单位
        /// </summary>
        public const string ProjectUnitType_5 = "5";
        /// <summary>
        /// 其他
        /// </summary>
        public const string ProjectUnitType_0 = "0";
        #endregion

        #region 岗位类型
        /// <summary>
        /// 一般管理岗位
        /// </summary>
        public const string PostType_1 = "1";
        /// <summary>
        /// 特种作业人员
        /// </summary>
        public const string PostType_2 = "2";
        /// <summary>
        /// 一般作业岗位
        /// </summary>
        public const string PostType_3 = "3";
        /// <summary>
        /// 特种管理人员
        /// </summary>
        public const string PostType_4 = "4";
        /// <summary>
        /// 特种设备作业人员
        /// </summary>
        public const string PostType_5 = "5";
        #endregion

        #region 内置岗位
        /// <summary>
        /// HSE工程师岗位Id
        /// </summary>
        public static string WorkPost_HSSEEngineer = "9b15af2e-6131-462c-9f8a-71533957ca0e";       

        /// <summary>
        /// HSE经理岗位Id
        /// </summary>
        public static string WorkPost_SafetyManager = "e426e560-9906-4aba-843e-2daef9ebeb05";

        /// <summary>
        /// 焊工岗位Id
        /// </summary>
        public static string WorkPost_Welder = "19B8F2A9-28D3-4F20-867A-1B2237C2E228";

        /// <summary>
        /// 无损检测工岗位Id
        /// </summary>
        public static string WorkPost_Checker = "595999E9-75A8-4064-9BD1-9EE02F236297";
        #endregion

        #region 系统环境设置常量
        /// <summary>
        /// 月报上报时间
        /// </summary>
        public const string MonthReprotDate = "MonthReprotDate";
        /// <summary>
        /// 季报上报时间 第一季月
        /// </summary>
        public const string QuarterReprotMonth_1 = "QuarterReprotMonth_1";
        /// <summary>
        /// 季报上报时间 第一季日
        /// </summary>
        public const string QuarterReprotDate_1 = "QuarterReprotDate_1";
        /// <summary>
        /// 季报上报时间 第二季月
        /// </summary>
        public const string QuarterReprotMonth_2 = "QuarterReprotMonth_2";
        /// <summary>
        /// 季报上报时间 第二季日
        /// </summary>
        public const string QuarterReprotDate_2 = "QuarterReprotDate_2";
        /// <summary>
        /// 季报上报时间 第三季月
        /// </summary>
        public const string QuarterReprotMonth_3 = "QuarterReprotMonth_3";
        /// <summary>
        /// 季报上报时间 第三季日
        /// </summary>
        public const string QuarterReprotDate_3 = "QuarterReprotDate_3";
        /// <summary>
        /// 季报上报时间 第四季月
        /// </summary>
        public const string QuarterReprotMonth_4 = "QuarterReprotMonth_4";
        /// <summary>
        /// 季报上报时间 第四季日
        /// </summary>
        public const string QuarterReprotDate_4 = "QuarterReprotDate_4";
        /// <summary>
        /// 半年上报时间 上半年月
        /// </summary>
        public const string HalfYearMonth_1 = "HalfYearMonth_1";
        /// <summary>
        /// 半年上报时间 上半年日
        /// </summary>
        public const string HalfYearDate_1 = "HalfYearDate_1";
        /// <summary>
        /// 半年上报时间 下半年月
        /// </summary>
        public const string HalfYearMonth_2 = "HalfYearMonth_2";
        /// <summary>
        /// 半年上报时间 下半年日
        /// </summary>
        public const string HalfYearDate_2 = "HalfYearDate_2";
        #endregion              
        
        #region 内置项目角色定义
        /// <summary>
        /// 项目经理
        /// </summary>
        public const string ProjectManager = "1184835B-73AF-47FB-9F83-20740CE2FAD7";
        /// <summary>
        /// 项目副经理
        /// </summary>
        public const string ProjectAssistantManager = "A184835B-73AF-ERHM-9F83-20740CE2FAD7";
        /// <summary>
        /// 质量经理
        /// </summary>
        public const string QAManager = "GCB64EF3-AB0A-40BC-824D-CC314598D5DC";
        /// <summary>
        /// 施工经理
        /// </summary>
        public const string ConstructionManager = "22F78A47-F59C-4FE8-9C43-2DD304CB2108";
        /// <summary>
        /// 施工副经理
        /// </summary>
        public const string ConstructionAssistantManager = "AAE579CF-A249-4336-BAFE-7FB4D5753451";
        /// <summary>
        /// 项目HSSE经理
        /// </summary>
        public const string HSSEManager = "3314753D-E295-4D87-B938-E8C8EF5C17BC";
        /// <summary>
        /// 控制经理
        /// </summary>
        public const string ControlManager = "UTD74411-D95E-4CB3-B6D1-F41B4F4FB268";
        /// <summary>
        /// 控制副经理
        /// </summary>
        public const string ControlAssistantManager = "65453141-716D-44F5-922F-80601AC1C219";
        /// <summary>
        /// 设计经理
        /// </summary>
        public const string DesignManager = "9BED9186-1FB1-4711-963F-E90D6EC1D629";
        /// <summary>
        /// 设计副经理
        /// </summary>
        public const string DesignAssistantManager = "3D2E1BC5-CE1C-4BE3-9A3E-0FBC3738729F";
        /// <summary>
        /// 采购经理
        /// </summary>
        public const string PurchasingManager = "DBEB0B79-0713-495C-8BD9-8C4330D1178A";
        /// <summary>
        /// 采购副经理
        /// </summary>
        public const string PurchasingAssistantManager = "23D656A6-F887-4DBB-9DB6-BBC2887DD86C";
        /// <summary>
        /// 开车经理
        /// </summary>
        public const string DriveManager = "TA3737E4-3677-4B7C-B6F6-D546CC2D83E0";
        /// <summary>
        /// HSSE工程师
        /// </summary>
        public const string HSSEEngineer = "66D2ECB4-2946-4C6E-8E4B-1C4CD096B8A5";
        /// <summary>
        /// 执行经理
        /// </summary>
        public static string ZXPrincipalRole = "52F3101F-A191-43C3-B2FD-E26ED31E2B71";
        /// <summary>
        /// 项目技术负责人
        /// </summary>
        public static string TechnicalPrincipalRole = "BGFBHRRE-0813-6666-828E-FDSAFDSFVDDF";
        /// <summary>
        /// 费控工程师
        /// </summary>
        public static string KZEngineer = "CADC4D2C-E101-44E0-978A-9AD86C3B5F48";
        /// <summary>
        /// 质量工程师
        /// </summary>
        public static string CQEngineer = "eb2646ae-36dd-4199-a75e-55a6f42935ad";
        /// <summary>
        /// 土建工程师
        /// </summary>
        public static string CVEngineer = "012B3348-0813-46C0-828E-41CB6457FACD";
        /// <summary>
        /// 设备工程师
        /// </summary>
        public static string FEEngineer = "FAFDSFDD-5555-46C0-828E-9OPYTT54TRET";
        /// <summary>
        /// 管道工程师
        /// </summary>
        public static string PDEngineer = "VBFSDVFF-0813-46C0-7777-54GTRT56HRRE";
        /// <summary>
        /// 电气工程师
        /// </summary>
        public static string EHEngineer = "DVDASFDE-0813-46C0-828E-VBFDBGDFEWER";
        /// <summary>
        /// 仪表工程师
        /// </summary>
        public static string EAEngineer = "AAE5734F-A249-4336-BAFE-7FB4D5753451";
        /// <summary>
        /// 焊接工程师
        /// </summary>
        public static string HJEngineer = "17F2C0B6-5505-46FA-AB34-54CEECD5CF66";
        #endregion

        #region 报表流程定义
        /// <summary>
        /// 编制中
        /// </summary>
        public const string HandleState_1 = "1";

        /// <summary>
        /// 审核中
        /// </summary>
        public const string HandleState_2 = "2";

        /// <summary>
        /// 审批中
        /// </summary>
        public const string HandleState_3 = "3";

        /// <summary>
        /// 审批完成
        /// </summary>
        public const string HandleState_4 = "4";
        #endregion

        #region 危险观察流程定义
        /// <summary>
        /// 待整改
        /// </summary>
        public const string RegistrationState_0 = "0";
        /// <summary>
        /// 已整改
        /// </summary>
        public const string RegistrationState_1 = "1";
        /// <summary>
        /// 已闭环
        /// </summary>
        public const string RegistrationState_2 = "2";
        /// <summary>
        /// 重新整改
        /// </summary>
        public const string RegistrationState_3 = "3";
        #endregion

        #region 菜单模块类型常量
        /*本部*/
        /// <summary>
        /// 系统设置菜单
        /// </summary>
        public const string Menu_SysSet = "Menu_SysSet";
        /// <summary>
        /// 我的菜单
        /// </summary>
        public const string Menu_Personal = "Menu_Personal";
        /// <summary>
        /// 员工菜单
        /// </summary>
        public const string Menu_Person = "Menu_Person";
        /// <summary>
        /// 通知
        /// </summary>
        public const string Menu_Notice = "Menu_Notice";
        /// <summary>
        /// 项目菜单
        /// </summary>
        public const string Menu_Project = "Menu_Project";
        /// <summary>
        /// 本部检查菜单
        /// </summary>
        public const string Menu_Server = "Menu_Server";
        /// <summary>
        /// 合同菜单
        /// </summary>
        public const string Menu_Contract = "Menu_HTGL";
        /// <summary>
        /// 大数据
        /// </summary>
        public const string Menu_DigData = "Menu_DigData";
        /// <summary>
        /// 综合管理菜单
        /// </summary>
        public const string Menu_ZHGL = "Menu_ZHGL";
        /// <summary>
        /// 党建群团
        /// </summary>
        public const string Menu_Party = "Menu_Party";
        
        /*项目*/
        /// <summary>
        /// 项目设置
        /// </summary>
        public const string Menu_ProjectSet = "Menu_ProjectSet";
        /// <summary>
        /// 质量
        /// </summary>
        public const string Menu_CQMS = "Menu_CQMS";
        /// <summary>
        /// 安全
        /// </summary>
        public const string Menu_HSSE = "Menu_HSSE";
        /// <summary>
        /// 进度
        /// </summary>
        public const string Menu_JDGL = "Menu_JDGL";
        /// <summary>
        /// 焊接管理
        /// </summary>
        public const string Menu_HJGL = "Menu_HJGL";
        /// <summary>
        /// 项目合同
        /// </summary>
        public const string Menu_PHTGL = "Menu_PHTGL";
        /// <summary>
        /// 试车管理
        /// </summary>
        public const string Menu_TestRun = "Menu_TestRun";
        /// <summary>
        /// 施工综合
        /// </summary>
        public const string Menu_PZHGL = "Menu_PZHGL";
        /// <summary>
        /// 数字工地
        /// </summary>
        public const string Menu_DigitalSite = "Menu_DigitalSite";
        /// <summary>
        /// 项目大数据
        /// </summary>
        public const string Menu_PDigData = "Menu_PDigData";
        #endregion
        #endregion

        #region 菜单id
        #region 系统设置
        /// <summary>
        /// 单位设置
        /// </summary>
        public const string UnitMenuId = "8IDKGJE2-09B1-4607-BC6D-865CE48F0002";

        /// <summary>
        /// 角色管理
        /// </summary>
        public const string RoleMenuId = "EBAD373C-8EB4-49A1-91F6-6794FFCAF9B6";

        /// <summary>
        /// 用户
        /// </summary>
        public const string UserMenuId = "E6F0167E-B0FD-4A32-9C47-25FB9E0FDC4E";

        /// <summary>
        /// 角色授权
        /// </summary>
        public const string RolePowerMenuId = "2231022B-3519-42FC-A2E6-1DB9A98039DD";

        /// <summary>
        /// 报表设计
        /// </summary>
        public const string PrintDesignerMenuId = "0C67F4A8-1BE7-40BE-9621-B02A28FD13ED";

        /// <summary>
        /// 环境设置
        /// </summary>
        public const string SysConstSetMenuId = "E4BFDCFD-2B1F-49C5-B02B-1C91BFFAAC6E";

        /// <summary>
        /// 数据同步
        /// </summary>
        public const string SynchronizationMenuId = "6EDFBE24-9419-4E73-AC2E-CAD30A754A73";
        #endregion

        #region 基础信息
        /// <summary>
        /// 单位类别
        /// </summary>
        public const string UnitTypeMenuId = "685F1E0D-987E-491C-9DC7-014098DEE0C3";
        /// <summary>
        /// 部门信息
        /// </summary>
        public const string DepartTypeMenuId = "D9F1F0BD-D48F-4C5B-AF26-7F4C561D1352";
        /// <summary>
        /// 岗位信息
        /// </summary>
        public const string WorkPostMenuId = "D4FC3583-31A7-49B3-8F32-007E9756D678";
        /// <summary>
        /// 职务信息
        /// </summary>
        public const string PositionMenuId = "1E81DF97-809E-479F-1111-508F2043BA69";
        /// <summary>
        /// 职称信息
        /// </summary>
        public const string PostTitleMenuId = "2E424093-81B8-421A-963F-D85D17B1E82A";
        /// <summary>
        /// 特岗证书
        /// </summary>
        public const string CertificateMenuId = "3A40AF0B-C9B8-4AF9-A683-FEADD8CC3A1C";
        /// <summary>
        /// 机具设备
        /// </summary>
        public const string SpecialEquipmentMenuId = "B7926494-8CCD-40BD-1111-6BD10176DA0D";
        /// <summary>
        /// 费用类型
        /// </summary>
        public const string CostTypeMenuId = "000F8328-AB84-4235-95F8-E826DB292D83";
        /// <summary>
        /// 事故类型
        /// </summary>
        public const string AccidentTypeMenuId = "F6C0E3DE-84CB-45CE-A46A-84DD51B8FBCB";
        /// <summary>
        /// 货物类型
        /// </summary>
        public const string GoodsTypeMenuId = "87F312AD-8611-49F9-8487-66B5240568E0";
        /// <summary>
        /// 气瓶类型
        /// </summary>
        public const string GasCylinderMenuId = "9922D495-76ED-4951-AC61-7559C00597DF";
        /// <summary>
        /// 违章种类
        /// </summary>
        public const string ViolationSortMenuId = "B3FF303D-1A3E-40A3-8E9C-DDE4F32FFA91";
        /// <summary>
        /// 许可证类型
        /// </summary>
        public const string LicenseTypeMenuId = "4C86AEA0-5D8A-4677-8F53-D1C757141CD3";
        /// <summary>
        /// 专业信息
        /// </summary>
        public const string PersonSpecialtyMenuId = "724E082C-EDDA-40FF-B396-F5441C5F6B15";
        /// <summary>
        /// 专家类别
        /// </summary>
        public const string ExpertTypeMenuId = "45EBF358-F03D-4A85-848C-0C59766D3CC8";
        /// <summary>
        /// 法律法规类型
        /// </summary>
        public const string LawsRegulationsTypeMenuId = "871A4DA2-2CE0-4049-8255-3759A269110E";
        /// <summary>
        /// 标准规范类型
        /// </summary>
        public const string HSSEStandardListTypeMenuId = "ACA1906A-4A68-4632-A13A-342DECE32D3E";
        /// <summary>
        /// 管理规定分类
        /// </summary>
        public const string ManageRuleTypeMenuId = "5DAADE2B-C2D2-4765-A47A-A1C3139775DC";
        /// <summary>
        /// 规章制度类别
        /// </summary>
        public const string RulesRegulationsTypeMenuId = "F7B600D2-999C-4C60-96D4-B7CB7129C0B5";
        /// <summary>
        /// 应急预案类型
        /// </summary>
        public const string EmergencyTypeMenuId = "CD5D9B58-4313-4463-BAE6-481A0D3775D9";
        /// <summary>
        ///专项方案类别
        /// </summary>
        public const string SpecialSchemeTypeMenuId = "2181FB42-22F2-4342-B604-96F5770FE892";
        /// <summary>
        /// 交流话题类型
        /// </summary>
        public const string ContentTypeMenuId = "F416839E-A37B-4DA0-AA50-8B06EBEF3139";
        /// <summary>
        /// 物资类别
        /// </summary>
        public const string GoodsCategoryMenuId = "CCF9E615-78C9-4085-BCFE-0F9907D3FA0A";
        /// <summary>
        /// 物资名称
        /// </summary>
        public const string GoodsDefMenuId = "7C07BCB4-5D51-4061-BB71-60AD25F6F21D";
        /// <summary>
        /// 培训类别
        /// </summary>
        public const string TrainTypeMenuId = "091322A8-4FFB-472C-91EB-ABB3E491262A";
        /// <summary>
        /// 培训级别
        /// </summary>
        public const string TrainLevelMenuId = "B27A0196-06A0-412D-B734-FB348AA6E312";
        /// <summary>
        /// 综合应急预案模板类型
        /// </summary>
        public static string ComplexEmergencyTypeId = "b96f6467-95a0-459f-9582-5d9fd55bd7c9";
        /// <summary>
        /// 工作阶段
        /// </summary>
        public static string WorkStageMenuId = "38143A01-D17C-4234-A1A1-D90A3F887438";
        /// <summary>
        /// 施工方案类型
        /// </summary>
        public const string SolutionTempleteTypeMenuId = "179F286B-6DF8-414E-947F-82267076D4C8";
        /// <summary>
        /// 安全巡检类型
        /// </summary>
        public const string HazardRegisterTypesMenuId = "3EC2676A-70EB-400E-BE17-EEBBA0B7E9D7";
        /// <summary>
        /// 领导督察类型
        /// </summary>
        public const string HazardRegisterTypesSupervisionMenuId = "60BFBA33-B5C7-4EBF-A4AE-1B820559676D";
        /// <summary>
        /// 处罚项信息
        /// </summary>
        public const string PunishItemMenuId = "015F94F0-F2EB-403F-B8CA-FF44AB9BDA5B";
        /// <summary>
        /// 专项检查类型
        /// </summary>
        public const string SpecialCheckTypesMenuId = "7AC97581-08C2-4848-A117-BDC034EF6666";
        /// <summary>
        /// 安全措施
        /// </summary>
        public const string SafetyMeasuresMenuId = "4BB6AC40-D4D5-439B-85CC-100E431CF3F1";
        /// <summary>
        /// 项目图片分类
        /// </summary>
        public const string PictureTypeMenuId = "D95CA72C-182C-4718-92A4-3F665E0FB660";
        #endregion

        #region 质量基础信息
        /// <summary>
        /// 施工专业定义
        /// </summary>
        public const string CNProfessionalMenuId = "D00A978A-DDEE-4BFF-8872-FFF65A7BC0BC";

        /// <summary>
        /// 设计专业定义
        /// </summary>
        public const string DesignProfessionalMenuId = "BADDFC6A-F0E2-4443-A98D-12439C4A2F16";

        /// <summary>
        /// 质量问题类别定义
        /// </summary>
        public const string QualityQuestionTypeMenuId = "24F9A1ED-0F4C-407C-8EB3-2A8711BB6ECC";
        #endregion

        #region 项目设置
        /// <summary>
        /// 项目信息
        /// </summary>
        public const string ProjectSetMenuId = "0C3386D2-8F86-40AC-94F6-EE0100324FDD";
        /// <summary>
        /// 项目主项
        /// </summary>
        public const string MainItemMenuId = "336FE0BB-AE1D-4F15-A35B-1FC582E01A14";
        /// <summary>
        /// 项目状态及软件关闭
        /// </summary>
        public const string ProjectShutdownMenuId = "D24ACD3C-086C-4AC8-9AFA-16D48893215E";
        /// <summary>
        /// 项目单位
        /// </summary>
        public const string ProjectUnitMenuId = "64690369-3049-4009-82EE-437DF2E606BA";
        /// <summary>
        /// 项目用户
        /// </summary>
        public const string ProjectUserMenuId = "CDB80E91-61A8-4E4D-BA97-3ADDC3208B66";
        /// <summary>
        /// 班组信息
        /// </summary>
        public const string TeamGroupMenuId = "2C970C89-8C69-4A6C-B832-8A64B8A701CA";
        /// <summary>
        /// 单位工程
        /// </summary>
        public const string UnitWorkMenuId = "CBA3833A-C705-4B4E-A4A7-ACC27D0ECDCE";
        /// <summary>
        /// 项目用户转换
        /// </summary>
        public const string ProjectDataSendReceiveMenuId = "DB3F7E51-7700-4B87-A529-3070AA652517";
        /// <summary>
        /// 编码/模板
        /// </summary>
        public const string ProjectCodeTemplateRuleMenuId = "09769041-79BB-4456-8DF1-45548E72E423";
        /// <summary>
        /// 项目移动端首页
        /// </summary>
        public const string ProjectPageDataMenuId = "44140854-701D-4D67-AD8E-AA8DD48B6D6A";
        /// <summary>
        /// 项目地图
        /// </summary>
        public const string ProjectProjectMapMenuId = "F266456A-991F-45A3-BCD6-CF2515D71E39";
        #endregion

        #region 公共资源
        #region 安全合规
        /// <summary>
        /// 安全法律法规
        /// </summary>
        public const string LawRegulationListMenuId = "F4B02718-0616-4623-ABCE-885698DDBEB1";

        /// <summary>
        /// 安全标准规范
        /// </summary>
        public const string HSSEStandardListMenuId = "EFDSFVDE-RTHN-7UMG-4THA-5TGED48F8IOL";

        /// <summary>
        /// 政府部门安全规章制度
        /// </summary>
        public const string RulesRegulationsMenuId = "DF1413F3-4CE5-40B3-A574-E01CE64FEA25";

        /// <summary>
        /// 安全管理规定
        /// </summary>
        public const string ManageRuleMenuId = "56960940-81A8-43D1-9565-C306EC7AFD12";
        #endregion

        #region 安全体系
        /// <summary>
        /// 安全组织体系
        /// </summary>
        public const string HSSEOrganizeMenuId = "8IDKGJE2-09B1-6UIO-3EFM-5TGED48F0001"; 
        /// <summary>
        /// 安全管理机构
        /// </summary>
        public const string HSSEManageMenuId = "32F5CC8C-E0F4-456C-AB88-77E36269FA50";
        /// <summary>
        /// 安全主体责任
        /// </summary>
        public const string HSSEMainDutyMenuId = "1DB91420-47D3-4219-AAB4-7E613FCBCC90";
        /// <summary>
        /// 安全制度
        /// </summary>
        public const string ServerSafetyInstitutionMenuId = "499E23C1-057C-4B04-B92A-973B1DACD546";
        #endregion

        #region 安全教育
        /// <summary>
        /// 培训教材库
        /// </summary>
        public const string TrainDBMenuId = "9D99A981-7380-4085-84FA-8C3B1AFA6202";
        /// <summary>
        /// 公司教材库
        /// </summary>
        public const string CompanyTrainingMenuId = "9D4F76A1-CD2E-4E66-B833-49425CD879EB";
        /// <summary>
        /// 安全试题库
        /// </summary>
        public const string TrainTestDBMenuId = "F58EE8ED-9EB5-47C7-9D7F-D751EFEA44CA";
        /// <summary>
        /// 事故案例库
        /// </summary>
        public const string AccidentCaseMenuId = "D86917DB-D00A-4E18-9793-C290B5BBA84C";
        /// <summary>
        /// 应知应会库
        /// </summary>
        public const string KnowledgeDBMenuId = "AB7A3D78-2D89-4488-97E3-8F8616BDDE30";
        /// <summary>
        /// 考试试题库
        /// </summary>
        public const string TestTrainingMenuId = "4D6BD686-DA06-45CC-9DB8-54B342651724";
        #endregion

        #region 安全技术
        /// <summary>
        /// 危险源清单
        /// </summary>
        public const string HazardListMenuId = "8333727B-A2CE-4CE2-A019-21AC9EE61A4C";
        /// <summary>
        /// 公司危险源清单
        /// </summary>
        public const string CompanyHazardListMenuId = "C0018E8C-C88B-4E25-BCFC-F0BF3CACC63A";
        /// <summary>
        /// 环境因素危险源
        /// </summary>
        public const string EnvironmentalMenuId = "773B59F9-61F9-4F5E-9D68-A1BF9322AFFA";
        /// <summary>
        /// 公司环境因素危险源
        /// </summary>
        public const string CompanyEnvironmentalMenuId = "DC2AA8C2-82A8-4F7A-832D-9889C65AA228";
        /// <summary>
        /// 安全隐患
        /// </summary>
        public const string RectifyMenuId = "2D86AD87-4108-428C-BA3D-F81FB85511FE";
        /// <summary>
        /// 检查项
        /// </summary>
        public const string TechniqueCheckItemSetMenuId = "9C26BF2A-091D-4AC3-8678-334DE4E1CED7";
        /// <summary>
        /// HAZOP管理
        /// </summary>
        public const string HAZOPMenuId = "41C22E63-36B7-4C44-89EC-F765BFBB7C55";
        /// <summary>
        /// 安全评价
        /// </summary>
        public const string AppraiseMenuId = "0ADD01FB-8088-4595-BB40-6A73F332A229";
        /// <summary>
        /// 安全专家
        /// </summary>
        public const string ExpertMenuId = "05495F29-B583-43D9-89D3-3384D6783A3F";
        /// <summary>
        /// 应急预案
        /// </summary>
        public const string EmergencyMenuId = "D11BFC83-BE64-457D-B8FA-11C37D35CD72";
        /// <summary>
        /// 施工方案
        /// </summary>
        public const string SpecialSchemeMenuId = "3E2F2FFD-ED2E-4914-8370-D97A68398814";
        #endregion

        #region 标牌管理
        /// <summary>
        /// 标牌管理
        /// </summary>
        public const string SignManageMenuId = "022CA9C1-70F0-4C07-996C-0736D32B442A";
        #endregion

        #region 参考资料
        /// <summary>
        /// 参考资料
        /// </summary>
        public const string ResourcesDataMenuId = "EC1BED24-CDA6-4041-9B2A-BEB5E354D58F";

        /// <summary>
        /// 问题及答案管理
        /// </summary>
        public const string ProblemAnswerMenuId = "37EB5621-A9D3-405A-854B-4722B045CC1E";
        #endregion

        #region 安全交流
        /// <summary>
        /// 注册管理
        /// </summary>
        public const string RegisterMenuId = "8FA7237E-DB0C-436C-9BC6-8C3A560EE688";

        /// <summary>
        /// 内容管理
        /// </summary>
        public const string ContentMenuId = "7CCD36CB-6BFE-4FD7-8497-4DACB565298E";
        #endregion
        #endregion

        #region 个人设置菜单
        /// <summary>
        /// 个人信息
        /// </summary>
        public const string PersonalInfoMenuId = "42368A1C-EE84-423D-9003-B0CAD0FF169D";

        /// <summary>
        /// 操作日志
        /// </summary>
        public const string RunLogMenuId = "D363BD9D-4DEC-45D8-89C8-B0E49DEF61B4";

        /// <summary>
        /// 个人文件夹
        /// </summary>
        public const string PersonalFolderMenuId = "A6994B53-6237-4C2B-BDC5-E7E79A1E7F88";
        #endregion

        #region 本部菜单管理
        #region 项目管理
        /// <summary>
        /// 项目信息
        /// </summary>
        public const string SeverProjectSetMenuId = "B830399C-CA36-4C23-A170-21E556D052DD";

        /// <summary>
        /// 项目单位
        /// </summary>
        public const string SeverProjectUnitMenuId = "EB9C4E5E-15DB-426A-9800-6B03E64EC5DE";

        /// <summary>
        /// 项目用户
        /// </summary>
        public const string SeverProjectUserMenuId = "0CF5F6A1-4AEB-4034-9C3B-591838E1290A";
        /// <summary>
        /// 项目地图
        /// </summary>
        public const string ServerProjectMapMenuId = "C73EC3F3-6743-4A09-8433-E7DCC22E88C0";
        #endregion

        #region 安全报表（集团）
        #region 安全信息上报
        /// <summary>
        /// 百万工时安全统计月报表
        /// </summary>
        public const string MillionsMonthlyReportMenuId = "3156A9F0-276D-4AD4-BF84-45CF6DFC215C";

        /// <summary>
        /// 职工伤亡事故原因分析报表
        /// </summary>
        public const string AccidentCauseReportMenuId = "4BC71D2E-7D94-48C1-A61A-139637825AA5";

        /// <summary>
        /// 安全生产数据季报
        /// </summary>
        public const string SafetyQuarterlyReportMenuId = "A3894BAD-3F4A-4BB4-98CF-A76C588AE53F";

        /// <summary>
        /// 应急演练开展情况季报表
        /// </summary>
        public const string DrillConductedQuarterlyReportMenuId = "7985C759-8EB9-4B7D-AC65-00541280B46C";

        /// <summary>
        /// 应急演练工作计划半年报
        /// </summary>
        public const string DrillPlanHalfYearReportMenuId = "70DEB27A-D6FF-4D57-879B-0270F2967FA0";
        #endregion

        #region 安全信息分析
        /// <summary>
        /// 人工时费用分析
        /// </summary>
        public const string AnalyseWorkTimeCostMenuId = "598568A0-A338-499F-888C-1B73665837F9";

        /// <summary>
        /// 安全事故分析
        /// </summary>
        public const string AnalyseSafeAccidentMenuId = "8396C9E2-3376-4144-978A-CC6041EC6C6A";

        /// <summary>
        /// 安全隐患分析
        /// </summary>
        public const string AnalyseHiddenDangerMenuId = "5B645281-A055-4AA1-9245-DACBD984C76F";

        /// <summary>
        /// 资源来源统计
        /// </summary>
        public const string AnalyseResourceMenuId = "195D508D-E929-4B91-891E-307DC4E4338F";
        #endregion
        #endregion

        #region HSSE管理工作报告
        /// <summary>
        /// 管理月报
        /// </summary>
        public const string ServerMonthReportMenuId = "26CE4208-7DEE-46A2-A1D2-9C182D9C1DFC";
        /// <summary>
        /// 总部管理月报B
        /// </summary>
        public const string ServerMonthReportBMenuId = "B995396A-B01C-4F03-858A-FFDC853BA4B8";
        /// <summary>
        /// HSSE月总结
        /// </summary>
        public const string ServerManagerTotalMonthMenuId = "8051C9AA-801D-4001-9CB6-833CB407A169";
        /// <summary>
        /// 报表上报情况
        /// </summary>
        public const string ServerReportRemindMenuId = "D67D1C85-3798-47A9-A0DB-B4DB47FF2E7D";
        /// <summary>
        /// 项目安全文件
        /// </summary>
        public const string ServerSafeReportMenuId = "306A1C97-B6B1-4AE4-AFC1-6933E821C129";
        /// <summary>
        /// 分公司安全文件
        /// </summary>
        public const string ServerSafeUnitReportMenuId = "2C673125-AAA4-4F41-A827-0F04DFE55DED";

        #endregion
        
        #region 企业安全大检查
        /// <summary>
        /// 安全监督检查报告
        /// </summary>
        public const string SuperviseCheckReportMenuId = "1C6F9CA9-FDAC-4CE5-A19C-5536538851E1";

        /// <summary>
        /// 安全监督检查整改
        /// </summary>
        public const string SuperviseCheckRectifyMenuId = "55976B16-2C33-406E-B514-2FE42D031071";
        #endregion

        #region 集团安全监督
        /// <summary>
        /// 企业上报监督检查报告
        /// </summary>
        public const string UpCheckReportMenuId = "B9950CB5-C47A-4C0A-A6CC-C7DDBBDE7D1E";

        /// <summary>
        /// 企业安全文件上报
        /// </summary>
        public const string SubUnitReportMenuId = "3D1CFA31-96A9-496E-9A94-318670C9D658";

        /// <summary>
        /// 集团下发监督检查整改
        /// </summary>
        public const string CheckRectifyMenuId = "4A87774E-FEA5-479A-97A3-9BBA09E4862E";

        /// <summary>
        /// 集团下发监督检查报告
        /// </summary>
        public const string CheckInfoReportMenuId = "091D7D24-E706-465A-95FD-8EF359CB8667";
        #endregion

        #region 绩效评价
        /// <summary>
        /// 绩效评价
        /// </summary>
        public const string ProjectEvaluationMenuId = "DEE90726-E00D-462B-A4BF-7E36180DD5B8";
        #endregion

        #region 职业健康
        /// <summary>
        /// 危害检测
        /// </summary>
        public const string ServerHazardDetectionMenuId = "D4802FF6-0AE0-4F9B-9D69-FD895CF9F5C0";

        /// <summary>
        /// 体检管理
        /// </summary>
        public const string ServerPhysicalExaminationMenuId = "DB06084F-742F-49F1-A9B9-1100919E49DB";

        /// <summary>
        /// 职业病事故
        /// </summary>
        public const string ServerOccupationalDiseaseAccidentMenuId = "52DA3277-DCC1-4612-8083-A576BF85953A";
        #endregion

        #region 环境保护
        /// <summary>
        /// 环境监测数据
        /// </summary>
        public const string ServerEnvironmentalMonitoringMenuId = "FD4E234C-265F-4B45-A35A-C9659AF9C173";

        /// <summary>
        /// 突发环境事件
        /// </summary>
        public const string ServerUnexpectedEnvironmentalMenuId = "6C36DBFF-E765-4FC9-B978-51ADBE696C10";

        /// <summary>
        /// 环境事件应急预案
        /// </summary>
        public const string ServerEnvironmentalEmergencyPlanMenuId = "6A8EAA9C-08E9-4F1F-B824-67B60D49258A";

        /// <summary>
        /// 环评报告
        /// </summary>
        public const string ServerEIAReportMenuId = "FB943BD9-33A5-4680-82C1-29A4741D8636";

        #endregion

        #region 安全事故
        /// <summary>
        /// 事故快报
        /// </summary>
        public const string ServerAccidentReportMenuId = "DC871FCA-FBA8-4533-B5D6-DF64BCE40287";

        /// <summary>
        /// 事故处理
        /// </summary>
        public const string ServerAccidentStatisticsMenuId = "BE2F6161-7C17-41FF-A314-8C0AE323D5A4";

        /// <summary>
        /// 事故统计
        /// </summary>
        public const string ServerAccidentAnalysisMenuId = "71A5556F-1590-4D4C-9A31-703DCD5C2910";

        /// <summary>
        /// 事故台账
        /// </summary>
        public const string ServerAccidentDataListMenuId = "6F2C0F0A-3CF6-4B28-AFE2-FB7415ECDB91";
        #endregion

        #region 信息管理
        /// <summary>
        /// 管理通知
        /// </summary>
        public const string ServerNoticeMenuId = "E2F56879-5337-4BEF-8113-62845DF616EF";

        /// <summary>
        /// 项目图片
        /// </summary>
        public const string ServerPictureMenuId = "278DF0FE-35E2-470F-9AE4-23C57EDF797F";
        #endregion

        #region 信息统计
        #endregion

        #region 企业安全管理资料
        /// <summary>
        /// 企业安全管理资料设置
        /// </summary>
        public const string ServerSafetyDataMenuId = "60E00925-3357-441E-BD2F-2DF8C91BDDE6";

        /// <summary>
        /// 企业安全管理资料考核计划
        /// </summary>
        public const string ServerSafetyDataPlanMenuId = "039BD08A-9C38-4C28-81EE-A6CA86F580B2";

        /// <summary>
        /// 项目企业安全管理资料
        /// </summary>
        public const string ServerProjectSafetyDataMenuId = "74A51BC9-EE10-4534-A4A7-37889B07753C";

        /// <summary>
        /// 企业安全管理资料考核
        /// </summary>
        public const string ServerSafetyDataCheckMenuId = "2A405839-FD14-4398-8AEE-48B44BFDA1F6";

        /// <summary>
        /// 公司安全人工时管理
        /// </summary>
        public const string ServerAccidentDataMenuId = "A139FF69-8B74-489B-AB5F-526B2207DD89";
        #endregion

        #region E项目安全管理资料
        /// <summary>
        /// 资料目录设置
        /// </summary>
        public const string ServerSafetyDataEMenuId = "5786468C-3EB3-4219-8121-70F603E48A1A";

        /// <summary>
        /// 考核计划总表
        /// </summary>
        public const string ServerSafetyDataEPlanMenuId = "F29C7286-446D-49CC-AB6B-40CA48132CB8";

        /// <summary>
        /// 项目上报资料
        /// </summary>
        public const string ServerProjectSafetyDataEMenuId = "CDEFCAB5-328C-406B-9551-435250C84D1D";
        #endregion

        #region 文件柜
        /// <summary>
        /// 文件柜1(集团检查类)
        /// </summary>
        public const string ServerFileCabinetMenuId = "6621CF4A-EAD4-40AF-9FFE-51DA4348C10C";

        /// <summary>
        /// 文件柜1(内业)
        /// </summary>
        public const string ServerFileCabinetBMenuId = "DDD1CE30-F8B9-4011-A20F-7AC60B34788C";
        #endregion

        #region 知识竞赛
        /// <summary>
        /// 制定计划
        /// </summary>
        public const string ServerTestPlanMenuId = "A048350B-25E6-4DE1-A701-73638656D15F";
        /// <summary>
        /// 答题记录
        /// </summary>
        public const string ServerTestRecordMenuId = "64CFD530-A4F9-4857-BE3C-D243B4333ECA";
        /// <summary>
        /// 成绩排名
        /// </summary>
        public const string ServerTestRankingMenuId = "19FB0C6D-E2CA-4352-B635-060347D45C4A";
        #endregion
        #endregion

        #region 项目菜单
        

        #region 安全体系
        /// <summary>
        /// 安全组织机构
        /// </summary>
        public const string ProjectSafetyOrganizationMenuId = "1EDD072E-473A-4CDB-A2D3-E401C146B2B2"; 
        /// <summary>
        /// 安全体系
        /// </summary>
        public const string ProjectSafetySystemMenuId = "21C779D6-269B-4CB7-AFFB-F59958AC0EF0"; 
        /// <summary>
        /// 安全制度
        /// </summary>
        public const string ProjectSafetyInstitutionMenuId = "9D04CD8B-575C-4854-B8B0-F90CEEB75815";        
        /// <summary>
        /// 安全管理组织机构
        /// </summary>
        public const string ProjectSafetyManageOrganizationMenuId = "C8AC9BBB-6E6E-4871-BADC-7963EB1CCAA8";
        #endregion

        #region HSSE实施计划及管理规定
        /// <summary>
        /// HSSE实施计划
        /// </summary>
        public const string ProjectActionPlanListMenuId = "CBC47C8B-141C-446B-90D9-CE8F5AE66CE4";

        /// <summary>
        /// HSSE实施计划总结
        /// </summary>
        public const string ProjectActionPlanSummaryMenuId = "9BEB66C0-E6DE-44DD-94F6-5C7433E6DE62";

        /// <summary>
        /// HSSE管理规定发布
        /// </summary>
        public const string ActionPlan_ManagerRuleMenuId = "775EFCF4-DE5C-46E9-8EA3-B16270E2F6A6";

        /// <summary>
        /// HSSE管理规定清单
        /// </summary>
        public const string ActionPlan_ManagerRuleListMenuId = "703D90A7-C40B-4753-943B-8A59AABDC043";
        #endregion

        #region HSSE资质审核
        /// <summary>
        /// 分包商资质
        /// </summary>
        public const string SubUnitQualityMenuId = "DFDFEDA3-FECB-40DA-9216-C67B48002A8A";

        /// <summary>
        /// 采购供货厂家管理
        /// </summary>
        public const string InUnitMenuId = "03BAA34B-87D2-4479-9E69-10DD4A62A2A8";

        /// <summary>
        /// 特殊岗位人员资质
        /// </summary>
        public const string PersonQualityMenuId = "EBEA762D-1F46-47C5-9EAD-759E13D9B41C";

        /// <summary>
        /// 特种设备作业人员资质
        /// </summary>
        public const string EquipmentPersonQualityMenuId = "96FB96FF-20D8-4CE9-AF57-C19717BB5D3E";

        /// <summary>
        /// 特殊机具设备资质
        /// </summary>
        public const string EquipmentQualityMenuId = "2DEDD752-8BAF-43CD-933D-932AF9AF2F58";

        /// <summary>
        /// 一般机具设备资质
        /// </summary>
        public const string GeneralEquipmentQualityMenuId = "BFD62699-47F0-49FA-AD39-FAEE8A6C3313";

        /// <summary>
        /// 项目协议记录
        /// </summary>
        public const string ProjectRecordMenuId = "874B4232-E0AD-41CD-8C66-8A7FF2D79358";

        /// <summary>
        /// 安全人员资质
        /// </summary>
        public const string SafePersonQualityMenuId = "750F5074-45B9-470E-AE1E-6204957421E6";
        /// <summary>
        /// 管理人员资质
        /// </summary>
        public const string ManagePersonQualityMenuId = "07435F23-F87D-4F52-B32C-A3DA95B10DA2";
        #endregion

        #region HSSE危险源辨识与评价
        /// <summary>
        /// 环境危险源辨识与评价
        /// </summary>
        public const string ProjectEnvironmentalRiskListMenuId = "762F0BF9-471B-4115-B35E-03A26C573877";

        /// <summary>
        /// 危险源辨识与评价清单
        /// </summary>
        public const string ProjectHazardListMenuId = "EDC50857-7762-4498-83C6-5BDE85036BAB";

        /// <summary>
        /// 风险提示
        /// </summary>
        public const string HazardPromptMenuId = "F6A6D53A-150E-43DB-A3C4-6FA18E9401E9";

        /// <summary>
        /// 危险观察登记
        /// </summary>
        public const string HazardRegisterMenuId = "A6B2879F-73CD-490C-8843-1F2F25D6EC61";

        /// <summary>
        /// 危险辨识分析
        /// </summary>
        public const string HazardAnalyseMenuId = "C763FEE5-B703-481A-BBDB-C85CE9B7846A";

        /// <summary>
        /// 其他危险源辨识文件
        /// </summary>
        public const string OtherHazardMenuId = "E22F555A-D41C-4F5F-9734-39B578957732";

        #endregion

        #region HSSE应急响应管理
        /// <summary>
        /// HSSE应急预案管理清单
        /// </summary>
        public const string ProjectEmergencyListMenuId = "ABD84F93-A84E-448C-8A67-AB0FE4E8D10C";

        /// <summary>
        /// 应急演练
        /// </summary>
        public const string ProjectDrillRecordListMenuId = "CF5516F7-0735-44EF-9A6D-46FABF8EBC6E";

        /// <summary>
        /// 应急物资管理
        /// </summary>
        public const string ProjectEmergencySupplyMenuId = "39244F05-0D9E-4750-B12E-CEA5E11338A8";

        /// <summary>
        /// 应急队伍与培训
        /// </summary>
        public const string ProjectEmergencyTeamAndTrainMenuId = "6FDF9DAE-2161-4F67-931F-85DEAFC3842A";

        /// <summary>
        /// 应急响应记录与评价
        /// </summary>
        public const string ProjectEmergencyResponseRecordMenuId = "A95A7C98-C186-4418-9C41-BD7775D85284";
        #endregion

        #region 施工方案/方案审查
        /// <summary>
        /// 施工方案及审查
        /// </summary>
        public const string ProjectConstructSolutionMenuId = "9B42977B-FA0B-48EF-8616-D53FC14E5127";

        /// <summary>
        /// 现场施工方案模板
        /// </summary>
        public const string ProjectSolutionTempleteMenuId = "5FE00FAB-8191-4AA9-B8B2-BFDC91200B24";
        /// <summary>
        /// 方案模板
        /// </summary>
        public const string SolutionTemplateMenuId = "D5F6DFAA-4051-4E0E-818B-2A45F985C5A4";

        /// <summary>
        /// 危险性较大的工程清单
        /// </summary>
        public const string ProjectLargerHazardListMenuId = "5B3D3F7B-9B50-4927-B131-11D13D4D1C19";

        /// <summary>
        /// 专家论证清单
        /// </summary>
        public const string ProjectExpertArgumentMenuId = "27DE7248-C4FF-4288-BBAC-11CB8741AD67";
        #endregion      

        #region 现场动态管理
        /// <summary>
        /// 现场动态信息
        /// </summary>
        public const string ProjectConstructionDynamicMenuId = "5B22A9E3-4701-4F49-9405-846B3E63F43B";

        /// <summary>
        /// 月度计划
        /// </summary>
        public const string ProjectMonthPlanMenuId = "FD1F5E74-4843-4F6B-B893-8A16D26443D9";
        #endregion

        #region 现场人员动态管理
        /// <summary>
        /// 人员信息
        /// </summary>
        public const string PersonListMenuId = "AD6FC259-CF40-41C7-BA3F-15AC50C1DD20";
        /// <summary>
        /// 人员项目转换
        /// </summary>
        public const string SendReceiveMenuId = "06F7E687-51B3-4357-BD6D-E6AEFC0E3975";
        /// <summary>
        /// 人工时日报
        /// </summary>
        public const string DayReportMenuId = "8F15D3BE-BE21-4A6F-AD5C-2BBECEE46149";

        /// <summary>
        /// 人工时月报
        /// </summary>
        public const string ProjectMonthReportMenuId = "6C97E014-AF13-46E5-ADB2-03D327C560EC";

        /// <summary>
        /// 发卡管理
        /// </summary>
        public const string SendCardMenuId = "7ACB0CB1-15D8-4E8E-A54D-0CDC5F69B39A";

        /// <summary>
        /// 现场人员考勤管理
        /// </summary>
        public const string PersonInfoMenuId = "12F7123B-C2ED-4011-9859-83260AC91F09";

        /// <summary>
        /// 现场人员统计
        /// </summary>
        public const string PersonStatisticMenuId = "7D36E853-CC79-48B9-9E7F-E34797B4E87E";  
        
        /// <summary>
        /// 人员变化
        /// </summary>
        public const string ProjectPersonChangeMenuId = "846565DD-89FC-4191-A71B-5CBEA2BE00E0";
        #endregion

        #region HSSE教育培训
        /// <summary>
        /// 培训记录
        /// </summary>
        public const string ProjectTrainRecordMenuId = "1182E353-FAB9-4DB1-A1EC-F41A00892128";
        /// <summary>
        /// 人员培训查询
        /// </summary>
        public const string ProjectTrainFindMenuId = "F81E3F54-B3A9-4DDB-9C8C-1574317E040F";
        /// <summary>
        /// 培训计划
        /// </summary>
        public const string ProjectTrainingPlanMenuId = "B782A26B-D85C-4F84-8B45-F7AA47B3159E";
        /// <summary>
        /// 培训任务
        /// </summary>
        public const string ProjectTrainingTaskMenuId = "E108F75D-89D0-4DCA-8356-A156C328805C";
        /// <summary>
        /// 培训试题
        /// </summary>
        public const string ProjectTrainTestRecordMenuId = "6C314522-AF62-4476-893E-5F42C09C3077";
        /// <summary>
        /// 考试计划
        /// </summary>
        public const string ProjectTestPlanMenuId = "FAF7F4A4-A4BC-4D94-9E88-0CF5A380DB34";
        /// <summary>
        /// 考试记录
        /// </summary>
        public const string ProjectTestRecordMenuId = "0EEB138D-84F9-4686-8CBB-CAEAA6CF1B2A";
        /// <summary>
        /// 模拟考试
        /// </summary>
        public const string ProjectModelTestRecordMenuId = "1C80EF15-B75B-473D-B190-CE12E4DDA287";
        /// <summary>
        /// 考试统计
        /// </summary>
        public const string ProjectTestStatisticsMenuId = "6FF941C1-8A00-4A74-8111-C892FC30A8DA";
        #endregion

        #region HSSE许可管理
        /// <summary>
        /// 现场HSSE作业许可证
        /// </summary>
        public const string ProjectLicenseManagerMenuId = "0E9B7084-D021-4CA3-B9D2-9CBAA27A571B";
        /// <summary>
        /// 新开项目作业许可证
        /// </summary>
        public const string ProjectSecurityLicenseMenuId = "915F5AB2-CDCA-4025-A462-AC873D8FB037";
        /// <summary>
        /// 施工机具、安全设施检查验收
        /// </summary>
        public const string ProjectEquipmentSafetyListMenuId = "9703D711-85DA-4A0B-B08B-70F791418696";
        /// <summary>
        /// 安全技术交底
        /// </summary>
        public const string ProjectHSETechnicalMenuId = "49485F7E-8E71-4EED-87B4-BF6CC180C69C";
        /// <summary>
        /// 动火作业票
        /// </summary>
        public const string ProjectFireWorkMenuId = "2E58D4F1-2FF1-450E-8A00-1CE3BBCF8D4B";
        /// <summary>
        /// 高处作业票
        /// </summary>
        public const string ProjectHeightWorkMenuId = "DA1CAE8E-B5BF-4AC0-9996-AF6CAA412CA9";
        /// <summary>
        /// 受限空间作业票
        /// </summary>
        public const string ProjectLimitedSpaceMenuId = "AEC9166D-1C91-45F0-8BFE-D3D0479A28C7";
        /// <summary>
        /// 射线作业票
        /// </summary>
        public const string ProjectRadialWorkMenuId = "F72FF20B-D3EB-46A5-97F7-C99B2473A140";
        /// <summary>
        /// 断路(占道)作业票
        /// </summary>
        public const string ProjectOpenCircuitMenuId = "4E607E83-41FC-4F49-B26F-A21CFE38328F";
        /// <summary>
        /// 动土作业票
        /// </summary>
        public const string ProjectBreakGroundMenuId = "755C6AC9-2E38-4D4F-AF33-33CB1744A907";
        /// <summary>
        /// 夜间施工作业票
        /// </summary>
        public const string ProjectNightWorkMenuId = "7BBAE649-7B00-4475-A911-BFE3A37AC55B";
        /// <summary>
        /// 吊装作业票
        /// </summary>
        public const string ProjectLiftingWorkMenuId = "A1BE3AB6-9D4A-41E7-8870-E73423165451";
        #endregion

        #region HSSE检查管理
        /// <summary>
        /// 检查项目设置
        /// </summary>
        public const string ProjectCheckItemSetMenuId = "9CEB4059-5826-4B8C-923C-6FE4131ED636";
        /// <summary>
        /// HSSE日常巡检
        /// </summary>
        public const string ProjectCheckDayMenuId = "9F6FB863-4001-49BD-A748-66009891D13C";
        /// <summary>
        /// HSSE日常巡检（五环）
        /// </summary>
        public const string ProjectCheckDayWHMenuId = "0E30F917-0C51-4C45-BD19-981039CA44F5";
        /// <summary>
        /// HSSE日常巡检（新奥）
        /// </summary>
        public const string ProjectCheckDayXAMenuId = "38FBBA77-8B35-470C-90EE-6861E6DDE03F";
        /// <summary>
        /// HSSE专项检查
        /// </summary>
        public const string ProjectCheckSpecialMenuId = "1B08048F-93ED-4E84-AE65-DB7917EA2DFB"; 
        /// <summary>
        /// HSSE综合检查
        /// </summary>
        public const string ProjectCheckColligationMenuId = "C198EBA8-9E23-4654-92E1-09C61105C522";
        /// <summary>
        /// HSSE综合检查（五环）
        /// </summary>
        public const string ProjectCheckColligationWHMenuId = "E8363923-06CD-47C2-BFE1-F18212094710";
        /// <summary>
        /// 开工前HSSE检查
        /// </summary>
        public const string ProjectCheckWorkMenuId = "9212291A-FBC5-4F6D-A5F6-60BFF4E30F6F";  
        /// <summary>
        /// 季节性/节假日HSSE检查
        /// </summary>
        public const string ProjectCheckHolidayMenuId = "0D23A707-ADA0-4C2B-9665-611134243529"; 
        /// <summary>
        /// 危险因素统计分析
        /// </summary>
        public const string CheckAnalysisMenuId = "14C42C8E-8D3D-4D30-AA56-4F96828610AD";
        /// <summary>
        /// 隐患整改通知单
        /// </summary>
        public const string ProjectRectifyNoticeMenuId = "0038D764-D628-46F0-94FF-D0A22C3C45A3";
        /// <summary>
        /// 隐患整改单(赛鼎)
        /// </summary>
        public const string ProjectRectifyNoticesMenuId = "C3B6B222-44DC-46F3-B783-B1FC0F743C48";
        /// <summary>
        /// 企业监督检查整改
        /// </summary>
        public const string ProjectSuperviseCheckRectifyMenuId = "29F27641-06ED-435A-9F9B-FCE6366801BE";
        /// <summary>
        /// 工程暂停令
        /// </summary>
        public const string ProjectPauseNoticeMenuId = "C81DB7ED-165E-4C69-86B0-A3AAE37059FE";
        /// <summary>
        /// 隐患巡检（手机端）
        /// </summary>
        public const string RegistrationMenuId = "B6BE5FE0-CB84-47FF-A6C3-5AD9E1CCE079";
        /// <summary>
        /// 隐患巡检记录 （手机端）
        /// </summary>
        public const string RegistrationRecordMenuId = "9B2F84A8-50EB-49F1-A876-E2BE1524A941";
        /// <summary>
        /// 监理整改通知单
        /// </summary>
        public const string ProjectSupervisionNoticeMenuId = "F910062E-98B0-486F-A8BD-D5B0035F808F";
        /// <summary>
        /// 联系单
        /// </summary>
        public const string ProjectContactListMenuId = "F057C207-4549-48AE-B838-A1920E5709D8";
        #endregion

        #region APP检查管理
        /// <summary>
        /// 日常巡检
        /// </summary>
        public const string HSSE_HiddenRectificationListMenuId = "2FC8AA2A-F421-4174-A05E-2711167AF141";

        /// <summary>
        /// 日常巡检记录
        /// </summary>
        public const string HSSE_HiddenRectificationRecordMenuId = "23855EB9-0BB1-4B3E-BA69-3C0222F5E2A4";

        /// <summary>
        /// APP专项检查
        /// </summary>
        public const string HSSE_APPCheckSpecialMenuId = "B7A5F84B-843A-4C13-9844-023D8B4A41AC";

        /// <summary>
        /// APP领导督察
        /// </summary>
        public const string HSSE_APPSafeSupervisionMenuId = "247B76AA-01BF-4A40-BB4C-B3EAF441F538";

        #endregion

        #region 隐患巡检
        /// <summary>
        /// 巡检登记
        /// </summary>
        public const string RegistrationInspectionMenuId = "80AEE39B-11B2-494C-A1D4-42ACA05BCC68";

        /// <summary>
        /// 隐患整改
        /// </summary>
        public const string HiddenRectificationMenuId = "9D9E4E15-CCC3-4315-8596-4BAC9E131542";

        /// <summary>
        /// 隐患分析
        /// </summary>
        public const string RiskAnalysisMenuId = "540EF1FB-9EDC-42D5-BA7B-AC56ED6FE7EF";
        #endregion

        #region HSSE奖惩管理
        /// <summary>
        /// 奖励通知单
        /// </summary>
        public const string ProjectIncentiveNoticeMenuId = "96F21A83-6871-4CC4-8901-1B99C376395C";
        /// <summary>
        /// 处罚通知单
        /// </summary>
        public const string ProjectPunishNoticeMenuId = "755F1C1D-2178-47D8-9F82-A501B53A2436";
        /// <summary>
        /// 处罚统计
        /// </summary>
        public const string ProjectPunishNoticeStatisticsMenuId = "CCD0E55B-300A-454B-8559-155ADAD386AE";
        /// <summary>
        /// 违规人员记录
        /// </summary>
        public const string ProjectViolationPersonMenuId = "06958288-96F4-4291-909A-FFC2FC76814D";
        /// <summary>
        /// HSSE获奖证书或奖杯
        /// </summary>
        public const string ProjectHSECertificateMenuId = "9A034CAD-C7D5-4DE4-9FF5-828D35FFEE28";
        #endregion

        #region HSSE会议管理
        /// <summary>
        /// HSSE周例会
        /// </summary>
        public const string ProjectWeekMeetingMenuId = "5236B1D9-8B57-495E-8644-231DF5D066CE";
        /// <summary>
        /// HSSE月例会
        /// </summary>
        public const string ProjectMonthMeetingMenuId = "D5DD5EBD-A5F2-4A43-BA4C-E9A242B43684"; 
        /// <summary>
        /// HSSE专题会议
        /// </summary>
        public const string ProjectSpecialMeetingMenuId = "BB6CEC48-283B-46AD-BEDD-F964D261698F"; 
        /// <summary>
        /// 其他会议记录
        /// </summary>
        public const string ProjectAttendMeetingMenuId = "B2DA78EF-EECA-4AF4-9FAC-ECCFF1D6E459";
        /// <summary>
        /// 班前会
        /// </summary>
        public const string ProjectClassMeetingMenuId = "F8ADCDBC-AAAD-4884-9935-2B63562E4779";
        #endregion

        #region 出入场管理
        /// <summary>
        /// 特种设备机具入场报批
        /// </summary>
        public const string EquipmentInMenuId = "3E167389-4775-4AC3-9D31-2E570682EDA1";

        /// <summary>
        /// 特种设备机具出场报批
        /// </summary>
        public const string EquipmentOutMenuId = "A4832598-E3D4-4906-88E5-A3886A85FC5A";

        /// <summary>
        /// 一般设备机具入场报批
        /// </summary>
        public const string GeneralEquipmentInMenuId = "06EA1483-7397-46DD-818D-56911EA7B679";

        /// <summary>
        /// 一般设备机具出场报批
        /// </summary>
        public const string GeneralEquipmentOutMenuId = "56F241A9-0AA3-4EDB-8C5C-999C487C06DA";

        /// <summary>
        /// 普通货物入场报批
        /// </summary>
        public const string GoodsInMenuId = "32CA471B-86FD-420F-8DAA-FF16B5BEBBB4";

        /// <summary>
        /// 普通货物出场报批
        /// </summary>
        public const string GoodsOutMenuId = "ADD37E84-923C-4AFA-BE37-670B40ABF0F7";

        /// <summary>
        /// 气瓶入场报批
        /// </summary>
        public const string GasCylinderInMenuId = "33327576-D346-45C9-9C97-805EB0C92EF2";

        /// <summary>
        /// 气瓶出场报批
        /// </summary>
        public const string GasCylinderOutMenuId = "CB107947-AE48-466B-87F5-58BCA72FE1AC";

        /// <summary>
        /// 特种车辆入场审批
        /// </summary>
        public const string CarInMenuId = "3D12E3FD-5AC3-485A-A39F-08689234450A";

        /// <summary>
        /// 普通车辆入场审批
        /// </summary>
        public const string GeneralCarInMenuId = "7087F8BB-DC8C-4A77-8E16-232E0B8481D2";

        /// <summary>
        /// 特种设备审批
        /// </summary>
        public const string EquipmentQualityInMenuId = "42E7E869-67EA-446E-A910-BE7BF95EDC00";
        #endregion

        #region HSSE费用及物资管理
        /// <summary>
        /// HSSE措施费用使用计划
        /// </summary>
        public const string ProjectExpenseMenuId = "EEE7CBBE-2EFB-4D64-96A6-A932E20FF9DB";
        /// <summary>
        /// HSSE费用投入登记
        /// </summary>
        public const string ProjectCostSmallDetailMenuId = "0C311396-C859-40B0-9D72-6A8B20733002";
        /// <summary>
        /// 安全措施费使用计划
        /// </summary>
        public const string ProjectMeasuresPlanMenuId = "6FBF4B7D-21D2-4013-9465-12AC093109D4";
        /// <summary>
        /// 安全费用投入登记
        /// </summary>
        public const string ProjectPayRegistrationMenuId = "9EFF1A0F-87AA-43E7-83B0-79EEAAC8848E";
        /// <summary>
        /// HSSE费用管理
        /// </summary>
        public const string ProjectCostManageMenuId = "FF68C697-B058-4687-A98F-71C591650E02";
        /// <summary>
        /// 合同HSE费用及支付台账
        /// </summary>
        public const string ProjectCostLedgerMenuId = "19C1370F-92C0-4E31-87B4-8BADA74113E4"; 
        /// <summary>
        /// HSSE费用统计表
        /// </summary>
        public const string TC_CostMenuId = "89B42B7B-8AEE-4199-923E-81A602FC77E0";  
        /// <summary>
        /// HSSE物资管理
        /// </summary>
        public const string GoodsManageMenuId = "3DC61C8A-7C54-49E0-96C4-DED9CC6BFD0B";
        /// <summary>
        /// 物资入库管理
        /// </summary>
        public const string GoodsIn2MenuId = "FDA02FCA-4E23-469E-AB26-2D625D0E579A";  
        /// <summary>
        /// 物资出库管理
        /// </summary>
        public const string GoodsOut2MenuId = "881D6FE5-C281-4DA8-80CD-D7C6624796FC"; 
        /// <summary>
        /// 物资库存管理
        /// </summary>
        public const string GoodsStockMenuId = "B97B1C46-25B0-477B-9925-B6AB1D45204E";
        /// <summary>
        /// 安全分包费用
        /// </summary>
        public const string ProjectHSSECostUnitManageMenuId = "6488F005-95F2-4D49-BC95-6DF4C9B23F3D";
        /// <summary>
        /// 安全分包费用审核
        /// </summary>
        public const string ProjectHSSECostUnitManageAuditMenuId = "0051CA27-83A3-4CAD-8A0B-64C4DFE3944C";
        /// <summary>
        /// 安全分包费用核定
        /// </summary>
        public const string ProjectHSSECostUnitManageRatifiedMenuId = "8B71C8C4-0738-4D5F-8916-57FF3197709C";
        /// <summary>
        /// 安全费用管理
        /// </summary>
        public const string ProjectHSSECostManageMenuId = "5C74F09D-FDE3-4995-A1D6-0549A8693940";
        #endregion

        #region HSSE行政管理
        /// <summary>
        /// HSSE检查类别设置
        /// </summary>
        public const string CheckTypeSetMenuId = "7353F3C1-0EAF-4DB7-86B4-93E3B96805F8"; 
        /// <summary>
        /// HSSE行政管理检查记录
        /// </summary>
        public const string ManageCheckMenuId = "A25EB19A-F06E-4AAF-A589-E2B8F7FA7857"; 
        /// <summary>
        /// 现场车辆管理
        /// </summary>
        public const string CarManagerMenuId = "CF82805A-84BE-400A-B939-83F7612D76F5"; 
        /// <summary>
        /// 现场驾驶员管理
        /// </summary>
        public const string DriverManagerMenuId = "754C23CA-F1BC-4F44-9D34-B185099EDCA0"; 
        /// <summary>
        /// 职业健康管理
        /// </summary>
        public const string HealthManageMenuId = "D55ACBCB-DE1C-4180-BA7D-3353C51D2B5F";
        #endregion

        #region 安全活动
        /// <summary>
        /// 主题安全活动
        /// </summary>
        public const string ProjectThemeActivitiesMenuId = "96CC0F1F-405B-4EE7-87CF-4CAE1FCC9435";
        /// <summary>
        /// 月度安全评比
        /// </summary>
        public const string ProjectMonthlyRatingMenuId = "293FD782-7B39-4F0F-A826-CA790A70CCC7";  
        /// <summary>
        /// 全国安全月
        /// </summary>
        public const string ProjectSafetyMonthMenuId = "71DA0222-1509-4154-AB39-8E88769C043C";
        /// <summary>
        /// 119消防
        /// </summary>
        public const string ProjectFireActivitiesMenuId = "58F5A99C-8104-459F-8934-54C30EE972AA";
        /// <summary>
        /// 其他
        /// </summary>
        public const string ProjectOtherActivitiesMenuId = "446D702B-F0C1-4D12-A862-0B8317D95928";
        #endregion
        
        #region 职业健康
        /// <summary>
        /// 危害检测
        /// </summary>
        public const string HazardDetectionMenuId = "DD5E76FC-C45E-4F4F-8889-A2F4703F93DD";

        /// <summary>
        /// 体检管理
        /// </summary>
        public const string PhysicalExaminationMenuId = "9EACCED2-B646-489C-84AD-1C22066F00AE";

        /// <summary>
        /// 职业病事故
        /// </summary>
        public const string OccupationalDiseaseAccidentMenuId = "775B77C0-E28D-4746-BCFE-F3E927D515A8";
        #endregion

        #region 环境保护
        /// <summary>
        /// 环境监测数据
        /// </summary>
        public const string EnvironmentalMonitoringMenuId = "342B5DEA-ECE1-46A4-BAA6-F0DB5276C769";

        /// <summary>
        /// 突发环境事件
        /// </summary>
        public const string UnexpectedEnvironmentalMenuId = "74F23370-56D8-426E-822D-5E367F620546";

        /// <summary>
        /// 环境事件应急预案
        /// </summary>
        public const string EnvironmentalEmergencyPlanMenuId = "3EF6E488-21D2-47BB-AA33-4FB0E9FED606";

        /// <summary>
        /// 环评报告
        /// </summary>
        public const string EIAReportMenuId = "97DCAFD1-DDA5-48B4-8E2D-E82702BA899B";

        #endregion

        #region HSSE管理工作报告
        /// <summary>
        /// HSSE管理周报
        /// </summary>
        public const string ProjectManagerWeekMenuId = "AE118E9C-C309-43B7-A198-8CA90A8D98EB";
        /// <summary>
        /// HSSE管理月报
        /// </summary>
        public const string ProjectManagerMonthMenuId = "5D006DDC-3AED-4E5A-8597-3C972D96F983";
        /// <summary>
        /// HSSE管理月报B
        /// </summary>
        public const string ProjectManagerMonthBMenuId = "363EB208-7BB1-4A55-85F3-2501B2F10B45";
        /// <summary>
        /// HSSE管理月报C
        /// </summary>
        public const string ProjectManagerMonthCMenuId = "68A52EEA-2661-4CB0-9382-A36AA5DCC480";
        /// <summary>
        /// HSSE管理月报D
        /// </summary>
        public const string ProjectManagerMonthDMenuId = "E18AF205-9C5B-40F8-B77B-B30C31B10BB5";
        /// <summary>
        /// 月报(赛鼎)
        /// </summary>
        public const string ProjectManagerMonth_SeDinMenuId = "D0EC3002-E1FA-457D-AC3B-4C7B2D71DD82";
        /// <summary>
        /// HSSE管理季报
        /// </summary>
        public const string ProjectManagerQuarterlyMenuId = "28668BD9-3E14-438F-8BEE-24BAF6031B63";
        /// <summary>
        /// HSSE完工报告
        /// </summary>
        public const string ProjectCompletionReportMenuId = "88AE0EF8-D29E-409F-A154-CCA3999B00AE";
        /// <summary>
        /// HSSE管理工作总结
        /// </summary>
        public const string ProjectManagerTotalMenuId = "3ADE41A3-4B0E-4D36-ABFF-ABB94519A943";
        /// <summary>
        /// 现场HSSE工作顾客评价
        /// </summary>
        public const string ProjectManagerPerformanceMenuId = "A9190CE2-EA31-421F-B733-8824B8A64EE2";
        /// <summary>
        /// HSSE月总结
        /// </summary>
        public const string ProjectManagerTotalMonthMenuId = "EF920FED-70A4-4789-B5DE-2F8220EEEF72";
        /// <summary>
        /// 工程现场环境与职业健康月报
        /// </summary>
        public const string HealthMenuId = "BD19F5D9-24EA-483D-B734-25B65A5ECDB1";
        /// <summary>
        /// 分包商HSSE周报
        /// </summary>
        public const string SubManagerWeekMenuId = "04A15594-9DCA-46A3-9224-4DEAA0BC556E";
        /// <summary>
        /// 分包商HSSE月报
        /// </summary>
        public const string SubManagerMonthMenuId = "71519DDC-9FF1-4C05-9B31-F249B3ED0106";
        /// <summary>
        /// 企业安全管理资料
        /// </summary>
        public const string ProjectSafetyDataMenuId = "54A3E23A-DAC7-484B-8C97-40AD785688FC";       
        /// <summary>
        /// HSSE日志暨管理数据收集
        /// </summary>
        public const string ProjectHSSELogMenuId = "3E077A36-EC12-4FC7-B685-1F439291C9B8";
        /// <summary>
        /// HSSE经理暨HSSE工程师细则
        /// </summary>
        public const string ProjectHSSELogMonthMenuId = "E763BC17-EC0D-4AB3-A388-EC7F734B56F2";
        /// <summary>
        /// 项目安全文件
        /// </summary>
        public const string ProjectSafeReportMenuId = "BD16BE8D-0F4D-4C80-A4D7-707156B541B8";
        /// <summary>
        /// 海外工程项目月度HSSE统计表
        /// </summary>
        public const string ProjectManagerMonthEMenuId = "288C6A04-456F-4364-B4FE-82C14B8B3CC9";
        /// <summary>
        /// 分包商安全费用投入登记
        /// </summary>
        public const string ProjectSubPayRegistrationMenuId = "E7B8059B-304B-47C6-90C8-D88E4A3EC506";
        /// <summary>
        /// 项目HSE绩效管理报告
        /// </summary>
        public const string ProjectPerformanceManagementReportMenuId = "A97295B1-86C5-45F6-B8EC-A520E8CF3A4B";
        /// <summary>
        /// 分包商上传周报
        /// </summary>
        public const string ProjectSubUploadWeekMenuId = "AB03E11C-8174-4B83-90F4-D07A3D933E1D";
        /// <summary>
        /// 工程师HSE日志
        /// </summary>
        public const string ProjectHSEDiaryMenuId = "8E5B4A8E-B06E-4C8A-A2C8-1091A9BCAF72";
        #endregion

        #region E项目安全管理资料
        /// <summary>
        /// 安全管理资料
        /// </summary>
        public const string ProjectSafetyDataESuperMenuId = "5BB670AD-CCAA-4004-B931-70C5879A968B";
        /// <summary>
        /// 安全管理资料
        /// </summary>
        public const string ProjectSafetyDataEMenuId = "BC3B7C4A-D69C-45CA-A951-98F4F64191BB";
        /// <summary>
        /// 安全管理资料考核
        /// </summary>
        public const string ProjectSafetyDataECheckMenuId = "8F2E3B63-8B98-44FC-8353-7F09DA86A463";
        #endregion

        #region HSSE绩效评价管理
        /// <summary>
        /// HSSE分包方绩效评价
        /// </summary>
        public const string PerfomanceRecordMenuId = "EED8DBEE-83F6-4B5B-8382-AF40EB66B0A9";

        /// <summary>
        /// HSSE个人绩效评价
        /// </summary>
        public const string PersonPerfomanceMenuId = "1320A6D8-713B-43D4-BB00-CDA3DE6D24CB";
        #endregion

        #region HSSE事故处理及预防
        /// <summary>
        /// 事故调查记录
        /// </summary>
        public const string ProjectAccidentPersonRecordMenuId = "FB5C66FF-3BFB-490F-A14F-0DD5B5A4D110";

        /// <summary>
        /// 事故调查处理报告
        /// </summary>
        public const string ProjectAccidentReportOtherMenuId = "B0A341A2-954A-4E24-BFC3-53D7F50628EE";

        /// <summary>
        /// HSSE事故(含未遂)处理
        /// </summary>
        public const string ProjectAccidentHandleMenuId = "9A9D6805-6C98-46E4-943E-130C4A3EB9A2";

        /// <summary>
        /// 事故调查报告
        /// </summary>
        public const string ProjectAccidentReportMenuId = "27C681E5-135A-414F-9FC2-D86D27805E6A";
        #endregion

        #region 强制性法律法规及标准规定
        /// <summary>
        /// 法律法规辨识
        /// </summary>
        public const string LawRegulationIdentifyMenuId = "C85CCDFC-E721-4B9D-B73F-F83C7578EE9B";
        /// <summary>
        /// 标准规范辨识
        /// </summary>
        public const string ConstructionStandardIdentifyMenuId = "28B0235F-3DB5-4C15-A7E3-6F5DF52C8FDC";

        #endregion

        #region 定稿文件
        /// <summary>
        /// 业主管理文档
        /// </summary>
        public const string OwnerFinalFileMenuId = "C5354813-7B1E-4155-8EE8-D349BF2F18F4";

        /// <summary>
        /// 监理管理文档
        /// </summary>
        public const string FinalFileListMenuId = "719DA0D3-FA59-4A03-B6E0-6663A211956F";

        /// <summary>
        /// 其他管理文档
        /// </summary>
        public const string OtherDocumentListMenuId = "B1C694BB-EF35-49B6-AEE4-0C5CA6803208";

        /// <summary>
        /// 现场HSSE已定稿文件
        /// </summary>
        public const string HSEFinalFileListMenuId = "24D6B764-7B91-46EB-9D80-A6073FC5720D";
        #endregion

        #region HSSE信息管理
        /// <summary>
        /// HSSE管理通知
        /// </summary>
        public const string ProjectNoticeMenuId = "B06EC998-60D2-4CBF-8080-9F000A1595AA";
        /// <summary>
        /// 违章曝光台
        /// </summary>
        public const string ProjectExposureMenuId = "09186AA8-991C-49F0-9D46-5C798D54FE0B";
        /// <summary>
        /// HSSE宣传活动
        /// </summary>
        public const string ProjectPromotionalActivitiesMenuId = "16092FE7-938B-4713-8084-4FBFA030F386";  
        /// <summary>
        /// 项目图片
        /// </summary>
        public const string ProjectPictureMenuId = "B58179BE-FE6E-4E91-84FC-D211E4692354";   
        /// <summary>
        /// 一般来文管理
        /// </summary>
        public const string ReceiveFileManagerMenuId = "4F5C00F3-DA7D-4B2D-B1EF-310DFFCA77DD";  
        /// <summary>
        /// 项目文件夹
        /// </summary>
        public const string ProjectFolderMenuId = "05C6C2AF-3B0B-4BF0-A8CE-1FC15DAC3C54";  
        /// <summary>
        /// 文件柜1
        /// </summary>
        public const string ProjectFileCabinetAMenuId = "C69B7409-BE1E-4754-AC90-57B56EEE198B"; 
        /// <summary>
        /// 文件柜2
        /// </summary>
        public const string ProjectFileCabinetBMenuId = "200019A4-E24F-4C87-8C52-9970F78DBF73"; 
        /// <summary>
        /// 信息统计
        /// </summary>
        public const string ProjectInfoStatisticMenuId = "0D5571B4-6B74-4782-8050-53529AEA5E98";
        #endregion

        #region 安全报表(集团)
        /// <summary>
        /// 百万工时安全统计月报
        /// </summary>
        public const string ProjectMillionsMonthlyReportMenuId = "6E7DC075-A7AF-4E42-8F8B-0174EFDD54A1";

        /// <summary>
        /// 职工伤亡事故原因分析报
        /// </summary>
        public const string ProjectAccidentCauseReportMenuId = "38E948BA-E043-4E89-9038-0CE1B508FA19";

        /// <summary>
        /// 安全生产数据季报
        /// </summary>
        public const string ProjectSafetyQuarterlyReportMenuId = "8B17DC64-A4B9-4283-B7A1-D2E944205FA5";

        /// <summary>
        /// 应急演练开展情况季报
        /// </summary>
        public const string ProjectDrillConductedQuarterlyReportMenuId = "0CDFC1BE-0796-4817-ADB8-7A0B48655E00";

        /// <summary>
        /// 应急演练工作计划半年报
        /// </summary>
        public const string ProjectDrillPlanHalfYearReportMenuId = "0973EE1C-CD2A-4116-BD67-1ABAD71D6C7C";
        #endregion

        #region  安全分析
        /// <summary>
        /// 安全分析
        /// </summary>
        public const string ProjectInformationAnalysisMenuId = "14DD34F2-0682-48D6-A199-108297A9825E";
        /// <summary>
        /// 危险辨识分析
        /// </summary>
        public const string ProjectHazardAnalyseMenuId = "C763FEE5-B703-481A-BBDB-C85CE9B7846A";
        /// <summary>
        /// 危险因素分析
        /// </summary>
        public const string ProjectCheckAnalysisMenuId = "14C42C8E-8D3D-4D30-AA56-4F96828610AD";
        #endregion

        #endregion

        #region 焊接管理
        #region 基础信息
        /// <summary>
        /// 焊接耗材
        /// </summary>
        public const string HJGL_ConsumablesMenuId = "8IDKGJE2-09B1-4607-BC6D-865CE48F0004";

        /// <summary>
        /// 管道等级
        /// </summary>
        public const string HJGL_PipingClassMenuId = "8IDKGJE2-09B1-1234-VC6D-865CE48F0005";

        /// <summary>
        /// 焊接方法
        /// </summary>
        public const string HJGL_WeldingMethodMenuId = "8IDKGJE2-09B1-4607-BC6D-865CE48F0010";

        /// <summary>
        /// 安装组件
        /// </summary>
        public const string HJGL_ComponentsMenuId = "8IDKGJE2-09B1-4607-BC6D-865CE48F0009";

        /// <summary>
        /// 探伤类型
        /// </summary>
        public const string HJGL_DetectionTypeMenuId = "8IDKGJE2-09B1-4607-BC6D-865CE48F0006";

        /// <summary>
        /// 探伤比例
        /// </summary>
        public const string HJGL_DetectionRateMenuId = "8IDKGJE2-09B1-4607-BC6D-865CE48F0007";

        /// <summary>
        /// 焊缝类型
        /// </summary>
        public const string HJGL_WeldTypeMenuId = "8IDKGJE2-09B1-4607-BC6D-865CE48F0008";

        /// <summary>
        /// 坡口类型
        /// </summary>
        public const string HJGL_GrooveTypeMenuId = "8IDKGJE2-09B1-4607-BC6D-865CE48F0012";

        /// <summary>
        /// 材质定义
        /// </summary>
        public const string HJGL_MaterialMenuId = "8IDKGJE2-09B1-4607-BC6D-865CE48F0013";

        /// <summary>
        /// 介质定义
        /// </summary>
        public const string HJGL_MediumMenuId = "8IDKGJE2-09B1-4607-BC6D-865CE48F0014";

        /// <summary>
        /// 直径寸径对照
        /// </summary>
        public const string HJGL_DNCompareMenuId = "8IDKGJE2-09B1-4607-BC6D-865CE48F0011";

        /// <summary>
        /// 安装组件
        /// </summary>
        public const string HJGL_PipeAssemblyMenuId = "8IDKGJE2-09B1-4607-BC6D-865CE48F0009";

        /// <summary>
        /// 缺陷性质
        /// </summary>
        public const string HJGL_DefectMenuId = "D68AE881-7472-4C48-953D-E13C46D6E2A4";

        /// <summary>
        /// 试压类型
        /// </summary>
        public const string HJGL_PressureMenuId = "8IDKGJE2-09B1-4607-BC6D-865CE48F0015";
        /// <summary>
        /// 焊接位置
        /// </summary>
        public const string HJGL_WeldingLocationMenuId = "8IDKGJE2-09B1-4607-BC6D-865CE48F0015";
        /// <summary>
        /// 试验介质
        /// </summary>
        public const string HJGL_TestMediumMenuId = "2db166e1-9648-42a9-8567-f63b1846a841";
        #endregion

        #region 人员管理
        /// <summary>
        /// 焊工信息
        /// </summary>
        public const string WelderManageMenuId = "1908E4C9-4A63-4A6C-6666-DA4910C2A8C7";
        #endregion

        #region 焊接工艺评定
        /// <summary>
        /// 焊接工艺评定图片
        /// </summary>
        public const string WPQListMenuId = "63CD1C52-1A87-4946-9CA7-8C3C2CB0CA2E";

        /// <summary>
        /// 焊接工艺评定查询
        /// </summary>
        public const string HJGL_WeldingProcedureSearchMenuId = "EFDSFVDE-RTHN-7UMG-6666-5TGED48F8IOL";
        #endregion

        #region 焊接管理

        /// <summary>
        /// 管线信息
        /// </summary>
        public const string HJGL_PipelineMenuId = "32F5CC8C-E0F4-456C-AB88-77E36269FA50";

        /// <summary>
        /// 焊口信息
        /// </summary>
        public const string HJGL_WeldJointMenuId = "32F5CC8C-E0F4-456C-AB88-77E36269FA50";

        /// <summary>
        /// 焊接日常管理
        /// </summary>
        public const string HJGL_WeldReportMenuId = "5TYHMD2F-2582-4DEB-905E-6E9DCFEFBGHO";
        #endregion

        #region 热处理/硬度委托
        /// <summary>
        /// 热处理委托及数据录入
        /// </summary>
        public const string HJGL_HotProessTrustMenuId = "90579BE7-E38C-4CD2-A3BC-755169FF3BB2";

        /// <summary>
        /// 热处理反馈及硬度委托
        /// </summary>
        public const string HJGL_HotProessFeedbackMenuId = "DAA6042B-6E65-49E4-9337-355E4420E25D";

        /// <summary>
        /// 硬度委托录入
        /// </summary>
        public const string HJGL_HotHardManageEditMenuId = "72B3E508-1315-4CC3-6666-E840FA701A0E";

        /// <summary>
        /// 硬度检验结果反馈
        /// </summary>
        public const string HJGL_HardFeedbackMenuId = "B0355AED-E873-4398-963C-07243E296A1D";
        #endregion

        #region 点口管理
        public const string HJGL_PointBatchMenuId = "3ACE25CE-C5CE-4CEC-AD27-0D5CF1DF2F01";
        #endregion

        #region 无损检测委托
        public const string HJGL_TrustBatchMenuId = "0CA58EC2-7934-49B3-A2C3-327FAD27C541";
        #endregion

        #region 无损检测
        public const string HJGL_NDTBatchMenuId = "CDECC461-1897-4D88-BD13-0824E540F565";
        #endregion

        #region 试压包
        /// <summary>
        /// 试压包录入
        /// </summary>
        public const string TestPackageEditMenuId = "1C6F9CA9-FDAC-4CE5-A19C-5536538851E1";

        /// <summary>
        /// 试压前尾项A检查
        /// </summary>
        public const string AItemEndCheckMenuId = "24941EDC-CED6-4176-8CCD-EB5F08156D08";

        /// <summary>
        /// 试压包审核
        /// </summary>
        public const string TestPackageAuditMenuId = "55976B16-2C33-406E-B514-2FE42D031071";

        /// <summary>
        /// 试压前尾项B检查
        /// </summary>
        public const string BItemEndCheckMenuId = "B7EF02DC-45AB-4CFB-ADC8-8340D85D57AD";

        /// <summary>
        /// 试压包完成
        /// </summary>
        public const string TestPackageCompleteMenuId = "82951D78-9029-4F69-A032-00C47551B3E6";
        #endregion

        #endregion

        #endregion

        #region 模版文件原始的虚拟路径
        /// <summary>
        /// 数据导入模版文件原始的虚拟路径
        /// </summary>
        public const string DataInTemplateUrl = "File\\Excel\\DataIn\\数据导入模版.xls";
        /// <summary>
        /// 人员模版文件原始的虚拟路径
        /// </summary>
        public const string PersonTemplateUrl = "File\\Excel\\DataIn\\人员信息模版.xls";
        /// <summary>
        /// 人工时日报模版文件原始的虚拟路径
        /// </summary>
        public const string DayReportTemplateUrl = "File\\Excel\\DataIn\\人工时日报导入模板.xls";
        /// <summary>
        /// 人工时日报模版文件原始的虚拟路径
        /// </summary>
        public const string DayReportTemplateUrl2 = "File\\Excel\\DataIn\\人工时日报导入模板2.xls";
        /// <summary>
        /// 人工时月报模版文件原始的虚拟路径
        /// </summary>
        public const string MonthReportTemplateUrl = "File\\Excel\\DataIn\\人工时月报模板.xls";
        /// <summary>
        /// 百万工时安全统计月报表模板文件原始虚拟路径
        /// </summary>
        public const string MillionsMonthlyReportTemplateUrl = "File\\Excel\\DataIn\\百万工时安全统计月报表模板.xls";
        /// <summary>
        /// 职工伤亡事故原因分析报表模板文件原始虚拟路径
        /// </summary>
        public const string AccidentCauseReportTemplateUrl = "File\\Excel\\DataIn\\职工伤亡事故原因分析报表模板.xls";
        /// <summary>
        /// 安全生产数据季报模板文件原始虚拟路径
        /// </summary>
        public const string SafetyQuarterlyReportTemplateUrl = "File\\Excel\\DataIn\\安全生产数据季报模板.xls";
        /// <summary>
        /// 应急演练开展情况季报表模板文件原始虚拟路径
        /// </summary>
        public const string DrillConductedQuarterlyReportTemplateUrl = "File\\Excel\\DataIn\\应急演练开展情况季报模板.xls";
        /// <summary>
        /// 应急演练工作计划半年报表模板文件原始虚拟路径
        /// </summary>
        public const string DrillPlanHalfYearReportTemplateUrl = "File\\Excel\\DataIn\\应急演练工作计划半年报表模板.xls";
        /// <summary>
        /// 危险源导入模板
        /// </summary>
        public const string HazardListTemplateUrl = "File\\Excel\\DataIn\\危险源导入模板.xls";
        /// <summary>
        /// 集团公司安全监督检查管理办法原始的虚拟路径
        /// </summary>
        public const string Check_CheckInfoTemplateUrl = "File\\Word\\集团公司安全监督检查管理办法.doc";
        /// <summary>
        /// 环境因素危险源导入模板
        /// </summary>
        public const string EnvironmentalTemplateUrl = "File\\Excel\\DataIn\\环境因素危险源导入模板.xls";
        /// <summary>
        /// 人员培训导入模板
        /// </summary>
        public const string TrainRecordTemplateUrl = "File\\Excel\\DataIn\\人员培训导入模板.xls";
        /// <summary>
        /// 考试试题模版文件原始的虚拟路径
        /// </summary>
        public const string TestTrainingTemplateUrl = "File\\Excel\\DataIn\\考试试题模版.xls";
        /// <summary>
        /// 日常巡检导入模板
        /// </summary>
        public const string CheckDayTemplateUrl = "File\\Excel\\DataIn\\日常巡检导入模板.xls";
        /// <summary>
        /// 专项检查导入模板
        /// </summary>
        public const string CheckSpecialTemplateUrl = "File\\Excel\\DataIn\\专项检查导入模板.xls";
        /// <summary>
        /// 综合检查导入模板
        /// </summary>
        public const string CheckColligationTemplateUrl = "File\\Excel\\DataIn\\综合检查导入模板.xls";
        /// <summary>
        /// 开工前检查导入模板
        /// </summary>
        public const string CheckWorkTemplateUrl = "File\\Excel\\DataIn\\开工前检查导入模板.xls";
        /// <summary>
        /// 季节性节假日检查检查导入模板
        /// </summary>
        public const string CheckHolidayTemplateUrl = "File\\Excel\\DataIn\\季节性节假日检查导入模板.xls";
        /// <summary>
        /// 单位信息导入模板
        /// </summary>
        public const string UnitTemplateUrl = "File\\Excel\\DataIn\\单位信息模版.xls";
        /// <summary>
        /// 用户信息导入模板
        /// </summary>
        public const string UserTemplateUrl = "File\\Excel\\DataIn\\用户信息模版.xls";
        #endregion

        #region 初始化上传路径

        /// <summary>
        /// Excel附件路径
        /// </summary>
        public const string ExcelUrl = "File\\Excel\\Temp\\";

        #endregion

        #region 报表对应ID
        /// <summary>
        /// 百万工时安全统计月报表
        /// </summary>
        public const string Information_MillionsMonthlyReportId = "1";
        /// <summary>
        /// 职工伤亡事故原因分析报表
        /// </summary>
        public const string Information_AccidentCauseReportId = "2"; 
        /// <summary>
        /// 安全生产数据季报
        /// </summary>
        public const string Information_SafetyQuarterlyReportId = "3";  
        /// <summary>
        /// 应急演练开展情况季报表
        /// </summary>
        public const string Information_DrillConductedQuarterlyReportId = "4";  
        /// <summary>
        /// 应急演练工作计划半年报表
        /// </summary>
        public const string Information_DrillPlanHalfYearReportId = "5";  
        /// <summary>
        /// 培训记录
        /// </summary>
        public const string TrainRecordReportId = "11";
        /// <summary>
        /// 环境危险源辨识与评价打印报表
        /// </summary>
        public const string EnvironmentalRiskReportId = "12";  
        /// <summary>
        /// 环境危险源辨识与评价（重要环境因素）打印报表
        /// </summary>
        public const string EnvironmentalRiskImportantReportId = "13";
        /// <summary>
        /// 职业健康安全危险源辨识与评价打印报表
        /// </summary>
        public const string HazardListReportId = "14"; 
        /// <summary>
        /// 职业健康安全危险源辨识与评价（重大危险源）打印报表
        /// </summary>
        public const string HazardListImportantReportId = "15";
        /// <summary>
        /// HSSE日志暨管理数据收集打印报表
        /// </summary>
        public const string HSSELogReportId = "16";
        /// <summary>
        /// 分包商上传周报打印报表
        /// </summary>
        public const string SubUploadWeekReportId = "23";
        #endregion

        #region 通用流程定义
        /// <summary>
        /// 待提交
        /// </summary>
        public const string State_0 = "0";
        /// <summary>
        /// 已提交
        /// </summary>
        public const string State_1 = "1";
        /// <summary>
        /// 已完成
        /// </summary>
        public const string State_2 = "2";
        /// <summary>
        /// 已完成/已闭环
        /// </summary>
        public const string State_3 = "3";
        /// <summary>
        /// 已完成/已上报
        /// </summary>
        public const string State_4 = "4";
        /// <summary>
        /// 已完成/已上报
        /// </summary>
        public const string State_5 = "5";
        /// <summary>
        /// 重新申请
        /// </summary>
        public const string State_R = "-1";
        /// <summary>
        /// 作废
        /// </summary>
        public const string State_C = "-2";
        #endregion

        #region APP专项检查流程定义
        /// <summary>
        /// 重新编制
        /// </summary>
        public const string APPCheckSpecial_ReCompile = "0";

        /// <summary>
        /// 编制
        /// </summary>
        public static string APPCheckSpecial_Compile = "1";

        /// <summary>
        /// 办理中
        /// </summary>
        public static string APPCheckSpecial_Check = "2";

        /// <summary>
        /// 审批完成
        /// </summary>
        public static string APPCheckSpecial_ApproveCompleted = "3";

        #endregion

        #region 移动端菜单
        /// <summary>
        /// 安全巡检
        /// </summary>
        public const string APP_HazardRegisterMenuId = "F21FFCAA-872A-4995-BB5B-E9C430950845";
        #endregion

        #region 质量
        #region  质量管理
        #region 基础设置
        /// <summary>
        /// 项目环境设置
        /// </summary>
        public const string CQMSSysSetMenuId = "9c62332c-d31f-4575-a323-49537bf335e8";
        #endregion

        #region WBS定制管理
        /// <summary>
        /// 施工WBS基础数据库
        /// </summary>
        public const string ControlItemInitSetMenuId = "DCVVBDE3-1B48-4615-FCD0-VDBCDS3ET4D9";

        /// <summary>
        /// 项目WBS基础数据库
        /// </summary>
        public const string ControlItemProjectSetMenuId = "F0E4A87F-0D1E-499D-A9EF-9D42ABF4422F";

        /// <summary>
        /// 装置/单元包含工作包设置
        /// </summary>
        public const string InstallToPackageMenuId = "7BA846E7-D9B2-4F08-9098-DF4DBD676611";

        /// <summary>
        /// 项目施工WBS定制
        /// </summary>
        public const string ControlItemAndCycleMenuId = "7BA846E7-D9B2-4F08-9098-DF4DBD654789";

        /// <summary>
        /// 项目施工WBS展示
        /// </summary>
        public const string ControlItemAndCycleShowMenuId = "94217974-201A-4D4F-A832-D9477ACD63A5";

        #endregion

        #region 工序验收
        /// <summary>
        /// 实体验收记录
        /// </summary>
        public const string SpotCheckMenuId = "CC43E208-335D-4AE3-A1CE-B2D8510AE854";

        /// <summary>
        /// 工序资料记录
        /// </summary>
        public const string SpotDataCheckMenuId = "8317C0F7-3A69-48AB-87E5-03E96C543B1C";
        #endregion

        #region 质量巡检记录
        /// <summary>
        /// 质量巡检记录
        /// </summary>
        public const string CheckListMenuId = "B3E99BD9-FDC7-4F15-8C3C-A7821AC9E306";
        #endregion

        #region 质量共检
        /// <summary>
        /// 质量共检
        /// </summary>
        public const string JointCheckMenuId = "E6703F32-4E42-4D03-84B3-5EC0E8E48E0F";
        #endregion

        #region 工程联络单
        /// <summary>
        /// 工程联络单
        /// </summary>
        public const string TechnicalContactListMenuId = "87867EA8-576A-4166-AE7A-7416115DF30B";
        #endregion

        #region 工作联系单
        /// <summary>
        /// 工作联系单
        /// </summary>
        public const string WorkContactMenuId = "43E96F2A-9497-4C37-998F-0EB324000609";
        #endregion

        #region 设计变更
        /// <summary>
        /// 设计变更
        /// </summary>
        public const string DesignMenuId = "38F11FC6-E9F0-4ABD-BD78-BB1807EA3CE3";
        #endregion

        #region 施工方案及审查
        /// <summary>
        /// 施工方案及审查
        /// </summary>
        public const string CQMSConstructSolutionMenuId = "91C4BFDB-0A51-4992-99CC-EB4EC185593D";
        #endregion

        #region 施工图纸管理
        /// <summary>
        /// 施工图纸管理
        /// </summary>
        public const string DrawMenuId = "3A9039B2-20DC-47FA-A943-E3604FBBF699";
        #endregion

        #region 检试验设备及测量器具
        /// <summary>
        /// 检试验设备及测量器具
        /// </summary>
        public const string CheckEquipmentMenuId = "BB8EB284-A014-442E-A1D7-956AA11272AE";
        #endregion

        #region 质量月报
        /// <summary>
        /// 质量月报
        /// </summary>
        public const string CheckMonthMenuId = "786864C0-2875-49CD-B55E-0B5C8253295D";
        #endregion

        #region 文件柜
        /// <summary>
        /// 重要文件
        /// </summary>
        public const string FileCabinetMenuId = "03CEB246-0CE5-48CB-9D6B-1B84AC2E27B3";
        #endregion
        #endregion

        #region 质量流程定义
        #region 工序验收流程定义
        /// <summary>
        /// 重新整理
        /// </summary>
        public const string SpotCheck_ReCompile = "0";

        /// <summary>
        /// 编制
        /// </summary>
        public const string SpotCheck_Compile = "1";

        /// <summary>
        /// 分包负责人确认
        /// </summary>
        public static string SpotCheck_Audit1 = "2";

        /// <summary>
        /// 总包专业工程师确认
        /// </summary>
        public static string SpotCheck_Audit2 = "3";

        /// <summary>
        /// 监理专业工程师确认
        /// </summary>
        public static string SpotCheck_Audit3 = "4";

        /// <summary>
        /// 建设单位确认
        /// </summary>
        public static string SpotCheck_Audit4 = "5";

        /// <summary>
        /// 分包专业工程师上传资料
        /// </summary>
        public static string SpotCheck_Audit5 = "6";

        /// <summary>
        /// 总包专业工程师确认
        /// </summary>
        public static string SpotCheck_Audit6 = "7";

        /// <summary>
        /// 分包负责人确认
        /// </summary>
        public static string SpotCheck_Audit7 = "Q";

        /// <summary>
        /// 审批完成
        /// </summary>
        public static string SpotCheck_Complete = "8";

        /// <summary>
        /// 分包专业工程师重新上传资料
        /// </summary>
        public static string SpotCheck_Audit5R = "9";

        /// <summary>
        /// 资料验收中
        /// </summary>
        public static string SpotCheck_Z = "Z";

        #endregion

        #region 质量巡检流程定义
        /// <summary>
        /// 重新编制
        /// </summary>
        public const string CheckControl_ReCompile = "0";//总包

        /// <summary>
        /// 总包专业工程师编制
        /// </summary>
        public const string CheckControl_Compile = "1";//总包

        /// <summary>
        /// 总包负责人审批
        /// </summary>
        public static string CheckControl_Audit1 = "2";//总包

        /// <summary>
        /// 分包专业工程师回复
        /// </summary>
        public static string CheckControl_Audit2 = "3";//分包

        /// <summary>
        /// 分包负责人审批
        /// </summary>
        public static string CheckControl_Audit3 = "4";//分包

        /// <summary>
        /// 总包专业工程师确认
        /// </summary>
        public static string CheckControl_Audit4 = "5";//总包

        /// <summary>
        /// 总包负责人审批
        /// </summary>
        public static string CheckControl_Audit5 = "6";//总包

        /// <summary>
        /// 审批完成
        /// </summary>
        public static string CheckControl_Complete = "7";

        /// <summary>
        /// 分包专业工程师重新回复
        /// </summary>
        public const string CheckControl_ReCompile2 = "8";//分包

        #endregion

        #region 质量共检流程定义
        /// <summary>
        /// 重新整理
        /// </summary>
        public const string JointCheck_ReCompile = "0";

        /// <summary>
        /// 编制
        /// </summary>
        public const string JointCheck_Compile = "1";

        /// <summary>
        /// 分包专工回复
        /// </summary>
        public static string JointCheck_Audit1 = "2";

        /// <summary>
        /// 分包负责人审批
        /// </summary>
        public static string JointCheck_Audit2 = "3";

        /// <summary>
        /// 总包专工回复
        /// </summary>
        public static string JointCheck_Audit3 = "4";

        /// <summary>
        /// 总包负责人审批
        /// </summary>
        public static string JointCheck_Audit4 = "5";

        /// <summary>
        /// 审批完成
        /// </summary>
        public static string JointCheck_Complete = "6";

        /// <summary>
        /// 分包专工重新回复
        /// </summary>
        public static string JointCheck_Audit1R = "7";

        /// <summary>
        /// 整改中
        /// </summary>
        public static string JointCheck_Z = "Z";

        #endregion

        #region 工程联络单流程定义
        /// <summary>
        /// 重新编制
        /// </summary>
        public const string TechnicalContactList_ReCompile = "0";

        /// <summary>
        /// 编制
        /// </summary>
        public const string TechnicalContactList_Compile = "1";

        /// <summary>
        /// 分包负责人审批
        /// </summary>
        public static string TechnicalContactList_Audit1 = "2";

        /// <summary>
        /// 总包专工确认
        /// </summary>
        public static string TechnicalContactList_Audit2 = "3";

        /// <summary>
        /// 总包负责人审批
        /// </summary>
        public static string TechnicalContactList_Audit3 = "4";

        /// <summary>
        /// 总包设计人员回复
        /// </summary>
        public static string TechnicalContactList_Audit4 = "5";

        /// <summary>
        /// 分包专工回复
        /// </summary>
        public static string TechnicalContactList_Audit6 = "7";

        /// <summary>
        /// 审批完成
        /// </summary>
        public static string TechnicalContactList_Complete = "8";

        /// <summary>
        /// 分包专工重新回复
        /// </summary>
        public static string TechnicalContactList_Audit6R = "F";

        /// <summary>
        /// 总包专工重新回复
        /// </summary>
        public static string TechnicalContactList_Audit2R = "Z";

        /// <summary>
        /// 总包设计人员重新回复
        /// </summary>
        public static string TechnicalContactList_Audit4R = "J";

        /// <summary>
        /// 总包专工回复
        /// </summary>
        public static string TechnicalContactList_Audit2H = "H";
        #endregion

        #region 工作联系单流程定义
        /// <summary>
        /// 重新编制
        /// </summary>
        public const string WorkContact_ReCompile = "0";

        /// <summary>
        /// 编制
        /// </summary>
        public static string WorkContact_Compile = "1";

        /// <summary>
        /// 分包负责人审核
        /// </summary>
        public static string WorkContact_Audit1 = "2";

        /// <summary>
        /// 总包专工回复
        /// </summary>
        public static string WorkContact_Audit2 = "3";

        /// <summary>
        /// 总包负责人审核
        /// </summary>
        public static string WorkContact_Audit3 = "4";

        /// <summary>
        /// 审核完结
        /// </summary>
        public static string WorkContact_Complete = "5";

        /// <summary>
        /// 分包专工重新回复
        /// </summary>
        public static string WorkContact_Audit1R = "6";

        /// <summary>
        /// 总包专工重新回复
        /// </summary>
        public static string WorkContact_Audit2R = "7";

        /// <summary>
        /// 分包专工回复
        /// </summary>
        public static string WorkContact_Audit4 = "8";

        #endregion

        #region 设计变更流程定义
        /// <summary>
        /// 重新编制
        /// </summary>
        public const string Design_ReCompile = "0";

        /// <summary>
        /// 变更录入
        /// </summary>
        public const string Design_Compile = "1";

        /// <summary>
        /// 变更分析
        /// </summary>
        public static string Design_Audit1 = "2";

        /// <summary>
        /// 变更分析审核
        /// </summary>
        public static string Design_Audit2 = "3";

        /// <summary>
        /// 变更实施
        /// </summary>
        public static string Design_Audit3 = "4";

        /// <summary>
        /// 变更实施审核
        /// </summary>
        public static string Design_Audit4 = "5";

        /// <summary>
        /// 审批完成
        /// </summary>
        public static string Design_Complete = "6";

        #endregion

        #region 施工方案及审查流程定义

        /// <summary>
        /// 重报
        /// </summary>
        public const string CQMSConstructSolution_ReCompile = "0";

        /// <summary>
        /// 编制
        /// </summary>
        public static string CQMSConstructSolution_Compile = "1";

        /// <summary>
        /// 会签
        /// </summary>
        public static string CQMSConstructSolution_Audit = "2";

        /// <summary>
        /// 审批完成
        /// </summary>
        public static string CQMSConstructSolution_Complete = "3";

        #endregion

        #region 检试验设备及测量器具流程定义
        /// <summary>
        /// 重新编制
        /// </summary>
        public const string CheckEquipment_ReCompile = "0";

        /// <summary>
        /// 编制
        /// </summary>
        public const string CheckEquipment_Compile = "1";

        /// <summary>
        /// 审核
        /// </summary>
        public static string CheckEquipment_Approve = "2";

        /// <summary>
        /// 审批完成
        /// </summary>
        public static string CheckEquipment_Complete = "3";
        #endregion
        #endregion

        #region 质量模板文件路径
        /// <summary>
        /// 工程联络单的虚拟路径
        /// </summary>
        public const string TechnicalContactListTemplateUrl = "File\\Word\\CQMS\\Check\\工程联络单.doc";
        /// <summary>
        /// 工程联络单2的虚拟路径
        /// </summary>
        public const string TechnicalContactListTemplateUrl2 = "File\\Word\\CQMS\\Check\\工程联络单2.doc";
        /// <summary>
        /// 施工方案模版文件原始的虚拟路径
        /// </summary>
        public const string CQMSConstructSolutionTemplateUrl3 = "File\\Word\\CQMS\\Solution\\施工方案模板.doc";
        /// <summary>
        /// 施工组织设计模版文件原始的虚拟路径
        /// </summary>
        public const string CQMSConstructSolutionTemplateUrl1 = "File\\Word\\CQMS\\Solution\\施工组织设计模板.doc";
        /// <summary>
        /// 工作联系单2的虚拟路径
        /// </summary>
        public const string WorkContactTemplateUrl2 = "File\\Word\\CQMS\\Check\\工作联系单2.doc";
        /// <summary>
        /// 工作联系单的虚拟路径
        /// </summary>
        public const string WorkContactTemplateUrl = "File\\Word\\CQMS\\Check\\工作联系单.doc";
        /// <summary>
        /// 施工方案的虚拟路径
        /// </summary>
        public const string ConstructSolutionTemplateUrl = "File\\Word\\CQMS\\Check\\施工方案.doc";
        /// <summary>
        /// 质量月报的虚拟路径
        /// </summary>
        public const string CheckMonthTemplateUrl = "File\\Word\\CQMS\\Check\\质量月报.doc";
        /// <summary>
        /// 设计变更的虚拟路径
        /// </summary>
        public const string DesignTemplateUrl = "File\\Word\\CQMS\\Check\\设计变更.doc";
        /// <summary>
        /// 施工质量检查记录的虚拟路径
        /// </summary>
        public const string JointCheckTemplateUrl = "File\\Word\\CQMS\\Check\\施工质量检查记录.doc";
        /// <summary>
        /// 施工质量问题整改单的虚拟路径
        /// </summary>
        public const string CheckListTemplateUrl = "File\\Word\\CQMS\\Check\\施工质量问题整改单.doc";
        /// <summary>
        /// 施工质量问题整改单2的虚拟路径
        /// </summary>
        public const string CheckListTemplateUrl2 = "File\\Word\\CQMS\\Check\\施工质量问题整改单2.doc";
        /// <summary>
        /// 质量共检通知单的虚拟路径
        /// </summary>
        public const string SpotCheckTemplateUrl = "File\\Word\\CQMS\\Check\\质量共检通知单.doc";
        #endregion

        #endregion

        #region  焊接管理
        /// <summary>
        /// 焊工照片上传路径
        /// </summary>
        public static string WelderFilePath = "FileUpload\\Welder\\";
        /// <summary>
        /// PDMS导入模版文件原始的虚拟路径
        /// </summary>
        public const string WPQTemplateUrl = "File\\Excel\\焊接工艺评定导入模版.xls";

        #endregion
    }
}