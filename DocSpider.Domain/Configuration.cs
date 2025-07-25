﻿namespace DocSpider.Domain;

public static class Configuration
{
    public const int DefaultStatusCode = 200;
    public const int DefaultPageNumber = 1;
    public const int DefaultPageSize = 25;

    public static string ConnectionString = string.Empty;
    public static string BackendUrl = "https://localhost:7162";
    public static string FrontendUrl = "https://localhost:7087";
}
