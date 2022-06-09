using UnityEngine;

public class LineRendererUpdater : MonoBehaviour {

    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Transform transform1;
    [SerializeField] private Transform transform2;

    private void Update() {
        DrawRope();
    }

    private void DrawRope() {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, transform1.position);
        lineRenderer.SetPosition(1, transform2.position);
    }

}
