using UnityEngine;

public class BloodPuddle : MonoBehaviour
{
    private float bloodTimer;
    private void Update() {
        bloodTimer += Time.deltaTime;
        if (bloodTimer < 2) {
            transform.localScale = Vector3.one * bloodTimer/2;
        }
    }

}
