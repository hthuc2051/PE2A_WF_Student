/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.practicalExam.student.daos;

import java.io.Serializable;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import com.practicalExam.util.MyConnection;

/**
 *
 * @author ASUS
 */
public class UserDAO implements Serializable {

    Connection con = null;
    PreparedStatement stm = null;
    ResultSet rs = null;

    private void closeConnection() throws SQLException {
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

    public String login(String userName, String password) throws ClassNotFoundException, SQLException {
        String role = "failed";
        try {
            con = MyConnection.getConnection();
            if (con != null) {
                String sql = "Select role from tblUser Where username=? and password = ? and active = true";
                stm = con.prepareStatement(sql);
                stm.setString(1, userName);
                stm.setString(2, password);
                rs = stm.executeQuery();
                while (rs.next()) {
                    role = rs.getString("role");
                }
            }
        } finally {
            closeConnection();
        }

        return role;
    }
}
