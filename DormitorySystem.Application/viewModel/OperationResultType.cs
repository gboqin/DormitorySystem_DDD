using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitorySystem.Application.viewModel
{
    public enum OperationResultType
    {
        /// <summary>
        ///     操作成功
        /// </summary>
        [Description("操作成功。")]
        Success,
        /// <summary>
        ///     警告
        /// </summary>
        [Description("警告")]
        Warning,

        /// <summary>
        ///     操作引发错误
        /// </summary>
        [Description("操作引发错误。")]
        Error
    }
}
