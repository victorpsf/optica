﻿using Shared.Interfaces.Configurations;

namespace Shared.Models.Configurations;

public class ConfigurationProperty: IConfigurationProperty
{
    public string? Value { get; set; }

    public int ToIntValue(int defaultValue)
    {
        try { return Convert.ToInt32(this.Value); }
        catch { return defaultValue; }
    }

    public long ToLongValue(long defaultValue)
    {
        try { return Convert.ToInt64(this.Value); }
        catch { return defaultValue; }
    }

    public string ToStringValue(string defaultValue)
    { return this.Value ?? defaultValue; }

    public bool toBoolValue(bool defaultValue)
    {
        switch (this.Value?.ToUpperInvariant())
        {
            case "TRUE": return true;
            case "SIM": return true;
            case "YES": return true;
            case "1": return true;
            case "S": return true;
            default: return defaultValue;
        }
    }
}