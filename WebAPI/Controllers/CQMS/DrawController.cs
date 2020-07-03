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
    }
}
