﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet("playerData/{id}")]
        public ActionResult<Player> GetPlayerData(Guid id)
        {
            using (var contex = new OlimipiaContext())
            {
                var player = contex.Players.Include(x => x.Data).FirstOrDefault(player => player.Id == id);

                if (player != null)
                {
                    return Ok(player);
                }

                return NotFound();
            }
        }

        [HttpGet("lowPlayerData/{id}")]
        public ActionResult<LowPlayerData> GetlowPlayerData(Guid id)
        {
            using (var contex = new OlimipiaContext())
            {
                /*var player = contex.Players.Include(x => x.Data).FirstOrDefault(player => player.Id == id);
                var data = contex.Datas.FirstOrDefault(x => x.PalyerId == id);


                var lowData = new LowPlayerData
                {
                    Name = player.Name,
                    Country = data.Country,
                    County = data.County,
                    Decription = data.Description

                };*/

                var player = contex.Players.FirstOrDefault(x => x.Id == id);

                var data = contex.Datas.Select(x => new { player.Name, x.Country, x.County, x.Description, x.PalyerId }).Where(x => x.PalyerId == id).ToList(); ;



                if (data != null)
                {
                    return Ok(data);
                }

                return NotFound();
            }
        }
    }
}
