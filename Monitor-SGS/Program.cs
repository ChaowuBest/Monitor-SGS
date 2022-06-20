using Microsoft.Toolkit.Uwp.Notifications;
using System.Text;
using System.Threading;

namespace Monitor_SGS
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Request req = new Request();
            string info = "Parm1=%7B%22CompanyNo%22%3A1%2C%22SyndicateNo%22%3A1%2C%22ObjectMainGroupNo%22%3A17%2C%22Advertisements%22%3A%5B%7B%22No%22%3A-1%7D%5D%2C%22RentLimit%22%3A%7B%22Min%22%3A0%2C%22Max%22%3A25000%7D%2C%22AreaLimit%22%3A%7B%22Min%22%3A0%2C%22Max%22%3A150%7D%2C%22ApplySearchFilter%22%3Atrue%2C%22Page%22%3A1%2C%22Take%22%3A10%2C%22SortOrder%22%3A%22SeekAreaDescription%20ASC%2C%20street%20ASC%22%2C%22ReturnParameters%22%3A%5B%22ObjectNo%22%2C%22FirstEstateImageUrl%22%2C%22Street%22%2C%22SeekAreaDescription%22%2C%22PlaceName%22%2C%22ObjectSubDescription%22%2C%22ObjectArea%22%2C%22RentPerMonth%22%2C%22MarketPlaceDescription%22%2C%22CountInterest%22%2C%22FirstInfoTextShort%22%2C%22FirstInfoText%22%2C%22EndPeriodMP%22%2C%22FreeFrom%22%2C%22SeekAreaUrl%22%2C%22Latitude%22%2C%22Longitude%22%2C%22BoardNo%22%5D%7D&CallbackMethod=PostObjectSearch&CallbackParmCount=1&__WWEVENTCALLBACK=&";
            while (true)
            {
                Thread.Sleep(2000);
                if (req.httpRequest("https://marknad.sgs.se/API/Service/SearchServiceHandler.ashx", info, Encoding.UTF8, Encoding.UTF8))
                {
                    break;
                }
            }
            // Requires Microsoft.Toolkit.Uwp.Notifications NuGet package version 7.0 or greater
            new ToastContentBuilder()
                .AddArgument("action", "viewConversation")
                .AddArgument("conversationId", 9813)
                .AddText("SGS Release")
                .Show(); // Not seeing the Show() method? Make sure you have version 7.0, and if you're using .NET 6 (or later), then your TFM must be net6.0-windows10.0.17763.0 or greater
        }
    }
}
