using MMYBWebService.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MMYBWebService.Web.Util
{
    public class InterfaceHNUtil
    {

        private static long _port;
        private static string _server;
        private static string _servle;

        private static string _oper_centerid;
        private static string _oper_hospitalid;
        private static string _oper_hospitalid_pwd;
        private static string _oper_staffid;
        private static string _oper_staffid_pwd;


        static InterfaceHNUtil()
        {
            _port = 8080;
            _server = "192.168.169.54";
            _servle = "sicp3_test/ProcessAll";

            _oper_centerid = "440999";
            _oper_hospitalid = "440999024";
            _oper_staffid = "440999024";
            _oper_staffid_pwd = "440999024";
        }

        #region DLLImport

        [DllImport("InterfaceHN.dll")]
        private static extern long newinterface();


        [DllImport("InterfaceHN.dll")]
        private static extern long init(long pint, string addr, long port, string servlet);

        [DllImport("InterfaceHN.dll")]
        private static extern long newinterfacewithinit(string addr, long port, string servlet);

        [DllImport("InterfaceHN.dll")]
        private static extern long start(long pint, string id);

        [DllImport("InterfaceHN.dll")]
        private static extern long put(long pint, long row, string pname, string pvalue);

        [DllImport("InterfaceHN.dll")]
        private static extern long run(long pint);

        [DllImport("InterfaceHN.dll")]
        private static extern long setdebug(long pint, int flag, string in_direct);

        [DllImport("InterfaceHN.dll")]
        private static extern long getbyname(long pint, string pname, StringBuilder pvalue);

        [DllImport("InterfaceHN.dll")]
        private static extern long getbyindex(long pint, long pindex, StringBuilder pvalue);

        [DllImport("InterfaceHN.dll")]
        private static extern long getmessage(long pint, StringBuilder msg);


        [DllImport("InterfaceHN.dll")]
        private static extern long getexception(long pint, StringBuilder msg);

        [DllImport("InterfaceHN.dll")]
        private static extern void destoryinterface(long pint);

        [DllImport("InterfaceHN.dll")]
        private static extern int firstrow(long pint);

        [DllImport("InterfaceHN.dll")]
        private static extern int nextrow(long pint);

        [DllImport("InterfaceHN.dll")]
        private static extern int prevrow(long pint);


        [DllImport("InterfaceHN.dll")]
        private static extern int lastrow(long pint);

        [DllImport("InterfaceHN.dll")]
        private static extern int setresultset(long pint, string result_name);


        [DllImport("InterfaceHN.dll")]
        private static extern long set_ic_commport(long pint, string comm);

        #endregion

        private static long TryPutData(long pint, int row, string name, string value, string fun = "")
        {
            long ret = put(pint, row, name, value);
            if (ret <= 0)
            {
                throw new InterfaceHNException($"添加接口参数失败[{fun}]{row}-{name}-{value}！");
            }
            return ret;
        }

        private static long Login()
        {
            long pint = newinterfacewithinit(_server, _port, _servle);
            if (pint <= 0)
            {
                throw new InterfaceHNException("初始化接口函数失败-newinterfacewithinit！");
            }

            if (start(pint, InterfaceHNConst.FUN_LOGIN) <= 0)
            {
                throw new InterfaceHNException("接口登录失败-Start！");
            }

            TryPutData(pint, 1, "login_id", _oper_hospitalid, InterfaceHNConst.FUN_LOGIN);
            TryPutData(pint, 1, "login_password", _oper_hospitalid_pwd, InterfaceHNConst.FUN_LOGIN); 

            if (run(pint) <= 0)
            {
                StringBuilder msg = new StringBuilder(200);

                getmessage(pint, msg);
                throw new InterfaceHNException($"接口登录失败-run！\r\n{msg.ToString()}");
            } 
            return pint; 
        }

        public static void GetPersonInfo(string idCardNo, string bizType = "13")
        {

        }


    }

}
