using HarmonyLib;
using UnityEngine;
using DV.ThingTypes;
namespace FuelSupply.Patches
{
    [HarmonyPatch(typeof(LocoResourceModule), nameof(LocoResourceModule.UpdateResourcePricePerUnit))]
    public static class LocoResourceModule_UpdateResourcePricePerUnit_Patch
    {
        public static void Prefix(LocoResourceModule __instance, TrainCar trainCar, ref float newPricePerUnit)
        {
			ResourceType resourceType = __instance.resourceType;
			if (resourceType != ResourceType.Fuel)
			{
				return;
			}

			string pitStopName = __instance.transform.parent.parent.name;
			float stationPriceFactor = 1.0f;
			switch (pitStopName)
			{
				case "PitstopCoalMineEast":
					stationPriceFactor = 1.2f;
					break;
				case "PitstopCitySouth":
					stationPriceFactor = 1.2f;
					break;
				case "PitstopCityWest":
					stationPriceFactor = 1.0f;
					break;
				case "PitstopFoodFactory":
					stationPriceFactor = 1.0f;
					break;
				case "PitstopGoodsFactory":
					stationPriceFactor = 1.1f;
					break;
				case "PitstopHarbor":
					stationPriceFactor = 1.1f;
					break;
				case "PitstopMachineFactoryTown":
					stationPriceFactor = 1.0f;
					break;
				case "PitstopOilRefinery":
					stationPriceFactor = 0.8f;
					break;
				case "PitstopOilWellCentral":
					stationPriceFactor = 1.2f;
					break;
				case "PitstopOilWellNorth":
					stationPriceFactor = 1.2f;
					break;
				case "PitstopSteelMill":
					stationPriceFactor = 1.1f;
					break;
			}

			newPricePerUnit = newPricePerUnit * stationPriceFactor;
        }
    }
}
