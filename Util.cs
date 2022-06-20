using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CALCULADORA2014
{
    public class Util
    {
        private delegate double RoundingFunction(double value);

        private enum RoundingDirection
        {
            Up,
            Down
        }

        public double RoundUp(double value, int precision)
        {
            return Round(value, precision, RoundingDirection.Up);
        }

        public double RoundDown(double value, int precision)
        {
            return Round(value, precision, RoundingDirection.Down);
        }

        private double Round(double value, int precision, RoundingDirection roundingDirection)
        {
            RoundingFunction roundingFunction = null;
            if (roundingDirection == RoundingDirection.Up)
            {
                roundingFunction = Math.Ceiling;
            }
            else
            {
                roundingFunction = Math.Floor;
            }
            value = value * Math.Pow(10, precision);
            value = roundingFunction(value);
            return value * Math.Pow(10, -1 * precision);
        }

        public Double CalcularEdad(DateTime pFechaNacimiento)
        {
            return DateTime.Today.AddTicks(-pFechaNacimiento.Ticks).Year - 1;
        }
    }
}