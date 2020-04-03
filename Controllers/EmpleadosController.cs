using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRUDSinInyeccionASP.Models.Entities;
using CRUDSinInyeccionASP.Models.Abstract;

namespace CRUDSinInyeccionASP.Controllers
{
    public class EmpleadosController : Controller
    {
        private readonly IEmpleadoBusiness _context;
        private Empleado empleado;

        public EmpleadosController(IEmpleadoBusiness context)
        {
            _context = context;
        }

        // GET: Empleados
        public async Task<IActionResult> Index(string busqueda)
        {
            if (!string.IsNullOrEmpty(busqueda))
                return View(await _context.obtenerEmpleadosPorNombrePorId(busqueda));
            else
                return View(await _context.obtenerEmpleadosTodos());
        }


        // GET: Empleados/Details/5
        public async Task<IActionResult> CrearEditar(int id = 0)
        {
            List<CargoEmpleado> CargoEmpleado = new List<CargoEmpleado>();
            CargoEmpleado = await _context.obtenerCargoTodos();

            ViewBag.Cargos = CargoEmpleado;

            if (id == 0)
                return View(new Empleado());
            else
                empleado = await _context.obtenerEmpleadoPorID(id);
            return View(empleado);

        }

        [HttpPost]
        public async Task<IActionResult> CrearEditar([Bind("IdEmpleado,Nombre,Documento,Cargo,Telefono")] Empleado empleado)
        {
            //List<CargoEmpleado> cargoEmpleados = new List<CargoEmpleado>();
            //cargoEmpleados = await _context.obtenerCargoTodos();

            //ViewBag.Cargos = cargoEmpleados;

            if (ModelState.IsValid)
            {
                await _context.guardarEmpleado(empleado);

                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));

        }

        // GET: Empleados/Details/5
        [HttpGet]
        public async Task<ActionResult<Empleado>> EmpleadoPorID(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var empleado = await _context.obtenerEmpleadoPorID(id);

            if (empleado == null)
            {
                return NotFound();
            }

            return empleado;
        }
        public async Task<IActionResult> Eliminar(int? id)
        {

            await _context.eliminarEmpleado(await _context.obtenerEmpleadoPorID(id));
            return RedirectToAction(nameof(Index));
        }

    }
}
