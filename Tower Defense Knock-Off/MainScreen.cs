using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Media;


namespace Tower_Defense_Knock_Off
{
    public partial class GameScreen : UserControl
    {
        //Creating booleans
        Boolean wKeyDown, aKeyDown, sKeyDown, dKeyDown, enterKeyDown, escKeyDown, yKeyDown, nKeyDown, pauseScreen, loseScreen, turretEnter;
        //Creating ints
        int enemyX = 1400;
        int enemyY = 100;
        int enemySize = 30;
        int blockSize = 78;
        int blockSize2 = 80;
        int selectX = 0;
        int selectY = 0;
        int turretX = 0;
        int turretY = 0;
        int bulletX, bulletY;
        int turretSize = 80;
        int points = 100;
        int lives = 100;
        int enemySpeed = 4;
        int enemyCounter = 0;
        int waitCounter = 0;
        int shotTimer = 0;
        //Creating brush & fonts
        SolidBrush blackBrush = new SolidBrush(Color.Black);
        Font Spartan = new Font("Spartan", 20, FontStyle.Bold);
        //Class Lists
        List<enemy> Enemies = new List<enemy>();
        List<turret> Turrets = new List<turret>();
        List<bullet> Bullets = new List<bullet>();
        //Other lists
        List<select> select = new List<select>();
        List<int> xTurret = new List<int>();
        List<int> yTurret = new List<int>();
        List<int> xBullet = new List<int>();
        List<int> yBullet = new List<int>();
        List<int> xEnemy = new List<int>();
        List<int> yEnemy = new List<int>();
        List<string> directionBullet = new List<string>();
        SoundPlayer player = new SoundPlayer(Properties.Resources.Defense_Line);

        public GameScreen()
        {
            InitializeComponent();
            Cursor.Hide();
          
            OnStart();
            player.PlayLooping();

        }

        public void OnStart()
        {
            //Begins enemy creation
            enemy b1 = new enemy(enemyX, enemyY, enemySize, enemySize);
            Enemies.Add(b1);



        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            //Key up
            switch (e.KeyCode)
            {
                case Keys.W:
                    wKeyDown = false;
                    break;
                case Keys.A:
                    aKeyDown = false;
                    break;
                case Keys.S:
                    sKeyDown = false;
                    break;
                case Keys.D:
                    dKeyDown = false;
                    break;
                case Keys.Enter:
                    enterKeyDown = false;
                    break;
                case Keys.Escape:
                    escKeyDown = false;
                    break;
                case Keys.Y:
                    yKeyDown = false;
                    break;
                case Keys.N:
                    nKeyDown = false;
                    break;
            }
        }

        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //Key down
            switch (e.KeyCode)
            {
                case Keys.W:
                    wKeyDown = true;
                    break;
                case Keys.A:
                    aKeyDown = true;
                    break;
                case Keys.S:
                    sKeyDown = true;
                    break;
                case Keys.D:
                    dKeyDown = true;
                    break;
                case Keys.Enter:
                    enterKeyDown = true;
                    break;
                case Keys.Escape:
                    escKeyDown = true;
                    break;
                case Keys.Y:
                    yKeyDown = true;
                    if (pauseScreen == true)
                    {
                        gameTimer.Enabled = true;
                        pauseScreen = false;
                    }
                    if (loseScreen == true)
                    {
                        points = 100;
                        lives = 100;
                        Turrets.Clear();
                        Enemies.Clear();
                        enemy b1 = new enemy(enemyX, enemyY, enemySize, enemySize);
                        Enemies.Add(b1);
                        gameTimer.Enabled = true;
                        loseScreen = false;
                    }
                    break;
                case Keys.N:
                    nKeyDown = true;
                    if (pauseScreen == true || loseScreen == true)
                    {
                        Application.Exit();
                    }
                    break;
            }
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            //Drawing selector
            e.Graphics.DrawImage(Properties.Resources.Border2, selectX, selectY, 1300, 700);
            //Drawing text
            e.Graphics.DrawString("Points: " + points, Spartan, blackBrush, 90, 8);
            e.Graphics.DrawString("Lives: " + lives, Spartan, blackBrush, 90, 38);
            //Drawing enemies
            foreach (enemy b in Enemies)
            {
                e.Graphics.FillEllipse(blackBrush, b.enemyX, b.enemyY, enemySize, enemySize);
            }
            //Drawing turrets
            foreach (turret t in Turrets)
            {
                e.Graphics.FillEllipse(blackBrush, t.turretX, t.turretY, turretSize, turretSize);
            }
            if (pauseScreen == true)
            {
                e.Graphics.DrawImage(Properties.Resources.PAUSE, 255, 120, 768, 432);
            }
            if (loseScreen == true)
            {
                e.Graphics.DrawImage(Properties.Resources.LOSE, 255, 120, 768, 432);
            }
            foreach (bullet b in Bullets)
            {
                e.Graphics.FillEllipse(blackBrush, b.bulletX, b.bulletY, b.bulletSize, b.bulletSize);
            }
        }
        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //Tick counters
            enemyCounter++;
            waitCounter++;
            shotTimer++;

            if (escKeyDown == true)
            {
                gameTimer.Stop();
                pauseScreen = true;
            }
            //Check for enough points to place a turret
            if (enterKeyDown == true && points >= 20)
            {
                turretEnter = true;
                //Set turret XY to selector XY
                turretX = selectX;
                turretY = selectY;
                bool turretCheck = false;
                foreach (turret t in Turrets)
                {
                    if (selectX == t.turretX && selectY == t.turretY)
                    {

                        turretCheck = true;
                        break;
                    }

                }
                if (turretCheck == false)
                {

                    turret t1 = new turret(turretX, turretY, turretSize, turretSize);
                    Turrets.Add(t1);
                    points = points - 20;
                }


                waitCounter = 0;
            }

            if (shotTimer == 20)
            {
                foreach (turret t in Turrets)
                {

                    bullet b1 = new bullet(t.turretX + 40, t.turretY + 35, 10, 10);
                    Bullets.Add(b1);

                }
                shotTimer = 0;
            }

            foreach (bullet b in Bullets)
            {
                b.Move();

            }
            foreach (bullet b in Bullets)
            {
                foreach (enemy z in Enemies)
                {
                    if (b.Collision(z))
                    {
                        Bullets.Remove(b);
                        Enemies.Remove(z);
                        points++;
                        return;
                    }
                }
            }
            if (enemyCounter == 20)
            {
                enemy b1 = new enemy(enemyX, enemyY, enemySize, enemySize);
                Enemies.Add(b1);
                enemyCounter = 0;
            }
            foreach (enemy b in Enemies)
            {
                b.Move(enemySpeed);
              
            }
            


            {
                if (selectY > 0)
                {
                    if (wKeyDown == true)
                    {
                        selectY = selectY - blockSize;
                        wKeyDown = false;
                    }
                }
                if (selectY < (this.Height - 100))
                {
                    if (sKeyDown == true)
                    {
                        selectY = selectY + blockSize;
                        sKeyDown = false;
                    }
                }
                if (selectX > 0)
                {
                    if (aKeyDown == true)
                    {
                        selectX = selectX - blockSize2;
                        aKeyDown = false;
                    }
                }
                if (selectX < (this.Width - 100))
                {
                    if (dKeyDown == true)
                    {
                        selectX = selectX + blockSize2;
                        dKeyDown = false;
                    }
                }
                if (Enemies[0].enemyX == 0)
                {
                    lives = lives - 1;
                    Enemies.RemoveAt(0);
                    if (lives == 0)
                    {
                        gameTimer.Enabled = false;
                        loseScreen = true;
                    }
                }
                Refresh();
            }
        }
    }
}

