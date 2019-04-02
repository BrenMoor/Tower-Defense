using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tower_Defense_Knock_Off
{
    class bullet
    {
        public int bulletX, bulletY, bulletSize;
        public bullet(int _bulletX, int _bulletY, int _bulletSize, int __bulletSize)
        {
            bulletX = _bulletX;
            bulletY = _bulletY;
            bulletSize = _bulletSize;
            bulletSize = __bulletSize;

        }
        public void Move()
        {
            bulletX = bulletX + 15;
        }
        public bool Collision(enemy b)
        {
            Rectangle bulletRec = new Rectangle(bulletX, bulletY, bulletSize, bulletSize);
            Rectangle enemyRec = new Rectangle(b.enemyX, b.enemyY, b.enemySize, b.enemySize);

            if (bulletRec.IntersectsWith(enemyRec))
            {
                return true;
            }
            return false;
        }
    }
}
