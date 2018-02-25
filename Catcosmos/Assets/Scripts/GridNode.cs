using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]

public class GridNode : MonoBehaviour 
{
	public Plant plant;

	public bool active
	{
		get 
		{ 
			return active; 
		}
		set 
		{
			//SetActive() just being used for testing purposes
			//Implement highlighting active node later
			//transform.GetChild(0).gameObject.SetActive(value);
		}
	}
}
