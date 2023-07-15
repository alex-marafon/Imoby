using AutoMapper;
using Imoby.Domain.Interface.Service;
using Imoby.Entities.Entitie.Models;
using Microsoft.AspNetCore.Mvc;

namespace Imoby.Api.Controllers.Private.Cadastro;
[Route("api/[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IServiceUsuario _serviceUsuario;

    public UsuarioController(IServiceUsuario serviceUsuario, IMapper mapper)
    {
        _serviceUsuario = serviceUsuario;
        _mapper = mapper;
    }


    [HttpGet]
    public IActionResult Get()
    {
        var usuario = _mapper.Map<UsuarioViewModel>( _serviceUsuario.GetAll());

        return Ok(usuario);
    }


    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        return  Ok("value");
    }


    [HttpPost]
    public IActionResult Post([FromBody] UsuarioViewModel item)
    {
        try
        {
            return Ok(new { message = "Sucesso", success = true });
        }
        catch (Exception e)
        {
            return BadRequest(new { message = e.Message, success = false });
        }
    }


    [HttpPut]
    public IActionResult Put([FromBody] UsuarioViewModel item)
    {
        try
        {
            return Ok(new { message = "Sucesso", success = true });
        }
        catch (Exception e)
        {
            return BadRequest(new { message = e.Message, success = false });
        }
    }


    [HttpDelete("{id}")]
    public IActionResult Delete(string id)
    {
        try
        {
            return Ok(new { message = "Sucesso", success = true });
        }
        catch (Exception e)
        {
            return BadRequest(new { message = e.Message, success = false });
        }
    }
}
