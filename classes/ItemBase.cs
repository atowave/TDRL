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
        public string name;
        public ItemBase(string name)
        {
            this.name = name;
        }
        public virtual void OnItemEquip()
        {

        }
        public static List<ItemBase> items = new List<ItemBase>()
        {
            new WeaponItem("Default Gun", 8, 12, 20, a => { }),
            new WeaponItem("Sniper", 45, 100, 60, a => { }),
            new WeaponItem("Lasergun", 2, 2, 8, a => { }),
            new WeaponItem("Shotgun", 25, 8, 12, a => {
                Projectile p2 = a.copy();
                Projectile p3 = a.copy();
                Tuple<double, double> mov = new Tuple<double, double>(a.Movement[0], a.Movement[1]);
                Tuple<double, double> mov2 = Mathfuncs.RotateAroundOrigin(mov, new Tuple<double, double>(0, 0), 10);
                Tuple<double, double> mov3 = Mathfuncs.RotateAroundOrigin(mov, new Tuple<double, double>(0, 0), -10);
                double[] Mov2 = new[]{mov2.Item1,mov2.Item2};
                double[] Mov3 = new[]{mov3.Item1,mov3.Item2};
                p2.Movement = Mov2;
                p3.Movement = Mov3;
                Globals.projectiles.Add(p2);
                Globals.projectiles.Add(p3);
            }),
            new WeaponItem("Flamethrower", 5, 0.95, 6, a => {
                Projectile p2 = a.copy();
                Projectile p3 = a.copy();
                Projectile p4 = a.copy();
                Projectile p5 = a.copy();
                Projectile p6 = a.copy();
                Projectile p7 = a.copy();
                Tuple<double, double> mov = new Tuple<double, double>(a.Movement[0], a.Movement[1]);
                Tuple<double, double> mov2 = Mathfuncs.RotateAroundOrigin(mov, new Tuple<double, double>(0, 0), 5);
                Tuple<double, double> mov3 = Mathfuncs.RotateAroundOrigin(mov, new Tuple<double, double>(0, 0), -5);
                Tuple<double, double> mov4 = Mathfuncs.RotateAroundOrigin(mov, new Tuple<double, double>(0, 0), 8);
                Tuple<double, double> mov5 = Mathfuncs.RotateAroundOrigin(mov, new Tuple<double, double>(0, 0), -8);
                Tuple<double, double> mov6 = Mathfuncs.RotateAroundOrigin(mov, new Tuple<double, double>(0, 0), 12);
                Tuple<double, double> mov7 = Mathfuncs.RotateAroundOrigin(mov, new Tuple<double, double>(0, 0), -12);
                double[] Mov2 = new[]{mov2.Item1,mov2.Item2};
                double[] Mov3 = new[]{mov3.Item1,mov3.Item2};
                double[] Mov4 = new[]{mov4.Item1,mov4.Item2};
                double[] Mov5 = new[]{mov5.Item1,mov5.Item2};
                double[] Mov6 = new[]{mov6.Item1,mov6.Item2};
                double[] Mov7 = new[]{mov7.Item1,mov7.Item2};
                p2.Movement = Mov2;
                p3.Movement = Mov3;
                p4.Movement = Mov4;
                p5.Movement = Mov5;
                p6.Movement = Mov6;
                p7.Movement = Mov7;
                Globals.projectiles.Add(p2);
                Globals.projectiles.Add(p3);
                Globals.projectiles.Add(p4);
                Globals.projectiles.Add(p5);
                Globals.projectiles.Add(p6);
                Globals.projectiles.Add(p7);
            }) { projectileBrush = Brushes.OrangeRed },
        };
    }
    class WeaponItem : ItemBase
    {
        public int cooldown;
        public double damage;
        public int bulletspeed;
        Action<Projectile> onFire;
        public Brush projectileBrush = Brushes.Blue;
        public WeaponItem(string name, int cooldown, double damage, int bulletspeed, Action<Projectile> onFire) : base(name)
        {
            this.cooldown = cooldown;
            this.damage = damage;
            this.bulletspeed = bulletspeed;
            this.onFire = onFire;
        }
        public void OnFire(Projectile p)
        {
            onFire(p);
        }
        public override void OnItemEquip()
        {
            base.OnItemEquip();
        }
    }
}
