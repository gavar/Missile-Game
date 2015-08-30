#region References
using UnityEngine;
#endregion

public class ExplosionComponent : GameEntityComponent
{
	protected float lifeTime;
	public float maxLifeTime = 4f;
	public bool isEnemy;

	#region Unity
	protected void Update ()
	{
		if (lifeTime >= maxLifeTime) Destroy();
		lifeTime += Time.deltaTime;
	}

	protected void OnTriggerEnter2D (Collider2D other)
	{
		var layer = other.gameObject.layer;
		if (layer == GameLevel.CityLayer && !isEnemy) return;
		if (layer == GameLevel.EnemyLayer && isEnemy) return;
		var receiver = other.GetComponentInParent<GameEntityComponent>();
		if (receiver == null) return;
		receiver.HitByExplosion(this);
	}
	#endregion
}
