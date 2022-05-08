using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthScript : MonoBehaviour
{

    [SerializeField]
    private int StartingHealth;

    [SerializeField]
    private int MaxHealth;

    private int CurrentHealth;


    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = StartingHealth;
    }

    public void ApplyDamage(int damage)
    {
        Debug.Log("Player Damaged!" + damage);
        CurrentHealth -= damage;
        if(CurrentHealth <=0)
        {
            GameStateManager.LoseALife();
        }
    }
   
}
