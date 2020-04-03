using CRUDSinInyeccionASP.Clases;
using CRUDSinInyeccionASP.Models.Abstract;
using CRUDSinInyeccionASP.Models.DAL;
using CRUDSinInyeccionASP.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDSinInyeccionASP.Models.Business
{
    public class CargoBusiness : ICargoBusiness
    {
        private readonly DbContextPrueba _context;

        public CargoBusiness(DbContextPrueba context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<CargoDetalle>> ObtenerCargoEmpleadosTodos()
        {
            await using (_context)
            {
                IEnumerable<CargoDetalle> ListaCargoEmpleadoDetalle =
                (
                    from CargoEmpleado in _context.CargoEmpleados

                    select new CargoDetalle
                    {
                        IdCargo = CargoEmpleado.IdCargo,
                        Cargo = CargoEmpleado.Cargo,
                    }).ToList();

                return ListaCargoEmpleadoDetalle;
            }
        }

        public async Task guardarCargoEmpleado(CargoEmpleado cargoEmpleado)
        {
            if (cargoEmpleado.IdCargo == 0)
                _context.Add(cargoEmpleado);
            else
                _context.Update(cargoEmpleado);

            await _context.SaveChangesAsync();
        }

        public async Task eliminarCargoEmpleado(CargoEmpleado cargoEmpleado)
        {
            if (cargoEmpleado != null)
            {
                _context.Remove(cargoEmpleado);
                await _context.SaveChangesAsync();

            }

        }

        //Devuelve un empleado
        public async Task<CargoEmpleado> obtenerCargoEmpleadoPorID(int? id)
        {
            CargoEmpleado cargoEmpleado;
            cargoEmpleado = null;

            if (id == null)
            {
                return cargoEmpleado;
            }
            else
            {
                cargoEmpleado = await _context.CargoEmpleados.FirstOrDefaultAsync(m => m.IdCargo == id);
                return cargoEmpleado;

            }

        }

        //Este metodo deberia tener estar en su clase Businesss
        public async Task<List<CargoEmpleado>> obtenerCargoTodos()
        {
            return await _context.CargoEmpleados.ToListAsync();
        }

        public async Task<IEnumerable<CargoDetalle>> obtenerCargosPorNombrePorId(string busqueda)
        {

            await using (_context)
            {
                IEnumerable<CargoDetalle> listaCargoEmpleadoDetalle =
                (from cargoEmpleado in _context.CargoEmpleados

                 select new CargoDetalle
                 {
                     Cargo = cargoEmpleado.Cargo,


                 }).ToList();
                return listaCargoEmpleadoDetalle;
            }
        }
    }
}