<%-- 
    Document   : viewCart
    Created on : Mar 5, 2020, 8:10:14 AM
    Author     : Kieu Trong Khanh
--%>

<%@page import="java.util.Map"%>
<%@page import="com.practicalexam.student.khanhkt.cart.CartObject"%>
<%@page contentType="text/html" pageEncoding="UTF-8"%>
<!DOCTYPE html>
<html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
        <title>SuperMarket</title>
    </head>
    <body>
        <h1>View Your Cart</h1>
        
        <% 
            //1. User goes to cart place
            if (session != null) {
                //2. User takes his/her cart
                CartObject cart = (CartObject)session.getAttribute("CART");
                if (cart != null) {
                    Map<String, Integer> items = cart.getItems();
                    //3. Show all items from cart
                    if (items != null) {
                        %> 
                        <form action="DispatchController">
                        <table border="1">
                            <thead>
                                <tr>
                                    <th>No.</th>
                                    <th>Title</th>
                                    <th>Quantity</th>
                                    <th>Action</th>
                                </tr>
                            </thead>
                            <tbody>
                                <% 
                                    int count = 0;
                                    for (String itemKey : items.keySet()) {
                                        %> 
                                <tr>
                                    <td>
                                        <%= ++count %>
                                    .</td>
                                    <td>
                                        <%= itemKey %>
                                    </td>
                                    <td>
                                        <%= items.get(itemKey) %>
                                    </td>
                                    <td>
                                        <input type="checkbox" name="chkItem" 
                                               value="<%= itemKey %>" />
                                    </td>
                                </tr>        
                                <%
                                    }//end if for item
                                %>
                                <tr>
                                    <td colspan="3">
                                        <a href="superMarket.html">Add More Books to Your Cart</a>
                                    </td>
                                    <td>
                                        <input type="submit" value="Remove Selected Books" name="btAction" />
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        </form>
        <%
            return;
                    }//end if items is existed
                }//end if cart is existed
            }//end if session is existed
        %>
        
        <h2>
            <font color="red">
                No Cart is existed!!!!
            </font>
        </h2>
    </body>
</html>
