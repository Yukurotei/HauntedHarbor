using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int amount;
    public ItemType type;

    public enum ItemType
    {
        health,
        coin
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            switch(type)
            {
                case ItemType.health:
                    BY.Sprite p = BY.Sprite.PLAYER_INSTANCE;
                    p.currentHealth = p.currentHealth + amount <= p.stats.maxHealth ? p.currentHealth += amount : p.currentHealth = p.stats.maxHealth;
                    break;
                case ItemType.coin:
                    UI.INSTANCE.coins += amount; //UIController
                    break;
            }
            Destroy(gameObject);
        }
    }
}
