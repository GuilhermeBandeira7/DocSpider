using MudBlazor.Utilities;
using MudBlazor;

namespace DocSpider.Frontend
{
    public static class Configuration
    {
        public const string HttpClientName = "docspidercli";
        public static string StripePublicKey { get; set; } = "";
        public static string BackendUrl { get; set; } = "https://localhost:7162/";

        public static MudTheme Theme = new()
        {
            Typography = new Typography()
            {
                Default = new DefaultTypography()
                {
                    FontFamily = new[] { "Raleway", "sans-seric" }
                }
            },
            PaletteDark = new PaletteDark()
            {
                Primary = new MudColor("#0C71BF"),
                Secondary = Colors.LightBlue.Darken3,
                AppbarBackground = new MudColor("#0C71BF"),
                AppbarText = Colors.Shades.Black
            },
            PaletteLight = new PaletteLight()
            {
                Primary = new MudColor("#018FD3"),
                Secondary = Colors.LightBlue.Darken3,
                Background = Colors.Gray.Lighten4,
                AppbarBackground = new MudColor("#018FD3"),
                AppbarText = Colors.Shades.Black,
                TextPrimary = Colors.Shades.Black,
                PrimaryContrastText = Colors.Shades.Black,
                DrawerText = Colors.Shades.Black,
                DrawerBackground = Colors.LightBlue.Lighten4
            }
        };
    }
}
