using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMyVision : MonoBehaviour {
    public enum Sensitivity { LOW, HIGH }
    public Sensitivity sensitivity = Sensitivity.HIGH;
    public bool targetInSight = false;

    // Field of Vision
    public float fov = 45f;

    // Reference to target
    private Transform target = null;

    //NPC Eyes
    public Transform myEyes = null;

    // My Transform
    public Transform npcTransform = null;

    // My Collider

    private SphereCollider sphereCollider = null;

    // Last Known Sighting 
    public Vector3 lastKnownSighting = Vector3.zero;

    // private LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }

    private void Awake () {
        npcTransform = GetComponent<Transform> ();
        sphereCollider = GetComponent<SphereCollider> ();
        lastKnownSighting = npcTransform.position;
        target = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
        // lineRenderer = GetComponent<LineRenderer> ();
    }
    bool InMyFOV () {
        Vector3 dirToTarget = target.position - myEyes.position;
        //Get Angle 
        float angle = Vector3.Angle (myEyes.forward, dirToTarget);

        // print("Angle: " + angle);
        //
        if (angle <= fov) {
            return true;
        } else
            return false;
    }

    bool ClearLineofSight () {
        RaycastHit hit;
        // lineRenderer.SetPosition (0, myEyes.position);
        if (Physics.Raycast (myEyes.position, (target.position - myEyes.position).normalized, out hit, sphereCollider.radius)) {
            if (hit.transform.CompareTag ("Player")) {
                // Debug.Log ("Player Hit");
                return true;
            }

        }
        return false;
    }
    void UpdateSight () {
        switch (sensitivity) {
            case Sensitivity.HIGH:
                targetInSight = InMyFOV () && ClearLineofSight ();
                break;
            case Sensitivity.LOW:
                targetInSight = InMyFOV () || ClearLineofSight ();
                break;
        }
    }
    private void OnTriggerStay (Collider other) {
        UpdateSight ();
        
        if (targetInSight) {
            // Debug.Log ("Sphere Collision Player in Sight");
            lastKnownSighting = target.position;
        }
    }
    private void OnTriggerExit (Collider other) {
        if (!other.CompareTag ("Player")) {
            return;
        }
        targetInSight = false;
    }
}