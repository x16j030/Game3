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
        Chara ship;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            charaList = new List<Chara>();
            timeCount = new TimeCounter();
            ship = new Chara(Properties.Resources.ship);

            charaList.Add(ship);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
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
