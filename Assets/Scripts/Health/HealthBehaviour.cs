using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HealthBehaviour : MonoBehaviour
{
    public int maxHealth = 5;
    [SerializeField]
    protected int actualHealth;
    [SerializeField]
    protected float destroyDelay = 0.3f;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        actualHealth = maxHealth;
    }

    protected virtual void getHit()
    {
        actualHealth--;
        checkLife();
    }

    protected virtual void checkLife()
    {
        if (actualHealth <= 0)
            Destroy(this.gameObject, destroyDelay);
    }

    protected abstract void getHit(int dmg);

    protected virtual void OnTriggerEnter(Collider other)
    {
        getHit();
    }
}
