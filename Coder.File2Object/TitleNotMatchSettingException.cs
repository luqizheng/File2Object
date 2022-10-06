namespace Coder.File2Object
{
    /// <summary>
    /// 
    /// </summary>
    public class TitleNotMatchSettingException : File2ObjectException
    {
        public TitleNotMatchSettingException(string settingName,string actualName) : base($"文件标题不一致(设定:{settingName},实际:{actualName})，请检查是不是上传错文件")
        {
        }
    }
}