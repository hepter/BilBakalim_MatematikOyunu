using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatematikOyunu
{
    public partial class Form1 : MaterialForm
    {
        //DoulbeBuffered
        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        var cp = base.CreateParams;
        //        cp.ExStyle |= 0x02000000;    // Turn on WS_EX_COMPOSITED
        //        return cp;
        //    }
        //}
        public Form1()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);

        }
        Sorucuk []sorus;
        private void button1_Click(object sender, EventArgs e)
        {
            //sorus = new Sorucuk[5];

            //for (int i = 0; i < 5; i++)
            //{
            //    islem isl = new islem(Islem.carp,Hane.iki);
            //    Sorucuk a = new Sorucuk(isl);
            //    flowLayoutPanel1.Controls.Add(a);

            //    Button txb = (Button)a.Controls["akıllıButton1"];
            //  //  txb.Click += new EventHandler(soruHandler);
            //    sorus[i] = a;

            //}


            LevelAç(skr);
        }


        void soruHandler (object sender , EventArgs e)
        {

            akıllıButton txb = (akıllıButton)sender;
            MessageBox.Show(txb.AnaIslem.hesapla().ToString());
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
     
        }

        public static string AppDir = AppDomain.CurrentDomain.BaseDirectory;
        // Kayıt konum      
        string SkorKonum = "skor.txt";
        void LevelAç(Skor skor)
        {
            seviyeler.SkorYansıt(skr);
            for (int i = 0; i < 5; i++)
            {
               
                Button btn = (Button)seviyeler.tableLayoutPanel1.Controls["button" + (i+1)];        
                PictureBox box = (PictureBox)seviyeler.tableLayoutPanel1.Controls["pictureBox" + (i + 1)];
                btn.Enabled = true;
                int yıldız = skor.yıldızGetir(i);
                switch (yıldız)
                {
                    case 0:

                       if (i == 0)
                       {

                            box.Image = Properties.Resources.acik;
                       }
                       else if (i!=0&&skor.yıldızGetir(i-1)!=0)
                       {
                            box.Image = Properties.Resources.acik;
                       }
                       else
                       {
                            box.Image = Properties.Resources.kapali;
                       }                      
                        break;
                    case 1:
                        box.Image = Properties.Resources.y1;
                        break;
                    case 2:
                        box.Image = Properties.Resources.y2;
                        break;
                    case 3:
                        box.Image = Properties.Resources.y3;
                        break;                
                }

                if ((i!=0&& skor.yıldızGetir(i-1) == 0) && !hack().Contains((i+1).ToString()))
                {
                    btn.Enabled = false;
                }
                else if (hack().Contains((i + 1).ToString()))
                {

                    if (skor.yıldızGetir(i) == 0)
                    {
                        box.Image = Properties.Resources.acik;
                    }
                    

                }

            }   


        }
        Skor skr;
     



        anasayfa aSayfa;
        seviyeler seviyeler;
        sorular sorular;
        string[] args = Environment.GetCommandLineArgs();
        string hackStr = "";

        string hack()
        {
            if (hackStr!="")
            {
                return hackStr;
            }

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].ToLowerInvariant().Equals("open"))
                {

                    //open'dan sonra args devam ediyorsa
                    if ((args.Length>=i+2))
                    {
                        if ( args[i + 1] == "all")
                        {
                            hackStr = "12345";
                           
                        }
                        else
                        {
                            hackStr = args[i + 1];
                        }
                    }
                }
            }

            return hackStr;
            
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            skr = new Skor(AppDir + SkorKonum);//Dosyadan Skor oku
        
            aSayfa = new anasayfa(skr);
            aSayfaInit();

            seviyeler = new seviyeler();
            seviyelerInit();

            sorular = new sorular();
            sorularInit();


            LevelAç(skr);
        
            panel1.Controls.Add(aSayfa);


            timer1.Start();

        }

        /////Formların Click Handleleri
        void aSayfaInit()
        {
            aSayfa.Dock = DockStyle.Fill;
            aSayfa.button1.Click += new EventHandler(BaşlatHndl);
            aSayfa.button2.Click += new EventHandler(SeviyelerHndl);
            aSayfa.button3.Click += new EventHandler(ÇıkışHndl);


        }
        void seviyelerInit()
        {
            seviyeler.Dock = DockStyle.Fill;
            seviyeler.button1.Click += new EventHandler(SeviyelerOynaHndl);
            seviyeler.button2.Click += new EventHandler(SeviyelerOynaHndl);
            seviyeler.button3.Click += new EventHandler(SeviyelerOynaHndl);
            seviyeler.button4.Click += new EventHandler(SeviyelerOynaHndl);
            seviyeler.button5.Click += new EventHandler(SeviyelerOynaHndl);
            seviyeler.button6.Click += new EventHandler(SeviyelerGeriHndl);

        }

        void sorularInit()
        {
            sorular.Dock = DockStyle.Fill;
            sorular.button1.Click += new EventHandler(SorularKontrolEtHndl);
            sorular.button2.Click += new EventHandler(SorularGeriHndl);
            sorular.timer1.Tick += new EventHandler(this.timer1_Tick);
        }


        //anasayfa
        Stopwatch Zaman;
        int level = 0;//açılmış son level
        int Oynananlevel = 0;
        void BaşlatHndl(object sender,EventArgs e)
        {
            level = skr.LevelGetir();
            Oynananlevel = level;
            int yıldız = skr.yıldızGetir(Oynananlevel-1);
            //if (level==1)
            //{
            //    MessageBox.Show("Şu an ilk seviyedesiniz" + Environment.NewLine + "Tamam tuşunun ardından süre başlayacak ve en kısa sürede yanıtlamaya çalışmalısınız.");
            //}
            //else
            //{
            //    for (int i = 1; i < 5; i++)
            //    {        
            //        Button btn = (Button)seviyeler.Controls["tableLayoutPanel2"].Controls["tableLayoutPanel1"].Controls["button" + (i + 1)];
            //        if (btn.Enabled == false)
            //        {
            //            MessageBox.Show((i + 1) + ". Seviyedesiniz" + Environment.NewLine + "Tamam tuşunun ardından süre başlayacak ve en kısa sürede yanıtlamaya çalışmalısınız.");
            //            level = i + 1;
            //        }
            //    }
            //}

            switch (level)
            {
                case 1:
                    MessageBox.Show("Şu an ilk seviyedesiniz\nTamam tuşunun ardından süre başlayacak ve en kısa sürede yanıtlamaya çalışmalısınız.", "Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);

                    break;
                case 2: case 3: case 4:
                    MessageBox.Show(level + ". Seviyedesiniz\nTamam tuşunun ardından süre başlayacak ve en kısa sürede yanıtlamaya çalışmalısınız.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    break;
                case 5:
                    if (yıldız < 11)
                    {
                        MessageBox.Show("En son Seviyedesiniz\nTamam tuşunun ardından süre başlayacak ve en kısa sürede yanıtlamaya çalışmalısınız.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        break;
                    }
                    MessageBox.Show("Bütün Bölümleri bitirmiş Durumdasınız.\nLütfen oynamak istediğiniz seviyeyi kendiniz seçin", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    panel1.Controls.Clear();
                    panel1.Controls.Add(seviyeler);
                    break;

            }



            if (level!= 5 || yıldız<11)
            {            
                Zaman = Stopwatch.StartNew();
                panel1.Controls.Clear();

                sorular = new sorular(level,skr,aSayfa.AnasayfaIslem);
                verilenSüre = 120 * level;
                sorularInit();
                sorular.BeşliSoruAç();
                sorular.timer1.Start();
                panel1.Controls.Add(sorular);
            }
          

        }
        void SeviyelerHndl(object sender, EventArgs e)
        {

            panel1.Controls.Clear();
            panel1.Controls.Add(seviyeler);

        }
        void ÇıkışHndl(object sender, EventArgs e)
        {

            Application.Exit();
        }
      
        //seviyeler
        void SeviyelerGeriHndl(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            aSayfa.anasayfaYenile();
            panel1.Controls.Add(aSayfa);
            

        }
        void SeviyelerOynaHndl(object sender, EventArgs e)
        {
            int level = int.Parse(((Button)sender).Name.Replace("button",""));
            Oynananlevel = level;
            Zaman = Stopwatch.StartNew();
            panel1.Controls.Clear();

            sorular = new sorular(Oynananlevel, skr,seviyeler.SeviyefaIslem);
            verilenSüre = 120 * Oynananlevel;
            sorularInit();
            sorular.BeşliSoruAç();
            sorular.timer1.Start();
            panel1.Controls.Add(sorular);

        }
        //sorular

        string nl = Environment.NewLine;
        void Bitir(bool SüremiBitti=false, bool Gerimi = false)
        {
        
            bool drm = true;
            level = Oynananlevel;
            string süreBitti = "";
            if (SüremiBitti)
            {
                süreBitti = "Ne Yazıkki Süreniz Bitti"+nl;
            }
            int Dr, Skr;
            Dr = sorular.DogruYanıt;
            Skr = skr.skorHesapla(Dr, Zaman.ElapsedMilliseconds,level);

            if (!Gerimi)
            {
                if (Dr < 11)
                {
                    MessageBox.Show(süreBitti + "Malesef Sonraki Seviyeye Geçecek Kadar soru bilemediniz!" + nl + nl + "Toplam Doğru Yanıtınız:" + Dr, "Malesef...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    drm = false;
                }
                else if (Dr < 16)
                {
                    MessageBox.Show(süreBitti + "Seviyeyi Bir Yıldızla Açtınız!" + nl + nl + "Toplam Doğru Yanıtınız:" + sorular.DogruYanıt + nl + "Skorunuz:" + Skr, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (Dr < 19)
                {
                    MessageBox.Show(süreBitti + "Seviyeyi İki Yıldızla Açtınız!" + nl + nl + "Toplam Doğru Yanıtınız:" + Dr + nl + "Skorunuz:" + Skr, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(süreBitti + "Tebrikler! Seviyenin Tüm  Yıldızlarını Açtınız!" + nl + nl + "Toplam Doğru Yanıtınız:" + Dr + nl + "Skorunuz:" + Skr, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                drm = true;
            }

            if (drm)
            {
                skr.skorGüncelle(level, Skr, Dr);
                LevelAç(skr);

            }
            panel1.Controls.Clear();        
            panel1.Controls.Add(seviyeler);
            sorular.timer1.Stop();
        }

        void SorularKontrolEtHndl(object sender, EventArgs e)
        {

            if (!sorular.PaslarıGetir)
            {
                for (int i = 0; i < 5; i++)
                {

                    Sorucuk sor = (Sorucuk)sorular.flowLayoutPanel1.Controls[i];
                    if (sor.IslemDogrumu())
                    {

                        sorular.DogruYanıt++;
                    }

                    if (!sor.Pasmı)
                    {
                        sorular.KalanSoru--;
                    }
                        
                }
                Button btn = (Button)sender;
                btn.Enabled = false;
                Zaman.Stop();
                bekle(3000);
                Zaman.Start();
                btn.Enabled = true;
                sorular.BeşliSoruAç();
                if (sorular.PasSayı == 0&& sorular.PaslarıGetir)
                {
                    Bitir();
                
                    return;
                }
             
            }
            else
            {
                for (int i = 0; i < sorular.PasSayı; i++)
                {

                    Sorucuk sor = (Sorucuk)sorular.flowLayoutPanel1.Controls[i];
                    if (sor.IslemDogrumu())
                    {
                        sorular.DogruYanıt++;                        
                    }
                    sorular.KalanSoru--;
                    
                }

                Bitir();
                return;
            }
          
                  
            sorular.BilgiGüncelle();

        }

        void SorularGeriHndl(object sender, EventArgs e)
        {
            Zaman.Stop();
            if (DialogResult.Yes== MessageBox.Show("Bölümü iptal edip geri çıkmak istediğinize emin misiniz?","Uyarı",MessageBoxButtons.YesNo,MessageBoxIcon.Warning,MessageBoxDefaultButton.Button2))
            {
                Bitir(Gerimi:true);       
            }
            else
            {
                Zaman.Start();
            }


            
        }

        private void button2_Click(object sender, EventArgs e)
        {


            //sorus = new Sorucuk[5];
            //for (int i = 0; i < 5; i++)
            //{

            //    Sorucuk a =  (Sorucuk)flowLayoutPanel1.Controls[i];
            //    a.IslemDogrumu();

            //}
        }
        int verilenSüre = 0;
        public void timer1_Tick(object sender, EventArgs e)
        {


            TimeSpan çıkan = TimeSpan.FromSeconds(verilenSüre);

            DateTime dateTime = DateTime.Today.Add(çıkan);
            dateTime = dateTime.AddMilliseconds(-(Zaman.ElapsedMilliseconds));

            sorular.SaatGöster( dateTime.ToString("mm:ss"));
            
            if (verilenSüre * 1000 < Zaman.ElapsedMilliseconds)
            {
                Zaman.Stop();
                sorular.timer1.Stop();
                Bitir(true);               
               

             //   MessageBox.Show("Süreniz bitti" + TutulanKelimem);
              
            }
        }


        void bekle(int ms)
        {
            Stopwatch say = Stopwatch.StartNew();
            do
            {
                System.Threading.Thread.Sleep(10);
                Application.DoEvents();

                if (say.ElapsedMilliseconds>ms)
                {
                    break;
                }
            } while (true);

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
           
        }


     
        int r = 255, g = 0, b = 0;
        private void timer1_Tick_1(object sender, EventArgs e)
        {
           


            if (r > 0 && b == 0)
            {
                r--;
                g++;
            }
            if (g > 0 && r == 0)
            {
                g--;
                b++;
            }
            if (b > 0 && g == 0)
            {
                r++;
                b--;
            }

            aSayfa.label1.ForeColor = Color.FromArgb(255, r, g, b);
        }
    }
}
