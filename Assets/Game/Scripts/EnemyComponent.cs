#region References
using UnityEngine;
#endregion

public class EnemyComponent : GameEntityComponent
{
	public const int MIN_DISTANCE_TO_EXPLODE = 1;

	public EnemyType enemyType;
	public GameEntityComponent target;
	public float velocity = 200f;

	#region Overrides of GameEntityComponent
	public override void Initialize ()
	{
		base.Initialize();

		switch (enemyType)
		{
			case EnemyType.Missile:
				// select target
				var index = Random.Range(0, CityComponent.Cities.Count);
				target = CityComponent.Cities[index];
				break;
		}
	}
	#endregion

	protected void Update ()
	{
		switch (enemyType)
		{
			case EnemyType.Missile:
				if (target == null) break;
				var flyToPosition = target.transform.position;
				transform.LookAt2D(flyToPosition); // direction
				Vector2 position = transform.position;
				var next = Vector2.MoveTowards(position, flyToPosition, Time.deltaTime * velocity);
				transform.SetPosition2D(next);
				if (Vector2.Distance(next, flyToPosition) < MIN_DISTANCE_TO_EXPLODE)
				{
					Explode();
					Destroy();
				}
				break;
		}
	}

	#region Overrides of GameEntityComponent
	public override void HitByExplosion (ExplosionComponent explosion)
	{
		base.HitByExplosion(explosion);
		GameLevel.instance.OnKillEnemy(this);
	}
	#endregion
}
