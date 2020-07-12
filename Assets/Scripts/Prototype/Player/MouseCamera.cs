using UnityEngine;

public class MouseCamera : MonoBehaviour
{

    public GameObject camPos;
    public GameObject fPos;
    public GameObject tPos;
    public float flowSpeed;
    public float rotLimit = 75f; // this is set for firstperson starts off

    public float mouseSensitivity = 100f;

    public Transform playerBody;

    float xRotation = 0f;

    bool cameraChanging = false;

    public Transform bodyCheck;
    public float bodyDistance = 0.4f;
    public LayerMask bodyMask;
    public MeshRenderer bodyRender;

    public LayerMask fPerson;
    public LayerMask tPerson;

    bool isFirstPerson = true;
    bool isCurrentFP;
    bool isCurrentTP;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        isFirstPerson = Physics.CheckSphere(bodyCheck.position, bodyDistance, bodyMask);
        isCurrentFP = Physics.CheckSphere(transform.position, bodyDistance, tPerson);
        isCurrentTP = Physics.CheckSphere(transform.position, bodyDistance, fPerson);

        if (isFirstPerson)
        {
            bodyRender.enabled = false;
        }
        else if (!isFirstPerson)
        {
            bodyRender.enabled = true;
        }

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -rotLimit, rotLimit);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * mouseX);

        if (isCurrentFP)
        {
            cameraChanging = true;
            //for third person
        }

        if (isCurrentTP)
        {
            cameraChanging = false;
            //for first person
        }

        CameraChange();



    }

    void CameraChange()
    {
        if (cameraChanging == false)
        {
            camPos.transform.position = Vector3.MoveTowards(camPos.transform.position, fPos.transform.position, Time.deltaTime * flowSpeed);
            rotLimit = 75f;
        }
        else if (cameraChanging == true)
        {
            camPos.transform.position = Vector3.MoveTowards(camPos.transform.position, tPos.transform.position, Time.deltaTime * flowSpeed);
            rotLimit = 40f;
        }
    }
}
