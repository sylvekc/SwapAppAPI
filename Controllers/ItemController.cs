using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SwapApp.Entities;
using SwapApp.Models;
using SwapApp.Services;

namespace SwapApp.Controllers
{
    [Route("api/item")]
    public class ItemController : ControllerBase
    {
        private readonly ItemDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        { 
            _itemService = itemService;
        }
        

        [HttpPost]
        public ActionResult AddItem ([FromBody] AddItemDto addItem)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = _itemService.Create(addItem);
          
                return Created($"/api/item/{id}", null);
        }


        [HttpGet]
        public ActionResult<IEnumerable<ItemDto>> GetAll()
        {
            var itemsDtos = _itemService.GetAll();
            return Ok(itemsDtos);
        }


        [HttpGet("{id}")]
        public ActionResult<ItemDto> Get([FromRoute] int id)
        {
            var item = _itemService.GetById(id);

            if (item is null)
            {
                return NotFound($"Nie znaleziono przedmiotu o ID = {id}");
            }
            return Ok(item);
        }

    }
}
