using AutoMapper;

using LivingLab.Core.Entities;
using LivingLab.Core.Entities.DTO;
using LivingLab.Core.Models;
using LivingLab.Web.Models.DTOs.Todo;
using LivingLab.Web.Models.ViewModels;
using LivingLab.Web.Models.ViewModels.Accessory;
using LivingLab.Web.Models.ViewModels.Device;

namespace LivingLab.Web.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Todo, TodoDTO>().ReverseMap();
        CreateMap<Device, DeviceViewModel>().ReverseMap();
        CreateMap<Accessory, AccessoryViewModel>().ReverseMap();
        CreateMap<ViewDeviceTypeDTO, DeviceTypeViewModel>().ReverseMap();
        CreateMap<ViewAccessoryTypeDTO, AccessoryTypeViewModel>().ReverseMap();
    }
}
