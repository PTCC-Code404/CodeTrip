using Microsoft.AspNetCore.Mvc;
using CodeTrip.Models;
using CodeTrip.Repositorio;
using MySqlX.XDevAPI;
using MySql.Data.MySqlClient;

namespace CodeTrip.Controllers
{
    public class PedidoController : Controller
    {
        private readonly PedidoRepositorio _pedidoRepositorio;



        public PedidoController(PedidoRepositorio pedidoRepositorio)
        {
            _pedidoRepositorio = pedidoRepositorio;
        }

        public IActionResult Index()
        {
            return View(_pedidoRepositorio.TodosPedidos());
        }

        public IActionResult CadastrarPedido()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CadastrarPedido(Pedido pedido)
        {

            _pedidoRepositorio.Cadastrar(pedido);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult EditarPedido(int id)
        {
            var pedido = _pedidoRepositorio.ObterPedido(id);

            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditarPedido(int id, [Bind("Id_Pedido, Id_Usuario, CPF_Cli, Id_Transp, Id_End_Transporte, Id_Hospedagem, Id_Pagamento")] Pedido pedido)
        {
            if (id != pedido.Id_Pedido)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (_pedidoRepositorio.Atualizar(pedido))
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao Editar.");
                    return View(pedido);
                }
            }
            return View(pedido);
        }

        public IActionResult ExcluirPedido(int id)
        {
            _pedidoRepositorio.Excluir(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

