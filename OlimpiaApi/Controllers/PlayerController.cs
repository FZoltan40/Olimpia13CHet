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

        [HttpGet]
        public ActionResult<Player> Get()
        {
            using (var context = new OlimipiaContext())
            {
                return Ok(context.Players.ToList());
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Player> GetById(Guid id)
        {
            using (var context = new OlimipiaContext())
            {
                var player = context.Players.FirstOrDefault(playerTable => playerTable.Id == id);

                if (player != null)
                {
                    return Ok(player);
                }

                return NotFound();
            }

        }

        [HttpPut("{id}")]
        public ActionResult<Player> Put(UpdatePlayerDto updatePlayerDto, Guid id)
        {
            using (var context = new OlimipiaContext())
            {
                var existingPlayer = context.Players.FirstOrDefault(player => player.Id == id);

                if (existingPlayer != null)
                {
                    existingPlayer.Name = updatePlayerDto.Name;
                    existingPlayer.Age = updatePlayerDto.Age;
                    existingPlayer.Height = updatePlayerDto.Height;
                    existingPlayer.Weight = updatePlayerDto.Weight;

                    context.Players.Update(existingPlayer);
                    context.SaveChanges();
                    return Ok(existingPlayer);
                }

                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            using (var context = new OlimipiaContext())
            {
                var player = context.Players.FirstOrDefault(player => player.Id == id);

                if (player != null)
                {
                    context.Players.Remove(player);
                    context.SaveChanges();
                    return Ok(new { messege = "Sikeres törlés." });
                }

                return NotFound();
            }
        }

    }
}
