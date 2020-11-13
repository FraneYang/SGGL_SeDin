namespace BLL
{
    using System.Web.UI.WebControls;
    using Model;
    using BLL;
    using System.Collections.Generic;
    
    /// <summary>
    /// 自定义下拉框通用类
    /// </summary>
    public static class DropListService
    {
        #region 公共平台

        /// <summary>
        /// 获取模块
        /// </summary>
        /// <returns></returns>
        //public static ListItem[] GetSystemList()
        //{
        //    ListItem[] item = new ListItem[2];
        //    item[0] = new ListItem("施工综合平台", BLL.Const.System_1);
        //    item[1] = new ListItem("焊接管理", BLL.Const.System_6);
        //    return item;
        //}

        #region 是否选择下拉框
        /// <summary>
        ///  是否选择下拉框
        /// </summary>
        /// <returns></returns>
        //public static ListItem[] IsTrueOrFalseDrpList()
        //{
        //    ListItem[] lis = new ListItem[2];
        //    lis[0] = new ListItem("是", BLL.Const._True);
        //    lis[1] = new ListItem("否", BLL.Const._False);
        //    return lis;
        //}
        #endregion
   
        #endregion

        #region HJGL 焊接管理常量下拉框
        #region 本部基础信息
  
        /// <summary>
        /// 焊条/焊丝
        /// </summary>
        /// <returns></returns>
        public static ListItem[] HJGL_ConsumablesTypeList()
        {
            ListItem[] lis = new ListItem[3];
            lis[0] = new ListItem("焊丝", "1");
            lis[1] = new ListItem("焊条", "2");
            lis[2] = new ListItem("焊剂", "3");
            return lis;
        }
        #endregion

        /// <summary>
        /// 查询钢材类型下拉列表值
        /// </summary>
        /// <returns></returns>
        public static ListItem[] HJGL_GetSteTypeList()
        {
            ListItem[] list = new ListItem[7];
            list[0] = new ListItem("碳钢", "1");
            list[1] = new ListItem("不锈钢", "2");
            list[2] = new ListItem("铬钼钢", "3");
            list[3] = new ListItem("低合金钢", "4");
            list[4] = new ListItem("镍合金钢", "5");
            list[5] = new ListItem("钛合金钢", "6");
            list[6] = new ListItem("其他", "7");
            return list;
        }

        /// <summary>
        /// 探伤类型对应系统下拉框
        /// </summary>
        /// <returns></returns>
        public static ListItem[] HJGL_GetTestintTypeList()
        {
            ListItem[] list = new ListItem[5];
            list[0] = new ListItem("射线检测", "射线检测");
            list[1] = new ListItem("磁粉检测", "磁粉检测");
            list[2] = new ListItem("渗透检测", "渗透检测");
            list[3] = new ListItem("超声波检测", "超声波检测");
            list[4] = new ListItem("光谱检测", "光谱检测");
            return list;
        }

        #region 现场焊接
       
        /// <summary>
        /// 焊口属性
        /// </summary>
        /// <returns></returns>
        public static ListItem[] HJGL_JointAttribute()
        {
            ListItem[] list = new ListItem[2];
            list[0] = new ListItem("活动口", "活动口");
            list[1] = new ListItem("固定口", "固定口");
            return list;
        }
        #endregion

        public static ListItem[] HJGL_JointArea()
        {
            ListItem[] list = new ListItem[2];
            list[0] = new ListItem("安装", "安装");
            list[1] = new ListItem("预制", "预制");
            return list;
        }

        /// <summary>
        /// 机动化程度
        /// </summary>
        /// <returns></returns>
        public static ListItem[] HJGL_WeldingMode()
        {
            ListItem[] list = new ListItem[2];
            list[0] = new ListItem("手动", "手动");
            list[1] = new ListItem("机动/自动", "机动/自动");
            return list;
        }
        #endregion

        #region 安全
        #region 月报审核
        /// <summary>
        /// 获取模块
        /// </summary>
        /// <returns></returns>
        public static ListItem[] GetMonthReportStepList(string states)
        {
            if (states == Const.State_0 || string.IsNullOrEmpty(states)) ///待提交
            {
                ListItem[] list = new ListItem[2];
                list[0] = new ListItem("安全总监", Const.State_1);
                list[1] = new ListItem("项目经理", Const.State_2);
                return list;
            }
            else if (states == Const.State_1) /// 待安全
            {
                ListItem[] list = new ListItem[1];
                list[0] = new ListItem("项目经理", Const.State_2);
                return list;
            }
            else if (states == Const.State_2)
            {
                ListItem[] list = new ListItem[1];
                list[0] = new ListItem("审核完成", Const.State_3);
                return list;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #endregion
    }
}
