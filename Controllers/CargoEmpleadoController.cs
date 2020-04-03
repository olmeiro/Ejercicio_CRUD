using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRUDSinInyeccionASP.Models.DAL;
using CRUDSinInyeccionASP.Models.Entities;
using CRUDSinInyeccionASP.Models.Abstract;

namespace CRUDSinInyeccionASP.Controllers
{
    public class CargoEmpleadoController : Controller
    {
        private readonly ICargoBusiness _context;
        private CargoEmpleado cargoEmpleado;

        public CargoEmpleadoController(ICargoBusiness context)
        {
            _context = context;
        }

        // GET: CargoEmpleado
        public async Task<IActionResult> Index(string busqueda)
        {
            if (!string.IsNullOrEmpty(busqueda))
                return View(await _context.obtenerCargosPorNombrePorId(busqueda));
            else
                return View(await _context.ObtenerCargoEmpleadosTodos());
        }

        public async Task<IActionResult> CrearEditarCargo(int id = 0)
        {
            if (id == 0)
                return View(new CargoEmpleado());
            else
                cargoEmpleado = await _context.obtenerCargoEmpleadoPorID(id);
            return View(cargoEmpleado);

        }

        [HttpPost]
        public async Task<IActionResult> CrearEditarCargo([Bind("IdCargo,Cargo")] CargoEmpleado cargoEmpleado)
        {
            if (ModelState.IsValid)
            {
                await _context.guardarCargoEmpleado(cargoEmpleado);

                return RedirectToAction(nameof(Index));
            }
                return RedirectToAction(nameof(Index));
        }

        // GET: Empleados/Details/5
        [HttpGet]
        public async Task<ActionResult<CargoEmpleado>> EmpleadoPorID(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.obtenerCargoEmpleadoPorID(id);

            if (empleado == null)
            {
                return NotFound();
            }

            return empleado;
        }

        public async Task<IActionResult> Eliminar(int? id)
        {
            await _context.eliminarCargoEmpleado(await _context.obtenerCargoEmpleadoPorID(id));
            return RedirectToAction(nameof(Index));
        }
    }
}

