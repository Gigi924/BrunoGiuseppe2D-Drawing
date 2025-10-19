using System;
using System.Numerics;

namespace MohawkGame2D
{
    public class Game
    {
        // Constants 
        const int width = 800;
        const int height = 600;
        const int CarCount = 7;

        //  Car Data 
        Vector2[] carPositions = new Vector2[CarCount];
        float[] carSpeeds = new float[CarCount];
        Color[] carColors = new Color[CarCount];

        //  Setup 
        public void Setup()
        {
            Window.SetTitle("Car Animation");
            Window.SetSize(width, height);
            Draw.LineColor = Color.Clear;

            // Fixed colors for cars
            Color[] fixedColors = new Color[]
            {
                Color.Red,
                Color.Blue,
                Color.Green,
                Color.Yellow,
                Color.Magenta
            };

            for (int i = 0; i < CarCount; i++)
            {
                carPositions[i] = new Vector2(100 * i, 100 + i * 80);
                carSpeeds[i] = 2f + i;
                carColors[i] = fixedColors[i % fixedColors.Length];
            }
        }

        //  Update 
        public void Update()
        {
            Window.ClearBackground(Color.LightGray);

            Vector2 mousePos = Input.GetMousePosition();
            bool isLeftClick = Input.IsMouseButtonPressed(0);

            for (int i = 0; i < CarCount; i++)
            {
                // Move car
                carPositions[i].X += carSpeeds[i];

                // Reset if off screen (color stays the same now)
                if (carPositions[i].X > width + 100)
                {
                    carPositions[i].X = 100;
                }

                // Balloonstyle click detection
                float carCenterX = carPositions[i].X + 40;
                float carCenterY = carPositions[i].Y + 30; 
                float dx = mousePos.X  carCenterX;
                float dy = mousePos.Y  carCenterY;
                float distance = MathF.Sqrt(dx * dx + dy * dy);

                // Leftclick effect
                if (isLeftClick && distance <= 50f)
                {
                    carSpeeds[i] += 1f;
                }

                // Draw the car
                DrawCar(carPositions[i], carColors[i]);
            }
        }

        //  Helper: Draw Car 
        void DrawCar(Vector2 pos, Color color)
        {
            // Body
            Draw.FillColor = color;
            Draw.Rectangle((int)pos.X, (int)pos.Y + 20, 80, 20);

            // Roof
            Vector2[] roof = new Vector2[]
            {
                new Vector2(pos.X + 10, pos.Y + 20),
                new Vector2(pos.X + 30, pos.Y),
                new Vector2(pos.X + 50, pos.Y),
                new Vector2(pos.X + 70, pos.Y + 20)
            };
            Draw.Triangle(roof[0].X, roof[0].Y, roof[1].X, roof[1].Y, roof[2].X, roof[2].Y);
            Draw.Triangle(roof[0].X, roof[0].Y, roof[2].X, roof[2].Y, roof[3].X, roof[3].Y);

            // Wheels
            Draw.FillColor = Color.Black;
            Draw.Circle((int)pos.X + 20, (int)pos.Y + 40, 10);
            Draw.Circle((int)pos.X + 60, (int)pos.Y + 40, 10);
        }
    }
}