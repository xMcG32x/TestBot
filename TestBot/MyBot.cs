/**********************************************************************************************************************
/   Discord Test Bot
/   Created by Mike
/   Date: 10/21/2016
/   Purpose:  To test how a bot works in Discord chat and get a basic understanding of all the features.
/
/**********************************************************************************************************************/

using Discord;
using Discord.Commands;

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBot
{

    //MyBot Class which was delcared in the Program Class
    class MyBot
    {
        //Declaration of variables through class
        DiscordClient discord;
        CommandService commands;
        Random rand;
         

        //Public instance of the MyBot class
        public MyBot()
        {
            rand = new Random();
            
            //Logs for actions taken in Discord client.  Will display in console window.               
            discord = new DiscordClient(x =>
            {
                x.LogLevel = LogSeverity.Info;
                x.LogHandler = Log;
            });
                                         
           
            discord.ExecuteAndWait(async () =>
            {
                await discord.Connect("MjM5MDIxNTQxMjA2MTMwNjg4.Cuu9Qg.SneOzJQylShGfAdOJRSvVdP8Uwg", TokenType.Bot);
            });

            discord.UsingCommands(x =>
            {
                x.PrefixChar = '!';
                x.AllowMentionPrefix = true;
            });

            commands = discord.GetService<CommandService>();

            RegisterAdviceCommand();

            commands.CreateCommand("Hello")
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage("Fuck Off!!");
                });
        }

        private void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }

        private void RegisterAdviceCommand()
        {
            const string f = "advicedarkest.txt";
            List<string> lines = new List<string>();            

            using (StreamReader r = new StreamReader(f))
            {
                string line;
                while ((line = r.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }
            string[] a = lines.ToArray();

            commands.CreateCommand("advice")
               .Do(async (e) =>
               {
                   int randomAdviceIndex = rand.Next(a.Length);
                   await e.Channel.SendMessage(a[randomAdviceIndex]);
               });
        }
    }
}