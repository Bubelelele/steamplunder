using UnityEngine;

public class WeaponTrigger : MonoBehaviour
{
    public GameObject enemy;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && enemy.GetComponent<MeleeBandit>().lethal)
        {
            PlayerData.Damage(enemy.GetComponent<MeleeBandit>().attackDamage);
        }
    }
}
