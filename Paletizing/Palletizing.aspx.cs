using System;

namespace Palletizing
{
    public partial class Palletizing : System.Web.UI.Page
    {
        //ROTATION VARIABLES ---------------------------------
        //Total amount of cartons with rotation
        static double rotationMaxAmount = 0;
        //Amount of columns of horizontally positioned cartons
        static double rotationHorizontalColumns = 0;
        //Amount of rows of horizontally positioned cartons
        static double rotationHorizontalRows = 0;
        //Amount of columns of vertically positioned cartons
        static double rotationVerticalColumns = 0;
        //Amount of rows of vertically positioned cartons
        static double rotationVerticalRows = 0;
        //Length of horizontal part
        static double rotationHorizontalLength = 0;
        //Length of vertical part
        static double rotationVerticalLength = 0;
        //-----------------------------------------------------

        //NO ROTATION VARIABLES -------------------------------
        //Total amount of cartons without rotation
        static double maxAmount = 0;
        //Amount of columns of horizontally positioned cartons
        static double horizontalColumns = 0;
        //Amount of rows of horizontally positioned cartons
        static double horizontalRows = 0;
        //Amount of columns of vertically positioned cartons
        static double verticalColumns = 0;
        //Amount of rows of vertically positioned cartons
        static double verticalRows = 0;
        //------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void calcBtn_Click(object sender, EventArgs e)
        {
            //Creation of one big square with cartons positioned
            //both vertically and horizontally
            LayerCalculation general = new LayerCalculation();

            //Inputs
            general.setPalLength(Convert.ToInt32(PalletLength.Text.ToString()));
            general.setPalWidth(Convert.ToInt32(PalletWidth.Text.ToString()));
            general.setCartLength(Convert.ToInt32(CartonLength.Text.ToString()));
            general.setCartWidth(Convert.ToInt32(CartonWidth.Text.ToString()));

            //Calculation of the fitment within the pallet with rounding to lower int
            general.calculateFloor();

            //First check based on amount of horizontal columns
            //Check works in a way of putting one horizontally positioned carton column
            //at a time and filling the rest space with vertically positioned cartons
            for (double i = 1; i <= general.getHorizontalColumns(); i++)
            {
                double horizontalLength = general.getPalLength() - general.getCartLength() * i;
                double verticalLength = general.getPalLength() - horizontalLength;
                calculate(general, horizontalLength, verticalLength);
            }

            //Second check based on amount of vertical columns
            //Check works in a way of putting one vertically positioned carton column
            //at a time and filling the rest space with horizontally positioned cartons
            for (double i = 1; i <= general.getVerticalColumns(); i++)
            {
                double verticalLength = general.getPalLength() - general.getCartWidth() * i;
                double horizontalLength = general.getPalLength() - verticalLength;
                calculate(general, horizontalLength, verticalLength);
            }

            //Output results with rotation
            RotTotal.InnerText = rotationMaxAmount.ToString();
            RotHC.InnerText = rotationHorizontalColumns.ToString();
            RotHR.InnerText = rotationHorizontalRows.ToString();
            RotVC.InnerText = rotationVerticalColumns.ToString();
            RotVR.InnerText = rotationVerticalRows.ToString();
            RotHPL.InnerText = rotationHorizontalLength.ToString();
            RotVPL.InnerText = rotationVerticalLength.ToString();
            //Output results without rotation - check the amount of total cartons for both fitments
            //First generated square is used for calculation
            if (general.getAllCartonsHorizontally() >= general.getAllCartonsVertically())
            {
                maxAmount = general.getAllCartonsHorizontally();
                horizontalColumns = general.getHorizontalColumns();
                horizontalRows = general.getHorizontalRows();
                NoRotTotalH.InnerText = maxAmount.ToString();
                NoRotHC.InnerText = horizontalColumns.ToString();
                NoRotHR.InnerText = horizontalRows.ToString();
            }
            else
            {
                maxAmount = general.getAllCartonsVertically();
                verticalColumns = general.getVerticalColumns();
                verticalRows = general.getVerticalRows();
                NoRotTotalV.InnerText = maxAmount.ToString();
                NoRotVC.InnerText = verticalColumns.ToString();
                NoRotVR.InnerText = verticalRows.ToString();
            }
        }

        //Method where the calculations of two smaller squares are done by dividing one big square to two smaller ones
        protected static void calculate(LayerCalculation general, double horizontalLength, double verticalLength)
        {

            //Create the two squares, one horizontal and one vertical with remaining length of the pallet
            LayerCalculation horizontalSquare = new LayerCalculation(horizontalLength, general.getPalWidth(),
                    general.getCartLength(), general.getCartWidth());
            LayerCalculation verticalSquare = new LayerCalculation(verticalLength, general.getPalWidth(),
                    general.getCartLength(), general.getCartWidth());

            //Check when both horizontal and vertical carton calculation is floored
            horizontalSquare.calculateFloor();
            verticalSquare.calculateFloor();
            //Sum of horizontally positioned cartons and vertically positioned cartons
            double calculatedCartons = horizontalSquare.getAllCartonsHorizontally() + verticalSquare.getAllCartonsVertically();
            check(calculatedCartons, general, horizontalSquare, verticalSquare);
        }

        //Method that checks the constraints if the calculation came up with cartons exceeding the pallet length/width
        protected static void check(double calculatedCartons, LayerCalculation general,
                                    LayerCalculation horizontalSquare, LayerCalculation verticalSquare)
        {

            //Do not do this check if calculated amount is less than current biggest amount
            if (rotationMaxAmount < calculatedCartons)
            {
                //Occupied amount of horizontal cartons
                double lengthLimitHS = horizontalSquare.getHorizontalColumns() * horizontalSquare.getCartLength();
                //Occupied amount of vertical cartons
                double lengthLimitVS = verticalSquare.getVerticalColumns() * verticalSquare.getCartWidth();
                //Total occupied space
                double lengthLimit = general.getPalLength() - (lengthLimitHS + lengthLimitVS);
                //Occupied width of horizontal cartons
                double widthLimitHS = general.getPalWidth() - horizontalSquare.getHorizontalRows() * horizontalSquare.getCartWidth();
                //Occupied width of vertical cartons
                double widthLimitVS = general.getPalWidth() - verticalSquare.getVerticalRows() * verticalSquare.getCartLength();
                //Extra check in case rounding goes to 0, if not done - can be bugged!
                if (horizontalSquare.getHorizontalColumns() != 0 && verticalSquare.getVerticalColumns() != 0
                        && horizontalSquare.getHorizontalRows() != 0 && verticalSquare.getVerticalRows() != 0)
                {
                    //Check of constrains
                    if (lengthLimit >= 0 && widthLimitHS >= 0 && widthLimitVS >= 0)
                    {
                        //Change in current variable values
                        rotationMaxAmount = calculatedCartons;
                        rotationHorizontalColumns = horizontalSquare.getHorizontalColumns();
                        rotationHorizontalRows = horizontalSquare.getHorizontalRows();
                        rotationVerticalColumns = verticalSquare.getVerticalColumns();
                        rotationVerticalRows = verticalSquare.getVerticalRows();
                        rotationHorizontalLength = horizontalSquare.getPalLength();
                        rotationVerticalLength = verticalSquare.getPalLength();
                    }
                }
            }
        }
    }
    public partial class LayerCalculation
    {

        //Variables -----------------------------
        double palLength;
        double palWidth;
        double cartLength;
        double cartWidth;
        double palLengthToCartLength;
        double palLengthToCartWidth;
        double palWidthToCartWidth;
        double palWidthToCartLength;
        double allCartonsHorizontally;
        double allCartonsVertically;
        //---------------------------------------

        //Constructors --------------------------
        public LayerCalculation(double palLength, double palWidth, double cartLength, double cartWidth)
        {
            this.palLength = palLength;
            this.palWidth = palWidth;
            this.cartLength = cartLength;
            this.cartWidth = cartWidth;
        }

        public LayerCalculation()
        {
        }
        //---------------------------------------

        //Methods for calculation ---------------
        //Calculate the amount of cartons rounding to the closest integer
        public void calculateRound()
        {
            //How many columns fit horizontally
            palLengthToCartLength = Math.Round(palLength / cartLength);
            //How many columns fit vertically
            palLengthToCartWidth = Math.Round(palLength / cartWidth);
            //How many rows fit horizontally
            palWidthToCartWidth = Math.Round(palWidth / cartWidth);
            //How many rows fit vertically
            palWidthToCartLength = Math.Round(palWidth / cartLength);
            //Calculate all the horizontal cartons
            allCartonsHorizontally = palLengthToCartLength * palWidthToCartWidth;
            //Calculate all the vertical cartons
            allCartonsVertically = palLengthToCartWidth * palWidthToCartLength;
        }

        //Calculate the amount of cartons rounding to the lower integer
        public void calculateFloor()
        {
            //How many columns fit horizontally
            palLengthToCartLength = Math.Floor(palLength / cartLength);
            //How many columns fit vertically
            palLengthToCartWidth = Math.Floor(palLength / cartWidth);
            //How many rows fit horizontally
            palWidthToCartWidth = Math.Floor(palWidth / cartWidth);
            //How many rows fit vertically
            palWidthToCartLength = Math.Floor(palWidth / cartLength);
            //Calculate all the horizontal cartons
            allCartonsHorizontally = palLengthToCartLength * palWidthToCartWidth;
            //Calculate all the vertical cartons
            allCartonsVertically = palLengthToCartWidth * palWidthToCartLength;
        }
        //---------------------------------------

        //Getters and setters -------------------
        public double getAllCartonsHorizontally()
        {
            return allCartonsHorizontally;
        }
        public double getAllCartonsVertically()
        {
            return allCartonsVertically;
        }
        public double getHorizontalColumns()
        {
            return palLengthToCartLength;
        }
        public double getVerticalColumns()
        {
            return palLengthToCartWidth;
        }
        public double getHorizontalRows()
        {
            return palWidthToCartWidth;
        }
        public double getVerticalRows()
        {
            return palWidthToCartLength;
        }
        public double getPalLength()
        {
            return palLength;
        }
        public double getPalWidth()
        {
            return palWidth;
        }
        public double getCartLength()
        {
            return cartLength;
        }
        public double getCartWidth()
        {
            return cartWidth;
        }
        public void setPalLength(double palLength)
        {
            this.palLength = palLength;
        }
        public void setPalWidth(double palWidth)
        {
            this.palWidth = palWidth;
        }
        public void setCartLength(double cartLength)
        {
            this.cartLength = cartLength;
        }
        public void setCartWidth(double cartWidth)
        {
            this.cartWidth = cartWidth;
        }
        //---------------------------------------
    }
}
