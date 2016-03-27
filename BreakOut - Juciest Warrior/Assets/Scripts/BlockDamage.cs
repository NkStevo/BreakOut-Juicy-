using UnityEngine;
using System.Collections;

public class BlockDamage : MonoBehaviour {
    private GameObject levelManager;
    private LevelManager level;

    public int blockHealth = 2;

    void Start()
    {
        levelManager = GameObject.FindGameObjectWithTag("LevelManager");
        level = levelManager.GetComponent<LevelManager>();
    }

    public void Damage(int damage)
    {
        blockHealth -= damage;

        if(blockHealth <= 0)
        {
            level.blockNum--;
            Destroy(gameObject);
        }
    }
}
