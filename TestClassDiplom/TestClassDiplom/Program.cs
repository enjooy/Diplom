using Diplomnaya;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplomnaya
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = 0;  // Номер задачи
            switch (n)
            {
                case 0:
                    SMOn s1 = new SMOn(3, 20, 2, 6, 1, 2, 2);
                    s1.outUslZadach();
                    break;
                case 1:
                    SMOn s2 = new SMOn(2, 1.5, 1, 2, 1, 1, 2);
                    s2.outUslZadach();
                    break;
                case 2:
                    SMOnm s3 = new SMOnm(2,1,5,2,20,1,1,2);
                    s3.outUslZadach();
                    break;
                case 3:

                    break;
                default:

                    break;
            }
        }
    }
}
