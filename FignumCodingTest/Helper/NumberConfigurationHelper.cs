using System.Text.RegularExpressions;
using FignumCodingTest.Models;

namespace FignumCodingTest.Helper
{
    public static class NumberConfigurationHelper
    {
        /// <summary>
        /// Remove any prime numbers filter.
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns>A list of numbers or null</returns>
        public static async Task<Response> RemovePrimeNumbersFilter(string numbers)
        {
            Response response = new Response();
            List<int>? result = null;

            if (!IsStringCorrect(numbers))
            {
                response.CheckAndSortedList = null;
                response.Status = 400;
                response.Errors.InputStringNumber.Add($"The numbers string provided is not in a correct format {numbers} - eg: 1,2,3,4,5");
                return response;
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

            response.Status = 200;
            response.CheckAndSortedList = result;
            return response;
        }

        /// <summary>
        /// Sort a list of numbers
        /// </summary>
        /// <param name="response"></param>
        /// <returns>A sorted list of numbers</returns>
        public static async Task<Response> SortFilter(Response response)
        {
            if (response is { Status: 200, CheckAndSortedList: not null })
            {
                await Task.Run(response.CheckAndSortedList.Sort);
            }

            return response;
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
            Regex reg = new Regex("^([-?0-9]+,*)*$");
            return !string.IsNullOrWhiteSpace(numbers) && reg.IsMatch(numbers);
        }
    }
}
