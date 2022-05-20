using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FractureRock : MonoBehaviour
{
	public GameObject originalGameObject;
	public GameObject[] fracturedObject;
	public float originalSpawnDelay;

	private bool isPressed;

	void Update()
    {
		if (Input.GetMouseButtonDown (0))
		{
			SpawnFracturedObject();
		}
    }
	public void SpawnFracturedObject()
	{
		
		if (!isPressed)
		{
			originalGameObject.SetActive(false);
			int randomNumber = Random.Range(0, 3);
			fracturedObject[randomNumber].SetActive(true);
			

			isPressed = true;
			print(isPressed);
		}
		
		
	}
	
}
