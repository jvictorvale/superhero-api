using AutoMapper;
using SuperHero.Application.DTOs.Heroi;
using SuperHero.Application.DTOs.Superpoder;
using SuperHero.Domain.Entities;

namespace SuperHero.Application.Settings;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        #region HeroiMapper

        CreateMap<Heroi, AdicionarHeroiDto>().ReverseMap();
        CreateMap<Heroi, AtualizarHeroiDto>().ReverseMap();
        CreateMap<Heroi, HeroiDto>().ReverseMap();

        #endregion

        #region SuperpoderMapper

        CreateMap<Superpoder, SuperpoderDto>().ReverseMap();

        #endregion
    }
}