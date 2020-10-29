using Financial.Application.Usuario;
using Financial.Application.ValueObject;
using MediatR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Financial.Services.Handler.Usuario
{
    public class GetDadosUsuarioHandler : IRequestHandler<GetDadosUsuario, UsuarioVO>
    {
        string filename = "./usuarios.json";

        private readonly IMediator _mediator;
        public GetDadosUsuarioHandler(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<UsuarioVO> Handle(GetDadosUsuario request, CancellationToken cancellationToken)
        {
            try
            {
                return ReadFile().FirstOrDefault(x => x.Id.Equals(request));
            }
            catch (Exception)
            {
                throw;
            }
        }

        private List<UsuarioVO> ReadFile()
        {
            //List<UsuarioVO> list;

            //Verificando a existencia do arquivo
            //if (File.Exists(filename))
            //{
            //    string jsonString = File.ReadAllText(filename);
            //    return JsonSerializer.Deserialize<List<UsuarioVO>>(jsonString);
            //}
            //else
            //{
            //    return new List<UsuarioVO>();
            //}

            //Versão Operador Ternário
            return File.Exists(filename)
                ? JsonSerializer.Deserialize<List<UsuarioVO>>(File.ReadAllText(filename))
                : new List<UsuarioVO>();
        }
    }
}
