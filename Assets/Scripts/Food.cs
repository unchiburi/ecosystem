using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public float KakusanJikan;
    public float value;
    public float kikaku;
    float radius;
    float time;
    float D;
    float ScreenHalfHeight,ScreenHalfWidth;
    int frameCount = 0; 
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        ScreenHalfHeight = Camera.main.orthographicSize;
        ScreenHalfWidth = ScreenHalfHeight*Camera.main.aspect;
        D = ScreenHalfWidth*ScreenHalfWidth/(2*KakusanJikan);
    }

    // Update is called once per frame
    void Update()
    {
        radius = 3*Mathf.Sqrt(2*D*time);
        if(frameCount==100){
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, 1 << LayerMask.NameToLayer("Fish"));
            foreach (var hitCollider in hitColliders)
            {
                Fish fishScript = hitCollider.transform.gameObject.GetComponent<Fish>();
                Vector3 toFish = hitCollider.transform.position-transform.position;
                float dist = toFish.magnitude;
                float scent = kikaku*Mathf.Exp(-dist*dist/(4f*D*time))/Mathf.Sqrt(4f*Mathf.PI*D*time);
                fishScript.SenseFood(gameObject, scent, value);
            }
            frameCount = 0;
        }
        else frameCount++;
        time += Time.deltaTime;
        if(time > 2*KakusanJikan){
            Destroy(gameObject);
        }
    }
}
