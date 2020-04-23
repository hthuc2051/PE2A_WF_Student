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
            <input type="text" name="txtUserName"/>
            <input type="password" name="txtPassword"/>
            <input type="hidden" name="action"/>
            <input type="submit" value="Login"/>
            ${requestScope.LOGIN_FAIL}
        </form>
    </body>
</html>
