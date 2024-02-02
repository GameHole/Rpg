using System.Collections.Generic;

namespace RPG
{
    public abstract class AAction
    {
        protected Character character;
        protected List<Transation> transations = new List<Transation>();
        public void SetCharacter(Character character)
        {
            this.character = character;
            foreach (var item in transations)
            {
                item.SetCharacter(character);
            }
        }
        public virtual void Start() { }
        public virtual void Run()
        {
            foreach (var item in transations)
            {
                if (item.isVailed())
                {
                    item.Switch();
                    return;
                }
            }
        }
    }
}
