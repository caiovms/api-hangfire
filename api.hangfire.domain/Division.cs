﻿using System;

namespace api.hangfire.domain
{
    public class Division
    {
        public void DivisionRandom()
        {
            try
            {
                var number1 = new Random().Next(0, 10);
                var number2 = new Random().Next(0, 10);

                var result = number1 / number2;
            }
            catch (DivideByZeroException ex)
            {
                throw new Exception($"Division by zero {ex.Message}");
            }
        }
    }
}
