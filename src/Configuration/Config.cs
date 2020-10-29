﻿namespace iPhoneController.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using Newtonsoft.Json;

    using iPhoneController.Diagnostics;

    public class Config
    {
        private static readonly IEventLogger _logger = EventLogger.GetLogger("CONFIG");

        #region Properties

        [JsonProperty("host")]
        public string Host { get; set; }

        [JsonProperty("port")]
        public ushort Port { get; set; }

        [JsonProperty("guildId")]
        public ulong GuildId { get; set; }

        [JsonProperty("ownerId")]
        public ulong OwnerId { get; set; }

        [JsonProperty("channelId")]
        public ulong ChannelId { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("commandPrefix")]
        public string CommandPrefix { get; set; }

        [JsonProperty("requiredRoles")]
        public List<ulong> RequiredRoles { get; set; }

        #endregion

        #region Constructor

        public Config()
        {
            Host = "*";
            Port = 6542;
            RequiredRoles = new List<ulong>();
        }

        #endregion

        public void Save(string filePath)
        {
            var data = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(filePath, data);
        }

        public static Config Load(string filePath)
        {
            return LoadInit<Config>(filePath, typeof(Config));
        }

        private static T LoadInit<T>(string filePath, Type type)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"{filePath} file not found.", filePath);
            }

            var data = File.ReadAllText(filePath);
            if (string.IsNullOrEmpty(data))
            {
                _logger.Error($"{filePath} database is empty.");
                return default;
            }

            return (T)JsonConvert.DeserializeObject(data, type);
        }

    }
}