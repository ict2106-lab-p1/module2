using LivingLab.Core.Constants;
using LivingLab.Core.Entities;

using Microsoft.EntityFrameworkCore;

namespace LivingLab.Infrastructure.Data;

public static class DataSeeder
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        // modelBuilder.Entity<Device>().HasData(
        //     new Device { Id = 1, DeviceSerialNumber = "DEVICE-3390", Type = DeviceTypes.SMART_SENSOR, EnergyUsageThreshold = GetRandomNumber(), Lab = new Lab { Id = 1, Area = 12 }},
        //     new Device { Id = 2, DeviceSerialNumber = "DEVICE-6049", Type = DeviceTypes.SMART_SENSOR, EnergyUsageThreshold = GetRandomNumber(), Lab = new Lab { Id = 1, Area = 12 } },
        //     new Device { Id = 3, DeviceSerialNumber = "DEVICE-1598", Type = DeviceTypes.SMART_SENSOR, EnergyUsageThreshold = GetRandomNumber(), Lab = new Lab { Id = 1, Area = 12 } },
        //     new Device { Id = 4, DeviceSerialNumber = "DEVICE-1232", Type = DeviceTypes.SMART_SENSOR, EnergyUsageThreshold = GetRandomNumber(), Lab = new Lab { Id = 1, Area = 12 } },
        //     new Device { Id = 5, DeviceSerialNumber = "DEVICE-1123", Type = DeviceTypes.SMART_SENSOR, EnergyUsageThreshold = GetRandomNumber(), Lab = new Lab { Id = 1, Area = 12 } },
        //     new Device { Id = 6, DeviceSerialNumber = "DEVICE-8987", Type = DeviceTypes.SMART_SENSOR, EnergyUsageThreshold = GetRandomNumber(), Lab = new Lab { Id = 1, Area = 12 } },
        //     new Device { Id = 7, DeviceSerialNumber = "DEVICE-2435", Type = DeviceTypes.SMART_SENSOR, EnergyUsageThreshold = GetRandomNumber(), Lab = new Lab { Id = 1, Area = 12 } },
        //     new Device { Id = 8, DeviceSerialNumber = "DEVICE-1234", Type = DeviceTypes.SMART_SENSOR, EnergyUsageThreshold = GetRandomNumber(), Lab = new Lab { Id = 1, Area = 12 } },
        //     new Device { Id = 9, DeviceSerialNumber = "DEVICE-5423", Type = DeviceTypes.SMART_SENSOR, EnergyUsageThreshold = GetRandomNumber(), Lab = new Lab { Id = 1, Area = 12 } },
        //     new Device { Id = 10, DeviceSerialNumber = "DEVICE-7452", Type = DeviceTypes.SMART_SENSOR, EnergyUsageThreshold = GetRandomNumber(), Lab = new Lab { Id = 1, Area = 12 } }
        // );
        //
        
        modelBuilder.Entity<Lab>().HasData(
            new Lab { Id = 1, Area = 12, LabStatus = "Available", Location = "NYP-SR7C", PersonInCharge = "Mr Tan" },
            new Lab { Id = 2, Area = 12, LabStatus = "Available", Location = "NYP-SR7C", PersonInCharge = "Mr Tan" },
            new Lab { Id = 3, Area = 12, LabStatus = "Available", Location = "NYP-SR7C", PersonInCharge = "Mr Tan" },
            new Lab { Id = 4, Area = 12, LabStatus = "Available", Location = "NYP-SR7C", PersonInCharge = "Mr Tan" }
        );
        
        modelBuilder.Entity<Device>().HasData(
                new Device { Id = 1, LastUpdated = new DateTime(2020, 10, 10), DeviceSerialNumber = "SC1001", LabId = 1, Status = "Available", Type = "Surveillance Camera", Description = "Its purpose is to detect situation in the laboratory" },
                new Device { Id = 2, LastUpdated = new DateTime(2020, 10, 11), DeviceSerialNumber = "R1001", LabId = 1, Status = "Available", Type = "Temperature Sensor", Description = "Its purpose is to detect temperature in the laboratory" },
                new Device { Id = 3, LastUpdated = new DateTime(2020, 9, 9), DeviceSerialNumber = "S1001", LabId = 1, Status = "Available", Type = "Humidity Sensor", Description = "Its purpose is to detect humidity in the laboratory" },
                new Device { Id = 4, LastUpdated = new DateTime(2019, 8, 1), DeviceSerialNumber = "SL1001", LabId = 1, Status = "Available", Type = "Light Sensor", Description = "Its purpose is to detect light in the laboratory" },
                new Device { Id = 5, LastUpdated = new DateTime(2019, 7, 3), DeviceSerialNumber = "VRL1001", LabId = 1, Status = "Unavailable", Type = "VR Light Controls", Description = "It is used to control brightness of the lights in the lab" }
        );

        // Accessory and Accessory Types
        modelBuilder.Entity<AccessoryType>().HasData(
                new AccessoryType{ Id = 1, Type = "Camera", Borrowable = true, Name = "Sony A7 IV", Description = "Its purpose is to capture images and videos" },
                new AccessoryType{ Id = 2, Type = "Ultrasonic Sensor", Borrowable = true, Name = "MA300D1-1", Description = "Its purpose is to detect obstacles" },
                new AccessoryType{ Id = 3, Type = "Humidity Sensor", Borrowable = true, Name = "DHT22", Description = "Its purpose is to detect humidity in the environment" },
                new AccessoryType{ Id = 4, Type = "Water pressure Sensor", Borrowable = true, Name = "LEFOO LFT2000W", Description = "Its purpose is to detect water pressure" },
                new AccessoryType{ Id = 5, Type = "IR Sensor", Borrowable = true, Name = "RM1802", Description = "It is used to switch on the lights in the lab" },
                new AccessoryType{ Id = 6, Type = "Proximity Sensor", Borrowable = true, Name = "HC-SR04", Description = "Its purpose is to detect proximity of an obstacle" },
                new AccessoryType{ Id = 7, Type = "LED Lights", Borrowable = false, Name = "EDGELEC 4Pin LED Diodes", Description = "Its purpose is to emit light" },
                new AccessoryType{ Id = 8, Type = "Buzzer", Borrowable = true, Name = "TMB09A05", Description = "Its purpose is to emit sound from the device" }
        );
        
        
        modelBuilder.Entity<Accessory>().HasData(
                new Accessory{ Id = 1, Status = "Available", LastUpdated = new DateTime(2021, 10, 10), LabId = 1, AccessoryTypeId = 1 },
                new Accessory{ Id = 2, Status = "Borrowed", LastUpdated = new DateTime(2021, 10, 14), LabId = 1, AccessoryTypeId = 1, LabUserId = 1, DueDate = new DateTime(2022, 10, 14) },
                new Accessory{ Id = 3, Status = "Available", LastUpdated = new DateTime(2021, 10, 17), LabId = 1, AccessoryTypeId = 2 },
                new Accessory{ Id = 4, Status = "Available", LastUpdated = new DateTime(2021, 10, 21), LabId = 1, AccessoryTypeId = 2 },
                new Accessory{ Id = 5, Status = "Borrowed", LastUpdated = new DateTime(2021, 9, 9), LabId = 1, AccessoryTypeId = 3, LabUserId = 2, DueDate = new DateTime(2022, 9, 9) },
                new Accessory{ Id = 6, Status = "Available", LastUpdated = new DateTime(2021, 9, 5), LabId = 1, AccessoryTypeId = 3 },
                new Accessory{ Id = 7, Status = "Available", LastUpdated = new DateTime(2021, 8, 1), LabId = 1, AccessoryTypeId = 4 },
                new Accessory{ Id = 8, Status = "Borrowed", LastUpdated = new DateTime(2021, 8, 10), LabId = 1, AccessoryTypeId = 4, LabUserId = 3, DueDate = new DateTime(2022, 9, 5) },
                new Accessory{ Id = 9, Status = "Available", LastUpdated = new DateTime(2021, 7, 3), LabId = 1, AccessoryTypeId = 5 },
                new Accessory{ Id = 10, Status = "Borrowed", LastUpdated = new DateTime(2021, 6, 24), LabId = 1, AccessoryTypeId = 5, LabUserId = 4, DueDate = new DateTime(2022, 10, 14) },
                new Accessory{ Id = 11, Status = "Available", LastUpdated = new DateTime(2021, 7, 25), LabId = 1, AccessoryTypeId = 6 },
                new Accessory{ Id = 12, Status = "Available", LastUpdated = new DateTime(2021, 4, 3), LabId = 1, AccessoryTypeId = 6 },
                new Accessory{ Id = 13, Status = "Borrowed", LastUpdated = new DateTime(2021, 7, 19), LabId = 1, AccessoryTypeId = 7, LabUserId = 5, DueDate = new DateTime(2022, 7, 19) },
                new Accessory{ Id = 14, Status = "Borrowed", LastUpdated = new DateTime(2021, 12, 14), LabId = 1, AccessoryTypeId = 7, LabUserId = 6, DueDate = new DateTime(2022, 12, 14) },
                new Accessory{ Id = 15, Status = "Available", LastUpdated = new DateTime(2021, 11, 12), LabId = 1, AccessoryTypeId = 8 },
                new Accessory{ Id = 16, Status = "Available", LastUpdated = new DateTime(2021, 7, 3), LabId = 1, AccessoryTypeId = 8 }
        );
    }

    private static int GetRandomNumber()
    {
        var random = new Random();
        return random.Next(1000, 9999);
    }
}
