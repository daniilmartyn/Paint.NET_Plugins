// Name:Edge Fade
// Submenu:
// Author:Dan
// Title:Edge Fade
// Version:
// Desc:Fade selection
// Keywords:
// URL:
// Help:
#region UICode
IntSliderControl Amount1 = 50; // [5,500] Border Radius
#endregion

double scale_down(double x){
    return ((Math.PI/2)*x)/Amount1;
}

void Linear(Surface dst, Surface src, Rectangle selection, Rectangle rect){
    ColorBgra CurrentPixel;

    for (int y = rect.Top; y < rect.Bottom; y++){
        if (IsCancelRequested) return;
        for (int x = rect.Left; x < rect.Right; x++){
            CurrentPixel = src[x,y];

            if(x >= selection.Left && x < selection.Left + Amount1){
                double coeff = Math.Sin(scale_down(x));
                CurrentPixel.A = (byte)(255*coeff);
            }
            if(x >= selection.Right-Amount1 && x < selection.Right){
                double coeff = Math.Sin(scale_down(selection.Right - x));
                CurrentPixel.A = (byte)(255*coeff);
            }
            
            if(y >= selection.Top && y < selection.Top + Amount1){
                double coeff = Math.Sin(scale_down(y));
                byte alpha = (byte)(255*coeff);
                
                if((x >= selection.Left && x < selection.Left + Amount1) || (x >= selection.Right-Amount1 && x < selection.Right)){
                    CurrentPixel.A = (CurrentPixel.A <= alpha) ? CurrentPixel.A : alpha;
                } else{
                    CurrentPixel.A = alpha;
                }
            }
            if(y >= selection.Bottom-Amount1 && y < selection.Bottom){
                double coeff = Math.Sin(scale_down(selection.Bottom - y));
                byte alpha = (byte)(255*coeff);
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


void Render(Surface dst, Surface src, Rectangle rect)
{
    Rectangle selection = EnvironmentParameters.GetSelection(src.Bounds).GetBoundsInt();

    //ColorBgra CurrentPixel;
    
    if((Amount1 > selection.Width/2) || (Amount1 > selection.Height/2)){
        return;
    }
    

    Linear(dst, src, selection, rect);

}
