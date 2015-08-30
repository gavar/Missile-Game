#region References
using UnityEngine;
using UnityEngine.UI;
#endregion

public class HUD : MonoBehaviour
{
	protected bool isDirty;
	public Text totalScore;
	public Text waveScore;
	public Text wave;
	public Button replay;

	protected void Start ()
	{
		isDirty = true;
		replay.onClick.AddListener(() => GameLevel.instance.Replay());
		Player.Main.PropertyChanged += (x, e) => isDirty = true;
	}

	protected void LateUpdate () { if (Property.Set(ref isDirty, false)) Repaint(); }

	protected void Repaint ()
	{
		var player = Player.Main;
		if (totalScore) totalScore.text = string.Format("{0:N0}", player.TotalScore);
		if (waveScore) waveScore.text = string.Format("Wave Score: {0:N0}", player.WaveScore);
		if (wave) wave.text = string.Format("Wave: {0:N0}", player.Wave);
	}
}
