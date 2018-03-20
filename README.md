# Hangfire Extensions

A small project to create jobs from the WebConfig, instead of hardcoding the configuration.

Quick configure it in two steps.

+ 1 In your global.asax:
```csharp
protected void Application_Start(object sender, EventArgs e)
{
  /// code
  // if you want to clear your jobs before creating them
  // HangfireExtensions.JobsInitializer.ClearJobs();
  
  HangfireExtensions.JobsInitializer.RegisterJobs();
  /// even more code
}
```

+ 2 Configure the jobs in you web/app.config
```xml
<configuration>
  <configSections>
  <!-- Some configs -->
  <section name="HangFireJobs" type="HangfireExtensions.JobsConfig" />
  <!-- Even more configs -->
  </configSections>
  <!-- Plenty of configs you got here Eh? -->
  <HangFireJobs>
    <Jobs>
      <Job jobName="Mail_the_Rebellion" jobClass="Rebellion.Mailing"
           jobCron="* * * * *" jobParameters="splited by, commas"
           jobTimeZone="Central Brazilian Standard Time">
      </Job>
  </Jobs>
  <!-- Some configs -->
</configuration>
```

More about [Time Zones in CSharp](https://docs.microsoft.com/en-us/previous-versions/windows/embedded/gg154758(v=winembedded.80)) and [Cron Expressions](https://docs.oracle.com/cd/E12058_01/doc/doc.1014/e12030/cron_expressions.htm)
