﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedLaunch.Classes.Controls.VirtualDevices
{
    public class Nes : VirtualDeviceBase
    {
        public static DeviceDefinition GamePad(int VirtualPort)
        {
            DeviceDefinition device = new DeviceDefinition();
            device.DeviceName = "NES GamePad";
            device.ControllerName = "gamepad";
            device.CommandStart = "nes.input.port" + VirtualPort;
            device.VirtualPort = VirtualPort;
            device.MapList = new List<Mapping>
            {
                /* MapList is now autogenerated from mednafen.cfg */
            };

            DeviceDefinition.ParseOptionsFromConfig(device);

            DeviceDefinition.PopulateConfig(device);
            return device;
        }

        public static DeviceDefinition Zapper(int VirtualPort)
        {
            DeviceDefinition device = new DeviceDefinition();
            device.DeviceName = "NES Zapper";
            device.ControllerName = "zapper";
            device.CommandStart = "nes.input.port" + VirtualPort;
            device.VirtualPort = VirtualPort;
            device.MapList = new List<Mapping>
            {
                /* MapList is now autogenerated from mednafen.cfg */
            };

            DeviceDefinition.ParseOptionsFromConfig(device);

            DeviceDefinition.PopulateConfig(device);
            return device;
        }

        public static DeviceDefinition PowerPadA(int VirtualPort)
        {
            DeviceDefinition device = new DeviceDefinition();
            device.DeviceName = "NES Power Pad Side A";
            device.ControllerName = "powerpada";
            device.CommandStart = "nes.input.port" + VirtualPort;
            device.VirtualPort = VirtualPort;
            device.MapList = new List<Mapping>
            {
                /* MapList is now autogenerated from mednafen.cfg */
            };

            DeviceDefinition.ParseOptionsFromConfig(device);

            DeviceDefinition.PopulateConfig(device);
            return device;
        }

        public static DeviceDefinition PowerPadB(int VirtualPort)
        {
            DeviceDefinition device = new DeviceDefinition();
            device.DeviceName = "NES Power Pad Side B";
            device.ControllerName = "powerpadb";
            device.CommandStart = "nes.input.port" + VirtualPort;
            device.VirtualPort = VirtualPort;
            device.MapList = new List<Mapping>
            {
                /* MapList is now autogenerated from mednafen.cfg */
            };

            DeviceDefinition.ParseOptionsFromConfig(device);

            DeviceDefinition.PopulateConfig(device);
            return device;
        }

        public static DeviceDefinition ArkanoidPaddle(int VirtualPort)
        {
            DeviceDefinition device = new DeviceDefinition();
            device.DeviceName = "NES Arkanoid Paddle";
            device.ControllerName = "arkanoid";
            device.CommandStart = "nes.input.port" + VirtualPort;
            device.VirtualPort = VirtualPort;
            device.MapList = new List<Mapping>
            {
                /* MapList is now autogenerated from mednafen.cfg */
            };

            DeviceDefinition.ParseOptionsFromConfig(device);

            DeviceDefinition.PopulateConfig(device);
            return device;
        }
    }

    
}
