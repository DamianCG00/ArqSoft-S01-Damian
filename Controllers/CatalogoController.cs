using Catalogo.Models;
using Microsoft.AspNetCore.Mvc;


namespace Catalogo.Controllers
{
    public class CatalogoController : Controller

    {
        private static List<Item> _items = new()
        {
            new Item{
                Id          = 1,
                Titulo      = "Devil May Cry",
                Genero      = "Hack and slash",
                Ano         = 2001,
                Consola     = "PlayStation 2",
                Descripcion = "Videojuego que trata de un cazador...",
            },

            new Item
            {
                Id          = 1,
                Titulo      ="Castelvania: Syphony of the Night",
                Genero      = "Metroidvania",
                Ano         = 1997,
                Consola     = "PlayStation 2",
                Descripcion = "Videjuego que trata de un cazador..."
            },
            new Item { /*Datos del Videjuego NieR: Automatica */ }
        };

        //Lista - con filtro opcional por género
        public IActionResult Index(String? genero)
        {
            var resultado = string.IsNullOrEmpty(genero)
                ? _items
                : _items.Where(i => i.Genero == genero).ToList();

            ViewBag.Generos = _items.Select(i => i.Genero).Distinct().ToList();
            ViewBag.GeneroActual = genero;

            return View(resultado);

        }

        //Detalle
        public IActionResult Detalle(int id)
        {
            var item = _items.FirstOrDefault(i => i.Id == id);
            return item == null ? NotFound() : View(item);
        }

        //Formulario - GET
        public IActionResult Agregar()
        {
            return View();
        }

        //formulario - POST
        [HttpPost]
        public IActionResult Agregar(Item item)
        {
            // Busca el ID más alto actual y le suma 1 para el nuevo juego
            item.Id = _items.Any() ? _items.Max(i => i.Id) + 1 : 1;

            _items.Add(item);
            return RedirectToAction("Index");
        }
    }
    
}
