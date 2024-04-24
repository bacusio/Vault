//Struct vs Class 

using System;

public class 2024.02.29
{
    struct WholookLike
    {
        public string Name;//
        public string Tall;//
        public string Weight;//
        public string Age;//
        public string Title;//
        public int ID;//
        public string school;//
    }

    static bool IsAmingKnowYaoMing(WholookLike Aming, WholookLike YaoMing)
    {
        if (Aming.school == "SZXiaoXue" && YaoMing.school == "SZXiaoXue")
        {
            return true;
        }
        return false;
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
            Aming.school = "SZXiaoXue";

            YaoMing.Name = "YaoMing";
            YaoMing.Tall = "2.26m";
            YaoMing.Weight = "141kg";
            YaoMing.Age = "39";
            YaoMing.Title = "Ex Basketball Player";
            YaoMing.ID = 002;
            YaoMing.school = "SZXiaoXue";
            Console.WriteLine("Aming's Name is {0}, Tall is {1}, Weight is {2}, Age is {3}, Title is {4}, ID is {5}", Aming.Name, Aming.Tall, Aming.Weight, Aming.Age, Aming.Title, Aming.ID);
            
            
            //chu结果
            Raylib.InitWindow(800, 450, "WhoKnowWho");
            string FirstWho,LastWho ;
            FirstWho = "Aming";
            LastWho = "YaoMing";
            if (IsAmingKnowYaoMing(Aming, YaoMing))
            {
                string result = FirstWho + " know " + LastWho;
            }
            else
            {
                string result = FirstWho + " don't know " + LastWho;
            }
            Raylib.EndDrawing();
   
    }
    }
    class School
    {
        static void Main(string[]args)
        {
           var SZXiaoXue = new School();
              //Aming:SZXiaoXue.Name = "AmingTongXue"


        }

    }
    
}