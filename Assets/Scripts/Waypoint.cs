using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour {
    [SerializeField]
    float recommendedSpeed;

    new Transform transform;

    public Vector3 Position {
        get {
            return transform.position;
        }
    }

    public float RecommendedSpeed {
        get {
            return recommendedSpeed;
        }
    }

    void Awake() {
        transform = GetComponent<Transform>();
    }
}
