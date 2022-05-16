using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour, IRestartable
{
    public static ProjectilePool instance;
    
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private int initialCount = 20;

    private List<Projectile> pooledObjects;

    public Projectile GetProjectile()
    {
        foreach (Projectile p in pooledObjects)
        {
            if (!p.gameObject.activeInHierarchy)
            {
                return p;
            }
        }

        Projectile projectile = AddPooledObject();
        return projectile;
    }
    
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        FillPool(initialCount);
    }

    private void Start()
    {
        RestartController.instance.AddListener(this);
    }

    private void FillPool(int number)
    {
        pooledObjects = new List<Projectile>(initialCount);

        for (int i = 0; i < number; i++)
        {
            AddPooledObject();
        }
    }

    private Projectile AddPooledObject()
    {
        Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.gameObject.SetActive(false);
        pooledObjects.Add(projectile);

        return projectile;
    }

    public void OnRestart()
    {
        foreach (Projectile p in pooledObjects)
        {
            p.gameObject.SetActive(false);
        }
    }
}
