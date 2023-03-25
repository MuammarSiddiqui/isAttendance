using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zkemkeeper;

namespace isAttendance
{
    public static class MyClass
    {


        public enum CONSTANTS
        {
            PORT = 4370,
        }


        public static void getAll(out List<AttendanceLog> p1,out List<AttendanceHistory> p2,string ip,int port,Guid CampusId)
        {

            try
            {
                List<AttendanceLog> list = new List<AttendanceLog>();
                List<AttendanceHistory> lst = new List<AttendanceHistory>();
                Service1.WriteToFile("Connecting... " + DateTime.Now);
                CZKEM objCZKEM = new CZKEM();
                if (objCZKEM.Connect_Net(ip,port))
                {
                    objCZKEM.SetDeviceTime2(objCZKEM.MachineNumber, DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                   
                    Service1.WriteToFile("Connection Successfull... " + DateTime.Now);
                }
                else
                {
                    Service1.WriteToFile("Connection Failed... " + DateTime.Now);
                }
                //if (objCZKEM.ReadGeneralLogData(objCZKEM.MachineNumber))
                if (true)
                {
                    //ArrayList logs = new ArrayList();
                    string log;
                    string dwEnrollNumber;
                    int dwVerifyMode;
                    int dwInOutMode;
                    int dwYear;
                    int dwMonth;
                    int dwDay;
                    int dwHour;
                    int dwMinute;
                    int dwSecond;
                    int dwWorkCode = 1;
                    int AWorkCode;
                    var res = objCZKEM.GetWorkCode(dwWorkCode, out AWorkCode);
                    //objCZKEM.SaveTheDataToFile(objCZKEM.MachineNumber, "attendance.txt", 1);
                    while (true)
                    {
                        if (!objCZKEM.SSR_GetGeneralLogData(
                        objCZKEM.MachineNumber,
                        out dwEnrollNumber,
                        out dwVerifyMode,
                        out dwInOutMode,
                        out dwYear,
                        out dwMonth,
                        out dwDay,
                        out dwHour,
                        out dwMinute,
                        out dwSecond,
                        ref AWorkCode
                        ))
                        {
                            break;
                        }
                        AttendanceHistory aloghistory = new AttendanceHistory();
                        log = "User ID:" + dwEnrollNumber + " " + verificationMode(dwVerifyMode) + " " + InorOut2(dwInOutMode) + " " + dwDay + "/" + dwMonth + "/" + dwYear + " " + time(dwHour) + ":" + time(dwMinute) + ":" + time(dwSecond);
                        aloghistory.UserId = dwEnrollNumber;
                        var date = dwMonth + "/" + dwDay + "/" + dwYear;
                        var t = time(dwHour) + ":" + time(dwMinute) + ":" + time(dwSecond);
                      
                            aloghistory.SignIn = t;
                        
                        aloghistory.Date = date;
                        aloghistory.Status = InorOut(dwInOutMode);
                        log = "User ID:" + dwEnrollNumber + " " + verificationMode(dwVerifyMode) + " " + InorOut2(dwInOutMode) + " " + dwDay + "/" + dwMonth + "/" + dwYear + " " + time(dwHour) + ":" + time(dwMinute) + ":" + time(dwSecond);
                        aloghistory.CampusId = CampusId;
                        lst.Add(aloghistory);
                        if (InorOut(dwInOutMode) == "IN" || InorOut(dwInOutMode) == "OUT")
                        {
                            AttendanceLog alog = new AttendanceLog();
                            log = "User ID:" + dwEnrollNumber + " " + verificationMode(dwVerifyMode) + " " + InorOut(dwInOutMode) + " " + dwDay + "/" + dwMonth + "/" + dwYear + " " + time(dwHour) + ":" + time(dwMinute) + ":" + time(dwSecond);
                            alog.EmployeeId = dwEnrollNumber;
                            alog.CampusId = CampusId;
                            //var date = dwDay + "/" + dwMonth + "/" + dwYear;
                            //var t = time(dwHour) + ":" + time(dwMinute) + ":" + time(dwSecond);
                            var d = DateTime.Parse(t);
                            if (d.Hour <= 10)
                            {
                                alog.SignIn = t;
                            }
                            else if (d.Hour > 10 )
                            {
                                alog.SignOut = t;
                            }
                                 else if (InorOut(dwInOutMode) == "IN")
                                {
                                    alog.SignIn = t;
                                }
                                else if (InorOut(dwInOutMode) == "OUT")
                                {
                                    alog.SignOut = t;
                                }
                          

                            alog.AttendanceDate = DateTime.Parse(date);
                            log = "User ID:" + dwEnrollNumber + " " + verificationMode(dwVerifyMode) + " " + InorOut(dwInOutMode) + " " + dwDay + "/" + dwMonth + "/" + dwYear + " " + time(dwHour) + ":" + time(dwMinute) + ":" + time(dwSecond);
                          
                            list.Add(alog);
                        }

                    }
                }
                p1 = list;
                p2 = lst;
            }
            catch (Exception)
            {
                throw;
            }
        }
        //public static List<AttendanceHistory> getAllHistory()
        //{

        //    try
        //    {
        //        List<AttendanceHistory> list = new List<AttendanceHistory>();
        //        Service1.WriteToFile("Connecting... " + DateTime.Now);
        //        CZKEM objCZKEM = new CZKEM();
        //        if (objCZKEM.Connect_Net(StaticValues.GetIpAddress(), (int)CONSTANTS.PORT))
        //        {
        //            objCZKEM.SetDeviceTime2(objCZKEM.MachineNumber, DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                   
        //            Service1.WriteToFile("Connection Successfull... " + DateTime.Now);
        //        }
        //        else
        //        {
        //            Service1.WriteToFile("Connection Failed... " + DateTime.Now);
        //        }
        //        //if (objCZKEM.ReadGeneralLogData(objCZKEM.MachineNumber))
        //        if (true)
        //        {
        //            //ArrayList logs = new ArrayList();
        //            string log;
        //            string dwEnrollNumber;
        //            int dwVerifyMode;
        //            int dwInOutMode;
        //            int dwYear;
        //            int dwMonth;
        //            int dwDay;
        //            int dwHour;
        //            int dwMinute;
        //            int dwSecond;
        //            int dwWorkCode = 1;
        //            int AWorkCode;
        //            var res = objCZKEM.GetWorkCode(dwWorkCode, out AWorkCode);
        //            //objCZKEM.SaveTheDataToFile(objCZKEM.MachineNumber, "attendance.txt", 1);
        //            while (true)
        //            {
        //                if (!objCZKEM.SSR_GetGeneralLogData(
        //                objCZKEM.MachineNumber,
        //                out dwEnrollNumber,
        //                out dwVerifyMode,
        //                out dwInOutMode,
        //                out dwYear,
        //                out dwMonth,
        //                out dwDay,
        //                out dwHour,
        //                out dwMinute,
        //                out dwSecond,
        //                ref AWorkCode
        //                ))
        //                {
        //                    break;
        //                }
                      
        //                    AttendanceHistory alog = new AttendanceHistory();
        //                    log = "User ID:" + dwEnrollNumber + " " + verificationMode(dwVerifyMode) + " " + InorOut2(dwInOutMode) + " " + dwDay + "/" + dwMonth + "/" + dwYear + " " + time(dwHour) + ":" + time(dwMinute) + ":" + time(dwSecond);
        //                    alog.UserId = dwEnrollNumber;
        //                    var date = dwDay + "/" + dwMonth + "/" + dwYear;
        //                    var t = time(dwHour) + ":" + time(dwMinute) + ":" + time(dwSecond);
        //                    if (InorOut2(dwInOutMode) == "IN")
        //                    {
        //                        alog.SignIn = t;
        //                    }
        //                    else if (InorOut2(dwInOutMode) == "OUT")
        //                    {
        //                        alog.SignOut = t;
        //                    }
        //                    alog.Date = date;
        //                    log = "User ID:" + dwEnrollNumber + " " + verificationMode(dwVerifyMode) + " " + InorOut2(dwInOutMode) + " " + dwDay + "/" + dwMonth + "/" + dwYear + " " + time(dwHour) + ":" + time(dwMinute) + ":" + time(dwSecond);
                          
        //                    list.Add(alog);
                        

        //            }
        //        }
        //        return list;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        //public static List<string> MYtest()
        //{

        //    try
        //    {
        //        List<string> list = new List<string>();
        //        //Console.WriteLine("Connecting...");
        //        Service1.WriteToFile("Connecting");
        //        CZKEM objCZKEM = new CZKEM();
        //        if (objCZKEM.Connect_Net(StaticValues.GetIpAddress(), (int)CONSTANTS.PORT))
        //        {
        //            objCZKEM.SetDeviceTime2(objCZKEM.MachineNumber, DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                   
        //            Service1.WriteToFile("Connection Succesful ");
        //        }
        //        else
        //        {
                    
        //            Service1.WriteToFile("Connection Failed");
        //        }
        //        if (true)
        //        {
                   
        //            string log;
        //            string dwEnrollNumber;
        //            int dwVerifyMode;
        //            int dwInOutMode;
        //            int dwYear;
        //            int dwMonth;
        //            int dwDay;
        //            int dwHour;
        //            int dwMinute;
        //            int dwSecond;
        //            int dwWorkCode = 1;
        //            int AWorkCode;
        //            var res = objCZKEM.GetWorkCode(dwWorkCode, out AWorkCode);
        //            //objCZKEM.SaveTheDataToFile(objCZKEM.MachineNumber, "attendance.txt", 1);
        //            while (true)
        //            {
        //                if (!objCZKEM.SSR_GetGeneralLogData(
        //                objCZKEM.MachineNumber,
        //                out dwEnrollNumber,
        //                out dwVerifyMode,
        //                out dwInOutMode,
        //                out dwYear,
        //                out dwMonth,
        //                out dwDay,
        //                out dwHour,
        //                out dwMinute,
        //                out dwSecond,
        //                ref AWorkCode
        //                ))
        //                {
        //                    break;
        //                }
        //                //log = "User ID:" + dwEnrollNumber + " " + verificationMode(dwVerifyMode) + " " + InorOut(dwInOutMode) + " " + dwDay + "/" + dwMonth + "/" + dwYear + " " + time(dwHour) + ":" + time(dwMinute) + ":" + time(dwSecond);

        //                //log = "User ID:" + dwEnrollNumber + " " + verificationMode(dwVerifyMode) + " " + InorOut(dwInOutMode) + " " + dwDay + "/" + dwMonth + "/" + dwYear + " " + time(dwHour) + ":" + time(dwMinute) + ":" + time(dwSecond);
        //                //Service1.WriteToFile(log);
        //               // list.Add(log);

        //            }
        //        }
        //        return list;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        public static void getAttendanceLogs(CZKEM objCZKEM)
        {
            string log;
            string dwEnrollNumber;
            int dwVerifyMode;
            int dwInOutMode;
            int dwYear;
            int dwMonth;
            int dwDay;
            int dwHour;
            int dwMinute;
            int dwSecond;
            int dwWorkCode = 1;
            int AWorkCode;
            objCZKEM.GetWorkCode(dwWorkCode, out AWorkCode);
            //objCZKEM.SaveTheDataToFile(objCZKEM.MachineNumber, "attendance.txt", 1);
            while (true)
            {
                if (!objCZKEM.SSR_GetGeneralLogData(
                objCZKEM.MachineNumber,
                out dwEnrollNumber,
                out dwVerifyMode,
                out dwInOutMode,
                out dwYear,
                out dwMonth,
                out dwDay,
                out dwHour,
                out dwMinute,
                out dwSecond,
                ref AWorkCode
                ))
                {
                    break;
                }
                log = "User ID:" + dwEnrollNumber + " " + verificationMode(dwVerifyMode) + " " + InorOut(dwInOutMode) + " " + dwDay + "/" + dwMonth + "/" + dwYear + " " + time(dwHour) + ":" + time(dwMinute) + ":" + time(dwSecond);
               // Console.WriteLine(log);
            }
        }

        public static string time(int Time)
        {
            string stringTime = "";
            if (Time < 10)
            {
                stringTime = "0" + Time.ToString();
            }
            else
            {
                stringTime = Time.ToString();
            }
            return stringTime;
        }

        public static string verificationMode(int verifyMode)
        {
            String mode = "";
            switch (verifyMode)
            {
                case 0:
                    mode = "Password";
                    break;
                case 1:
                    mode = "Fingerprint";
                    break;
                case 2:
                    mode = "Card";
                    break;
            }
            return mode;
        }

        public static string InorOut(int InOut)
        {
            string InOrOut = "";
            switch (InOut)
            {
                case 0:
                    InOrOut = "IN";
                    break;
                case 1:
                    InOrOut = "OUT";
                    break;
                case 2:
                    // InOrOut = "BREAK-OUT";
                    InOrOut = "OUT";
                    break;
                case 3:
                    //  InOrOut = "BREAK-IN";
                    InOrOut = "IN";
                    break;
                case 4:
                  //  InOrOut = "OVERTIME-IN";
                    InOrOut = "IN";
                    break;
                case 5:
                    // InOrOut = "OVERTIME-OUT";
                    InOrOut = "OUT";
                    break;

            }
            return InOrOut;
        }
        public static string InorOut2(int InOut)
        {
            string InOrOut = "";
            switch (InOut)
            {
                case 0:
                    InOrOut = "IN";
                    break;
                case 1:
                    InOrOut = "OUT";
                    break;
                case 2:
                     InOrOut = "BREAK-OUT";
                    
                    break;
                case 3:
                      InOrOut = "BREAK-IN";
                  
                    break;
                case 4:
                      InOrOut = "OVERTIME-IN";
                  
                    break;
                case 5:
                     InOrOut = "OVERTIME-OUT";
                   // InOrOut = "OUT";
                    break;

            }
            return InOrOut;
        }
    }
}
