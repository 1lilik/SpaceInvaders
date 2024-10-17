using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]

public class Bunker : MonoBehaviour
{
    int nrOfHits = 0;
    SpriteRenderer spRend;

    AudioSource audioSource;
    private void Awake()
    {
        spRend = GetComponent<SpriteRenderer>();

        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.layer == LayerMask.NameToLayer("Missile") || other.gameObject.layer == LayerMask.NameToLayer("Invader"))
        {

            //Ändrar färgen beroende på antal träffar.
            nrOfHits++;
            Color oldColor = spRend.color;

            Debug.Log("red:" + oldColor.r);
            Debug.Log("green:" + oldColor.g);

            Color newColor = new Color(oldColor.r + (nrOfHits * 0.1f), oldColor.g + (nrOfHits * 0.1f), oldColor.b + (nrOfHits * 0.1f));

            spRend.color = newColor;
            

            if (nrOfHits == 4)
            {
                audioSource.Play();
                Invoke("die", 0.8f);
            }

        }
    }

    public void ResetBunker()
    {
        gameObject.SetActive(true);   
    }

    private void die()
    {
        GameManager.Instance.BunkerDie(this);
    }
}