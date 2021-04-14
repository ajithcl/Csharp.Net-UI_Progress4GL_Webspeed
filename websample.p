{src/web2/wrap-cgi.i}
DEFINE VARIABLE miGuestNr AS INTEGER NO-UNDO.
output-content-type ("text/html":U).
miGuestNr = INTEGER (get-value ("guestnumber":U)).

DEFINE TEMP-TABLE ttGuest NO-UNDO 
  FIELD NAME AS CHARACTER 
  FIELD address1 AS CHARACTER 
  FIELD country AS CHARACTER.


/***
RUN StartHtml.
RUN getGuestDetails (INPUT miGuestNr).
RUN EndHtml.
***/


RUN getGuestXML(miGuestNr).
TEMP-TABLE ttGuest:WRITE-XML ("stream",  "webstream",YES). /*Write the XML to default Webspeed stream*/




PROCEDURE getGuestXML:
  DEFINE INPUT PARAMETER ipGuestNr AS INTEGER NO-UNDO.
  FOR FIRST guest NO-LOCK WHERE guest.gastnr =  ipGuestNr:
    CREATE ttGuest.
    ASSIGN 
      ttGuest.name = guest.Name
      ttGuest.address1 = guest.adresse1
      ttGuest.country = guest.land.
  END.
  IF NOT AVAILABLE guest THEN DO:
    CREATE ttGuest.
  END.
END PROCEDURE. 

PROCEDURE StartHtml:
  {&OUT}
    "<HTML>":U SKIP
    "<HEAD>":U SKIP
    "<TITLE> Guest Details </TITLE>":U SKIP
    "</HEAD>":U SKIP
    "<BODY>":U SKIP
    .
END PROCEDURE.

PROCEDURE EndHtml:
  {&OUT}
    "</BODY>":U SKIP
    "</HTML>":U SKIP
    .
END PROCEDURE.
    
PROCEDURE getGuestDetails:
  DEFINE INPUT PARAMETER ipGuestNr AS INTEGER NO-UNDO.
  
  FOR FIRST guest NO-LOCK WHERE guest.gastnr = ipGuestNr:
    {&OUT}
      "<table><tr><td>"
      "Name:    ":U  "</td><td>":U    guest.name "</td></tr>":U  
      "<tr><td>" 
      "Phone:   ":U
      "</td><td>":U    guest.telefon  "</td></tr>"
      "<tr><td>"
      "Address1:":U
      "</td><td>":U guest.adresse1 "</td></tr>"  
      "<tr><td>"
      "Address2:":U 
      "</td><td>":U guest.adresse2  "</td></tr>"  
      "<tr><td>"
      "Address3:":U 
      "</td><td>":U guest.adresse3  "</td></tr>"  
      "</table>":U
      SKIP.
  END.
  IF NOT AVAILABLE guest THEN 
  DO:
    {&OUT}
      "Guest record not available."
      SKIP.
  END.
END PROCEDURE.