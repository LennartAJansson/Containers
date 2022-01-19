namespace NATS.Extensions.DependencyInjection
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using NATS.Client;
    using NATS.Client.JetStream;

    public static class JetStreamUtils
    {
        // ----------------------------------------------------------------------------------------------------
        // STREAM INFO / CREATE / UPDATE 
        // ----------------------------------------------------------------------------------------------------
        public static StreamInfo? GetStreamInfoOrNullWhenNotExist(IJetStreamManagement jsm, string streamName)
        {
            try
            {
                return jsm.GetStreamInfo(streamName);
            }
            catch (NATSJetStreamException e)
            {
                if (e.ErrorCode == 404)
                {
                    return null;
                }
                throw;
            }
        }

        public static bool StreamExists(IConnection c, string streamName) =>
            GetStreamInfoOrNullWhenNotExist(c.CreateJetStreamManagementContext(), streamName) != null;

        public static bool StreamExists(IJetStreamManagement jsm, string streamName) =>
            GetStreamInfoOrNullWhenNotExist(jsm, streamName) != null;

        public static void ExitIfStreamExists(IJetStreamManagement jsm, string streamName)
        {
            if (StreamExists(jsm, streamName))
            {
                Environment.Exit(-1);
            }
        }

        public static void ExitIfStreamNotExists(IConnection c, string streamName)
        {
            if (!StreamExists(c, streamName))
            {
                Environment.Exit(-1);
            }
        }

        public static StreamInfo CreateStream(IJetStreamManagement jsm, string streamName, StorageType storageType, params string[] subjects)
        {
            StreamConfiguration sc = StreamConfiguration.Builder()
                .WithName(streamName)
                .WithStorageType(storageType)
                .WithSubjects(subjects)
                .Build();

            StreamInfo si = jsm.AddStream(sc);

            return si;
        }

        public static StreamInfo CreateStream(IJetStreamManagement jsm, string stream, params string[] subjects) =>
            CreateStream(jsm, stream, StorageType.Memory, subjects);

        public static StreamInfo CreateStream(IConnection c, string stream, params string[] subjects) =>
            CreateStream(c.CreateJetStreamManagementContext(), stream, StorageType.Memory, subjects);

        public static StreamInfo CreateStreamExitWhenExists(IConnection c, string streamName, params string[] subjects) =>
            CreateStreamExitWhenExists(c.CreateJetStreamManagementContext(), streamName, subjects);

        public static StreamInfo CreateStreamExitWhenExists(IJetStreamManagement jsm, string streamName, params string[] subjects)
        {
            ExitIfStreamExists(jsm, streamName);
            return CreateStream(jsm, streamName, StorageType.Memory, subjects);
        }

        public static void CreateStreamWhenDoesNotExist(IJetStreamManagement jsm, string stream, params string[] subjects)
        {
            try
            {
                jsm.GetStreamInfo(stream);
                return;
            }
            catch (NATSJetStreamException)
            {
            }

            StreamConfiguration sc = StreamConfiguration.Builder()
                .WithName(stream)
                .WithStorageType(StorageType.Memory)
                .WithSubjects(subjects)
                .Build();
            jsm.AddStream(sc);
        }

        public static void CreateStreamWhenDoesNotExist(IConnection c, string stream, params string[] subjects) =>
            CreateStreamWhenDoesNotExist(c.CreateJetStreamManagementContext(), stream, subjects);

        public static StreamInfo CreateStreamOrUpdateSubjects(IJetStreamManagement jsm, string streamName, StorageType storageType, params string[] subjects)
        {

            StreamInfo? si = GetStreamInfoOrNullWhenNotExist(jsm, streamName);
            if (si == null)
            {
                return CreateStream(jsm, streamName, storageType, subjects);
            }

            StreamConfiguration sc = si.Config;
            bool needToUpdate = false;
            foreach (string sub in subjects)
            {
                if (!sc.Subjects.Contains(sub))
                {
                    needToUpdate = true;
                    sc.Subjects.Add(sub);
                }
            }

            if (needToUpdate)
            {
                si = jsm.UpdateStream(sc);
            }

            return si;
        }

        public static StreamInfo CreateStreamOrUpdateSubjects(IJetStreamManagement jsm, string streamName, params string[] subjects) =>
            CreateStreamOrUpdateSubjects(jsm, streamName, StorageType.Memory, subjects);

        public static StreamInfo CreateStreamOrUpdateSubjects(IConnection c, string stream, params string[] subjects) =>
            CreateStreamOrUpdateSubjects(c.CreateJetStreamManagementContext(), stream, StorageType.Memory, subjects);

        // ----------------------------------------------------------------------------------------------------
        // PUBLISH
        // ----------------------------------------------------------------------------------------------------
        public static void Publish(IConnection c, string subject, int count) =>
            Publish(c.CreateJetStreamContext(), subject, "data", count);

        public static void Publish(IJetStream js, string subject, int count) =>
            Publish(js, subject, "data", count);

        public static void Publish(IJetStream js, string subject, string prefix, int count)
        {
            for (int x = 1; x <= count; x++)
            {
                string data = prefix + x;
                js.Publish(subject, Encoding.UTF8.GetBytes(data));
            }
        }

        public static void PublishInBackground(IJetStream js, string subject, string prefix, int count)
        {
            new Thread(() =>
            {
                try
                {
                    for (int x = 1; x <= count; x++)
                    {
                        js.Publish(subject, Encoding.ASCII.GetBytes(prefix + "-" + x));
                    }
                }
                catch (Exception)
                {
                    Environment.Exit(-1);
                }
            }).Start();
            Thread.Sleep(100); // give the publish thread a little time to get going
        }

        // ----------------------------------------------------------------------------------------------------
        // READ MESSAGES
        // ----------------------------------------------------------------------------------------------------
        public static IList<Msg> ReadMessagesAck(ISyncSubscription sub, int timeout = 1000)
        {
            IList<Msg> messages = new List<Msg>();
            bool keepGoing = true;
            while (keepGoing)
            {
                try
                {
                    Msg msg = sub.NextMessage(timeout);
                    messages.Add(msg);
                    msg.Ack();
                }
                catch (NATSTimeoutException)
                {
                    keepGoing = false;
                }
            }

            return messages;
        }
    }
}
