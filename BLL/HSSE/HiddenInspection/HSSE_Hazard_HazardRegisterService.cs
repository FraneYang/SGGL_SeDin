using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web.UI.WebControls;

namespace BLL
{
    public static class HSSE_Hazard_HazardRegisterService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据危险观察登记主键获取危险观察登记信息
        /// </summary>
        /// <param name="hazardRegisterId">危险观察登记主键</param>
        /// <returns>危险观察登记信息</returns>
        public static Model.HSSE_Hazard_HazardRegister GetHazardRegisterByHazardRegisterId(string hazardRegisterId)
        {
            return Funs.DB.HSSE_Hazard_HazardRegister.FirstOrDefault(x => x.HazardRegisterId == hazardRegisterId);
        }

        /// <summary>
        ///  获取项目风险列表
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static List<Model.HSSE_Hazard_HazardRegister> GetHazardRegisterListByProjectId(string projectId)
        {
            return (from x in Funs.DB.HSSE_Hazard_HazardRegister where x.ProjectId == projectId select x).ToList();
        }

        /// <summary>
        /// 增加危险观察登记信息
        /// </summary>
        /// <param name="hazardRegister">危险观察登记实体</param>
        public static void AddHazardRegister(Model.HSSE_Hazard_HazardRegister hazardRegister)
        {
            Model.SGGLDB db = Funs.DB;
            Model.HSSE_Hazard_HazardRegister newHazardRegister = new Model.HSSE_Hazard_HazardRegister
            {
                HazardRegisterId = hazardRegister.HazardRegisterId,
                HazardCode = hazardRegister.HazardCode,
                RegisterDate = DateTime.Now,
                RegisterDef = hazardRegister.RegisterDef,
                RegisterTypesId = hazardRegister.RegisterTypesId,
                CheckCycle = hazardRegister.CheckCycle,
                Rectification = hazardRegister.Rectification,
                Place = hazardRegister.Place,
                ResponsibleUnit = hazardRegister.ResponsibleUnit,
                Observer = hazardRegister.Observer,
                RectifiedDate = hazardRegister.RectifiedDate,
                AttachUrl = hazardRegister.AttachUrl,
                ProjectId = hazardRegister.ProjectId,
                States = hazardRegister.States,
                IsEffective = hazardRegister.IsEffective,
                ResponsibleMan = hazardRegister.ResponsibleMan,
                CheckManId = hazardRegister.CheckManId,
                CheckTime = hazardRegister.CheckTime,
                RectificationPeriod = hazardRegister.RectificationPeriod,
                ImageUrl = hazardRegister.ImageUrl,
                RectificationImageUrl = hazardRegister.RectificationImageUrl,
                RectificationTime = hazardRegister.RectificationTime,
                ConfirmMan = hazardRegister.ConfirmMan,
                ConfirmDate = hazardRegister.ConfirmDate,
                HandleIdea = hazardRegister.HandleIdea,
                CutPayment = hazardRegister.CutPayment,
                ProblemTypes = hazardRegister.ProblemTypes,
                DIC_ID = hazardRegister.DIC_ID,

                HazardValue = hazardRegister.HazardValue,
                RegisterTypes2Id = hazardRegister.RegisterTypes2Id,
                RegisterTypes3Id = hazardRegister.RegisterTypes3Id,
                RegisterTypes4Id = hazardRegister.RegisterTypes4Id,
                CCManIds = hazardRegister.CCManIds
            };
            db.HSSE_Hazard_HazardRegister.InsertOnSubmit(newHazardRegister);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改危险观察登记信息
        /// </summary>
        /// <param name="hazardRegister">危险观察登记实体</param>
        public static void UpdateHazardRegister(Model.HSSE_Hazard_HazardRegister hazardRegister)
        {
            Model.SGGLDB db = Funs.DB;
            Model.HSSE_Hazard_HazardRegister newHazardRegister = db.HSSE_Hazard_HazardRegister.FirstOrDefault(e => e.HazardRegisterId == hazardRegister.HazardRegisterId);
            if (newHazardRegister != null)
            {
                newHazardRegister.HazardCode = hazardRegister.HazardCode;
                newHazardRegister.RegisterDef = hazardRegister.RegisterDef;
                newHazardRegister.Rectification = hazardRegister.Rectification;
                newHazardRegister.Place = hazardRegister.Place;
                newHazardRegister.ResponsibleUnit = hazardRegister.ResponsibleUnit;
                newHazardRegister.Observer = hazardRegister.Observer;
                newHazardRegister.RectifiedDate = hazardRegister.RectifiedDate;
                newHazardRegister.AttachUrl = hazardRegister.AttachUrl;
                newHazardRegister.ProjectId = hazardRegister.ProjectId;
                newHazardRegister.States = hazardRegister.States;
                newHazardRegister.IsEffective = hazardRegister.IsEffective;
                newHazardRegister.ResponsibleMan = hazardRegister.ResponsibleMan;
                newHazardRegister.CheckManId = hazardRegister.CheckManId;
                //newHazardRegister.CheckTime = hazardRegister.CheckTime;
                newHazardRegister.RectificationPeriod = hazardRegister.RectificationPeriod;
                newHazardRegister.ImageUrl = hazardRegister.ImageUrl;
                newHazardRegister.RectificationImageUrl = hazardRegister.RectificationImageUrl;
                newHazardRegister.RectificationTime = hazardRegister.RectificationTime;
                newHazardRegister.ConfirmMan = hazardRegister.ConfirmMan;
                newHazardRegister.ConfirmDate = hazardRegister.ConfirmDate;
                newHazardRegister.HandleIdea = hazardRegister.HandleIdea;
                newHazardRegister.CutPayment = hazardRegister.CutPayment;
                newHazardRegister.ProblemTypes = hazardRegister.ProblemTypes;
                newHazardRegister.DIC_ID = hazardRegister.DIC_ID;
                newHazardRegister.HazardValue = hazardRegister.HazardValue;
                newHazardRegister.RegisterTypes2Id = hazardRegister.RegisterTypes2Id;
                newHazardRegister.RegisterTypes3Id = hazardRegister.RegisterTypes3Id;
                newHazardRegister.RegisterTypes4Id = hazardRegister.RegisterTypes4Id;
                newHazardRegister.CCManIds = hazardRegister.CCManIds;
                //把附件表的路径复制过来
                Model.AttachFile file = BLL.AttachFileService.GetAttachFile(hazardRegister.HazardRegisterId, Const.HSSE_HiddenRectificationListMenuId);
                if (file != null)
                {
                    newHazardRegister.ImageUrl = file.AttachUrl;
                }
                Model.AttachFile fileR = BLL.AttachFileService.GetAttachFile(hazardRegister.HazardRegisterId + "-R", Const.HSSE_HiddenRectificationListMenuId);
                if (fileR != null)
                {
                    newHazardRegister.RectificationImageUrl = fileR.AttachUrl;
                }

                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据危险观察登记主键删除一个危险观察登记信息
        /// </summary>
        /// <param name="hazardRegisterId">危险观察登记主键</param>
        public static void DeleteHazardRegisterByHazardRegisterId(string hazardRegisterId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.HSSE_Hazard_HazardRegister hazardRegister = db.HSSE_Hazard_HazardRegister.FirstOrDefault(e => e.HazardRegisterId == hazardRegisterId);
            if (hazardRegister != null)
            {
                try
                {
                    BLL.UploadFileService.DeleteFile(Funs.RootPath, hazardRegister.ImageUrl);//删除整改前图片
                    BLL.UploadFileService.DeleteFile(Funs.RootPath, hazardRegister.RectificationImageUrl);//删除整改后图片
                }
                catch (Exception)
                {
                }
                db.HSSE_Hazard_HazardRegister.DeleteOnSubmit(hazardRegister);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 对单位分组查询危险观察登记信息并
        /// </summary>
        /// <returns>危险观察登记信息</returns>
        public static ListItem[] GetResponsibleUnitList(string projectId)
        {
            var q = (from x in Funs.DB.HSSE_Hazard_HazardRegister where x.ProjectId == projectId group x by x.ResponsibleUnit into x select new { ResponsibleUnit = x.Key }).ToList();
            ListItem[] list = new ListItem[q.Count()];
            for (int i = 0; i < q.Count(); i++)
            {
                list[i] = new ListItem(BLL.UnitService.GetUnitByUnitId(q[i].ResponsibleUnit).UnitName ?? "", q[i].ResponsibleUnit.ToString());
            }
            return list;
        }

        /// <summary>
        /// 获取危险观察登记状态集合
        /// </summary>
        /// <returns>危险观察登记状态集合</returns>
        public static List<Model.HandleStep> GetStatesList()
        {
            List<Model.HandleStep> handleSteps = new List<Model.HandleStep>();
            Model.HandleStep handleStep1 = new Model.HandleStep();
            handleStep1.Id = "1";
            handleStep1.Name = "待整改";
            handleSteps.Add(handleStep1);
            Model.HandleStep handleStep2 = new Model.HandleStep();
            handleStep2.Id = "2";
            handleStep2.Name = "已整改";
            handleSteps.Add(handleStep2);
            Model.HandleStep handleStep3 = new Model.HandleStep();
            handleStep3.Id = "3";
            handleStep3.Name = "已闭环";
            handleSteps.Add(handleStep3);
            Model.HandleStep handleStep4 = new Model.HandleStep();
            handleStep4.Id = "4";
            handleStep4.Name = "已作废";
            handleSteps.Add(handleStep4);
            return handleSteps;
        }
    }
}
