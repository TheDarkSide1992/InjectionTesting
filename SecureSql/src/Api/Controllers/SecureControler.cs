using Infratructue;
using Infratructue.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SecureController : ControllerBase
{
    private ISecureRepository _secureRepository;

    public SecureController(ISecureRepository secureRepository)
    {
        _secureRepository = secureRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _secureRepository.GetUsers());
    } 
    
    [HttpGet]
    [Route("{name}")]
    public async Task<IActionResult> GetByName([FromRoute] string name)
    {
        Console.WriteLine(Environment.GetEnvironmentVariable("pgconn")!);
        return Ok(await _secureRepository.GetUsersByName(name));
    } 
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UserModel user)
    {
        return Ok(await _secureRepository.CreateUser(user));
    } 
    
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UserModel user)
    {
        return Ok(await _secureRepository.CreateUser(user));
    } 
    
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        return Ok(await _secureRepository.DeleteUserById(id));
    } 
    
    
   
}