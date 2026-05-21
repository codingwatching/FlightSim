using System;
using System.Collections.Generic;
using UnityEngine;

public class WaypointState {
    WaypointList waypoints;
    int index;

    public bool Finished { get; private set; }

    public Vector3 CurrentPosition {
        get {
            return waypoints.GetWaypoint(index).Position;
        }
    }

    public WaypointState(WaypointList waypoints, int index) {
        if (waypoints == null) throw new ArgumentNullException(nameof(waypoints));

        this.waypoints = waypoints;
        this.index = index;
    }

    public void Update(Vector3 position) {
        var waypoint = waypoints.GetWaypoint(index);
        var distance = Vector3.Distance(waypoint.Position, position);

        if (distance < waypoints.ArrivalRadius) {
            index++;

            if (index >= waypoints.Count) {
                if (waypoints.Looping) {
                    index = 0;
                } else {
                    Finished = true;
                }
            }
        }
    }
}

public class WaypointList : MonoBehaviour {
    [SerializeField]
    List<Waypoint> waypoints;
    [SerializeField]
    bool looping = true;
    [SerializeField]
    float arrivalRadius;

    public bool Looping {
        get {
            return looping;
        }
    }

    public float ArrivalRadius {
        get {
            return arrivalRadius;
        }
    }

    public int Count {
        get {
            return waypoints.Count;
        }
    }

    public WaypointState StartWaypoints(Vector3 startPosition) {
        int selectedIndex = 0;
        float bestDistance = float.PositiveInfinity;

        // select nearest waypoint to start
        for (int i = 0; i < waypoints.Count; i++) {
            var waypoint = waypoints[i];
            var distance = Vector3.Distance(waypoint.Position, startPosition);

            if (distance < bestDistance) {
                bestDistance = distance;
                selectedIndex = i;
            }
        }

        return new WaypointState(this, selectedIndex);
    }

    public Waypoint GetWaypoint(int index) {
        return waypoints[index];
    }
}
