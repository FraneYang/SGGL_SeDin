using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Model;
using BLL;

namespace WebAPI.Controllers
{
    public class PersonCheckController : ApiController
    {
        /// <summary>
        /// 根据用户Id获取需要打分的人员考核
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="index"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet]
        public ResponseData<List<Person_QuarterCheck>> Index(string userId, int index, int page)
        {
            ResponseData<List<Person_QuarterCheck>> res = new ResponseData<List<Person_QuarterCheck>>();

            res.successful = true;
            res.resultValue = BLL.Person_QuarterCheckService.GetListDataForApi(userId, index, page);
            return res;
        }

        /// <summary>
        /// 根据id获取人员考核
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseData<Person_QuarterCheck> GetPersonCheck(string id)
        {
            ResponseData<Person_QuarterCheck> res = new ResponseData<Person_QuarterCheck>();
            Person_QuarterCheck personCheck = BLL.Person_QuarterCheckService.GetPersonCheckForApi(id);
            res.successful = true;
            res.resultValue = BeanUtil.CopyOjbect<Person_QuarterCheck>(personCheck, true);
            return res;
        }

        /// <summary>
        /// 根据主表Id和用户id获取需要打分的明细项
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        public ResponseData<List<Person_QuarterCheckItem>> GetPersonCheckItem(string id, string userId)
        {
            ResponseData<List<Person_QuarterCheckItem>> res = new ResponseData<List<Person_QuarterCheckItem>>();

            res.successful = true;
            res.resultValue = BLL.Person_QuarterCheckItemService.GetListDataForApi(id, userId);
            return res;
        }

        /// <summary>
        /// 根据主表id和用户id获取对应的办理信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public ResponseData<Person_QuarterCheckApprove> GetCurrApproveByCode(string id, string userId)
        {
            ResponseData<Person_QuarterCheckApprove> res = new ResponseData<Person_QuarterCheckApprove>();

            res.successful = true;
            res.resultValue = BeanUtil.CopyOjbect<Person_QuarterCheckApprove>(BLL.Person_QuarterCheckApproveService.getCurrApproveForApi(id, userId), true);
            return res;
        }

        /// <summary>
        /// 根据id更新办理信息
        /// </summary>
        /// <param name="approveId"></param>
        /// <returns></returns>
        [HttpGet]
        public ResponseData<string> UpdateApprove(string approveId)
        {
            ResponseData<string> res = new ResponseData<string>();
            try
            {
                BLL.Person_QuarterCheckApproveService.UpdateApproveForApi(approveId);
                res.successful = true;

            }
            catch (Exception e)
            {
                res.resultHint = e.StackTrace;
                res.successful = false;
            }
            return res;
        }

        /// <summary>
        /// 根据拼接字符串更新打分项
        /// </summary>
        /// <param name="approveId"></param>
        /// <returns></returns>
        [HttpGet]
        public ResponseData<string> UpdateItems(string str)
        {
            ResponseData<string> res = new ResponseData<string>();
            try
            {
                if (!string.IsNullOrEmpty(str))
                {
                    string[] strs = str.Split(',');
                    foreach (var item in strs)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            string[] list = item.Split('$');
                            Model.Person_QuarterCheckItem cItem = BLL.Person_QuarterCheckItemService.GetCheckItemById(list[0]);
                            if (cItem != null)
                            {
                                if (!string.IsNullOrEmpty(list[1]))
                                {
                                    cItem.Grade = Convert.ToDecimal(list[1]);
                                }
                                else
                                {
                                    cItem.Grade = 0;
                                }
                            }
                            BLL.Person_QuarterCheckItemService.UpdateCheckItem(cItem);
                        }
                    }
                }
                res.successful = true;

            }
            catch (Exception e)
            {
                res.resultHint = e.StackTrace;
                res.successful = false;
            }
            return res;
        }
    }
}
