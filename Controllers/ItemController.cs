using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwapApp.Entities;
using SwapApp.Models;
using SwapApp.Services;
using System.Security.Claims;
using SwapApp.Exceptions;

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
        public async Task<ActionResult> AddItem([FromForm] AddItemDto addItem, List<IFormFile> photos)
        {
            var id = _itemService.AddItem(addItem);
            await _itemService.UploadPhotos(photos,id);

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
        public ActionResult UpdateItem([FromForm] UpdateItemDto updateItem, [FromRoute] int id)
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
        public ActionResult DeleteItem([FromRoute] int id)
        {
            _itemService.DeleteItem(id);
            return NoContent();
        }

        //[HttpPost("upload")]
        //public async Task<IActionResult> Upload([FromForm] List<IFormFile> files)

        //{

        //    var result = new List<ItemPhoto>();
        //    foreach (var file in files)
        //    {
        //        if (file != null && file.Length > 0)
        //        {
        //            var rootPath = Directory.GetCurrentDirectory();
        //            var fileName = file.FileName;
        //            var fullPath = $"{rootPath}/ItemPhotos/{fileName}";
        //            using (var stream = new FileStream(fullPath, FileMode.Create))
        //            {
        //                await file.CopyToAsync(stream);
        //            }
        //        }
        //        else
        //        {
        //            throw new BadRequestException("Something went wrong");
        //        }

        //        result.Add(new ItemPhoto() { FileName = fileName });
        //    }

        //    return Ok();
        //}

    }
}
