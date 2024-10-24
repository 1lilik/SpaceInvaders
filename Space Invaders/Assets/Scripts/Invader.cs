using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(AudioSource))]

public class Invader : MonoBehaviour
{
    public Sprite sadCat;
    [SerializeField] Sprite happyCat; 
    public float stayBeforeDie;
     public bool isDead = false;
    AudioSource audioSource;

    public SpriteRenderer spRend;
    int animationFrame;
    // Start is called before the first frame update

    private void Awake()
    {
        spRend = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Laser"))
        {
            isDead = true;
            spRend.sprite = happyCat;
            Invoke("Die", stayBeforeDie);
            audioSource.Play();
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Boundary")) //nått nedre kanten
        {
            GameManager.Instance.OnBoundaryReached();
        }
    }
    private void Die()
    {
        GameManager.Instance.OnInvaderKilled(this);
        spRend.sprite = sadCat;
    }

}
