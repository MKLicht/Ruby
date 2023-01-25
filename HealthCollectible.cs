using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public AudioClip collectedClip;
    public ParticleSystem starEffect;

    void OnTriggerEnter2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController>();
        if (controller != null)
        {
            if (controller.health < controller.maxHealth)
            {
                controller.changeHealth(1);
                ParticleSystem star = Instantiate(starEffect);
                star.transform.position = this.transform.position;
                star.Play();
                Destroy(gameObject);
                controller.PlaySound(collectedClip);
            }
        }
    }
}
