using HarmonyLib;

namespace FuelSupply.Patches
{
    [HarmonyPatch(typeof(LocoResourceModule), nameof(LocoResourceModule.UpdateResourcePricePerUnit))]
    public static class LocoResourceModule_UpdateResourcePricePerUnit_Patch
    {
        public static void Prefix(TrainCar trainCar, ref float newPricePerUnit)
        {
			newPricePerUnit = 5;
        }
    }
}
