using UnityEngine;

public class GridScanner : MonoBehaviour
{
    [Header("Scan settings")]
    [SerializeField] private float XRange = 1;
    [SerializeField] private float YRange = 1;
    [SerializeField] private Vector2 interval = new Vector2(1, 1);
    [SerializeField] private LayerMask detectionLayer;
    [SerializeField] private float checkRadius = 1;

    [Header("Debug")]
    public bool showGizmos = true;

    public bool ScanForObjectsAround()
    {
        Vector2 origin = transform.position;

        for (float x = -XRange; x <= XRange; x += interval.x)
        {
            for (float y = -YRange; y <= YRange; y += interval.y)
            {

                Vector2 checkPos = origin + new Vector2(x, y);
                Collider2D hit = Physics2D.OverlapCircle(checkPos, checkRadius, detectionLayer);
                if (hit != null)
                {
                    return true;
                }
            }
        }

        return false;
    }
    private void OnDrawGizmosSelected()
    {
        if (!showGizmos) return;

        Vector2 origin = transform.position;
        Gizmos.color = Color.yellow;

        for (float x = -XRange; x <= XRange; x += interval.x)
        {
            for (float y = -YRange; y <= YRange; y += interval.y)
            {
                Vector2 checkPos = origin + new Vector2(x, y);
                float newX = Mathf.Round(checkPos.x / interval.x) * interval.x;
                float newY = Mathf.Round(checkPos.y / interval.y) * interval.y;  
                //Gizmos.DrawWireSphere(checkPos, checkRadius);
                Gizmos.DrawWireSphere(new Vector2(newX, newY), checkRadius);
            }
        }
    }
}