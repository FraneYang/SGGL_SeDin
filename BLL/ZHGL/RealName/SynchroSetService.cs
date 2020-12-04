using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
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
        public static Model.RealName_SynchroSet GetSynchroSetByUnitId(string unitId)
        {
            return Funs.DB.RealName_SynchroSet.FirstOrDefault(e => e.UnitId == unitId);
        }

        #region 同步设置 数据维护
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SynchroSet"></param>
        public static void SaveSynchroSet(Model.RealName_SynchroSet SynchroSet)
        {
            var getSynchroSet = GetSynchroSetByUnitId(SynchroSet.UnitId);
            if (getSynchroSet != null)
            {
                getSynchroSet.ClientId = SynchroSet.ClientId;
                getSynchroSet.UserName = SynchroSet.UserName;
                getSynchroSet.Password = SynchroSet.Password;
                getSynchroSet.Timestamp = SynchroSet.Timestamp;
                getSynchroSet.Token = SynchroSet.Token;
                getSynchroSet.Tokenendtime = SynchroSet.Tokenendtime;
                getSynchroSet.Intervaltime = SynchroSet.Intervaltime;
                Funs.DB.SubmitChanges();
            }
            else
            {
                SynchroSet.SynchroSetId = SQLHelper.GetNewID();
                Funs.DB.RealName_SynchroSet.InsertOnSubmit(SynchroSet);
                Funs.DB.SubmitChanges();
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
        public static string getaccess_token()
        {
            string access_token = string.Empty;
            var getToken = GetSynchroSetByUnitId(Const.UnitId_SEDIN);
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
            string access_token = string.Empty;
            string clientId = SynchroSet.ClientId;
            string userName = SynchroSet.UserName;
            string password = Funs.EncryptionPassword(SynchroSet.Password);
            string timestamp = string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now);
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
            string url = SynchroSet.ApiUrl + "/foreignApi/auth/accessToken";
            var returndata = BLL.APIGetHttpService.OutsideHttp(url, "POST", contenttype, null, JsonConvert.SerializeObject(getToken));
            if (!string.IsNullOrEmpty(returndata))
            {
                JObject obj = JObject.Parse(returndata);
                if (obj["success"] != null && Convert.ToBoolean(obj["success"].ToString()))
                {
                    access_token = obj["data"].ToString();
                    SynchroSet.Token = access_token;
                    SynchroSet.Tokenendtime = DateTime.Now.AddHours(24);
                    SaveSynchroSet(SynchroSet);
                }
            }
            return access_token;
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string getBasicData(string type)
        {
            Model.SGGLDB db = Funs.DB;
            string mess = "";
            string contenttype = "application/json;charset=unicode";
            var getSynchroSet = GetSynchroSetByUnitId(Const.UnitId_SEDIN);
            if (getSynchroSet != null)
            {
                string url = getSynchroSet.ApiUrl + "/foreignApi/baseData/getDict";
                var dictTypeCode = new
                {
                    dictTypeCode = type
                };
                Hashtable newToken = new Hashtable
                {
                    { "token", getaccess_token() }
                };
                var returndata = BLL.APIGetHttpService.OutsideHttp(url, "POST", contenttype, newToken, JsonConvert.SerializeObject(dictTypeCode));
                if (!string.IsNullOrEmpty(returndata))
                {
                    JObject obj = JObject.Parse(returndata);
                    mess = obj["message"].ToString();
                    if (obj["success"] != null && Convert.ToBoolean(obj["success"].ToString()))
                    {
                        JArray arr = JArray.Parse(obj["data"].ToString());
                        foreach (var item in arr)
                        {
                            string code = item["dictCode"].ToString();
                            string name = item["dictName"].ToString();
                            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(code))
                            {
                                var getBasicData = db.RealName_BasicData.FirstOrDefault(x => x.DictCode == code && x.DictTypeCode == type);
                                if (getBasicData != null)
                                {
                                    getBasicData.DictName = name;
                                    db.SubmitChanges();
                                }
                                else
                                {
                                    Model.RealName_BasicData newBasicData = new Model.RealName_BasicData();
                                    newBasicData.BasicDataId = SQLHelper.GetNewID();
                                    newBasicData.DictTypeCode = type;
                                    newBasicData.DictCode = code;
                                    newBasicData.DictName = name;
                                    db.RealName_BasicData.InsertOnSubmit(newBasicData);
                                    db.SubmitChanges();
                                }
                            }
                        }
                    }
                }
            }
            return mess;
        }
    }
}
