using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator), typeof(Rigidbody), typeof(CapsuleCollider))]
[RequireComponent(typeof(PlayerController))]

public class PlayerControlScript : MonoBehaviour
{
    public Animator anim;
    private Rigidbody rbody;
    public GameObject tofuRecipe;
    private PlayerController pinput;
    public bool canReceiveInput;
    public bool inputReceived;
    public float iFrameTotal;
    public float iFrameCounter;
    public int hp;

    // Sounds
    public AudioClip grunt;
    public AudioClip roll;
    public AudioClip weaponSound;
    public AudioClip spellCast;
    private AudioSource audio;
    // Public Dust
    public ParticleSystem dust; 

    // For inputType, 1 = attack, 2 = special, 3 = roll
    public int inputType;

    public static PlayerControlScript instance;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody>();
        pinput = GetComponent<PlayerController>();
        audio = GetComponent<AudioSource>();
        canReceiveInput = true;
        inputReceived = false;
        inputType = 0;
        iFrameTotal = 1.7f;
        iFrameCounter = 0f;

    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("isActing", false);

        if (anim.GetBool("iFrames"))
        {
            iFrameCounter += Time.deltaTime;
            if (iFrameCounter >= iFrameTotal)
            {
                anim.SetBool("iFrames", false);
                iFrameCounter = 0f;
            }
        }

        if (pinput._input != Vector3.zero)
        {
            anim.SetBool("isMoving", true);
            anim.SetBool("isActing", true);
        } else
        {
            anim.SetBool("isMoving", false);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }

        if (Input.GetButtonDown("Fire2"))
        {
            Cast();
        }

        if (Input.GetButtonDown("Dodge") && pinput._input != Vector3.zero)
        {
            Dodge();
        }

        if (Input.GetButtonDown("SwitchRecipe"))
        {
            Debug.Log("Equipped Recipe Changed");
            if (RecipeController.instance.activeRecipeSlot == 2) 
            {
                RecipeController.instance.activeRecipeSlot = 0;
            } else
            {
                RecipeController.instance.activeRecipeSlot++;
            }
        }
    } 

    public void Attack()
    {
        if (canReceiveInput == true && !tofuRecipe.GetComponent<TofuRecipe>().isActive)
        {
                inputReceived = true;
                canReceiveInput = false;
                inputType = 1;
                anim.SetBool("isActing", true);           
        }
        else
        {
            return;
        }
    }

    public void Cast()
    {
        if (canReceiveInput == true && !tofuRecipe.GetComponent<TofuRecipe>().isActive)
        {
            inputReceived = true;
            canReceiveInput = false;
            inputType = 2;
            anim.SetBool("isActing", true);
            audio.PlayOneShot(spellCast, 2.0f);

        }
        else
        {
            return;
        }
    }

    public void Dodge()
    {
        if (canReceiveInput == true && !tofuRecipe.GetComponent<TofuRecipe>().isActive)
        {
            inputReceived = true;
            canReceiveInput = false;
            inputType = 3;
            anim.SetBool("isActing", true);
            audio.clip = roll;
            audio.Play();
            CreateDust();
        }
        else
        {
            return;
        }
    }


    public void InputManager()
    {
        if (!canReceiveInput)
        {
            canReceiveInput = true;
        } else
        {
            canReceiveInput = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {

            if (!anim.GetBool("iFrames") && hp >= 1 && !tofuRecipe.GetComponent<TofuRecipe>().isActive) { 

                anim.SetBool("isHit", true);
                audio.clip = grunt;
                audio.Play();

            }

            if (hp == 0)
            {
                //audioSource.PlayOneShot(grunt);
            }

        }
    }


    void CreateDust()
    {
        dust.Play();
    }

    void Slash()
    {
        audio.PlayOneShot(weaponSound, 1.0f);
    }
}
