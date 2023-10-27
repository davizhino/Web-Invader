using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerHealth : HealthBehaviour
{
   /* private void Start()
    {
        base.Start();
        GameManager.gameManager.setPlayer(this.gameObject);
        actualHealth = GameManager.gameManager.getMaxLife();
    }*/

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger");
        if (other.gameObject.CompareTag("eBullet"))
        {
            
            if (other.gameObject.TryGetComponent<damageBehaviour>(out damageBehaviour damageBehaviour))
            {
                getHit(damageBehaviour.getDamage());
            }
        }
    }


    //private void OnValidate()
    //{
    //    getHit(0);
    //}
    protected override void getHit(int dmg)
    {
        actualHealth = actualHealth - dmg;
        Debug.Log(actualHealth);
        GameManager.gameManager?.checkGame(actualHealth);
    }
}
