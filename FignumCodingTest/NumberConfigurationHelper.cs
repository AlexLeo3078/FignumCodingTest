using System.Text.RegularExpressions;

namespace FignumCodingTest
{
    public static class NumberConfigurationHelper
    {
        /// <summary>
        /// Remove any prime numbers and sort.
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns>A list of numbers or null</returns>
        public static async Task<List<int>?> CheckAndSort(string numbers)
        {
            List<int>? result = null;

            if (!IsStringCorrect(numbers))
            {
                return result;
            }

            var numbersList = numbers.Split(',').ToList();
            await Task.Run(() =>
            {
                result = new List<int>();
                foreach (var number in numbersList)
                {
                    if (int.TryParse(number, out int num))
                    {
                        if (!IsPrime(num))
                        {
                            result.Add(num);
                        }
                    }
                }
            });

            return result;
        }

        /// <summary>
        /// Check if a number is prime. There is a property of being primed called primality.
        /// Is a simple solution called trial division, tests whether a number N is a multiple
        /// of any integer between 2 and  sqrt(N) - 
        /// </summary>
        /// <param name="number"></param>
        /// <returns>A boolean value indicating whether a number is prime or not.</returns>
        private static bool IsPrime(int number)
        {
            switch (number)
            {
                case <= 1:
                    return false;
                case 2:
                    return true;
            }

            if (number % 2 == 0) return false;

            var limit = (int)Math.Floor(Math.Sqrt(number));

            for (var i = 3; i <= limit; i++)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        ///  Check if the number input strings is in the correct format eg: 1,2,3,4,5
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns>A boolean value indicating whether the number input strings is in the correct format.</returns>
        private static bool IsStringCorrect(string numbers)
        {
            Regex reg = new Regex("^([0-9]+,*)*$");
            return !string.IsNullOrWhiteSpace(numbers) && reg.IsMatch(numbers);
        }
    }
}
