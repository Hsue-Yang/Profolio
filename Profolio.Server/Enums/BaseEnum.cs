using System.ComponentModel;

namespace Profolio.Server.Enums
{

    public enum SuccessEnum
    {
        /// <summary> 成功 </summary>
        [Description("Txt_成功")]
        Success = 1,

        /// <summary> 失敗 </summary>
        [Description("Txt_失敗")]
        Fail = 2,
    }
}