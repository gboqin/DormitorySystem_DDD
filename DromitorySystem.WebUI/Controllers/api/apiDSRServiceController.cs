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
    public class apiDSRServiceController : ApiController
    {
        private IDormOptiontypeService _DOService;
        private IDormSettingService _DSService;
        private IDormSetRelationService _DSRService;
        private IDormitoryService _DormService;
        public apiDSRServiceController( IDormOptiontypeService DOService, IDormSettingService DSService, 
            IDormSetRelationService DSRService, IDormitoryService DormService)
        {
            _DOService = DOService;
            _DSService = DSService;
            _DSRService = DSRService;
            _DormService = DormService;
        }

        public IEnumerable<DormitoryDto> GetDormitoryTree(string id)
        {
            return this._DormService.GetRootData(id);
        }

        public easyuiGridDto<DormSetRelationDto> GetDormSetings(long? dormId, string page, string rows)
        {
            return this._DSRService.GetSetRelations(dormId??0, page,rows);
        }

        public IEnumerable<DormOptiontypeDto> GetOptiontype()
        {
            return this._DOService.GetAll();
        }

        public IEnumerable<DormSettingDto> GetSetting(long? typeId)
        {
            return this._DSService.GetAll().Where(d=>d.TypeId==typeId).ToList();
        }

        [ResponseType(typeof(DormSetRelationDto))]
        public IHttpActionResult PostSetRelation(DormSetRelationDto setRelation)
        {
            OperationResult result = null;
            if (setRelation.dsr_Cover == true)
            {
                var dormlist = this._DormService.GetDormitories(setRelation.dsr_DormId);
                result = this._DSRService.AddDormlistSetRelation(setRelation, dormlist);
            }
            else { result = this._DSRService.Add(setRelation); }
   
            if (result.ResultType == OperationResultType.Success)
            {
                DormSetRelationDto _setRelation = (DormSetRelationDto)result.AppendData;
                return CreatedAtRoute("DefaultApi", new { id = _setRelation.Id }, _setRelation);
            }
            else
            {
                ModelState.AddModelError("error", result.Message);
                return BadRequest(ModelState);
            }
        }

        [ResponseType(typeof(DormSetRelationDto))]
        public IHttpActionResult PuteSetRelation(DormSetRelationDto setRelation)
        {
            OperationResult result = this._DSRService.Update(setRelation);
            if (result.ResultType == OperationResultType.Success)
            {
                DormSetRelationDto _setRelation = (DormSetRelationDto)result.AppendData;
                return CreatedAtRoute("DefaultApi", new { id = _setRelation.Id }, _setRelation);
            }
            ModelState.AddModelError("error", result.Message);
            return BadRequest(ModelState);
        }

        [ResponseType(typeof(DormSetRelationDto))]
        public IHttpActionResult PuteDelSetRelation(DormSetRelationDto setRelation)
        {
            var dormlist = this._DormService.GetDormitories(setRelation.dsr_DormId);
            OperationResult result = this._DSRService.Delete(setRelation, dormlist);
            if (result.ResultType == OperationResultType.Success)
            {
                return CreatedAtRoute("DefaultApi", new { id = setRelation.Id }, setRelation);
            }
            ModelState.AddModelError("error", result.Message);
            return BadRequest(ModelState);
        }
    }
}
