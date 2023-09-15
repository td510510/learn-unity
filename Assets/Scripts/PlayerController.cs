using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float jumpForce;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool gameOver = false;
    private Animator playerAnim;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound;
    public AudioClip collectItemSound;
    public AudioClip crashSound;
    public AudioClip crashBombSound;
    private AudioSource playAudio;
    public bool isIdling = false;
    private GameManager gameManager;
    private BoxCollider groundBox;
    private TextMeshProUGUI scoreText;
    private int score;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playAudio = GetComponent<AudioSource>();
        scoreText = GameObject.Find("Score Text").GetComponent<TextMeshProUGUI>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        groundBox = GameObject.Find("Ground").GetComponent<BoxCollider>();
        Physics.gravity *= gravityModifier;
        score = 0;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //if ((Input.GetKeyDown(KeyCode.Space) || Touchscreen.current.primaryTouch.press.isPressed) && isOnGround && !gameOver && !isIdling)
        //{
        //    playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        //    isOnGround = false;
        //    playerAnim.SetTrigger("Jump_trig");
        //    dirtParticle.Stop();
        //    playAudio.PlayOneShot(jumpSound, 1.0f);
        //}
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();
            isIdling = false;
        } else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Died();
        } else if (collision.gameObject.CompareTag("Box"))
        {
            Idle();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            isIdling = false;
            playerAnim.SetFloat("Speed_f", 1f);
            dirtParticle.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hole"))
        {
            isIdling = true;
            playerAnim.SetFloat("Speed_f", 0.2f);
            dirtParticle.Stop();
            groundBox.isTrigger = true;
            gameOver = true;
            gameManager.GameOver();
            explosionParticle.Play();
            dirtParticle.Play();
            playAudio.PlayOneShot(crashSound, 1.0f);
            return;
        }
        if (other.gameObject.CompareTag("Dimond"))
        {
            score += 2;
            Destroy(other.gameObject);
            playAudio.PlayOneShot(collectItemSound, 1.0f);
        }
        if (other.gameObject.CompareTag("Bomb"))
        {
            score--;
            // Trigger destroy object when position.x < 15
            other.gameObject.transform.position = new Vector3(-16, other.transform.position.y, other.transform.position.z);
            playAudio.PlayOneShot(crashBombSound, 1.0f);
            StartCoroutine(Stuck());
        }
        if (score <= 0)
        {
            score = 0;
            Died();
        }
        scoreText.text = "Score: " + score;
    }

    void Died()
    {
        gameOver = true;
        playerAnim.SetBool("Death_b", true);
        playerAnim.SetInteger("DeathType_int", 1);
        explosionParticle.Play();
        dirtParticle.Play();
        playAudio.PlayOneShot(crashSound, 1.0f);
        gameManager.GameOver();
    }

    void Idle()
    {
        isIdling = true;
        playerAnim.SetFloat("Speed_f", 0.2f);
        dirtParticle.Stop();
    }

    void Run()
    {
        isIdling = false;
        playerAnim.SetFloat("Speed_f", 1f);
        dirtParticle.Play();
    }

    IEnumerator Stuck()
    {
        Idle();
        yield return new WaitForSeconds(1);
        Run();
    }
}
