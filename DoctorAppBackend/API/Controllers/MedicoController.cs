using BLL.Servicios.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs;
using System.Net;

namespace API.Controllers
{
    [Authorize(Policy = "AdminAgendadorRol")]
    public class MedicoController : BaseAPIController
    {
        private readonly IMedicoServicio _medicoServicio;
        private ApiResponse _response;
        public MedicoController(IMedicoServicio medicoServicio)
        {
            _medicoServicio = medicoServicio;
            _response = new();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                _response.Resultado = await _medicoServicio.ObtenerTodos(); // Obtener todas las especialidades
                _response.EsExitoso = true; // Indicar que la operación fue exitosa
                _response.StatusCode = HttpStatusCode.OK; // 200
            }
            catch (Exception ex)
            {

                _response.EsExitoso = false; // Indicar que la operación no fue exitosa
                _response.Mensaje = ex.Message; // Detallar el error
                _response.StatusCode = HttpStatusCode.BadRequest; // 400
            }

            return Ok(_response);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(MedicoDTO modeloDto)
        {
            try
            {
                await _medicoServicio.Agregar(modeloDto);
                _response.EsExitoso = true;
                _response.StatusCode = HttpStatusCode.Created; // 201
            }
            catch (Exception ex)
            {
                _response.EsExitoso = false;
                _response.Mensaje = ex.Message;
                _response.StatusCode = HttpStatusCode.BadRequest; // 400
            }

            return Ok(_response);
        }

        [HttpPut]
        public async Task<IActionResult> Editar(MedicoDTO modeloDto)
        {
            try
            {
                await _medicoServicio.Actualizar(modeloDto);
                _response.EsExitoso = true;
                _response.StatusCode = HttpStatusCode.NoContent; // 200
            }
            catch (Exception ex)
            {
                _response.EsExitoso = false;
                _response.Mensaje = ex.Message;
                _response.StatusCode = HttpStatusCode.BadRequest; // 400
            }
            return Ok(_response);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                await _medicoServicio.Remover(id);
                _response.EsExitoso = true;
                _response.StatusCode = HttpStatusCode.NoContent; // 204
            }
            catch (Exception ex)
            {
                _response.EsExitoso = false;
                _response.Mensaje = ex.Message;
                _response.StatusCode = HttpStatusCode.BadRequest; // 400
            }
            return Ok(_response);

        }
    }
}