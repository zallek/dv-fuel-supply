using HarmonyLib;
using DV.ThingTypes;
using UnityEngine;

namespace FuelSupply.Patches;

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

		newPricePerUnit = FuelPriceManager.Instance.GetPricePerUnitAtPitStop(pitStopName);
	}
}

[HarmonyPatch(typeof(LocoResourceModule), "Update")]
public static class LocoResourceModule_Update_Patch
{
	public static void Postfix(LocoResourceModule __instance, PitStopStation ___pitStopStation)
	{
		if (__instance.resourceType == ResourceType.Fuel)
		{
			__instance.UpdateResourcePricePerUnit(___pitStopStation.pitstop.CurrentCar, 0);
		}
	}
}
