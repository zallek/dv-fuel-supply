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
			float priceFactor = 1.0f;
			switch (pitStopName)
			{
				case "PitstopCoalMineEast":
					priceFactor = 1.0f;
					break;
				case "PitstopCitySouth":
					priceFactor = 1.0f;
					break;
				case "PitstopCityWest":
					priceFactor = 1.0f;
					break;
				case "PitstopFoodFactory":
					priceFactor = 1.0f;
					break;
				case "PitstopGoodsFactory":
					priceFactor = 1.0f;
					break;
				case "PitstopHarbor":
					priceFactor = 1.0f;
					break;
				case "PitstopMachineFactoryTown":
					priceFactor = 1.0f;
					break;
				case "PitstopOilRefinery":
					priceFactor = 1.0f;
					break;
				case "PitstopOilWellCentral":
					priceFactor = 1.0f;
					break;
				case "PitstopOilWellNorth":
					priceFactor = 1.0f;
					break;
				case "PitstopSteelMill":
					priceFactor = 2.0f;
					break;
			}

			newPricePerUnit = newPricePerUnit * priceFactor;
        }
    }
}
