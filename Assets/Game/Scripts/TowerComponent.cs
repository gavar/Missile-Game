﻿#region References
using System.Collections.Generic;
using UnityEngine;
#endregion

/// <summary>
/// City defensive tower.
/// </summary>
public class TowerComponent : CityComponent
{
	#region Static
	public static readonly List<TowerComponent> Towers = new List<TowerComponent>();

	public static TowerComponent AutoSelectToFire (Vector3 position)
	{
		if (Towers.Count < 1) return null;
		var sqrDistance = float.MaxValue;
		TowerComponent tower = null;
		foreach (var instance in Towers)
		{
			if (!instance.CanFire) continue;
			var sqr = Vector3.SqrMagnitude(position - instance.transform.position);
			if (sqr > sqrDistance) continue;
			sqrDistance = sqr;
			tower = instance;
		}

		return tower;
	}
	#endregion

	public int ammo;
	public Transform rocketPoint;
	public RocketComponent nextRocket;

	public override void Initialize ()
	{
		ammo = 10;
		base.Initialize();
		InitializeNextRocket();
	}

	#region Overrides of CityComponent
	public override void HitByExplosion (ExplosionComponent explosion)
	{
		base.HitByExplosion(explosion);
		ammo = 0;
		nextRocket.Explode();
	}
	#endregion

	public bool CanFire { get { return (GameLevel.instance.unlimitedAmmo || ammo > 0) && !broken; } }

	public void FireRocket (Vector3 position)
	{
		if (!CanFire) return;
		InitializeNextRocket();

		if (nextRocket == null) return;
		nextRocket.canFly = true;
		nextRocket.canLook = false;
		nextRocket.flyToPosition = position;
		nextRocket = null;

		ammo--;
		InitializeNextRocket();
	}

	#region Unity
	protected virtual void LateUpdate ()
	{
		// follow mouse
		if (nextRocket != null) nextRocket.flyToPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}
	#endregion

	protected void InitializeNextRocket ()
	{
		if (!CanFire) return;
		if (nextRocket != null) return;
		nextRocket = GameLevel.instance.CreateRocket();
		nextRocket.transform.SetParent(rocketPoint, true);
		nextRocket.transform.SetLocalPosition2D(Vector2.zero);
		nextRocket.canLook = true;
	}

	protected override void UpdateRegistry (bool submit)
	{
		base.UpdateRegistry(submit);
		if (submit) Towers.Add(this);
		else Towers.Remove(this);
	}
}
