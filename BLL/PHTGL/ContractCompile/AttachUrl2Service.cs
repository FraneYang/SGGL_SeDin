using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    /// <summary>
    /// 附件2 合同价格及支付办法
    /// </summary>
    public static class AttachUrl2Service
    {
        /// <summary>
        /// 根据附件Id获取附件2信息
        /// </summary>
        /// <param name="attachUrlId"></param>
        /// <returns></returns>
        public static Model.PHTGL_AttachUrl2 GetAttachUrlByAttachUrlId(string attachUrlId)
        {
            return Funs.DB.PHTGL_AttachUrl2.FirstOrDefault(e => e.AttachUrlId == attachUrlId);
        }

        /// <summary>
        /// 增加附件2
        /// </summary>
        /// <param name="att"></param>
        public static void AddAttachUrl2(Model.PHTGL_AttachUrl2 att)
        {
            Model.PHTGL_AttachUrl2 newAtt = new Model.PHTGL_AttachUrl2();
            newAtt.AttachUrlItemId = att.AttachUrlItemId;
            newAtt.AttachUrlId = att.AttachUrlId;
            newAtt.ContractPrice = att.ContractPrice;
            newAtt.ComprehensiveUnitPrice = att.ComprehensiveUnitPrice;
            newAtt.ComprehensiveRate1 = att.ComprehensiveRate1;
            newAtt.ComprehensiveRate2 = att.ComprehensiveRate2;
            newAtt.ComprehensiveRate3 = att.ComprehensiveRate3;
            newAtt.ComprehensiveRate4 = att.ComprehensiveRate4;
            newAtt.ComprehensiveRate5 = att.ComprehensiveRate5;
            newAtt.TotalPriceDown1 = att.TotalPriceDown1;
            newAtt.TotalPriceDown2 = att.TotalPriceDown2;
            newAtt.TotalPriceDown3 = att.TotalPriceDown3;
            newAtt.TotalPriceDown4 = att.TotalPriceDown4;
            newAtt.TotalPriceDown5 = att.TotalPriceDown5;
            newAtt.TechnicalWork = att.TechnicalWork;
            newAtt.PhysicalLaborer = att.PhysicalLaborer;
            newAtt.TestCar1 = att.TestCar1;
            newAtt.TestCar2 = att.TestCar2;
            newAtt.PayWay = att.PayWay;
            newAtt.PayMethod = att.PayMethod;
            Funs.DB.PHTGL_AttachUrl2.InsertOnSubmit(newAtt);
            Funs.DB.SubmitChanges();
        }

        /// <summary>
        /// 修改附件2
        /// </summary>
        /// <param name="att"></param>
        public static void UpdateAttachUrl2(Model.PHTGL_AttachUrl2 att)
        {
            Model.PHTGL_AttachUrl2 newAtt = Funs.DB.PHTGL_AttachUrl2.FirstOrDefault(e => e.AttachUrlId == att.AttachUrlId);
            if (newAtt != null)
            {
                newAtt.ContractPrice = att.ContractPrice;
                newAtt.ComprehensiveUnitPrice = att.ComprehensiveUnitPrice;
                newAtt.ComprehensiveRate1 = att.ComprehensiveRate1;
                newAtt.ComprehensiveRate2 = att.ComprehensiveRate2;
                newAtt.ComprehensiveRate3 = att.ComprehensiveRate3;
                newAtt.ComprehensiveRate4 = att.ComprehensiveRate4;
                newAtt.ComprehensiveRate5 = att.ComprehensiveRate5;
                newAtt.TotalPriceDown1 = att.TotalPriceDown1;
                newAtt.TotalPriceDown2 = att.TotalPriceDown2;
                newAtt.TotalPriceDown3 = att.TotalPriceDown3;
                newAtt.TotalPriceDown4 = att.TotalPriceDown4;
                newAtt.TotalPriceDown5 = att.TotalPriceDown5;
                newAtt.TechnicalWork = att.TechnicalWork;
                newAtt.PhysicalLaborer = att.PhysicalLaborer;
                newAtt.TestCar1 = att.TestCar1;
                newAtt.TestCar2 = att.TestCar2;
                newAtt.PayWay = att.PayWay;
                newAtt.PayMethod = att.PayMethod;
                Funs.DB.SubmitChanges();
            }
        }
    }
}
