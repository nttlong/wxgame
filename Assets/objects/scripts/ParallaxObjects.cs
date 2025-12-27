using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxObject : MonoBehaviour
{
    // Start is called before the first frame update
    public float parallaxStrength = 0.2f; // 0 = đứng yên, 1 = chạy theo camera

    private Transform cam;
    private Vector3 lastCamPos;

    void Start()
    {
        cam = Camera.main.transform;
        lastCamPos = cam.position;
    }

    void LateUpdate()
    {
        Vector3 delta = cam.position - lastCamPos;
        transform.position += new Vector3(delta.x * parallaxStrength, delta.y * parallaxStrength, 0);
        lastCamPos = cam.position;
    }
}
