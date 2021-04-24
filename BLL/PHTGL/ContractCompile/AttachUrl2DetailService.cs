using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class AttachUrl2DetailService
    {
        public static List<Model.PHTGL_AttachUrl2Detail> GetDetailListByItemId(string AttachUrlItemId, string type)
        {
            return (from x in Funs.DB.PHTGL_AttachUrl2Detail where x.AttachUrlItemId == AttachUrlItemId && x.DetailType == type select x).ToList();
        }

        /// <summary>
        /// 添加签证台班价格
        /// </summary>
        /// <param name="detail"></param>
        public static void AddAttachUrl2Detail(Model.PHTGL_AttachUrl2Detail detail)
        {
            Model.PHTGL_AttachUrl2Detail newDetail = new Model.PHTGL_AttachUrl2Detail();
            newDetail.AttachUrlDetaild = detail.AttachUrlDetaild;
            newDetail.AttachUrlItemId = detail.AttachUrlItemId;
            newDetail.DetailType = detail.DetailType;
            newDetail.Specifications = detail.Specifications;
            newDetail.MachineTeamPrice = detail.MachineTeamPrice;
            newDetail.Remark = detail.Remark;
            Funs.DB.PHTGL_AttachUrl2Detail.InsertOnSubmit(newDetail);
            Funs.DB.SubmitChanges();
        }

        public static void DeleteAttachUrl2DetailByItemId(string itemId, string type)
        {
            var detail = (from x in Funs.DB.PHTGL_AttachUrl2Detail where x.AttachUrlItemId == itemId && x.DetailType == type select x).ToList();
            if (detail!=null)
            {
                Funs.DB.PHTGL_AttachUrl2Detail.DeleteAllOnSubmit(detail);
                Funs.DB.SubmitChanges();
            }
        }
    }
}
