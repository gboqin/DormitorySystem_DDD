using DormitorySystem.Application;
using DormitorySystem.Application.viewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace DromitorySystem.WebUI.Controllers.api
{
    public class apiDormitoryController : ApiController
    {
        private IDormitoryService _dormService;
        public apiDormitoryController(IDormitoryService dormService)
        {
            this._dormService = dormService;
        }

        public IEnumerable<DormitoryDto> GetList(string id)
        {
            return this._dormService.GetRootData(id);
        }

        [ResponseType(typeof(BuildDto))]
        public IHttpActionResult PostAdd(BuildDto model)
        {
            if (model == null) { return BadRequest(ModelState); }
            OperationResult result = this._dormService.Add(model);
            if (result.ResultType == OperationResultType.Success)
            {
                DormitoryDto dorm = (DormitoryDto)result.AppendData;
                return CreatedAtRoute("DefaultApi", new { id = dorm.id }, dorm);
            }
            return BadRequest(ModelState);
        }

        [ResponseType(typeof(DormitoryDto))]
        public IHttpActionResult PuteDelete(DormitoryDto model)
        {
            if (model == null) { return BadRequest(ModelState); }
            OperationResult result = this._dormService.Delete(model);
            if (result.ResultType == OperationResultType.Success)
            {
                return CreatedAtRoute("DefaultApi", new { id = model.id }, model);
            }
            return BadRequest(ModelState);
        }
    }
}
