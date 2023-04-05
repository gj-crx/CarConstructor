using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveLoadSystem
{
    public class LogSaver : MonoBehaviour
    {
        public static LogSaver Singleton;
        public string logWritingPath = "Logs.txt";

        private LogInfo logs = new LogInfo();
        void Start()
        {
            Application.logMessageReceived += LogCallback;
            Singleton = this;
        }


        public void AddLog(string logMessage)
        {
            logs.logInfoList.Add(new Logs("saveload message", logMessage));
        }

        void LogCallback(string condition, string stackTrace, LogType type)
        {
            try
            {
                if (gameObject.activeInHierarchy == false) return;
                //Create new Log
                if (type == LogType.Error || type == LogType.Exception)
                {
                    Logs logInfo = new Logs(condition, stackTrace);

                    //Add it to the List
                    logs.logInfoList.Add(logInfo);
                }
            }
            catch { }
        }

        [Serializable]
        public struct Logs
        {
            public string condition;
            public string stackTrace;

            public Logs(string condition, string stackTrace)
            {
                this.condition = condition;
                this.stackTrace = stackTrace;
            }
        }
        [Serializable]
        public class LogInfo
        {
            public List<Logs> logInfoList = new List<Logs>();
        }
        private void OnApplicationQuit()
        {
            if (isActiveAndEnabled) SerializationManager.SaveObject(logWritingPath, Application.persistentDataPath + "/", logs);
        }
        private void OnApplicationPause(bool pause)
        {
            if (isActiveAndEnabled) SerializationManager.SaveObject(logWritingPath, Application.persistentDataPath + "/", logs);
        }


    }
}
