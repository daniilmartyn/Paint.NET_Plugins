#region UICode
int Amount1 = 2;    // [1,10] Base star size
int Amount2 = 7; // [1,10] Star size variation
int Amount3 = 20;      // [1, 500] Star density
byte Amount4 = 0;     // [255] Randomize starfield
#endregion

void Render(Surface dst, Surface src, Rectangle rect)
{
    // Delete any of these lines you don't need
    Rectangle selection = this.EnvironmentParameters.GetSelection(src.Bounds).GetBoundsInt();
    ColorBgra PrimaryColor = (ColorBgra)EnvironmentParameters.PrimaryColor;
    ColorBgra SecondaryColor = (ColorBgra)EnvironmentParameters.SecondaryColor;

    ColorBgra CurrentPixel;
    
    SolidBrush brush = new SolidBrush(SecondaryColor);
    Graphics g = new RenderArgs(dst).Graphics;
    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
    g.Clip = new Region(rect);
    
    if(Amount2 < Amount1){
            Amount2 = Amount1;
    }
    
    for (int y = rect.Top; y < rect.Bottom; y++)
    {
        for (int x = rect.Left; x < rect.Right; x++)
        {
            CurrentPixel = src[x,y];
            // TODO: Add pixel processing code here
            // Access RGBA values this way, for example:
            
             CurrentPixel.R = (byte)PrimaryColor.R;
             CurrentPixel.G = (byte)PrimaryColor.G;
             CurrentPixel.B = (byte)PrimaryColor.B;
             CurrentPixel.A = (byte)PrimaryColor.A;
            dst[x,y] = CurrentPixel;
        }
    }
    
    Rectangle r;
    Random rand = new Random(Amount4);
    
    for(int i = 0; i < Amount3; i++){
            int x = rand.Next(selection.Left,selection.Right);
            int y = rand.Next(selection.Top,selection.Bottom);
            int sz = rand.Next(Amount1, Amount2);
            r = new Rectangle(x,y,sz,sz);
            g.FillEllipse(brush,r);

    }
}
