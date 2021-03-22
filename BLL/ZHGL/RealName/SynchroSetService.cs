using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace BLL
{
    public class SynchroSetService
    {
        public static Model.SGGLDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取同步设置
        /// </summary>
        /// <param name="SynchroSetId"></param>
        /// <returns></returns>
        public static Model.RealName_SynchroSet GetSynchroSetById(string SynchroSetId)
        {
            return Funs.DB.RealName_SynchroSet.FirstOrDefault(e => e.SynchroSetId == SynchroSetId);
        }
        /// <summary>
        /// 根据单位ID获取同步设置
        /// </summary>
        /// <param name="SynchroSetId"></param>
        /// <returns></returns>
        public static Model.RealName_SynchroSet GetSynchroSetByUnitId(string unitId, string proCode)
        {
            //return Funs.DB.RealName_SynchroSet.FirstOrDefault(e => e.UnitId == unitId);
            if (!string.IsNullOrEmpty(proCode))
            {
                return Funs.DB.RealName_SynchroSet.FirstOrDefault(e => e.UnitId == unitId && e.ProCode == proCode);
            }
            else
            {
                return Funs.DB.RealName_SynchroSet.FirstOrDefault(e => e.UnitId == unitId);
            }
        }

        #region 国家基础数据下拉框
        /// <summary>
        ///  表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitCountryDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease)
        {
            dropName.DataValueField = "CountryId";
            dropName.DataTextField = "Cname";
            dropName.DataSource = (from x in Funs.DB.RealName_Country orderby x.Cname select x).ToList();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        #endregion

        #region 项目基础数据下拉框
        /// <summary>
        ///  表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitProjectDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease)
        {
            dropName.DataValueField = "ProCode";
            dropName.DataTextField = "ProName";
            dropName.DataSource = (from x in Funs.DB.RealName_Project
                                   orderby x.ProCode
                                   select x).ToList();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        #endregion

        #region 同步设置 数据维护
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SynchroSet"></param>
        public static void SaveSynchroSet(Model.RealName_SynchroSet SynchroSet)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getSynchroSet = db.RealName_SynchroSet.FirstOrDefault(e => e.UnitId == SynchroSet.UnitId && e.ProCode == SynchroSet.ProCode);
                if (getSynchroSet != null)
                {
                    getSynchroSet.ApiUrl = Funs.RealNameApiUrl;
                    getSynchroSet.ClientId = SynchroSet.ClientId;
                    getSynchroSet.UserName = SynchroSet.UserName;
                    getSynchroSet.Password = SynchroSet.Password;
                    getSynchroSet.Timestamp = SynchroSet.Timestamp;
                    getSynchroSet.ProCode = SynchroSet.ProCode;
                    if (!string.IsNullOrEmpty(SynchroSet.Token))
                    {
                        getSynchroSet.Token = SynchroSet.Token;
                        getSynchroSet.Tokenendtime = SynchroSet.Tokenendtime;
                    }
                    getSynchroSet.Intervaltime = SynchroSet.Intervaltime;
                    db.SubmitChanges();
                }
                else
                {
                    SynchroSet.SynchroSetId = SQLHelper.GetNewID();
                    db.RealName_SynchroSet.InsertOnSubmit(SynchroSet);
                    db.SubmitChanges();
                }
            }
        }

        /// <summary>
        /// 根据主键删除同步设置
        /// </summary>
        /// <param name="SynchroSetId"></param>
        public static void DeleteSynchroSetById(string SynchroSetId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.RealName_SynchroSet SynchroSet = db.RealName_SynchroSet.FirstOrDefault(e => e.SynchroSetId == SynchroSetId);
            if (SynchroSet != null)
            {
                db.RealName_SynchroSet.DeleteOnSubmit(SynchroSet);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除同步设置
        /// </summary>
        /// <param name="SynchroSetId"></param>
        public static void DeleteSynchroSetByUnitId(string unitId)
        {
            Model.SGGLDB db = Funs.DB;
            Model.RealName_SynchroSet SynchroSet = db.RealName_SynchroSet.FirstOrDefault(e => e.UnitId == unitId);
            if (SynchroSet != null)
            {
                db.RealName_SynchroSet.DeleteOnSubmit(SynchroSet);
                db.SubmitChanges();
            }
        }
        #endregion

        #region 获取access_token信息
        /// <summary>
        /// 获取凭证
        /// </summary>
        /// <returns></returns>
        public static string getaccess_token(string proCode)
        {
            string access_token = string.Empty;
            var getToken = GetSynchroSetByUnitId(Const.UnitId_SEDIN, proCode);
            if (getToken != null)
            {
                if (getToken.Tokenendtime > DateTime.Now)
                {
                    access_token = getToken.Token;
                }
                else
                {
                    access_token = SaveToken(getToken);
                }
            }
            return access_token;
        }

        /// <summary>
        ///  获取access_token信息
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string SaveToken(Model.RealName_SynchroSet SynchroSet)
        {
            try
            {
                string access_token = string.Empty;
                string clientId = SynchroSet.ClientId;
                string userName = SynchroSet.UserName;
                string password = Funs.EncryptionPassword(SynchroSet.Password); 
                string timestamp = string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now.AddMinutes(-2));
                string sign = Funs.EncryptionPassword(clientId + userName + password + timestamp);
                var getToken = new
                {
                    clientId,
                    userName,
                    password,
                    timestamp,
                    sign
                };
                string contenttype = "application/json;charset=utf-8";
                var returndata = BLL.APIGetHttpService.OutsideHttp(Funs.RealNameApiUrl + "/foreignApi/auth/accessToken", "POST", contenttype, null, JsonConvert.SerializeObject(getToken));
                if (!string.IsNullOrEmpty(returndata))
                {
                    JObject obj = JObject.Parse(returndata);
                    if (obj["success"] != null && Convert.ToBoolean(obj["success"].ToString()))
                    {
                        access_token = obj["data"].ToString();
                        SynchroSet.Token = access_token;
                        SynchroSet.Tokenendtime = DateTime.Now.AddHours(23);
                        SynchroSet.Timestamp = Funs.GetNewDateTime(timestamp);
                        SaveSynchroSet(SynchroSet);
                        InsertRealNamePushLog(null, SynchroSet.ProCode, "获取凭证", obj["success"].ToString(), obj["code"].ToString(), obj["message"].ToString());
                    }
                    else
                    {
                        InsertRealNamePushLog(null, SynchroSet.ProCode, "获取凭证", "False", "", returndata);
                    }
                }
                else
                {
                    InsertRealNamePushLog(null, SynchroSet.ProCode, "获取凭证", "False", "", "获取凭证失败" + JsonConvert.SerializeObject(getToken));
                }
                return access_token;
            }
            catch (Exception ex)
            {
                ErrLogInfo.WriteLog(ex, " 获取access_token信息", "SynchroSetService.SaveToken");
                return "";
            }
        }
        #endregion

        #region 实名制推送记录日志
        /// <summary>
        ///  实名制推送记录日志
        /// </summary>
        public static void InsertRealNamePushLog(string ProjectId, string ProjectCode, string PushType, string Success, string Code, string Message)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                Model.RealName_PushLog newLog = new Model.RealName_PushLog
                {
                    PushLogId = SQLHelper.GetNewID(),
                    ProjectId = ProjectId,
                    ProjectCode = ProjectCode,
                    PushType = PushType,
                    Success = Success,
                    Code = Code,
                    Message = Message,
                    PushTime = DateTime.Now,
                };
                db.RealName_PushLog.InsertOnSubmit(newLog);
                db.SubmitChanges();
            }
        }
        #endregion

        #region 获取基础字典数据
        /// <summary>
        ///  获取基础字典数据
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string getBasicData(string type)
        {
            try
            {
                Model.SGGLDB db = Funs.DB;
                string mess = string.Empty;
                string sucess = string.Empty;
                string code = string.Empty;
                string contenttype = "application/json;charset=unicode";
                string url = Funs.RealNameApiUrl + "/foreignApi/baseData/getDict";
                var dictTypeCode = new
                {
                    dictTypeCode = type
                };
                Hashtable newToken = new Hashtable
                {
                    { "token", getaccess_token(null) }
                };
                var returndata = BLL.APIGetHttpService.OutsideHttp(url, "POST", contenttype, newToken, JsonConvert.SerializeObject(dictTypeCode));
                if (!string.IsNullOrEmpty(returndata))
                {
                    JObject obj = JObject.Parse(returndata);
                    mess = obj["message"].ToString();
                    code = obj["code"].ToString();
                    sucess = obj["success"].ToString();
                    if (Convert.ToBoolean(obj["success"].ToString()))
                    {
                        JArray arr = JArray.Parse(obj["data"].ToString());
                        foreach (var item in arr)
                        {
                            string dictCode = item["dictCode"].ToString();
                            string name = item["dictName"].ToString();
                            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(dictCode))
                            {
                                var getBasicData = db.RealName_BasicData.FirstOrDefault(x => x.DictCode == dictCode && x.DictTypeCode == type);
                                if (getBasicData != null)
                                {
                                    getBasicData.DictName = name;
                                    db.SubmitChanges();
                                }
                                else
                                {
                                    Model.RealName_BasicData newBasicData = new Model.RealName_BasicData
                                    {
                                        BasicDataId = SQLHelper.GetNewID(),
                                        DictTypeCode = type,
                                        DictCode = dictCode,
                                        DictName = name
                                    };
                                    db.RealName_BasicData.InsertOnSubmit(newBasicData);
                                    db.SubmitChanges();
                                }

                                if (type == "LAB_WORK_TYPE")
                                {
                                    var getWorkPost = Funs.DB.Base_WorkPost.FirstOrDefault(x => x.WorkPostCode == dictCode);
                                    if (getWorkPost == null)
                                    {
                                        Model.Base_WorkPost newWorkPost = new Model.Base_WorkPost
                                        {
                                            WorkPostId = SQLHelper.GetNewID(),
                                            WorkPostCode = dictCode,
                                            WorkPostName = name,
                                            PostType = "3",
                                            Remark = "来源实名制系统"
                                        };
                                        Funs.DB.Base_WorkPost.InsertOnSubmit(newWorkPost);
                                        Funs.DB.SubmitChanges();
                                    }
                                }
                            }
                        }
                    }
                }
                InsertRealNamePushLog(null, null, "获取基础字典[" + type + "]", sucess, code, mess);
                return mess;
            }
            catch (Exception ex)
            {
                ErrLogInfo.WriteLog(ex, "获取基础字典", "SynchroSetService.getBasicData");
                return "";
            }
        }
        #endregion

        #region 获取国家字典数据
        /// <summary>
        ///  获取基础字典数据
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string getCountry()
        {
            Model.SGGLDB db = Funs.DB;
            string mess = string.Empty;
            string sucess = string.Empty;
            string code = string.Empty;
            string contenttype = "application/json;charset=unicode";

            string url = Funs.RealNameApiUrl + "/foreignApi/baseData/getCountry";
            Hashtable newToken = new Hashtable
                {
                    { "token", getaccess_token(null) }
                };
            var returndata = BLL.APIGetHttpService.OutsideHttp(url, "POST", contenttype, newToken, null);
            if (!string.IsNullOrEmpty(returndata))
            {
                JObject obj = JObject.Parse(returndata);
                mess = obj["message"].ToString();
                code = obj["code"].ToString();
                sucess = obj["success"].ToString();
                if (Convert.ToBoolean(obj["success"].ToString()))
                {
                    JArray arr = JArray.Parse(obj["data"].ToString());
                    foreach (var item in arr)
                    {
                        string countryId = item["countryId"].ToString();
                        string cname = item["cname"].ToString();
                        string name = item["name"].ToString();
                        if (!string.IsNullOrEmpty(countryId))
                        {
                            var getCountry = db.RealName_Country.FirstOrDefault(x => x.CountryId == countryId);
                            if (getCountry != null)
                            {
                                getCountry.Cname = cname;
                                getCountry.Name = name;
                                db.SubmitChanges();
                            }
                            else
                            {
                                Model.RealName_Country newCountry = new Model.RealName_Country
                                {
                                    ID = SQLHelper.GetNewID(),
                                    CountryId = countryId,
                                    Cname = cname,
                                    Name = name
                                };
                                db.RealName_Country.InsertOnSubmit(newCountry);
                                db.SubmitChanges();
                            }
                        }
                    }
                }
            }
            InsertRealNamePushLog(null, null, "获取国家数据", sucess, code, mess);
            return mess;
        }
        #endregion

        #region 获取省份字典数据
        /// <summary>
        ///  获取基础字典数据
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string getCity(string countryId)
        {
            Model.SGGLDB db = Funs.DB;
            string mess = string.Empty;
            string sucess = string.Empty;
            string code = string.Empty;
            string contenttype = "application/json;charset=unicode";

            string url = Funs.RealNameApiUrl + "/foreignApi/baseData/getCity";
            var getCountrys = from x in db.RealName_Country where countryId == null || x.CountryId == countryId select x;
            foreach (var citem in getCountrys)
            {
                var country = new
                {
                    countryId = citem.CountryId
                };
                Hashtable newToken = new Hashtable
                {
                    { "token", getaccess_token(null) }
                };
                var returndata = BLL.APIGetHttpService.OutsideHttp(url, "POST", contenttype, newToken, JsonConvert.SerializeObject(country));
                if (!string.IsNullOrEmpty(returndata))
                {
                    JObject obj = JObject.Parse(returndata);
                    mess = obj["message"].ToString();
                    code = obj["code"].ToString();
                    sucess = obj["success"].ToString();
                    if (Convert.ToBoolean(obj["success"].ToString()))
                    {
                        JArray arr = JArray.Parse(obj["data"].ToString());
                        foreach (var item in arr)
                        {
                            string provinceCode = item["provinceCode"].ToString();
                            string cityCode = item["cityCode"].ToString();
                            string cname = item["cname"].ToString();
                            string cnShortName = item["cnShortName"].ToString();
                            string name = item["name"].ToString();
                            if (!string.IsNullOrEmpty(provinceCode))
                            {
                                var getCity = db.RealName_City.FirstOrDefault(x => x.CountryId == citem.CountryId && x.ProvinceCode == provinceCode);
                                if (getCity == null)
                                {
                                    Model.RealName_City newCity = new Model.RealName_City
                                    {
                                        ID = SQLHelper.GetNewID(),
                                        CountryId = citem.CountryId,
                                        ProvinceCode = provinceCode,
                                        CityCode = cityCode,
                                        Cname = cname,
                                        CnShortName = cnShortName,
                                        Name = name
                                    };
                                    db.RealName_City.InsertOnSubmit(newCity);
                                    db.SubmitChanges();
                                }
                            }
                        }
                    }
                }
            }
            InsertRealNamePushLog(null, null, "获取省份数据", sucess, code, mess);
            return mess;
        }
        #endregion

        #region 获取项目数据
        /// <summary>
        ///  获取项目数据
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string getProject()
        {
            Model.SGGLDB db = Funs.DB;
            string mess = string.Empty;
            string sucess = string.Empty;
            string code = string.Empty;
            string proCode = string.Empty;
            string contenttype = "application/json;charset=unicode";

            string url = Funs.RealNameApiUrl + "/foreignApi/baseData/getProjectList";
            Hashtable newToken = new Hashtable
                {
                    { "token", getaccess_token(null) }
                };
            var returndata = BLL.APIGetHttpService.OutsideHttp(url, "POST", contenttype, newToken, null);
            if (!string.IsNullOrEmpty(returndata))
            {
                JObject obj = JObject.Parse(returndata);
                mess = obj["message"].ToString();
                code = obj["code"].ToString();
                sucess = obj["success"].ToString();
                if (Convert.ToBoolean(obj["success"].ToString()))
                {
                    JArray arr = JArray.Parse(obj["data"].ToString());
                    foreach (var item in arr)
                    {
                        proCode = item["proCode"].ToString();
                        string proName = item["proName"].ToString();
                        string proShortName = item["proShortName"].ToString();
                        if (!string.IsNullOrEmpty(proCode))
                        {
                            var getProject = db.RealName_Project.FirstOrDefault(x => x.ProCode == proCode);
                            if (getProject != null)
                            {
                                getProject.ProName = proName;
                                getProject.ProShortName = proShortName;
                                db.SubmitChanges();
                            }
                            else
                            {
                                Model.RealName_Project newProject = new Model.RealName_Project
                                {
                                    ID = SQLHelper.GetNewID(),
                                    ProCode = proCode,
                                    ProName = proName,
                                    ProShortName = proShortName
                                };
                                db.RealName_Project.InsertOnSubmit(newProject);
                                db.SubmitChanges();
                            }
                        }
                    }
                }
            }
            InsertRealNamePushLog(null, proCode, "获取项目数据", sucess, code, mess);
            return mess;
        }
        #endregion

        #region 获取施工队字典数据
        /// <summary>
        ///  获取基础字典数据
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string getCollTeam(string proCode)
        {
            try
            {
                string mess = string.Empty;
                string sucess = string.Empty;
                string code = string.Empty;
                string contenttype = "application/json;charset=unicode";
                string url = Funs.RealNameApiUrl + "/foreignApi/baseData/getCollTeam";
                var getProjects = from x in Funs.DB.RealName_Project where proCode == null || x.ProCode == proCode select x;
                foreach (var citem in getProjects)
                {
                    var getProject = new
                    {
                        proCode = citem.ProCode
                    };
                    Hashtable newToken = new Hashtable
                {
                    { "token", getaccess_token(citem.ProCode) }
                };
                    var returndata = BLL.APIGetHttpService.OutsideHttp(url, "POST", contenttype, newToken, JsonConvert.SerializeObject(getProject));
                    if (!string.IsNullOrEmpty(returndata))
                    {
                        JObject obj = JObject.Parse(returndata);
                        mess = obj["message"].ToString();
                        code = obj["code"].ToString();
                        sucess = obj["success"].ToString();
                        if (Convert.ToBoolean(obj["success"].ToString()))
                        {
                            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
                            {
                                JArray arr = JArray.Parse(obj["data"].ToString());
                                foreach (var item in arr)
                                {
                                    long? teamId = Funs.GetNewlong(item["teamId"].ToString());
                                    string teamName = item["teamName"].ToString();
                                    string teamLeaderName = item["teamLeaderName"].ToString();
                                    string teamLeaderMobile = item["teamLeaderMobile"].ToString();
                                    string thirdTeamCode = item["thirdTeamCode"].ToString();
                                    if (teamId.HasValue)
                                    {
                                        var getCollTeam = db.RealName_CollTeam.FirstOrDefault(x => x.ProCode == citem.ProCode && x.TeamId == teamId);
                                        if (getCollTeam == null)
                                        {
                                            Model.RealName_CollTeam newCollTeam = new Model.RealName_CollTeam
                                            {
                                                ID = SQLHelper.GetNewID(),
                                                TeamId = teamId,
                                                ProCode = citem.ProCode,
                                                TeamName = teamName,
                                                TeamLeaderName = teamLeaderName,
                                                TeamLeaderMobile = teamLeaderMobile
                                            };
                                            db.RealName_CollTeam.InsertOnSubmit(newCollTeam);
                                            db.SubmitChanges();
                                        }
                                        else
                                        {
                                            if (getCollTeam.TeamName != teamName || getCollTeam.TeamLeaderName != teamLeaderName)
                                            {
                                                getCollTeam.TeamName = teamName;
                                                getCollTeam.TeamLeaderName = teamLeaderName;
                                                getCollTeam.TeamLeaderMobile = teamLeaderMobile;
                                                db.SubmitChanges();
                                            }
                                        }

                                        var getTeamGroup = db.ProjectData_TeamGroup.FirstOrDefault(x => x.TeamGroupId == thirdTeamCode && (x.TeamId != teamId || x.TeamId == null));
                                        if (getTeamGroup != null)
                                        {
                                            getTeamGroup.TeamId = teamId;
                                            getTeamGroup.RealNamePushTime = DateTime.Now;
                                            db.SubmitChanges();
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            InsertRealNamePushLog(null, proCode, "获取施工队数据", sucess, code, mess);
                        }
                    }
                    else
                    {
                        InsertRealNamePushLog(null, proCode, "获取施工队数据", sucess, code, mess);
                    }
                }
                return mess;
            }
            catch (Exception ex)
            {
                ErrLogInfo.WriteLog(ex, "获取施工队数据", "SynchroSetService.getCollTeam");
                return "";
            }
        }
        #endregion

        #region 推送参建企业数据
        /// <summary>
        ///  推送参建企业数据
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string PushCollCompany()
        {
            Model.SGGLDB db = Funs.DB;
            string mess = string.Empty;
            string sucess = string.Empty;
            string code = string.Empty;
            string contenttype = "application/json;charset=unicode";
            string url = Funs.RealNameApiUrl + "/foreignApi/accept/collCompany";
            var getUnit = (from x in db.Base_Unit
                           where !x.RealNamePushTime.HasValue && x.CollCropCode != null
                           select x).Take(200).ToList();
            if (getUnit.Count() > 0)
            {
                var listObject = new
                {
                    list = getUnit.Select(x => new { collCompanyName = x.UnitName, collCropCode = x.CollCropCode, isChina = x.IsChina })
                };
                Hashtable newToken = new Hashtable
                    {
                        { "token", getaccess_token(null) }
                    };
                var returndata = BLL.APIGetHttpService.OutsideHttp(url, "POST", contenttype, newToken, JsonConvert.SerializeObject(listObject));
                if (!string.IsNullOrEmpty(returndata))
                {
                    JObject obj = JObject.Parse(returndata);
                    mess = obj["message"].ToString();
                    code = obj["code"].ToString();
                    sucess = obj["success"].ToString();
                    if (Convert.ToBoolean(obj["success"].ToString()))
                    {
                        foreach (var item in getUnit)
                        {
                            item.RealNamePushTime = DateTime.Now;
                            db.SubmitChanges();
                        }
                    }
                }

                InsertRealNamePushLog(null, null, "推送参建企业数据", sucess, code, mess);
            }
            else
            {
                mess = "没有符合条件的数据！";
            }

            return mess;
        }
        #endregion

        #region 推送项目参建企业数据
        /// <summary>
        ///  推送项目参建企业数据
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string PushProCollCompany(string proCode)
        {
            Model.SGGLDB db = Funs.DB;
            string mess = string.Empty;
            string sucess = string.Empty;
            string code = string.Empty;
            string contenttype = "application/json;charset=unicode";

            string url = Funs.RealNameApiUrl + "/foreignApi/accept/proCollCompany";
            var getProjectUnits = (from x in db.Project_ProjectUnit
                                   join y in db.Base_Project on x.ProjectId equals y.ProjectId
                                   join z in db.RealName_Project on y.ProjectCode equals z.ProCode
                                   join u in db.Base_Unit on x.UnitId equals u.UnitId
                                   where z.ProCode != null && !x.RealNamePushTime.HasValue && x.UnitType != null
                                   && (proCode == null || y.ProjectCode == proCode)
                                   && u.CollCropCode != null && u.CollCropCode != ""
                                   select new
                                   {
                                       proCode = y.ProjectCode,
                                       collCropCode = u.CollCropCode,
                                       collCropType = db.Sys_Const.First(t => t.GroupId == ConstValue.Group_ProjectUnitType && t.ConstValue == x.UnitType).Remark,
                                       entryTime = string.Format("{0:yyyy-MM-dd}", x.InTime),
                                       exitTime = string.Format("{0:yyyy-MM-dd}", x.OutTime),
                                       linkName = u.LinkName,
                                       idcardType = u.IdcardType,
                                       idcardNumber = u.IdcardNumber,
                                       linkMobile = u.LinkMobile,
                                       collCropStatus = u.CollCropStatus,
                                       x.RealNamePushTime,
                                       x.ProjectUnitId,
                                   }).Take(200).ToList();
            if (getProjectUnits.Count() > 0)
            {
                var listObject = new
                {
                    list = getProjectUnits.Select(x => new { x.proCode, x.collCropCode, x.collCropType, x.entryTime, x.exitTime, x.linkName, x.idcardType, x.idcardNumber, x.linkMobile, x.collCropStatus, })
                };
                Hashtable newToken = new Hashtable
                    {
                        { "token", getaccess_token(proCode) }
                    };
                var returndata = BLL.APIGetHttpService.OutsideHttp(url, "POST", contenttype, newToken, JsonConvert.SerializeObject(listObject));
                if (!string.IsNullOrEmpty(returndata))
                {
                    JObject obj = JObject.Parse(returndata);
                    mess = obj["message"].ToString();
                    code = obj["code"].ToString();
                    sucess = obj["success"].ToString();
                    if (Convert.ToBoolean(obj["success"].ToString()))
                    {
                        foreach (var item in getProjectUnits)
                        {
                            var getPUnit = db.Project_ProjectUnit.FirstOrDefault(x => x.ProjectUnitId == item.ProjectUnitId);
                            if (getPUnit != null)
                            {
                                getPUnit.RealNamePushTime = DateTime.Now;
                                db.SubmitChanges();
                            }
                        }
                    }
                }
                InsertRealNamePushLog(null, proCode, "推送项目参建企业数据", sucess, code, mess);
            }
            else
            {
                mess = "没有符合条件的数据！";
            }

            return mess;
        }
        #endregion

        #region 推送项目部/施工队（以下简称施工队）数据
        /// <summary>
        ///  推送项目部/施工队（以下简称施工队）数据
        /// </summary>
        /// <returns></returns>
        public static string PushCollTeam(string proCode)
        {
            Model.SGGLDB db = Funs.DB;
            string mess = string.Empty;
            string sucess = string.Empty;
            string code = string.Empty;
            string contenttype = "application/json;charset=unicode";

            string url = Funs.RealNameApiUrl + "/foreignApi/accept/collTeam";
            var getCollTeam = (from x in db.ProjectData_TeamGroup
                               join y in db.Base_Project on x.ProjectId equals y.ProjectId
                               join z in db.RealName_Project on y.ProjectCode equals z.ProCode
                               join u in db.Base_Unit on x.UnitId equals u.UnitId
                               join s in db.SitePerson_Person on x.GroupLeaderId equals s.PersonId into jonPerson
                               from s in jonPerson.DefaultIfEmpty()
                               where z.ProCode != null && !x.RealNamePushTime.HasValue && x.TeamTypeId != null
                                 && (proCode == null || y.ProjectCode == proCode)
                                 && u.CollCropCode != null && u.CollCropCode != ""
                               select new
                               {
                                   proCode = y.ProjectCode,
                                   collCropCode = u.CollCropCode,
                                   teamType = x.TeamTypeId,
                                   teamName = x.TeamGroupName,
                                   thirdTeamCode = x.TeamGroupId,
                                   entryTime = string.Format("{0:yyyy-MM-dd}", x.EntryTime),
                                   exitTime = string.Format("{0:yyyy-MM-dd}", x.ExitTime),
                                   teamLeaderName = s.PersonName,
                                   teamLeaderIdcardType = s.IdcardType,
                                   teamLeaderIdcardNumber = s.IdentityCard,
                                   teamLeaderMobile = s.Telephone,
                                   x.RealNamePushTime,
                                   x.TeamGroupId,
                               }).Take(200).ToList();
            if (getCollTeam.Count() > 0)
            {
                var listObject = new
                {
                    list = getCollTeam.Select(x => new { x.proCode, x.collCropCode, x.teamType, x.teamName, x.thirdTeamCode, x.entryTime, x.exitTime, x.teamLeaderName, x.teamLeaderIdcardType, x.teamLeaderIdcardNumber, x.teamLeaderMobile })
                };
                Hashtable newToken = new Hashtable
                    {
                        { "token", getaccess_token(proCode) }
                    };
                var returndata = BLL.APIGetHttpService.OutsideHttp(url, "POST", contenttype, newToken, JsonConvert.SerializeObject(listObject));
                if (!string.IsNullOrEmpty(returndata))
                {
                    JObject obj = JObject.Parse(returndata);
                    mess = obj["message"].ToString();
                    code = obj["code"].ToString();
                    sucess = obj["success"].ToString();
                    if (Convert.ToBoolean(obj["success"].ToString()))
                        if (obj["success"] != null && Convert.ToBoolean(obj["success"].ToString()))
                        {
                            foreach (var item in getCollTeam)
                            {
                                var getTeamGroup = db.ProjectData_TeamGroup.FirstOrDefault(x => x.TeamGroupId == item.TeamGroupId);
                                if (getTeamGroup != null)
                                {
                                    getTeamGroup.RealNamePushTime = DateTime.Now;
                                    db.SubmitChanges();
                                }
                            }
                        }
                }
                InsertRealNamePushLog(null, proCode, "推送项目部/施工队数据", sucess, code, mess);
            }
            else
            {
                mess = "没有符合条件的数据！";
            }

            return mess;
        }
        #endregion

        #region 推送人员数据
        /// <summary>
        ///  推送人员数据
        /// </summary>
        /// <returns></returns>
        public static string PushPersons(string type, string proCode)
        {
            try
            {
                string mess = string.Empty;
                string sucess = string.Empty;
                string code = string.Empty;
                string contenttype = "application/json;charset=unicode";
                var getData = (from x in Funs.DB.SitePerson_Person
                               join y in Funs.DB.Base_Project on x.ProjectId equals y.ProjectId
                            //   join z in Funs.DB.RealName_Project on y.ProjectCode equals z.ProCode
                               join u in Funs.DB.Base_Unit on x.UnitId equals u.UnitId
                               join v in Funs.DB.ProjectData_TeamGroup on x.TeamGroupId equals v.TeamGroupId
                               join w in Funs.DB.Base_WorkPost on x.WorkPostId equals w.WorkPostId
                               where x.IdentityCard != null && x.IdentityCard != ""
                                && y.ProjectCode == proCode
                                && v.TeamId.HasValue && x.HeadImage != null
                               && ((type == Const.BtnModify && !x.RealNameUpdateTime.HasValue) || (type != Const.BtnModify && !x.RealNameAddTime.HasValue))
                               && ((x.IdcardType == null || x.IdcardType == "SHENFEN_ZHENGJIAN") 
                               && (x.IdentityCard.Length == 15 || x.IdentityCard.Length == 18))
                               select new
                               {
                                   name = x.PersonName,
                                   idcardType = x.IdcardType ?? "SHENFEN_ZHENGJIAN",
                                   idcardNumber = x.IdentityCard,
                                   idcardStartDate = x.IdcardStartDate.HasValue ? string.Format("{0:yyyy-MM-dd}", x.IdcardStartDate) : null,
                                   idcardEndDate = x.IdcardEndDate.HasValue ? string.Format("{0:yyyy-MM-dd}", x.IdcardEndDate) : null,
                                   idcardForever = x.IdcardForever,
                                   politicsStatus = x.PoliticsStatus,
                                   eduLevel = x.EduLevel,
                                   maritalStatus = x.MaritalStatus,
                                   sex = (x.Sex == "2" ? "F" : "M"),
                                   idcardAddress = x.IdcardAddress,
                                   homeAddress = x.Address,
                                   birthday = x.Birthday.HasValue ? string.Format("{0:yyyy-MM-dd}", x.Birthday) : null,
                                   nation = x.Nation,
                                   countryCode = x.CountryCode,
                                   provinceCode = x.ProvinceCode,
                                   //positiveIdcardImage = db.AttachFile.First(t => (x.PersonId + "#1") == t.ToKeyId).ImageByte,
                                   //negativeIdcardImage = db.AttachFile.First(t => (x.PersonId + "#5") == t.ToKeyId).ImageByte,
                                   headImage = x.HeadImage,
                                   proCode = y.ProjectCode,
                                   teamId = v.TeamId,
                                   mobile = x.Telephone,
                                   teamLeaderFlag = (v.GroupLeaderId == x.PersonId ? "Y" : "N"),
                                   userType = ((w.PostType == "1" || w.PostType == "4") ? "LAB_USER_MANAGE" : "LAB_USER_BULIDER"),
                                   workType = w.WorkPostCode,
                                   isLeave = x.OutTime < DateTime.Now ? "Y" : "N",
                                   entryTime = x.InTime.HasValue ? string.Format("{0:yyyy-MM-dd}", x.InTime) : null,
                                   exitTime = x.OutTime.HasValue ? string.Format("{0:yyyy-MM-dd}", x.OutTime) : null,
                                   x.RealNameAddTime,
                                   x.RealNameUpdateTime,
                                   x.PersonId,
                               }).Take(200).ToList();
                if (getData.Count() > 0)
                {
                    string returndata = string.Empty;
                    Hashtable newToken = new Hashtable
                    {
                        { "token", getaccess_token(proCode) }
                    };
                    if (type == Const.BtnModify)
                    {
                        var updatelistObject = new
                        {
                            list = getData.Select(x => new { x.name, x.idcardType, x.idcardNumber, x.idcardStartDate, x.idcardEndDate, x.idcardForever, x.politicsStatus, x.eduLevel, x.maritalStatus, x.sex, x.idcardAddress, x.homeAddress, x.birthday, x.nation, x.countryCode, x.provinceCode, x.proCode, x.teamId, x.mobile, x.teamLeaderFlag, x.userType, x.workType, x.isLeave, x.entryTime, x.exitTime })
                        };
                        returndata = BLL.APIGetHttpService.OutsideHttp(Funs.RealNameApiUrl + "/foreignApi/accept/updatePersons", "POST", contenttype, newToken, JsonConvert.SerializeObject(updatelistObject));
                    }
                    else
                    {
                        var addlistObject = new
                        {
                            list = getData.Select(x => new { x.name, x.idcardType, x.idcardNumber, x.idcardStartDate, x.idcardEndDate, x.idcardForever, x.politicsStatus, x.eduLevel, x.maritalStatus, x.sex, x.idcardAddress, x.homeAddress, x.birthday, x.nation, x.countryCode, x.provinceCode, x.headImage, x.proCode, x.teamId, x.mobile, x.teamLeaderFlag, x.userType, x.workType, x.isLeave, x.entryTime, x.exitTime })
                        };
                        returndata = BLL.APIGetHttpService.OutsideHttp(Funs.RealNameApiUrl + "/foreignApi/accept/persons", "POST", contenttype, newToken, JsonConvert.SerializeObject(addlistObject));
                    }

                    if (!string.IsNullOrEmpty(returndata))
                    {
                        JObject obj = JObject.Parse(returndata);
                        mess = obj["message"].ToString();
                        code = obj["code"].ToString();
                        sucess = obj["success"].ToString();
                        if (obj["success"] != null && Convert.ToBoolean(obj["success"].ToString()))
                        {
                            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
                            {
                                foreach (var item in getData)
                                {
                                    var getPerson = db.SitePerson_Person.FirstOrDefault(x => x.PersonId == item.PersonId);
                                    if (getPerson != null)
                                    {
                                        if (!getPerson.RealNameAddTime.HasValue)
                                        {
                                            getPerson.RealNameAddTime = DateTime.Now;
                                        }
                                        getPerson.RealNameUpdateTime = DateTime.Now;
                                        db.SubmitChanges();
                                    }
                                }
                            }
                        }
                    }

                    InsertRealNamePushLog(null, proCode, "推送人员数据", sucess, code, mess);
                }
                else
                {
                    mess = "没有符合条件的数据！";
                }

                return mess;
            }
            catch (Exception ex)
            {
                ErrLogInfo.WriteLog(ex, "推送人员数据", "SynchroSetService.PushPersons");
                return "";
            }
        }
        #endregion

        #region 推送考勤数据
        /// <summary>
        ///  推送考勤数据
        /// </summary>
        /// <returns></returns>
        public static string PushAttendance(string proCode)
        {
            try
            {
                string mess = string.Empty;
                string sucess = string.Empty;
                string code = string.Empty;
                //string contenttype = "application/json;charset=unicode";
                string url = Funs.RealNameApiUrl + "/foreignApi/accept/attendance";
                var getData = (from x in Funs.DB.RealName_PersonInOutNow
                               join p in Funs.DB.SitePerson_Person on x.PersonId equals p.PersonId
                               join v in Funs.DB.ProjectData_TeamGroup on p.TeamGroupId equals v.TeamGroupId
                               join r in Funs.DB.RealName_CollTeam on v.TeamId equals r.TeamId
                               where x.IdcardNumber != null && x.IdcardType != null && x.ChangeTime.HasValue
                               && (proCode == null || x.ProCode == proCode)
                               && v.TeamId.HasValue && p.HeadImage != null && r.TeamId.HasValue
                               select new
                               {
                                   p.PersonId,
                                   proCode = x.ProCode,
                                   name = x.Name,
                                   idcardType = x.IdcardType,
                                   idcardNumber = x.IdcardNumber,
                                   checkType = x.CheckType,
                                   checkTime = string.Format("{0:yyyy-MM-dd HH:mm:ss}", x.ChangeTime),
                                   dierction = x.IsIn == true ? "JINCHANG_JINCHU" : "TUICHANG_JINCHU",
                                   checkWay = x.CheckWay,
                                   checkLocation = x.CheckLocation,
                                   longitude = x.Longitude,
                                   latitude = x.Latitude,
                                   x.RealNamePushTime,
                                   x.PersonInOutId,
                                   v.TeamGroupId,
                               }).Take(200).ToList();
                if (getData.Count() > 0)
                {
                    var listObject = new
                    {
                        list = getData.Select(x => new { x.proCode, x.name, x.idcardType, x.idcardNumber, x.checkType, x.checkTime, x.dierction, x.checkWay, x.checkLocation, x.longitude, x.latitude })
                    };
                    Hashtable newToken = new Hashtable
                    {
                        { "token", getaccess_token(proCode) }
                    };
                    
                    addTeam(getData.Select(x => x.TeamGroupId).Distinct().ToList(), newToken);
                    getTeam(proCode, newToken);
                    addPerson(getData.Select(x => x.PersonId).Distinct().ToList(), newToken);

                    var returndata = BLL.APIGetHttpService.OutsideHttp(url, "POST", null, newToken, JsonConvert.SerializeObject(listObject));
                    if (!string.IsNullOrEmpty(returndata))
                    {
                        JObject obj = JObject.Parse(returndata);
                        mess = obj["message"].ToString();
                        code = obj["code"].ToString();
                        sucess = obj["success"].ToString();
                        if (obj["success"] != null && Convert.ToBoolean(obj["success"].ToString()))
                        {
                            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
                            {
                                foreach (var item in getData)
                                {
                                    var getPersonInOutNow = db.RealName_PersonInOutNow.FirstOrDefault(x => x.PersonInOutId == item.PersonInOutId);
                                    if (getPersonInOutNow != null)
                                    {
                                        //getPersonInOutNow.RealNamePushTime = DateTime.Now;
                                        db.RealName_PersonInOutNow.DeleteOnSubmit(getPersonInOutNow);
                                        db.SubmitChanges();
                                    }
                                }
                            }
                        }
                    }
                    InsertRealNamePushLog(null, proCode, "推送考勤数据", sucess, code, mess);
                }
                else
                {
                    mess = "没有符合条件的数据！";
                }

                return mess;
            }
            catch (Exception ex)
            {
                ErrLogInfo.WriteLog(ex, "推送考勤数据", "SynchroSetService.PushAttendance");
                return "";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="proCode"></param>
        /// <param name="newToken"></param>
        public static void getTeam(string proCode, Hashtable newToken)
        {
            var getProject = new
            {
                 proCode
            };
            var returndata = BLL.APIGetHttpService.OutsideHttp(Funs.RealNameApiUrl + "/foreignApi/baseData/getCollTeam", "POST", null, newToken, JsonConvert.SerializeObject(getProject));
            if (!string.IsNullOrEmpty(returndata))
            {
                JObject obj = JObject.Parse(returndata);
                if (Convert.ToBoolean(obj["success"].ToString()))
                {
                    using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
                    {
                        JArray arr = JArray.Parse(obj["data"].ToString());
                        foreach (var item in arr)
                        {
                            long? teamId = Funs.GetNewlong(item["teamId"].ToString());
                            string teamName = item["teamName"].ToString();
                            string teamLeaderName = item["teamLeaderName"].ToString();
                            string teamLeaderMobile = item["teamLeaderMobile"].ToString();
                            string thirdTeamCode = item["thirdTeamCode"].ToString();
                            if (teamId.HasValue)
                            {
                                var getCollTeam = db.RealName_CollTeam.FirstOrDefault(x => x.ProCode == proCode && x.TeamId == teamId);
                                if (getCollTeam == null)
                                {
                                    Model.RealName_CollTeam newCollTeam = new Model.RealName_CollTeam
                                    {
                                        ID = SQLHelper.GetNewID(),
                                        TeamId = teamId,
                                        ProCode = proCode,
                                        TeamName = teamName,
                                        TeamLeaderName = teamLeaderName,
                                        TeamLeaderMobile = teamLeaderMobile
                                    };
                                    db.RealName_CollTeam.InsertOnSubmit(newCollTeam);
                                    db.SubmitChanges();
                                }

                                var getTeamGroup = db.ProjectData_TeamGroup.FirstOrDefault(x => x.TeamGroupId == thirdTeamCode && (x.TeamId != teamId || x.TeamId == null));
                                if (getTeamGroup != null)
                                {
                                    getTeamGroup.TeamId = teamId;
                                    getTeamGroup.RealNamePushTime = DateTime.Now;
                                    db.SubmitChanges();
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        ///   推送 施工队
        /// </summary>
        public static void addTeam(List<string> teamGroupIds, Hashtable newToken)
        {
            var getCollTeam = (from x in db.ProjectData_TeamGroup
                               join y in db.Base_Project on x.ProjectId equals y.ProjectId
                               join u in db.Base_Unit on x.UnitId equals u.UnitId
                               join s in db.SitePerson_Person on x.GroupLeaderId equals s.PersonId into jonPerson
                               from s in jonPerson.DefaultIfEmpty()
                               where teamGroupIds.Contains(x.TeamGroupId)
                               select new
                               {
                                   proCode = y.ProjectCode,
                                   collCropCode = u.CollCropCode,
                                   teamType = x.TeamTypeId,
                                   teamName = x.TeamGroupName,
                                   thirdTeamCode = x.TeamGroupId,
                                   entryTime = string.Format("{0:yyyy-MM-dd}", x.EntryTime),
                                   exitTime = string.Format("{0:yyyy-MM-dd}", x.ExitTime),
                                   teamLeaderName = s.PersonName,
                                   teamLeaderIdcardType = s.IdcardType,
                                   teamLeaderIdcardNumber = s.IdentityCard,
                                   teamLeaderMobile = s.Telephone,
                                   x.RealNamePushTime,
                                   x.TeamGroupId,
                               }).ToList();
            if (getCollTeam.Count() > 0)
            {
                var listObject = new
                {
                    list = getCollTeam.Select(x => new { x.proCode, x.collCropCode, x.teamType, x.teamName, x.thirdTeamCode, x.entryTime, x.exitTime, x.teamLeaderName, x.teamLeaderIdcardType, x.teamLeaderIdcardNumber, x.teamLeaderMobile })
                };
                BLL.APIGetHttpService.OutsideHttp(Funs.RealNameApiUrl + "/foreignApi/accept/collTeam", "POST", null, newToken, JsonConvert.SerializeObject(listObject));
            }
        }

        /// <summary>
        ///  推送人员
        /// </summary>
        /// <param name="personList"></param>
        public static void addPerson(List<string> personList, Hashtable newToken)
        {
            using (Model.SGGLDB db = new Model.SGGLDB(Funs.ConnString))
            {
                var getData = (from x in db.SitePerson_Person
                               join y in db.Base_Project on x.ProjectId equals y.ProjectId
                               join v in db.ProjectData_TeamGroup on x.TeamGroupId equals v.TeamGroupId
                               join w in db.Base_WorkPost on x.WorkPostId equals w.WorkPostId
                               where personList.Contains(x.PersonId)
                               select new
                               {
                                   name = x.PersonName,
                                   idcardType = x.IdcardType ?? "SHENFEN_ZHENGJIAN",
                                   idcardNumber = x.IdentityCard,
                                   idcardStartDate = x.IdcardStartDate.HasValue ? string.Format("{0:yyyy-MM-dd}", x.IdcardStartDate) : null,
                                   idcardEndDate = x.IdcardEndDate.HasValue ? string.Format("{0:yyyy-MM-dd}", x.IdcardEndDate) : null,
                                   idcardForever = x.IdcardForever,
                                   politicsStatus = x.PoliticsStatus,
                                   eduLevel = x.EduLevel,
                                   maritalStatus = x.MaritalStatus,
                                   sex = (x.Sex == "2" ? "F" : "M"),
                                   idcardAddress = x.IdcardAddress,
                                   homeAddress = x.Address,
                                   birthday = x.Birthday.HasValue ? string.Format("{0:yyyy-MM-dd}", x.Birthday) : null,
                                   nation = x.Nation,
                                   countryCode = x.CountryCode,
                                   provinceCode = x.ProvinceCode,
                                   //positiveIdcardImage = db.AttachFile.First(t => (x.PersonId + "#1") == t.ToKeyId).ImageByte,
                                   //negativeIdcardImage = db.AttachFile.First(t => (x.PersonId + "#5") == t.ToKeyId).ImageByte,
                                   headImage = x.HeadImage,
                                   proCode = y.ProjectCode,
                                   teamId = v.TeamId,
                                   mobile = x.Telephone,
                                   teamLeaderFlag = (v.GroupLeaderId == x.PersonId ? "Y" : "N"),
                                   userType = ((w.PostType == "1" || w.PostType == "4") ? "LAB_USER_MANAGE" : "LAB_USER_BULIDER"),
                                   workType = w.WorkPostCode,
                                   isLeave = x.OutTime < DateTime.Now ? "Y" : "N",
                                   entryTime = x.InTime.HasValue ? string.Format("{0:yyyy-MM-dd}", x.InTime) : null,
                                   exitTime = x.OutTime.HasValue ? string.Format("{0:yyyy-MM-dd}", x.OutTime) : null,
                                   x.RealNameAddTime,
                                   x.RealNameUpdateTime,
                                   x.PersonId,
                               }).ToList();
                if (getData.Count() > 0)
                {
                    var addlistObject = new
                    {
                        list = getData.Select(x => new { x.name, x.idcardType, x.idcardNumber, x.idcardStartDate, x.idcardEndDate, x.idcardForever, x.politicsStatus, x.eduLevel, x.maritalStatus, x.sex, x.idcardAddress, x.homeAddress, x.birthday, x.nation, x.countryCode, x.provinceCode, x.headImage, x.proCode, x.teamId, x.mobile, x.teamLeaderFlag, x.userType, x.workType, x.isLeave, x.entryTime, x.exitTime })
                    };

                    BLL.APIGetHttpService.OutsideHttp(Funs.RealNameApiUrl + "/foreignApi/accept/persons", "POST", null, newToken, JsonConvert.SerializeObject(addlistObject));
                    BLL.APIGetHttpService.OutsideHttp(Funs.RealNameApiUrl + "/foreignApi/accept/updatePersons", "POST", null, newToken, JsonConvert.SerializeObject(addlistObject));                    
                }
            }
        }
        #endregion
    }
}
