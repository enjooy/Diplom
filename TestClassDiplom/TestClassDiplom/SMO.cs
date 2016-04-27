using Diplomnaya;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplomnaya
{
    class SMO : SMOnm
    {
        public double Nu;
        public double beta;

        private bool b = false;

        public SMO( uint n, uint m, double _1el, uint lt, double _2el, uint mt, uint ct, uint flag, double Nu ){
            this.Nu = Nu;
            beta = Nu / Mu;
            init();
        }

        private void init()
        {
            if (b == false)
            {
                double mnoj = 1;
                double mnoj2 = 1;
                double summ = 0;
                p0 = 1;
                for (int i = 1; i <= n; i++)
                {
                    mnoj *= ro / i;
                    p0 += mnoj;
                }
                for (int i = 1; i <= m; i++)
                {
                    mnoj2 *= ro / (n + i * beta);
                    summ += mnoj2;
                }
                p0 += summ * mnoj;
                p0 = 1 / p0;
                b = true;
            }
        }

        public new double pk(uint k)
        {
            init();
            double mnoj = 1;
            double mnoj2 = 1;
            if (k == 0)
            {
                return p0;
            }
            else
            {
                if (k <= n)
                {
                    for (int i = 1; i <= k; i++)
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

                    for (int i = 1; i <= m; i++)
                    {
                        mnoj2 *= n + i * beta;
                    }
                    return Math.Pow(ro, k) * p0 / (mnoj * mnoj2);
                }
            }
        }
    }
}
