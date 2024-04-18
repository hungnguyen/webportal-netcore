# Web Portal NetCore

## Tech stack
* Net 8
* SQL Server
* React
* TypeScript
* Redux

## Overview

This is a content management system designed and developed by myself to meet the basic requirements of a business website, news website, online sales website, blog, ... of customers. With the characteristics of ease of use, high performance, and easy maintenance, I always try to bring the best experience to website administrators. This CMS is designed with a backend of C# + ASP.NET Core API + SQL Server, a frontend of React + Redux + Material UI. The frontend is integrated with the RestAPI backend, allowing the system to be deployed distributedly on many different servers and clouds and easily connected to each other to ensure system scalability and minimize system latency.

## Main fuction

### Admin account management function and decentralization: 
Admin account management, this function to add and edit admin accounts, change passwords, lock or unlock accounts. Manage rights and functions to decentralize access to functions on the CMS.

### Content management function: 
Manage article types, article types will differ in the number of information fields, when creating article types, there will be a management page for those article types. Manage categories and categories to group articles and display them in lists on the website. Manage articles, to add and edit new articles.

### Sales management function: 
Customer management, to save information about customers who have purchased products on the website. Manage orders, to confirm orders and update order status. Product management, to update product information, product images, product prices and quantities.

### Multilingual function: 
CMS also supports websites that require languages, the administrator will need to translate titles and text on the website, and add articles according to the language, so that the website can display. multilingual.

### Other utilities: 
In addition to the main functions, CMS also provides other utility functions, helping to increase the experience and support administrators in managing the website well. That is Chat, this is a chatbox that allows administrators to send real-time messages to each other to exchange work within the same CMS system. FileManager, this is a function that allows administrators to manage folders and images saved on the server/hosting. Analytics, this is the function that displays traffic reports connected to GoogleAnalytics.