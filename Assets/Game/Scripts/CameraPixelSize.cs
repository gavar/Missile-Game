#region References
using UnityEngine;
#endregion

[ExecuteInEditMode]
public class CameraPixelSize : MonoBehaviour
{
	protected new Camera camera;
	public float pixelsPerUnit = 1f;

	#region Unity
	protected void OnEnable () { camera = GetComponent<Camera>(); }
	protected void Update () { if (camera) camera.orthographicSize = Screen.height * .5f / pixelsPerUnit; }
	#endregion
}
