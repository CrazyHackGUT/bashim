namespace BashIm
{
    public class BestOnYearLoader : BaseLoader
    {
        public BestOnYearLoader(int Year, int Month = -1)
        {
            if (Month == -1)
                DataType = $"bestyear/{Year}";
            else
                DataType = $"bestyear/{Year}/{Month}";
        }
    }
}