using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmitMapper;

namespace BLL
{
    public static class APIHazardRegisterService
    {
        /// <summary>
        /// 根据HazardRegisterId获取风险巡检信息
        /// </summary>
        /// <param name="hazardRegisterId"></param>
        /// <returns></returns>
        public static Model.HazardRegisterItem getHazardRegisterByHazardRegisterId(string hazardRegisterId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getHazardRegister = db.View_Hazard_HazardRegister.FirstOrDefault(x => x.HazardRegisterId == hazardRegisterId);
                return ObjectMapperManager.DefaultInstance.GetMapper<Model.View_Hazard_HazardRegister, Model.HazardRegisterItem>().Map(getHazardRegister);
            }
        }

        /// <summary>
        /// 根据projectId、states获取风险信息（状态 1：待整改；2：已整改，待确认；3：已确认，即已闭环；4：已作废）
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="states"></param>
        /// <returns></returns>
        public static List<Model.HazardRegisterItem> getHazardRegisterByProjectIdStates(string projectId, string states, int pageIndex)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var hazardRegisters = (from x in db.View_Hazard_HazardRegister
                                       where x.ProjectId == projectId && (x.States == states || states == null)
                                       orderby x.CheckTime descending
                                       select x).Skip(Funs.PageSize * (pageIndex - 1)).Take(Funs.PageSize).ToList();
                return ObjectMapperManager.DefaultInstance.GetMapper<List<Model.View_Hazard_HazardRegister>, List<Model.HazardRegisterItem>>().Map(hazardRegisters.ToList());
            }
        }

        /// <summary>
        /// 保存HazardRegister
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public static void SaveHazardRegister(Model.HazardRegisterItem hazardRegister)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                Model.HSSE_Hazard_HazardRegister newHazardRegister = new Model.HSSE_Hazard_HazardRegister
                {
                    HazardRegisterId = hazardRegister.HazardRegisterId,
                    HazardCode = hazardRegister.HazardCode,
                    RegisterDef = hazardRegister.RegisterDef,
                    Rectification = hazardRegister.Rectification,
                    Place = hazardRegister.Place,
                    ResponsibleUnit = hazardRegister.ResponsibleUnit,
                    ProjectId = hazardRegister.ProjectId,
                    States = hazardRegister.States,
                    IsEffective = "1",
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
                    RegisterTypesId = hazardRegister.RegisterTypesId,
                    CheckCycle = hazardRegister.CheckCycle,
                    SafeSupervisionIsOK = hazardRegister.SafeSupervisionIsOK,
                    IsWx = "Y",
                    CCManIds = hazardRegister.CCManIds,
                    Requirements = hazardRegister.Requirements,
                };
                var isUpdate = db.HSSE_Hazard_HazardRegister.FirstOrDefault(x => x.HazardRegisterId == newHazardRegister.HazardRegisterId);
                if (isUpdate == null)
                {
                    newHazardRegister.RegisterDate = DateTime.Now;
                    newHazardRegister.CheckTime = DateTime.Now;
                    if (string.IsNullOrEmpty(newHazardRegister.HazardRegisterId))
                    {
                        newHazardRegister.HazardRegisterId = SQLHelper.GetNewID();
                    }
                     db.HSSE_Hazard_HazardRegister.InsertOnSubmit(newHazardRegister);
                }
                else
                {
                    if (newHazardRegister.States == "2")
                    {
                        isUpdate.RectificationTime = DateTime.Now;
                        isUpdate.Rectification = newHazardRegister.Rectification;
                        isUpdate.RectificationImageUrl = newHazardRegister.RectificationImageUrl;
                    }
                    else
                    {
                        isUpdate.ConfirmDate = DateTime.Now;
                        isUpdate.ConfirmMan = newHazardRegister.ConfirmMan;
                        isUpdate.HandleIdea = newHazardRegister.HandleIdea;
                        isUpdate.SafeSupervisionIsOK = newHazardRegister.SafeSupervisionIsOK;
                    }

                    isUpdate.States = newHazardRegister.States;
                }
                db.SubmitChanges();
                if (hazardRegister.States == Const.State_1)
                {
                    APICommonService.SendSubscribeMessage(hazardRegister.ResponsibleMan, "安全巡检问题待整改", hazardRegister.CheckManName, string.Format("{0:yyyy-MM-dd HH:mm:ss}", hazardRegister.CheckTime));

                }
                else if (hazardRegister.States == Const.State_2)
                {
                    APICommonService.SendSubscribeMessage(hazardRegister.CheckManId, "安全巡检待复查验收", hazardRegister.ResponsibilityManName, string.Format("{0:yyyy-MM-dd HH:mm:ss}", hazardRegister.RectificationTime));
                }
            }
        }
    }
}
