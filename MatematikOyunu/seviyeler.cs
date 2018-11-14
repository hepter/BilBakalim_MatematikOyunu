using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace MatematikOyunu
{
    public partial class seviyeler : UserControl
    {
        //DoulbeBuffered
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;   
                return cp;
            }
        }
        public seviyeler()
        {
            InitializeComponent();
    
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {
            
        }
        public void SkorYansıt(Skor skr)
        {
            string nl = Environment.NewLine;
            string str = "~Yanıt~"+ nl + "{0}" + nl + "~Skor~" + nl + "{1}";
            for (int i = 0; i < 5; i++)
            {
                Label lbl = (Label)tableLayoutPanel1.Controls["label"+(i+6)];
                lbl.Text=string.Format(str, skr.skorDoğruSayı(i) + "/20", skr.SkorGetir(i));

            }

        }

        RadioButton currentChecked;
        public Islem SeviyefaIslem = Islem.random;
        private void RadioKontrol(object sender, EventArgs e)
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
            switch (id - 1)
            {
                case 0:
                    SeviyefaIslem = Islem.random;
                    break;
                case 1:
                    SeviyefaIslem = Islem.top;
                    break;
                case 2:
                    SeviyefaIslem = Islem.cık;
                    break;
                case 3:
                    SeviyefaIslem = Islem.carp;
                    break;
                case 4:
                    SeviyefaIslem = Islem.böl;
                    break;
            }

        }

        private void seviyeler_Load(object sender, EventArgs e)
        {

      
            currentChecked = radioButton1;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }
    }
    //public static class ControlExtentions
    //{
    //    /// <summary>
    //    /// DoubleBuffer
    //    /// </summary>
    //    /// <param name="control">DoubleBuffer Yapıacak obje</param>
    //    /// <param name="setting">true yaparak doublebuffer'i aç</param>
    //    public static void DoubleBuffered(this Control control, bool setting)
    //    {
    //        Type controlType = control.GetType();
    //        PropertyInfo pi = controlType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
    //        pi.SetValue(control, setting, null);
    //    }
    //}
}
