package com.practicalexam;

import com.practicalexam.student.Cabinet;
import java.util.Scanner;

/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
/**
 *
 * @author ADMIN


 */
public class Program {

    public static void main(String[] args) {

        Cabinet management = new Cabinet();
        Scanner scannerObj = new Scanner(System.in);
        int chooseOption = 0;
        do {
            System.out.println("Welcome to WatchShop - @ 2020  by <Student ID - Student Name >");
            System.out.println("Select the options below: ");
            System.out.println("1.	Add a watch to the list");
            System.out.println("2.	Search a watch by ID");
            System.out.println("3.	Update a watch by ID");
            System.out.println("4.	Remove a watch by ID");
            System.out.println("5.	Print the watch list in the ascending order of Name");
            System.out.println("6.      Quit");
            System.out.println("Choose your option : ");
            scannerObj = new Scanner(System.in);
            chooseOption = scannerObj.nextInt();
            switch (chooseOption) {
                case 1: {
                    management.add();
                    break;
                }
                case 2: {
                    management.search();
                    break;
                }
                case 3: {
                    management.update();
                    break;
                }
                case 4: {
                    management.remove();
                    break;
                }
                case 5: {

                    management.sort();
                    break;
                }
            }
        } while (chooseOption > 0 && chooseOption < 6);
        System.out.println("Thank you for using app");
    }
}
