using Microsoft.AspNetCore.Mvc;
using Recap.Business.Abstract;
using Recap.Entities.Concrete;

namespace Recap.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DepartmansController : ControllerBase
    {
        IDepartmanService _departmanService;

        public DepartmansController(IDepartmanService departmanService)
        {
            _departmanService = departmanService;
        }

        [HttpPost]
        public IActionResult Add(Departman param)
        {
            var result = _departmanService.Add(param);
            return result.Success ? Ok(result) : BadRequest(result);
        }


        [HttpPost]
        public IActionResult Update(Departman param)
        {
            var result = _departmanService.Update(param);
            return result.Success ? Ok(result) : BadRequest(result);
        }


        [HttpPost("{param}")]
        public IActionResult Delete(int param)
        {
            var result = _departmanService.Delete(param);
            return result.Success ? Ok(result) : BadRequest(result);
        }






        [HttpPost("{param}")]
        public IActionResult GetById(int param)
        {
            var result = _departmanService.GetById(param);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("{skip}/{take}/{IsActive}/{Id}/{Name}/{RootPath}")]
        public IActionResult GetAll(int skip = 0, int take = 0, bool IsActive = false, int Id = 0, string Name = "Q", string RootPath = "Q")
        {
            //bool temp = IsActive==0 ? false : true;
            var result = _departmanService.GetAll(skip, take, IsActive, Id, Name, RootPath);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
