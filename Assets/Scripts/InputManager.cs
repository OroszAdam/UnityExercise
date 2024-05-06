using UnityEngine;

public class InputManager : MonoBehaviour
{
    private GameObject capsule1;
    private GameObject capsule2;

    private GameObject selectedObject;
    private Vector3 offset;

    void Start()
    {
        capsule1 = GameController.Instance.capsule1;
        capsule2 = GameController.Instance.capsule2;
    }
    void Update()
    {
        HandleObjectSelection();
        MoveSelectedObject();
        RotateSelectedObject();
    }

    void HandleObjectSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.collider.gameObject;

                if ((hitObject == capsule1 || hitObject == capsule2) && hitObject.CompareTag("Selectable"))
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

    void MoveSelectedObject()
    {
        if (selectedObject != null && Input.GetMouseButton(0))
        {
            Vector3 targetPos = GetMouseWorldPosition() + offset;
            selectedObject.transform.position = targetPos;
        }
    }

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

    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = selectedObject.transform.position.z - Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}

