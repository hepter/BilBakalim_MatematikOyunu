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
    public partial class sorular : UserControl
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
        public sorular()
        {
            InitializeComponent();
        }
        public sorular(int level, Skor skr,Islem islem)
        {

            InitializeComponent();
            ZorlukveSeviyeGöster((Hane)level);

            DogruYanıt = 0;
            KalanSoru = 20;
            PasSayı = 0;
            soruSıra = 0;
            SoruOluştur(level,islem);
            PaslarıGetir = false;
            aşama = 0;
        }


        Sorucuk[] sorus = new Sorucuk[20];
        public int DogruYanıt;
        public int KalanSoru;
        public int PasSayı;
        int aşama = 0;
        public void SorucukPasHndl(object sender, EventArgs e)
        {
            //akıllıButton btn = (akıllıButton)sender;
            PasSayı++;

            BilgiGüncelle();
        }




        int soruSıra = 0;
      public  bool PaslarıGetir{ get; set; }
        void PasSorularıAç()
        {
            for (int i = 0; i < 20; i++)
            {
                Sorucuk a = sorus[i];

                if (a.Pasmı)
                {
                    a.pasDüzelt();
                    button1.FlatAppearance.BorderColor = Color.Yellow;
                    flowLayoutPanel1.Controls.Add(a);
                }
            }
        }


        public void BeşliSoruAç()
        {

            if (aşama >= 4)
            {

                label7.Text = "PAS";
            }
            else
            {
                label7.Text = (aşama+1) + "/" + 4;
            }
            aşama++;
            

            flowLayoutPanel1.Controls.Clear();
            if (soruSıra>=20)
            {
                PaslarıGetir = true;
                PasSorularıAç();
                return;
            }

            for (int i = 0; i < 5; i++)
            {
                flowLayoutPanel1.Controls.Add(sorus[soruSıra]);
                soruSıra++;
            }
         
        }


        void SoruOluştur(int level,Islem islem)
        {
            Random rnd = new Random();
            for (int i = 0; i< 20; i++)
            {



                islem isl = new islem(islem, (Hane)level);
                Sorucuk a = new Sorucuk(isl);
                a.akıllıButton1.Click += new EventHandler(SorucukPasHndl);
       
                //flowLayoutPanel1.Controls.Add(a);

                //    Button txb = (Button)a.Controls["akıllıButton1"];
                //  txb.Click += new EventHandler(soruHandler);
                sorus[i] = a;

            }
        }

        public void SaatGöster(string FormatlıSaat)
        {
            lblZorluk.Text = "Zaman:"+FormatlıSaat;
        }
        void ZorlukveSeviyeGöster(Hane hane)
        {
            Label lb = label2;
            switch (hane)
            {               
                case Hane.bir:
                    lb.Text = "Çok Kolay";
                    label3.Text = "Seviye - 1";
                    pictureBox1.Image = Properties.Resources._1;
                    break;
                case Hane.iki:
                    lb.Text = "Kolay";
                    label3.Text = "Seviye - 2";
                    pictureBox1.Image = Properties.Resources._2;
                    break;
                case Hane.uc:
                    lb.Text = "Orta";
                    label3.Text = "Seviye - 3";
                    pictureBox1.Image = Properties.Resources._3;
                    break;
                case Hane.dort:
                    lb.Text = "Zor";
                    label3.Text = "Seviye - 4";
                    pictureBox1.Image = Properties.Resources._4;
                    break;
                case Hane.bes:
                    lb.Text = "Çok Zor";
                    label3.Text = "Seviye - 5";
                    pictureBox1.Image = Properties.Resources._5;
                    break;             
            }
        }


        public void BilgiGüncelle()
        {
            string data = Environment.NewLine;
            data = DogruYanıt+"/"+KalanSoru+  data + PasSayı ;
            label6.Text = data;
        }
        public void BilgiGüncelle(string bilgi)
        {

            label6.Text = bilgi;

        }
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

       
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
    }
}
