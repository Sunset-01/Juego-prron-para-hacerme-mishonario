using UnityEngine;
using System.Collections;

public class ToyCarrier : MonoBehaviour
{
    [Header("Agarre")]
    public Transform holdPoint;
    public float minHoldTime = 2f;
    public float maxHoldTime = 5f;

    [Header("Movimiento")]
    public float releaseSpeedThreshold = 0.1f;

    bool isHolding = false;
    Rigidbody currentToy;
    Rigidbody characterRb;

    void Awake()
    {
        characterRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // 🔽 AQUÍ VA LO DE SOLTAR SI SE MUEVE
        if (isHolding && characterRb != null)
        {
            if (characterRb.linearVelocity.magnitude > releaseSpeedThreshold)
            {
                ReleaseToy();
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isHolding) return;

        if (collision.gameObject.CompareTag("Toy"))
        {
            Rigidbody toyRb = collision.gameObject.GetComponent<Rigidbody>();
            if (toyRb != null)
            {
                StartCoroutine(HoldToyRoutine(toyRb));
            }
        }
    }

    IEnumerator HoldToyRoutine(Rigidbody toyRb)
    {
        isHolding = true;
        currentToy = toyRb;

        toyRb.isKinematic = true;
        toyRb.detectCollisions = false;

        toyRb.transform.SetParent(holdPoint);
        toyRb.transform.localPosition = Vector3.zero;
        toyRb.transform.localRotation = Quaternion.identity;

        float holdTime = Random.Range(minHoldTime, maxHoldTime);
        yield return new WaitForSeconds(holdTime);

        ReleaseToy();
    }

    void ReleaseToy()
    {
        if (currentToy == null) return;

        currentToy.transform.SetParent(null);
        currentToy.isKinematic = false;
        currentToy.detectCollisions = true;

        currentToy = null;
        isHolding = false;
    }
}
