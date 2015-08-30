#region References
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
#endregion

public class GameLevel : MonoBehaviour
{
	public static GameLevel instance;

	public RectTransform canvas;
	public GameObject explosion;
	public GameObject rocket;

	#region Factory
	public RocketComponent CreateRocket ()
	{
		if (rocket == null) return null;
		var clone = Instantiate(rocket);
		var retVal = clone.GetComponent<RocketComponent>() ?? clone.AddComponent<RocketComponent>();
		retVal.Initialize();
		return retVal;
	}

	public ExplosionComponent CreateExplosion ()
	{
		if (explosion == null) return null;
		var clone = Instantiate(explosion);
		var retVal = clone.GetComponent<ExplosionComponent>() ?? clone.AddComponent<ExplosionComponent>();
		retVal.Initialize();
		return retVal;
	}
	#endregion

	public int AutoFireToPosition (Vector3 position)
	{
		var tower = TowerComponent.AutoSelectToFire(position);
		if (tower == null) return -10;
		tower.FireRocket(position);
		return 0;
	}

	public void InitializeLevel ()
	{
		// TODO: don't create copy of list
		foreach (var entity in GameEntityComponent.Registry.ToList()) entity.Initialize();
	}

	#region Unity
	protected void OnEnable ()
	{
		instance = this;
		InitializeLevel();
	}
	protected void Start ()
	{
		var clickEvents = new EventTrigger.TriggerEvent();
		// USER CLICK
		clickEvents.AddListener(x =>
		{
			var position = Camera.main.ScreenToWorldPoint((x as PointerEventData).pointerCurrentRaycast.screenPosition);
			AutoFireToPosition(position);
		});
		var eventListener = canvas.gameObject.AddComponent<EventTrigger>();
		eventListener.triggers.Add(new EventTrigger.Entry { eventID = EventTriggerType.PointerClick, callback = clickEvents });
	}
	protected void Update () { }
	#endregion
}
