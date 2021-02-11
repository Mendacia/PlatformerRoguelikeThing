using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform[] bgs;
    private float[] parallaxScales;
    public float smoothing = 1;

    private Transform cam;
    private Vector3 previousCamPos;

    private void Awake()
    {
        cam = Camera.main.transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        previousCamPos = cam.position;

        parallaxScales = new float[bgs.Length];

        for (int i = 0; i < bgs.Length; i++)
        {
            parallaxScales[i] = bgs[i].position.z * -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < bgs.Length; i++)
        {
            float parallaxX = (previousCamPos.x - cam.position.x) * parallaxScales[i];
            float parallaxY = (previousCamPos.y - cam.position.y) * parallaxScales[i];

            float backGroundTargetPosX = bgs[i].position.x + parallaxX;
            float backGroundTargetPosY = bgs[i].position.x + parallaxY;

            Vector3 backgroundTargetPos = new Vector3(backGroundTargetPosX, backGroundTargetPosY, bgs[i].position.z);

            bgs[i].position = Vector3.Lerp(bgs[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        previousCamPos = cam.position;
    }
}
