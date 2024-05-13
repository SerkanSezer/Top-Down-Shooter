using UnityEngine;

public class Crosshair : MonoBehaviour
{
    private float scaleTimer;
    [SerializeField] AnimationCurve crossScaleCurve;

    void Update()
    {
        transform.position = Input.mousePosition;
        AnimateCrosshair();
    }

    public void AnimateCrosshair() {
        scaleTimer += Time.deltaTime;
        if (scaleTimer < 1) {
            float localScale = crossScaleCurve.Evaluate(scaleTimer);
            transform.localScale = new Vector3(localScale, localScale, localScale);
        }
        else { 
            scaleTimer = 0; 
        }
    }
}
