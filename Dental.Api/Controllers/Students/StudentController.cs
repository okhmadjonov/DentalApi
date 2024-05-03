using Dental.Domain.Configurations;
using Dental.Domain.Dtos.Student;
using Dental.Domain.Models.Response;
using Dental.Domain.Models.StudentModels;
using Dental.Service.Extentions;
using Dental.Service.Interfaces.Students;
using Microsoft.AspNetCore.Mvc;

namespace Dental.Api.Controllers.Students;

[Route("api/[controller]")]
[ApiController]
public sealed class StudentController : ControllerBase
{
    private readonly IStudentRepository _studentRepository;

    public StudentController(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseModel<StudentModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> GetAll([FromQuery] PaginationParams @params)
      => ResponseHandler.ReturnResponseList(await _studentRepository.GetAll(@params));

    [HttpPost]
    //[Authorize]
    [ProducesResponseType(typeof(ResponseModel<StudentModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> CreateAsync([FromBody] StudentDto studentDto)
        => ResponseHandler.ReturnIActionResponse(await _studentRepository.CreateAsync(studentDto));


    [HttpDelete("{id}")]
    //[Authorize]
    [ProducesResponseType(typeof(ResponseModel<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> DeleteAsync([FromRoute] int id)
         => ResponseHandler.ReturnIActionResponse(await _studentRepository.DeleteAsync(id));

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ResponseModel<StudentModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> GetAsync([FromRoute] int id)
        => ResponseHandler.ReturnIActionResponse(await _studentRepository.GetAsync(u => u.Id == id));

    [HttpPut]
    //[Authorize]
    [ProducesResponseType(typeof(ResponseModel<StudentModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> UpdateAsync(int id, [FromBody] StudentDto studentDto)
        => ResponseHandler.ReturnIActionResponse(await _studentRepository.UpdateAsync(id, studentDto));


}
