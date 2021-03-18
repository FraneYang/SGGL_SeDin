using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web.UI.WebControls;

namespace BLL
{
    public static class PipelineService
    {
        /// <summary>
        /// 根据管线ID获取管线信息
        /// </summary>
        /// <param name="pipelineName"></param>
        /// <returns></returns>
        public static Model.HJGL_Pipeline GetPipelineByPipelineId(string pipelineId)
        {
            return Funs.DB.HJGL_Pipeline.FirstOrDefault(e => e.PipelineId == pipelineId);
        }

        /// <summary>
        /// 根据管线ID获取管线信息
        /// </summary>
        /// <param name="pipelineName"></param>
        /// <returns></returns>
        public static Model.View_HJGL_Pipeline GetViewPipelineByPipelineId(string pipelineId)
        {
            return Funs.DB.View_HJGL_Pipeline.FirstOrDefault(e => e.PipelineId == pipelineId);
        }

        /// <summary>
        /// 根据管线Code获取管线信息
        /// </summary>
        /// <param name="isoNo"></param>
        /// <returns></returns>
        public static bool IsExistPipelineCode(string pipelineCode, string workAreaId, string PipelineId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.HJGL_Pipeline q = null;
            if (!string.IsNullOrEmpty(PipelineId))
            {
                q = Funs.DB.HJGL_Pipeline.FirstOrDefault(x => x.PipelineCode == pipelineCode && x.UnitWorkId == workAreaId && x.PipelineId != PipelineId);
            }
            else
            {
                q = Funs.DB.HJGL_Pipeline.FirstOrDefault(x => x.PipelineCode == pipelineCode && x.UnitWorkId == workAreaId);
            }

            if (q != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 添加作业管线
        /// </summary>
        /// <param name="pipeline"></param>
        public static void AddPipeline(Model.HJGL_Pipeline pipeline)
        {
            Model.SGGLDB db = Funs.DB;
            Model.HJGL_Pipeline newPipeline = new Model.HJGL_Pipeline();
            newPipeline.PipelineId = pipeline.PipelineId;
            newPipeline.ProjectId = pipeline.ProjectId;
            //newPipeline.InstallationId = pipeline.InstallationId;
            newPipeline.UnitId = pipeline.UnitId;
            newPipeline.UnitWorkId = pipeline.UnitWorkId;
            newPipeline.PipelineCode = pipeline.PipelineCode;
            newPipeline.SingleNumber = pipeline.SingleNumber;
            newPipeline.PipingClassId = pipeline.PipingClassId;
            newPipeline.MediumId = pipeline.MediumId;
            newPipeline.DetectionRateId = pipeline.DetectionRateId;
            newPipeline.DetectionType = pipeline.DetectionType;
            newPipeline.DesignPress = pipeline.DesignPress;
            newPipeline.DesignTemperature = pipeline.DesignTemperature;
            newPipeline.TestPressure = pipeline.TestPressure;
            newPipeline.TestMedium = pipeline.TestMedium;
            newPipeline.PipeLenth = pipeline.PipeLenth;
            newPipeline.PressurePipingClassId = pipeline.PressurePipingClassId;
            newPipeline.Remark = pipeline.Remark;
            newPipeline.LeakPressure = pipeline.LeakPressure;
            newPipeline.LeakMedium = pipeline.LeakMedium;
            newPipeline.VacuumPressure = pipeline.VacuumPressure;
            newPipeline.PCMedium = pipeline.PCMedium;
            newPipeline.PCtype = pipeline.PCtype;
            newPipeline.MaterialId = pipeline.MaterialId;
            db.HJGL_Pipeline.InsertOnSubmit(newPipeline);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改作业管线
        /// </summary>
        /// <param name="pipeline"></param>
        public static void UpdatePipeline(Model.HJGL_Pipeline pipeline)
        {
            Model.SGGLDB db = Funs.DB;
            Model.HJGL_Pipeline newPipeline = db.HJGL_Pipeline.FirstOrDefault(e => e.PipelineId == pipeline.PipelineId);
            if (newPipeline != null)
            {
                //newPipeline.InstallationId = pipeline.InstallationId;
                newPipeline.UnitId = pipeline.UnitId;
                newPipeline.UnitWorkId = pipeline.UnitWorkId;
                newPipeline.PipelineCode = pipeline.PipelineCode;
                newPipeline.SingleNumber = pipeline.SingleNumber;
                newPipeline.PipingClassId = pipeline.PipingClassId;
                newPipeline.MediumId = pipeline.MediumId;
                newPipeline.DetectionRateId = pipeline.DetectionRateId;
                newPipeline.DetectionType = pipeline.DetectionType;
                newPipeline.DesignPress = pipeline.DesignPress;
                newPipeline.DesignTemperature = pipeline.DesignTemperature;
                newPipeline.TestPressure = pipeline.TestPressure;
                newPipeline.TestMedium = pipeline.TestMedium;
                newPipeline.PipeLenth = pipeline.PipeLenth;
                newPipeline.PressurePipingClassId = pipeline.PressurePipingClassId;
                newPipeline.LeakPressure = pipeline.LeakPressure;
                newPipeline.LeakMedium = pipeline.LeakMedium;
                newPipeline.VacuumPressure = pipeline.VacuumPressure;
                newPipeline.PCMedium = pipeline.PCMedium;
                newPipeline.PCtype = pipeline.PCtype;
                newPipeline.Remark = pipeline.Remark;
                newPipeline.MaterialId = pipeline.MaterialId;
                try
                {
                    db.SubmitChanges(System.Data.Linq.ConflictMode.ContinueOnConflict);
                }
                catch (System.Data.Linq.ChangeConflictException ex)
                {
                    foreach (System.Data.Linq.ObjectChangeConflict occ in db.ChangeConflicts)
                    {
                        // 使用Linq缓存中实体对象的值，覆盖当前数据库中的值
                        occ.Resolve(System.Data.Linq.RefreshMode.KeepCurrentValues);
                    }
                    // 这个地方要注意，Catch方法中，我们前面只是指明了怎样来解决冲突，这个地方还需要再次提交更新，这样的话，值    //才会提交到数据库。
                    db.SubmitChanges();
                }
            }
        }

        /// <summary>
        /// 根据作业管线Id删除一个作业管线信息
        /// </summary>
        /// <param name="pipelineId"></param>
        public static void DeletePipeline(string pipelineId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.HJGL_Pipeline pipeline = db.HJGL_Pipeline.FirstOrDefault(e => e.PipelineId == pipelineId);
            //var jot = db.HJGL_Pipeline.Where(e => e.PipelineId == pipelineId);
            if (pipeline != null)
            {
                //db.HJGL_Pipeline.DeleteAllOnSubmit(jot);
                db.HJGL_Pipeline.DeleteOnSubmit(pipeline);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 获取管线名称项
        /// </summary>
        /// <param name="projectId">项目Id</param>
        /// <returns></returns>
        public static ListItem[] GetPipelineList(string unitWorkId)
        {
            List<Model.HJGL_Pipeline> q = (from x in Funs.DB.HJGL_Pipeline where x.UnitWorkId== unitWorkId orderby x.PipelineCode select x).ToList();
            ListItem[] item = new ListItem[q.Count()];
            for (int i = 0; i < q.Count(); i++)
            {
                item[i] = new ListItem(q[i].PipelineCode, q[i].PipelineId.ToString());
            }
            return item;
        }

        /// <summary>
        ///  管线下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitPipelineDownList(FineUIPro.DropDownList dropName, string unitWorkId, bool isShowPlease)
        {
            dropName.DataValueField = "Value";
            dropName.DataTextField = "Text";
            dropName.DataSource = GetPipelineList(unitWorkId);
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
    }
}
