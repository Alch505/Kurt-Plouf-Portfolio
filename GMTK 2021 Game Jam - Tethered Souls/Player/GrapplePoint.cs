using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplePoint : MonoBehaviour
{
    public LineRenderer lineRenderer;

    [System.Obsolete]
    private void Start()
    {
        lineRenderer.SetWidth(0.1f, 0.1f);
        lineRenderer.SetPosition(0, transform.position);
    }

    void Update()
    {
        lineRenderer.SetPosition(1, GameManager.instance.player.transform.position);

        if (Input.GetButtonUp("Fire2")) 
        {
            Destroy(this.gameObject);
        }
    }
}
