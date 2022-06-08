using UnityEngine;

public class FractureRock : MonoBehaviour
{
	public GameObject originalGameObject;
	public GameObject[] fracturedObject;
	public float originalSpawnDelay;
	public float cogChancePercent = 66.6f;

	private bool isPressed;

	public void SpawnFracturedObject()
	{
		
		if (!isPressed)
		{
			originalGameObject.SetActive(false);
			int randomNumber = Random.Range(0, 3);
			fracturedObject[randomNumber].SetActive(true);
			
			if (Random.Range(0, 100f) <= cogChancePercent)
				EffectSpawner.SpawnDroppedCog(transform.position);

			isPressed = true;
		}
		
		
	}
	
}
