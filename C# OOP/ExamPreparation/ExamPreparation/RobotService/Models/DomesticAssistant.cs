﻿namespace RobotService.Models
{
    public class DomesticAssistant : Robot
    {
        private const int DomesticAssistantBatteryCapacity = 20000;
        private const int DomesticAssistantConvertionCapacityIndex = 2000;
        public DomesticAssistant(string model)
            : base(model, DomesticAssistantBatteryCapacity, DomesticAssistantConvertionCapacityIndex) { }
    }
}
