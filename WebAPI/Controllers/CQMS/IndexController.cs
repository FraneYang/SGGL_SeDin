using Microsoft.Ajax.Utilities;
using Model;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Http;

namespace Mvc.Controllers
{
    public class IndexController : ApiController
    {
        //
        // GET: /Index/

        [HttpGet]
        public ResponseData<string> CheckControlCount(string projectId)
        {
            ResponseData<string> res = new ResponseData<string>();

            res.successful = true;
            res.resultValue = "" + BLL.CheckControlService.GetListCount(projectId);
            return res; ;
        }
        [HttpGet]
        public ResponseData<string> JointCheckCount(string projectId)
        {
            ResponseData<string> res = new ResponseData<string>();

            res.successful = true;
            res.resultValue = "" + BLL.JointCheckService.GetListCount(projectId);
            return res; ;
        }
        [HttpGet]
        public ResponseData<string> WorkContactCount(string projectId)
        {
            ResponseData<string> res = new ResponseData<string>();

            res.successful = true;
            res.resultValue = "" + BLL.WorkContactService.getListCount(projectId);
            return res; ;
        }
        [HttpGet]
        public ResponseData<string> TechnicalContactCount(string projectId)
        {
            ResponseData<string> res = new ResponseData<string>();

            res.successful = true;
            res.resultValue = "" + BLL.TechnicalContactListService.getListCount(projectId);
            return res; ;
        }
        [HttpGet]
        public ResponseData<List<object>> todo(string projectId, string userId)
        {
            ResponseData<List<object>> res = new ResponseData<List<object>>();
            List<object> list = new List<object>();
            res.resultValue = list;
            SqlParameter[] val = new SqlParameter[]
             {
                    new SqlParameter("@UserId", userId),
                    new SqlParameter("@ProjectId", projectId)
             };

            var dt = BLL.SQLHelper.GetDataTableRunProc("SpAuditingManageByProjectId", val).AsEnumerable().ToArray();
            foreach (var item in dt)
            {
                if (!item.ItemArray[1].ToString().Contains("检试验设备及测量器具") && !item.ItemArray[1].ToString().Contains("工序资料验收"))
                {
                    ToDoModel toDoModel = new ToDoModel();
                    toDoModel.ID = item.ItemArray[0].ToString();
                    toDoModel.Title = item.ItemArray[1].ToString();
                    toDoModel.State = item.ItemArray[4].ToString();
                    
                    list.Add(toDoModel);
                }
            }
            res.successful = true;
            return res;

        }
        [HttpGet]
        public ResponseData<List<ToDoModel>> notice(string projectId, string userId)
        {
            ResponseData<List<ToDoModel>> res = new ResponseData<List<ToDoModel>>();
            List<ToDoModel> list = new List<ToDoModel>();

            SqlParameter[] val = new SqlParameter[]
                       {
                    new SqlParameter("@UserId", userId),
                    new SqlParameter("@ProjectId", projectId)
                       };

            var dt = BLL.SQLHelper.GetDataTableRunProc("SpNoticeByProjectId", val).AsEnumerable().ToArray();
            foreach (var item in dt)
            {
                if (!item.ItemArray[1].ToString().Contains("检试验设备及测量器具")&& !item.ItemArray[1].ToString().Contains("工序资料验收"))
                {
                    ToDoModel toDoModel = new ToDoModel();
                    toDoModel.ID = item.ItemArray[0].ToString();
                    toDoModel.Title = item.ItemArray[1].ToString();
                    list.Add(toDoModel);
                }
            }
            res.resultValue = list;
            res.successful = true;
            return res;

        }
        [HttpGet]
        public ResponseData<List<object>> warning(string projectId, string userId)
        {
            ResponseData<List<object>> res = new ResponseData<List<object>>();
            List<object> list = new List<object>();
            SqlParameter[] val = new SqlParameter[]
                      {
                    new SqlParameter("@ProjectId", projectId),
                      };

            var dt = BLL.SQLHelper.GetDataTableRunProc("SpEnableCueQualityByPrject", val).AsEnumerable().ToArray();
            foreach (var item in dt)
            {
                ToDoModel toDoModel = new ToDoModel();
                toDoModel.ID = item.ItemArray[0].ToString();
                toDoModel.Title = item.ItemArray[1].ToString();
                list.Add(toDoModel);
            }
            res.resultValue = list;
            res.successful = true;
            return res;

        }
    }
}
