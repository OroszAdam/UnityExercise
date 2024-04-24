using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject capsule1;
    public GameObject capsule2;
    public TextMeshProUGUI distanceText;
    public TextMeshProUGUI angleXText;
    public TextMeshProUGUI angleYText;
    public TextMeshProUGUI angleZText;
    public Material redMaterial;
    public Material yellowMaterial;
    public Material greenMaterial;
    public GameObject infoPanel;
    public AudioClip collisionSound;
    private AudioSource audioSource;
    private GameObject selectedObject;
    // Offset for smooth dragging
    private Vector3 offset;

    // UI panel offset relative to the capsule object
    private Vector3 offsetUI = new Vector3(0, 2.5f, 0);
    private Camera mainCamera;
    private float distance;
    private Vector3 deltaRotation;
    private CapsuleCollider collider1;
    private CapsuleCollider collider2;
    private bool previousCollisionState = false; // Variable to track previous collision state

    void Start()
    {
        mainCamera = Camera.main;
        collider1 = capsule1.GetComponent<CapsuleCollider>();
        collider2 = capsule2.GetComponent<CapsuleCollider>();
        UpdateUI();

        // Reference to AudioSource component
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        HandleObjectSelection();
        MoveSelectedObject();
        RotateSelectedObject();
        UpdateUI();
        UpdateObjectAppearance();
    }

    // Select objects by clicking on them
    // Unselect by clicking "away", aka not on the objects
    void HandleObjectSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.collider.gameObject;

                if (
                    (hitObject == capsule1 || hitObject == capsule2)
                    && hitObject.tag == "Selectable"
                )
                {
                    selectedObject = hitObject;
                    offset = selectedObject.transform.position - GetMouseWorldPosition();
                }
                else
                {
                    selectedObject = null;
                }
            }
            else
            {
                selectedObject = null;
            }
        }
    }

    // Drag (translate) game objects using the left mouse button
    void MoveSelectedObject()
    {
        if (selectedObject != null && Input.GetMouseButton(0))
        {
            Vector3 targetPos = GetMouseWorldPosition() + offset;
            selectedObject.transform.position = targetPos;
        }
    }

    // Rotate game objects using the right mouse button
    void RotateSelectedObject()
    {
        if (selectedObject != null && Input.GetMouseButton(1))
        {
            float rotateSpeed = 10f;
            float mouseX = Input.GetAxis("Mouse X") * rotateSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * rotateSpeed;

            selectedObject.transform.Rotate(Vector3.up, -mouseX, Space.World);
            selectedObject.transform.Rotate(Vector3.right, mouseY, Space.World);
        }
    }

    // Update UI elements
    void UpdateUI()
    {
        // Updating the position of the UI panel
        infoPanel.transform.position = capsule1.transform.position + offsetUI; 
        // Making sure that the info panel is always facing the camera
        // Not so important at the moment with stationary camera
        if (mainCamera != null)
        {
            infoPanel.transform.forward = mainCamera.transform.forward;
        }

        // Update the content of the UI panel
        GetDistance();
        distanceText.text = distance.ToString("F1");
        GetRotation();
        angleXText.text = deltaRotation.x.ToString("F1");
        angleYText.text = deltaRotation.y.ToString("F1");
        angleZText.text = deltaRotation.z.ToString("F1");


    }
    void GetDistance()
    {
        // Calculate distance of objects
        Vector3 direction = capsule2.transform.position - capsule1.transform.position;
        distance = direction.magnitude;
    }
    void GetRotation()
    {
        // The angles are the angles between the respective axes, i.e angleX is the angle between the x axes of the two object
        // float angleX = Vector3.Angle(capsule1.transform.right, capsule2.transform.right);
        // float angleY = Vector3.Angle(capsule1.transform.up, capsule2.transform.up);
        // float angleZ = Vector3.Angle(capsule1.transform.forward, capsule2.transform.forward);
        
        // Get the Euler angles of the rotations
        Vector3 euler1 = capsule1.transform.rotation.eulerAngles;
        Vector3 euler2 = capsule2.transform.rotation.eulerAngles;
        // Calculate the difference for each axis separately
        float angleX = Mathf.DeltaAngle(euler1.x, euler2.x);
        float angleY = Mathf.DeltaAngle(euler1.y, euler2.y);
        float angleZ = Mathf.DeltaAngle(euler1.z, euler2.z);
        deltaRotation = new Vector3(angleX, angleY, angleZ);
    }

    // Set 
    void UpdateObjectAppearance()
    {
        if (selectedObject != null)
        {
            // Checking if there is collision
            bool collided = Physics.ComputePenetration(
                collider1,
                capsule1.transform.position,
                capsule1.transform.rotation,
                collider2,
                capsule2.transform.position,
                capsule2.transform.rotation,
                out Vector3 dir,
                out float dist
            );

            // Setting the color according to collision state
            if (collided)
            {
                // Given the two capsules have same dimensions,
                // I approximate their overlap by their pose (position and rotation)
                if (
                    distance < 0.2f
                    && Math.Abs(deltaRotation.x) < 5
                    && Math.Abs(deltaRotation.y) < 5
                    && Math.Abs(deltaRotation.z) < 5
                )
                {
                    capsule1.GetComponent<Renderer>().material = greenMaterial;
                    capsule2.GetComponent<Renderer>().material = greenMaterial;       
                }
                else
                {
                    capsule1.GetComponent<Renderer>().material = yellowMaterial;
                    capsule2.GetComponent<Renderer>().material = yellowMaterial;
                    // Play collision sound
                    if (!previousCollisionState)
                    {
                        audioSource.PlayOneShot(collisionSound);
                    }
                }
            }
            else
            {
                capsule1.GetComponent<Renderer>().material = redMaterial;
                capsule2.GetComponent<Renderer>().material = redMaterial;
            }
            // Update previous collision state
            previousCollisionState = collided;
        }
    }

    // Get world point coordinates of the cursor
    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = selectedObject.transform.position.z - mainCamera.transform.position.z;
        return mainCamera.ScreenToWorldPoint(mousePoint);
    }
}
