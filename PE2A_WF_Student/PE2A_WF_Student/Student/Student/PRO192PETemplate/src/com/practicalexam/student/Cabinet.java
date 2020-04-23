/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.practicalexam.student;

import com.practicalexam.student.data.Jewelry;
import java.util.ArrayList;
import java.util.Collections;
import java.util.List;
import java.util.Scanner;

/**
 *
 * @author ADMIN
 */
public class Cabinet {

    //StartList
    // Declare ArrayList or Array here
    public List<Jewelry> listJewelrys = new ArrayList<>();
    //EndList
    Scanner scannerObj = new Scanner(System.in);

    public void add() {
        while (true) {
            try {
                System.out.println("Add a Jewrly to the list");
                System.out.println("Input Code : ");
                scannerObj = new Scanner(System.in);
                String id = scannerObj.nextLine();
                if (checkDuplicatedId(id)) {
                    System.out.println("Input Name : ");
                    scannerObj = new Scanner(System.in);
                    String name = scannerObj.nextLine();
                    System.out.println("Input Brand : ");
                    scannerObj = new Scanner(System.in);
                    String brand = scannerObj.nextLine();
                    System.out.println("Input Price : ");
                    scannerObj = new Scanner(System.in);
                    double price = Double.parseDouble(scannerObj.nextLine());
                    System.out.println(id + name + brand + price);
                    Jewelry dto = new Jewelry(id, name, brand, price);
                    if (listJewelrys.add(dto)) {
                        System.out.println("Inserted successfully");
                        dto.showDetail();
                        break;
                    } else {
                        System.out.println("Something wrong");
                    }
                } else {
                    System.out.println("Code has been existed!!! Please try again");
                }

            } catch (Exception ex) {

            }
        }

        // Print the object details after adding
    }

    public boolean checkDuplicatedId(String id) {
        // Your code here
        for (int i = 0; i < listJewelrys.size(); i++) {
            Jewelry getJewelry = listJewelrys.get(i);
            if (getJewelry.getCode().equals(id)) {
                return false;
            }
        }
        return true;
    }

    public Jewelry getJewelryById(String id) {
        // Your code here
        for (int i = 0; i < listJewelrys.size(); i++) {
            Jewelry getJewelry = listJewelrys.get(i);
            if (getJewelry.getCode().equals(id)) {
                return getJewelry;
            }
        }
        return null;
    }

    public void update() {
        while (true) {
            try {
                System.out.println("Update a Jewrly to the list");
                System.out.println("Input Code : ");
                scannerObj = new Scanner(System.in);
                String id = scannerObj.nextLine();
                Jewelry getJewrly = getJewelryById(id);
                if (getJewrly != null) {
                    System.out.println("Input Name : ");
                    scannerObj = new Scanner(System.in);
                    String name = scannerObj.nextLine();
                    System.out.println("Input Brand : ");
                    scannerObj = new Scanner(System.in);
                    String brand = scannerObj.nextLine();
                    System.out.println("Input Price : ");
                    scannerObj = new Scanner(System.in);
                    double price = Double.parseDouble(scannerObj.nextLine());
                    System.out.println(id + name + brand + price);
                    getJewrly.setName(name);
                    getJewrly.setBrand(brand);
                    getJewrly.setPrice(price);
                    System.out.println("Updated successfully");
                    getJewrly.showDetail();
                    break;
                } else {
                    System.out.println("not found code : " + id);
                }

            } catch (Exception ex) {

            }
        }
    }

    public void search() {
        // Print the object details after searching
        System.out.println("Search a Jewrly to the list");
        System.out.println("Input Code : ");
        scannerObj = new Scanner(System.in);
        String id = scannerObj.nextLine();
        for (int i = 0; i < listJewelrys.size(); i++) {
            Jewelry getJewelry = listJewelrys.get(i);
            if (getJewelry.getCode().equals(id)) {
                getJewelry.showDetail();
            }
        }
    }

    public void remove() {
        System.out.println("Search a Jewrly to the list");
        System.out.println("Input Code : ");
        scannerObj = new Scanner(System.in);
        String id = scannerObj.nextLine();
        if (checkDuplicatedId(id) == false) {
            System.out.println("Do you want to delete this ring (Press Y or N)");
            scannerObj = new Scanner(System.in);
            String choose = scannerObj.nextLine();
            if (choose.toLowerCase().equals("y")) {
                Jewelry getJewelry = getJewelryById(id);
                listJewelrys.remove(getJewelry);
                getJewelry.showDetail();
            }
        } else {
            System.out.println("Not found code : " + id);
        }

    }

    public void sort() {
        // Print the object details after sorting
        Collections.sort(listJewelrys);
        printAll();

    }

    public void printAll() {
        for (int i = 0; i < listJewelrys.size(); i++) {
            Jewelry getJewelry = listJewelrys.get(i);
            getJewelry.showDetail();
        }
    }
}
