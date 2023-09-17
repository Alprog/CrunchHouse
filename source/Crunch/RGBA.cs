
namespace Crunch
{
    public struct RGBA
    {
        byte R;
        byte G;
        byte B;
        byte A;

        public uint UInt => (uint)R << 24 | (uint)G << 16 | (uint)B << 8 | (uint)A;

        public RGBA(uint rgba)
        {
            this.R = (byte)(rgba >> 24 & 0xFF);
            this.G = (byte)(rgba >> 16 & 0xFF);
            this.B = (byte)(rgba >> 08 & 0xFF);
            this.A = (byte)(rgba >> 00 & 0xFF);
        }

        public RGBA(byte r, byte g, byte b, byte a)
        {
            this.R = r;
            this.G = g;
            this.B = b;
            this.A = a;
        }
    }
}