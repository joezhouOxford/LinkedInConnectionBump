# LinkedInConnectionBump
an bot to send invite to linkedin suggested connections. write with C#,chrome webdriver and JavaScript.  
&lt;appSettings&gt;

    <add key="LinkedInUserName" value=""/> <!--LinkedIn UserName-->
    <add key="LinkedInPassword" value=""/> <!--LinkedIn Password-->
    <add key="ConnectionBatchSize" value="100"/> <!--how many connections you want linkedIn suggest before add all of them-->
    <add key="AddConnectionLimit" value="600"/><!--how many connection invitation in total you want to send-->
    <add key="AddedConnections" value="0"/>
    <!--how many connection invitation you have sent, will be bit higher than the limit due to the batch size-->
    
  &lt;/appSettings&gt;
