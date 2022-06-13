using Microsoft.AspNetCore.Mvc;
using Recap.Business.Abstract;
using Recap.Core.Entities.Concrete;

namespace Recap.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUserService _systemUserService;

        public UsersController(IUserService systemUserService)
        {
            _systemUserService = systemUserService;
        }



        // [CancellationTokenAspect]= Toplu işlemlerde  , işlem uun sürerse ve client vazgeçerse kullanılır.
        [HttpPost]
        public IActionResult Add(User param)
        {
            var result = _systemUserService.Add(param);
            return result.Success ? Ok(result) : BadRequest(result);
        }


        [HttpPost]
        public IActionResult Update(User param)
        {
            var result = _systemUserService.Update(param);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("{param}")]
        public IActionResult Delete(int param)
        {
            var result = _systemUserService.Delete(param);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("{UserId}/{param}")]
        public IActionResult PasswordChanged(int UserId, string param)
        {
            var result = _systemUserService.PasswordChanged(UserId, param);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("{param}")]
        public IActionResult PasswordSenderEmail(string param)
        {
            var result = _systemUserService.PasswordSenderEmail(param);
            return result.Success ? Ok(result) : BadRequest(result);
        }





        [HttpPost("{param}")]
        public IActionResult GetById(int param)
        {
            var result = _systemUserService.GetById(param);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("{skip}/{take}/{IsActive}/{Id}/{Isim}/{Soyisim}/{Email}/{SicilNo}/{Pozisyon}/{DahiliNo}/{DepartmanId}/{UnvanId}")]
        public IActionResult GetAll(int skip = 0, int take = 0, bool IsActive = false, int Id = 0, string Isim = "Q", string Soyisim = "Q", string Email = "Q", string SicilNo = "Q",
            string Pozisyon = "Q", string DahiliNo = "Q", int DepartmanId = 0, int UnvanId = 0)
        {
            var result = _systemUserService.GetAll(skip,take,IsActive, Id, Isim, Soyisim, Email, SicilNo, Pozisyon, DahiliNo, DepartmanId, UnvanId);
            return result.Success ? Ok(result) : BadRequest(result);
        }

    }
}
