#region References
using UnityEngine;
#endregion

public class RocketComponent : GameEntityComponent
{
	public const int MIN_DISTANCE_TO_EXPLODE = 1;

	public Vector2 flyToPosition;
	public bool canFly, canLook;
	public float velocity = 200f;

	public override void Explode ()
	{
		base.Explode();
		Destroy();
	}

	#region Overrides of GameEntityComponent
	public override void HitByExplosion (ExplosionComponent explosion)
	{
		// don't create explosion
	}
	#endregion

	#region Unity
	protected void Update ()
	{
		// direction 
		if (canLook) transform.LookAt2D(flyToPosition);

		// fly
		if (canFly)
		{
			Vector2 position = transform.position;
			var next = Vector2.MoveTowards(position, flyToPosition, Time.deltaTime * velocity);
			transform.SetPosition2D(next);
			if (Vector2.Distance(next, flyToPosition) < MIN_DISTANCE_TO_EXPLODE) Explode();
		}
	}
	#endregion
}
