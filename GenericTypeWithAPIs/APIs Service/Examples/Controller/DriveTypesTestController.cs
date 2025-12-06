using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vehicle_Business;

namespace VehicleAPIsService.Controllers
{
    [Route("api/DriveTypesTest")]
    [ApiController]
    public class DriveTypesTestController : BaseControllerTest<clsDriveTypeTest>
    {
    }
}
