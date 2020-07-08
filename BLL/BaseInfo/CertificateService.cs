using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 特岗证书
    /// </summary>
    public static class CertificateService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 根据主键获取特岗证书
        /// </summary>
        /// <param name="certificateId"></param>
        /// <returns></returns>
        public static Model.Base_Certificate GetCertificateById(string certificateId)
        {
            return new Model.SGGLDB(Funs.ConnString).Base_Certificate.FirstOrDefault(e => e.CertificateId == certificateId);
        }

        /// <summary>
        /// 添加特岗证书
        /// </summary>
        /// <param name="certificate"></param>
        public static void AddCertificate(Model.Base_Certificate certificate)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Base_Certificate newCertificate = new Model.Base_Certificate
            {
                CertificateId = certificate.CertificateId,
                CertificateCode = certificate.CertificateCode,
                CertificateName = certificate.CertificateName,
                Remark = certificate.Remark
            };
            db.Base_Certificate.InsertOnSubmit(newCertificate);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改特岗证书
        /// </summary>
        /// <param name="certificate"></param>
        public static void UpdateCertificate(Model.Base_Certificate certificate)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Base_Certificate newCertificate = db.Base_Certificate.FirstOrDefault(e => e.CertificateId == certificate.CertificateId);
            if (newCertificate != null)
            {
                newCertificate.CertificateCode = certificate.CertificateCode;
                newCertificate.CertificateName = certificate.CertificateName;
                newCertificate.Remark = certificate.Remark;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除特岗证书
        /// </summary>
        /// <param name="certificateId"></param>
        public static void DeleteCertificateById(string certificateId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Base_Certificate certificate = db.Base_Certificate.FirstOrDefault(e => e.CertificateId == certificateId);
            if (certificate != null)
            {
                db.Base_Certificate.DeleteOnSubmit(certificate);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 获取特岗证书列表
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_Certificate> GetCertificateList()
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).Base_Certificate orderby x.CertificateCode select x).ToList();
        }

        #region 表下拉框
        /// <summary>
        ///  表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitCertificateDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease)
        {
            dropName.DataValueField = "CertificateId";
            dropName.DataTextField = "CertificateName";
            dropName.DataSource = BLL.CertificateService.GetCertificateList();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        #endregion
    }
}
