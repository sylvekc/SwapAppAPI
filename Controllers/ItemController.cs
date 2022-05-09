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
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        { 
            _itemService = itemService;
        }
        
        [HttpDelete("{id}")]
        public ActionResult DeleteItem ([FromRoute] int id)
        {
           var isDeleted = _itemService.DeleteItem(id);
            if (isDeleted)
            {
                return NoContent();
            }
            return NotFound();
        }


        [HttpPost]
        public ActionResult AddItem ([FromBody] AddItemDto addItem)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = _itemService.AddItem(addItem);
          
                return Created($"/api/item/{id}", null);
        }


        [HttpGet]
        public ActionResult<IEnumerable<ItemDto>> GetAllItems()
        {
            var itemsDtos = _itemService.GetAllItems();
            return Ok(itemsDtos);
        }


        [HttpGet("{id}")]
        public ActionResult<ItemDto> GetItem([FromRoute] int id)
        {
            var item = _itemService.GetItemById(id);

            if (item is null)
            {
                return NotFound($"Nie znaleziono przedmiotu o ID = {id}");
            }
            return Ok(item);
        }

    }
}
