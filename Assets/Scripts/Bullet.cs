using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            print("hit" +  collision.gameObject.name + "!");

            CreateBulletImpactEffect(collision);

            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            print("hit" + collision.gameObject.name);

            CreateBulletImpactEffect(collision);

            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Beer"))
        {
            print("hit beer bottle");
            collision.gameObject.GetComponent<Bottle>().Shatter();
            Destroy(gameObject);
            // here we shall not destroy the bullet after collision, coz that might destroy next target and thus pass on destroying other target,
            //the bullet will get destroyed by its own lifespan time
        }
    }
    void CreateBulletImpactEffect(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        GameObject hole = Instantiate(
            GlobalReference.Instance.bulletImpactEffectPrefab,
            contact.point,
            Quaternion.LookRotation(contact.normal)
            );
        hole.transform.SetParent(collision.gameObject.transform);

    }
    private void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
