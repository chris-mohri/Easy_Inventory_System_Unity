using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class TopDownCharacterController : MonoBehaviour
    {
        public float walkSpeed;
        public float runSpeed;
        //public float sprintSpeed;
        private float currentSpeed;
        private float sprintTime=0;
        private bool sprintStarted=false;
        public bool sprinting=false;
        public float stamina=100;
        public bool allowSprintContinue;

        private Animator animator;

        Vector2 dir = Vector2.zero;

        public PlayerControls controls;
        //InputManager inputManager;
        //public CharacterController controller;

        public Vector2 currentDirection;
        private bool stopPlayerMovementInput = false;
        private Vector2 impact;


        //Attributes
        public float mass;

        public void addImpact(Vector2 force)
        {
            impact=force;
            stopPlayerMovementInput=true;
        }

        public void addForce(Vector2 force)
        {
            impact=force;
            //stopPlayerMovementInput=true;
        }




        private void Start()
            {
                animator = GetComponent<Animator>();
            }

        private void Awake()
        {
            controls = new PlayerControls();
            //controls.Player.Jump.performed += cntxt=>Jump();
            
        }

        private void Sprint()
        {
            currentSpeed=walkSpeed;
            //Debug.Log("asdf");
            if (sprinting && (dir.x!=0f || dir.y!=0f))
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
                sprinting=false;
                sprintTime=0f;
            }


            if (allowSprintContinue)
                if (sprintStarted && !sprinting)
                {
                    sprintTime+=Time.deltaTime;
                }
                if (sprintTime>=0.5)
                {
                    sprinting=true;
                }

            
        }


        private void Update()
        {
            
            float vertical = controls.Player.Movement.ReadValue<Vector2>().y;
            float horizontal = controls.Player.Movement.ReadValue<Vector2>().x;
            dir = Vector2.zero;

            if (horizontal==-1)
            {
                //dir.x = -1;
                animator.SetInteger("Direction", 3);
            }
            else if (horizontal==1)
            {
                //dir.x = 1;
                animator.SetInteger("Direction", 2);
            }

            if (vertical==1)
            {
                //dir.y = 1;
                animator.SetInteger("Direction", 1);
            }
            else if (vertical==-1)
            {
                //dir.y = -1;
                animator.SetInteger("Direction", 0);
            }

            dir = new Vector2(horizontal, vertical);

            // sprint mechanics
            Sprint();
            
            
            //Debug.Log("Horiz: " + horizontal);

            dir.Normalize();
            currentDirection = dir;

            animator.SetBool("IsMoving", dir.magnitude > 0);


            if (!stopPlayerMovementInput)
            {
                GetComponent<Rigidbody2D>().velocity = (currentSpeed * dir) + impact;
            }

            if (impact != Vector2.zero && stopPlayerMovementInput)
            {
                GetComponent<Rigidbody2D>().velocity = impact;
               
            }

            //reset exterior force impact
            currentSpeed=walkSpeed;
            impact = Vector2.Lerp(impact, Vector2.zero, 5*Time.deltaTime);
            if (stopPlayerMovementInput && impact.x<=0.1 || impact.y<=0.1)
                    stopPlayerMovementInput=false;

           // Debug.Log(impact);

        }

    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }

    
}
