
using UnityEngine;
using UnityEngine.Events;


public class Health : MonoBehaviour
{
    public int maxHp = 100;
    public int hp;

    public bool destroyOnDeath = true;

    private void Start()
    {
        hp = maxHp;
    }

    public void Damage(int amount)
    {
        hp -= amount;
        if (hp <= 0) Die();
    }

    public void Die()
    {
        if (destroyOnDeath)
        {
            gameObject.SetActive(false);
        }
    }
}
