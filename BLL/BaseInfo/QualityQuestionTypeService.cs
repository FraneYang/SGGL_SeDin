using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Caching;
using System.Web.UI.WebControls;

namespace BLL
{
    public class QualityQuestionTypeService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 获取实体集合
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_QualityQuestionType> GetList()
        {
            var q = (from x in Funs.DB.Base_QualityQuestionType orderby x.SortIndex select x).ToList();
            return q;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="?"></param>
        public static void AddQualityQuestionType(Model.Base_QualityQuestionType qualityQuestionType)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_QualityQuestionType newQualityQuestionType = new Model.Base_QualityQuestionType
            {
                QualityQuestionTypeId = qualityQuestionType.QualityQuestionTypeId,
                QualityQuestionType = qualityQuestionType.QualityQuestionType,
                SortIndex = qualityQuestionType.SortIndex
            };

            db.Base_QualityQuestionType.InsertOnSubmit(newQualityQuestionType);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="teamGroup"></param>
        public static void UpdateQualityQuestionType(Model.Base_QualityQuestionType qualityQuestionType)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_QualityQuestionType newQualityQuestionType = db.Base_QualityQuestionType.FirstOrDefault(e => e.QualityQuestionTypeId == qualityQuestionType.QualityQuestionTypeId);
            if (newQualityQuestionType != null)
            {
                newQualityQuestionType.QualityQuestionType = qualityQuestionType.QualityQuestionType;
                newQualityQuestionType.SortIndex = qualityQuestionType.SortIndex;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="qualityQuestionTypeId"></param>
        public static void DeleteQualityQuestionTypeById(string qualityQuestionTypeId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.Base_QualityQuestionType qualityQuestionType = db.Base_QualityQuestionType.FirstOrDefault(e => e.QualityQuestionTypeId == qualityQuestionTypeId);
            {
                db.Base_QualityQuestionType.DeleteOnSubmit(qualityQuestionType);
                db.SubmitChanges();
            }
        }
        /// <summary>
        /// 质量问题类别下拉框
        /// </summary>
        /// <param name="dropName"></param>
        /// <param name="isShowPlease"></param>
        public static void InitQualityQuestionTypeDownList(FineUIPro.DropDownList dropName, bool isShowPlease)
        {
            dropName.DataValueField = "Value";
            dropName.DataTextField = "Text";
            dropName.DataSource = GetQualityQuestionTypeItem();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        
        public static void InitQualityQuestionType(FineUIPro.DropDownList dropName, bool isShowPlease)
        {
            dropName.DataValueField = "Text";
            dropName.DataTextField = "Text";
            dropName.DataSource = GetQualityQuestionTypeItem();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        /// <summary>
        /// 获取质量问题类别集合
        /// </summary>
        /// <returns></returns>
        public static ListItem[] GetQualityQuestionTypeItem()
        {
            var q = (from x in Funs.DB.Base_QualityQuestionType orderby x.SortIndex select x).ToList();
            ListItem[] list = new ListItem[q.Count()];
            for (int i = 0; i < q.Count(); i++)
            {
                list[i] = new ListItem(q[i].QualityQuestionType ?? "", q[i].QualityQuestionTypeId);
            }
            return list;
        }
        /// <summary>
        /// 获取一个质量问题类别信息
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public static Model.Base_QualityQuestionType GetQualityQuestionType(string QualityQuestionTypeId)
        {
            return Funs.DB.Base_QualityQuestionType.FirstOrDefault(e => e.QualityQuestionTypeId == QualityQuestionTypeId);
        }
    }
}
