using log4net;
using System.Reflection;
using System.Text.Json;
using UniManage.Core.Models;

namespace UniManage.Core.Logging
{
    public static class UniLogManager
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static void WriteApiLog(CoreLogModel logData)
        {
            // Serialize header info thành JSON nếu có
            string headerInfo = "";
            if (logData.HeaderInfo != null)
            {
                headerInfo = JsonSerializer.Serialize(logData.HeaderInfo);
            }

            if (logData.IsException == 1)
            {
                log.Error($"API: {logData?.HeaderInfo?.ApiName}, HeaderInfo: {headerInfo}, Message: {logData?.Message}", logData?.Result as System.Exception);
            }
            else
            {
                log.Info($"API: {logData?.HeaderInfo?.ApiName}, HeaderInfo: {headerInfo}, Message: {logData?.Message}");
            }
        }
    }
}
