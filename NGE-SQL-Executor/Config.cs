using System;
using System.Collections.Generic;
using System.Text;

namespace NGE_SQL_Executor
{
    public class Config
    {
        public String DbServerName { get; set; } = @"localhost\MSSQLSERVER2017";
        public String DbName { get; set; } = @"Ngeretail";
        public String DbUserName { get; set; } = @"sa";
        public String DbPassword { get; set; } = @"123456";
        public String ScriptFolderPath { get; set; } = @"";
        public bool CheckedOnly { get; set; } = false;
        public String XMLFolderPath { get; set; } = @"C:\Bosnet\Bin";
        public String XMLFileName { get; set; } = @"SqlExecutorHistory.xml";
        public String Mode { get; set; } = @"patch";
        public bool Dev { get; set; } = true;

        public override string ToString()
        {
            
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"{nameof(DbServerName)}         : {DbServerName}");
            stringBuilder.AppendLine($"{nameof(DbName)}               : {DbName}");
            stringBuilder.AppendLine($"{nameof(DbUserName)}           : {DbUserName}");
            stringBuilder.AppendLine($"{nameof(DbPassword)}           : **********");
            stringBuilder.AppendLine($"{nameof(ScriptFolderPath)}     : {ScriptFolderPath}");
            stringBuilder.AppendLine($"{nameof(CheckedOnly)}          : {CheckedOnly}");
            stringBuilder.AppendLine($"{nameof(XMLFileName)}          : {XMLFileName}");
            stringBuilder.AppendLine($"{nameof(XMLFolderPath)}        : {XMLFolderPath}");
            stringBuilder.AppendLine($"{nameof(Mode)}                 : {Mode}");
            stringBuilder.AppendLine($"{nameof(Dev)}                  : {Dev}");

            return stringBuilder.ToString();
        }
    }
}
