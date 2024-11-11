using Microsoft.AspNetCore.Mvc;
using OlimpiaApi.Models;


namespace OlimpiaApi.Controllers
{
    [Route("players")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        [HttpPost]
        public ActionResult<Player> Post(CreatePlayerDto createPlayer)
        {
            var player = new Player
            {
                Id = Guid.NewGuid(),
                Name = createPlayer.Name,
                Age = createPlayer.Age,
                Height = createPlayer.Height,
                Weight = createPlayer.Weight,
                CreatedTime = DateTime.Now
            };

            if (player != null)
            {
                using (var context = new OlimipiaContext())
                {
                    context.Players.Add(player);
                    context.SaveChanges();
                    return StatusCode(201, player);

                }
            }

            return BadRequest();
        }
    }
}
