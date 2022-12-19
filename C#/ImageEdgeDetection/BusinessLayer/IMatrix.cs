/*
 * The Following Code was developed by Dewald Esterhuizen
 * View Documentation at: http://softwarebydefault.com
 * Licensed under Ms-PL 
*/
namespace ImageEdgeDetection
{
    public interface IMatrix
    {
        double[,] Gaussian3x3 { get; }
        double[,] Gaussian5x5Type1 { get; }
        double[,] Gaussian5x5Type2 { get; }
        double[,] Kirsch3x3Horizontal { get; }
        double[,] Kirsch3x3Vertical { get; }
        double[,] Laplacian3x3 { get; }
        double[,] Laplacian5x5 { get; }
        double[,] LaplacianOfGaussian { get; }
        double[,] Prewitt3x3Horizontal { get; }
        double[,] Prewitt3x3Vertical { get; }
        double[,] Sobel3x3Horizontal { get; }
        double[,] Sobel3x3Vertical { get; }
    }
}