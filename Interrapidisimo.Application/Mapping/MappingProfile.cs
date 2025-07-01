using AutoMapper;
using Interrapidisimo.Application.DTOs;
using Interrapidisimo.Domain.Entities;

namespace Interrapidisimo.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Estudiante mappings
            CreateMap<Estudiante, EstudianteDto>();
            
            //CreateMap<Estudiante, EstudianteListDto>();
            CreateMap<Estudiante, EstudianteListDto>()
                .ForMember(dest => dest.NombreCompleto,
                            opt => opt.MapFrom(src => $"{src.Nombre} {src.Apellido}"));

            CreateMap<Estudiante, EstudianteCompaneroDto>();
            CreateMap<EstudianteCreateDto, Estudiante>();
            CreateMap<EstudianteUpdateDto, Estudiante>();

            // Profesor mappings
            CreateMap<Profesor, ProfesorDto>();
            CreateMap<Profesor, ProfesorListDto>();
            CreateMap<Profesor, ProfesorDisponibleDto>()
                .ForMember(dest => dest.ProfesorId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.NombreCompleto, opt => opt.MapFrom(src => $"{src.Nombre} {src.Apellido}"));
            CreateMap<ProfesorCreateDto, Profesor>();
            CreateMap<ProfesorUpdateDto, Profesor>();

            // Materia mappings
            CreateMap<Materia, MateriaDto>();
            CreateMap<Materia, MateriaListDto>();
            CreateMap<Materia, MateriasDisponiblesParaEstudianteDto>()
                .ForMember(dest => dest.MateriaId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.NombreMateria, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.CodigoMateria, opt => opt.MapFrom(src => src.Codigo))
                .ForMember(dest => dest.CreditosMateria, opt => opt.MapFrom(src => src.Creditos));
            CreateMap<MateriaCreateDto, Materia>();
            CreateMap<MateriaUpdateDto, Materia>();

            // EstudianteMateriaProfesor mappings
            CreateMap<EstudianteMateriaProfesor, MateriaInscritaDto>()
                .ForMember(dest => dest.MateriaId, opt => opt.MapFrom(src => src.MateriaId))
                .ForMember(dest => dest.NombreMateria, opt => opt.MapFrom(src => src.Materia.Nombre))
                .ForMember(dest => dest.Creditos, opt => opt.MapFrom(src => src.Materia.Creditos))
                .ForMember(dest => dest.ProfesorId, opt => opt.MapFrom(src => src.ProfesorId))
                .ForMember(dest => dest.NombreProfesor, opt => opt.MapFrom(src => $"{src.Profesor.Nombre} {src.Profesor.Apellido}"))
                .ForMember(dest => dest.FechaInscripcion, opt => opt.MapFrom(src => src.FechaInscripcion));
        }
    }
}
