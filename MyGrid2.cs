#region UICode
int xx = 10;	//[0,100]Column Width
int yy = 10;	//[0,100]Row Height
#endregion


void Render(Surface dst, Surface src, Rectangle rect)
{
    // Delete any of these lines you don't need
    Rectangle selection = this.EnvironmentParameters.GetSelection(src.Bounds).GetBoundsInt();
    long CenterX = (long)(((selection.Right - selection.Left) / 2)+selection.Left);
    long CenterY = (long)(((selection.Bottom - selection.Top) / 2)+selection.Top);
    ColorBgra PrimaryColor = (ColorBgra)EnvironmentParameters.PrimaryColor;
    ColorBgra SecondaryColor = (ColorBgra)EnvironmentParameters.SecondaryColor;
    //int BrushWidth = (int)EnvironmentParameters.BrushWidth;

    ColorBgra CurrentPixel;
    for (int y = rect.Top; y < rect.Bottom; y++)
    {
        for (int x = rect.Left; x < rect.Right; x++)
        {
                CurrentPixel = src[x,y];

                if((x%xx == 0) || (y%yy == 0)){
            // TODO: Add pixel processing code here
            // Access RGBA values this way, for example:
             CurrentPixel.R = (byte)PrimaryColor.R;
             CurrentPixel.G = (byte)PrimaryColor.G;
             CurrentPixel.B = (byte)PrimaryColor.B;
             CurrentPixel.A = (byte)PrimaryColor.A;
                dst[x,y] = CurrentPixel;
            }else{
                    CurrentPixel.R = (byte)SecondaryColor.R;
                     CurrentPixel.G = (byte)SecondaryColor.G;
                     CurrentPixel.B = (byte)SecondaryColor.B;
                     CurrentPixel.A = (byte)SecondaryColor.A;
                dst[x,y] = CurrentPixel;
            }
        }
    }
}
