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

        public static InterfaceHNSetting config { get; set; }

        //private static long _port;
        //private static string _server;
        //private static string _servle;

        //private static string _oper_centerid;
        //private static string _oper_hospitalid;
        //private static string _oper_hospitalid_pwd;
        //private static string _oper_staffid;
        //private static string _oper_staffid_pwd;


        static InterfaceHNUtil()
        {
            //_port = 8080;
            //_server = "192.168.169.54";
            //_servle = "sicp3_test/ProcessAll";

            //_oper_centerid = "440999";
            //_oper_hospitalid = "440999024";
            //_oper_staffid = "440999024";
            //_oper_staffid_pwd = "440999024";
        }


        #region DLLImport

        const string DLL_NAME = @"InterfaceHN.dll";

        [DllImport(DLL_NAME, EntryPoint = "newinterface", CharSet = CharSet.Ansi)]
        private static extern int newinterface();


        [DllImport(DLL_NAME, EntryPoint = "init", CharSet = CharSet.Ansi)]
        private static extern int init(int pint, string addr, int port, string servlet);

        [DllImport(DLL_NAME, EntryPoint = "newinterfacewithinit", CharSet = CharSet.Ansi)]
        private static extern int newinterfacewithinit(string addr, int port, string servlet);

        [DllImport(DLL_NAME, EntryPoint = "start", CharSet = CharSet.Ansi)]
        private static extern int start(int pint, string id);

        [DllImport(DLL_NAME, EntryPoint = "put", CharSet = CharSet.Ansi)]
        private static extern int put(int pint, int row, string pname, string pvalue);

        [DllImport(DLL_NAME, EntryPoint = "run", CharSet = CharSet.Ansi)]
        private static extern int run(int pint);

        [DllImport(DLL_NAME, EntryPoint = "setdebug", CharSet = CharSet.Ansi)]
        private static extern int setdebug(int pint, int flag, string in_direct);

        [DllImport(DLL_NAME, EntryPoint = "getbyname", CharSet = CharSet.Ansi)]
        private static extern int getbyname(int pint, string pname, StringBuilder pvalue);

        [DllImport(DLL_NAME, EntryPoint = "getbyindex", CharSet = CharSet.Ansi)]
        private static extern int getbyindex(int pint, int pindex, StringBuilder pvalue);

        [DllImport(DLL_NAME, EntryPoint = "getmessage", CharSet = CharSet.Ansi)]
        private static extern int getmessage(int pint, StringBuilder msg);

        [DllImport(DLL_NAME, EntryPoint = "getexception", CharSet = CharSet.Ansi)]
        private static extern int getexception(int pint, StringBuilder msg);

        [DllImport(DLL_NAME, EntryPoint = "destoryinterface", CharSet = CharSet.Ansi)]
        private static extern void destoryinterface(int pint);

        [DllImport(DLL_NAME, EntryPoint = "firstrow", CharSet = CharSet.Ansi)]
        private static extern int firstrow(int pint);

        [DllImport(DLL_NAME, EntryPoint = "nextrow", CharSet = CharSet.Ansi)]
        private static extern int nextrow(int pint);

        [DllImport(DLL_NAME, EntryPoint = "prevrow", CharSet = CharSet.Ansi)]
        private static extern int prevrow(int pint);


        [DllImport(DLL_NAME, EntryPoint = "lastrow", CharSet = CharSet.Ansi)]
        private static extern int lastrow(int pint);

        [DllImport(DLL_NAME, EntryPoint = "setresultset", CharSet = CharSet.Ansi)]
        private static extern int setresultset(int pint, string result_name);


        [DllImport(DLL_NAME, EntryPoint = "set_ic_commport", CharSet = CharSet.Ansi)]
        private static extern long set_ic_commport(int pint, string comm);

        #endregion

        #region 包装方法

        private static StringBuilder CreateOutParam(int size = 1024)
        {
            return new StringBuilder(size);
        }

        private static string GetErrorMessage(int pint)
        {
            StringBuilder msg = CreateOutParam(10240);
            getmessage(pint, msg);
            return msg.ToString();
        }

        private static long TryPutData(int pint, int row, string name, string value, string fun = "")
        {
            long ret = put(pint, row, name, value);
            if (ret <= 0)
            {
                string msg = GetErrorMessage(pint);

                throw new InterfaceHNException($"接口{pint}添加参数失败！[{fun}]{row}-{name}-{value}！\r\n{msg}");
            }
            return ret;
        }

        private static string TryGetData(int pint, string name)
        {
            StringBuilder stb = CreateOutParam();
            int ret = getbyname(pint, name, stb);
            if (ret <= 0)
            {
                string msg = GetErrorMessage(pint);

                throw new InterfaceHNException($"接口{pint}获取数据失败！{name}! \r\n{msg}");
            }
            return stb.ToString();
        }

        private static long TryStart(int pint, string func)
        {
            long ret = start(pint, func);
            if (ret <= 0)
            {
                string msg = GetErrorMessage(pint);

                throw new InterfaceHNException($"接口{pint}Start失败！{func}！\r\n{msg}");
            }

            TryPutData(pint, 1, "oper_centerid", config.CenterId, func);
            TryPutData(pint, 1, "oper_hospitalid", config.HospitalId, func);
            TryPutData(pint, 1, "oper_staffid", config.StaffId, func);

            return ret;
        }

        private static long TryPutData<T>(int pint, int row, T data, string fun = "") where T : class
        {
            var props = typeof(T).GetProperties();
            foreach (var p in props)
            {
                if (p.CustomAttributes.ToList().Exists(x => x.AttributeType == typeof(IgnorePutDataAttribute)))
                {
                    continue;
                }
                // 为null就不传入值
                if (p.GetValue(data) == null)
                {
                    continue;
                }

                TryPutData(pint, row, p.Name, p.GetValue(data).ToString(), fun);
            }
            return pint;
        }

        private static T TrySetData<T>(int pint) where T : new()
        {
            T data = new T();

            var props = typeof(T).GetProperties();
            string value = "";
            foreach (var p in props)
            {
                if (p.CustomAttributes.ToList().Exists(x => x.AttributeType == typeof(IgnoreSetDataAttribute)))
                {
                    continue;
                }
                value = TryGetData(pint, p.Name);

                p.SetValue(data, value);
            }

            return data;

        }

        private static List<T> TrySetData<T>(int pint, string dsName) where T : new()
        {
            List<T> list = new List<T>();
            long ret = setresultset(pint, dsName);
            if (ret < 0)
            {
                string msg = GetErrorMessage(pint);

                throw new InterfaceHNException($"接口{pint}setresultset获取数据集失败！{dsName}！\r\n{msg}");
            }
            if (ret == 0)
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

        private static int TryRun(int pint, string func)
        {
            int ret = run(pint);
            if (ret <= 0)
            {
                string msg = GetErrorMessage(pint);

                throw new InterfaceHNException($"接口{pint}Run执行失败！{func}！\r\n{msg}");
            }
            return ret;
        }

        private static int Login()
        {
            int pint = newinterface();
            int ret = init(pint,config.Server, config.Port, config.Servle);
            string msg = "";

            if (pint <= 0)
            {
                msg = GetErrorMessage(pint);
                throw new InterfaceHNException($"接口{pint}newinterfacewithinit初始化失败！\r\n{msg}");
            }

            if (TryStart(pint, InterfaceHNConst.FUN_LOGIN) <= 0)
            {
                msg = GetErrorMessage(pint);
                throw new InterfaceHNException($"接口{pint}Start登录失败！\r\n{msg}");
            }

            TryPutData(pint, 1, "login_id", config.HospitalId, InterfaceHNConst.FUN_LOGIN);
            TryPutData(pint, 1, "login_password", config.HospitalId, InterfaceHNConst.FUN_LOGIN);

            if (run(pint) <= 0)
            {
                msg = GetErrorMessage(pint);
                throw new InterfaceHNException($"接口{pint}Run登录失败！\r\n{msg.ToString()}");
            }
            return pint;
        }

        private static void DestoryPint(int pint)
        {
            destoryinterface(pint);
        }

        #endregion

        #region 主要方法

        /// <summary>
        /// 获取人员信息
        /// </summary>
        /// <param name="reqPerson"></param>
        /// <returns></returns>
        public static ResPersonInfo_DS GetPersonInfo(ReqPersonInfo reqPerson)
        {
            ResPersonInfo_DS ds = new ResPersonInfo_DS();
            int pint = Login();

            TryStart(pint, InterfaceHNConst.FUN_BIZC131101);
            TryPutData<ReqPersonInfo>(pint, 1, reqPerson, InterfaceHNConst.FUN_BIZC131101);

            long ret = TryRun(pint, InterfaceHNConst.FUN_BIZC131101);

            List<ResPersonInfo> personList = TrySetData<ResPersonInfo>(pint, InterfaceHNConst.DS_PERSONINFO);

            // 多个PersonInfo，只返回PersonInfo数据
            if (personList.Count == 1)
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
            //else if (ret <= -1)
            //{
            //    StringBuilder msg = new StringBuilder(1024);

            //    getmessage(pint, msg);
            //    throw new InterfaceHNException($"接口执行失败-run！{ InterfaceHNConst.FUN_BIZC131101}！\r\n{msg.ToString()}");
            //}

            DestoryPint(pint);

            return ds;


        }

        /// <summary>
        /// 缴费
        /// </summary>
        /// <param name="reqChargeFee"></param>
        /// <returns></returns>
        public static ResChargeFee_DS ChargeFee(ReqChargeFee reqChargeFee)
        {
            ResChargeFee_DS ds = new ResChargeFee_DS();

            int pint = Login();

            TryStart(pint, InterfaceHNConst.FUN_BIZC131104);

            TryPutData<ReqChargeFee>(pint, 1, reqChargeFee, InterfaceHNConst.FUN_BIZC131104);
            long ret = setresultset(pint, InterfaceHNConst.DS_FEEINFO);
            int row = 1;
            foreach (var item in reqChargeFee.feeinfo)
            {
                TryPutData<ReqChargeFeeDetail>(pint, row, item, InterfaceHNConst.FUN_BIZC131104);
                row++;
            }

            ret = TryRun(pint, InterfaceHNConst.FUN_BIZC131104);

            List<ResBizInfo> bizInfoList = TrySetData<ResBizInfo>(pint, InterfaceHNConst.DS_BIZINFO);
            ds.BizInfoList = bizInfoList;

            List<ResPayInfo> payInfoList = TrySetData<ResPayInfo>(pint, InterfaceHNConst.DS_PAYINFO);
            ds.PayInfoList = payInfoList;

            List<ResDetailPay> detailPayList = TrySetData<ResDetailPay>(pint, InterfaceHNConst.DS_DETAILPAY);
            ds.DetailPayList = detailPayList;

            DestoryPint(pint);
            return ds;
        }

        /// <summary>
        /// 改费
        /// </summary>
        /// <param name="reqChangeFee"></param>
        /// <returns></returns>
        public static ResChargeFee_DS ChangeFee(ReqChangeFee reqChangeFee)
        {
            ResChargeFee_DS ds = new ResChargeFee_DS();

            int pint = Login();

            TryStart(pint, InterfaceHNConst.FUN_BIZC131104);
            TryPutData<ReqChangeFee>(pint, 1, reqChangeFee, InterfaceHNConst.FUN_BIZC131104);

            long ret = setresultset(pint, InterfaceHNConst.DS_FEEINFO);
            int row = 1;
            foreach (var item in reqChangeFee.feeinfo)
            {
                TryPutData<ReqChargeFeeDetail>(pint, row, item, InterfaceHNConst.FUN_BIZC131104);
                row++;
            }

            ret = TryRun(pint, InterfaceHNConst.FUN_BIZC131104);

            List<ResBizInfo> bizInfoList = TrySetData<ResBizInfo>(pint, InterfaceHNConst.DS_BIZINFO);
            ds.BizInfoList = bizInfoList;

            List<ResPayInfo> payInfoList = TrySetData<ResPayInfo>(pint, InterfaceHNConst.DS_PAYINFO);
            ds.PayInfoList = payInfoList;

            List<ResDetailPay> detailPayList = TrySetData<ResDetailPay>(pint, InterfaceHNConst.DS_DETAILPAY);
            ds.DetailPayList = detailPayList;

            DestoryPint(pint);
            return ds;
        }

        #endregion

    }
}
