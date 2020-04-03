using CRUDSinInyeccionASP.Clases;
using CRUDSinInyeccionASP.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRUDSinInyeccionASP.Models.Abstract
{
    public interface ICargoBusiness
    {
        Task<IEnumerable<CargoDetalle>> ObtenerCargoEmpleadosTodos();
        Task guardarCargoEmpleado(CargoEmpleado cargoEmpleado);
        Task eliminarCargoEmpleado(CargoEmpleado cargoEmpleado);
        Task<CargoEmpleado> obtenerCargoEmpleadoPorID(int? id);
        Task<List<CargoEmpleado>> obtenerCargoTodos();
        Task<IEnumerable<CargoDetalle>> obtenerCargosPorNombrePorId(string busqueda);
    }
}
