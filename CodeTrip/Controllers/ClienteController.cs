using Microsoft.AspNetCore.Mvc;
using CodeTrip.Models;
using CodeTrip.Repositorio;
using MySqlX.XDevAPI;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CodeTrip.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ClienteRepositorio _clienteRepositorio;



        public ClienteController(ClienteRepositorio clienteRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;
        }

        public IActionResult Index()
        {
            return View(_clienteRepositorio.TodosClientes());
        }

        public IActionResult CadastrarCliente()
        {
            var estados = _clienteRepositorio.Estados() ?? new List<Estado>();
            ViewBag.Estados = new SelectList(estados, "UF_Estado", "Nome_Estado");
            return View();
        }

        

        [HttpPost]
        public IActionResult CadastrarCliente(string UF_Estado)
        {
            var cidades = _clienteRepositorio.CidadesPorEstado(UF_Estado);
            ViewBag.Cidades = new SelectList(cidades, "Cidade_Nome", "UF_Estado", UF_Estado);
            ViewBag.HasCidades = cidades != null && cidades.Any();

            var cidadesPorEstado = _clienteRepositorio.CidadesPorEstado(UF_Estado);
            return View(cidadesPorEstado);
            
        }


        [HttpPost]
        public IActionResult CadastrarCliente(Cliente cliente)
        {

            _clienteRepositorio.Cadastrar(cliente);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult EditarCliente(string cpf)
        {
            var cliente = _clienteRepositorio.ObterCliente(cpf);

            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditarCliente(string cpf, [Bind("Nome_Cli, Email_Cli, Data_Nasc_Cli, CPF_Cli, Telefone_Cli, Logradouro_Cli, Numero_Cli, Bairro_Cli, Complemento_Cli, Cidade_Nome, UF_Estado")] Cliente cliente)
        {
            if (cpf != cliente.CPF_Cli)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (_clienteRepositorio.Atualizar(cliente))
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao Editar.");
                    return View(cliente);
                }
            }
            return View(cliente);
        }

        public IActionResult ExcluirCliente(string cpf)
        {
            _clienteRepositorio.Excluir(cpf);
            return RedirectToAction(nameof(Index));
        }
    }
}
