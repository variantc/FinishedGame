using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse3D : MonoBehaviour
{
    // Event for other objects with which to register when mouse clicked
    public event Action<Vector3> OnMouseLeftClick;      // Event when LMB clicked
    public event Action<Vector3> OnMouseRightClick;      // Event when RMB clicked

    References refs;

    void Start()
    {
        refs = FindObjectOfType<References>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit hit = CastRay(Input.mousePosition);

            if (hit.collider == null)
                return;
            if (hit.collider.gameObject.tag == "Floor")
                OnMouseLeftClick?.Invoke(hit.point);
            if (hit.collider.gameObject.tag == "Enemy" || hit.collider.gameObject.tag == "Treasure")
                OnMouseLeftClick?.Invoke(new Vector3(hit.point.x, 0, hit.point.z));
        }
        if (Input.GetMouseButtonUp(1))
        {
            RaycastHit hit = CastRay(Input.mousePosition);
            OnMouseRightClick?.Invoke(hit.point);
        }
    }

    RaycastHit CastRay(Vector3 mousePos)
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, 100f);
        return hit;
    }
}
