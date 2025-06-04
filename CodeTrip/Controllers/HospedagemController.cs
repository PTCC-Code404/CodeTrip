using Microsoft.AspNetCore.Mvc;
using CodeTrip.Models;
using CodeTrip.Repositorio;
using MySqlX.XDevAPI;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CodeTrip.Controllers
{
    public class HospedagemController : Controller
    {
        private readonly HospedagemRepositorio _hospedagemRepositorio;



        public HospedagemController(HospedagemRepositorio hospedagemRepositorio)
        {
            _hospedagemRepositorio = hospedagemRepositorio;
        }

        public IActionResult Index()
        {
            return View(_hospedagemRepositorio.TodasHospedagens());
        }

        public IActionResult CadastrarHospedagem()
        {
            var estados = _hospedagemRepositorio.Estados() ?? new List<Estado>();
            ViewBag.Estados = new SelectList(estados, "UF_Estado", "Nome_Estado");
            var cidades = _hospedagemRepositorio.Cidades() ?? new List<Cidade>();
            ViewBag.Cidades = new SelectList(cidades, "UF_Estado", "Cidade_Nome"); 
            return View();
        }

        [HttpPost]
        public IActionResult CadastrarHospedagem(Hospedagem hospedagem)
        {

            _hospedagemRepositorio.Cadastrar(hospedagem);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult EditarHospedagem(int id)
        {
            var hospedagem = _hospedagemRepositorio.ObterHospedagem(id);

            if (hospedagem == null)
            {
                return NotFound();
            }

            return View(hospedagem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditarHospedagem(int id, [Bind("Nome_Hospedagem, Tipo_Hospedagem, Pensao, Logradouro_Endereço_Hospedagem, Numero_Endereço_Hospedagem, Bairro_Endereço_Hospedagem, Complemento_Endereço_Hospedagem, Cidade_Nome, UF_Estado")] Hospedagem hospedagem)
        {
            if (id != hospedagem.Id_Hospedagem)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (_hospedagemRepositorio.Atualizar(hospedagem))
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao Editar.");
                    return View(hospedagem);
                }
            }
            return View(hospedagem);
        }

        public IActionResult ExcluirHospedagem(int id)
        {
            _hospedagemRepositorio.Excluir(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
