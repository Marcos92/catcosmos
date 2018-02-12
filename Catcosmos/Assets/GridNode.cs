using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]

public class GridNode : MonoBehaviour 
{
	public bool active
	{
		get 
		{ 
			return active; 
		}
		set 
		{
			//SetActive() just being used for testing purposes
			transform.GetChild(0).gameObject.SetActive(value);
		}
	}

	
}
