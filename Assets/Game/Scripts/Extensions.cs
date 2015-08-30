#region References
using UnityEngine;
#endregion

public static class Extensions
{
	public static void SetPosition2D (this Transform transform, Vector2 position)
	{
		if (transform == null) return;
		var p = transform.position;
		p.x = position.x;
		p.y = position.y;
		transform.position = p;
	}

	public static void SetLocalPosition2D (this Transform transform, Vector2 localPosition)
	{
		if (transform == null) return;
		var p = transform.localPosition;
		p.x = localPosition.x;
		p.y = localPosition.y;
		transform.localPosition = p;
	}

	public static void SetRotationZ (this Transform transform, float z)
	{
		if (transform == null) return;
		var r = transform.eulerAngles;
		r.z = z;
		transform.eulerAngles = r;
	}

	public static void LookAt2D (this Transform transform, Vector3 position)
	{
		var direction = transform.position - position;
		var angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle + 180f, -Vector3.forward);
	}
}
