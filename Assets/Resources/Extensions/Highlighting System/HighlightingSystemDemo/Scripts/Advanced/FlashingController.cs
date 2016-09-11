using UnityEngine;
using System.Collections;

public class FlashingController : HighlighterController
{
	public Color flashingStartColor = Color.blue;
	public Color flashingEndColor = Color.cyan;
	public float flashingDelay = 2.5f;
	public float flashingFrequency = 2f;
	private EnergyBar eBar;
	public int triggerEnergyValue;

	// 
	protected override void Start()
	{
		base.Start();
		eBar = GameObject.FindGameObjectWithTag("energyBar").GetComponent<EnergyBar>();
		//StartCoroutine(DelayFlashing());
	}

	private void Update() {
		if(eBar.valueCurrent <= triggerEnergyValue) {
			h.FlashingOn(flashingStartColor, flashingEndColor, flashingFrequency);
		} else {
			h.FlashingOff();
		}
	}

	// 
	protected IEnumerator DelayFlashing()
	{
		yield return new WaitForSeconds(flashingDelay);
		
		// Start object flashing after delay
		h.FlashingOn(flashingStartColor, flashingEndColor, flashingFrequency);
	}
}
