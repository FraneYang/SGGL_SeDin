using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class EquipmentQualityItem
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public string EquipmentQualityId { get; set; }
        /// <summary>
        /// 项目ID
        /// </summary>
        public string ProjectId { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public string EquipmentQualityCode { get; set; }
        /// <summary>
        /// 所属单位ID
        /// </summary>
        public string UnitId { get; set; }
        /// <summary>
        /// 所属单位
        /// </summary>
        public string UnitName { get; set; }
        /// <summary>
        /// 设备类型
        /// </summary>
        public string SpecialEquipmentName { get; set; }
        /// <summary>
        /// 设备类型ID
        /// </summary>
        public string SpecialEquipmentId { get; set; }
        /// <summary>
        /// 设备名称
        /// </summary>
        public string EquipmentQualityName { get; set; }
        /// <summary>
        /// 出厂编号
        /// </summary>
        public string FactoryCode { get; set; }
        /// <summary>
        /// 规格型号
        /// </summary>
        public string SizeModel { get; set; }
        /// <summary>
        /// 合格证编号
        /// </summary>
        public string CertificateCode { get; set; }
        /// <summary>
        /// 最近检验时间
        /// </summary>
        public string CheckDate { get; set; }
        /// <summary>
        /// 有效期至
        /// </summary>
        public string LimitDate { get; set; }
        /// <summary>
        /// 进场时间
        /// </summary>
        public string InDate { get; set; }
        /// <summary>
        /// 出场时间
        /// </summary>
        public string OutDate { get; set; }
        /// <summary>
        /// 审核人
        /// </summary>
        public string ApprovalPerson { get; set; }
        /// <summary>
        /// 车牌号
        /// </summary>
        public string CarNum { get; set; }
        /// <summary>
        /// 编制人ID
        /// </summary>
        public string CompileManId { get; set; }
        /// <summary>
        /// 编制人
        /// </summary>
        public string CompileManName { get; set; }
        /// <summary>
        /// 编制日期
        /// </summary>
        public string CompileDate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 附件
        /// </summary>
        public string AttachUrl { get; set; }
    }
}
