using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatematikOyunu
{

    public partial class anasayfa : UserControl
    {
        //DoulbeBuffered
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;    // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }
        public anasayfa()
        {
            InitializeComponent();
        }

        Skor _Seviye;
        public anasayfa(Skor Seviye)
        {
            _Seviye = Seviye;
            InitializeComponent();
            label3.Text = "~ " + (Seviye.LevelGetir()).ToString() + "/5 ~";
        }

        public void anasayfaYenile()
        {
            if (_Seviye!=null)
            {    
                label3.Text = "~ " + (_Seviye.LevelGetir()).ToString() + "/5 ~";
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
        RadioButton currentChecked;
        private void anasayfa_Load(object sender, EventArgs e)
        {
            currentChecked = radioButton1;
        }
       public Islem AnasayfaIslem = Islem.random;
        private void checkIslemKontrol(object sender, EventArgs e)
        {
         
            RadioButton r = (RadioButton)sender;
            int id = int.Parse(r.Name.Replace("radioButton", ""));
            if (r != currentChecked)
            {
                currentChecked.Checked = false;
                currentChecked = r;
            }
            switch (id)
            {
                case 1:
                    pictureBox6.Image = Properties.Resources.shuffle1;
                    pictureBox7.Image = Properties.Resources.plus_;
                    pictureBox8.Image = Properties.Resources.minus_;
                    pictureBox9.Image = Properties.Resources.multiply_;
                    pictureBox10.Image = Properties.Resources.division_;

                    break;
                case 2:

                    pictureBox6.Image = Properties.Resources.shuffle_;
                    pictureBox7.Image = Properties.Resources.plus1;
                    pictureBox8.Image = Properties.Resources.minus_;
                    pictureBox9.Image = Properties.Resources.multiply_;
                    pictureBox10.Image = Properties.Resources.division_;
                    break;
                case 3:
                    pictureBox6.Image = Properties.Resources.shuffle_;
                    pictureBox7.Image = Properties.Resources.plus_;
                    pictureBox8.Image = Properties.Resources.minus1;
                    pictureBox9.Image = Properties.Resources.multiply_;
                    pictureBox10.Image = Properties.Resources.division_;
                    break;
                case 4:
                    pictureBox6.Image = Properties.Resources.shuffle_;
                    pictureBox7.Image = Properties.Resources.plus_;
                    pictureBox8.Image = Properties.Resources.minus_;
                    pictureBox9.Image = Properties.Resources.multiply1;
                    pictureBox10.Image = Properties.Resources.division_;
                    break;
                case 5:
                    pictureBox6.Image = Properties.Resources.shuffle_;
                    pictureBox7.Image = Properties.Resources.plus_;
                    pictureBox8.Image = Properties.Resources.minus_;
                    pictureBox9.Image = Properties.Resources.multiply_;
                    pictureBox10.Image = Properties.Resources.division1;
                    break;
            }



            switch (id-1)
            {
                case 0:
                    AnasayfaIslem = Islem.random;
                    break;
                case 1:
                    AnasayfaIslem = Islem.top;
                    break;
                case 2:
                    AnasayfaIslem = Islem.cık;
                    break;
                case 3:
                    AnasayfaIslem = Islem.carp;
                    break;
                case 4:
                    AnasayfaIslem = Islem.böl;
                    break;
               
            }
        }
    }
}
