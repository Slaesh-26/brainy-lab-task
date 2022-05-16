using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	[SerializeField] private Damager damager;
	[SerializeField] private Transform spawnPoint;

	public void Fire()
	{
		Projectile projectile = ProjectilePool.instance.GetProjectile();
		projectile.Activate(spawnPoint.position, spawnPoint.rotation, damager);
	}
}
