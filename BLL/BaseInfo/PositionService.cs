using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 职务
    /// </summary>
    public static class PositionService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);

        /// <summary>
        /// 根据主键获取主键
        /// </summary>
        /// <param name="positionId"></param>
        /// <returns></returns>
        public static Model.Base_Position GetPositionById(string positionId)
        {
            return new Model.SGGLDB(Funs.ConnString).Base_Position.FirstOrDefault(e => e.PositionId == positionId);
        }

        /// <summary>
        /// 添加职务
        /// </summary>
        /// <param name="position"></param>
        public static void AddPosition(Model.Base_Position position)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Base_Position newPosition = new Model.Base_Position
            {
                PositionId = position.PositionId,
                PositionCode = position.PositionCode,
                PositionName = position.PositionName,
                Remark = position.Remark
            };
            db.Base_Position.InsertOnSubmit(newPosition);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改职务
        /// </summary>
        /// <param name="position"></param>
        public static void UpdatePosition(Model.Base_Position position)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Base_Position newPosition = db.Base_Position.FirstOrDefault(e => e.PositionId == position.PositionId);
            if (newPosition != null)
            {
                newPosition.PositionCode = position.PositionCode;
                newPosition.PositionName = position.PositionName;
                newPosition.Remark = position.Remark;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除职务
        /// </summary>
        /// <param name="positionId"></param>
        public static void DeletePositionById(string positionId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Base_Position position = db.Base_Position.FirstOrDefault(e => e.PositionId == positionId);
            if (position!=null)
            {
                db.Base_Position.DeleteOnSubmit(position);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 获取职务列表
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_Position> GetPositionList()
        {
            return (from x in new Model.SGGLDB(Funs.ConnString).Base_Position orderby x.PositionCode select x).ToList();
        }

        #region 职务表下拉框
        /// <summary>
        ///  职务表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitPositionDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease)
        {
            dropName.DataValueField = "PositionId";
            dropName.DataTextField = "PositionName";
            dropName.DataSource = BLL.PositionService.GetPositionList();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        #endregion
    }
}
