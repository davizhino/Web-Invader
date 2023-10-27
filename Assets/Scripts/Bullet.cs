using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : damageBehaviour
{
    public float lifeTime;
    public float speedMultiplier;
    private Vector3 velocity;

    public void SetDirection(Vector3 direction)
    {
        velocity = direction * speedMultiplier;
    }



    private void Start()
    {
        Debug.Log("Bala instanciada");
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(this.gameObject);
        }
        transform.position += velocity * Time.deltaTime;

    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject, 0.03f);
    }


}
