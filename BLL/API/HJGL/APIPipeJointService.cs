using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public static class APIPipeJointService
    {
        #region 获取管线信息
        /// <summary>
        ///  根据单位工程ID获取管线列表
        /// </summary>
        /// <param name="unitWrokId"></param>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getPipelineList(string unitWrokId)
        {
            var getDataLists = (from x in Funs.DB.HJGL_Pipeline
                                where x.UnitWorkId == unitWrokId
                                orderby x.PipelineCode
                                select new Model.BaseInfoItem
                                {
                                    BaseInfoId = x.PipelineId,
                                    BaseInfoCode = x.PipelineCode

                                }
                                ).ToList();
            return getDataLists;
        }
        #endregion 

        #region 获取焊口信息
        /// <summary>
        ///  根据管线ID获取焊口列表
        /// </summary>
        /// <param name="pipeLineId"></param>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getWeldJointList(string pipeLineId)
        {
            var getDataLists = (from x in Funs.DB.HJGL_WeldJoint
                                where x.PipelineId == pipeLineId && x.WeldingDailyId == null
                                orderby x.WeldJointCode
                                select new Model.BaseInfoItem
                                {
                                    BaseInfoId = x.WeldJointId,
                                    BaseInfoCode = x.WeldJointCode

                                }
                                ).ToList();
            return getDataLists;
        }
        #endregion


        #region 获取焊工列表
        /// <summary>
        ///  根据管线ID获取焊口列表
        /// </summary>
        /// <param name="unitWorkId"></param>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getWelderList(string unitWorkId)
        {
            var p = Funs.DB.WBS_UnitWork.Where(x => x.UnitWorkId == unitWorkId).FirstOrDefault();
            var getDataLists = (from x in Funs.DB.SitePerson_Person
                                where x.UnitId == p.UnitId
                                orderby x.WelderCode
                                select new Model.BaseInfoItem
                                {
                                    BaseInfoId = x.PersonId,
                                    BaseInfoCode = x.WelderCode

                                }
                                ).ToList();
            return getDataLists;
        }
        #endregion



        #region 获取焊口详细信息
        /// <summary>
        ///  根据焊口ID获取焊口详细信息
        /// </summary>
        /// <param name="weldJointId"></param>
        /// <returns></returns>
        public static Model.WeldJointItem getWeldJointInfo(string weldJointId)
        {
            var getDateInfo = from x in Funs.DB.HJGL_WeldJoint
                              where x.WeldJointId == weldJointId

                              select new Model.WeldJointItem
                              {
                                  WeldJointId = x.WeldJointId,
                                  WeldJointCode = x.WeldJointCode,
                                  PipelineId = x.PipelineId,
                                  PipelineCode = x.PipelineCode,
                                  JointArea = x.JointArea,
                                  JointAttribute = x.JointAttribute,
                                  WeldingMode=x.WeldingMode,
                                  Size = x.Size,
                                  Dia = x.Dia,
                                  Thickness = x.Thickness,
                                  WeldingMethodCode = Funs.DB.Base_WeldingMethod.First(y => y.WeldingMethodId == x.WeldingMethodId).WeldingMethodCode,
                                  IsHotProess = x.IsHotProess == true ? "是" : "否",
                                  AttachUrl = x.JointAttribute

                              };
            return getDateInfo.FirstOrDefault();
        }
        #endregion

        /// <summary>
        /// 保存预提交日报
        /// </summary>
        /// <param name="addItem"></param>
        public static void SavePreWeldingDaily(Model.WeldJointItem addItem)
        {
            Model.SGGLDB db = Funs.DB;
            string projectId = string.Empty;
            string unitId = string.Empty;
            var p = Funs.DB.WBS_UnitWork.Where(x => x.UnitWorkId == addItem.UnitWorkId).FirstOrDefault();

            if (p != null)
            {
                projectId = p.ProjectId;
                unitId = p.UnitId;
            }

            Model.HJGL_PreWeldingDaily newP = new Model.HJGL_PreWeldingDaily
            {
                PreWeldingDailyId = SQLHelper.GetNewID(),
                ProjectId = projectId,
                UnitWorkId = addItem.UnitWorkId,
                UnitId = unitId,
                WeldJointId = addItem.WeldJointId,
                WeldingDate = DateTime.Now,
                BackingWelderId = addItem.BackingWelderId,
                CoverWelderId = addItem.CoverWelderId,
                JointAttribute = addItem.JointAttribute,
                WeldingMode = addItem.WeldingMode,
                AttachUrl = addItem.AttachUrl

            };
            db.HJGL_PreWeldingDaily.InsertOnSubmit(newP);
            db.SubmitChanges();

        }

    }
}
