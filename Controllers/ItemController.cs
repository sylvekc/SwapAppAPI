using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwapApp.Entities;
using SwapApp.Models;
using SwapApp.Services;

namespace SwapApp.Controllers
{
    [Route("api/item")]
    [ApiController]
    [Authorize]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        { 
            _itemService = itemService;
        }

        [HttpPut("{id}")]
        public ActionResult UpdateItem ([FromBody] UpdateItemDto updateItem, [FromRoute] int id)
        {
            _itemService.UpdateItem(updateItem, id);

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteItem ([FromRoute] int id)
        {
           _itemService.DeleteItem(id);
          return NoContent();
        }

        [HttpPost]
        public ActionResult AddItem ([FromBody] AddItemDto addItem)
        {
            var id = _itemService.AddItem(addItem);
          
                return Created($"/api/item/{id}", null);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IEnumerable<GetItemDto>> GetAllItems()
        {
            var itemsDtos = _itemService.GetAllItems();
            return Ok(itemsDtos);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<GetItemDto> GetItem([FromRoute] int id)
        {
            var item = _itemService.GetItemById(id);

            return Ok(item);
        }

    }
}
