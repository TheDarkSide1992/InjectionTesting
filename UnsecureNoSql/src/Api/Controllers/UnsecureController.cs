using Infastructure;
using Infastructure.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Api.Controllers;

[ApiController]
public class UnsecureController : ControllerBase
{
    private readonly IMongoUserRepo _userRepo;

    public UnsecureController(MongoUserRepo userRepo)
    {
        _userRepo = userRepo;
    }
    
    
    [HttpGet]
    [Route("GetUser")]
    public async Task<IActionResult> GetUser([FromQuery] string name)
    {
        var result = await _userRepo.GetUserByName(name);
        
        return result != null ? Ok(result) : NotFound();
    }
    
    [HttpPut]
    [Route("Update")]
    public async Task<IActionResult> UpdateUser([FromBody] User user)
    {
        var updated = await _userRepo.UpdateUser(user);
        return updated != null ? Ok(updated) : NotFound();
    }

    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> CreateUser([FromBody] User user)
    {
        var response = await _userRepo.CreateUser(user);

        return Ok(response);
    }

    [HttpDelete] 
    [Route("{userId}")] 
    public async Task<IActionResult> DeleteUser([FromRoute] string UserId)
    {
        bool success = await _userRepo.DeleteUser(new UserId{Value = new ObjectId(UserId)});
        return success ? Ok() : Conflict();
    }
}