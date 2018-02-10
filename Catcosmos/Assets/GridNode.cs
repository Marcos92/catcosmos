using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridNode : MonoBehaviour {

	public bool active
	{
		get 
		{ 
			return active; 
		}
		set 
		{ 
			//active = value; 

			transform.GetChild(0).gameObject.SetActive(value);
		}
	}
}
