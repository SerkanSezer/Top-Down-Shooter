using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    private Quaternion targetAngle;
    private float turnSpeed = 500;
    private bool isProcess = true;
    private bool isOpen = false;

    public void Interact() {
        StartCoroutine(Open());
    }

    private void Awake() {
        targetAngle = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z+179);
    }

    public IEnumerator Open() {
        if (!isOpen) {
            isOpen = true;
            AudioManager.instance.PlayDoorSound();
            while (isProcess) {
                yield return null;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetAngle, turnSpeed * Time.deltaTime);
                if (Vector3.Distance(transform.rotation.eulerAngles, targetAngle.eulerAngles) < 1) {
                    isProcess = false;
                }
            }
        }
    }

}
