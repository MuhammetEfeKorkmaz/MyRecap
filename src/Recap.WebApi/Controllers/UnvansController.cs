using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recap.Business.Abstract;
using Recap.Entities.Concrete;

namespace Recap.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UnvansController : ControllerBase
    {
        IUnvanService _unvanService;

        public UnvansController(IUnvanService unvanService)
        {
            _unvanService = unvanService;
        }

        [HttpPost]
        public IActionResult Add(Unvan param)
        {
            var result = _unvanService.Add(param);
            return result.Success ? Ok(result) : BadRequest(result);
        }


        [HttpPost]
        public IActionResult Update(Unvan param)
        {
            var result = _unvanService.Update(param);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("{param}")]
        public IActionResult Delete(int param)
        {
            var result = _unvanService.Delete(param);
            return result.Success ? Ok(result) : BadRequest(result);
        }






        [HttpPost("{param}")]
        public IActionResult GetById(int param)
        {
            var result = _unvanService.GetById(param);
            return result.Success ? Ok(result) : BadRequest(result);
        }


        [HttpPost("{skip}/{take}/{IsActive}/{Id}/{Name}")]
        public IActionResult GetAll(int skip = 0, int take = 0, bool IsActive = false, int Id = 0, string Name = "Q")
        {
            var result = _unvanService.GetAll(skip,take,IsActive, Id, Name);
            return result.Success ? Ok(result) : BadRequest(result);
        }

    }
}
