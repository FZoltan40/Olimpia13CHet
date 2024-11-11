using Microsoft.AspNetCore.Mvc;
using OlimpiaApi.Models;

namespace OlimpiaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        [HttpPost]
        public ActionResult<Data> Post(CreateDataDto createDataDto)
        {
            var data = new Data
            {
                Id = Guid.NewGuid(),
                Country = createDataDto.Country,
                County = createDataDto.County,
                Description = createDataDto.Description,
                CreatedTime = DateTime.Now,
                UpdatedTime = DateTime.Now,
                PalyerId = createDataDto.PalyerId
            };

            if (data != null)
            {
                using (var context = new OlimipiaContext())
                {
                    context.Datas.Add(data);
                    context.SaveChanges();
                    return StatusCode(201, data);

                }
            }

            return BadRequest();
        }
    }
}
