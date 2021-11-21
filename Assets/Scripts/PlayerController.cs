using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Photon.Realtime;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 0.1f;
    Vector3 velocity;
    public float gravity = -9.8f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;
    public float jumpHeight = 3f;
    float timeSinceStep = 0;

    private AudioSource audioSource;
    Vector3 move;
    [SerializeField] AudioClip footstep1;
    [SerializeField] AudioClip footstep2;
    [SerializeField] AudioClip footstep3;
    [SerializeField] AudioClip footstep4;
    [SerializeField] AudioClip jumpSound;

    public float threshold;





    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        //movement and jump functions
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(((move * speed) / 2) * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            audioSource.PlayOneShot(jumpSound);
            velocity.y = Mathf.Sqrt(jumpHeight * -1f * gravity);
        }
        if (move.x != 0 && isGrounded)
        {
            ProcessFootsteps();
        }

       // if (Input.GetKeyDown(KeyCode.Mouse0))
       // {
         //   Punch();
       // }
    }

   // void Punch()
    //{
      //  this.GetComponent<Rigidbody>().AddForce(Vector3.left * 1000);
   // }

    //footsteps every .4seconds played
    void ProcessFootsteps()
    {

        timeSinceStep += Time.deltaTime;
        if (timeSinceStep > 0.4)
        {
            int randomSound = Random.Range(1, 4);
            switch (randomSound)
            {
                case 1:
                    // print("playedsound1");
                    audioSource.PlayOneShot(footstep1);
                    break;
                case 2:
                    //print("playedsound2");
                    audioSource.PlayOneShot(footstep2);
                    break;
                case 3:
                    // print("playedsound3");
                    audioSource.PlayOneShot(footstep3);
                    break;
                case 4:
                    //  print("playedsound4");
                    audioSource.PlayOneShot(footstep4);
                    break;
            }
            timeSinceStep = 0;
        }
    }

    void FixedUpdate()
    {
        //Respawn if position greater than threshold
        if (transform.position.y < threshold)
        {
            transform.position = new Vector3(0, 10, 0);
        }
    }
}
