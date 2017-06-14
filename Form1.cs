using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game2
{
    public partial class Form1 : Form
    {
        [DllImport("user32")] static extern short GetAsyncKeyState(Keys vKey);
        List<Chara> charaList;
        TimeCounter timeCount;
        Chara ship;     //自機
        Chara[] missile;  //ミサイル
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            charaList = new List<Chara>();
            timeCount = new TimeCounter();
            ship = new Chara(Properties.Resources.ship);
            missile = new Chara[10];
            for (int i=0; i<missile.Length; i++)
            {
                missile[i] = new Chara(Properties.Resources.Missile);
                missile[i].Visible = false;
                charaList.Add(missile[i]);
            }

            charaList.Add(ship);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int mtime=0;
            long count = timeCount.getCount();
            for(long i = 0; i < count; i++)
            {
                if (GetAsyncKeyState(Keys.Down) < 0)
                    ship.Y += 1;
                if (GetAsyncKeyState(Keys.Up) < 0)
                    ship.Y -= 1;
                if (GetAsyncKeyState(Keys.Right) < 0)
                    ship.X += 1;
                if (GetAsyncKeyState(Keys.Left) < 0)
                    ship.X -= 1;
                if (GetAsyncKeyState(Keys.Space) < 0 && mtime<=0)
                {
                    for (int j = 0; j < missile.Length; j++)
                    {
                        if (missile[j].Visible==false)
                        {
                            mtime = 16;
                            missile[j].Visible = true;
                            missile[j].X = ship.X + (48 - 16) / 2;
                            missile[j].Y = ship.Y;
                            break;
                        }
                        if (missile[j].Y == -32&&missile[j].Visible==true)
                        {
                            missile[j].Visible = false;
                        }
                    }
                }
                mtime--;
                for (int k=0; k<missile.Length; k++) {
                    missile[k].Y -= 2;
                }

                if (ship.Y < 0) ship.Y = 0;
                if (ship.X < 0) ship.X = 0;
                if (ship.Y > ClientSize.Height - 48) ship.Y = ClientSize.Height - 48;
                if (ship.X > ClientSize.Width - 48) ship.X = ClientSize.Width - 48;
            }
            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            foreach(Chara chara in charaList)
            {
                chara.draw(g);
            }
        }
    }
}
