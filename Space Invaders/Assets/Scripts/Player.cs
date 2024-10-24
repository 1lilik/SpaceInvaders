using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Player : MonoBehaviour
{
    SpriteRenderer spRend;
    public Sprite idle;
    public Sprite throwPancake;
    public float timeBeforeIdleAnimation = 0;
    public Laser laserPrefab;
    Laser laser;
    float speed = 5f;


    public AudioClip pankaka;
    public AudioClip playerkilled;
    AudioSource audioSource;
    private void Start()
    {
        spRend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Vector3 position = transform.position;
        

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            position.x -= speed * Time.deltaTime;
            transform.localScale = new Vector3(-5, 5, 1);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            position.x += speed * Time.deltaTime;
            transform.localScale = new Vector3(5, 5, 1);
        }

        transform.position = position;

        if (Input.GetKeyDown(KeyCode.Space) && laser == null)
        {
            laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);

            
            spRend.sprite = throwPancake;
            audioSource.PlayOneShot(pankaka, 0.7f);
            Invoke("IdleAnimation", timeBeforeIdleAnimation);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Missile") || collision.gameObject.layer == LayerMask.NameToLayer("Invader"))
        {

            Invoke("Playerdie", 1.2f);
            audioSource.PlayOneShot(playerkilled, 1f);
        }
    }
    
    private void IdleAnimation()
    {
        spRend.sprite = idle;
    }

    public void Playerdie()
    {  
        GameManager.Instance.OnPlayerKilled(this);
    }
}
