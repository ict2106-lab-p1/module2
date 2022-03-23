using AutoMapper;

using LivingLab.Core.Entities;
using LivingLab.Web.Models.ViewModels.EnergyUsage;

namespace LivingLab.Web.Mapping;

public class LabProfile : Profile
{
    public LabProfile()
    {
        CreateMap<Lab, EnergyUsageLabViewModel>().ReverseMap();
    }
}
