using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody[] joints;
    private Vector3 originalPos;
    //private float speed = 0;

    [SerializeField]
    public Game game;

    // Start is called before the first frame update
    void Start()
    {
        originalPos = gameObject.transform.position;

        joints = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rb in joints)
        {
            rb.isKinematic = true;
        }
    }

    //private void FixedUpdate()
    //{
    //    speed = 0;

    //    foreach (Rigidbody rb in joints)
    //    {
    //        if (!rb.isKinematic)
    //        {
    //            speed += rb.velocity.magnitude;
    //        }
    //    }

    //    /*if (speed > 0 && speed < 0.2)
    //        game.GameOver();*/
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            foreach (Rigidbody rb in joints)
            {
                rb.isKinematic = false;
            }
        }
    }

    public void ResetPlayer()
    {
        foreach (Rigidbody rb in joints)
        {
            rb.isKinematic = true;
        }

        gameObject.transform.position = originalPos;
    }
}
