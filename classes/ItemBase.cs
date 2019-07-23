using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovingEngine.classes
{
    class ItemBase
    {
        public string name;
        public ItemBase(string name)
        {
            this.name = name;
        }
        public virtual void OnItemEquip()
        {

        }
    }
    class WeaponItem : ItemBase
    {
        public WeaponItem(string name, int cooldown, int damage, int bulletspeed, Action<Projectile> onFire) : base(name)
        {

        }

        public override void OnItemEquip()
        {
            base.OnItemEquip();
        }
    }
}
