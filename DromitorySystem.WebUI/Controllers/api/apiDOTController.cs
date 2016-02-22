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
    public class apiDOTController : ApiController
    {
        private IDormOptiontypeService _dotService;
        public apiDOTController(IDormOptiontypeService dotService)
        {
            this._dotService = dotService;
        }

        public easyuiGridDto<DormOptiontypeDto> GetByName(string page = null, string rows = null, string search = null, string name = null)
        {
            if (search == "do")
            {
                return _dotService.GetAll(page, rows, name);
            }
            else
            {
                return new easyuiGridDto<DormOptiontypeDto> { total = 0, rows = this._dotService.GetAll().Where(t => t.Id == -1).ToList() };
            }
        }

        [ResponseType(typeof(DormOptiontypeDto))] 
        public IHttpActionResult PostAdd(DormOptiontypeDto model)
        {
            OperationResult result = this._dotService.Add(model);
            if (result.ResultType == OperationResultType.Success)
            {
                DormOptiontypeDto data = (DormOptiontypeDto)result.AppendData;
                return CreatedAtRoute("DefaultApi", new { id = data.Id }, data);
            }
            return BadRequest(ModelState);
        }

        [ResponseType(typeof(DormOptiontypeDto))]
        public IHttpActionResult PuteEdit(DormOptiontypeDto model)
        {
            OperationResult result = this._dotService.Edit(model);
            if (result.ResultType == OperationResultType.Success)
            {
                return CreatedAtRoute("DefaultApi", new { id = model.Id }, model);
            }
            return BadRequest(ModelState);
        }

        [ResponseType(typeof(DormOptiontypeDto))]
        public IHttpActionResult PuteDelete(DormOptiontypeDto model)
        {
            OperationResult result = this._dotService.Delete(model);
            if (result.ResultType == OperationResultType.Success)
            {
                return CreatedAtRoute("DefaultApi", new { id = model.Id }, model);
            }
            return BadRequest(ModelState);
        }

    }
}
