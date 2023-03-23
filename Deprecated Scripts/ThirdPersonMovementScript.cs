using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonMovementScript : MonoBehaviour
{
    public PlayerControls controls;
    //InputManager inputManager;
    public CharacterController controller;
    public float walkSpeed=6f;
    public float runSpeed=9f;
    public float turnSmoothTime=0.1f;
    public bool rotate=true;
    public Transform cam;
    float turnSmoothVelocity;

    public float gravity=-9.81f;
    public float jumpHeight=3f;
    public float gravityMultiplier=2f;
    
    private int jumps;
    public int maxJumps=2;
    public bool allowRotate=true;
    private float currentSpeed=6f;
    private bool _isShiftPressedDown;
    public float xOrient=1f;
    public Vector3 moveDir;
    private float sprintTime=0;
    private bool sprintStarted=false;
    public bool sprintContinue=false;
    public float stamina=100;

    //Impact stuff
    public float mass=3f;
    Vector3 externalForce=Vector3.zero;
    Vector3 impact = Vector3.zero;
 

    //for gravity/jump physics
    Vector3 velocity;
    bool isGrounded;
    private Vector3 moveDirection;



    private void Awake()
    {
        controls = new PlayerControls();
        controls.Player.Jump.performed += cntxt=>Jump();
        //controls.Player.Dash.performed += cntxt=>Dash(cntxt);
        //controls.Player.Ability1.performed+=cntxt=> Debug.Log(cntxt.ReadValue<float>());
        
    }


    public void  AddImpact(Vector3 dir, float force)
    {
        dir.Normalize();
        //if (dir.y < 0) dir.y = -dir.y; // reflect down force on the ground
        impact = dir.normalized * force / mass;
        externalForce+=impact;
    }


    private void Jump()
    {
        if (jumps>0){

            velocity.y=Mathf.Sqrt(jumpHeight *-1f*gravity);
            jumps-=1;
        }
    }

    private void Sprint()
    {
        currentSpeed=walkSpeed;
        //Debug.Log("asdf");
        if (sprintContinue && moveDir.x!=0f && moveDir.z!=0f)
        {
            currentSpeed=runSpeed;
        }
        else if (controls.Player.Sprint.ReadValue<float>()==1)
        {
            currentSpeed=runSpeed;
            sprintStarted=true;
            //Debug.Log("Started");
        }
        else{
            sprintStarted=false;
            sprintContinue=false;
            sprintTime=0f;
        }

        
    }

    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }

    void Update()
    {
        if (sprintStarted && !sprintContinue)
        {
            sprintTime+=Time.deltaTime;
        }
        if (sprintTime>=1)
        {
            sprintContinue=true;
        }



    }


    public void HandleRotate()
    {
        if (xOrient<0)
        {
            transform.rotation=Quaternion.Euler(0f, 180, 0f);
        }
        else if (xOrient>0)
        {
            transform.rotation=Quaternion.Euler(0f, 0f, 0f);
        }
        
    }


    public void HandleMovement()
    {

        //jumping/gravity
        velocity.y +=gravityMultiplier*gravity * Time.deltaTime;
        
        //isGrounded = Physics.CheckBox(groundCheck.position, new Vector3(1,1,1), Quaternion.Euler(0f, 0f, 0f));
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y<0)
        {
            jumps=maxJumps;
            velocity.y=-5f;
           
        }
        Sprint();

        //Debug.Log("ha");
        float vertical = controls.Player.Movement.ReadValue<Vector2>().y;
        float horizontal = controls.Player.Movement.ReadValue<Vector2>().x;
        xOrient = controls.Player.Movement.ReadValue<Vector2>().x;

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        moveDir=new Vector3();

        Vector3 movementVelocity = moveDirection;

        if (allowRotate==false && false)
            {
                float targetAngle=cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation=Quaternion.Euler(0f, angle, 0f);
            }


        if (direction.magnitude >=0.01f && allowRotate==true)
        {
            float targetAngle=Mathf.Atan2(direction.x, direction.z)*Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            if (rotate==true && allowRotate==true)
                {
                    transform.rotation=Quaternion.Euler(0f, angle, 0f);
                }
            else if (allowRotate==true)
            {
                {
                    transform.rotation=Quaternion.Euler(0f, 0f, 0f);
                }
            }

            
            moveDir=Quaternion.Euler(0f, targetAngle,0f)*Vector3.forward;
            moveDir=moveDir.normalized*currentSpeed;
            //controller.Move(moveDir.normalized*currentSpeed*Time.deltaTime);

        }

        else if (direction.magnitude >=0.1f && allowRotate==false)
        {
            float targetAngle=Mathf.Atan2(direction.x, direction.z)*Mathf.Rad2Deg + cam.eulerAngles.y;

            moveDir=Quaternion.Euler(0f, targetAngle,0f)*Vector3.forward;
            moveDir=moveDir.normalized*currentSpeed;
            //Debug.Log(moveDir);
            //controller.Move(moveDir.normalized*currentSpeed*Time.deltaTime);
        }
        moveDir.y=velocity.y;
        //Debug.Log(moveDir);
        controller.Move((externalForce+moveDir)*Time.deltaTime);


        //reset values
        currentSpeed=walkSpeed;
        externalForce = Vector3.Lerp(externalForce, Vector3.zero, 5*Time.deltaTime); //decrease impact
    }

   // private void OnGUI()
    //{
    //    _isShiftPressedDown = Input.GetKey(KeyCode.LeftShift);
    //}
}
