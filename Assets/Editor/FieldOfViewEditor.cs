using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        FieldOfView fov = (FieldOfView)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transformPosition.position, Vector3.up, Vector3.forward, 360, fov.radius);

        Vector3 viewAngle01 = DirectionFromAngle(fov.transformPosition.eulerAngles.y, -fov.angle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(fov.transformPosition.eulerAngles.y, fov.angle / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(fov.transformPosition.position, fov.transformPosition.position + viewAngle01 * fov.radius);
        Handles.DrawLine(fov.transformPosition.position, fov.transformPosition.position + viewAngle02 * fov.radius);

        if (fov.CanSeePlayer)
        {
            Handles.color = Color.green;
            Handles.DrawLine(fov.transformPosition.position, fov.target.position);
        }
    }
    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
