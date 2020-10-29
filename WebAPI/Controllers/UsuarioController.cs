using Financial.Application.Usuario;
using Financial.Application.ValueObject;
using MediatR;
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
        public ActionResult<UsuarioVO> Get(int id)
        {
            
            return Ok(mediator.Send(new GetDadosUsuario(id)));
        }
    }
}
