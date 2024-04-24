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
    
        // F2 
        // 值类型 内存区域 栈
        Vector2 rolePos = new Vector2(320, 240);

        // 引用类型 内存区域 堆
        Vector2[] bullets = new Vector2[100000]; // 1. Allocate Array Memory
        bullets[0] = new Vector2(100, 100); // 2. Set Value To One Array Element
        Vector2 bpos = bullets[0]; // 3. Get Value From One Array Element

        int[] indices = new int[10]; // 1. Allocate Array Memory
        indices[0] = 100; // 2. Set Value To One Array Element
        int index = indices[0]; // 3. Get Value From One Array Element
        // int v = bullets[0]; // X Y, 

        int bulletCount = 0;

        Vector2 vec = new Vector2(640, 480);

        int[] circles = new int[5];
        circles[0] = 50;
        circles[1] = 150;
        circles[2] = 250;
        circles[3] = 350;
        circles[4] = 450;

        //testland

        while (!Raylib.WindowShouldClose()) 
        {

            Raylib.BeginDrawing();

            float dt = Raylib.GetFrameTime();

            // ProcessInput
            Vector2 move = GetMoveInput();
            bool isShoot = Raylib.IsKeyPressed(KeyboardKey.Space);

            // DoLogic
            // - Role Move
            rolePos = rolePos + move * 100 * dt;

            // - Role Shoot
            if (isShoot) {
                System.Console.WriteLine("Shoot");
                // Bullets 第 bullet Count 个 = rolePos
                bullets[bulletCount] = rolePos; // 从角色位置发射子弹
                bulletCount += 1;
            }

            // - Bullet Move
            for (int i = 0; i < bulletCount; i += 1) {
                Vector2 bullet = bullets[i];
                bullet.Y -= 100 * dt;
                bullets[i] = bullet;
            }

            // Draw
            Raylib.ClearBackground(Color.RayWhite); 
            Raylib.DrawRectangle((int)rolePos.X,  (int)rolePos.Y, 30, 80, Color.Blue);
            DrawBullet(bullets, bulletCount);

            // ctrl + /
            // for (i=0; i < 5; i++) {
               
            //     //Raylib.DrawRectangle(100*i, 100, 50, 50, Color.Blue);
            //     Raylib.DrawCircle(100*i+50, 100, 50, Color.Red);
            // }

            // for (int j = 0; j < circles.Length; j += 1) {
            //     Raylib.DrawCircle(circles[j], 200, 50, Color.Yellow);
            // }

            //Raylib.DrawCircle((int)vec.X-320, (int)vec.Y-240, 50, Color.Blue);

            Raylib.EndDrawing();

        }

        Raylib.CloseWindow();

    }

    // 封装
    static Vector2 GetMoveInput() {
        Vector2 moveAxis = new Vector2(0, 0);
        if (Raylib.IsKeyDown(KeyboardKey.D)) {
            moveAxis.X += 1;
        } else if (Raylib.IsKeyDown(KeyboardKey.A)) {
            moveAxis.X -= 1;
        }

        if (Raylib.IsKeyDown(KeyboardKey.S)) {
            moveAxis.Y += 1;
        } else if (Raylib.IsKeyDown(KeyboardKey.W)) {
            moveAxis.Y -= 1;
        }
        return moveAxis;
    }

    static void DrawBullet(Vector2[] bullets, int bulletCount) {
        for (int i = 0; i < bulletCount; i += 1) {
            Vector2 bullet = bullets[i];
            Raylib.DrawCircle((int)bullet.X, (int)bullet.Y, 10, Color.Red);
        }
    }

}