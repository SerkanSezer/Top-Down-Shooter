using TMPro;
using UnityEngine;

public class DamageNumber : MonoBehaviour
{
    private TextMeshPro killText;
    private float damageNumberSpeed = 4;
    private Vector3 killTextStopPos;

    private void Awake() {
       killText = GetComponent<TextMeshPro>();
    }

    private void Start() {
        killTextStopPos = transform.position + transform.up * 7;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, killTextStopPos, damageNumberSpeed*Time.deltaTime);
        if (Vector3.Distance(transform.position,killTextStopPos) < 0.2f) {
            Destroy(gameObject);
        }
    }
    
    public void SetKillPoints(int points) {
        killText.text = "+" + points.ToString();
    }
}
