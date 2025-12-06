using Microsoft.AspNetCore.Mvc;
using Vehicle_Business;
namespace VehicleAPIsService.Controllers
{
    [Route("api/BodiesTest")]
    [ApiController]
    public class BodiesTestController : BaseControllerTest<clsBodyTest>
    {
    }
}
