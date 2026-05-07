using System.Windows.Forms;
using System;
using System.Windows;

public class FunctionGraph : Control
{
    private float xMin = -20f;
    private float yMin = -20f;
    private float xMax = 20f;
    private float yMax = 20f;

    private float func(float x, float y) => (float)Algorithm.Function(x);
    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        Graphics g = e.Graphics;
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

        g.Clear(Color.White);
        
        DrawGrid(g);
        DrawAxes(g);
        DrawGraph(g);
    }

    private void DrawAxes(Graphics g)
    {
        Pen axisPen = new Pen(Color.Black, 2);

        int centerX = Width / 2;
        int centerY = Height / 2;

        g.DrawLine(axisPen, 0, centerY, Width, centerY);
        g.DrawLine(axisPen, centerX, 0, centerX, Height);

        DrawArrow(g, new Point(Width - 10, centerY), new Point(Width, centerY));
        DrawArrow(g, new Point(centerX, 10), new Point(centerX, 0));

        using Font font = new Font("Arial", 8);
        g.DrawString("X", font, Brushes.Black, Width - 20, centerY - 20);
        g.DrawString("Y", font, Brushes.Black, centerX + 5, 5);

        DrawTickLabels(g, 0, centerY);
    }

    private void DrawTickLabels(Graphics g, int centerX, int centerY)
    {
        using Font font = new Font("Arial", 8);

        for (int x = (int)xMin; x < xMax; x++)
        {
            if (x == 0) continue;
            int pixelX = centerX + (int)((x - xMin) * Width / (xMax - xMin));
            g.DrawLine(Pens.Gray, pixelX, centerY - 3, pixelX, centerY + 3);
            g.DrawString(x.ToString(), font, Brushes.Black, pixelX - 5, centerY + 5);
        }

        g.DrawString("0", font, Brushes.Black, centerX + Width / 2 + 3, centerY + 3);
    }

    private void DrawGrid(Graphics g)
    {
        Pen gridPen = new Pen(Color.LightGray, 1);
        gridPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

        int centerX = 0;
        int centerY = Height;

        for (int x = (int)xMin; x < xMax; x++)
        {
            if (x == 0) continue;
            int pixelX = centerX + (int)((x - xMin) * Width / (xMax - xMin));
            g.DrawLine(gridPen, pixelX, 0, pixelX, Height);
        }

        for (int y = (int)yMin; y < yMax; y++)
        {
            if (y == 0) continue;
            int pixelY = centerY - (int)((y - yMin) * Height / (yMax - yMin));
            g.DrawLine(gridPen, 0, pixelY, Width, pixelY);
        }
    }

    private void DrawGraph(Graphics g)
    {
        using Pen graphPen = new Pen(Color.Blue, 2);

        PointF[] points = new PointF[Width];
        int pointIndex = 0;

        for (int pixelX = 0; pixelX < Width; pixelX++)
        {
            float x = xMin + (pixelX * (xMax - xMin) / Width);
            // TODO поменять
            float y = (float)Algorithm.Function(x);

            int pixelY = (int)(Height - ((y - yMin) * Height / (yMax - yMin)));

            if (pixelY >= 0 && pixelY <= Height && y >= yMin && y <= yMax)
                points[pointIndex++] = new PointF(pixelX, pixelY);
            else if (pointIndex > 1)
            {
                g.DrawLines(graphPen, points[0..pointIndex]);
                pointIndex = 0;
            }
        }

        if (pointIndex > 1)
            g.DrawLines(graphPen, points[0..pointIndex]);
    }

    private void DrawArrow(Graphics g, Point start, Point end)
    {
        Pen pen = new Pen(Color.Black, 2);
        g.DrawLine(pen, start, end);

        const int arrowSize = 5;
        double angle = Math.Atan2(end.Y - start.Y, end.X - start.X);

        Point arrow1 = new Point(
            (int)(end.X - arrowSize * Math.Cos(angle - Math.PI / 6)),
            (int)(end.Y - arrowSize * Math.Sin(angle - Math.PI / 6)));

        Point arrow2 = new Point(
            (int)(end.X - arrowSize * Math.Cos(angle + Math.PI / 6)),
            (int)(end.Y - arrowSize * Math.Sin(angle + Math.PI / 6)));

        g.DrawLine(pen, end, arrow1);
        g.DrawLine(pen, end, arrow2);
    }
}

