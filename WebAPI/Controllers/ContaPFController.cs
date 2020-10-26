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
        public IEnumerable<ContaPF> Get()
        {
            return GerarLista();
        }


        /// <summary>
        /// Filtro de lista de conta pessoa física, pelo id da conta
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ContaPF Get(int id)
        {
            return GerarLista()
                .FirstOrDefault(conta => conta.Id == id); //<- Expressão Lambda
        }

        /// <summary>
        /// Método que irá incrementar um registro na lista de contas
        /// </summary>
        /// <param name="contaPF"></param>
        [HttpPost]
        public IActionResult Post([FromBody] ContaPF contaPF)
        {
            var listaContas = GerarLista();
            var conta = new ContaPF()
            {
                Id = 6, //Automatizar ente número pra buscar o ultimo id da lista, e incrementar +1
                TipoConta = contaPF.TipoConta,
                Agencia = contaPF.Agencia,
                Conta = contaPF.Conta,
                NomeCompleto = contaPF.NomeCompleto
            };

            listaContas.Add(conta);

            try
            {
                if (listaContas.Count > 5)
                    return Ok(listaContas);
                else
                    throw new Exception();
            }
            catch (Exception)
            {
                return BadRequest(conta);
            }
        }

        /// <summary>
        /// Método que atualiza todo o registro desejado
        /// </summary>
        /// <param name="id"></param>
        /// <param name="contaPF"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ContaPF Put(int id, [FromBody] ContaPF contaPF)
        {
            var conta = Get(id);

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

        // 1. Web Method (endpoint) para retornar a quantidade de items da minha lista
        /// <summary>
        /// Método ou endpoint que tem por finalidade retornar o totalizador de contas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("count")]
        public int GetQuantidade()
        {
            return GerarLista().Count();
        }

        // 2. Web Method para retornar o ultimo item (conta) da minha lista de contas
        [HttpGet]
        [Route("last")]
        public ContaPF GetUltimo()
        {
            return GerarLista().LastOrDefault();
        }

        // 3. Web Method Patch, para atualizar Nome Completo

        // 4. Método privado pra validar todas as propriedades estão sendo todas inseridas, menos o Id

        // 5. Web Method retornando a lista, ordenada por ordem alfabética, com relação ao nome (logo o Id poderá não ficar ordenado)
        [HttpGet]
        [Route("order-by-name")]
        public IOrderedEnumerable<ContaPF> GetOrderByName()
        {
            return GerarLista().OrderBy(conta => conta.NomeCompleto);
        }
    }
}
