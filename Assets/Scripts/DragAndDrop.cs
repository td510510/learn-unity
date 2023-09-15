using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField] private InputAction press, screenPos;
    private MoveLeft moveLeftScript;
    private AudioSource playAudio;
    public AudioClip jumpSound;

    private Vector3 curScreenPos;

    private Camera cameraObject;
    private bool isDragging;

    // Start is called before the first frame update
    void Start()
    {
        cameraObject = Camera.main;
        moveLeftScript = GetComponent<MoveLeft>();
        playAudio = GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void Update()
    {
        if (!moveLeftScript.isDestroyGameObject)
        {
            HandleDrag();
        }
    }

    private Vector3 WorldPos
    {
        get
        {
            float z = cameraObject.WorldToScreenPoint(transform.position).z;
            return cameraObject.ScreenToWorldPoint(curScreenPos + new Vector3(0, 0, z));
        }
    }
    private bool isClickedOn
    {
        get
        {
            if (moveLeftScript.isDestroyGameObject || !cameraObject)
            {
                return false;
            }
            Ray ray = cameraObject.ScreenPointToRay(curScreenPos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                return hit.transform == transform;
            }
            return false;
        }
    }

    private void HandleDrag()
    {
        screenPos.Enable();
        press.Enable();
        screenPos.performed += context => { curScreenPos = context.ReadValue<Vector2>(); };
        press.performed += _ => { if (isClickedOn) StartCoroutine(Drag()); };
        press.canceled += _ => { isDragging = false; };
    }

    private IEnumerator Drag()
    {
        isDragging = true;
        playAudio.PlayOneShot(jumpSound, 1.0f);
        Vector3 offset = transform.position - WorldPos;
        // grab
        GetComponent<Rigidbody>().useGravity = false;
        while (isDragging)
        {
            // dragging
            transform.position = WorldPos + offset;
            yield return null;
        }
        // drop
        GetComponent<Rigidbody>().useGravity = true;
    }
}
