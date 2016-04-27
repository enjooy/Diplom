using Diplomnaya;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplomnaya
{
    class SMOnm : SMOn
    {
        public uint m;

        private bool b = false;
        private struct firstData
        {
            public double   _1el;
            public uint     _1elinfo;
            public double   _2el;
            public uint     _2elinfo;
            public uint     sc;
            public uint     flag;
        }
        private firstData fd;

        public SMOnm() {

        }
        public SMOnm( uint n, uint m, double _1el, uint lt, double _2el, uint mt, uint ct, uint flag)
        {
            this.n = n;
            this.m = m;

            // Сохранения начальных данных
            fd._1el = _1el;
            fd._1elinfo = lt;
            fd._2el = _2el;
            fd._2elinfo = mt;
                fd.sc = ct;
            fd.flag = flag;

            // Преобразование и сохранение интенсивности поступления и обслуживания
            switch ( flag )
            {
                case 0:
                    Lambda = convint(ct, _1el, lt);
                    Mu = convint(ct, _2el, mt);
                    ro = Lambda / Mu;
                    break;

                case 1:
                    Lambda = getintime(lt, _1el, ct);
                    Mu = convint(ct, _2el, mt);
                    ro = Lambda / Mu;
                    break;

                case 2:
                    Lambda = convint(ct, _1el, lt);
                    Mu = getintime(mt, _2el, ct);
                    ro = Lambda / Mu;
                    break;

                case 3:
                    Lambda = getintime(lt, _1el, ct);
                    Mu = getintime(mt, _2el, ct);
                    ro = Lambda / Mu;
                    break;
            }
        }

        private void init()
        {
            if ( b == false )
            {
                double mnoj = 1;
                p0 = 1;
                for ( int i = 1; i <= n; i++ )
                {
                    mnoj *= ro / i;
                    p0 += mnoj;
                }
                mnoj *= ( ro / n - Math.Pow( ro / n, m + 1 ) ) / ( 1 - ro / n );
                mnoj += p0;
                p0 = 1 / mnoj;
                b = true;
            }
        }

        public new double pk( uint k )
        {
            init();
            double mnoj = 1;
            if (k == 0)
            {
                return p0;
            }
            else
            {
                if ( k <= n )
                {
                    for ( int i = 1; i <= k; i++ )
                    {
                        mnoj *= i;
                    }
                    return p0 * Math.Pow(ro, k) / mnoj;
                }
                else
                {
                    for (int i = 1; i <= n; i++)
                    {
                        mnoj *= i;
                    }
                    return Math.Pow( ro, k ) * p0 / ( Math.Pow( n, ( k - n ) ) * mnoj );
                }
            }
        }

        public new void outUslZadach()
        {
            Console.WriteLine("N = " + n + ";");

            switch (fd.flag)
            {
                case 0:
                    switch (fd._1elinfo)
                    {
                        case 0:
                            Console.WriteLine("Lambda = " + fd._1el + " Заявок/сек");
                            break;
                        case 1:
                            Console.WriteLine("Lambda = " + fd._1el + " Заявок/мин");
                            break;
                        case 2:
                            Console.WriteLine("Lambda = " + fd._1el + " Заявок/час");
                            break;
                        case 3:
                            Console.WriteLine("Lambda = " + fd._1el + " Заявок/день");
                            break;
                        default:
                            break;
                    }

                    switch (fd._2elinfo)
                    {
                        case 0:
                            Console.WriteLine("Mu = " + fd._2el + " Заявок/сек");
                            break;
                        case 1:
                            Console.WriteLine("Mu = " + fd._2el + " Заявок/мин");
                            break;
                        case 2:
                            Console.WriteLine("Mu = " + fd._2el + " Заявок/час");
                            break;
                        case 3:
                            Console.WriteLine("Mu = " + fd._2el + " Заявок/день");
                            break;
                        default:
                            break;
                    }
                    break;
                case 1:
                    switch (fd._1elinfo)
                    {
                        case 0:
                            Console.WriteLine("За " + fd._2el + " сек. приходит 1 заявка");
                            break;
                        case 1:
                            Console.WriteLine("За " + fd._2el + " мин. приходит 1 заявка");
                            break;
                        case 2:
                            Console.WriteLine("За " + fd._2el + " ч. приходит 1 заявка");
                            break;
                        case 3:
                            Console.WriteLine("За " + fd._2el + " д. приходит 1 заявка");
                            break;
                        default:
                            break;
                    }
                    switch (fd._2elinfo)
                    {
                        case 0:
                            Console.WriteLine("Mu = " + fd._2el + " Заявок/сек");
                            break;
                        case 1:
                            Console.WriteLine("Mu = " + fd._2el + " Заявок/мин");
                            break;
                        case 2:
                            Console.WriteLine("Mu = " + fd._2el + " Заявок/час");
                            break;
                        case 3:
                            Console.WriteLine("Mu = " + fd._2el + " Заявок/день");
                            break;
                        default:
                            break;
                    }
                    break;
                case 2:
                    switch (fd._1elinfo)
                    {
                        case 0:
                            Console.WriteLine("Lambda = " + fd._1el + " Заявок/сек");
                            break;
                        case 1:
                            Console.WriteLine("Lambda = " + fd._1el + " Заявок/мин");
                            break;
                        case 2:
                            Console.WriteLine("Lambda = " + fd._1el + " Заявок/час");
                            break;
                        case 3:
                            Console.WriteLine("Lambda = " + fd._1el + " Заявок/день");
                            break;
                        default:
                            break;
                    }
                    switch (fd._2elinfo)
                    {
                        case 0:
                            Console.WriteLine("За " + fd._2el + " сек. обслуживается 1 заявка");
                            break;
                        case 1:
                            Console.WriteLine("За " + fd._2el + " мин. обслуживается 1 заявка");
                            break;
                        case 2:
                            Console.WriteLine("За " + fd._2el + " ч. обслуживается 1 заявка");
                            break;
                        case 3:
                            Console.WriteLine("За " + fd._2el + " д. обслуживается 1 заявка");
                            break;
                        default:
                            break;
                    }
                    break;
                case 3:
                    switch (fd._1elinfo)
                    {
                        case 0:
                            Console.WriteLine("За " + fd._2el + " сек. приходит 1 заявка");
                            break;
                        case 1:
                            Console.WriteLine("За " + fd._2el + " мин. приходит 1 заявка");
                            break;
                        case 2:
                            Console.WriteLine("За " + fd._2el + " ч. приходит 1 заявка");
                            break;
                        case 3:
                            Console.WriteLine("За " + fd._2el + " д. приходит 1 заявка");
                            break;
                        default:
                            break;
                    }
                    switch (fd._2elinfo)
                    {
                        case 0:
                            Console.WriteLine("За " + fd._2el + " сек. обслуживается 1 заявка");
                            break;
                        case 1:
                            Console.WriteLine("За " + fd._2el + " мин. обслуживается 1 заявка");
                            break;
                        case 2:
                            Console.WriteLine("За " + fd._2el + " ч. обслуживается 1 заявка");
                            break;
                        case 3:
                            Console.WriteLine("За " + fd._2el + " д. обслуживается 1 заявка");
                            break;
                        default:
                            break;
                    }
                    break;
            }
            for (int i = 0; i <= n + m; i++)
            {
                Console.WriteLine("Вероятность " + i + " состояния: " + pk((uint)i));
            }
        }
    }
}