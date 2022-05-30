using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment1.Models
{
    public class DoctorModel
    {
        private static readonly double threshold_hypothermia = 36.0d;
        private static readonly double threshold_fever = 38.0d;

        public static string CheckTemperature(string temperature, string measurement)
        {
            if (string.IsNullOrEmpty(temperature)
            || !double.TryParse(temperature.Replace('.', ','), out double result)) {
                return "Invalid input, enter a decimal value.";
            }

            if (!string.IsNullOrEmpty(measurement) && measurement == "fahrenheit") {              
                result = (result - 32.0d) * 5.0d / 9.0d;
            }

            return result <= threshold_hypothermia
                ? "You got hypothermia!" 
                : result >= threshold_fever
                ? "You got fever!"
                : "You're fine, no hypothermia or fever detected.";
        }
    }
}
