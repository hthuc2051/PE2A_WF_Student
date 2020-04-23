/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.practicalexam.student.khanhkt.controller;

import java.io.IOException;
import java.io.PrintWriter;
import java.sql.SQLException;
import javax.naming.NamingException;
import javax.servlet.RequestDispatcher;
import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import com.practicalexam.student.khanhkt.registration.RegistrationCreateErrors;
import com.practicalexam.student.khanhkt.registration.RegistrationDAO;
import com.practicalexam.student.khanhkt.registration.RegistrationDTO;

/**
 *
 * @author Kieu Trong Khanh
 */
@WebServlet(name = "CreateNewAccountServlet", urlPatterns = {"/CreateNewAccountServlet"})
public class CreateNewAccountServlet extends HttpServlet {
    private final String ERROR_PAGE = "createNewAccount.jsp";
    private final String LOGIN_PAGE = "login.jsp";
    
    /**
     * Processes requests for both HTTP <code>GET</code> and <code>POST</code>
     * methods.
     *
     * @param request servlet request
     * @param response servlet response
     * @throws ServletException if a servlet-specific error occurs
     * @throws IOException if an I/O error occurs
     */
    protected void processRequest(HttpServletRequest request, HttpServletResponse response)
            throws ServletException, IOException {
        response.setContentType("text/html;charset=UTF-8");
        PrintWriter out = response.getWriter();
        
        String url = ERROR_PAGE;
        
        String username = request.getParameter("txtUsername");
        String password = request.getParameter("txtPassword");
        String confirm = request.getParameter("txtConfirm");
        String fullname = request.getParameter("txtFullname");
        
        RegistrationCreateErrors errors = new RegistrationCreateErrors();
        boolean foundErr = false;
        
        try  {
            //1. Check validation all user's errors
            if (username.trim().length() < 6 || username.trim().length() > 30) {
                foundErr = true;
                errors.setUsernameLengthError("Username string is required from 6 to 30 characters");
            }
            if (password.trim().length() < 6 || password.trim().length() > 20) {
                foundErr = true;
                errors.setPasswordLengthError("Password string is required from 6 to 20 characters");
            } else if (!password.trim().equals(confirm.trim())) {
                foundErr = true;
                errors.setConfirmNotMatched("Confirm must match password");
            }
            if (fullname.trim().length() < 2 || fullname.trim().length() > 50) {
                foundErr = true;
                errors.setFullNameLengthError("Full name string is required from 2 to 50 characters");
            }
            //2. handle error
            if (foundErr) {
                //store errors using attribute in scope
                request.setAttribute("CREATEERROR", errors);
            } else {
                //insert to DB
                RegistrationDTO dto 
                        = new RegistrationDTO(username, password, fullname, false);
                RegistrationDAO dao = new RegistrationDAO();
                boolean result = dao.insertAccount(dto);
                
                if (result) {
                    url = LOGIN_PAGE;
                }//end if result is true
            }
        } catch (NamingException ex) {
            log("CreateNewAccountServlet_Naming: " + ex.getMessage());
        } catch (SQLException ex) {
            String msgErr = ex.getMessage();
            log("CreateNewAccountServlet_SQL " + msgErr);
            if (msgErr.contains("duplicate")) {
                errors.setUsernameIsExisted(username + " is existed. Please, choose another!!");
                //update errors to Scope
                request.setAttribute("CREATEERROR", errors);
            }
        }finally {
            RequestDispatcher rd = request.getRequestDispatcher(url);
            rd.forward(request, response);
            out.close();
        }
    }

    // <editor-fold defaultstate="collapsed" desc="HttpServlet methods. Click on the + sign on the left to edit the code.">
    /**
     * Handles the HTTP <code>GET</code> method.
     *
     * @param request servlet request
     * @param response servlet response
     * @throws ServletException if a servlet-specific error occurs
     * @throws IOException if an I/O error occurs
     */
    @Override
    protected void doGet(HttpServletRequest request, HttpServletResponse response)
            throws ServletException, IOException {
        processRequest(request, response);
    }

    /**
     * Handles the HTTP <code>POST</code> method.
     *
     * @param request servlet request
     * @param response servlet response
     * @throws ServletException if a servlet-specific error occurs
     * @throws IOException if an I/O error occurs
     */
    @Override
    protected void doPost(HttpServletRequest request, HttpServletResponse response)
            throws ServletException, IOException {
        processRequest(request, response);
    }

    /**
     * Returns a short description of the servlet.
     *
     * @return a String containing servlet description
     */
    @Override
    public String getServletInfo() {
        return "Short description";
    }// </editor-fold>

}
