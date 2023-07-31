using AutoMapper;
using DnsClient;
using Imoby.Api.Token;
using Imoby.Domain.Interface.Service;
using Imoby.Entities.Entitie.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using MongoDB.Bson;
using System.Text;

namespace Imoby.Api.Controllers.Private.Cadastro;
[Route("api/v1/[controller]")]
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

    #region Controller para gerar token

    //[AllowAnonymous]
    //[HttpPost("/api/CriarTokenIdentity")]
    //public async Task<IActionResult> CriarTokenIdentity([FromBody] UsuarioViewModel item)
    //{
    //    if (string.IsNullOrWhiteSpace(item.Email) || string.IsNullOrWhiteSpace(item.Senha))
    //    {
    //        return Unauthorized();
    //    }

    //    var resultado = await _signInManager.PasswordSignInAsync(item.email, item.senha, false, lockoutOnFailure: false);

    //    if (resultado.Succeeded)
    //    {
    //        // Recupera Usuário Logado
    //        var userCurrent = await _userManager.FindByEmailAsync(item.email);
    //        var idUsuario = userCurrent.Id;

    //        var token = new TokenJWTBuilder()
    //            .AddSecurityKey(JwtSecurityKey.Create("Secret_Key-12345678"))
    //        .AddSubject("Empresa - Canal Dev Net Core")
    //        .AddIssuer("Teste.Securiry.Bearer")
    //        .AddAudience("Teste.Securiry.Bearer")
    //        .AddClaim("idUsuario", idUsuario)
    //        .AddExpiry(5)
    //        .Builder();

    //        return Ok(token.value);
    //    }
    //    else
    //    {
    //        return Unauthorized();
    //    }
    //}

    #endregion


    [AllowAnonymous]
    [HttpPost("createLogin")]
    public async Task<IActionResult> CreateLogin([FromBody] UsuarioViewModel login)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(login.Name) || string.IsNullOrWhiteSpace(login.Email) || string.IsNullOrWhiteSpace(login.Senha))
                return BadRequest("Falta alguns dados");

            if (login.ValidaPoliticaLogin(login.Email))
                return BadRequest("Email invalido");

            if (login.ValidaPoliticaSenha(login.Senha))
                return BadRequest("Senha nao atende requisitos minimos");

            //Validar email
            var userExist = _serviceUsuario.GetEmailExist(login.Email);
            if (!userExist)
                return BadRequest("Email informado não esta disponivel");

            var user = new UsuarioViewModel
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Name = login.Name,
                Email = login.Email,
                Senha = login.Senha,
            };

            user.Senha = login.CriptografaMd5(user.Senha);
            _serviceUsuario.Add(_mapper.Map<Usuario>(user));


            return Ok(login.CreateToken(user));
            //  return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(new { authenticated = false, message = e.Message, success = false });
        }

    }




}
