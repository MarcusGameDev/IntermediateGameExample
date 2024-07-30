using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileLife = 3f;
    public int damageAmount = 1;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, projectileLife);
    }

    private void OnCollisionEnter(Collision collision)
    {
        TargetHealth targetHit = collision.gameObject.GetComponent<TargetHealth>();
        if (targetHit != null)
        {
            targetHit.Damage(damageAmount);
        }

        Destroy(gameObject);   
    }
}
