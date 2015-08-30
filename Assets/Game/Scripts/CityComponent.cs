#region References
using System.Collections.Generic;
using UnityEngine;
#endregion

public class CityComponent : GameEntityComponent
{
	public static readonly List<CityComponent> Cities = new List<CityComponent>();

	public SpriteRenderer body;
	public bool broken;

	public override void HitByExplosion (ExplosionComponent explosion)
	{
		if (broken) return;
		Explode();
		SetBroken(true);
	}

	public virtual void SetBroken (bool value)
	{
		if (broken == value) return;
		broken = value;
		if (body != null) body.enabled = !broken;
	}

	protected override void UpdateRegistry (bool submit)
	{
		base.UpdateRegistry(submit);
		if (submit) Cities.Add(this);
		else Cities.Remove(this);
	}
}
