using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    //private Animator anim;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    private enum MovementState { idle, jumping, falling}
    private bool isGrounded = false;
    private float jumpHeight;
    private GameObject jumpCloud;

    [SerializeField] private AudioSource jumpSoundEffect;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        jumpHeight = LevelGenerator.instance.cloudSpacing;
        //anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
        if (dirX < 0)
            sprite.flipX = true;
        else if (dirX > 0)
            sprite.flipX = false;

        if ((rb.velocity.y < 0) && isGrounded) //falling
        {
            //jumpSoundEffect.Play();
            //Debug.Log("jump: " + (int)Math.Round(transform.position.y / jumpHeight));
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            Destroy(jumpCloud);
        }

        //UpdateAnimationState();
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Cloud"))
        {
            jumpCloud = other.gameObject;
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if ((other.gameObject.CompareTag("Cloud")))
        {
            isGrounded = false;
        }
    }
}