using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatematikOyunu
{
    public enum Islem
    {
      random,
      top,
      cık,
      carp,
      böl
    }
    
    public enum Hane
    {
      sıfır,
      bir,
      iki,
      uc,
      dort,
      bes,
      alti,   
    }

    public class islem
    {

        public int Sayı1
        {
            get
            {
                return (int)s1;
            }
        }
        public int Sayı2
        {
            get
            {
                return (int)s2;
            }
        }



        float s1, s2;
        Islem isl;
        Hane hn;


        public islem(int sayı1,int sayı2, Islem islem)
        {
            s1 = sayı1;
            s2 = sayı2;
            isl = islem;
        }


        public islem(Islem islem, Hane hane)
        {
            Islem AktifIslem = islem;
            int say = (int)hane;
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            int aralık1, aralık2;
            aralık1 = (say == 1 || say == 2) ? 1 : ((say == 2 || say == 3) ? 10 : 100);
            aralık2 = (say == 1) ? 10 : ((say == 2|| say == 3) ? 100 : (say == 4)?1000:2000);
            //s1 = sayı1;
            //s2 = sayı2;
            if ((int)AktifIslem == 0)
            {
                AktifIslem = (Islem)rnd.Next(1,5);
            }

            switch (AktifIslem)
            {
                case Islem.top:
                case Islem.cık:                
                case Islem.carp:
                    s1 = rnd.Next(aralık1, aralık2);
                    s2 = rnd.Next(aralık1, aralık2);


                    break;
                case Islem.böl:
                    do
                    {
                        rnd = new Random(Guid.NewGuid().GetHashCode());
                        float bölm, bölm2;
                        //int altlimit = (int)Math.Pow(10, (Math.Sign((say - 2) * -1) == 1) ? (say - 2) + 1 : (say - 2));
                        //  s1 = rnd.Next(altlimit, (int)Math.Pow(10, say) - 1);
                        //  s2 = rnd.Next(altlimit, (int)Math.Pow(10, say) - 1);
                        s1 = rnd.Next(aralık1, aralık2);
                        s2 = rnd.Next(aralık1, aralık2);
                        bölm = s1 / s2;
                        //bölm2 = s2 / s1;
                        if ((float)(int)bölm == bölm)
                        {
                            break;
                        }
                            
                    } while (true);
                    break;
            }

           



           
            isl = AktifIslem;
            hn = hane;
        }
       int toplam = 0;
       public int Toplam
        {
            get
            {
                return toplam;
            }
        }

        public string IslemSembolGetir()
        {
            switch (isl)
            {
                case Islem.top:

                    return "+";
                  
                case Islem.cık:


                    return "-" ;
                case Islem.carp:


                    return "X" ;
                case Islem.böl:


                    return "/" ;
            }


            return "" ;

        }


        public int hesapla()
        {

           

            switch (isl)
            {
                case Islem.top:
                    toplam= (int)s1 + (int)s2;


                    break;
                case Islem.cık:
                    toplam = (int)s1 - (int)s2;


                    break;
                case Islem.carp:
                    toplam = (int)s1 * (int)s2;


                    break;
                case Islem.böl:
                    toplam = (int)s1 / (int)s2;

                    break;
            }




            return toplam;
        }







    }
}
