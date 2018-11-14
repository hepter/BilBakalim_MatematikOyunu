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

namespace MatematikOyunu
{
    public partial class Sorucuk : UserControl
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
        public Sorucuk()
        {
            InitializeComponent();
        }

        public Sorucuk(islem islm)
        {
            InitializeComponent();

            label2.Text = islm.Sayı1.ToString();
            label3.Text = islm.Sayı2.ToString();
            label1.Text = islm.IslemSembolGetir();
            _islem = islm;
            akıllıButton1.AnaIslem = islm;
        }


        private islem _islem;
       public islem islem
       {
            get
            {
                return _islem;
            }
       }
        Color _Renk=Color.Yellow;

        public Color Renk
        {
            get
            {
                return _Renk;
            }

            set
            {

                button1.FlatAppearance.BorderColor = value;
                _Renk= value;
            }
        }


       
        Thread FlipFlop;
        public bool IslemDogrumu()
        {
            int flipflopMs = 150;//flipflopMs*2*flip => toplam Ms
            int flip = 10;
            if (Pasmı)
            {
                return false;
            }

            if (textBox1.Text=="")
            {
                textBox1.Text = "0";
            }
            if (islem.hesapla()==int.Parse(textBox1.Text))
            {


                FlipFlop = new Thread(a => {

                    Color renk = Color.Green;                    
                    Color yedek = Renk;
                    for (int i = 0; i < flip; i++)
                    {
                        Renk = renk;
                        Thread.Sleep(flipflopMs);
                        Renk = yedek;
                        Thread.Sleep(flipflopMs);
                    }
                });
                FlipFlop.Start();
                return true;
            }
            else
            {
                FlipFlop = new Thread(a => {

                    Color renk = Color.Red;              
                    Color yedek = Renk;
                    for (int i = 0; i < flip; i++)
                    {
                        Renk = renk;
                        Thread.Sleep(flipflopMs);
                        Renk = yedek;
                        Thread.Sleep(flipflopMs);
                    }
                });

                FlipFlop.Start();
                return false;
            }
        }




        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.textBox1.TextChanged -= new System.EventHandler(this.textBox1_TextChanged);
            for (int i = 0; i < textBox1.Text.Length; i++)
            {
                char a = Convert.ToChar(textBox1.Text.Substring(i, 1));
                if ((a<48 || a>57)&& '-'!=a)
                {
                    
                    textBox1.Text = textBox1.Text.Replace(a.ToString(), "");
                }
            }
            if (textBox1.Text.Contains("-"))
            {
                textBox1.Text = textBox1.Text.Replace("-", "");
                textBox1.Text = "-" + textBox1.Text;
            }

            textBox1.SelectionStart = textBox1.Text.Length;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);

        }

        private void label2_TextChanged(object sender, EventArgs e)
        {
            OtomatikScale((Label)sender);
        }

        float OtomatikScale(Label lbl, bool ozel = false)
        {

            string txt = lbl.Text;
            float best_size = 0;
            if (txt.Length > 0)
            {
                best_size = 100;

           
                float wid = lbl.DisplayRectangle.Width - 3;
                float hgt = lbl.DisplayRectangle.Height - 3;
             //   hgt = hgt * (float)((ScaleValue - 5) / 100);
              //  wid = wid * (float)((ScaleValue - 5) / 100);
                // Make a Graphics object to measure the text.
                using (Graphics gr = lbl.CreateGraphics())
                {
                    for (int i = 1; i <= 100; i++)
                    {
                        using (Font test_font =
                            new Font(lbl.Font.FontFamily, i))
                        {
                            SizeF text_size =
                                gr.MeasureString(txt, test_font);



                            if ((text_size.Width > wid) ||
                                (text_size.Height > hgt))
                            {
                                best_size = i - 1;
                                break;
                            }
                        }
                    }
                }
                if (ozel)
                {
                    return best_size;
                }
                if (best_size <= 0)
                {
                    best_size = 1;
                }
                // Use that font size.
             

                    lbl.Font = new Font(lbl.Font.FontFamily, best_size, lbl.Font.Style);
              

            }
            return 0;
        }

       bool PasToYanlış = false;
       public bool Pasmı
       {
            get
            {
                return _pasdurum;
            }
        }


        bool _pasdurum = false;
        Color YedekButonClr;
        public void pasDüzelt()
        {

          //  akıllıButton1.BackColor = YedekButonClr;
       //     akıllıButton1.Enabled = true;
            textBox1.Enabled = true;
            Renk = Color.LightBlue;
            _pasdurum = false;
        }
        private void button2_Click(object sender, EventArgs e)
        {
        
            textBox1.Enabled = false;
            Renk = Color.DimGray;
            YedekButonClr= akıllıButton1.BackColor; 
            akıllıButton1.BackColor = Color.DimGray;
            akıllıButton1.Enabled = false;
            if (!PasToYanlış)
            {
                _pasdurum = true;
            }
            else
            {
                _pasdurum = false;
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_DoubleClick(object sender, EventArgs e)
        {

            if (Control.ModifierKeys == Keys.Control)
            {
                string tt = islem.hesapla().ToString();
                ((TextBox)sender).Text= tt;
            }


        }
    }

    class akıllıButton : Button
    {

        private islem _AnaIslem;


        public islem AnaIslem
        {
            get
            {
                return _AnaIslem;
            }
            set
            {
                _AnaIslem = value;
            }
        }
    }

}
