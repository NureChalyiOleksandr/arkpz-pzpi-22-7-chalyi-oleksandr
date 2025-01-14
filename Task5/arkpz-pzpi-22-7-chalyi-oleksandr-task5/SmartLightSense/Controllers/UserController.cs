using Microsoft.AspNetCore.Mvc;
using SmartLightSense.Interfaces;
using SmartLightSense.Models;
using SmartLightSense.Dtos;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;

namespace SmartLightSense.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserController(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("search")]
    public async Task<ActionResult<List<UserDto>>> SearchUsers([FromQuery] string searchTerm)
    {
        if (string.IsNullOrEmpty(searchTerm))
        {
            return BadRequest(new { Message = "Search term cannot be empty" });
        }

        var users = await _userRepository.SearchAsync(searchTerm);

        if (users == null || !users.Any())
        {
            return NotFound(new { Message = "No users found matching the search term" });
        }

        var userDtos = _mapper.Map<List<UserDto>>(users);
        return Ok(userDtos);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<ActionResult<List<UserDto>>> GetAll()
    {
        var users = await _userRepository.GetAllAsync();
        return Ok(users);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetById(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        var userDto = _mapper.Map<UserDto>(user);
        return Ok(userDto);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<ActionResult<UserDto>> Update(int id, [FromBody] UserUpdateDto updateUserDto)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        if (!string.IsNullOrEmpty(updateUserDto.UserName))
        {
            user.UserName = updateUserDto.UserName;
        }
        if (!string.IsNullOrEmpty(updateUserDto.Password))
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(updateUserDto.Password);
        }
        if (!string.IsNullOrEmpty(updateUserDto.Role))
        {
            user.Role = updateUserDto.Role;
        }
        if (!string.IsNullOrEmpty(updateUserDto.Email))
        {
            user.Email = updateUserDto.Email;
        }
        if (!string.IsNullOrEmpty(updateUserDto.PhoneNumber))
        {
            user.PhoneNumber = updateUserDto.PhoneNumber;
        }

        var updatedUser = await _userRepository.UpdateAsync(user);
 
        return Ok(updatedUser);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var success = await _userRepository.DeleteAsync(id);
        if (!success)
        {
            return NotFound();
        }
        return NoContent();
    }
}
