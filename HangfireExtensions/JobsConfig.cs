using System.Configuration;

namespace HangfireExtensions
{
    public class JobsConfig : ConfigurationSection
    {
        public static JobsConfig GetConfig()
        {
            return (JobsConfig)ConfigurationManager.GetSection("HangFireJobs") ?? new JobsConfig();
        }

        [ConfigurationProperty("Jobs")]
        [ConfigurationCollection(typeof(JobsConfigElement), AddItemName = "Job")]
        public JobsConfigCollection Jobs
        {
            get
            {
                var o = this["Jobs"];
                return o as JobsConfigCollection;
            }
        }
    }

    public class JobsConfigCollection : ConfigurationElementCollection
    {
        public JobsConfigElement this[int index]
        {
            get { return BaseGet(index) as JobsConfigElement; }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        public new JobsConfigElement this[string responseString]
        {
            get { return (JobsConfigElement)BaseGet(responseString); }
            set
            {
                if (BaseGet(responseString) != null)
                {
                    BaseRemoveAt(BaseIndexOf(BaseGet(responseString)));
                }
                BaseAdd(value);
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new JobsConfigElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((JobsConfigElement)element).JobName;
        }
    }

    public class JobsConfigElement : ConfigurationElement
    {
        [ConfigurationProperty("jobName", IsKey = true, IsRequired = true)]
        public string JobName => (string)this["jobName"];

        [ConfigurationProperty("jobClass", IsKey = false, IsRequired = true)]
        public string JobClass => (string)this["jobClass"];

        [ConfigurationProperty("jobCron", IsKey = false, IsRequired = true)]
        public string JobCron => (string)this["jobCron"];

        [ConfigurationProperty("jobTimeZone", IsKey = false, IsRequired = false)]
        public string JobTimeZone => (string)this["jobTimeZone"];

        [ConfigurationProperty("jobParameters", IsKey = false, IsRequired = false)]
        public string JobParameters => (string)this["jobParameters"];
    }
}
