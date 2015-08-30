#region References
using UnityEngine;
#endregion

public class RocketComponent : GameEntityComponent
{
	public const int MIN_DISTANCE_TO_EXPLODE = 1;

	public Vector2 flyToPosition;
	public bool canFly, canFollow;
	public float velocity = 200f;

	public void Explode ()
	{
		var explosion = GameLevel.instance.CreateExplosion();
		explosion.transform.SetPosition2D(transform.position);
		Destroy();
	}

	protected void Update ()
	{
		// follow 
		if (canFollow) transform.LookAt2D(flyToPosition);

		// fly
		if (canFly)
		{
			Vector2 position = transform.position;
			var next = Vector2.MoveTowards(position, flyToPosition, Time.deltaTime * velocity);
			transform.SetPosition2D(next);
			if (Vector2.Distance(next, flyToPosition) < MIN_DISTANCE_TO_EXPLODE) Explode();
		}
	}
}
