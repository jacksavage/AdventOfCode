using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2020
{
    internal class Day01 : ISolution
    {
        string ISolution.Run(int part, IEnumerable<string> input)
        {
            int[] nums = input.Select(int.Parse).OrderBy(num => num).ToArray();
            return part switch 
            {
                1 => Part1(nums, target: 2020).ToString(),
                _ => null
            };
        }

        // Find two numbers whose sum is the target and multiply them together.
        // `nums` must be sorted in ascending order
        static int Part1(int[] nums, int target)
        {
            var i = 0; var j = nums.Length - 1;
            var current = nums[i] + nums[j];

            while (current != target)
            {
                if (current > target) j--;
                else i++;

                if (i >= j) return -1;

                current = nums[i] + nums[j];
            }
            
            return nums[i] * nums[j];
        }
    }
}