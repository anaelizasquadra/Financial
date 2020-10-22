using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Financial.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaPFController : ControllerBase
    {
        private static readonly string[] TipoConta = new[]
        {
            "Poupança", "Conta Corrente", "Conta Salário", "Depósito Judicial"
        };

        private static readonly string[] Nome = new[]
{
            "João", "Maria", "Creuza", "José"
        };

        private readonly ILogger<ContaPFController> _logger;

        public ContaPFController(ILogger<ContaPFController> logger)
        {
            _logger = logger;
        }


        /// <summary>
        /// Lista de contas pessoa física
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<ContaPF> GetAll()
        {
            //var rng = new Random();
            //return Enumerable.Range(1, 1).Select(index => new ContaPF //Arrow Function
            //{
            //    Agencia = rng.Next(1111, 9999),
            //    Conta = rng.Next(111111, 999999),
            //    TipoConta = TipoConta[rng.Next(TipoConta.Length)],
            //    NomeCompleto = Nome[rng.Next(Nome.Length)],
            //})
            //.ToList();

            return GerarLista();
        }


        /// <summary>
        /// Filtro de lista de conta pessoa física, pelo id da conta
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ContaPF GetById(int id)
        {
            return GerarLista()
                .FirstOrDefault(conta => conta.Id == id);
        }

        // POST api/<ContaPFController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ContaPFController>/5
        [HttpPut("{id}")]
        public ContaPF Put(int id, [FromBody] ContaPF contaPF)
        {
            var conta = GetById(id);

            conta.Agencia = contaPF.Agencia;
            conta.Conta = contaPF.Conta;
            conta.TipoConta = contaPF.TipoConta;
            conta.NomeCompleto = contaPF.NomeCompleto;

            return conta;
        }

        /// <summary>
        /// Método que remove um item da lista, filtrado pelo id.
        /// </summary>
        /// <param name="idParam"></param>
        /// <returns></returns>
        /// <response code="404">Se a Conta não foi encontrada</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{idParam}")]
        public IActionResult Delete(int idParam)
        {            
            try
            {
                //gerar lista
                var contas = GerarLista();

                //selecionar a conta desejada a remover
                var contaASerRemovida = contas.FirstOrDefault(item => item.Id == idParam);

                //Retorna se foi removido ou não
                var seRemovido = contas.Remove(contaASerRemovida);

                if (seRemovido)
                    return Ok(contaASerRemovida);
                else
                    throw new Exception();
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        #region Métodos Privados
        private List<ContaPF> GerarLista()
        {
            var rng = new Random();
            var lista = Enumerable.Range(1, 3).Select(index => new ContaPF //Arrow Function
            {
                Agencia = rng.Next(1111, 9999),
                Conta = rng.Next(111111, 999999),
                TipoConta = TipoConta[rng.Next(TipoConta.Length)],
                NomeCompleto = Nome[rng.Next(Nome.Length)],
            })
            .ToList();

            lista.Add(new ContaPF
            {
                Agencia = 1234,
                Conta = 123456,
                TipoConta = TipoConta[rng.Next(TipoConta.Length)],
                NomeCompleto = "Maria Antonieta da Silva"
            });

            lista.Add(new ContaPF
            {
                Agencia = 1235,
                Conta = 123457,
                TipoConta = TipoConta[rng.Next(TipoConta.Length)],
                NomeCompleto = "José Maria de Souza e Albuquerque de Medeiros e Sá"
            });

            var id = 1;
            foreach (var item in lista)
            {
                item.Id = id;
                id++;
            }

            return lista;
        }

        #endregion Métodos Privados
    }
}
