using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    Rigidbody helicopterRigid;
    public Rotor MainBlade;
    public Rotor SubBlade;

    private float enginePower;
    public float EnginePower {
        get {
            return enginePower;
        }
        set {
            MainBlade.BladeSpeed = value * 250;
            SubBlade.BladeSpeed = value * 500;
            enginePower = value;
        }
    }

    public float effectiveHeight;
    public float EngineLift = 0.00075f;

    public float ForwardForce;
    public float BackwardForce;

    public LayerMask groundLayer;
    private float distanceToground;
    public bool isOnGround = true;

    private Vector2 movement = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        helicopterRigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // HandleGroundCheck();
        HandleInputs();
    }

    protected void FixedUpdate() {
        HelicopterHover();
        // HelicopterMovements();
    }

    void HandleInputs() {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        if (Input.GetKey(KeyCode.DownArrow)) {
            EnginePower -= EngineLift;
        }
        if (EnginePower < 0) {
            EnginePower = 0;
        }
        if (Input.GetKey(KeyCode.UpArrow)) {
            EnginePower += EngineLift;
        }
        /*
        if (!isOnGround) {
            movement.x = Input.GetAxis("Horizontal");
            movement.y = Input.GetAxis("Vertical");
            if (Input.GetKey(KeyCode.DownArrow)) {
                EnginePower -= EngineLift;
            }
            if (EnginePower < 0) {
                EnginePower = 0;
            }
        }
        if (Input.GetKey(KeyCode.UpArrow)) {
                EnginePower += EngineLift;
        }
        */
    }
    void HelicopterHover() {
        float upForce = 1 - Mathf.Clamp(helicopterRigid.transform.position.y / effectiveHeight, 0, 1);
        upForce = Mathf.Lerp(0, EnginePower, upForce) * helicopterRigid.mass;
        helicopterRigid.AddRelativeForce(Vector3.up * upForce);
    }

    /*
    void HelicopterMovements() {
        if(Input.GetAxis("Vertical") > 0) {
            helicopterRigid.AddRelativeForce(Vector3.forward * Mathf.Max(0f,movement.y * ForwardForce * helicopterRigid.mass));
        } else if (Input.GetAxis("Vertical") < 0) {
            helicopterRigid.AddRelativeForce(Vector3.back * Mathf.Max(0f,-movement.y * BackwardForce * helicopterRigid.mass));
        }
    }
    */

    /*
    void HandleGroundCheck() {
        RaycastHit hit;
        Vector3 direction = transform.TransformDirection(Vector3.down);
        Ray ray = new Ray(transform.position, direction);

        if (Physics.Raycast(ray, out hit, 3000, groundLayer)) {
            distanceToground = hit.distance;
            if (distanceToground < 2) {
                isOnGround = true;
            } else {
                isOnGround = false;
            }
        }
    }
    */

}


