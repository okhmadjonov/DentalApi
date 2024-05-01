using Dental.Api.FluentValidation;
using Dental.Domain.Dtos.User;
using Dental.Domain.Models.Response;
using Dental.Domain.Models.UserModels;
using Dental.Service.Extentions;
using Dental.Service.Interfaces.Users;
using Microsoft.AspNetCore.Mvc;

namespace Dental.Api.Controllers.Users;

[Route("customapi/[controller]")]
[ApiController]
public sealed class AuthController : ControllerBase
{
    protected readonly IAuthRepository _authRepository;

    public AuthController(IAuthRepository authRepository) => _authRepository = authRepository;

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        var validator = new LoginDtoValidator();
        var validationResult = validator.Validate(loginDto);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(error => error.ErrorMessage));
        }
        //return ResponseHandler.ReturnIActionResponse($"token: {await _authRepository.Login(loginDto)}");
        return ResponseHandler.ReturnIActionResponse(await _authRepository.Login(loginDto));

    }

    [HttpPost("register")]
    [ProducesResponseType(typeof(ResponseModel<UserModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<>), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> CreateAsync(UserForCreationDto user)
    {
        var validator = new RegisterValidator();
        var validationResult = validator.Validate(user);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(error => error.ErrorMessage));
        }
        return ResponseHandler.ReturnIActionResponse(await _authRepository.Registration(user));

    }
}
