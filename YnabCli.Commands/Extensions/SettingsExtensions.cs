using YnabCli.Database.Settings;

namespace YnabCli.Commands.Extensions;

public static class SettingsExtensions
{
    [Obsolete("Attach as Settings behaviour")]
    public static Setting? GetYnabApiTokenSetting(this ICollection<Setting> settings)
        => settings.FirstOrDefault(setting => setting.Type.Name == SettingTypeNames.YnabApiKey);
}