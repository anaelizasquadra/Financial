using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Financial.WebAPI.Models.Response
{
    public class UsuarioResponseModel
    {
        public int Id { get; set; }
        public string NomeCompleto { get; set; }
        public string CPF { get; set; }
        public string TelefoneFixo { get; set; }
        public string TelefoneCelular { get; set; }

    }
}
