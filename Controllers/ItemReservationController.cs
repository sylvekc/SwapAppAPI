using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwapApp.Models;
using SwapApp.Services;

namespace SwapApp.Controllers
{

    [Route("api/reservation")]
    [ApiController]
    [Authorize]

    public class ItemReservationController : ControllerBase
    {
        private readonly IItemReservationService _reservationService;

        public ItemReservationController(IItemReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet("myReservations")]
        public ActionResult<IEnumerable<GetItemDto>> GetAllUserItems([FromQuery] ItemQuery query)
        {
            var itemsDtos = _reservationService.GetAllItemsReservedByUser(query);
            return Ok(itemsDtos);
        }


        [HttpPut("item/{id}")]
        public ActionResult AddReservation([FromRoute] int id)
        {
            _reservationService.AddReservation(id);
            return Ok();
        }

    }
}
