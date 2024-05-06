using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    private GameObject capsule1;
    private GameObject capsule2;
    public Material redMaterial;
    public Material yellowMaterial;
    public Material greenMaterial;
    private CapsuleCollider collider1;
    private CapsuleCollider collider2;
    private bool previousCollisionState = false;

    void Start()
    {
        capsule1 = GameController.Instance.capsule1;
        capsule2 = GameController.Instance.capsule2;
        collider1 = capsule1.GetComponent<CapsuleCollider>();
        collider2 = capsule2.GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        UpdateObjectAppearance();
    }

    void UpdateObjectAppearance()
    {
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

        if (collided)
        {
            if (Vector3.Distance(capsule1.transform.position, capsule2.transform.position) < 0.2f &&
                Mathf.Abs(GetRotation().x) < 5 && Mathf.Abs(GetRotation().y) < 5 && Mathf.Abs(GetRotation().z) < 5)
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
                    AudioManager.Instance.PlayCollision();
                }
            }
        }
        else
        {
            capsule1.GetComponent<Renderer>().material = redMaterial;
            capsule2.GetComponent<Renderer>().material = redMaterial;
        }

        previousCollisionState = collided;
    }

    Vector3 GetRotation()
    {
        Vector3 euler1 = capsule1.transform.rotation.eulerAngles;
        Vector3 euler2 = capsule2.transform.rotation.eulerAngles;
        float angleX = Mathf.DeltaAngle(euler1.x, euler2.x);
        float angleY = Mathf.DeltaAngle(euler1.y, euler2.y);
        float angleZ = Mathf.DeltaAngle(euler1.z, euler2.z);
        return new Vector3(angleX, angleY, angleZ);
    }
}