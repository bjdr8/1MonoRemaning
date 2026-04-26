public class Timer
{
    private float time;
    private float maxTime;

    public bool IsDone => time <= 0;

    public Timer(float duration)
    {
        maxTime = duration;
        time = duration;
    }

    public void Tick(float deltaTime)
    {
        if (IsDone) return;
        time -= deltaTime;
    }

    public void Reset()
    {
        time = maxTime;
    }
}
