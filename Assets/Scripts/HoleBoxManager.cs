using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleBoxMamager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Box"))
        {
            // Trigger destroy object when position.x < 15
            other.gameObject.transform.position = new Vector3(-16, other.transform.position.y, other.transform.position.z);
            gameObject.transform.position = new Vector3(-16, transform.position.y, transform.position.z);
        }
    }
}
