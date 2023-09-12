

using Godot;

public class TileMap
{
    private int[,] Data;
    private Image Image;
    private bool Changed;
    public ImageTexture Texture { get; private set; }

    public TileMap(Vector2I size)
    {
        Data = new int[size.X, size.Y];
        Image = Image.Create(size.X, size.Y, false, Image.Format.Rgba8);
        Texture = ImageTexture.CreateFromImage(Image);
    }

    public VectorXZI Size => new VectorXZI(Data.GetLength(0), Data.GetLength(1));

    public int GetTileIndex(VectorXZI crd) => GetTileIndex(crd.X, crd.Z);

    public int GetTileIndex(int x, int z)
    {
        return Data[x, z];
    }

    public void SetTileIndex(VectorXZI crd, int index) => SetTileIndex(crd.X, crd.Z, index);

    public void SetTileIndex(int x, int z, int index)
    {
        Data[x, z] = index;
        Image.SetPixel(x, z, new Color((uint)index));
        Changed = true;
    }

    public void Sync()
    {
        if (Changed)
        {
            Texture.Update(Image);
            Changed = false;
        }
    }
}