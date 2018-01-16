namespace BashIm
{
    public class NewLoader : BaseLoader
    {
        public NewLoader(bool showBad = false)
        {
            DataType = showBad ? "hidebad" : "showbad";
        }
    }
}