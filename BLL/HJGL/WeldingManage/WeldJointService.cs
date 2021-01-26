using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web.UI.WebControls;

namespace BLL
{
    public static class WeldJointService
    {
        /// <summary>
        /// 根据焊口Id获取焊口信息
        /// </summary>
        /// <param name="weldJointId"></param>
        /// <returns></returns>
        public static Model.HJGL_WeldJoint GetWeldJointByWeldJointId(string weldJointId)
        {
            return Funs.DB.HJGL_WeldJoint.FirstOrDefault(e => e.WeldJointId == weldJointId);
        }

        /// <summary>
        /// 判断焊口号是否已存在
        /// </summary>
        /// <param name="isoNo"></param>
        /// <returns></returns>
        public static bool IsExistWeldJointCode(string weldJointCode, string pipelineId, string weldJointId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.HJGL_WeldJoint jot = null;
            if (!string.IsNullOrEmpty(weldJointId))
            {
                jot = Funs.DB.HJGL_WeldJoint.FirstOrDefault(x => x.WeldJointCode == weldJointCode && x.PipelineId == pipelineId && x.WeldJointId != weldJointId);
            }
            else
            {
                jot = Funs.DB.HJGL_WeldJoint.FirstOrDefault(x => x.WeldJointCode == weldJointCode && x.PipelineId == pipelineId);
            }

            if (jot != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 根据焊接日报获取焊口数
        /// </summary>
        /// <param name="weldingDailyId"></param>
        /// <returns></returns>
        public static List<Model.HJGL_WeldJoint> GetWeldlinesByWeldingDailyId(string weldingDailyId)
        {
            var q = (from x in Funs.DB.HJGL_WeldJoint where x.WeldingDailyId == weldingDailyId orderby x.PipelineId, x.WeldJointCode select x).ToList();
            return q;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="weldJoint"></param>
        public static void AddWeldJoint(Model.HJGL_WeldJoint weldJoint)
        {
            Model.SGGLDB db = Funs.DB;
            Model.HJGL_WeldJoint newWeldJoint = new Model.HJGL_WeldJoint
            {
                WeldJointId = SQLHelper.GetNewID(typeof(Model.HJGL_WeldJoint)),
                ProjectId = weldJoint.ProjectId,
                PipelineId = weldJoint.PipelineId,
                PipelineCode = weldJoint.PipelineCode,
                WeldJointCode = weldJoint.WeldJointCode,
                WeldTypeId = weldJoint.WeldTypeId,
                Material1Id = weldJoint.Material1Id,
                Material2Id = weldJoint.Material2Id,
                Thickness = weldJoint.Thickness,
                Dia = weldJoint.Dia,
                Size = weldJoint.Size,
                DetectionTypeId = weldJoint.DetectionTypeId,
                JointArea = weldJoint.JointArea,
                WeldingMethodId = weldJoint.WeldingMethodId,
                WeldingLocationId = weldJoint.WeldingLocationId,
                JointAttribute = weldJoint.JointAttribute,
                IsHotProess = weldJoint.IsHotProess,
                WeldingWire = weldJoint.WeldingWire,
                WeldingRod = weldJoint.WeldingRod,
                GrooveTypeId = weldJoint.GrooveTypeId,
                Specification = weldJoint.Specification,
                WPQId = weldJoint.WPQId,
                HeartNo1 = weldJoint.HeartNo1,
                HeartNo2 = weldJoint.HeartNo2,
               
                Components1Id = weldJoint.Components1Id,
                Components2Id = weldJoint.Components2Id,
                PreTemperature = weldJoint.PreTemperature,
                Remark = weldJoint.Remark,
            };

            db.HJGL_WeldJoint.InsertOnSubmit(newWeldJoint);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="weldJoint"></param>
        public static void UpdateWeldJoint(Model.HJGL_WeldJoint weldJoint)
        {
            Model.SGGLDB db = Funs.DB;
            Model.HJGL_WeldJoint newWeldJoint = db.HJGL_WeldJoint.FirstOrDefault(e => e.WeldJointId == weldJoint.WeldJointId);
            if (newWeldJoint != null)
            {
                newWeldJoint.WeldJointCode = weldJoint.WeldJointCode;
                newWeldJoint.ProjectId = weldJoint.ProjectId;
                newWeldJoint.PipelineCode = weldJoint.PipelineCode;
                newWeldJoint.WPQId = weldJoint.WPQId;
                newWeldJoint.WeldTypeId = weldJoint.WeldTypeId;
                newWeldJoint.Material1Id = weldJoint.Material1Id;
                newWeldJoint.Material2Id = weldJoint.Material2Id;
                newWeldJoint.Thickness = weldJoint.Thickness;
                newWeldJoint.Dia = weldJoint.Dia;
                newWeldJoint.Size = weldJoint.Size;
                newWeldJoint.DetectionTypeId = weldJoint.DetectionTypeId;
                newWeldJoint.JointArea = weldJoint.JointArea;
                newWeldJoint.WeldingMethodId = weldJoint.WeldingMethodId;
                newWeldJoint.IsHotProess = weldJoint.IsHotProess;
                newWeldJoint.WeldingLocationId = weldJoint.WeldingLocationId;
                newWeldJoint.JointAttribute = weldJoint.JointAttribute;
                newWeldJoint.WeldingWire = weldJoint.WeldingWire;
                newWeldJoint.WeldingRod = weldJoint.WeldingRod;
                newWeldJoint.GrooveTypeId = weldJoint.GrooveTypeId;
                newWeldJoint.HeartNo1 = weldJoint.HeartNo1;
                newWeldJoint.HeartNo2 = weldJoint.HeartNo2;
                newWeldJoint.Components1Id = weldJoint.Components1Id;
                newWeldJoint.Components2Id = weldJoint.Components2Id;
                newWeldJoint.BackingWelderId = weldJoint.BackingWelderId;
                newWeldJoint.CoverWelderId = weldJoint.CoverWelderId;
                newWeldJoint.WeldingDailyId = weldJoint.WeldingDailyId;
                newWeldJoint.WeldingMode = weldJoint.WeldingMode;
                newWeldJoint.WeldingDailyCode = weldJoint.WeldingDailyCode;
                newWeldJoint.Specification = weldJoint.Specification;
                newWeldJoint.PreTemperature = weldJoint.PreTemperature;
                newWeldJoint.Remark = weldJoint.Remark;
                newWeldJoint.AttachUrl = weldJoint.AttachUrl;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 回写焊口信息
        /// </summary>
        /// <param name="weldJoint"></param>
        public static void WriteBackWeldJoint(Model.HJGL_WeldJoint weldJoint)
        {
            Model.SGGLDB db = Funs.DB;
            Model.HJGL_WeldJoint newWeldJoint = db.HJGL_WeldJoint.FirstOrDefault(e => e.WeldJointId == weldJoint.WeldJointId);
            if (newWeldJoint != null)
            {
                newWeldJoint.BackingWelderId = weldJoint.BackingWelderId;
                newWeldJoint.CoverWelderId = weldJoint.CoverWelderId;
                newWeldJoint.WeldingDailyId = weldJoint.WeldingDailyId;
                newWeldJoint.WeldingDailyCode = weldJoint.WeldingDailyCode;
                newWeldJoint.WeldingLocationId = weldJoint.WeldingLocationId;
                newWeldJoint.JointAttribute = weldJoint.JointAttribute;
            }
        }

        /// <summary>
        /// 根据主键删除焊口信息
        /// </summary>
        /// <param name="weldJointId"></param>
        public static void DeleteWeldJointById(string weldJointId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.HJGL_WeldJoint weldline = db.HJGL_WeldJoint.FirstOrDefault(e => e.WeldJointId == weldJointId);
            if (weldline != null)
            {
                db.HJGL_WeldJoint.DeleteOnSubmit(weldline);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 更新焊口号 固定焊口号后 +G
        /// </summary>
        /// <param name="jotId">焊口id</param>
        /// <param name="jointAttribute">焊口属性</param>
        /// <param name="operateState">日报操作（增加、删除）</param>
        public static void UpdateWeldJointAddG(string weldJointId, string jointAttribute, string operateState)
        {
            Model.SGGLDB db = Funs.DB;
            if (operateState == Const.BtnDelete || jointAttribute != "固定口")
            {
                Model.HJGL_WeldJoint deleteWeldJoint = db.HJGL_WeldJoint.FirstOrDefault(e => e.WeldJointId == weldJointId);
                if (deleteWeldJoint.WeldJointCode.Last() == 'G')
                {
                    deleteWeldJoint.WeldJointCode = deleteWeldJoint.WeldJointCode.Substring(0, deleteWeldJoint.WeldJointCode.Length - 1);
                    db.SubmitChanges();
                }
            }
            else
            {
                Model.HJGL_WeldJoint addJointInfo = db.HJGL_WeldJoint.FirstOrDefault(e => e.WeldJointId == weldJointId);
                if (addJointInfo.WeldJointCode.Last() != 'G')
                {
                    addJointInfo.WeldJointCode += "G";
                }

                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键获取焊口信息视图
        /// </summary>
        /// <param name="weldJointId"></param>
        /// <returns></returns>
        public static Model.View_HJGL_WeldJoint GetViewWeldJointById(string weldJointId)
        {
            Model.SGGLDB db = Funs.DB;
            return db.View_HJGL_WeldJoint.FirstOrDefault(e => e.WeldJointId == weldJointId);
        }

        /// <summary>
        /// 根据焊接日报获取焊口数
        /// </summary>
        /// <param name="weldingDailyId"></param>
        /// <returns></returns>
        public static List<Model.HJGL_WeldJoint> GetWeldJointsByWeldingDailyId(string weldingDailyId)
        {
            var q = (from x in Funs.DB.HJGL_WeldJoint where x.WeldingDailyId == weldingDailyId orderby x.PipelineId, x.WeldJointCode select x).ToList();
            return q;
        }

        /// <summary>
        /// 根据焊接日报获取焊口数
        /// </summary>
        /// <param name="dreportId"></param>
        /// <returns></returns>
        public static List<Model.View_HJGL_WeldJoint> GetViewWeldJointsByWeldingDailyId(string weldingDailyId)
        {
            var q = (from x in Funs.DB.View_HJGL_WeldJoint where x.WeldingDailyId == weldingDailyId orderby x.PipelineCode, x.WeldJointCode select x).ToList();
            return q;
        }

        /// <summary>
        /// 获取当前焊工工作量是否超过60寸的焊接人员
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="unitId">单位id</param>
        /// <param name="jot_Id">焊口ID</param>
        /// <param name="jotDate">焊接日期</param>
        /// <returns></returns>
        public static bool GetWelderLimitDN(string projectId, string welderId, DateTime weldingDate)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var jots = from x in db.HJGL_WeldJoint
                           join y in db.HJGL_WeldingDaily on x.WeldingDailyId equals y.WeldingDailyId
                           where x.ProjectId == projectId && y.WeldingDate == weldingDate && (welderId == x.CoverWelderId || welderId == x.BackingWelderId)
                           select x;
                decimal? count = jots.Sum(x => x.Size);
                if (count >= 60)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }

        /// <summary>
        /// 根据管线ID获取焊口信息
        /// </summary>
        /// <param name="pipelineId"></param>
        /// <returns></returns>
        public static List<Model.View_HJGL_WeldJoint> GetViewWeldJointsByPipelineId(string pipelineId)
        {
            var q = (from x in Funs.DB.View_HJGL_WeldJoint where x.PipelineId == pipelineId orderby x.WeldJointCode select x).ToList();
            return q;
        }
    }
}
