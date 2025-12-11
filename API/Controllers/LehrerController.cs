using API.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs.Teacher;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LehrerController : ControllerBase
{
    private readonly ILehrerService _lehrerService;
    public LehrerController(ILehrerService lehrerService)
    {
        _lehrerService = lehrerService;
    }

    [HttpGet]
    public async Task<ActionResult<List<TeacherDTO>?>> GetAllTeachers()
    {
        List<TeacherDTO>? result = await _lehrerService.GetAllTeachers();

        if(result == null)
        {
            return BadRequest("Something went wrong while getting shi");
        }

        return Ok(result);
    }
}
