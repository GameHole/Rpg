namespace RPG
{
    public class Hittable
    {
        public bool value { get; private set; }
        public virtual void Set()
        {
            value = true;
        }
        public void Reset()
        {
            value = false;
        }
    }
}

