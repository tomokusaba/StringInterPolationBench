using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using System;
using System.Diagnostics;
using System.Text;

namespace ConsoleApp2
{
    [Config(typeof(MultipleRuntimesConfig))]
    [ShortRunJob]
    [MinColumn, MaxColumn]
    public class Program
    {
        readonly int Count = 100000;
        readonly string Str1 = DateTime.Now.ToString();
        readonly string Str2 = DateTime.Now.AddDays(2).ToString();
        readonly string Str3 = DateTime.Now.AddMonths(2).ToString();
        readonly string Str4 = DateTime.UtcNow.AddYears(2).ToString();
        readonly string Str5 = DateTime.Now.AddYears(3).ToString();
        readonly string Str6 = DateTime.UtcNow.AddYears(4).ToString();
        readonly string Str7 = DateTime.Now.AddYears(5).ToString();

        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<Program>();
        }

        [Benchmark]
        public string DoPlus()
        {
            string str = string.Empty;
            for (int i = 0; i < Count; i++)
            {
                str =  Str1 + "," + Str2 + "," + Str3 + "," + Str4 + "," + Str5 + "," + Str6 + "," + Str7 ;
            }
            return str;
        }

        [Benchmark]
        public string DoInterpolation()
        {
            string str = string.Empty;
            for (int i = 0; i < Count; i++)
            {
                str = $"{Str1},{Str2},{Str3},{Str4},{Str5},{Str6},{Str7}";
            }
            return str;
        }

        [Benchmark]
        public string DoStringBuilder()
        {
            StringBuilder sb = new StringBuilder(1000);
            const string commma = ",";
            for (int i = 0; i < Count; i++)
            {
                sb.Clear();
                sb.Append(Str1).Append(commma).Append(Str2).Append(commma).Append(Str3).Append(commma).Append(Str4).Append(commma).Append(Str5).Append(commma).Append(Str6).Append(commma).Append(Str7);
            }
            return sb.ToString();
        }

        public class MultipleRuntimesConfig : ManualConfig
        {
            public MultipleRuntimesConfig()
            {
                AddJob(Job.ShortRun.WithRuntime(ClrRuntime.Net48));
                AddJob(Job.ShortRun.WithRuntime(CoreRuntime.Core50));
                AddJob(Job.ShortRun.WithRuntime(CoreRuntime.Core60));
                AddJob(Job.ShortRun.WithRuntime(CoreRuntime.Core70));
            }

        }
    }
}
