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
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataLists = (from x in db.HJGL_Pipeline
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
        }

        #endregion 

        #region 获取未焊接的焊口信息
        /// <summary>
        ///  根据管线ID获取未焊接的焊口信息
        /// </summary>
        /// <param name="pipeLineId"></param>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> GetWeldJointList(string pipeLineId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getData = (from x in db.View_HJGL_NoWeldJointFind
                               where x.PipelineId == pipeLineId
                                    && x.WeldingDailyId == null
                               orderby x.WeldJointCode
                               select new Model.BaseInfoItem
                               {
                                   BaseInfoId = x.WeldJointId,
                                   BaseInfoCode = x.WeldJointCode
                               }
                                ).ToList();


                return getData;
            }
        }
        #endregion

        #region 根据管线ID获取所有焊口信息
        /// <summary>
        ///  根据管线ID获取所有焊口信息
        /// </summary>
        /// <param name="pipeLineId"></param>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> GetAllWeldJointList(string pipeLineId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDataLists = (from x in db.HJGL_WeldJoint
                                    where x.PipelineId == pipeLineId
                                    orderby x.WeldJointCode
                                    select new Model.BaseInfoItem
                                    {
                                        BaseInfoId = x.WeldJointId,
                                        BaseInfoCode = x.WeldJointCode
                                    }
                                ).ToList();
                return getDataLists;
            }
        }
        #endregion

        #region 获取焊工列表
        /// <summary>
        ///  根据管线ID获取焊口列表
        /// </summary>
        /// <param name="unitWorkId"></param>
        /// <returns></returns>
        public static List<Model.BaseInfoItem> getWelderList(string unitWorkId, string weldJointId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var jot = BLL.WeldJointService.GetWeldJointByWeldJointId(weldJointId);
                var joty = BLL.Base_WeldTypeService.GetWeldTypeByWeldTypeId(jot.WeldTypeId);
                string weldType = string.Empty;
                if (joty != null && joty.WeldTypeCode.Contains("B"))
                {
                    weldType = "对接焊缝";
                }
                else
                {
                    weldType = "角焊缝";
                }

                decimal? dia = jot.Dia;
                decimal? sch = Funs.GetNewDecimal(jot.Thickness.HasValue ? jot.Thickness.Value.ToString() : "");
                string wmeCode = string.Empty;
                var wm = BLL.Base_WeldingMethodService.GetWeldingMethodByWeldingMethodId(jot.WeldingMethodId);
                if (wm != null)
                {
                    wmeCode = wm.WeldingMethodCode;
                }
                string[] wmeCodes = wmeCode.Split('+');
                //string location = item.JOT_Location;
                string ste = jot.Material1Id;
                string jointAttribute = jot.JointAttribute;
                var p = db.WBS_UnitWork.Where(x => x.UnitWorkId == unitWorkId).FirstOrDefault();
                var projectWelder = from x in db.SitePerson_Person
                                    where x.ProjectId == p.ProjectId && x.IsUsed == true &&
                                    x.UnitId == p.UnitId && x.WorkPostId == Const.WorkPost_Welder
                                    && x.WelderCode != null && x.WelderCode != ""
                                    select x;
                List<Model.SitePerson_Person> list = new List<Model.SitePerson_Person>();
                foreach (var welder in projectWelder)
                {
                    bool canSave = false;
                    List<Model.Welder_WelderQualify> welderQualifys = (from x in Funs.DB.Welder_WelderQualify
                                                                       where x.WelderId == welder.PersonId && x.WeldingMethod != null
                                                                                      && x.MaterialType != null && x.WeldType != null
                                                                                      && x.ThicknessMax != null && x.SizesMin != null
                                                                                      && x.LimitDate > DateTime.Now && x.IsAudit == true
                                                                       select x).ToList();
                    if (welderQualifys != null)
                    {
                        if (wmeCodes.Count() <= 1) // 一种焊接方法
                        {
                            canSave = OneWmeIsOK(welderQualifys, wmeCode, jointAttribute, weldType, ste, dia, sch);
                        }
                        else  // 大于一种焊接方法，如氩电联焊
                        {
                            canSave = TwoWmeIsOK(welderQualifys, wmeCodes[0], wmeCodes[1], jointAttribute, weldType, ste, dia, sch);
                        }
                        if (canSave)
                        {
                            list.Add(welder);
                        }
                    }
                }
                var getDataLists = (from x in list
                                    orderby x.WelderCode
                                    select new Model.BaseInfoItem
                                    {
                                        BaseInfoId = x.PersonId,
                                        BaseInfoCode = x.WelderCode
                                    }
                                    ).ToList();
                return getDataLists;
            }
        }

        #region 焊工资质判断
        /// <summary>
        /// 一种焊接方法资质判断
        /// </summary>
        /// <param name="welderQualifys"></param>
        /// <param name="wmeCode"></param>
        /// <param name="jointAttribute"></param>
        /// <param name="weldType"></param>
        /// <param name="ste"></param>
        /// <param name="dia"></param>
        /// <param name="sch"></param>
        /// <returns></returns>
        public static bool OneWmeIsOK(List<Model.Welder_WelderQualify> welderQualifys, string wmeCode, string jointAttribute, string weldType, string ste, decimal? dia, decimal? sch)
        {
            bool isok = false;

            var mat = BLL.Base_MaterialService.GetMaterialByMaterialId(ste);
            var welderQ = from x in welderQualifys
                          where wmeCode.Contains(x.WeldingMethod)
                          && (mat == null || x.MaterialType.Contains(mat.MetalType ?? ""))
                          && x.WeldType.Contains(weldType)
                          select x;

            if (welderQ.Count() > 0)
            {
                if (jointAttribute == "固定口")
                {
                    welderQ = welderQ.Where(x => x.IsCanWeldG == true);
                }
                if (welderQ.Count() > 0)
                {
                    if (weldType == "1") // 1-对接焊缝 2-表示角焊缝，当为角焊缝时，管径和壁厚不限制
                    {
                        var welderDiaQ = welderQ.Where(x => x.SizesMin <= dia || x.SizesMax == 0);

                        if (welderDiaQ.Count() > 0)
                        {
                            var welderThick = welderDiaQ.Where(x => x.ThicknessMax >= sch || x.ThicknessMax == 0);

                            // 只要有一个不限（为0）就通过
                            if (welderThick.Count() > 0)
                            {
                                isok = true;
                            }
                        }
                    }
                    else
                    {
                        isok = true;
                    }
                }
            }

            return isok;
        }
        /// <summary>
        /// 两种焊接方法资质判断
        /// </summary>
        /// <param name="floorWelderQualifys"></param>
        /// <param name="cellWelderQualifys"></param>
        /// <param name="wmeCode1"></param>
        /// <param name="wmeCode2"></param>
        /// <param name="jointAttribute"></param>
        /// <param name="weldType"></param>
        /// <param name="ste"></param>
        /// <param name="dia"></param>
        /// <param name="sch"></param>
        /// <returns></returns>
        public static bool TwoWmeIsOK(List<Model.Welder_WelderQualify> welderQualifys, string wmeCode1, string wmeCode2, string jointAttribute, string weldType, string ste, decimal? dia, decimal? sch)
        {
            bool isok = false;

            decimal? fThicknessMax = 0;
            decimal? cThicknessMax = 0;

            var mat = BLL.Base_MaterialService.GetMaterialByMaterialId(ste);
            var floorQ = from x in welderQualifys
                         where wmeCode1.Contains(x.WeldingMethod)
                         && (mat == null || x.MaterialType.Contains(mat.MetalType ?? ""))
                         && x.WeldType.Contains(weldType)
                         // && (dia == null || x.SizesMin<=dia)
                         select x;
            var cellQ = from x in welderQualifys
                        where wmeCode2.Contains(x.WeldingMethod)
                         && (mat == null || x.MaterialType.Contains(mat.MetalType ?? ""))
                         && x.WeldType.Contains(weldType)
                        // && (dia == null || x.SizesMin <= dia)
                        select x;
            if (floorQ.Count() > 0 && cellQ.Count() > 0)
            {
                if (jointAttribute == "固定口")
                {
                    floorQ = floorQ.Where(x => x.IsCanWeldG == true);
                    cellQ = cellQ.Where(x => x.IsCanWeldG == true);
                }
                if (floorQ.Count() > 0 && cellQ.Count() > 0)
                {
                    if (weldType == "1") // 1-对接焊缝 2-表示角焊缝，当为角焊缝时，管径和壁厚不限制
                    {
                        var floorDiaQ = floorQ.Where(x => x.SizesMin <= dia || x.SizesMax == 0);
                        var cellDiaQ = cellQ.Where(x => x.SizesMin <= dia || x.SizesMax == 0);

                        if (floorDiaQ.Count() > 0 && cellDiaQ.Count() > 0)
                        {
                            var fThick = floorDiaQ.Where(x => x.ThicknessMax == 0);
                            var cThick = cellDiaQ.Where(x => x.ThicknessMax == 0);

                            // 只要有一个不限（为0）就通过
                            if (fThick.Count() > 0 || cThick.Count() > 0)
                            {
                                isok = true;
                            }

                            else
                            {
                                fThicknessMax = floorQ.Max(x => x.ThicknessMax);
                                cThicknessMax = cellQ.Max(x => x.ThicknessMax);

                                if ((fThicknessMax + cThicknessMax) >= sch)
                                {
                                    isok = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        isok = true;
                    }
                }
            }

            return isok;
        }
        #endregion
        #endregion



        #region 根据焊口ID获取焊口信息
        /// <summary>
        ///  根据焊口ID获取焊口信息
        /// </summary>
        /// <param name="weldJointId"></param>
        /// <returns></returns>
        public static Model.WeldJointItem getWeldJointInfo(string weldJointId)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDateInfo = from x in db.HJGL_WeldJoint
                                  where x.WeldJointId == weldJointId

                                  select new Model.WeldJointItem
                                  {
                                      WeldJointId = x.WeldJointId,
                                      WeldJointCode = x.WeldJointCode,
                                      PipelineId = x.PipelineId,
                                      PipelineCode = x.PipelineCode,
                                      JointArea = x.JointArea,
                                      JointAttribute = x.JointAttribute,
                                      WeldingMode = x.WeldingMode,
                                      Size = x.Size,
                                      Dia = x.Dia,
                                      Thickness = x.Thickness,
                                      WeldingMethodCode = db.Base_WeldingMethod.First(y => y.WeldingMethodId == x.WeldingMethodId).WeldingMethodCode,
                                      DetectionRate = GetDetectionRate(x.PipelineId),
                                      IsHotProess = x.IsHotProess == true ? "是" : "否",
                                      AttachUrl = x.AttachUrl

                                  };
                return getDateInfo.FirstOrDefault();
            }
        }
        #endregion

        /// <summary>
        /// 根据管线ID获取探伤比例
        /// </summary>
        /// <param name="pipeLineId"></param>
        /// <returns></returns>
        private static string GetDetectionRate(string pipeLineId)
        {
            string detectionRate = string.Empty;
            var pipe = BLL.PipelineService.GetPipelineByPipelineId(pipeLineId);
            if (pipe != null && !string.IsNullOrEmpty(pipe.DetectionRateId))
            {
                var r = BLL.Base_DetectionRateService.GetDetectionRateByDetectionRateId(pipe.DetectionRateId);
                detectionRate = r.DetectionRateValue + "%";
            }
            return detectionRate;
        }

        #region 根据焊口标识获取焊口详细信息
        /// <summary>
        ///  根据焊口标识获取焊口详细信息
        /// </summary>
        /// <param name="weldJointIdentify">焊口标识</param>
        /// <returns></returns>
        public static Model.WeldJointItem getWeldJointByIdentify(string weldJointIdentify)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getDateInfo = from x in db.HJGL_WeldJoint
                                  join y in db.HJGL_WeldingDaily on x.WeldingDailyId equals y.WeldingDailyId
                                  where x.WeldJointIdentify == weldJointIdentify

                                  select new Model.WeldJointItem
                                  {
                                      WeldJointId = x.WeldJointId,
                                      WeldJointCode = x.WeldJointCode,
                                      WeldJointIdentify = x.WeldJointIdentify,
                                      Position = x.Position,
                                      PipelineId = x.PipelineId,
                                      PipelineCode = x.PipelineCode,
                                      JointArea = x.JointArea,
                                      JointAttribute = x.JointAttribute,
                                      WeldingMode = x.WeldingMode,
                                      Size = x.Size,
                                      Dia = x.Dia,
                                      Thickness = x.Thickness,
                                      WeldingMethodCode = db.Base_WeldingMethod.First(z => z.WeldingMethodId == x.WeldingMethodId).WeldingMethodCode,
                                      WeldingDate = y.WeldingDate,
                                      BackingWelderCode = db.SitePerson_Person.First(z => z.PersonId == x.BackingWelderId).WelderCode,
                                      CoverWelderCode = db.SitePerson_Person.First(z => z.PersonId == x.CoverWelderId).WelderCode,
                                      IsHotProess = x.IsHotProess == true ? "是" : "否",
                                      AttachUrl = x.AttachUrl

                                  };
                return getDateInfo.FirstOrDefault();
            }
        }
        #endregion

        #region 保存管线焊口信息
        /// <summary>
        /// 保存管线焊口信息
        /// </summary>
        /// <param name="addItem"></param>
        public static void SavePipeWeldJoint(Model.WeldJointItem addItem)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                string projectId = string.Empty;
                string unitId = string.Empty;
                string pipelineId = string.Empty;

                var p = db.HJGL_Pipeline.Where(x => x.ProjectId == addItem.ProjectId && x.UnitWorkId == addItem.UnitWorkId && x.PipelineCode == addItem.PipelineCode).FirstOrDefault();
                var w = db.WBS_UnitWork.Where(x => x.UnitWorkId == addItem.UnitWorkId).FirstOrDefault();
                if (w != null)
                {
                    projectId = w.ProjectId;
                    unitId = w.UnitId;
                }
                if (p != null)
                {
                    pipelineId = p.PipelineId;
                }
                else
                {
                    // 保存管线信息
                    pipelineId = SQLHelper.GetNewID();
                    var pipeClass = db.Base_PipingClass.FirstOrDefault(z => z.PipingClassCode == addItem.PipingClass);
                    var medium = db.Base_Medium.FirstOrDefault(z => z.MediumCode == addItem.Medium);
                    var detectionRate = db.Base_DetectionRate.FirstOrDefault(z => z.DetectionRateCode == addItem.DetectionRate);
                    var detectionType = db.Base_DetectionType.FirstOrDefault(z => z.DetectionTypeCode == addItem.DetectionType);
                    var testMedium = db.Base_Medium.FirstOrDefault(z => z.MediumCode == addItem.TestMedium);
                    Model.HJGL_Pipeline newPipe = new Model.HJGL_Pipeline();

                    newPipe.PipelineId = pipelineId;

                    newPipe.ProjectId = projectId;
                    newPipe.UnitWorkId = addItem.UnitWorkId;
                    newPipe.UnitId = unitId;
                    newPipe.PipelineCode = addItem.PipelineCode;
                    newPipe.SingleNumber = addItem.SingleNumber;
                    if (pipeClass != null)
                    {
                        newPipe.PipingClassId = pipeClass.PipingClassId;
                    }
                    if (medium != null)
                    {
                        newPipe.MediumId = medium.MediumId;
                    }
                    if (detectionRate != null)
                    {
                        newPipe.DetectionRateId = detectionRate.DetectionRateId;
                    }
                    if (detectionType != null)
                    {
                        newPipe.DetectionType = detectionType.DetectionTypeId;
                    }

                    newPipe.TestPressure = addItem.TestPressure.ToString();
                    if (testMedium != null)
                    {
                        newPipe.TestMedium = testMedium.MediumId;
                    }

                    db.HJGL_Pipeline.InsertOnSubmit(newPipe);
                    db.SubmitChanges();

                }

                var jot = db.HJGL_WeldJoint.Where(x => x.PipelineId == pipelineId && (x.WeldJointCode == addItem.WeldJointCode || x.WeldJointIdentify == addItem.WeldJointIdentify)).FirstOrDefault();
                if (jot == null)
                {
                    var weldType = db.Base_WeldType.FirstOrDefault(z => z.WeldTypeCode == addItem.WeldType);
                    var material1 = db.Base_Material.FirstOrDefault(z => z.MaterialCode == addItem.Material1);
                    var material2 = db.Base_Material.FirstOrDefault(z => z.MaterialCode == addItem.Material2);
                    var weldingMethod = db.Base_WeldingMethod.FirstOrDefault(z => z.WeldingMethodCode == addItem.WeldingMethodCode);
                    var grooveType = db.Base_GrooveType.FirstOrDefault(z => z.GrooveTypeCode == addItem.GrooveType);
                    var weldingLocation = db.Base_WeldingLocation.FirstOrDefault(z => z.WeldingLocationCode == addItem.WeldingLocation);
                    var weldingWire = db.Base_Consumables.FirstOrDefault(z => z.ConsumablesName == addItem.WeldingWire);
                    var weldingRod = db.Base_Consumables.FirstOrDefault(z => z.ConsumablesName == addItem.WeldingRod);

                    Model.HJGL_WeldJoint newJot = new Model.HJGL_WeldJoint();

                    newJot.WeldJointId = SQLHelper.GetNewID();
                    newJot.WeldJointCode = addItem.WeldJointCode;
                    newJot.WeldJointIdentify = addItem.WeldJointIdentify;
                    newJot.Position = addItem.Position;
                    newJot.ProjectId = projectId;
                    newJot.PipelineId = pipelineId;
                    newJot.PipelineCode = addItem.PipelineCode;

                    if (weldType != null)
                    {
                        newJot.WeldTypeId = weldType.WeldTypeId;
                    }
                    if (material1 != null)
                    {
                        newJot.Material1Id = material1.MaterialId;
                    }
                    if (material2 != null)
                    {
                        newJot.Material2Id = material2.MaterialId;
                    }
                    if (weldingMethod != null)
                    {
                        newJot.WeldingMethodId = weldingMethod.WeldingMethodId;
                    }

                    newJot.JointArea = addItem.JointArea;
                    newJot.Dia = addItem.Dia;
                    newJot.Thickness = addItem.Thickness;
                    newJot.Specification = addItem.Specification;
                    newJot.JointAttribute = addItem.JointAttribute;

                    if (grooveType != null)
                    {
                        newJot.GrooveTypeId = grooveType.GrooveTypeId;
                    }
                    if (weldingLocation != null)
                    {
                        newJot.WeldingLocationId = weldingLocation.WeldingLocationId;
                    }

                    if (weldingWire != null)
                    {
                        newJot.WeldingWire = weldingWire.ConsumablesId;
                    }
                    if (weldingRod != null)
                    {
                        newJot.WeldingRod = weldingRod.ConsumablesId;
                    }

                    db.HJGL_WeldJoint.InsertOnSubmit(newJot);
                    db.SubmitChanges();
                }
            }
        }
        #endregion

        #region 批量保存管线焊口信息
        /// <summary>
        /// 批量保存管线焊口信息
        /// </summary>
        /// <param name="addItems"></param>
        public static void SavePipeWeldJointList(List<Model.WeldJointItem> addItems)
        {
            if (addItems.Count() > 0)
            {
                foreach (Model.WeldJointItem addItem in addItems)
                {
                    SavePipeWeldJoint(addItem);
                }
            }
        }

        #endregion

        /// <summary>
        /// 保存预提交日报
        /// </summary>
        /// <param name="addItem"></param>
        public static void SavePreWeldingDaily(Model.WeldJointItem addItem)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                string projectId = string.Empty;
                string unitId = string.Empty;
                var p = db.WBS_UnitWork.Where(x => x.UnitWorkId == addItem.UnitWorkId).FirstOrDefault();

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
}
