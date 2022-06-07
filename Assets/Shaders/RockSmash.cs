using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSmash : MonoBehaviour
{
	public GameObject originalGameObject;
	public GameObject fracturedObject;
	public float originalSpawnDelay;

	private bool isPressed;
	
	public void SpawnFracturedObject()
	{

		if (!isPressed)
		{
			originalGameObject.SetActive(false);
			
			fracturedObject.SetActive(true);


			isPressed = true;
		}


	}

}