using System.Collections.Generic;
using System.Web.Http;
using Model;

namespace Mvc.Controllers
{
    public class DrawController : ApiController
    {
        //
        // GET: /Draw/
        [HttpGet]
        public ResponseData<List<Check_Draw>> Index(string projectId, int index, int page,string name=null)
        {
            ResponseData<List<Check_Draw>> res = new ResponseData<List<Check_Draw>>();
            if (name == null)
                name = "";
            res.successful = true;
            res.resultValue = BLL.DrawService.GetDrawByProjectIdForApi(name,projectId, index, page); ;
            return res;
        }

        //
        // GET: /Draw/
        [HttpGet]
        public ResponseData<Check_Draw> GetDrawByDrawId(string id)
        {
            ResponseData<Check_Draw> res = new ResponseData<Check_Draw>();
            Check_Draw cd  =  BLL.DrawService.GetDrawByDrawId(id );
            
            res.successful = true;
            res.resultValue = BeanUtil.CopyOjbect<Check_Draw>(cd, true); 
            return res;
        }
        /// <summary>
        /// 根据code获取详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseData<Check_Draw> GetDraw(string id)
        {
            ResponseData<Check_Draw> res = new ResponseData<Check_Draw>();
            Check_Draw checkControl = BLL.DrawService.GetDrawForApi(id);
            res.successful = true;
            res.resultValue = BeanUtil.CopyOjbect<Check_Draw>(checkControl, true);
            res.resultValue.Edition = checkControl.Edition;
            return res;
        }
        //
        // GET: /Draw/
        [HttpGet]
        public ResponseData<Check_Draw> responseData(string id)
        {
            ResponseData<Check_Draw> res = new ResponseData<Check_Draw>();
            Check_Draw cd = BLL.DrawService.GetDrawByDrawId(id);

            res.successful = true;
            res.resultValue = BeanUtil.CopyOjbect<Check_Draw>(cd, true);
            return res;
        }

        [HttpGet]
        public ResponseData<string> see(string dataId, string userId)
        {
            ResponseData<string> res = new ResponseData<string>();
            res.successful = true;
            BLL.DrawApproveService.See(dataId, userId);
            return res;
        }
    }
}
