using Financial.Application.Usuario;
using Financial.Application.ValueObject;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Financial.Services.Handler.Usuario
{
    public class GetDadosUsuarioHandler : IRequestHandler<GetDadosUsuario, UsuarioVO>
    {
        public GetDadosUsuarioHandler()
        {
        }

        public Task<UsuarioVO> Handle(GetDadosUsuario request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
