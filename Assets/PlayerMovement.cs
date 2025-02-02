using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] TMP_Text speedometer;
    internal Rigidbody characterBody;
    Vector2 moveInput;
    Vector3 moveDirection;
    internal Vector3 velocity;
    internal Vector3 velocityLastFrame;

    [SerializeField] LayerMask whatIsGround;

    internal bool isGrounded => CheckIsGrounded();

    [Header("Player Move Values")]
    [SerializeField]
    [Tooltip("Adjusts the player's max speed in meters/second")]
    private float maxSpeed; 

    [SerializeField]
    private float maxAirSpeed;
    [SerializeField]
    private float maxGroundSpeed;
    [SerializeField]
    private float airStrafeForce;

    //Handle the cap on the maxium acceleration
    float maxAccel => maxGroundSpeed * 2;

    [Space(10)]
    [SerializeField]
    [Tooltip("Readonly variable. Speed in m/s")]
    private float playerCurrentSpeed;
    public float movementSpeedMultiplier;

    //internal bool isGrounded => character.isGrounded;
    public event Action OnBeforeMove;

    PlayerInput playerInput;
        InputAction moveAction;
        InputAction lookAction;
        InputAction sprintAction;
        InputAction jumpAction;
        InputAction interactAction;

    void Awake()
    {
        characterBody = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
    }
    void OnEnable()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        playerCurrentSpeed = Mathf.Round(characterBody.velocity.magnitude);
        if(playerCurrentSpeed <= 1)
        {
            playerCurrentSpeed = 0;
        }

        speedometer.text = playerCurrentSpeed.ToString("#### km/s");
    }

    void FixedUpdate()
    {
        //UpdateGravity();
        //UpdateMovement();
        if(isGrounded)
        {
          // velocity.y = -1;
        }

        MovePlayer();

    }

    //rewrite the character controller to be based on rigidbody forces

    void MovePlayer()
    {
        OnBeforeMove?.Invoke();
        var input = GetMovementInput();
        velocity = input * maxGroundSpeed;
        characterBody.velocity = new Vector3(velocity.x, characterBody.velocity.y, velocity.z);
    }

    Vector3 GetMovementInput()
    {    
        var moveInput = moveAction.ReadValue<Vector2>();
        //If the player is moving, enable the footsteps

        //Get the player input
        var input = new Vector3();
        input += transform.forward * moveInput.y;
        input += transform.right * moveInput.x;
        input = Vector3.ClampMagnitude(input, 1f);
        return input;
    }
    /*
    void UpdateGravity()
    {
        var gravity =  characterBody.mass * Time.fixedDeltaTime * Physics.gravity;


        if(!isGrounded)
        {
           characterBody.AddForce(Physics.gravity, ForceMode.Force);
        }
        //velocity.y = isGrounded ? - 1f : velocity.y += gravity.y;
    }
    
    void UpdateMovement()
    {
        movementSpeedMultiplier = 1f;
        //Check to see if anything should happen before moving
        OnBeforeMove?.Invoke();
        var input = GetMovementInput();
        


        if(isGrounded && velocity.y <= 0)
        {
            velocity = ChangeGroundVel(input, velocity, Time.fixedDeltaTime);

           
            
        }
        else
        {
            velocity = ChangeAirVel(input, velocity, Time.fixedDeltaTime);
            //characterBody.useGravity = true;
            
            
            
        }
        velocity = new Vector3(velocity.x, characterBody.velocity.y, velocity.z);
        characterBody.velocity = velocity;
        characterBody.velocity = Vector3.ClampMagnitude(characterBody.velocity, maxSpeed);



    }

#region Vector Functions for input and handling movement changes due to physics

    
    Vector3 Friction(Vector3 velocity, float currentFrame)
    {
        float c = 0.0001f; // The coefficient of friction for surfaces.

        Vector3 friction = velocity;
        friction.Normalize();
        friction *= -c * currentFrame;
        //friction *= currentFrame;
        return friction;
        // -1 * Mue (= to 1 and is constant) * magnitude of normal force * velocity.normalized * Time.deltaTime
        
    }

    public Vector3 ChangeGroundVel(Vector3 desireDir, Vector3 velocity, float currentFrame)
    {
        velocity = Friction(velocity, currentFrame);
        float currentSpeed = Vector3.Dot(velocity, desireDir);
        float add_speed = Mathf.Clamp(maxGroundSpeed - currentSpeed, 0, maxAccel * currentFrame);

        return velocity + add_speed * desireDir;
    }

    public Vector3 ChangeAirVel(Vector3 desireDir, Vector3 velocity, float currentFrame)
    {   

        //Project the desired movement onto the current velocity vector
        Vector3 projectedVelocity = Vector3.Project(characterBody.velocity, desireDir);
        //Check to see if the movement is toward or away from the projected velocity
        bool isAway = Vector3.Dot(desireDir, projectedVelocity) <= 0f;

        Vector3 strafeForce = Vector3.zero;

        //Only change the velocity if moving away from velocity OR the velocity is below the max air speed
        if(projectedVelocity.magnitude < maxAirSpeed || isAway)
        {
            //Calculate the ideal movement 

            strafeForce = desireDir * airStrafeForce;

            //Set the cap for the speed 
            if(!isAway)
            {
                strafeForce = Vector3.ClampMagnitude(strafeForce, maxAirSpeed - projectedVelocity.magnitude);
            }
            else
            {
                strafeForce = Vector3.ClampMagnitude(strafeForce, maxAirSpeed + projectedVelocity.magnitude);
            }


        }

        return velocity + strafeForce;
    }
    #endregion
    */
    bool CheckIsGrounded()
    {
        return Physics.Raycast(characterBody.position, Vector3.down, 1.05f, whatIsGround);
    }

    void OnCollisionEnter(Collision collision)
    {

        Debug.Log("Contact!");
        foreach(ContactPoint contact in collision.contacts)
        {
            string tag = collision.collider.tag;


            switch(tag)
            {
                case "Bouncy":
                    Debug.Log("Hit bouncy surface. Player velocity: " + collision.relativeVelocity);
                    Vector3 bounce = Vector3.Reflect(collision.relativeVelocity,contact.normal);
                    //characterBody.AddForce(bounce, ForceMode.VelocityChange);
                    //SendMessage("OnBounce", bounce);
                break;
            }
        }

    }
}
