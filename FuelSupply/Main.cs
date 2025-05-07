using System;
using System.Reflection;
using HarmonyLib;
using UnityModManagerNet;
using UnityEngine;

namespace FuelSupply;

#if DEBUG
    [EnableReloading]
#endif
public static class FuelSupply
{
	// Unity Mod Manage Wiki: https://wiki.nexusmods.com/index.php/Category:Unity_Mod_Manager
	private static bool Load(UnityModManager.ModEntry modEntry)
	{
		Harmony? harmony = null;

		try
		{
			harmony = new Harmony(modEntry.Info.Id);
			harmony.PatchAll(Assembly.GetExecutingAssembly());
		}
		catch (Exception ex)
		{
			modEntry.Logger.LogException($"Failed to load {modEntry.Info.DisplayName}:", ex);
			harmony?.UnpatchAll(modEntry.Info.Id);
			return false;
		}

		modEntry.OnUnload = Unload;
		return true;
	}

	private static bool Unload(UnityModManager.ModEntry modEntry)
	{
		var harmony = new Harmony(modEntry.Info.Id);
		harmony.UnpatchAll(modEntry.Info.Id);

		return true;
	}
}
