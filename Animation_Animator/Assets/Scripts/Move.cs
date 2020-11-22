using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float moveSpeed;
    private float moveSpeedStart;

    private Vector3 dir;
    private float h;
    private float v;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        moveSpeed = 0.02f;
        moveSpeedStart = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Control();
    }

    public void Control()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        dir = new Vector3(h, 0, v);
        transform.LookAt(transform.position + dir);                 //角色朝向
        transform.Translate(dir * moveSpeed, Space.World);          //用Tranlate方法实现移动

        if ((h != 0 | v != 0))
        {
            animator.SetBool("iswalking", true);
        }
        else
            animator.SetBool("iswalking", false);

        if (Input.GetKeyDown(KeyCode.B))
        {
            if (animator.GetBool("iswalking") == true)
            {
                animator.SetBool("isrunning", true);
                moveSpeed *= 2;
            }
            else
            {
                animator.SetBool("isrunning", false);
            }

        }

        if (Input.GetKeyUp(KeyCode.B))
        {
            animator.SetBool("isrunning", false);
            moveSpeed /= 2;
        }

        if (Input.GetKeyDown(KeyCode.J))
        { 
            animator.SetBool("isattacking", true);
            moveSpeed = 0;
        }
        else
            animator.SetBool("isattacking", false);
    }

    public void AttackOver(float x)
    {
        moveSpeed = x;
        Debug.Log('a');
    }
}
