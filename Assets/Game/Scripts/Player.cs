#region References
#endregion

public class Player : ReactiveObject
{
	public static readonly Player Main = new Player();

	protected int waveScore;
	protected int totalScore;
	protected int wave;

	public int TotalScore { get { return totalScore; } set { SetProperty(ref totalScore, value); } }
	public int WaveScore { get { return waveScore; } set { SetProperty(ref waveScore, value); } }
	public int Wave { get { return wave; } set { SetProperty(ref wave, value); } }

	public void Clear ()
	{
		wave = 1;
		waveScore = 0;
		totalScore = 0;
	}
}
