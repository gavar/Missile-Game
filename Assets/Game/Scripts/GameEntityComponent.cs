#region References
using System.Collections.Generic;
using UnityEngine;
#endregion

/// <summary>
/// Base class for game entities.
/// </summary>
public class GameEntityComponent : MonoBehaviour
{
	public static readonly List<GameEntityComponent> Registry = new List<GameEntityComponent>();

	public virtual void Initialize () { }
	public virtual void Destroy () { Destroy(gameObject); }
	protected virtual void UpdateRegistry (bool submit)
	{
		if (submit) Registry.Add(this);
		else Registry.Remove(this);
	}

	#region Unity
	protected virtual void Awake () { UpdateRegistry(true); }
	protected virtual void Start () { Initialize(); }
	protected virtual void Reset () { Initialize(); }
	protected virtual void OnDestroy () { UpdateRegistry(false); }
	#endregion
}
