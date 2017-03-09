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
DoubleSliderControl Amount2 = 1; // [0.01,10] Fade Falloff
#endregion

void Linear(Surface dst, Surface src, Rectangle selection, Rectangle rect){
    ColorBgra CurrentPixel;

    for (int y = rect.Top; y < rect.Bottom; y++){
        if (IsCancelRequested) return;
        for (int x = rect.Left; x < rect.Right; x++){
            CurrentPixel = src[x,y];

            if(x >= selection.Left && x < selection.Left + Amount1){
                double coeff = Math.Pow((double)(x-selection.Left+1)/(double)Amount1, 1.0/Amount2);
                CurrentPixel.A = (byte)(255*coeff);
            }
            if(x >= selection.Right-Amount1 && x < selection.Right){
                double coeff = Math.Pow((double)(selection.Right-x)/(double)Amount1, 1.0/Amount2);
                CurrentPixel.A = (byte)(255*coeff);
            }
            
            if(y >= selection.Top && y < selection.Top + Amount1){
                double coeff = Math.Pow((double)(y- selection.Top+1)/(double)Amount1, 1.0/Amount2);
                byte alpha = (byte)(255*coeff);
                
                if((x >= selection.Left && x < selection.Left + Amount1) || (x >= selection.Right-Amount1 && x < selection.Right)){
                    CurrentPixel.A = (CurrentPixel.A <= alpha) ? CurrentPixel.A : alpha;
                } else{
                    CurrentPixel.A = alpha;
                }
            }
            if(y >= selection.Bottom-Amount1 && y < selection.Bottom){
                double coeff = Math.Pow((double)(selection.Bottom-y)/(double)Amount1, 1.0/Amount2);
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
