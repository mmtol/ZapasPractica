using Microsoft.AspNetCore.Mvc;
using ZapasPractica.Models;
using ZapasPractica.Repositories;

namespace ZapasPractica.Controllers
{
    public class ZapatillasController : Controller
    {
        private RepositoryZapatillas repo;

        public ZapatillasController(RepositoryZapatillas repo)
        {
            this.repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            List<Zapatilla> zapatillas = await repo.GetZapatillasAsync();
            return View(zapatillas);
        }

        public async Task<IActionResult> Details(int idzapa, int? posicion)
        {
            Zapatilla zapa = await repo.FindZapatillaAsync(idzapa);

            if (posicion == null)
            {
                posicion = 1;
            }

            int registros = await repo.GetCountImagenesZapaAsync(idzapa);
            ViewData["registros"] = registros;

            long siguiente = posicion.Value + 1;
            if (siguiente > registros)
            {
                siguiente = registros;
            }
            long anterior = posicion.Value - 1;
            if (anterior < 1)
            {
                anterior = 1;
            }

            ViewData["ultimo"] = registros;
            ViewData["siguiente"] = siguiente;
            ViewData["anterior"] = anterior;

            ViewData["numpagina"] = posicion;

            return View(zapa);
        }

        public async Task<IActionResult> _ImagenPartial(int idzapa, long? posicion)
        {
            if (posicion == null)
            {
                posicion = 1;
            }

            ImagenZapatilla img = await repo.GetImagenZapaAsync(idzapa, posicion);
            ViewData["idzapa"] = idzapa;
            return PartialView("_ImagenPartial", img);
        }
    }
}
