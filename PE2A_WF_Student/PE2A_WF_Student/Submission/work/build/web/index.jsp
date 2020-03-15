<%-- 
    Document   : index
    Created on : Mar 15, 2020, 10:25:03 AM
    Author     : ASUS
--%>

<%@page contentType="text/html" pageEncoding="UTF-8"%>
<!DOCTYPE html>
<html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
        <title>JSP Page</title>
    </head>
    <body>
          <form action="MainController">
           username <input type="text" name="txtUserName"/><br/>
           password <input type="password" name="txtPassword"/><br/>
           <input type="hidden" name="action" value="login"/>
            <input type="submit" value="Login"/><br/>
            <font color="red" >${requestScope.LOGIN_FAIL}</font>
        </form>
    </body>
</html>
