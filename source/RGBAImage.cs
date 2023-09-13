

using Godot;

public class RGBAImage
{
    private Vector2I Size;
    private RGBA[,] Data;

    public RGBAImage(Vector2I size)
    {
        this.Size = size;
        this.Data = new RGBA[size.X, size.Y];
    }

    public RGBA GetColor(Vector2I point)
    {
        return Data[point.X, point.Y];
    }

    public void SetColor(Vector2I point, RGBA color)
    {
        Data[point.X, point.Y] = color;
    }

    public void Fill(Rect2I rect, RGBA color)
    {
        var start = rect.Position;
        var end = rect.End;
        for (int y = start.Y; y < end.Y; y++)
        {
            for (int x = start.X; x < end.X; x++)
            {
                Data[x, y] = color;
            }
        }
    }

    public void Blit(RGBAImage srcImage, Rect2I srcRect, Vector2I pos)
    {
        
    }
}