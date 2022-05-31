using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FractureRock : MonoBehaviour
{
	public GameObject originalGameObject;
	public GameObject[] fracturedObject;
	public float originalSpawnDelay;

	private bool isPressed;

	public void SpawnFracturedObject()
	{
		
		if (!isPressed)
		{
			originalGameObject.SetActive(false);
			int randomNumber = Random.Range(0, 3);
			fracturedObject[randomNumber].SetActive(true);
			EffectSpawner.SpawnDroppedCog(transform.position);

			isPressed = true;
		}
		
		
	}
	
}
