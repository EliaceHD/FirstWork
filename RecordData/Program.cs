using System;
using System.Threading;
using System.Net;


namespace RecordData
{
    class GetRecords
    {
        const int MachineID = 104;
        const string MachineIP = "192.168.0.16";
        const int MachinePort = 4370;
        const string SeverURL = "192.168.1.251";

        public zkemkeeper.CZKEMClass rootCZKEM = new zkemkeeper.CZKEMClass();
        bool isConnect = false;
        static void Main(string[] args) {
            GetRecords gr = new GetRecords();
            Thread th = new Thread(new ThreadStart(gr.GetData));
            th.Start();
            Console.WriteLine("Exit");
        }
        void GetData() {
            bool isOK = false;
            string userID = null;
            //string userName = null;
            //string userPassword = null;
            //int userType = 0;
            //bool userIsUsed = false;
            int logVerifyType = 0;
            int logState = 0;
            int logYear = 0;
            int logMonth = 0;
            int logDay = 0;
            int logHour = 0;
            int logMinute = 0;
            int logSecond = 0;
            int logWorkCode = 1;

            Console.WriteLine("Connecting...");
            isConnect = rootCZKEM.Connect_Net(MachineIP, MachinePort);
            if (isConnect) {
                Console.WriteLine("Connect Succeed!");
            }
            else {
                while (!isConnect) {
                    Thread.Sleep(1000);
                    Console.WriteLine("Connect Failed.Trying again");
                    isConnect = rootCZKEM.Connect_Net(MachineIP, MachinePort);
                }
            }
            isOK = rootCZKEM.ReadAllGLogData(MachineID);
            if (isOK) {
            }
            isOK = true;
            while (isOK) {
                isOK = rootCZKEM.SSR_GetGeneralLogData(MachineID, out userID, out logVerifyType, out logState, out logYear, out logMonth, out logDay, out logHour, out logMinute, out logSecond, ref logWorkCode);
                if (isOK) {
                    Console.WriteLine("ID:" + userID + " VerifyType:" + logVerifyType + "State:" + logState + "Time:" + logYear + "." + logMonth + "." + logDay + " " + logHour + ":" + logMinute + ":" + logSecond + "WorkCode:" + logWorkCode);
                }
                else {
                    break;
                }
            }
            HttpWebRequest httpWebRequest =(HttpWebRequest)HttpWebRequest.Create(SeverURL);
            httpWebRequest.Method = "POST";



        }
        void Test() {
            Console.Write("test message");
        }

    }
}
