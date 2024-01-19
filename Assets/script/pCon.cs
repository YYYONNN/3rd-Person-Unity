using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pCon : MonoBehaviour
{

    private CharacterController chCon;
    private Vector3 pVelo;

    public bool pOnGround;
    public Animator animator;

    [SerializeField] private float pSpeed = 1f;
    [SerializeField] private float rSpeed = 5f;
    [SerializeField] private float gValue = -13f;
    [SerializeField] private float jHeight = 2f;


    [SerializeField] private Camera followCam;


    public static pCon instance;

    private void Awake()
    {   
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        chCon = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(gateTrigger.instance.isWinner)
        {
            case true:
                animator.SetBool("Victory", gateTrigger.instance.isWinner);
                break;
            case false:
                Mvmt();
                break;
        }
    }

    void Mvmt()
    {

        pOnGround = chCon.isGrounded;

        if (chCon.isGrounded && pVelo.y < -2)
        {
            pVelo.y = -1f;
        }

        //move
        float hInp = Input.GetAxis("Horizontal"),
              vInp = Input.GetAxis("Vertical");

        Vector3 mvmtInp = Quaternion.Euler(0,followCam.transform.eulerAngles.y,0) * new Vector3 (hInp,0,vInp);
        Vector3 mvmtDir = mvmtInp.normalized;

        chCon.Move(mvmtDir * pSpeed * Time.deltaTime);

        //rotate
        if (mvmtDir != Vector3.zero)
        {
            Quaternion desiRotate = Quaternion.LookRotation(mvmtDir, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiRotate, rSpeed * Time.deltaTime);
        }

        //jump
        if (Input.GetButtonDown("Jump") && pOnGround)
        {
            pVelo.y += Mathf.Sqrt(jHeight * -3.0f * gValue);
        }

        //gravity
        pVelo.y += gValue * Time.deltaTime;
        chCon.Move(pVelo * Time.deltaTime);



        animator.SetFloat("Speed", Mathf.Abs(mvmtDir.x) + Mathf.Abs(mvmtDir.z));
        animator.SetBool("Ground", chCon.isGrounded);

    }
}
