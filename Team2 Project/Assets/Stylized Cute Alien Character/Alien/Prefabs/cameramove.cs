using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameramove : MonoBehaviour
{
    public Transform objectTofollow;
    public float followspeed = 10f;
    public float sensitivity = 100f;
    public float clampAngle = 70f;
    private float rotx;
    private float roty;

    public Transform realCamerea;
    public Vector3 dirNormalized;
    public Vector3 finalDir;
    public float minDistance;
    public float maxDistance;
    public float finalDistance;
    public float smoothness = 10f;
    // Start is called before the first frame update
    void Start()
    {
        rotx = transform.localRotation.eulerAngles.x;
        roty = transform.localRotation.eulerAngles.y;

        dirNormalized = realCamerea.localPosition.normalized;
        finalDistance = realCamerea.localPosition.magnitude;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        rotx += -(Input.GetAxis("Mouse Y")) * sensitivity * Time.deltaTime;
        roty += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;

        rotx = Mathf.Clamp(rotx, -clampAngle, clampAngle);
        Quaternion rot = Quaternion.Euler(rotx, roty, 0);
        transform.rotation = rot;
    }
    void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, objectTofollow.position, followspeed * Time.deltaTime);
        finalDir = transform.TransformPoint(dirNormalized * maxDistance);

        RaycastHit hit;

        if (Physics.Linecast(transform.position, finalDir, out hit))
        {
            finalDistance = Mathf.Clamp(hit.distance, minDistance, maxDistance);
        }
        else
        {
            finalDistance = maxDistance;
        }
        realCamerea.localPosition = Vector3.Lerp(realCamerea.localPosition, dirNormalized * finalDistance, Time.deltaTime * smoothness);
    }
}
