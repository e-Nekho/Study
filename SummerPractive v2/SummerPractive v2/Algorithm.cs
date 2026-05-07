using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class Algorithm
{
    public static double Function(double targetX, int steps = 1000)
    {
        double x = 1.0;
        double y = 1.0;
        double xEnd = 2.0;

        double h = (targetX - x) / steps;

        for (int i = 0; i < steps; i++)
        {
            double dy = Equation(x, y);
            x += h;
            y += h * dy;
        }
        return y;
    }

    private static double Equation(double x, double y)
    {
        if (x == 0) return double.NaN;
        return y * (1 - (x + 2) * y) / (2 * x);
    }
}
