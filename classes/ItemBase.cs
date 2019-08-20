using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MovingEngine.classes
{
    class ItemBase
    {
        public int probability = 1;
        public string name;
        public ItemBase(int probability, string name)
        {
            this.name = name;
            this.probability = probability;
        }
        public virtual void OnItemEquip()
        {
            
        }
        public static ItemBase GetRandomItem()
        {
            int currentprob = 0;
            Dictionary<int, ItemBase> sets = new Dictionary<int, ItemBase>();
            foreach(var i in items)
            {
                sets.Add(currentprob, i);
                currentprob += i.probability;
            }
            int choice = Globals.random.Next(0, currentprob);
            ItemBase choiceItem = null;
            foreach(var i in sets)
            {
                if(choice >= i.Key)
                {
                    choiceItem = i.Value;
                }
            }
            return choiceItem;
        }
        public static List<ItemBase> items = new List<ItemBase>()
        {
            new WeaponItem(100, "Default Gun", 8, 12, 20, 25, a => { }),
            new WeaponItem(33, "Sniper Rifle", 45, 100, 60, 50, a => { }),
            new WeaponItem(33, "Lasergun", 1, 2, 50, 10, a => { }) { projectileLength = 80 },
            new WeaponItem(50, "Shotgun", 25, 8, 12, 50, a => { }) { projectilesPerShot = 9 },
            new WeaponItem(33, "Flamethrower", 1, 3, 6, 50, a=>{ }) { projectileBrush = Brushes.OrangeRed, projectileLength = 10, projectileWidth = 40 },
            new WeaponItem(33, "Pass-Through Rifle", 13, 18, 45, 10, a=>{ }) {Tags=new string[] { "Pass-Through"} },
            new EffectItem(0, "No Effect", "NONE")
        };
    }
    class WeaponItem : ItemBase
    {
        public int cooldown;
        public double damage;
        public int bulletspeed;
        public double accuracy;
        public double currentAccuracy = 0;
        public Action<Projectile> onFire = (p) => { };
        public Brush projectileBrush = Brushes.Blue;
        public int projectileLength = 40;
        public int projectileWidth = 10;
        public int projectilesPerShot = 1;
        public string[] Tags = new string[] { };
        public Action onAim = () => { };
        public double disappearTime = 1;
        public WeaponItem(int probability, string name, int cooldown, double damage, int bulletspeed, int accuracy, Action<Projectile> onFire) : base(probability, name)
        {
            this.cooldown = cooldown;
            this.damage = damage;
            this.bulletspeed = bulletspeed;
            this.onFire = onFire;
            this.accuracy = accuracy;
        }
        public override void OnItemEquip()
        {
            Weapons.equipped = this;
        }
    }
    class EffectItem : ItemBase
    {
        public string effect;
        public EffectItem(int probability, string name, string effect) : base(probability, name)
        {
            this.effect = effect;
        }
    }
}
