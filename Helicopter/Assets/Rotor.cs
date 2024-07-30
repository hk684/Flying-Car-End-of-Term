using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotor : MonoBehaviour
{
    public enum Axis {
        x,
        y,
        z
    }

    public Axis rotationAxis;
    // public float bladeSpeed;
    private float bladeSpeed;
    public float BladeSpeed {
        get {
            return bladeSpeed;
        }
        set {
            bladeSpeed = Mathf.Clamp(value, 0, 2000000);
        }
    }
    public bool reverseRotation = false;
    private Vector3 rotationStat;
    private float rotateDegree;

    // Start is called before the first frame update
    void Start()
    {
        rotationStat = transform.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if (reverseRotation == true) {
            rotateDegree = - bladeSpeed * Time.deltaTime;
        } else {
            rotateDegree = bladeSpeed * Time.deltaTime;
        }
        rotateDegree = rotateDegree % 360;
        if (rotationAxis == Axis.x) {
            transform.localRotation = Quaternion.Euler(rotateDegree, rotationStat.y, rotationStat.z);
        } else if (rotationAxis == Axis.y) {
            transform.localRotation = Quaternion.Euler(rotationStat.x, rotateDegree, rotationStat.z);
        } else {
            transform.localRotation = Quaternion.Euler(rotationStat.x, rotationStat.y, rotateDegree);
        }
    }

}
