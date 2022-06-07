using UnityEngine;

public class Explode : MonoBehaviour
{
    public float destroyDelay;
    public float minForce;
    public float maxForce;
    public float radius;

    

    public GameObject Spawn;

    // Start is called before the first frame update
    void Start()
    {
        Explotion(); 
    }

    public void Explotion()
	{
        foreach (Transform t in transform)
		{
            var rb = t.GetComponent<Rigidbody>();

			if (rb != null)
			
                rb.AddExplosionForce(Random.Range(minForce, maxForce), transform.position, radius);
			  
		}
        Destroy(Spawn, destroyDelay);
        
	}
}
