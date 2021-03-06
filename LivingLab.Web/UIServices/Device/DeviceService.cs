using AutoMapper;

using LivingLab.Core.DomainServices.Equipment.Device;
using LivingLab.Core.DomainServices.Lab;
using LivingLab.Core.Entities.DTO.Device;
using LivingLab.Core.Entities.Identity;
using LivingLab.Core.Notifications;
using LivingLab.Web.Models.ViewModels.Device;

using Microsoft.AspNetCore.Identity;

namespace LivingLab.Web.UIServices.Device;

/// <remarks>
/// Author: Team P1-3
/// </remarks>
public class DeviceService : IDeviceService
{
    private readonly IMapper _mapper;
    private readonly ILogger<DeviceService> _logger;
    private readonly IDeviceDomainService _deviceDomainService;
    private readonly IEmailNotifier _emailSender;
    private readonly ILabProfileDomainService _labProfileDomainService;
    private readonly UserManager<ApplicationUser> _userManager;

    public DeviceService(IMapper mapper, ILogger<DeviceService> logger, IDeviceDomainService deviceService,
        IEmailNotifier emailSender, ILabProfileDomainService labProfileDomainService,
        UserManager<ApplicationUser> userManager)
    {
        _mapper = mapper;
        _logger = logger;
        _deviceDomainService = deviceService;
        _emailSender = emailSender;
        _labProfileDomainService = labProfileDomainService;
        _userManager = userManager;
    }

    /// <summary>
    /// View list of devices by device type and lab location
    /// </summary>
    /// <param name="deviceType">Device Type</param>
    /// <param name="labLocation">Lab Location</param>
    /// <returns>ViewDeviceViewModel</returns>
    public async Task<ViewDeviceViewModel> ViewDevice(string deviceType, string labLocation)
    {
        //retrieve data from db
        List<Core.Entities.Device> deviceList = await _deviceDomainService.ViewDevice(deviceType, labLocation);

        //map entity model to view model
        List<DeviceViewModel> devices = _mapper.Map<List<Core.Entities.Device>, List<DeviceViewModel>>(deviceList);

        //add list of device view model to the view device view model
        ViewDeviceViewModel viewDevices = new ViewDeviceViewModel();
        viewDevices.DeviceList = devices;
        return viewDevices;
    }

    /// <summary>
    /// View list of devices by lab location
    /// </summary>
    /// <param name="labLocation">Lab Location</param>
    /// <returns>ViewDeviceTypeViewModel</returns>
    public async Task<ViewDeviceTypeViewModel> ViewDeviceType(string labLocation)
    {
        List<ViewDeviceTypeDTO> viewDeviceTypeDtos = await _deviceDomainService.ViewDeviceType(labLocation);
        //map viewDeviceTypeDto to deviceTypeViewModel
        List<DeviceTypeViewModel> deviceList =
            _mapper.Map<List<ViewDeviceTypeDTO>, List<DeviceTypeViewModel>>(viewDeviceTypeDtos);
        ViewDeviceTypeViewModel deviceTypeViewModel = new ViewDeviceTypeViewModel();
        deviceTypeViewModel.ViewDeviceTypeDtos = deviceList;
        deviceTypeViewModel.labLocation = labLocation;
        return deviceTypeViewModel;
    }

    /// <summary>
    /// View details of a specific device
    /// </summary>
    /// <param name="id">Device ID</param>
    /// <returns>DeviceViewModel</returns>
    public async Task<DeviceViewModel> ViewDeviceDetails(int id)
    {
        //retrieve data from db
        Core.Entities.Device device = await _deviceDomainService.ViewDeviceDetails(id);
        DeviceViewModel deviceVM = _mapper.Map<Core.Entities.Device, DeviceViewModel>(device);
        return deviceVM;
    }

    /// <summary>
    /// Add new device from modal
    /// </summary>
    /// <param name="deviceViewModel">Details of Device</param>
    /// <returns>AddDeviceViewModel</returns>
    public async Task<DeviceViewModel> AddDevice(AddDeviceViewModel deviceViewModel)
    {
        if (deviceViewModel.Device.Type.Equals("Others"))
        {
            deviceViewModel.Device.Type = deviceViewModel.NewType;
        }

        var lab = await _labProfileDomainService.GetLabProfileDetails(deviceViewModel.Device.Lab.LabLocation);
        deviceViewModel.Device.Lab.LabId = lab.LabId;
        Core.Entities.Device addDevice = _mapper.Map<DeviceViewModel, Core.Entities.Device>(deviceViewModel.Device);
        await _deviceDomainService.AddDevice(addDevice);
        return deviceViewModel.Device;
    }

    /// <summary>
    /// View device details required before adding of device
    /// E.g. Device ID (for auto-increment)
    /// </summary>
    /// <returns>AddDeviceViewModel</returns>
    public async Task<AddDeviceViewModel> ViewAddDetails()
    {
        Core.Entities.Device device = await _deviceDomainService.GetDeviceLastRow();
        DeviceViewModel deviceVM = _mapper.Map<Core.Entities.Device, DeviceViewModel>(device);
        List<String> deviceTypes = await _deviceDomainService.GetDeviceTypes();
        return new AddDeviceViewModel { Device = deviceVM, DeviceTypes = deviceTypes };
    }

    /// <summary>
    /// Send email to request approval for add device/accessory to lab tech in charge
    /// </summary>
    /// <param name="url">Link appended in e-mail (e.g. Review Equipment page)</param>
    /// <param name="labLocation">Lab Location</param>
    /// <param name="labTech">Lab Technician</param>
    /// <returns>Boolean value whether e-mail has been sent</returns>
    public async Task<bool> SendReviewerEmail(string url, string labLocation, ApplicationUser labTech)
    {
        try
        {
            var lab = await _labProfileDomainService.GetLabProfileDetails(labLocation);
            if (labTech.Id == lab.LabInCharge)
            {
                var link = url + "/Equipment/ReviewEquipment/" + lab.LabLocation;
                var msg = "<h3>[" + lab.LabLocation + "]<br> New Device/Accessory Added</h3>" +
                          "Hi " + labTech.FirstName + ",<br>" +
                          "There is a new device/accessory added to <b>" + lab.LabLocation +
                          "</b> that requires your review. <br>" +
                          "Please click <a href='" + link + "'>here</a> " +
                          " to approve/decline, and to view other pending review requests.</br>";
                await _emailSender.SendEmailAsync(labTech.Email, "New Device/Accessory Review Requested", msg);
                _logger.LogInformation("Email sent to labTech in charge");
            }
            else
            {
                _logger.LogInformation("LabTech in charge not found. Email not sent.");
                return false;
            }

            return true;
        }
        catch (Exception)
        {
            _logger.LogInformation("Exception caught. Email not sent. ");
            return false;
        }
    }

    /// <summary>
    /// Edit device from modal
    /// </summary>
    /// <param name="deviceViewModel">DeviceViewModel</param>
    /// <returns>DeviceViewModel</returns>
    public async Task<DeviceViewModel> EditDevice(DeviceViewModel deviceViewModel)
    {
        //retrieve data from db
        Core.Entities.Device editDevice = _mapper.Map<DeviceViewModel, Core.Entities.Device>(deviceViewModel);
        await _deviceDomainService.EditDeviceDetails(editDevice);
        return deviceViewModel;
    }

    /// <summary>
    /// Delete device
    /// </summary>
    /// <param name="deviceViewModel">DeviceViewModel</param>
    /// <returns>DeviceViewModel</returns>
    public async Task<DeviceViewModel> DeleteDevice(DeviceViewModel deviceViewModel)
    {
        //retrieve data from db
        Core.Entities.Device deleteDevice = _mapper.Map<DeviceViewModel, Core.Entities.Device>(deviceViewModel);
        await _deviceDomainService.DeleteDevice(deleteDevice);
        return deviceViewModel;
    }
}
