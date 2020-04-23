<%-- 
    Document   : search
    Created on : Feb 27, 2020, 7:38:42 AM
    Author     : Kieu Trong Khanh
--%>

<%@page import="com.practicalexam.student.khanhkt.registration.RegistrationDTO"%>
<%@page import="java.util.List"%>
<%@page contentType="text/html" pageEncoding="UTF-8"%>
<%@taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c"%>
<!DOCTYPE html>
<html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
        <title>Search</title>
    </head>
    <body>
        <%-- <% 
             Cookie[] cookies = request.getCookies();
             if (cookies != null) {
                 String username = cookies[cookies.length - 1].getName();
                 %> 
                 <font color="red">
                     Welcome, <%= username %>
                 </font>
         <%
             }//end if cookies is existed
         %>--%>
        <font color="red">
        Welcome, ${sessionScope.USERNAME}
        </font>

        <h1>Search Page</h1>
        <form action="DispatchController">
            Search Value <input type="text" name="txtSearchValue" 
                                value="${param.txtSearchValue}" /><br/>
            <input type="submit" value="Search" name="btAction" />
        </form><br/>

        <c:set var="searchValue" value="${param.txtSearchValue}"/>
        <c:if test="${not empty searchValue}">
            <c:set var="result" value="${requestScope.SEARCHRESULT}"/>
            <c:if test="${not empty result}">
                <table border="1">
                    <thead>
                        <tr>
                            <th>No.</th>
                            <th>Username</th>
                            <th>Password</th>
                            <th>Last name</th>
                            <th>Role</th>
                            <th>Delete</th>
                            <th>Update</th>
                        </tr>
                    </thead>
                    <tbody>
                        <c:forEach var="dto" items="${result}" varStatus="counter">
                        <form action="DispatchController">
                            <tr>
                                <td>
                                    ${counter.count}
                                .</td>
                                <td>
                                    ${dto.username}
                                    <input type="hidden" name="txtUsername" 
                                           value="${dto.username}" />
                                </td>
                                <td>
                                    <input type="text" name="txtPassword" 
                                           value="${dto.password}" /> 
                                </td>
                                <td>
                                    ${dto.lastname}
                                </td>
                                <td>
                                    <input type="checkbox" name="chkAdmin" value="ON" 
                                           <c:if test="${dto.role}">
                                               checked="checked"
                                           </c:if>
                                           />
                                </td>
                                <td
                                    <c:url var="urlRewriting" value="DispatchController">
                                        <c:param name="btAction" value="delete"/>
                                        <c:param name="username" value="${dto.username}"/>
                                        <c:param name="lastSearchValue" value="${searchValue}"/>
                                    </c:url>
                                    <a href="${urlRewriting}">Delete</a>
                                </td>
                                <td>
                                    <input type="submit" value="Update" name="btAction" />
                                    <input type="hidden" name="lastSearchValue" value="${searchValue}" />
                                </td>
                            </tr>
                        </form>    
                        </c:forEach>
                    </tbody>
                </table>

            </c:if>
            <c:if test="${empty result}">
                <h2>
                    No record is matched!!!!
                </h2>
            </c:if>
        </c:if>
        <%-- <% 
             String searchValue = request.getParameter("txtSearchValue");
             if (searchValue != null) {
                 List<RegistrationDTO> result
                         = (List<RegistrationDTO>)request.getAttribute("SEARCHRESULT");
                 
                 if (result != null) {
                     %> 
                     <table border="1">
                         <thead>
                             <tr>
                                 <th>No.</th>
                                 <th>Username</th>
                                 <th>Password</th>
                                 <th>Last name</th>
                                 <th>Role</th>
                                 <th>Delete</th>
                                 <th>Update</th>
                             </tr>
                         </thead>
                         <tbody>
                             <% 
                                 int count = 0;
                                 for (RegistrationDTO dto : result) {
                                     String urlRewriting = "DispatchController"
                                                             + "?btAction=delete"
                                                             + "&username=" + dto.getUsername()
                                                             + "&lastSearchValue=" + searchValue;
                                     %>
                         <form action="DispatchController">
                                     <tr>
                                 <td>
                                     <%= ++count %>
                                 .</td>
                                 <td>
                                     <%= dto.getUsername() %>
                                     <input type="hidden" name="txtUsername" 
                                            value="<%= dto.getUsername() %>" />
                                 </td>
                                 <td>
                                     <input type="text" name="txtPassword" 
                                            value="<%= dto.getPassword()%>" />
                                 </td>
                                 <td>
                                     <%= dto.getLastname() %>
                                 </td>
                                 <td>
                                     <input type="checkbox" name="chkAdmin" value="ON" 
                                            <% 
                                             if (dto.isRole()) {
                                                 %> 
                                                 checked="checked"
                                            <%
                                             }//end if role is admin
                                            %>
                                            />
                                 </td>
                                 <td>
                                     <a href="<%= urlRewriting %>">Delete</a>
                                 </td>
                                 <td>
                                     <input type="submit" value="Update" name="btAction" />
                                     <input type="hidden" name="lastSearchValue" 
                                            value="<%= searchValue %>" />
                                 </td>
                             </tr>
                         </form>     
                             <%
                                 }//end for dto
                             %>
                         </tbody>
                     </table>

        <%
                } else {
                    %>
                    <h2>
                        No Record is matched!!!
                    </h2>
        <%
                }
            }//end if search Value is existed
        %>--%>
    </body>
</html>
