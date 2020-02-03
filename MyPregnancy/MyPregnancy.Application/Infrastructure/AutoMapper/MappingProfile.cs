namespace MyPregnancy.Application.Infrastructure.AutoMapper
{
    using Common.Dtos;
    using Patients.Commands.CreatePatient;
    using Domain.Entities;
    using global::AutoMapper;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Patient, PatientDto>()
                .ForMember(dest => dest.BloodGroup, opt => opt.MapFrom(src => src.MedicalDetail.BloodGroup))
                .ForMember(dest => dest.KnownAllergies, opt => opt.MapFrom(src => src.MedicalDetail.KnownAllergies))
                .ForMember(dest => dest.Rhesus, opt => opt.MapFrom(src => src.MedicalDetail.Rhesus));

            CreateMap<CreatePatientCommand, Patient>().ForMember(dest => dest.MedicalDetail, opt => opt.MapFrom(src => src));

            CreateMap<CreatePatientCommand, MedicalDetail>()
                .ForMember(dest => dest.BloodGroup, opt => opt.MapFrom(src => src.BloodGroup))
                .ForMember(dest => dest.KnownAllergies, opt => opt.MapFrom(src => src.KnownAllergies))
                .ForMember(dest => dest.Rhesus, opt => opt.MapFrom(src => src.Rhesus));

        }
    }
}
