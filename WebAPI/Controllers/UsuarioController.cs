using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Financial.Application.Usuario;
using Financial.WebAPI.Models.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Financial.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IMediator mediator;

        public UsuarioController(IMediator _mediator)
        {
            mediator = _mediator;
        }

        /// <summary>
        /// Método utilizado para retornar os dados do usuário
        /// </summary>
        /// <remarks>Dados retornados, filtrado pelo id do usuário desejado</remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<UsuarioResponseModel> Get(int id)
        {
            var dadosUsuario = mediator.Send(new GetDadosUsuario(id));
            return Ok();
        }
    }
}
