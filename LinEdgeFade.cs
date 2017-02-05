// Name:
// Submenu:
// Author:
// Title:
// Version:
// Desc:
// Keywords:
// URL:
// Help:
#region UICode
IntSliderControl Amount1=50; //[5,500]Border Radius
#endregion

void Render(Surface dst, Surface src, Rectangle rect)
{
    Rectangle selection = EnvironmentParameters.GetSelection(src.Bounds).GetBoundsInt();

    ColorBgra CurrentPixel;
    
    if((Amount1 > selection.Width/2) || (Amount1 > selection.Height/2)){
        return;
    }
    
    for (int y = rect.Top; y < rect.Bottom; y++)
    {
        if (IsCancelRequested) return;
        for (int x = rect.Left; x < rect.Right; x++)
        {
            CurrentPixel = src[x,y];

            if(x >= selection.Left && x < selection.Left + Amount1){
                CurrentPixel.A = (byte)(255*((float)(x-selection.Left)/(float)Amount1));
            }
            if(x >= selection.Right-Amount1 && x < selection.Right){
                CurrentPixel.A = (byte)(255*((float)(selection.Right-x)/(float)Amount1));
            }
            
            if(y >= selection.Top && y < selection.Top + Amount1){
                
                byte alpha = (byte)(255*((float)(y- selection.Top)/(float)Amount1));
                
                if((x >= selection.Left && x < selection.Left + Amount1) || (x >= selection.Right-Amount1 && x < selection.Right)){
                    CurrentPixel.A = (CurrentPixel.A <= alpha) ? CurrentPixel.A : alpha;
                } else{
                    CurrentPixel.A = alpha;
                }
            }
            if(y >= selection.Bottom-Amount1 && y < selection.Bottom){
                byte alpha = (byte)(255*((float)(selection.Bottom-y)/(float)Amount1));
                if((x >= selection.Left && x < selection.Left + Amount1) || (x >= selection.Right-Amount1 && x < selection.Right)){
                    CurrentPixel.A = (CurrentPixel.A <= alpha) ? CurrentPixel.A : alpha;
                } else{
                    CurrentPixel.A = alpha;
                }                
            }
            dst[x,y] = CurrentPixel;
        }
    }
}
