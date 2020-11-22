using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionPoint : MonoBehaviour
{
    public GameObject sparkParticles;
    private GameObject target;

    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        speed = target.GetComponent<Move>().moveSpeed;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.tag == "Stone")&&(speed==0))
        {
            ContactPoint contact = collision.contacts[0];
            Vector3 pos = contact.point;
            Quaternion rot = Quaternion.FromToRotation(pos, target.transform.position);

            //GameObject.Find("Camera").SendMessage("OnEnable");
            //Debug.Log(GameObject.Find("Camera").GetComponent<ShakeCamera>().isshakeCamera);

            CinemachineShake.Instance.ShakeCamera(5f,.2f);

            GameObject clone;
            clone = (GameObject)Instantiate(sparkParticles, pos, rot);
            GameObject.Destroy(clone, 1.0f);
        }

    }
}
