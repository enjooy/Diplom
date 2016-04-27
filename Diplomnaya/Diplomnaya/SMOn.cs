using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplomnaya
{
    class SMOn
    {
        public uint n;
        public double Lambda;
        public double Mu;
        public double p0;
        public double ro;

        public SMOn( uint n, double Lambda, double Mu )
        {
            this.n = n;
            this.Lambda = Lambda;
            this.Mu = Mu;
            ro = Lambda / Mu;

            init();
        }

        private void init()
        {
            double mnoj = 1;
            for (int i = 0; i < n; i++)
            {
                mnoj *= ro / (i + 1);
                p0 += mnoj;
            }
            p0 = 1 / p0;
        }

        public double pk( uint k ) {
            double mnoj = 1;
            if ( k == 0 )
            {
                return p0;
            }
            else
            {
                for (int i = 1; i <= k; i++)
                {
                    mnoj *= i;
                }
                return p0 * Math.Pow(ro, k) / mnoj;
            }
        }
    }
}
