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
    public class apiGoodsController : ApiController
    {
        private IGoodsService _goodsService;

        public apiGoodsController(IGoodsService goodsService)
        {
            this._goodsService = goodsService;
        }

        public easyuiGridDto<GoodsDto> GetByName(string page = null, string rows = null,string search=null, string name = null)
        {
            if (search == "do")
            {
                return this._goodsService.GetAll(page, rows, name);
            }
            else
            {
                return new easyuiGridDto<GoodsDto> { total = 0, rows = this._goodsService.GetAll().Where(g => g.Id == -1).OrderBy(g => g.Id).ToList() };
            }          
        }

        [ResponseType(typeof(GoodsDto))]
        public IHttpActionResult PostGoods(GoodsDto model)
        {
            OperationResult result = this._goodsService.Add(model);
            if (result.ResultType == OperationResultType.Success)
            {
                GoodsDto _goods = (GoodsDto)result.AppendData;
                return CreatedAtRoute("DefaultApi", new { id = _goods.Id }, _goods);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [ResponseType(typeof(GoodsDto))]
        public IHttpActionResult PuteGoods(GoodsDto model)
        {
            OperationResult result = this._goodsService.Edit(model);
            if (result.ResultType == OperationResultType.Success) {
                return CreatedAtRoute("defaultApi", new { id = model.Id }, model);
            }
            else { return BadRequest(ModelState); }

        }

        [ResponseType(typeof(GoodsDto))]
        public IHttpActionResult PuteDGoods(GoodsDto model)
        {
            OperationResult result = this._goodsService.Delete(model);
            if (result.ResultType == OperationResultType.Success)
            {
                return CreatedAtRoute("defaultApi", new { id = model.Id }, model);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
