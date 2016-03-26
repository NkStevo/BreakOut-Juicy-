using UnityEngine;
using System.Collections;

public class BlockDamage : MonoBehaviour {
    public int blockHealth = 2;

    public void Damage(int damage)
    {
        blockHealth -= damage;
        Debug.Log(blockHealth);

        if(blockHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
