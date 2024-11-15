using UnityEngine;

public class doorinteraction : MonoBehaviour
{
    public Animator doorAnimator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CuteAlien"))
        {
            Debug.Log("Press E to open the door");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            doorAnimator.SetTrigger("Open");
        }
    }
}
