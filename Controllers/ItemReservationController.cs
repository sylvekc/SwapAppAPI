using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwapApp.Services;

namespace SwapApp.Controllers
{

    [Route("api/reservation/item")]
    [ApiController]
    [Authorize]

    public class ItemReservationController : ControllerBase
    {
        private readonly IItemReservationService _reservationService;

        public ItemReservationController(IItemReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpPut("{id}")]
        public ActionResult AddReservation([FromRoute] int id)
        {
            _reservationService.AddReservation(id);
            return Ok();
        }

    }
}
