using System.Web;
using System.Web.Http;
using Model;
using System.Collections.Generic;
using System.IO;
using System.Configuration;
using System;

namespace Mvc.Controllers
{
    public class CommenInfoController : ApiController
    {
        //
        // GET: /CommenInfo/
        [HttpGet]
        public ResponseData<List<Base_Unit>> GetSubUnitList(string projectId, string unitType = "", string name = "")
        {
            ResponseData<List<Base_Unit>> res = new ResponseData<List<Base_Unit>>();
            if (string.IsNullOrEmpty(name)) name = "";
            if (string.IsNullOrEmpty(unitType)) unitType = "";
            List<Project_ProjectUnit> q = BLL.ProjectUnitService.GetProjectUnitListByProjectIdForApi(projectId, unitType ,  name );
            List<Base_Unit> a = new List<Base_Unit>();
            for (int i = 0; i < q.Count; i++)
            {
                a.Add(BeanUtil.CopyOjbect<Base_Unit>(q[i].Base_Unit, true));
            }
            res.successful = true;
            res.resultValue = a;
            return res;
        }

        /// <summary>
        /// 获取质量问题类别
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResponseData<List<Base_QualityQuestionType>> GetQualityQuestionType()
        {
            ResponseData<List<Base_QualityQuestionType>> res = new ResponseData<List<Base_QualityQuestionType>>();
            List<Base_QualityQuestionType> q = BLL.QualityQuestionTypeService.GetList();
            List<Base_QualityQuestionType> a = new List<Base_QualityQuestionType>();
            for (int i = 0; i < q.Count; i++)
            {
                a.Add(BeanUtil.CopyOjbect<Base_QualityQuestionType>(q[i], true));
            }
            res.successful = true;
            res.resultValue = a;
            return res;
        }


        public ResponseData<List<ProjectData_MainItem>> GetMainItemList(string projectId, string unitWorks = "", string name = "")
        {
            ResponseData<List<ProjectData_MainItem>> res = new ResponseData<List<ProjectData_MainItem>>();
            if (string.IsNullOrEmpty(name)) name = "";
            if (string.IsNullOrEmpty(unitWorks)) unitWorks = "";
            res.resultValue = BLL.MainItemService.GetMainItemList(projectId, unitWorks, name);
            res.successful = true;

            return res;
        }



        [HttpGet]
        public ResponseData<List<WBS_UnitWork>> GetUnitWorkListByPid(string projectId)
        {
            ResponseData<List<WBS_UnitWork>> res = new ResponseData<List<WBS_UnitWork>>();
            List<WBS_UnitWork> a = BLL.UnitWorkService.GetUnitWorkListByPid(projectId);
            List<WBS_UnitWork> q = new List<WBS_UnitWork>();
            foreach (var u in a)
            {
                var temp = BeanUtil.CopyOjbect<WBS_UnitWork>(u, true);
                temp.UnitWorkId = u.UnitWorkId;
                q.Add(temp);

            }
            res.successful = true;
            res.resultValue = q;
            return res;
        }
        [HttpGet]
        public ResponseData<List<WBS_UnitWork>> GetUnitWorkListByPidAndPtype(string projectId,string projectType)
        {
            ResponseData<List<WBS_UnitWork>> res = new ResponseData<List<WBS_UnitWork>>();
            List<WBS_UnitWork> a = BLL.UnitWorkService.GetUnitWorkListByPidForApi(projectId, projectType);
            List<WBS_UnitWork> q = new List<WBS_UnitWork>();
            foreach (var u in a)
            {
                var temp = BeanUtil.CopyOjbect<WBS_UnitWork>(u, true);
                temp.UnitWorkId = u.UnitWorkId;
                q.Add(temp);

            }
            res.successful = true;
            res.resultValue = q;
            return res;
        }
        [HttpGet]
        public ResponseData<List<Base_CNProfessional>> GetCNProfessionalMainItem()
        {
            ResponseData<List<Base_CNProfessional>> res = new ResponseData<List<Base_CNProfessional>>();
            List<Base_CNProfessional> resList = new List<Base_CNProfessional>();
            var list = BLL.CNProfessionalService.GetList();
            if(list != null)
            {
                foreach(var item in list){
                    Base_CNProfessional main = new Base_CNProfessional();
                    main.CNProfessionalCode = item.CNProfessionalId;
                    main.SortIndex = item.SortIndex;
                    main.ProfessionalName = item.ProfessionalName;
                    resList.Add(main);
                }
            }
            res.successful = true;  
            res.resultValue = resList;
            return res;
        }
        [HttpGet]
        public ResponseData<List<Base_DesignProfessional>> GetDesignProfessionalMainItem()
        {
            ResponseData<List<Base_DesignProfessional>> res = new ResponseData<List<Base_DesignProfessional>>();
            List<Base_DesignProfessional> resList = new List<Base_DesignProfessional>();
            var list = BLL.DesignProfessionalService.GetList();
            if (list != null)
            {
                foreach (var item in list)
                {
                    Base_DesignProfessional main = new Base_DesignProfessional();
                    main.DesignProfessionalId = item.DesignProfessionalId;
                    main.SortIndex = item.SortIndex;
                    main.ProfessionalName = item.ProfessionalName;
                    resList.Add(main);
                }
            }
            res.successful = true;
            res.resultValue = resList;
            return res;
        }
        [HttpGet]
        public ResponseData<List<Sys_User>> FindAllUser()
        {
            ResponseData<List<Sys_User>> res = new ResponseData<List<Sys_User>>();
            List<Sys_User> list;

            list = BLL.UserService.GetUserList();

            List<Sys_User> users = new List<Sys_User>();
            foreach (var item in list)
            {
                users.Add(BeanUtil.CopyOjbect<Sys_User>(item, true));
            }
            res.successful = true;
            res.resultValue = users;
            return res;
        }
        [HttpGet]
        public ResponseData<List<Sys_User>> FindAllUser(string projectId, string name = "")
        {
            ResponseData<List<Sys_User>> res = new ResponseData<List<Sys_User>>();
            List<Sys_User> list;
            if (string.IsNullOrEmpty(name))
                name = "";

            list = BLL.UserService.GetProjectUserListByProjectIdForApi(projectId,name);
            List<Sys_User> users = new List<Sys_User>();
            foreach (var item in list)
            {
                users.Add(BeanUtil.CopyOjbect<Sys_User>(item, true));
            }
            res.successful = true;
            res.resultValue = users;
            return res;
        }
        [HttpGet]
        public ResponseData<List<Sys_User>> FindUserByUnit(string projectId,string unitId="",string unitType="", string name = "")
        {
            ResponseData<List<Sys_User>> res = new ResponseData<List<Sys_User>>();
            List<Sys_User> list;
            if (string.IsNullOrEmpty(unitId))
                unitId = "";
            if (string.IsNullOrEmpty(unitType))
                unitType = "";
            if (string.IsNullOrEmpty(name))
                name = "";
            list = BLL.UserService.GetProjectUserListByProjectIdForApi(projectId, unitId, unitType, name);
            List<Sys_User> users = new List<Sys_User>();
            foreach (var item in list)
            {
                users.Add(BeanUtil.CopyOjbect<Sys_User>(item, true));
            }
            res.successful = true;
            res.resultValue = users;
            return res;
        }
        [HttpGet]
        public ResponseData<Sys_User> GetUserById(string userId)
        {
            ResponseData<Sys_User> res = new ResponseData<Sys_User>();
            Model.Sys_User u = BLL.UserService.GetUserByUserId(userId);
            Model.Sys_User tempu = BeanUtil.CopyOjbect<Sys_User>(u, true);
            if (u.Base_Unit != null)
            {
                tempu.UnitId = u.UnitId + u.Base_Unit.UnitName;
            }
            res.successful = true;
            res.resultValue = tempu;
            return res;
        }
        [HttpGet]
        public ResponseData<List<WBS_WorkPackage>> GetAllWorkPackage(string unitWorkId)
        {
            ResponseData<List<WBS_WorkPackage>> res = new ResponseData<List<WBS_WorkPackage>>();
            List<WBS_WorkPackage> list = new List<WBS_WorkPackage>();
            List<WBS_WorkPackage> pagelist = BLL.WorkPackageService.GetAllWorkPackagesByUnitWorkId(  unitWorkId);
            foreach (WBS_WorkPackage w in pagelist)
            {
                WBS_WorkPackage temp = BeanUtil.CopyOjbect<WBS_WorkPackage>(w, true);
                temp.UnitWorkId = w.UnitWorkId;
                list.Add(temp);
            }
            res.resultValue = list;
            res.successful = true;
            return res;
        }
        [HttpGet]
        public ResponseData<List<WBS_ControlItemAndCycle>> GetControlItemAndCycle(string workPackageId)
        {
            ResponseData<List<WBS_ControlItemAndCycle>> res = new ResponseData<List<WBS_ControlItemAndCycle>>();
            List<WBS_ControlItemAndCycle> list = new List<WBS_ControlItemAndCycle>();
            List<WBS_ControlItemAndCycle> pagelist = BLL.ControlItemAndCycleService.GetListByWorkPackageIdForApi(workPackageId); ;
            foreach (WBS_ControlItemAndCycle w in pagelist)
            {
                var item = BeanUtil.CopyOjbect<WBS_ControlItemAndCycle>(w, true);
                item.CheckNum = w.CheckNum;
                list.Add(item);

            }
            res.resultValue = list;
            res.successful = true;
            return res;
        }
        [HttpGet]
        public ResponseData<List<Base_Unit>> FindAllUnit(string projectId="")
        {
            ResponseData<List<Base_Unit>> res = new ResponseData<List<Base_Unit>>();
            List<Base_Unit> list = BLL.UnitService.GetMainAndSubUnitByProjectIdList(projectId);
            List<Base_Unit> units = new List<Base_Unit>();
            foreach (var item in list)
            {
                units.Add(BeanUtil.CopyOjbect<Base_Unit>(item, true));
            }
            res.successful = true;
            res.resultValue = units;
            return res;
        }

        //
        // GET: /FileUpload/

        /// <summary> 
        /// 附件上传 
        /// </summary> 
        /// <returns></returns> 
        public String FileUploadForWX()
        {
            HttpFileCollection files = HttpContext.Current.Request.Files;
            string typeName = HttpContext.Current.Request["typeName"];
            if (string.IsNullOrEmpty(typeName))
            {
                typeName = "WebApi";
            }
            string reUrl = string.Empty;
            if (files != null && files.Count > 0)
            {
                string folderUrl = "FileUpLoad/" + typeName + "/" + DateTime.Now.ToString("yyyy-MM") + "/";
                string localRoot = ConfigurationManager.AppSettings["localRoot"] + folderUrl; //物理路径 
                if (!Directory.Exists(localRoot))
                {
                    Directory.CreateDirectory(localRoot);
                }
                foreach (string key in files.AllKeys)
                {
                    HttpPostedFile file = files[key];//file.ContentLength文件长度 
                    if (!string.IsNullOrEmpty(file.FileName))
                    {
                        string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                        file.SaveAs(localRoot + fileName);
                        if (string.IsNullOrEmpty(reUrl))
                        {
                            reUrl += folderUrl + fileName;
                        }
                        else
                        {
                            reUrl += "," + folderUrl + fileName;
                        }
                    }
                }
            }
            
            return reUrl;
        }

        //
        // GET: /FileUpload/

        /// <summary> 
        /// 附件上传 
        /// </summary> 
        /// <returns></returns> 
        public ResponseData<string> FileUpload()
        {
            HttpFileCollection files = HttpContext.Current.Request.Files;
            string typeName = HttpContext.Current.Request["typeName"];
            if (string.IsNullOrEmpty(typeName))
            {
                typeName = "WebApi";
            }
            string reUrl = string.Empty;
            if (files != null && files.Count > 0)
            {
                string folderUrl = "FileUpLoad/" + typeName + "/" + DateTime.Now.ToString("yyyy-MM") + "/";
                string localRoot = ConfigurationManager.AppSettings["localRoot"] + folderUrl; //物理路径 
                if (!Directory.Exists(localRoot))
                {
                    Directory.CreateDirectory(localRoot);
                }
                foreach (string key in files.AllKeys)
                {
                    HttpPostedFile file = files[key];//file.ContentLength文件长度 
                    if (!string.IsNullOrEmpty(file.FileName))
                    {
                        string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                        file.SaveAs(localRoot + fileName);
                        if (string.IsNullOrEmpty(reUrl))
                        {
                            reUrl += folderUrl + fileName;
                        }
                        else
                        {
                            reUrl += "," + folderUrl + fileName;
                        }
                    }
                }
            }
            ResponseData<string> res = new ResponseData<string>();
            res.successful = true;
            res.resultValue = reUrl;
            return res;
        }

        [HttpGet]
        public ResponseData<string> getDefaultCode(string projectId, string type)
        {
            ResponseData<string> res = new ResponseData<string>();
            res.successful = true;
            switch (type)
            {
                case "checkList":
                    {
                        string prefix = BLL.ProjectService.GetProjectByProjectId(projectId).ProjectCode + "-06-CM03-XJ-";
                        res.resultValue = BLL.SQLHelper.RunProcNewId("SpGetNewCode5", "dbo.Check_CheckControl", "DocCode", prefix);
                    }
                    break;
                case "jointCheck":
                    {
                        string prefix = "T18006-JC-";
                        res.resultValue = BLL.SQLHelper.RunProcNewId("SpGetNewCode3", "dbo.Check_JointCheck", "JointCheckCode", prefix);
                    }
                    break;
                case "TechnicalContact":
                    {
                        res.resultValue = BLL.SQLHelper.RunProcNewId2("SpGetNewCode3ByProjectId", "dbo.Check_TechnicalContactList", "Code", projectId);
                    }
                    break;
                case "design":
                    {
                        res.resultValue = BLL.SQLHelper.RunProcNewId2("SpGetNewCode3ByProjectId", "dbo.Check_Design", "DesignCode", projectId);
                    }
                    break;
                case "contact":
                    {
                        res.resultValue = BLL.SQLHelper.RunProcNewId2("SpGetNewCode3ByProjectId", "dbo.Unqualified_WorkContact", "Code", projectId);
                    }
                    break;
                case "spotCheck":
                    {
                        string prefix = BLL.ProjectService.GetProjectByProjectId(projectId).ProjectCode + "-06-CM03-XJ-";
                        res.resultValue = BLL.SQLHelper.RunProcNewId("SpGetNewCode5", "dbo.Check_SpotCheck", "DocCode", prefix);                        
                    }
                    break;

            }
            return res;
        }

    }
}
