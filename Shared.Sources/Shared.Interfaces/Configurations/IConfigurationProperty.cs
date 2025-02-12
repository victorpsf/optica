namespace Shared.Interfaces.Configurations;

public interface IConfigurationProperty
{
    public string? Value { get; set; }

    int ToIntValue(int defaultValue);
    long ToLongValue(long defaultValue);
    string ToStringValue(string defaultValue);
    bool toBoolValue(bool defaultValue);
}