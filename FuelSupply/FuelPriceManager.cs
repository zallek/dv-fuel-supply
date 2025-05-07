using UnityEngine;
using DV;
using DV.ThingTypes;
using DV.Utils;
using System.Collections.Generic;
using System;

namespace FuelSupply;

/**
 * Manages the price of fuel at different pit stops.
 *
 * - Price is based on the distance from the refinery.
 * - Price fluctuates overtime (using a noisy sinusoidal pattern)
 */
public class FuelPriceManager : SingletonBehaviour<FuelPriceManager> {
	public float timeFluctuationAmplitude = 0.3f; // Maximum price fluctuation from base
	public float timeFluctuationPeriod = 4 * 3600f; // 4 hours in seconds
	public float noiseAmplitude = 0.02f; // Maximum random noise amplitude
	public float updateInterval = 5f; // Update prices every second

	private float lastUpdateTime = 0f;
	private System.Random random = new System.Random();

	private Dictionary<string, float> pitStopsPricePerUnit = new Dictionary<string, float>();

	public new static string AllowAutoCreate()
	{
		return "[FuelPriceManager]";
	}

	protected void Update() {
		float currentTime = Time.time;
		if (currentTime - lastUpdateTime >= updateInterval) {
			lastUpdateTime = currentTime;

			UpdatePitStopsPricePerUnit();
		}
	}

	private void UpdatePitStopsPricePerUnit() {
		foreach (string pitStopName in pitStopsPricePerUnit.Keys) {
			UpdatePitStopPricePerUnit(pitStopName);
		}
	}

	private float UpdatePitStopPricePerUnit(string pitStopName) {
		float pricePerUnit = ResourceTypes.GetFullUnitPriceOfResource(ResourceType.Fuel, null, null, Globals.G.GameParams.ResourcesParams);

		float stationPriceFactor = GetPitStopPriceFactor(pitStopName);

		// Calculate time-based fluctuation. Range: -timeFluctuationAmplitude to +timeFluctuationAmplitude
		float time = Time.time;
		float timeFluctuation = Mathf.Sin(2f * Mathf.PI * time / timeFluctuationPeriod) * timeFluctuationAmplitude;

		// Add random noise. Range: -noiseAmplitude to +noiseAmplitude
		float noise = ((float)random.NextDouble() * 2f - 1f) * noiseAmplitude;

		// Combine all factors
		float finalPricePerUnit = (float)Math.Round(pricePerUnit * (1f + timeFluctuation + noise) * stationPriceFactor, 2); // Round to 2 decimal places

		pitStopsPricePerUnit[pitStopName] = finalPricePerUnit;

		return finalPricePerUnit;
	}

	public float GetPricePerUnitAtPitStop(string pitStopName) {
		if (!pitStopsPricePerUnit.ContainsKey(pitStopName)) {
			UpdatePitStopPricePerUnit(pitStopName);
		}

		return pitStopsPricePerUnit[pitStopName];
	}

	private float GetPitStopPriceFactor(string pitStopName) {
		switch (pitStopName) {
			case "PitstopCoalMineEast":
				return 1.2f;
			case "PitstopCitySouth":
				return 1.2f;
			case "PitstopCityWest":
				return 1.0f;
			case "PitstopFoodFactory":
				return 1.0f;
			case "PitstopGoodsFactory":
				return 1.1f;
			case "PitstopHarbor":
				return 1.1f;
			case "PitstopMachineFactoryTown":
				return 1.0f;
			case "PitstopOilRefinery":
				return 0.8f;
			case "PitstopOilWellCentral":
				return 1.2f;
			case "PitstopOilWellNorth":
				return 1.2f;
			case "PitstopSteelMill":
				return 1.1f;
			default:
				throw new ArgumentException($"Invalid pit stop name: {pitStopName}");
		}
	}
}
