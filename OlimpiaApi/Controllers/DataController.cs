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

        [HttpGet]
        public ActionResult<Data> Get()
        {
            using (var context = new OlimipiaContext())
            {
                return Ok(context.Datas.ToList());
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Data> GetById(Guid id)
        {
            using (var context = new OlimipiaContext())
            {
                var data = context.Datas.FirstOrDefault(dataTable => dataTable.Id == id);

                if (data != null)
                {
                    return Ok(data);
                }

                return NotFound();
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Data> Put(UpdateDataDto updateDataDto, Guid id)
        {
            using (var context = new OlimipiaContext())
            {
                var exisitingData = context.Datas.FirstOrDefault(data => data.Id == id);

                if (exisitingData != null)
                {
                    exisitingData.Country = updateDataDto.Country;
                    exisitingData.County = updateDataDto.County;
                    exisitingData.Description = updateDataDto.Description;
                    exisitingData.UpdatedTime = DateTime.Now;

                    context.Datas.Update(exisitingData);
                    context.SaveChanges();

                    return Ok(exisitingData);
                }

                return NotFound();
            }
        }
    }
}
