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

	private float lifespan = 15f;
	private float deathTime;
	private bool alive = true;

	void Awake()
	{
		deathTime = lifespan;
	}

	void Update () 
	{
		age += Time.deltaTime;

		if(alive)
		{
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

			//Death
			if(age >= deathTime)
			{
				Debug.Log("The tree is dead! :(");
				transform.localScale *= -1; //Replace by death animation
				alive = false;
			}
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

		Debug.Log("Collect fruit! The tree has now " + currentFruits + " fruits!");
	}

	public void Water()
	{
		deathTime = age + lifespan;
		Debug.Log("Water tree!");
	}
}
