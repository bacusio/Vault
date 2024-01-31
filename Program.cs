using System.Numerics;
using Raylib_cs;


public class Program {

    public int Add(int a, int b) {
        if (a<b) {
            return b;
        }
        else {
            return a;
        }
    }
    
    public static void Main() {

        int x =640;
        int y = 480;
        Raylib.InitWindow(x, y, "Game");
    
        float posX = 320;
        float posY = 240;

        Vector2 vec = new Vector2(640, 480);
        int i= (int)vec.X+50;

        //testland

        while (!Raylib.WindowShouldClose()) 
        {

            Raylib.BeginDrawing();

            
    
            float dt = Raylib.GetFrameTime();
            if (Raylib.IsKeyDown(KeyboardKey.D)) {
                posX += 100*dt;
            }
             else if (Raylib.IsKeyDown(KeyboardKey.A))
              {
                posX -= 100*dt;
            }

            if (Raylib.IsKeyDown(KeyboardKey.S)) {
                posY += 100*dt;
            }
            else if (Raylib.IsKeyDown(KeyboardKey.W)) 
            {
                posY -= 100*dt;
            }
            Raylib.ClearBackground(Color.RayWhite); 

            for (i=0; i < 5; i++) {
               
                //Raylib.DrawRectangle(100*i, 100, 50, 50, Color.Blue);
                Raylib.DrawCircle(100*i, 100, 50, Color.Red);
            }

            //Raylib.DrawCircle((int)vec.X-320, (int)vec.Y-240, 50, Color.Blue);
            Raylib.DrawRectangle((int)posX,  (int)posY, 100, 150, Color.Blue);
            Raylib.EndDrawing();

        }

        Raylib.CloseWindow();

    }

}