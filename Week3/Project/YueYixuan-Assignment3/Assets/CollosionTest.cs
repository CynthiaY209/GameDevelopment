using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollosionTest : MonoBehaviour
{
    public float force;
    public float friction;

    public GameObject other;

    public GameObject wallRight;
    public GameObject wallLeft;
    public GameObject wallUp;
    public GameObject wallDown;

    private float singleAxisDistance;
    private float singleAxisDistanceOther;

    private OtherSphere otherSphere;

    //上一帧结束时的速度
    private Vector3 preV;

    void Start()
    {
        preV = Vector3.zero;
        if (other!=null)
            otherSphere = other.GetComponent<OtherSphere>();
    }

    void Update()
    {
        //摩擦力
        Vector3 frictionDeltaV = -Time.deltaTime * friction * preV.normalized;
        //防止摩擦力反向运动
        Vector3 finalV = preV + frictionDeltaV;
        if (finalV.x * preV.x <= 0)
            frictionDeltaV.x = -preV.x;
        if (finalV.y * preV.y <= 0)
            frictionDeltaV.y = -preV.y;
        if (finalV.z * preV.z <= 0)
            frictionDeltaV.z = -preV.z;

        //计算用户用力方向
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 fDir = new Vector3(moveHorizontal, 0.0f, moveVertical);
        fDir.Normalize();


        //计算加速度
        Vector3 acceleration = force * fDir;

        Vector3 prePos = transform.position;

        //应用加速度
        Vector3 curV = preV + Time.deltaTime * acceleration + frictionDeltaV;
        transform.Translate((curV + preV) * Time.deltaTime / 2);
        preV = curV;

        Vector3 otherPos = other.transform.position;


        //检测是否与其他球相撞
        Vector3 pos = transform.position;
        if (other != null)
        {
            //球体间碰撞检测，判断球心距离与两球半径之和即可
            if (Vector3.Distance(pos, otherPos) < 0.5 + otherSphere.radius) //简单起见，认为自己的半径为0.5
            {
                Debug.Log("与红球碰撞发生!");
                Vector3 v1 = preV;
                float m1 = 1.0f; // 简单起见，认为自己的质量为1
                Vector3 v2 = otherSphere.currentV;
                float m2 = otherSphere.mass;

                preV = ((m1 - m2) * v1 + 2 * m2 * v2) / (m1 + m2);
                otherSphere.currentV = ((m2 - m1) * v2 + 2 * m1 * v1) / (m1 + m2);

                //如果有碰撞，位置回退，防止穿透
                transform.position = prePos;
            }
        }

        //检测是否与墙相撞
        pos = transform.position;
        if (wallRight!=null)
        {
            Vector3 wallRightPos = wallRight.transform.position;
            singleAxisDistance = wallRightPos.x - pos.x;
            if (singleAxisDistance < 1)//默认墙体厚度为1，则半厚度+白球半径=1
            {
                Debug.Log("与右墙碰撞发生!");
                preV.x = -preV.x;

                transform.position = prePos;
            }
            singleAxisDistanceOther = wallRightPos.x - other.transform.position.x;
            if (singleAxisDistanceOther < 0.5 + otherSphere.radius)//默认墙体厚度为1，则半厚度+红球半径为判定条件
            {
                Debug.Log("红球与右墙碰撞发生!");
                otherSphere.currentV.x = -otherSphere.currentV.x;

                other.transform.position = otherPos;
            }
        }

        if (wallLeft != null)
        {
            Vector3 wallLeftPos = wallLeft.transform.position;
            singleAxisDistance = pos.x - wallLeftPos.x;
            if (singleAxisDistance < 1)
            {
                Debug.Log("与左墙碰撞发生!");
                preV.x = -preV.x;

                transform.position = prePos;
            }
            singleAxisDistanceOther = other.transform.position.x - wallLeftPos.x;
            if (singleAxisDistanceOther < 0.5 + otherSphere.radius)
            {
                Debug.Log("红球与左墙碰撞发生!");
                otherSphere.currentV.x = -otherSphere.currentV.x;

                other.transform.position = otherPos;
            }
        }
        if (wallUp != null)
        {
            Vector3 wallUpPos = wallUp.transform.position;
            singleAxisDistance = wallUpPos.z - pos.z;
            if (singleAxisDistance < 1)
            {
                Debug.Log("与上墙碰撞发生!");
                preV.z = -preV.z;

                transform.position = prePos;
            }
            singleAxisDistanceOther = wallUpPos.z - other.transform.position.z;
            if (singleAxisDistanceOther < 0.5 + otherSphere.radius)
            {
                Debug.Log("红球与上墙碰撞发生!");
                otherSphere.currentV.z = -otherSphere.currentV.z;

                other.transform.position = otherPos;
            }
        }
        if (wallDown != null)
        {
            Vector3 wallDownPos = wallDown.transform.position;
            singleAxisDistance = pos.z - wallDownPos.z;
            if (singleAxisDistance < 1)
            {
                Debug.Log("与下侧墙碰撞发生!");
                preV.z = -preV.z;

                transform.position = prePos;
            }
            singleAxisDistanceOther = other.transform.position.z - wallDownPos.z;
            if (singleAxisDistanceOther < 0.5 + otherSphere.radius)
            {
                Debug.Log("红球与下墙碰撞发生!");
                otherSphere.currentV.z = -otherSphere.currentV.z;

                other.transform.position = otherPos;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
