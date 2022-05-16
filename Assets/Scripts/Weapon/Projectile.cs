using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private MeshVisibilityStatus visibilityStatus;
    [SerializeField] private LayerMask obstacles;
    [SerializeField] private LayerMask players;

    private Damager damager;
    
    public void Activate(Vector3 position, Quaternion rotation, Damager damager)
    {
        this.damager = damager;
        
        transform.position = position;
        transform.rotation = rotation;
        
        gameObject.SetActive(true);
    }

    private void Update()
    {
        transform.position += transform.forward * (speed * Time.deltaTime);

        Ray ray = new Ray(transform.position, transform.forward);
        
        if (Physics.Raycast(ray, out RaycastHit obstacleHit, 0.1f, obstacles))
        {
            Vector3 normal = obstacleHit.normal;
            Vector3 newDir = Vector3.Reflect(transform.forward, normal);
            transform.forward = newDir;
        }
        else if (Physics.Raycast(ray, out RaycastHit playerHit, 0.1f, players))
        {
            if (playerHit.transform.TryGetComponent(out Damager other))
            {
                if (!other.Equals(damager))
                {
                    damager.IncreaseScore();
                }
            }
            
            gameObject.SetActive(false);
        }
    }
    
    private void OnEnable()
    {
        visibilityStatus.becameInvisible += OnLeftCameraBounds;
    }

    private void OnDisable()
    {
        visibilityStatus.becameInvisible -= OnLeftCameraBounds;
    }

    private void OnLeftCameraBounds()
    {
        gameObject.SetActive(false);
    }
}
