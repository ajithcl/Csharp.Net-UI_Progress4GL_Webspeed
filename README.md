# WindowsFormsAppWebspeed
Sample program for connecting C# client with Progress4GL webspeed.

Proof of concept for accessing data from Progress 4GL/OpenEdge Webspeed to UI built in C# .net.
XML is used as the medium for communicating between Progress 4GL and C#.

From the remote machine webspeed needs to be run via apache server.
websample.p program accepts one input parameter in the url parameter and returns result in the form of XML through TEMP-TABLE:WRITE-XML method.

C# UI, through the click event complete url will be generated and put GET request and access the XML data,parse it and shown in the UI fields.
