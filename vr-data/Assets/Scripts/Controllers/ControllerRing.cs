using UnityEngine;
using System.Collections.Generic;

public class ControllerRing : MonoBehaviour {

    private int objectsColliding = 0;
    private DataPoint closestSelectableDataPoint;
    private float distanceToClosestSelectableDataPoint = 9999f;

    private float movementTimer = 0f;
    private float movementTime = 0.1f;
    private bool extended = false;
    private bool contracted = true;
    private bool extending = false;
    private bool contracting = false;
    private Vector3 contractedPosition;
    private Vector3 extendedPosition;

    public bool Colliding {
        get { return objectsColliding > 0; }
    }

    public DataPoint DataPointToSelect {
        get { return closestSelectableDataPoint; }
    }

    void Start() {
        transform.localPosition = contractedPosition;
    }
    
    void OnTriggerStay(Collider other) {
        if (other.gameObject.GetComponent<DataPoint>() == null) return;
        float distanceBetweenOtherAndRing = Vector3.Distance(other.gameObject.transform.position, transform.position);
        if (distanceBetweenOtherAndRing < distanceToClosestSelectableDataPoint)
        {
            distanceToClosestSelectableDataPoint = distanceBetweenOtherAndRing;
            closestSelectableDataPoint = other.gameObject.GetComponent<DataPoint>();
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.GetComponent<DataPoint>() == null) return;
        objectsColliding++;
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.GetComponent<DataPoint>() == null) return;
        objectsColliding--;
        if (objectsColliding <= 0) {
            distanceToClosestSelectableDataPoint = 9999f;
            closestSelectableDataPoint = null;
        }
    }

    public void SetUpPositions(Vector3 newContractedPositon, Vector3 newExtendedPosition) {
        contractedPosition = newContractedPositon;
        extendedPosition = newExtendedPosition;
    }

    public void ExtendOrContract() {
        if (!extending && !contracting) {
            if (extended) {
                contracting = true;
                extended = false;
            } else if (contracted) {
                extending = true;
                contracted = false;
            }
        }
    }

    void Update() {
        if (extending) {
            movementTimer += (Time.deltaTime / movementTime);
            transform.localPosition = Vector3.Lerp(contractedPosition, extendedPosition, movementTimer);
            if (movementTimer >= 1.0f) {
                movementTimer = 0.0f;
                extending = false;
                extended = true;
            }
        }
        if (contracting) {
            movementTimer += (Time.deltaTime / movementTime);
            transform.localPosition = Vector3.Lerp(extendedPosition, contractedPosition, movementTimer);
            if (movementTimer >= 1.0f) {
                movementTimer = 0.0f;
                contracting = false;
                contracted = true;
            }
        }
    }
}
