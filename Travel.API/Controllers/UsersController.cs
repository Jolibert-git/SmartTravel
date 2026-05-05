using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Travel.Application.Contracts;
using Travel.Application.DTOs;
using Travel.Application.Request;
using Travel.Application.Responses;

namespace Travel.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>Retorna listado paginado de usuarios.</summary>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetPaged(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken cancellationToken = default)
        {
            var result = await _userService.GetPagedAsync(page, pageSize, cancellationToken);
            return Ok(result);
        }

        /// <summary>Retorna un usuario por ID.</summary>
        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id, CancellationToken cancellationToken)
        {
            var result = await _userService.GetByIdAsync(id, cancellationToken);
            return Ok(ApiResponse<UserResponseDto>.Ok(result));
        }

        /// <summary>Crea un nuevo usuario.</summary>
        [HttpPost]
        [AllowAnonymous]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(
            [FromBody] CreateUserRequest request,
            CancellationToken cancellationToken)
        {
            var result = await _userService.CreateAsync(request, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = result.Id },
                ApiResponse<UserResponseDto>.Created(result));
        }

        /// <summary>Actualiza datos básicos de un usuario.</summary>
        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(
            long id,
            [FromBody] UpdateUserRequest request,
            CancellationToken cancellationToken)
        {
            var result = await _userService.UpdateAsync(id, request, cancellationToken);
            return Ok(ApiResponse<UserResponseDto>.Ok(result, "Usuario actualizado correctamente."));
        }

        /// <summary>Cambia la contraseña del usuario.</summary>
        [HttpPatch("{id:long}/change-password")]
        public async Task<IActionResult> ChangePassword(
            long id,
            [FromBody] ChangePasswordRequest request,
            CancellationToken cancellationToken)
        {
            await _userService.ChangePasswordAsync(id, request, cancellationToken);
            return Ok(ApiResponse<object>.NoContent("Contraseña actualizada correctamente."));
        }

        /// <summary>Elimina un usuario.</summary>
        [HttpDelete("{id:long}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
        {
            await _userService.DeleteAsync(id, cancellationToken);
            return Ok(ApiResponse<object>.NoContent("Usuario eliminado correctamente."));
        }
    }
}
