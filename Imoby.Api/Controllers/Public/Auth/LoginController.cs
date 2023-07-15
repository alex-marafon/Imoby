using AutoMapper;
using Imoby.Domain.Interface.Service;
using Imoby.Entities.Entitie.Models;
using Microsoft.AspNetCore.Mvc;

namespace Imoby.Api.Controllers.Private.Cadastro;
[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IServiceUsuario _serviceUsuario;

    public LoginController(IServiceUsuario serviceUsuario, IMapper mapper)
    {
        _serviceUsuario = serviceUsuario;
        _mapper = mapper;
    }


    [HttpGet]
    public IActionResult Get()
    {
        try
        {
            var usuario = _mapper.Map<IList<UsuarioViewModel>>( _serviceUsuario.GetAll());

            return Ok(usuario);
        }
        catch (Exception e)
        {
            return BadRequest(new { message = e.Message, success = false });
        }
    }


    [HttpGet("{id}")]
    public IActionResult Get(string id)
    {
        try
        {
            var usuario = _mapper.Map<UsuarioViewModel>(_serviceUsuario.GetById(id));

            return Ok(usuario);
        }
        catch (Exception e)
        {
            return BadRequest(new { message = e.Message, success = false });
        }
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
    public IActionResult Put( [FromBody] UsuarioViewModel item)
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
    public IActionResult Delete(int id)
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
