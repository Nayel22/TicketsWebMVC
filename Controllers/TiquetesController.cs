using Microsoft.AspNetCore.Mvc;
using TicketsWebMVC.Models;
using TicketsWebMVC.Services;

namespace TicketsWebMVC.Controllers
{
    public class TiquetesController : Controller
    {

        private readonly IApiService<TiqueteModel> _apiService;
        private readonly IApiService<CategoriaModel> _categoriaService;
        private readonly IApiService<UrgenciaModel> _urgenciaService;
        private readonly IApiService<ImportanciaModel> _importanciaService;
        private readonly IApiService<UsuarioModel> _usuarioService;

        public TiquetesController(
            IApiService<TiqueteModel> apiService,
            IApiService<CategoriaModel> categoriaService,
            IApiService<UrgenciaModel> urgenciaService,
            IApiService<ImportanciaModel> importanciaService,
            IApiService<UsuarioModel> usuarioService)
        {
            _apiService = apiService;
            _categoriaService = categoriaService;
            _urgenciaService = urgenciaService;
            _importanciaService = importanciaService;
            _usuarioService = usuarioService;
        }

        public async Task<IActionResult> Index()
        {
            var tiquetes = await _apiService.GetAllAsync("api/Tiquetes");
            return View(tiquetes);
        }

        public async Task<IActionResult> Details(int id)
        {
            var tiquete = await _apiService.GetByIdAsync("api/Tiquetes", id);
            return View(tiquete);
        }

        public async Task<IActionResult> Create()
        {
            await CargarDatosComboBox();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TiqueteModel tiquete)
        {
            if (ModelState.IsValid)
            {
                tiquete.ti_adicionado_por = HttpContext.Session.GetString("UserName");
                tiquete.ti_fecha_adicion = DateTime.UtcNow;

                await _apiService.CreateAsync("api/Tiquetes", tiquete);
                return RedirectToAction(nameof(Index));
            }

            await CargarDatosComboBox();
            return View(tiquete);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var tiquete = await _apiService.GetByIdAsync("api/Tiquetes", id);
            await CargarDatosComboBox();
            return View(tiquete);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TiqueteModel tiquete)
        {
            if (id != tiquete.ti_identificador)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                tiquete.ti_modificado_por = HttpContext.Session.GetString("UserName");
                tiquete.ti_fecha_modificacion = DateTime.UtcNow;

                await _apiService.UpdateAsync("api/Tiquetes", id, tiquete);
                return RedirectToAction(nameof(Index));
            }

            await CargarDatosComboBox();
            return View(tiquete);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var tiquete = await _apiService.GetByIdAsync("api/Tiquetes", id);
            return View(tiquete);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _apiService.DeleteAsync("api/Tiquetes", id);
            return RedirectToAction(nameof(Index));
        }

        private async Task CargarDatosComboBox()
        {
            ViewBag.Categorias = await _categoriaService.GetAllAsync("api/Categorias");
            ViewBag.Urgencias = await _urgenciaService.GetAllAsync("api/Urgencias");
            ViewBag.Importancias = await _importanciaService.GetAllAsync("api/Importancias");
            ViewBag.Usuarios = await _usuarioService.GetAllAsync("api/Usuarios");
        }


    }
}
