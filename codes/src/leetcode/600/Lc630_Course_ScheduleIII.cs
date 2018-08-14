using System;
using System.Collections.Generic;
using System.Linq;

using alg;

/*
 * tags: heap, greedy
 * greedy: sort the courses by their deadlines, we have to deal with courses with early deadlines first.
 * for each course, if it can fit into the current queue then add it, or if its time is less than the max in the queue then replace it.
 */
namespace leetcode
{
    public class Lc630_Course_ScheduleIII
    {
        public int ScheduleCourse(int[,] courses)
        {
            int n = courses.GetLength(0);
            var crs = new Course[n];
            for (int i = 0; i < n; i++)
                crs[i] = new Course(courses[i, 0], courses[i, 1], i);

            Array.Sort(crs, (a, b) => a.d - b.d);

            int lastTime = 0;
            // if the heap or priority queue can hold duplicates, then the element can be int(time) only.
            var heap = new SortedSet<Course>(Comparer<Course>.Create(
                (a, b) => a.t != b.t ? a.t - b.t : a.id - b.id));

            foreach (var c in crs)
            {
                if (c.t > c.d) continue;

                lastTime += c.t;
                heap.Add(c);
                if (lastTime > c.d)
                { 
                    lastTime -= heap.Max.t;
                    heap.Remove(heap.Max);
                }
            }

            return heap.Count;
        }

        class Course
        {
            public int t, d, id;
            public Course(int t, int d, int id)
            {
                this.t = t;
                this.d = d;
                this.id = id;
            }
        }

        public void Test()
        {
            var nums = new int[,] { { 100, 200 }, { 200, 1300 }, { 1000, 1250 }, { 2000, 3200 } };
            Console.WriteLine(ScheduleCourse(nums) == 3);
        }
    }
}

