using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tower_Defense_Knock_Off
{
    class enemy
    {
        public int enemySize;
        public int enemyX = 1000;
        public int enemyY = 40;
        public bool up, down, left = true, right;
        public enemy(int _enemyX, int _enemyY, int _enemySize, int __enemySize)
        {
            enemyX = _enemyX;
            enemyY = _enemyY;
            enemySize = _enemySize;
            enemySize = __enemySize;

        }
        public void Move(int enemySpeed)
        {
            if (left == true)
            {
                enemyX = enemyX - enemySpeed;
                if (enemyX == 1068 && enemyY < 200)
                {
                    left = false;
                    down = true;
                }
                if (enemyX == 908)
                {
                    left = false;
                    up = true;
                }
                if (enemyX == 744 && enemyY > 400)
                {
                    left = false;
                    up = true;
                }
                if (enemyX == 580)
                {
                    left = false;
                    down = true;
                }
                if (enemyX == 428)
                {
                    left = false;
                    up = true;
                }
                if (enemyX == 260 && enemyY > 300)
                {
                    left = false;
                    down = true;
                }
                if (enemyX == 100 && enemyY > 300)
                {
                    left = false;
                    up = true;
                }
            }
            if (right == true)
            {
                enemyX = enemyX + enemySpeed;
                if (enemyX == 1148)
                {
                    right = false;
                    down = true;
                }
                if (enemyX == 904)
                {
                    right = false;
                    up = true;
                }
                if (enemyX == 340)
                {
                    right = false;
                    up = true;

                }

            }
            if (up == true)
            {
                enemyY = enemyY - enemySpeed;
                if (enemyY == 412 && enemyX > 200)
                {
                    up = false;
                    left = true;
                }
                if (enemyY == 260)
                {
                    up = false;
                    right = true;
                }
                if (enemyY == 100)
                {
                    up = false;
                    left = true;
                }

            }
            if (down == true)
            {
                enemyY = enemyY + enemySpeed;
                if (enemyY == 260 && enemyX > 1000)
                {
                    right = true;
                    down = false;
                }
                if (enemyY == 572)
                {
                    left = true;
                    down = false;
                }
            }
                
        }
    }
}
