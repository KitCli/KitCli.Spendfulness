using YnabCli.Database.Settings;

namespace YnabCli.Commands;

public static class SettingsExtensions
{
    public static Setting? GetYnabApiTokenSetting(this ICollection<Setting> settings)
        => settings.FirstOrDefault(setting => setting.Type.Name == SettingTypeNames.YnabApiKey);
}