using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwapApp.Entities;
using SwapApp.Models;
using SwapApp.Services;
using System.Security.Claims;

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

        [HttpPost]
        public ActionResult AddItem([FromBody] AddItemDto addItem)
        {
            var id = _itemService.AddItem(addItem);

            return Created($"/api/item/{id}", null);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IEnumerable<GetItemDto>> GetAllItems([FromQuery] ItemQuery query)
        {
            var itemsDtos = _itemService.GetAllItems(query);
            return Ok(itemsDtos);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<GetItemDto> GetItem([FromRoute] int id)
        {
            var item = _itemService.GetItemById(id);

            return Ok(item);
        }

        [HttpGet("user/{id}")]
        [AllowAnonymous]
        public ActionResult<IEnumerable<GetItemDto>> GetAllUserItems([FromQuery] ItemQuery query, [FromRoute] int id)
        {
            var itemsDtos = _itemService.GetAllUserItems(query, id);
            return Ok(itemsDtos);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateItem ([FromBody] UpdateItemDto updateItem, [FromRoute] int id)
        {
            _itemService.UpdateItem(updateItem, id);

            return Ok();
        }

        [HttpPut("extendValidity/{id}")]
        public ActionResult ExtendItemValidity([FromRoute] int id)
        {
            _itemService.ExtendValidity(id);
            return Ok();
        }

        [HttpPut("changeVisibility/{id}")]
        public ActionResult ChangeVisibility([FromRoute] int id)
        {
            _itemService.ChangeVisibility(id);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteItem ([FromRoute] int id)
        {
           _itemService.DeleteItem(id);
          return NoContent();
        }
    }
}
