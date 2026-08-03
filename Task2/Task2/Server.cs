using System;
using System.Threading;


    public static class Server
    {
        private static int count = 0;
        private static readonly ReaderWriterLockSlim locker = new ReaderWriterLockSlim();

        public static int GetCount()
        {
            locker.EnterReadLock();

            try
            {
                Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] Начал чтение");
                Thread.Sleep(2000);
                Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] Закончил чтение");

                return count;
            }
            finally
            {
                locker.ExitReadLock();
            }
        }

        public static void AddToCount(int value)
        {
            locker.EnterWriteLock();

            try
            {
                Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] Начал запись (+{value})");
                Thread.Sleep(3000);
                count += value;
                Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] Закончил запись. Count = {count}");
            }
            finally
            {
                locker.ExitWriteLock();
            }
        }
    }
