using UnityEngine;

public class UnlockShortcut : MonoBehaviour
{
    public GameObject Barrier1;
    public GameObject Barrier2;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Barrier1Trigger"))
        {
            Barrier1.SetActive(false);
        }

        if (other.CompareTag("Barrier2Trigger"))
        {
            Barrier2.SetActive(false);
        }
    }
}
