using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatematikOyunu
{
    public class Skor
    {



        string[] seviyeler = new string[5];
        string[] skorlar = new string[5];


        public Skor(string Txt, bool DosyadanMı = true)
        {
            metindenSkor(Txt, DosyadanMı);
        }

        string TxtKonum;
        string TxtData;
        void metindenSkor(string Txt, bool DosyadanMı)
        {

            if (DosyadanMı)
            {
                TxtKonum = Txt;
                string SkorKonum = Txt;
                string t1, t2;
                int i = 0;
                if (File.Exists(SkorKonum))
                {

                    StreamReader file = new StreamReader(SkorKonum);
                   
                   
                    TxtData = file.ReadToEnd();
                    foreach (string satır in TxtData.Split(new char[] { Environment.NewLine.ToCharArray()[0] }))
                    {
                        if (satır==""|| satır == "\n" || satır == "\r\n")
                        {
                            continue;
                        }
                        t1 = satır.Split(new char[] { '|' })[0];
                        t2 = satır.Split(new char[] { '|' })[1];
                        seviyeler[i] = t1;
                        skorlar[i] = t2;
                        i++;
                    }

                    file.Close();
                    file.Dispose();
                }
                else
                {
                    string a = Environment.NewLine;
                    string metin = "0/20|0" + a + "0/20|0" + a + "0/20|0" + a + "0/20|0" + a + "0/20|0";

                    foreach (string satır in metin.Split(new char[] { Environment.NewLine.ToCharArray()[0] }))
                    {
                        if (satır == "" || satır == "\n" || satır == "\r\n")
                        {
                            continue;
                        }
                        t1 = satır.Split(new char[] { '|' })[0];
                        t2 = satır.Split(new char[] { '|' })[1];
                        seviyeler[i] = t1;
                        skorlar[i] = t2;
                        i++;
                    }

                    File.WriteAllText(SkorKonum, metin);
                }
            }
            else
            {
                string t1, t2;
                int i = 0;
                foreach (string satır in Txt.Split(new char[] { Environment.NewLine.ToCharArray()[0] }))
                {
                    t1 = satır.Split(new char[] { '|' })[0];
                    t2 = satır.Split(new char[] { '|' })[1];
                    seviyeler[i] = t1;
                    skorlar[i] = t2;
                    i++;
                }
            }

        }



        public int SkorGetir(int LevelNo)
        {
            int s1 = int.Parse(skorlar[LevelNo]);
            return s1;
        }
  
        public int skorDoğruSayı(int LevelNo)
        {
            int s1 = int.Parse(seviyeler[LevelNo].Split(new char[] { '/' })[0]);
            return s1;
        }

        public int skorHesapla(float DogruYanıt, float GeçenMilisaniye,int level)

        {
            float oran = (250 * DogruYanıt) / 2;
            float oran2 = ((GeçenMilisaniye / 10000) + 100) / (GeçenMilisaniye / 10000);

            return (int)(oran * oran2*((level/2)+1));
        }
        public int LevelGetir()
        {
            int lvl=1;
            for (int i = 4; i >=1; i--)
            {
                int no = int.Parse(seviyeler[i-1].Split(new char[] { '/' })[0]);
                if (no>10)
                {
                 
                        return i+1;
                    
                    
                }
            }

            return lvl;
        }
        public int yıldızGetir(int SeviyeNo)
        {
            int bilinen = int.Parse(seviyeler[SeviyeNo].Split(new char[] { '/' })[0]);

            if (bilinen > 18)
            {
                return 3;
            }
            else if (bilinen > 15)
            {
                return 2;
            }
            else if (bilinen > 10)
            {
                return 1;

            }
            else
            {
                return 0;
            }

        }

        public void skorGüncelle(int level, int Skor, int DogruSayı)
        {

            //  string ff = "{0}/20|{1}";

            seviyeler[level-1] = DogruSayı.ToString()+"/20";
            skorlar[level-1] = Skor.ToString();

            skorKaydet();
        }

        public void skorKaydet()
        {
            
            string ff = "{0}|{1}",data="";
         
            for (int i = 0; i < 5; i++)
            {
                data = data +String.Format(ff, seviyeler[i], skorlar[i])+Environment.NewLine;
            }

            File.WriteAllText(TxtKonum, data);
            
        }
    }
}
