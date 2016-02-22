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
    public class apiDormSettingController : ApiController
    {
        private IDormSettingService _setService;
        private IDormOptiontypeService _dotService;
        public apiDormSettingController(IDormSettingService setService,IDormOptiontypeService dotService)
        {
            this._setService = setService;
            this._dotService = dotService;
        }

        public IEnumerable<DormOptiontypeDto> GetOptiontypes()
        {
            return _dotService.GetAll().ToList();
        }

        public easyuiGridDto<DormSettingDto> GetList(string page = null,string rows=null,string search="no",string typeid=null,string content=null)
        {
            if (search == "do")
            {
                return this._setService.GetList(page, rows, content, typeid);
            }
            else
            {
                return new easyuiGridDto<DormSettingDto> { total = 0, rows = this._setService.GetAll().Where(ds => ds.Id == -1).ToList() };
            }
        }

        [ResponseType(typeof(DormSettingDto))]
        public IHttpActionResult PostAdd(DormSettingDto model)
        {
            OperationResult result = _setService.Add(model);
            if (result.ResultType == OperationResultType.Success)
            {
                DormSettingDto dsDto = (DormSettingDto)result.AppendData;
                return CreatedAtRoute("DefaultApi", new { id = dsDto.Id }, dsDto);
            }
            return BadRequest(ModelState);
        } 

        [ResponseType(typeof(DormSettingDto))]
        public IHttpActionResult PuteEdit(DormSettingDto model)
        {
            OperationResult result = this._setService.Edit(model);
            if (result.ResultType == OperationResultType.Success)
            {
                return CreatedAtRoute("DefaultApi", new { id=model.Id},model);
            }
            return BadRequest(ModelState);
        }

        [ResponseType(typeof(DormSettingDto))]
        public IHttpActionResult PuteDelete(DormSettingDto model)
        {
            OperationResult result = this._setService.Delete(model);
            if (result.ResultType == OperationResultType.Success)
            {
                return CreatedAtRoute("DefaultApi", new { id = model.Id }, model);
            }
            return BadRequest(ModelState);
        }
    }
}
