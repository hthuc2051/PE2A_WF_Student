/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.practicalexam.student.khanhkt.registration;

import java.io.Serializable;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;
import javax.naming.NamingException;
import com.practicalexam.student.khanhkt.util.DBUtilities;

/**
 *
 * @author Kieu Trong Khanh
 */
public class RegistrationDAO implements Serializable {
    public boolean checkLogin(String username, String password)
        throws NamingException, SQLException {
        Connection con = null;
        PreparedStatement stm = null;
        ResultSet rs = null;
        
        try {
            //1. Connection
            con = DBUtilities.makeConnection();
            if (con != null) {
                //2. Create Sql String
                String sql = "Select username "
                                + "From Registration "
                                + "Where username = ? And password = ?";
                //3. Create statement
                stm = con.prepareStatement(sql);
                stm.setString(1, username);
                stm.setString(2, password);
                //4. Execute Query
                rs = stm.executeQuery();
                //5. Process
                if (rs.next()) {
                    return true;
                }//end if rs next
            }//end if con is not null
        } finally {
            if (rs != null) {
                rs.close();
            }
            if (stm != null) {
                stm.close();
            }
            if (con != null) {
                con.close();
            }
        }
        return false;
    }
    
    private List<RegistrationDTO> accountList;

    public List<RegistrationDTO> getAccountList() {
        return accountList;
    }
    
    public void searchLastname(String searchValue) 
        throws NamingException, SQLException {
        Connection con = null;
        PreparedStatement stm = null;
        ResultSet rs = null;
        
        try {
            //1. Connection
            con = DBUtilities.makeConnection();
            if (con != null) {
                //2. Create Sql String
                String sql = "Select username, password, lastname, isAdmin "
                                + "From Registration "
                                + "Where lastname Like ?";
                //3. Create statement
                stm = con.prepareStatement(sql);
                stm.setString(1, "%" + searchValue + "%");
                //4. Execute Query
                rs = stm.executeQuery();
                //5. Process
                while(rs.next()) {
                    String username = rs.getString("username");
                    String password = rs.getString("password");
                    String lastname = rs.getString("lastname");
                    boolean role = rs.getBoolean("isAdmin");
                    
                    RegistrationDTO dto 
                            = new RegistrationDTO(username, password, lastname, role);
                    
                    if (this.accountList == null) {
                        this.accountList = new ArrayList<>();
                    }//end if account List is null
                    
                    this.accountList.add(dto);
                }//end while rs is not null
            }//end if con is not null
        } finally {
            if (rs != null) {
                rs.close();
            }
            if (stm != null) {
                stm.close();
            }
            if (con != null) {
                con.close();
            }
        }
    }
    
    public boolean deleteAccount(String username) 
        throws NamingException, SQLException {
        Connection con = null;
        PreparedStatement stm = null;
        
        try {
            //1. Connection
            con = DBUtilities.makeConnection();
            if (con != null) {
                //2. Create Sql String
                String sql = "Delete From Registration "
                        + "Where username = ?";
                //3. Create statement
                stm = con.prepareStatement(sql);
                stm.setString(1, username);
                //4. Execute Query
                int rowEffect = stm.executeUpdate();
                //5. Process
                if (rowEffect > 0) {
                    return true;
                }
            }//end if con is not null
        } finally {
            if (stm != null) {
                stm.close();
            }
            if (con != null) {
                con.close();
            }
        }
        return false;
    }
    
    public boolean insertAccount(RegistrationDTO dto) 
        throws NamingException, SQLException {
        Connection con = null;
        PreparedStatement stm = null;
        
        try {
            //1. Connection
            con = DBUtilities.makeConnection();
            if (con != null) {
                //2. Create Sql String
                String sql = "Insert Into "
                        + "Registration(username, password, lastname, isAdmin) "
                        + "Values(?, ?, ?, ?)";
                //3. Create statement
                stm = con.prepareStatement(sql);
                stm.setString(1, dto.getUsername());
                stm.setString(2, dto.getPassword());
                stm.setString(3, dto.getLastname());
                stm.setBoolean(4, dto.isRole());
                //4. Execute Query
                int rowEffect = stm.executeUpdate();
                //5. Process
                if (rowEffect > 0) {
                    return true;
                }
            }//end if con is not null
        } finally {
            if (stm != null) {
                stm.close();
            }
            if (con != null) {
                con.close();
            }
        }
        return false;
    }
     public boolean updateAccount(RegistrationDTO dto) 
        throws NamingException, SQLException {
        Connection con = null;
        PreparedStatement stm = null;
        
        try {
            //1. Connection
            con = DBUtilities.makeConnection();
            if (con != null) {
                //2. Create Sql String
                String sql = "update "
                        + "Registration set password=?, isAdmin=? where username=? ";
                //3. Create statement
                stm = con.prepareStatement(sql);
                stm.setString(1, dto.getPassword());
                stm.setBoolean(2, dto.isRole());
                stm.setString(3, dto.getUsername());
                int rowEffect = stm.executeUpdate();
                //5. Process
                if (rowEffect > 0) {
                    return true;
                }
            }//end if con is not null
        } finally {
            if (stm != null) {
                stm.close();
            }
            if (con != null) {
                con.close();
            }
        }
        return false;
    }
     
     
}
