namespace RPG
{
    public class Trigger
    {
        private bool _value;
        public bool value
        {
            get
            {
                bool v = _value;
                _value = false;
                return v;
            } 
        }
        public virtual void Set()
        {
            _value = true;
        }
    }
}

