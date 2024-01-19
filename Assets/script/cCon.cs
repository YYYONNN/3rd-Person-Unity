using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cCon : MonoBehaviour
{

    [SerializeField] private float mouseSens = 3.0f;

    private float rY, rX;

    [SerializeField] private Transform target;
    [SerializeField] private float targetDist = 3.0f;

    private Vector3 curRotate, 
                    smoothVelo = Vector3.zero;

    [SerializeField] private float smoothTime = 0.2f;
    [SerializeField] private Vector2 rXminmax = new Vector2 (-20, 40);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        float mouseX = Input.GetAxis("Mouse X") * mouseSens,
              mouseY = Input.GetAxis("Mouse Y") * mouseSens * -1;

        rY += mouseX;
        rX += mouseY;

        rX = Mathf.Clamp(rX, rXminmax.x, rXminmax.y);

        Vector3 nextR = new Vector3(rX, rY);

        curRotate = Vector3.SmoothDamp(curRotate, nextR, ref smoothVelo, smoothTime);
        transform.localEulerAngles = curRotate;

        transform.position = target.position - transform.forward * targetDist;

    }
}
