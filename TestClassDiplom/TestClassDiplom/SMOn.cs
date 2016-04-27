using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplomnaya
{
    class SMOn
    {
    //  Объявление публичных переменных
        public uint     n;      // n - Количество элементов СМО
        public double   Lambda; // Lambda - Интенсивность поступления заявок в СМО
        public double   Mu;     // Mu - Интенсивность обслуживания заявки в СМО
        public double   p0;     // p0 - Вероятность нулевого состояния
        public double   ro;     // ro - приведенная интенсивность потока заявок или интенсивность нагрузки канала. Она выражает среднее число заявок, приходящее за среднее время обслуживания одной заявки.
        public double   potk;   // potk - Вероятность отказа
        public double   qu;     // qu - Относительная пропускная способность (Вероятность того, что заявка будет обслужена)

        //  Объявление приватных переменных
        private bool b = false;     // Нужна для проверки инициализации коэффициентов
        private struct firstData    // Структура для сохранения входных данных
        {
            public double   _1el;
            public uint     _1elinfo;
            public double   _2el;
            public uint     _2elinfo;
            public uint     sc;
            public uint     flag;
        }
        private firstData fd;

    //  Конструкторы
        public SMOn() {
        }

        public SMOn( uint n, double _1el, uint lt, double _2el, uint mt, uint ct, uint flag )
        {
            this.n = n;
            fd._1el =       _1el;
            fd._1elinfo =   lt;
            fd._2el =       _2el;
            fd._2elinfo =   mt;
            fd.sc =         ct;
            fd.flag =       flag;

            switch ( flag )
            {
                case 0:
                    Lambda = convint( ct, _1el, lt );
                    Mu = convint( ct, _2el, mt );
                    ro = Lambda / Mu;
                    break;
                    
                case 1:
                    Lambda = getintime( lt, _1el, ct );
                    Mu = convint( ct, _2el, mt );
                    ro = Lambda / Mu;
                    break;

                case 2:
                    Lambda = convint( ct, _1el, lt );
                    Mu = getintime( mt, _2el, ct );
                    ro = Lambda / Mu;
                    break;

                case 3:
                    Lambda = getintime( lt, _1el, ct );
                    Mu = getintime( mt, _2el, ct );
                    ro = Lambda / Mu;
                    break;
            }
        } 
        // n - Количество элементов
        // ct - Система СИ (В какой единице времени будут производится вычисления)
        // flag -   0 - Если первый элемент выражен через интенсивность поступления, а второй через интенсивность обработки соответственно
        //          1 - Если первый элемент дан во времени выполнения, а второй в интенсивности обработки
        //          2 - Если второй элемент дан во времени выполнения, а первый в интенсивности обслуживания
        //          3 - Если все два элемента даны во времени выполнения

        //  Описание методов
        private void init() // Для того, чтобы каждый раз не находить коэффициенты, мы их сохраняем
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
                p0 = 1 / p0;
                b = true;
            }
        }

        public void outUslZadach() {
            Console.WriteLine( "N = " + n + ";" );

            switch ( fd.flag )
            {
                case 0:
                    switch ( fd._1elinfo )
                    {
                        case 0:
                            Console.WriteLine( "Lambda = " + fd._1el + " Заявок/сек" );
                            break;
                        case 1:
                            Console.WriteLine( "Lambda = " + fd._1el + " Заявок/мин" );
                            break;
                        case 2:
                            Console.WriteLine( "Lambda = " + fd._1el + " Заявок/час" );
                            break;
                        case 3:
                            Console.WriteLine( "Lambda = " + fd._1el + " Заявок/день" );
                            break;
                        default:
                            break;
                    }

                    switch ( fd._2elinfo )
                    {
                        case 0:
                            Console.WriteLine( "Mu = " + fd._2el + " Заявок/сек" );
                            break;
                        case 1:
                            Console.WriteLine( "Mu = " + fd._2el + " Заявок/мин" );
                            break;
                        case 2:
                            Console.WriteLine( "Mu = " + fd._2el + " Заявок/час" );
                            break;
                        case 3:
                            Console.WriteLine( "Mu = " + fd._2el + " Заявок/день" );
                            break;
                        default:
                            break;
                    }
                    break;
                case 1:
                    switch ( fd._1elinfo )
                    {
                        case 0:
                            Console.WriteLine( "За " + fd._2el + " сек. приходит 1 заявка" );
                            break;
                        case 1:
                            Console.WriteLine( "За " + fd._2el + " мин. приходит 1 заявка" );
                            break;
                        case 2:
                            Console.WriteLine( "За " + fd._2el + " ч. приходит 1 заявка" );
                            break;
                        case 3:
                            Console.WriteLine( "За " + fd._2el + " д. приходит 1 заявка" );
                            break;
                        default:
                            break;
                    }
                    switch ( fd._2elinfo )
                    {
                        case 0:
                            Console.WriteLine( "Mu = " + fd._2el + " Заявок/сек" );
                            break;
                        case 1:
                            Console.WriteLine( "Mu = " + fd._2el + " Заявок/мин" );
                            break;
                        case 2:
                            Console.WriteLine( "Mu = " + fd._2el + " Заявок/час" );
                            break;
                        case 3:
                            Console.WriteLine( "Mu = " + fd._2el + " Заявок/день" );
                            break;
                        default:
                            break;
                    }
                    break;
                case 2:
                    switch ( fd._1elinfo )
                    {
                        case 0:
                            Console.WriteLine( "Lambda = " + fd._1el + " Заявок/сек" );
                            break;
                        case 1:
                            Console.WriteLine( "Lambda = " + fd._1el + " Заявок/мин" );
                            break;
                        case 2:
                            Console.WriteLine( "Lambda = " + fd._1el + " Заявок/час" );
                            break;
                        case 3:
                            Console.WriteLine( "Lambda = " + fd._1el + " Заявок/день" );
                            break;
                        default:
                            break;
                    }
                    switch ( fd._2elinfo )
                    {
                        case 0:
                            Console.WriteLine( "За " + fd._2el + " сек. обслуживается 1 заявка" );
                            break;
                        case 1:
                            Console.WriteLine( "За " + fd._2el + " мин. обслуживается 1 заявка" );
                            break;
                        case 2:
                            Console.WriteLine( "За " + fd._2el + " ч. обслуживается 1 заявка" );
                            break;
                        case 3:
                            Console.WriteLine( "За " + fd._2el + " д. обслуживается 1 заявка" );
                            break;
                        default:
                            break;
                    }
                    break;
                case 3:
                    switch ( fd._1elinfo )
                    {
                        case 0:
                            Console.WriteLine( "За " + fd._2el + " сек. приходит 1 заявка" );
                            break;
                        case 1:
                            Console.WriteLine( "За " + fd._2el + " мин. приходит 1 заявка" );
                            break;
                        case 2:
                            Console.WriteLine( "За " + fd._2el + " ч. приходит 1 заявка" );
                            break;
                        case 3:
                            Console.WriteLine( "За " + fd._2el + " д. приходит 1 заявка" );
                            break;
                        default:
                            break;
                    }
                    switch ( fd._2elinfo )
                    {
                        case 0:
                            Console.WriteLine( "За " + fd._2el + " сек. обслуживается 1 заявка" );
                            break;
                        case 1:
                            Console.WriteLine( "За " + fd._2el + " мин. обслуживается 1 заявка" );
                            break;
                        case 2:
                            Console.WriteLine( "За " + fd._2el + " ч. обслуживается 1 заявка" );
                            break;
                        case 3:
                            Console.WriteLine( "За " + fd._2el + " д. обслуживается 1 заявка" );
                            break;
                        default:
                            break;
                    }
                    break;
            }
            Console.WriteLine();
            for (int i = 0; i <= n; i++)
            {
                Console.WriteLine( "Вероятность " + i + " состояния: " + "{0:0.000}", pk( (uint) i ) );
            }
            Console.WriteLine();
            Console.WriteLine("Вероятность обработки: " + "{0:0.000}", (1 - pk( n ) ) );
            Console.WriteLine();
            Console.WriteLine("Вероятность отказа: " + "{0:0.000}", pk( n ) );
        }
        // Консольный вывод условия задачи

        public double pk( uint k ) {
            init();
            double mnoj = 1;
            if ( k == 0 )
            {
                return p0;
            }
            else
            {
                for ( int i = 1; i <= k; i++ )
                {
                    mnoj *= i;
                }
                return p0 * Math.Pow( ro, k ) / mnoj;
            }
        }
        // Получить вероятность k - ого состояния

        public double convint( uint i, double t, uint j ) {
            if ( i == j )
            {
                return t;
            }

            switch (i)
            {
                case 0:
                    switch ( j )
                    {
                        case 1:
                            return t / 60;
                        case 2:
                            return t / 3600;
                        case 3:
                            return t / 86400;
                        default:
                            return t;
                    }
                case 1:
                    switch ( j )
                    {
                        case 0:
                            return t * 60;
                        case 2:
                            return t / 60;
                        case 3:
                            return t / 1440;
                        default:
                            return t;
                    }
                case 2:
                    switch ( j )
                    {
                        case 0:
                            return t * 3600;
                        case 1:
                            return t * 60;
                        case 3:
                            return t / 24;
                        default:
                            return t;
                    }
                case 3:
                    switch ( j )
                    {
                        case 0:
                            return t * 86400;
                        case 1:
                            return t * 1440;
                        case 2:
                            return t * 24;
                        default:
                            return t;
                    }
                default:
                    return t;
            }
        }   
        // convint - convert intensity (Конвертация интенсивности поступления или выполнения)
        // i - В какую единицу измерения конвертировать 0 - сек; 1 - мин; 2 - часы; 3 - дни
        // t - Сам параметр интенсивности
        // j - Из какой единицы происходит конвертация 0 - сек; 1 - мин; 2 - часы; 3 - дни

        public double getintime( uint i, double t, uint j )
        {
            if ( i == j )
            {
                return 1 / t;
            }
            switch ( i )
            {
                case 0:
                    switch ( j )
                    {
                        case 1:
                            return 60 / t;
                        case 2:
                            return 3600 / t;
                        case 3:
                            return 86400 / t;
                        default:
                            return 1 / t;
                    }
                case 1:
                    switch (j)
                    {
                        case 0:
                            return t / 60;
                        case 2:
                            return 60 / t;
                        case 3:
                            return 1440 / t;
                        default:
                            return 1 / t;
                    }
                case 2:
                    switch (j)
                    {
                        case 0:
                            return t / 3600;
                        case 1:
                            return t / 60;
                        case 3:
                            return 24 / t;
                        default:
                            return 1 / t;
                    }
                case 3:
                    switch (j)
                    {
                        case 0:
                            return t / 86400;
                        case 1:
                            return t / 1440;
                        case 2:
                            return t / 24;
                        default:
                            return 1 / t;
                    }
                default:
                    return 1 / t;
            }
        }   
        //  getintime - get intensity on time (Получение интенсивности поступления или выполнения по времени)
        //  i - В какой единице времени измеряется выполнения 0 - сек; 1 - мин; 2 - часы; 3 - дни
        //  t - Само время выполнения
        //  j - В какой единице времени получить параметр интенсивности 0 - сек; 1 - мин; 2 - часы; 3 - дни
    }
}
