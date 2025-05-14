using Infratructue;
using Infratructue.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UnsecureController : ControllerBase
{
    private UnsecureRepository _UnsecureRepository;

    public UnsecureController(UnsecureRepository unsecureRepository)
    {
        _UnsecureRepository = unsecureRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _UnsecureRepository.GetUsers());
    } 
    
    [HttpGet]
    [Route("{name}")]
    public async Task<IActionResult> GetByName([FromRoute] string name)
    {
        Console.WriteLine(Environment.GetEnvironmentVariable("pgconn")!);
        return Ok(await _UnsecureRepository.GetUsersByName(name));
    } 
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UserModel user)
    {
        return Ok(await _UnsecureRepository.CreateUser(user));
    } 
    
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UserModel user)
    {
        return Ok(await _UnsecureRepository.CreateUser(user));
    } 
    
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        return Ok(await _UnsecureRepository.DeleteUserById(id));
    } 
    
    
   
}
