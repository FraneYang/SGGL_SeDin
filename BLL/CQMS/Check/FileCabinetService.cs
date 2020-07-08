using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class FileCabinetService
    {
        public static Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
        /// <summary>
        /// 增加信息
        /// </summary>
        /// <param name="addFileCabinet">实体</param>
        public static void AddFileCabinet(Model.Project_FileCabinet addFileCabinet)
        {
            Model.Project_FileCabinet newFileCabinet = new Model.Project_FileCabinet
            {
                FileCabinetId = addFileCabinet.FileCabinetId,
                ProjectId = addFileCabinet.ProjectId,
                FileType = addFileCabinet.FileType,
                FileCode = addFileCabinet.FileCode,
                FileContent = addFileCabinet.FileContent,
                FileDate = addFileCabinet.FileDate,
                CreateManId = addFileCabinet.CreateManId,
                FileUrl = addFileCabinet.FileUrl
            };
            db.Project_FileCabinet.InsertOnSubmit(newFileCabinet);
            db.SubmitChanges();
        }
        /// <summary>
        /// 记录数
        /// </summary>
        private static int count
        {
            get;
            set;
        }
        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="fileCabinetId">信息Id</param>
        public static void DeleteFileCabinet(string fileCabinetId)
        {
            Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString);
            Model.Project_FileCabinet FileCabinet = db.Project_FileCabinet.FirstOrDefault(e => e.FileCabinetId == fileCabinetId);
            if (FileCabinet != null)
            {
                db.Project_FileCabinet.DeleteOnSubmit(FileCabinet);
                db.SubmitChanges();
            }
        }
        /// <summary>
        /// 获取单条记录(根据id)
        /// </summary>
        /// <param name="fileCabinetId"></param>
        /// <returns></returns>
        public static Model.Project_FileCabinet getInfo(string fileCabinetId)
        {
            return db.Project_FileCabinet.FirstOrDefault(e => e.FileCabinetId == fileCabinetId);
        }
        /// <summary>
        /// 获取重要文件
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static IList<Model.FileCabinetItem> getList(string projectId)
        {
            var fileCabinets = (from x in new Model.SGGLDB(Funs.ConnString).Project_FileCabinet
                                where x.ProjectId == projectId
                                select new Model.FileCabinetItem
                                {
                                    FileCabinetId = x.FileCabinetId,
                                    ProjectId = x.ProjectId,
                                    FileCode = x.FileCode,
                                    FileContent = x.FileContent,
                                    FileDate = x.FileDate,
                                    FileUrl = x.FileUrl,
                                    CreateManId = x.CreateManId,
                                    CreateManName = new Model.SGGLDB(Funs.ConnString).Sys_User.First(y => y.UserId == x.CreateManId).UserName,
                                }).ToList();
            return fileCabinets;
        }

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static IEnumerable<Model.FileCabinetItem> getListData(string type, string projectId, string unitWorkId, string cNProfessionalCode, int startRowIndex, int maximumRows)
        {
            IQueryable<Model.FileCabinetItem> q = getFileCabinetList(type, projectId, unitWorkId, cNProfessionalCode).AsQueryable();
            count = q.Count();
            if (count == 0)
            {
                return null;
            }
            return from x in q.Skip(startRowIndex).Take(maximumRows)
                   orderby x.FileDate descending
                   select x;
        }
        /// <summary>
        /// 修改信息
        /// </summary>
        /// <param name="updateFileCabinet">实体</param>
        public static void UpdateFileCabinet(Model.Project_FileCabinet updateFileCabinet)
        {
            Model.Project_FileCabinet getFileCabinet = db.Project_FileCabinet.FirstOrDefault(e => e.FileCabinetId == updateFileCabinet.FileCabinetId);
            if (getFileCabinet != null)
            {
                getFileCabinet.FileCode = updateFileCabinet.FileCode;
                getFileCabinet.FileContent = updateFileCabinet.FileContent;
                getFileCabinet.FileDate = updateFileCabinet.FileDate;
                getFileCabinet.CreateManId = updateFileCabinet.CreateManId;
                getFileCabinet.FileUrl = updateFileCabinet.FileUrl;
                db.SubmitChanges();
            }
        }


        /// <summary>
        /// 获取列表数
        /// </summary>
        /// <returns></returns>
        public static int getListCount(string type, string projectId, string unitWorkId, string cNProfessionalCode)
        {
            return count;
        }
        public static List<Model.FileCabinetItem> getFileCabinetList(string type, string projectId, string mainItemId, string cNProfessionalCode)
        {
            List<Model.FileCabinetItem> fileCabinets = new List<Model.FileCabinetItem>();
            /// 质量验收记录
            if (type == "1") //Check_SpotCheck
            {
                fileCabinets = (from x in new Model.SGGLDB(Funs.ConnString).Check_SpotCheck
                                where x.ProjectId == projectId
                                select new Model.FileCabinetItem
                                {
                                    FileCabinetId = x.SpotCheckCode,
                                    ProjectId = x.ProjectId,
                                    FileCode = x.DocCode,
                                    FileContent = x.DocCode,
                                    FileDate = x.SpotCheckDate,
                                    CreateManId = x.CreateMan,
                                    CreateManName = new Model.SGGLDB(Funs.ConnString).Sys_User.First(y => y.UserId == x.CreateMan).UserName,
                                    FileUrl = x.AttachUrl,
                                }).ToList();
            }
            else if (type == "2")
            {
                fileCabinets = (from x in new Model.SGGLDB(Funs.ConnString).Check_CheckControl
                                where x.ProjectId == projectId && x.State == BLL.Const.CheckControl_Complete
                                select new Model.FileCabinetItem
                                {
                                    FileCabinetId = x.CheckControlCode,
                                    ProjectId = x.ProjectId,
                                    FileCode = x.DocCode,
                                    FileContent = x.QuestionDef,
                                    FileDate = x.CheckDate,
                                    FileUrl = x.AttachUrl,
                                    CreateManId = x.CheckMan,
                                    CreateManName = new Model.SGGLDB(Funs.ConnString).Sys_User.First(e => e.UserId == x.CheckMan).UserName,
                                }).ToList();
            }
            else if (type == "3")
            {
                fileCabinets = (from x in new Model.SGGLDB(Funs.ConnString).Check_JointCheck
                                where x.ProjectId == projectId && x.State == BLL.Const.JointCheck_Complete
                                select new Model.FileCabinetItem
                                {
                                    FileCabinetId = x.JointCheckId,
                                    ProjectId = x.ProjectId,
                                    FileCode = x.JointCheckCode,
                                    FileContent = x.CheckName,
                                    FileDate = x.CheckDate,
                                    FileUrl = string.Empty,
                                    CreateManId = x.CheckMan,
                                    CreateManName = new Model.SGGLDB(Funs.ConnString).Sys_User.First(y => y.UserId == x.CheckMan).UserName,
                                }).ToList();
            }
            else if (type == "4")
            {
                fileCabinets = (from x in new Model.SGGLDB(Funs.ConnString).Check_TechnicalContactList
                                where x.ProjectId == projectId && x.State == Const.TechnicalContactList_Complete
                                select new Model.FileCabinetItem
                                {
                                    FileCabinetId = x.TechnicalContactListId,
                                    ProjectId = x.ProjectId,
                                    FileCode = x.Code,
                                    FileContent = (x.ContactListType == "1" ? "图纸类:" : "非图纸类:") + x.Cause,
                                    FileDate = x.CompileDate,
                                    FileUrl = x.AttachUrl,
                                    CreateManId = x.CompileMan,
                                    CreateManName = new Model.SGGLDB(Funs.ConnString).Sys_User.First(y => y.UserId == x.CompileMan).UserName,
                                }).ToList();
            }
            else if (type == "5")
            {
                var designs = from x in new Model.SGGLDB(Funs.ConnString).Check_Design
                              where x.ProjectId == projectId && x.State == Const.Design_Complete
                              select x;
                if (mainItemId != Const._Null)
                {
                    designs = from x in designs where x.MainItemId == mainItemId select x;
                }
                if (cNProfessionalCode != Const._Null)
                {
                    designs = from x in designs where x.CNProfessionalCode == cNProfessionalCode select x;
                }
                fileCabinets = (from x in designs
                                select new Model.FileCabinetItem
                                {
                                    FileCabinetId = x.DesignId,
                                    ProjectId = x.ProjectId,
                                    FileCode = x.DesignCode,
                                    FileContent = x.DesignContents,
                                    FileDate = x.DesignDate,
                                    FileUrl = x.AttachUrl,
                                    CreateManId = x.CompileMan,
                                    CreateManName = new Model.SGGLDB(Funs.ConnString).Sys_User.First(y => y.UserId == x.CompileMan).UserName,
                                }).ToList();
            }
            else if (type == "6")
            {
                fileCabinets = (from x in new Model.SGGLDB(Funs.ConnString).Check_CheckMonth
                                where x.ProjectId == projectId
                                select new Model.FileCabinetItem
                                {
                                    FileCabinetId = x.CheckMonthId,
                                    ProjectId = x.ProjectId,
                                    FileCode = string.Format("{0:yyyy-MM}", x.Months),
                                    FileContent = "发出整改项" + x.ThisRectifyNum.ToString() + ",关闭整改项" + x.ThisOKRectifyNum.ToString() + "；实体验收完成数" + x.ThisSpotCheckNum.ToString() + "；一次检验合格率" + x.OnesOKRate + "。",
                                    FileDate = x.CompileDate,
                                    FileUrl = string.Empty,
                                    CreateManId = x.CompileMan,
                                    CreateManName = new Model.SGGLDB(Funs.ConnString).Sys_User.First(y => y.UserId == x.CompileMan).UserName,
                                }).ToList();
            }
            else if (type == "7")
            {
                fileCabinets = (from x in new Model.SGGLDB(Funs.ConnString).Solution_CQMSConstructSolution
                                where x.ProjectId == projectId && x.State == Const.CQMSConstructSolution_Complete
                                select new Model.FileCabinetItem
                                {
                                    FileCabinetId = x.ConstructSolutionId,
                                    ProjectId = x.ProjectId,
                                    FileCode = x.Code,
                                    FileContent = x.SolutionName,
                                    FileDate = x.CompileDate,
                                    FileUrl = x.AttachUrl,
                                    CreateManId = x.CompileMan,
                                    CreateManName = new Model.SGGLDB(Funs.ConnString).Sys_User.First(y => y.UserId == x.CompileMan).UserName,
                                }).ToList();
            }
            else if (type == "8")
            {
                fileCabinets = (from x in new Model.SGGLDB(Funs.ConnString).Unqualified_WorkContact
                                where x.ProjectId == projectId && x.State == Const.WorkContact_Complete
                                select new Model.FileCabinetItem
                                {
                                    FileCabinetId = x.WorkContactId,
                                    ProjectId = x.ProjectId,
                                    FileCode = x.Code,
                                    FileContent = x.Cause,
                                    FileDate = x.CompileDate,
                                    FileUrl = x.AttachUrl,
                                    CreateManId = x.CompileMan,
                                    CreateManName = new Model.SGGLDB(Funs.ConnString).Sys_User.First(y => y.UserId == x.CompileMan).UserName,
                                }).ToList();
            }
            else if (type == "-1")
            {
                fileCabinets = (from x in new Model.SGGLDB(Funs.ConnString).Project_FileCabinet
                                where x.ProjectId == projectId
                                select new Model.FileCabinetItem
                                {
                                    FileCabinetId = x.FileCabinetId,
                                    ProjectId = x.ProjectId,
                                    FileCode = x.FileCode,
                                    FileContent = x.FileContent,
                                    FileDate = x.FileDate,
                                    FileUrl = x.FileUrl,
                                    CreateManId = x.CreateManId,
                                    CreateManName = new Model.SGGLDB(Funs.ConnString).Sys_User.First(y => y.UserId == x.CreateManId).UserName,
                                }).ToList();
            }

            return fileCabinets;
        }
    }
}
