namespace RPG
{
    public class Timer: IFinisher
    {
        public float runTime { get; set; }
        public float duration { get; set; }
        public void Reset()
        {
            runTime = 0;
        }
        public bool isFinish()
        {
            return runTime >= duration;
        }
        public void Update(float dt)
        {
            runTime += dt;
        }
    }
}
