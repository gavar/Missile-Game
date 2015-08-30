#region References
using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
#endregion

public class GameLevel : MonoBehaviour
{
	public static LayerMask CityLayer = LayerMask.NameToLayer("City");
	public static LayerMask EnemyLayer = LayerMask.NameToLayer("Enemy");

	public static GameLevel instance;

	public RectTransform canvas;
	public GameObject explosion;
	public GameObject rocket;
	public Enemy[] enemies;

	#region Factory
	public RocketComponent CreateRocket ()
	{
		if (rocket == null) return null;
		var clone = Instantiate(rocket);
		var retVal = clone.RequireComponent<RocketComponent>();
		retVal.transform.SetParent(Scene2D.main.transform);
		retVal.Initialize();
		return retVal;
	}

	public ExplosionComponent CreateExplosion (GameEntityComponent owner)
	{
		if (explosion == null) return null;
		var clone = Instantiate(explosion);
		var retVal = clone.RequireComponent<ExplosionComponent>();
		retVal.transform.SetParent(Scene2D.main.transform);
		retVal.isEnemy = owner is EnemyComponent;
		retVal.Initialize();
		return retVal;
	}

	public EnemyComponent CreateEnemy (EnemyType enemyType)
	{
		var enemy = Array.Find(enemies, x => x.type == enemyType);
		if (enemy.instance == null) return null;
		var clone = Instantiate(enemy.instance);
		var retVal = clone.RequireComponent<EnemyComponent>();
		retVal.transform.SetParent(Scene2D.main.transform);
		retVal.enemyType = enemyType;
		retVal.Initialize();
		return retVal;
	}
	#endregion

	public void SpawnEnemy (EnemyType enemyType)
	{
		var rect = canvas.rect;
		var localPosition = new Vector3(UnityEngine.Random.Range(rect.x, rect.width), rect.height * 1.1f, 0f);
		var position = canvas.TransformPoint(localPosition);
		SpawnEnemy(enemyType, position);
	}
	public void SpawnEnemy (EnemyType enemyType, Vector3 position)
	{
		var enemy = CreateEnemy(enemyType);
		if (enemy == null) return;
		enemy.transform.SetPosition2D(position);
	}

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

		InitializeLevel();
		StartCoroutine(GameLoop());
	}
	protected void Update () { }
	#endregion

	protected IEnumerator GameLoop ()
	{
		while (true)
		{
			SpawnEnemy(EnemyType.Missile);
			yield return new WaitForSeconds(2f);
		}
	}
}

[Serializable]
public struct Enemy
{
	public EnemyType type;
	public GameObject instance;
}

public enum EnemyType
{
	Missile,
	Bombardier,
	Satelite,
	SmartBomb,
}
