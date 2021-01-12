using MMYBWebService.Web.Miscellaneous;
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
        private static extern int getbyname(long pint, string pname, StringBuilder pvalue);

        [DllImport("InterfaceHN.dll")]
        private static extern int getbyindex(long pint, long pindex, StringBuilder pvalue);

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

        #region 包装方法

        private static long TryPutData(long pint, int row, string name, string value, string fun = "")
        {
            long ret = put(pint, row, name, value);
            if (ret <= 0)
            {
                throw new InterfaceHNException($"添加接口参数失败[{fun}]{row}-{name}-{value}！");
            }
            return ret;
        }

        private static string TryGetData(long pint, string name)
        {
            StringBuilder stb = new StringBuilder(200);
            getbyname(pint, name, stb);
            return stb.ToString();
        }

        private static long TryStart(long pint, string func)
        {
            TryPutData(pint, 1, "oper_centerid", _oper_centerid, func);
            TryPutData(pint, 1, "oper_hospitalid", _oper_hospitalid, func);
            TryPutData(pint, 1, "oper_staffid", _oper_staffid, func);

            return pint;
        }

        private static long TryPutData<T>(long pint, int row, T data, string fun = "") where T : class
        {
            var props = typeof(T).GetProperties();
            foreach (var p in props)
            {
                if (p.CustomAttributes.ToList().Exists(x => x.AttributeType == typeof(IgnorePutDataAttribute)))
                {
                    continue;
                }

                TryPutData(pint, row, p.Name, p.GetValue(data).ToString(), fun);
            }
            return pint;
        }

        private static T TrySetData<T>(long pint) where T : new()
        {
            T data = new T();

            var props = typeof(T).GetProperties();
            string value = "";
            foreach (var p in props)
            {
                value = TryGetData(pint, p.Name);

                p.SetValue(data, value);
            }

            return data;

        }

        private static List<T> TrySetData<T>(long pint, string dsName) where T : new()
        {
            List<T> list = new List<T>();
            long ret = setresultset(pint, dsName);
            if (ret <= 0)
            {
                return list;
            }

            T data = TrySetData<T>(pint);
            list.Add(data);
            for (int i = 0; i < ret - 1; i++)
            {
                nextrow(pint);

                list.Add(TrySetData<T>(pint));
            }

            return list;
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

        private static void Logout(long pint)
        {
            destoryinterface(pint);
        }

        #endregion

        public static ResPersonInfo_DS GetPersonInfo(ReqPersonInfo reqPerson)
        {
            ResPersonInfo_DS ds = new ResPersonInfo_DS();
            long pint = Login();

            TryStart(pint, InterfaceHNConst.FUN_BIZC131101);
            TryPutData<ReqPersonInfo>(pint, 1, reqPerson, InterfaceHNConst.FUN_BIZC131101);

            long ret = run(pint);

            if (ret <= 0)
            {
                StringBuilder msg = new StringBuilder(200);

                getmessage(pint, msg);
                throw new InterfaceHNException($"接口执行失败-run！\r\n{ InterfaceHNConst.FUN_BIZC131101}\r\n{msg.ToString()}");
            }

            ret = setresultset(pint, InterfaceHNConst.DS_PERSONINFO);
            // 多个PersonInfo，只返回PersonInfo数据
            if (ret > 1)
            {
                ResPersonInfo personInfo = TrySetData<ResPersonInfo>(pint);
                ds.PersonInfoList.Add(personInfo);
                for (int i = 0; i < ret - 1; i++)
                {
                    nextrow(pint);
                    ds.PersonInfoList.Add(TrySetData<ResPersonInfo>(pint));
                }
            }
            else if (ret == 1)// 一个PersonInfo
            {
                ResPersonInfo personInfo = TrySetData<ResPersonInfo>(pint);
                ds.PersonInfoList.Add(personInfo);

                List<ResSpInfo> spList = TrySetData<ResSpInfo>(pint, InterfaceHNConst.DS_SPINFO);
                ds.SpInfoList = spList;

                List<ResElseInfo> elseList = TrySetData<ResElseInfo>(pint, InterfaceHNConst.DS_ELSEINFO);
                ds.ElseInfoList = elseList;

                List<ResLastBizInfo> lastBizList = TrySetData<ResLastBizInfo>(pint, InterfaceHNConst.DS_LASTBIZINFO);
                ds.LastBizInfoList = lastBizList;

                List<ResFreezeInfo> freezeList = TrySetData<ResFreezeInfo>(pint, InterfaceHNConst.DS_FREEZEINFO);
                ds.FreezeInfoList = freezeList;

                List<ResTotalBizInfo> totalbizList = TrySetData<ResTotalBizInfo>(pint, InterfaceHNConst.DS_TOTALBIZINFO);
                ds.TotalBizInfoList = totalbizList;
            }

            Logout(pint);

            return ds;


        }


        public static ResChargeFee_DS ChargeFee(ReqChargeFee reqChargeFee)
        {
            ResChargeFee_DS ds = new ResChargeFee_DS();

            long pint = Login();

            TryStart(pint, InterfaceHNConst.FUN_BIZC131104);
            TryPutData<ReqChargeFee>(pint, 1, reqChargeFee, InterfaceHNConst.FUN_BIZC131104);

            long ret = setresultset(pint, InterfaceHNConst.DS_FEEINFO);
            int row = 1;
            foreach (var item in reqChargeFee.details)
            {
                TryPutData<ReqChargeFeeDetail>(pint, row, item, InterfaceHNConst.FUN_BIZC131104);
                row++;
            }

            ret = run(pint);

            if (ret <= 0)
            {
                StringBuilder msg = new StringBuilder(200);

                getmessage(pint, msg);
                throw new InterfaceHNException($"接口执行失败-run！\r\n{ InterfaceHNConst.FUN_BIZC131104}\r\n{msg.ToString()}");
            }
            else
            {
                List<ResBizInfo> bizInfoList = TrySetData<ResBizInfo>(pint, InterfaceHNConst.DS_BIZINFO);
                ds.BizInfoList = bizInfoList;

                List<ResPayInfo> payInfoList = TrySetData<ResPayInfo>(pint, InterfaceHNConst.DS_PAYINFO);
                ds.PayInfoList = payInfoList;

                List<ResDetailPay> detailPayList = TrySetData<ResDetailPay>(pint, InterfaceHNConst.DS_DETAILPAY);
                ds.DetailPayList = detailPayList;
            }

            return ds;
        }

        public static ResChargeFee_DS ChangeFee(ReqChangeFee reqChangeFee)
        {
            ResChargeFee_DS ds = new ResChargeFee_DS();

            long pint = Login();

            TryStart(pint, InterfaceHNConst.FUN_BIZC131104);
            TryPutData<ReqChangeFee>(pint, 1, reqChangeFee, InterfaceHNConst.FUN_BIZC131104);

            long ret = setresultset(pint, InterfaceHNConst.DS_FEEINFO);
            int row = 1;
            foreach (var item in reqChangeFee.details)
            {
                TryPutData<ReqChargeFeeDetail>(pint, row, item, InterfaceHNConst.FUN_BIZC131104);
                row++;
            }

            ret = run(pint);

            if (ret <= 0)
            {
                StringBuilder msg = new StringBuilder(200);

                getmessage(pint, msg);
                throw new InterfaceHNException($"接口执行失败-run！\r\n{ InterfaceHNConst.FUN_BIZC131104}\r\n{msg.ToString()}");
            }
            else
            {
                List<ResBizInfo> bizInfoList = TrySetData<ResBizInfo>(pint, InterfaceHNConst.DS_BIZINFO);
                ds.BizInfoList = bizInfoList;

                List<ResPayInfo> payInfoList = TrySetData<ResPayInfo>(pint, InterfaceHNConst.DS_PAYINFO);
                ds.PayInfoList = payInfoList;

                List<ResDetailPay> detailPayList = TrySetData<ResDetailPay>(pint, InterfaceHNConst.DS_DETAILPAY);
                ds.DetailPayList = detailPayList;
            }

            return ds;
        }

    }
}
