/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.practicalexam.student.khanhkt.util;

import java.io.Serializable;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;
import javax.naming.Context;
import javax.naming.InitialContext;
import javax.naming.NamingException;
import javax.sql.DataSource;

/**
 * @author Kieu Trong Khanh
 */
public class DBUtilities implements Serializable {
//    public static Connection makeConnection()
//            throws /*ClassNotFoundException*/ NamingException, SQLException {
//        Connection con = null;
//        try {
//            Context context = new InitialContext();
//            Context tomContext = (Context) context.lookup("java:comp/env");
//            DataSource ds = (DataSource) tomContext.lookup("SE1402DS");
//            con = ds.getConnection();
//        } catch (Exception e) {
//            e.printStackTrace();
//        }
//
//
//        return con;
////        //1. Load Driver
////        Class.forName("com.microsoft.sqlserver.jdbc.SQLServerDriver");
////        //2. Create Connection String
////        String url = "jdbc:sqlserver://localhost:1433;databaseName=Sinhvien2K17;instanceName=SQL2017";
////        //3. Open Connection
////        Connection con = DriverManager.getConnection(url, "sa", "trongkhanh");
////
////        return con;
//    }
    public static Connection makeConnection()
            throws /*ClassNotFoundException*/ NamingException, SQLException {
        Connection con = null;
        try {
            Class.forName("com.microsoft.sqlserver.jdbc.SQLServerDriver");
            String url = "jdbc:sqlserver://localhost:1433;databaseName=Sinhvien2K17;instanceName=SQL2017";
             con = DriverManager.getConnection(url, "sa", "12");
        } catch (Exception e) {
            e.printStackTrace();
        }
        return con;
//        //1. Load Driver
//        Class.forName("com.microsoft.sqlserver.jdbc.SQLServerDriver");
//        //2. Create Connection String
//        String url = "jdbc:sqlserver://localhost:1433;databaseName=Sinhvien2K17;instanceName=SQL2017";
//        //3. Open Connection
//        Connection con = DriverManager.getConnection(url, "sa", "trongkhanh");
//
//        return con;
    }
}
