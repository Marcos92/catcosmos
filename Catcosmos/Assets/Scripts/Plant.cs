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

	private bool canGiveFruit = false;
	private float timeToGiveFruit = 5f;
	private float nextFruitTime = 0f;
	[HideInInspector]
	public int currentFruits = 0; //Replace later with array of Fruit instances
	private int maxFruits = 5;

	void Update () 
	{
		age += Time.deltaTime;

		//Grow
		if(growthStage < maxGrowthStage && age >= growthTransitions[growthStage])
		{
			growthStage++;
			transform.localScale *= 1.5f; //Replace later with tree growing animation

			if(growthStage == maxGrowthStage)
			{
				canGiveFruit = true;
				SetNextFruitTime();
			}
		}

		//Give fruit
		if(canGiveFruit && nextFruitTime <= age && currentFruits < maxFruits)
		{
			currentFruits++; //Instantiate fruit
			SetNextFruitTime();
			Debug.Log("Give fruit! The tree has now " + currentFruits + " fruits!");
		}
	}

	private void SetNextFruitTime()
	{
		nextFruitTime = age + timeToGiveFruit * Random.Range(0.75f, 1.25f);
	}

	public void CollectFruit()
	{
		if(currentFruits == maxFruits)
		{
			SetNextFruitTime();
		}
		
		currentFruits--;
	}
}
