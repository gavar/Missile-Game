#region References
using UnityEngine;
#endregion

public class ExplosionComponent : GameEntityComponent
{
	protected float lifeTime;
	public float maxLifeTime = 4f;

	protected void Update ()
	{
		lifeTime += Time.deltaTime;
		if (lifeTime >= maxLifeTime) Destroy();
	}
}
