using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public static class PracticeCertificateService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取执业资格证书
        /// </summary>
        /// <param name="PracticeCertificateId"></param>
        /// <returns></returns>
        public static Model.Base_PracticeCertificate GetPracticeCertificateById(string PracticeCertificateId)
        {
            return Funs.DB.Base_PracticeCertificate.FirstOrDefault(e => e.PracticeCertificateId == PracticeCertificateId);
        }

        /// <summary>
        /// 添加执业资格证书
        /// </summary>
        /// <param name="PracticeCertificate"></param>
        public static void AddPracticeCertificate(Model.Base_PracticeCertificate PracticeCertificate)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_PracticeCertificate newPracticeCertificate = new Model.Base_PracticeCertificate
            {
                PracticeCertificateId = PracticeCertificate.PracticeCertificateId,
                PracticeCertificateCode = PracticeCertificate.PracticeCertificateCode,
                PracticeCertificateName = PracticeCertificate.PracticeCertificateName,
                Remark = PracticeCertificate.Remark
            };
            db.Base_PracticeCertificate.InsertOnSubmit(newPracticeCertificate);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改职业资格证书
        /// </summary>
        /// <param name="PracticeCertificate"></param>
        public static void UpdatePracticeCertificate(Model.Base_PracticeCertificate PracticeCertificate)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_PracticeCertificate newPracticeCertificate = db.Base_PracticeCertificate.FirstOrDefault(e => e.PracticeCertificateId == PracticeCertificate.PracticeCertificateId);
            if (newPracticeCertificate != null)
            {
                newPracticeCertificate.PracticeCertificateCode = PracticeCertificate.PracticeCertificateCode;
                newPracticeCertificate.PracticeCertificateName = PracticeCertificate.PracticeCertificateName;
                newPracticeCertificate.Remark = PracticeCertificate.Remark;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除执业资格证书
        /// </summary>
        /// <param name="PracticeCertificateId"></param>
        public static void DeletePracticeCertificateById(string PracticeCertificateId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_PracticeCertificate PracticeCertificate = db.Base_PracticeCertificate.FirstOrDefault(e => e.PracticeCertificateId == PracticeCertificateId);
            if (PracticeCertificate != null)
            {
                db.Base_PracticeCertificate.DeleteOnSubmit(PracticeCertificate);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 获取特岗证书列表
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_PracticeCertificate> GetPracticeCertificateList()
        {
            return (from x in Funs.DB.Base_PracticeCertificate orderby x.PracticeCertificateCode select x).ToList();
        }

        #region 表下拉框
        /// <summary>
        ///  表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitPracticeCertificateDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease)
        {
            dropName.DataValueField = "PracticeCertificateId";
            dropName.DataTextField = "PracticeCertificateName";
            dropName.DataSource = BLL.PracticeCertificateService.GetPracticeCertificateList();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        #endregion
    }
}
