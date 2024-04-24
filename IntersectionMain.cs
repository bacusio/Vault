//Struct vs Class 

using System;
using blablabla;

namespace blablabla
{
    struct WholookLike
    {
        public string Name;//
        public string Tall;//
        public string Weight;//
        public string Age;//
        public string Title;//
        public int ID;//
    }

    public class ONE
    {   
        public static void Main(string[]args)
        {
            WholookLike Aming;
            WholookLike YaoMing;

            //VAR
            int a ;
            int b ;
            int TotallWhoNum;

            a = (int)Aming.ID;
            b = (int)YaoMing.ID;
            TotallWhoNum = a + b;
            Console.WriteLine("TotallWhoNum is {0}", TotallWhoNum);

            Aming.Name = "Aming";
            Aming.Tall = "1.56m";
            Aming.Weight = "37kg";
            Aming.Age = "15";
            Aming.Title = "Ninja";
            Aming.ID = 001;

            YaoMing.Name = "YaoMing";
            YaoMing.Tall = "2.26m";
            YaoMing.Weight = "141kg";
            YaoMing.Age = "39";
            YaoMing.Title = "Ex Basketball Player";
            YaoMing.ID = 002;
            Console.WriteLine("Aming's Name is {0}, Tall is {1}, Weight is {2}, Age is {3}, Title is {4}, ID is {5}", Aming.Name, Aming.Tall, Aming.Weight, Aming.Age, Aming.Title, Aming.ID);
        }
    }
}


//数组
/*
int[] n = new int[10];
int i, j;

int [] WholookLike = new string[]{BMing,CMing,DMing,EMing,FMing};

for (int i = 0; i < 10; i++)
{
    var n[i] = i + 100;
}
for (j =0; j <10 ,j++)
{
    Console.WriteLine("Element[{0}] = {1}", j, n[j]);
}
*/