using AutoMapper;
using Models.DTOs;
using Models.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilidades
{
    public class MappingProfile : Profile
    {
        // Clase de configuración de mapeo de AutoMapper
        // Sirve para definir cómo se mapean las entidades a los DTOs y viceversa
        //Ejemplo: Mapeo de Especialidad a EspecialidadDTO con conversión de bool a int para el campo Estado
        public MappingProfile()
        {
            CreateMap<Especialidad, EspecialidadDTO>()
                .ForMember(d => d.Estado, m => m.MapFrom(o => o.Estado == true ? 1 : 0));

            CreateMap<Medico, MedicoDTO>()
                .ForMember(d => d.Estado, m => m.MapFrom(o => o.Estado == true ? 1 : 0))
                .ForMember(d => d.NombreEspecialidad, m => m.MapFrom(o => o.Especialidad.NombreEspecialidad));

            CreateMap<HistoriaClinica, HistoriaClinicaDTO>()
                .ForMember(d => d.Id, m => m.MapFrom(o => o.Id.ToString()))
                .ForMember(d => d.Apellidos, m => m.MapFrom(o => o.Paciente.Apellidos))
                .ForMember(d => d.Nombres, m => m.MapFrom(o => o.Paciente.Nombres))
                .ForMember(d => d.Direccion, m => m.MapFrom(o => o.Paciente.Direccion))
                .ForMember(d => d.Telefono, m => m.MapFrom(o => o.Paciente.Telefono))
                .ForMember(d => d.Genero, m => m.MapFrom(o => o.Paciente.Genero))
                .ForMember(d => d.Estado, m => m.MapFrom(o => o.Paciente.Estado == true ? 1 : 0));


        }
    }
}
