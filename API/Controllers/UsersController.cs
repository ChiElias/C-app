using System.Security.Claims;
using API.DTOs;
using API.Extensions;
using API.Interfaces;
using AutoMapper;
using Company.ClassLibrary1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
#nullable disable

[AllowAnonymous]

public class UsersController : BaseApiController
{
    
    private readonly IUserRepository _userRepository;
    
    private readonly IMapper _mapper;
    private readonly IImageService _imageService;

    public UsersController(IImageService imageService,IUserRepository userRepository, IMapper mapper)
    {
        this._userRepository = userRepository;

        _mapper = mapper;

        _imageService = imageService;

    }
    private async Task<AppUser> _GetUser(){
        var username = User.GetUsername();
        if (username is null) return null;
        return await _userRepository.GetUserByUserNameAsync(username);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
    {
        return Ok(await _userRepository.GetMembersAsync());
    }


    [HttpGet("{id}")]
     public async Task<ActionResult<MemberDto>> GetUser(int id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        return _mapper.Map<MemberDto>(user);
    }


    [HttpGet("username/{username}")]
    public async Task<ActionResult<MemberDto>> GetUserByUserName(string username)
    {
        return await _userRepository.GetMemberByUsernameAsync(username);
    }
    [HttpPut]
    public async Task<ActionResult> UpdateUserProfile(MemberUpdateDto memberUpdateDto)
    {
        var appuser = await _GetUser();
        if (appuser is null) return NotFound();

        _mapper.Map(memberUpdateDto, appuser);
        if (await _userRepository.SaveAllAsync()) return NoContent();

        return BadRequest("Failed to update user profile!");
    }
    [HttpPost("add-image")]
    public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
    {
        var user = await _GetUser();
        if (user is null) return NotFound();

        var result = await _imageService.AddImageAsync(file);
        if (result.Error is not null) return BadRequest(result.Error.Message);

        var photo = new Photo
        {
            Url = result.SecureUrl.AbsoluteUri,
            PublicId = result.PublicId
        };
        if (user.Photos.Count == 0) photo.IsMain = true;

        user.Photos.Add(photo);
        if (await _userRepository.SaveAllAsync()) return _mapper.Map<PhotoDto>(photo);
        return BadRequest("Something has gone wrong!");
    }
}