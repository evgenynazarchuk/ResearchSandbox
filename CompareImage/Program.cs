using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.IO;

namespace CompareImage
{
    class Program
    {
        static void Main(string[] args)
        {
            float c = 0.2f;
            //
            List<bool> hash1 = GetHash(new Bitmap("Resources\\Images\\1.jpeg"), c);
            List<bool> hash2 = GetHash(new Bitmap("Resources\\Images\\2.jpeg"), c);
            List<bool> hash3 = GetHash(new Bitmap("Resources\\Images\\3.jpeg"), c);
            List<bool> hash4 = GetHash(new Bitmap("Resources\\Images\\4.jpeg"), c);
            List<bool> hash5 = GetHash(new Bitmap("Resources\\Images\\5.jpeg"), c);
            List<bool> hash6 = GetHash(new Bitmap("Resources\\Images\\6.jpeg"), c);

            SaveMinimize(new Bitmap("Resources\\Images\\1.jpeg"), "Resources\\Images\\1_minimize.jpeg", c);
            SaveMinimize(new Bitmap("Resources\\Images\\2.jpeg"), "Resources\\Images\\2_minimize.jpeg", c);
            SaveMinimize(new Bitmap("Resources\\Images\\3.jpeg"), "Resources\\Images\\3_minimize.jpeg", c);
            SaveMinimize(new Bitmap("Resources\\Images\\4.jpeg"), "Resources\\Images\\4_minimize.jpeg", c);
            SaveMinimize(new Bitmap("Resources\\Images\\5.jpeg"), "Resources\\Images\\5_minimize.jpeg", c);
            SaveMinimize(new Bitmap("Resources\\Images\\6.jpeg"), "Resources\\Images\\6_minimize.jpeg", c);

            int equalElement1and1 = hash1.Zip(hash1, (i, j) => i == j).Count(eq => eq);
            int equalElement1and2 = hash1.Zip(hash2, (i, j) => i == j).Count(eq => eq);
            int equalElement1and3 = hash1.Zip(hash3, (i, j) => i == j).Count(eq => eq);
            int equalElement1and4 = hash1.Zip(hash4, (i, j) => i == j).Count(eq => eq);
            int equalElement1and5 = hash1.Zip(hash5, (i, j) => i == j).Count(eq => eq);
            int equalElement1and6 = hash1.Zip(hash6, (i, j) => i == j).Count(eq => eq);

            Console.WriteLine($"equalElement1and1: {equalElement1and1}");
            Console.WriteLine($"equalElement1and2: {equalElement1and2}, errors: {(equalElement1and1 - equalElement1and2)}");
            Console.WriteLine($"equalElement1and3: {equalElement1and3}, errors: {(equalElement1and1 - equalElement1and3)}");
            Console.WriteLine($"equalElement1and4: {equalElement1and4}, errors: {(equalElement1and1 - equalElement1and4)}");
            Console.WriteLine($"equalElement1and5: {equalElement1and5}, errors: {(equalElement1and1 - equalElement1and5)}");
            Console.WriteLine($"equalElement1and6: {equalElement1and6}, errors: {(equalElement1and1 - equalElement1and6)}");
        }

        /// <summary>
        /// Get Image Hash
        /// </summary>
        /// <param name="bmpSource"></param>
        /// <returns></returns>
        public static List<bool> GetHash(Bitmap bmpSource, float MinimizationCoefficient)
        {
            List<bool> hash = new List<bool>();
            Bitmap minimize = new Bitmap(bmpSource, 
                new Size((int)(bmpSource.Width * MinimizationCoefficient), (int)(bmpSource.Height * MinimizationCoefficient)));

            for (int j = 0; j < minimize.Height; j++)
            {
                for (int i = 0; i < minimize.Width; i++)
                {
                    hash.Add(minimize.GetPixel(i, j).GetBrightness() < 0.5f);
                }
            }

            return hash;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bmpSource"></param>
        /// <param name="filepath"></param>
        public static void SaveMinimize(Bitmap bmpSource, string filepath, float MinimizationCoefficient)
        {
            Bitmap minimize = new Bitmap(bmpSource, new Size((int)(bmpSource.Width * MinimizationCoefficient), 
                (int)(bmpSource.Height * MinimizationCoefficient)));
            minimize.Save(filepath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bmpSource"></param>
        /// <returns></returns>
        //public static List<bool> GetHashBySegment(Bitmap bmpSource)
        //{
        //    List<bool> hash = new List<bool>();
        //    const int n = 3;
        //
        //    for (int j = 0; j < bmpSource.Height - n; j++)
        //    {
        //        
        //        for (int i = 0; i < bmpSource.Width - n; i++)
        //        {
        //            float average = 0;
        //
        //            for (int k = j; k < j + n; k++)
        //            {
        //                for (int p = i; p < i + n; p++)
        //                {
        //                    bool flag = bmpSource.GetPixel(p, k).GetBrightness() < 0.5f;
        //                    average += Convert.ToInt32(flag);
        //                }
        //            }
        //
        //            average /= 9;
        //            hash.Add(average < 0.5f);
        //        }
        //        
        //    }
        //
        //    return hash;
        //}
    }
}
