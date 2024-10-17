using UnityEngine;
using UnityEngine.AI;

public class MovementController : MonoBehaviour
{
    Game Game;

    public CharacterController controller;
    public Transform camera;
    public GameObject playerCamera;
    public AudioSource footstepAudio;
    public WeaponSFX weaponSFX;
    public AudioSource deadAudio;
    public SwordHit swordHit;

    Animator animator;

    public float speed;

    //Used for smoothing turns
    public bool active = false;
    public bool canMove = true;
    public float SmoothTime = 0.3f;

    private float velocity;
    public bool grounded = true;
    public bool moving = false;
    public bool dead = false;
    public float jumpDelay = 0;
    public float attackDelay = 2f;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        animator = GetComponent<Animator>();
    }

    // Animations and footsteps
    void Update()
    {
        if (!active) return;

        // Don't do anything if we're dead now
        if (dead) return;

        float h = Input.GetAxisRaw("Horizontal");
        if (h > 0) h = -h;
        float v = Input.GetAxisRaw("Vertical");

        // Moving?
        if (h == 0f && v == 0f) moving = false;
        else moving = true;

        animator.SetBool("Idle", !moving);
        animator.SetFloat("MovementVertical", (float)((v + h + 0.01) / 2)); // 0.01 to never be stuck in sliding position

        if (grounded)
        {
            // Jump
            if (jumpDelay > 0.3f && Input.GetKeyDown(KeyCode.Space))
            {
                grounded = false;
                animator.SetBool("Busy", true);
                controller.Move(new Vector3(0, 1.5f, 0));
                speed = 5;
            }

            // Attack
            if (attackDelay >= 2.5f)
            {
                canMove = true;

                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    canMove = false;
                    attackDelay = 0;
                    animator.SetInteger("SwordSwing", Random.Range(1, 4));
                    weaponSFX.Play(Random.Range(0, 3));
                    animator.SetBool("Busy", true);
                }
            }
            else
            {
                animator.SetBool("Busy", false);
                animator.SetInteger("SwordSwing", 0);
            }
        }

        jumpDelay += Time.deltaTime;
        attackDelay += Time.deltaTime;

        MoveCharacter();
    }

    private void MoveCharacter()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // Rotate character in direction of camera if we've moving, otherwise allow freelook
        if (canMove)
        {
            if (grounded && moving)
            {
                if (!footstepAudio.isPlaying) footstepAudio.Play();
            }
            else footstepAudio.Stop();

            //We will normalize this direction vector to make sure players don't move faster on diagonals (ex. holding W and A)
            //This will ensure the vector is always length 1
            //https://www.khanacademy.org/computing/computer-programming/programming-natural-simulations/programming-vectors/a/vector-magnitude-normalization
            //https://docs.unity3d.com/ScriptReference/Vector3.Normalize.html
            Vector3 dir = new Vector3(h, 0, v).normalized;

            //Smoothes an angle over time
            //In our case our character's current rotation along y to the camera's y over SmoothTime
            //https://docs.unity3d.com/ScriptReference/Mathf.SmoothDampAngle.html
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, camera.eulerAngles.y, ref velocity, SmoothTime);

            //transform.rotation is a quaternion
            //https://docs.unity3d.com/ScriptReference/Transform-rotation.html
            //Quaternion.Euler returns a rotation (a quaternion) which is rotated x,y,z degrees around their respective axes
            //In our case this will rotate the character around the y-axis to match the smoothed forward direction of the camera
            //https://docs.unity3d.com/ScriptReference/Quaternion.Euler.html
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            //The Character Controller component makes movement very easy, simply tell it the direction you want to move!
            //Multiplying a quaternion with a vector results in a vector that is rotated by the given quaternion
            //In our case we are rotating our cartesian movement dir vector to match our character's rotation
            controller.Move(transform.rotation * dir * speed * Time.deltaTime);
        }
        else footstepAudio.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Stop jumping, grounded
        if (other.tag.Equals("Ground"))
        {
            speed = 1.5f;
            jumpDelay = 0;
            grounded = true;
            animator.SetBool("Busy", false);
        }
    }

    public void Die()
    {
        dead = true;
        animator.SetBool("Idle", false);
        animator.SetFloat("MovementVertical", 0);
        animator.SetBool("Dead", true);
        if (!deadAudio.isPlaying) deadAudio.Play();
        footstepAudio.Stop();
    }

    public void Teleport(Vector3 position)
    {
        controller.enabled = false;
        controller.transform.position = position;
        controller.enabled = true;
    }

    public void SetCamActive(bool active)
    {
        playerCamera.SetActive(active);
    }

    private void HitActive()
    {
        swordHit.canHit = true;
    }

    private void HitInactive()
    {
        swordHit.canHit = false;
    }
}
