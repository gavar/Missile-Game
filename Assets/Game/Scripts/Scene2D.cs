#region References
using UnityEngine;
using UnityEngine.UI;
#endregion

[ExecuteInEditMode]
public class Scene2D : MonoBehaviour
{
	public static Scene2D main;

	public float pixelsPerUnit = 1f;
	public Vector2 referenceResolution = new Vector2(960f, 540); // 16:9
	[Range (0.0f, 1f)] public float widthOrHeight = 1f; // by height
	public CanvasScaler.ScreenMatchMode screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
	public bool isMain;

	protected void UpdateSceneScale ()
	{
		var factor = 1f;
		var screen = new Vector2(Screen.width, Screen.height);

		switch (screenMatchMode)
		{
			case CanvasScaler.ScreenMatchMode.MatchWidthOrHeight:
				factor = Mathf.Pow(2f, Mathf.Lerp(Mathf.Log(screen.x / referenceResolution.x, 2f), Mathf.Log(screen.y / referenceResolution.y, 2f), widthOrHeight));
				break;

			case CanvasScaler.ScreenMatchMode.Expand:
				factor = Mathf.Max(screen.x / referenceResolution.x, screen.y / referenceResolution.y);
				break;

			case CanvasScaler.ScreenMatchMode.Shrink:
				factor = Mathf.Min(screen.x / referenceResolution.x, screen.y / referenceResolution.y);
				break;
		}

		factor *= pixelsPerUnit;
		transform.localScale = new Vector3(factor, factor, factor);
	}

	#region Unity
	protected void Awake () { if (isMain) main = this; }
	protected void Update () { UpdateSceneScale(); }
	#endregion
}
