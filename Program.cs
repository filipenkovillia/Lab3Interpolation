using System;

namespace Lab3Interpolation
{
    class Program
    {
        private const double Dx = 32;
        private const double tau = 18;
        private const double T = tau;
        private const double tau0 = 7;
        private const double k = 1.5;
        private const double x_t_i = 250;
        private const double x_t_i_1 = 250;
        private const double x_t_i_2 = 250 + T;
        private const double Mx = 240;
        private const double koef = 0.55;

        static void Main(string[] args)
        {
            double t = koef * tau0;
            double t1 = koef * tau0;
            double t2 = koef * tau0 + T;
            Console.WriteLine($"Extrapolation in moment {koef * tau0} : {ExtrapolationX(koef * tau0)}");
            Console.WriteLine($"Interpolation in moment {Interpolation(t, t1, t2)}");
        }

        private static double AutocorrelationKx(double t)
        {
            if (-t == tau)
            {
                return 0.0;
            }
            else
            {
                return Dx * (1.0 - Math.Pow(t / tau, k));
            }
        }

        private static double ExtrapolationX(double t)
        {
            return AutocorrelationKx(t) / AutocorrelationKx(0) * (x_t_i - Mx) + Mx;
        }

        private static double Interpolation(double t, double t1, double t2)
        {
            
            double firstAddition = ((AutocorrelationKx(t - t1) * AutocorrelationKx(0) - 
                                     AutocorrelationKx(t - t2) * AutocorrelationKx(T)) / 
                                    (Math.Pow(AutocorrelationKx(0), 2.0) - Math.Pow(AutocorrelationKx(T), 2.0))) * 
                                   x_t_i_2;
            double secondAddition = ((AutocorrelationKx(t - t2) * AutocorrelationKx(0) - 
                                      AutocorrelationKx(t - t1) * AutocorrelationKx(T)) / 
                                     (Math.Pow(AutocorrelationKx(0), 2.0) - Math.Pow(AutocorrelationKx(T), 2.0))) * 
                                    x_t_i_1;
            double thirdAddition = Mx * ((AutocorrelationKx(t - t1) + AutocorrelationKx(t - t2)) / 
                                         (AutocorrelationKx(0) - AutocorrelationKx(T)) - 1);

            return firstAddition + secondAddition + thirdAddition;
        }
    }
}
