using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Errlog
{

    public string LogErr(string AppName, string PageName, string FuncName, string ErrDesc, string UserId, string strDBNameKey)
    {
        string strOutput = "";
        Hashtable htParam = new Hashtable();
        DAL dAL = new DAL();

        htParam.Add("@AppName", AppName);
        htParam.Add("@PageName", PageName);
        htParam.Add("@FuncName", FuncName);
        htParam.Add("@ErrDesc", ErrDesc);
        htParam.Add("@UserId", UserId);

        dAL.exec_commandWithConn("xxx", htParam, strDBNameKey);

        htParam.Clear();
        return strOutput;
    }
}

