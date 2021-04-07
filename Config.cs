using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.EntityFrameworkCore;
using GenerationTicketsWPF.Models;

namespace GenerationTicketsWPF
{
    static class Config
    {
        private static DbContextOptions<GenerationTicketsContext> options;
        public static DbContextOptions<GenerationTicketsContext> Options
        {
            get { return options; }
        }
        private static Worker user;
        public static Worker User{
            get { return user; }
            set { user = value; }
        }
        static Config()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());// установка пути к текущему каталогу
            builder.AddJsonFile("appconfig.json");// получаем конфигурацию из файла appsettings.json
            var config = builder.Build(); // создаем конфигурацию
            string connectionString = config.GetConnectionString("DefaultConnection");// получаем строку подключения

            var optionsBuilder = new DbContextOptionsBuilder<GenerationTicketsContext>();
            options = optionsBuilder
                .UseSqlServer(connectionString)
                .Options;

        }
        
    }
}
