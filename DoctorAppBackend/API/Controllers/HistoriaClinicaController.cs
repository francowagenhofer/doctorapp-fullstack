using BLL.Servicios.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs;
using System.Net;
using System.Security.Claims;

namespace API.Controllers
{
    [Authorize(Policy ="AdminAgendadorRol")]
    public class HistoriaClinicaController : BaseAPIController
    {
        private readonly IHistoriaClinicaServicio _historiaClinicaServicio;
        private ApiResponse _response;

        public HistoriaClinicaController(IHistoriaClinicaServicio historiaClinicaServicio)
        {
            _historiaClinicaServicio = historiaClinicaServicio;
            _response = new();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                _response.Resultado = await _historiaClinicaServicio.ObtenerTodos();
                _response.EsExitoso = true;
                _response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                _response.EsExitoso= false;
                _response.Mensaje= ex.Message;
                _response.StatusCode = HttpStatusCode.BadRequest;
            }
            return Ok(_response);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(HistoriaClinicaDTO modeloDto)
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
            modeloDto.UserName = claim.Value;
            try
            {
                await _historiaClinicaServicio.Agregar(modeloDto);
                _response.EsExitoso = true;
                _response.StatusCode = HttpStatusCode.Created;
            }
            catch (Exception ex)
            {
                _response.EsExitoso = false;
                _response.Mensaje = ex.Message;
                _response.StatusCode = HttpStatusCode.BadRequest;
            }
            return Ok(_response);
        }

    }
}
