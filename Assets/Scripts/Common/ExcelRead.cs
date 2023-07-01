using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System.IO;
using Excel;

public class ExcelRead
{
    public static DataSet ReadExcel(string path)
    {
        FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        IExcelDataReader iExcelDR = ExcelReaderFactory.CreateOpenXmlReader(fs);
        DataSet ds = iExcelDR.AsDataSet();
        fs.Close();
        return ds;
    }
}
