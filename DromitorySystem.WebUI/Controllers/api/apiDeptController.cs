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
    public class apiDeptController : ApiController
    {
        private IDepartmentService _deptService;
        public apiDeptController(IDepartmentService deptService)
        {
            this._deptService = deptService;
        }
        public List<TreeDto> GetTree()
        {
            return _deptService.GetAllTree();
        }

        public DepartmentDto GetNoteById(int id)
        {
            return _deptService.GetDeptByKey(id);
        }

        [ResponseType(typeof(DepartmentDto))]
        public IHttpActionResult PostDept(DepartmentDto model)
        {
            OperationResult result = _deptService.Add(model);
            DepartmentDto _dept = (DepartmentDto)result.AppendData;
            if (result.ResultType == OperationResultType.Success)
            {
                return CreatedAtRoute("defaultApi", new { id = _dept.id }, _dept);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [ResponseType(typeof(DepartmentDto))]
        public IHttpActionResult PuteDept(DepartmentDto model)
        {
            OperationResult result = _deptService.Update(model);
            DepartmentDto _dept = (DepartmentDto)result.AppendData;
            if (result.ResultType == OperationResultType.Success)
            {
                return CreatedAtRoute("defaultApi", new { id = _dept.id }, _dept);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [ResponseType(typeof(TreeDto))]
        public IHttpActionResult PuteTree(DepartmentDto model)
        {

            OperationResult result = _deptService.UpDeletedate(model.id);
            if (result.ResultType == OperationResultType.Success)
            {
                return CreatedAtRoute("defaultApi", new { id = model.id }, model);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [ResponseType(typeof(TreeDto))]
        public IHttpActionResult PuteDTree(TreeDto model)
        {
            OperationResult result = _deptService.UpDltdate(model);
            if (result.ResultType == OperationResultType.Success)
            {
                return CreatedAtRoute("defaultApi", new { id = model.id }, model);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
