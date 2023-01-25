using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float velocity = 3.0f;
    Rigidbody2D rigidbody2d;
    public bool vertical;
    public float orientTimer = 3.0f;
    float currentTimer;
    Animator animator;

    public ParticleSystem smokeEffect;

    int direction = 1;
    bool broken = true;

    AudioSource audioSource;
    public AudioClip CollisdeClip;
    public AudioClip HitClip;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        currentTimer = orientTimer;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!broken)
        {
            return;
        }
        currentTimer -= Time.deltaTime;
        if (currentTimer < 0)
        {
            direction = -direction;
            currentTimer = orientTimer;
        }
    }

    void FixedUpdate()
    {
        if (!broken)
        {
            return;
        }
        Vector2 position = rigidbody2d.position;
        if (vertical)
        {
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
            position.y += Time.deltaTime * velocity * direction;
        }
        else
        {
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
            position.x += Time.deltaTime * velocity * direction;
        }
        rigidbody2d.MovePosition(position);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();
        if (player != null)
        {
            player.changeHealth(-1);
            player.PlaySound(CollisdeClip);
        }
    }

    public void Fix()
    {
        PlaySound(HitClip);
        broken = false;
        rigidbody2d.simulated = false;
        smokeEffect.Stop();
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
