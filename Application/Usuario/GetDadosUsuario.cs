using System;
using System.Collections.Generic;
using System.Text;
using Financial.Application.ValueObject;
using MediatR;

namespace Financial.Application.Usuario
{
    public class GetDadosUsuario : IRequest<UsuarioVO>
    {
        private int id;

        public GetDadosUsuario(int id)
        {
            this.id = id;
        }
    }
}
