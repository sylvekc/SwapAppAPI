using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SwapApp.Entities;

namespace SwapApp.Controllers
{
    [Route("api/item")]
    public class ItemController : ControllerBase
    {
        private readonly ItemDbContext _dbContext;

        public ItemController(ItemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public ActionResult<Item> Post([FromBody] Item item)
        {
            try
            {
                _dbContext.Item.AddRange(item);
                _dbContext.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        public ActionResult<IEnumerable<Item>> GetAll()
        {
            var items = _dbContext.Item.ToList();
            return Ok(items);
        }
        [HttpGet("{id}")]
        public ActionResult<Item> Get([FromRoute] int id)
        {
            var item = _dbContext.Item.FirstOrDefault(x => x.Id == id);

            if (item is null)
            {
                return NotFound("Nie znaleziono przedmiotu o podanym ID");
            }
            return Ok(item);
        }

    }
}
