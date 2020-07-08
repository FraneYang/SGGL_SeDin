using System.Linq;

namespace BLL
{
    public static class EnvironmentalService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 获取环境危险源信息
        /// </summary>
        /// <param name="environmentalId">环境危险源Id</param>
        /// <returns></returns>
        public static Model.Technique_Environmental GetEnvironmental(string environmentalId)
        {
            return new Model.SGGLDB(Funs.ConnString).Technique_Environmental.FirstOrDefault(x => x.EnvironmentalId == environmentalId);
        }

        /// <summary>
        /// 增加环境危险源
        /// </summary>
        /// <param name="environmental"></param>
        public static void AddEnvironmental(Model.Technique_Environmental environmental)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Technique_Environmental newEnvironmental = new Model.Technique_Environmental
            {
                EnvironmentalId = environmental.EnvironmentalId,
                EType = environmental.EType,
                ActivePoint = environmental.ActivePoint,
                EnvironmentalFactors = environmental.EnvironmentalFactors,
                AValue = environmental.AValue,
                BValue = environmental.BValue,
                CValue = environmental.CValue,
                DValue = environmental.DValue,
                EValue = environmental.EValue,
                FValue = environmental.FValue,
                GValue = environmental.GValue,
                SmallType = environmental.SmallType,
                IsImportant = environmental.IsImportant,
                Code = environmental.Code,
                ControlMeasures = environmental.ControlMeasures,
                Remark = environmental.Remark,
                IsCompany = environmental.IsCompany
            };
            db.Technique_Environmental.InsertOnSubmit(newEnvironmental);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改环境危险源信息
        /// </summary>
        /// <param name="environmentalId">环境危险源主键</param>
        /// <param name="depCode"></param>
        /// <param name="depHead"></param>
        /// <param name="depName"></param>
        /// <param name="remark"></param>
        public static void UpdateEnvironmental(Model.Technique_Environmental environmental)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Technique_Environmental newEnvironmental = db.Technique_Environmental.FirstOrDefault(e => e.EnvironmentalId == environmental.EnvironmentalId);
            if (newEnvironmental != null)
            {
                newEnvironmental.EType = environmental.EType;
                newEnvironmental.ActivePoint = environmental.ActivePoint;
                newEnvironmental.EnvironmentalFactors = environmental.EnvironmentalFactors;
                newEnvironmental.AValue = environmental.AValue;
                newEnvironmental.BValue = environmental.BValue;
                newEnvironmental.CValue = environmental.CValue;
                newEnvironmental.DValue = environmental.DValue;
                newEnvironmental.EValue = environmental.EValue;
                newEnvironmental.FValue = environmental.FValue;
                newEnvironmental.GValue = environmental.GValue;
                newEnvironmental.SmallType = environmental.SmallType;
                newEnvironmental.IsImportant = environmental.IsImportant;
                newEnvironmental.Code = environmental.Code;
                newEnvironmental.ControlMeasures = environmental.ControlMeasures;
                newEnvironmental.Remark = environmental.Remark;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 删除环境危险源
        /// </summary>
        /// <param name="environmentalId"></param>
        public static void DeleteEnvironmental(string environmentalId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Technique_Environmental environmental = db.Technique_Environmental.FirstOrDefault(e => e.EnvironmentalId == environmentalId);
            if (environmental != null)
            {
                db.Technique_Environmental.DeleteOnSubmit(environmental);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 判断是否存在相同编号
        /// </summary>
        /// <param name="environmentalId"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static bool IsEnvironmentalCode(string environmentalId, string code,bool isCompany)
        {
            var q = new Model.SGGLDB(Funs.ConnString).Technique_Environmental.FirstOrDefault(x => (x.EnvironmentalId != environmentalId || (environmentalId == null && x.EnvironmentalId != null)) && x.Code == code && x.IsCompany == isCompany);
            if (q != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
