﻿using System;

using TwitterBot.DB;
using TwitterBot.DB.Entities;

namespace TwitterBot
{
    internal class Logs
    {
        internal void WriteStatusLog(string status)
        {
            using (var db = new TwitterBotContext())
            {
                var log = new StatusLog();
                log.Message = status;
                db.StatusLogs.Add(log);
                db.SaveChanges();
            }
        }

        internal void WriteLog(string message)
        {
            using (var db = new TwitterBotContext())
            {
                var log = new Log();
                log.Message = message;
                db.Logs.Add(log);
                db.SaveChanges();
            }
        }

        internal void WriteErrorLog(Exception e)
        {
            var ex = e;
            do
            {
                WriteErrorLog(ex.Message, ex.StackTrace);
                ex = ex.InnerException;
            }
            while (ex != null);
        }

        private void WriteErrorLog(string message, string stackTrace)
        {
            using (var db = new TwitterBotContext())
            {
                var log = new ErrorLog();
                log.Message = message;
                log.StackTrace = stackTrace;
                db.Errors.Add(log);
                db.SaveChanges();
            }
        }

        internal void WriteBlackList(ulong user)
		{
            using (var db = new TwitterBotContext())
            {
                var blackList = new BlackList();
                blackList.UserId = Convert.ToInt64(user);
                db.BlackLists.Add(blackList);
                db.SaveChanges();
            }
        }
    }
}
