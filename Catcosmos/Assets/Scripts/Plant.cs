using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour 
{
	//[HideInInspector]
	public float age = 0f;
	
	public int growthStage = 0;

	private int maxGrowthStage = 2;

	private float[] growthTransitions = {5f, 10f, 15f};
	
	void Update () 
	{
		if(growthStage < maxGrowthStage)
		{
			age += Time.deltaTime;

			if(age >= growthTransitions[growthStage])
			{
				growthStage++;
				transform.localScale *= 1.5f; //Replace later with tree growing animation
			}
		}
	}
}
