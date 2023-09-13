

using System;
using Godot;

public class RGBAImage
{
    private Vector2I Size;
    private RGBA[,] Data;

    public int Width => Size.X;
    public int Height => Size.Y;

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

    private int GetOffset(Vector2I pos) => pos.Y * Width + pos.X; // row-major

    public void Blit(RGBAImage srcImage, Rect2I srcRect, Vector2I dstPos)
    {
        Blit(srcImage, srcRect.Position, this, dstPos, srcRect.Size);
    }

    static public void Blit(RGBAImage srcImage, Vector2I srcPos, RGBAImage dstImage, Vector2I dstPos, Vector2I size)
    {
        var lineWidth = size.X;
        var srcIndex = srcImage.GetOffset(srcPos);
        var dstIndex = dstImage.GetOffset(dstPos);
        for (int line = 0; line < size.Y; line++)
        {
            Array.Copy(srcImage.Data, srcIndex, dstImage.Data, dstIndex, lineWidth);
            srcIndex += srcImage.Width;
            dstIndex += dstImage.Width;
        }        
    }
}