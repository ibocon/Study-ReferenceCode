<?php
  // 1. Create a database connection

  //using constant function
  define("DB_SERVER", "localhost");
  define("DB_USER", "root");
  define("DB_PASS", "wlfka102");
  define("DB_NAME","widget_corp");
  /*
    $dbhost = "localhost";
    $dbuser = "widget_cms";
    $dbpass = "secretpassword";
    $dbname = "widget_corp";
  */
  $connection = mysqli_connect(DB_SERVER, DB_USER, DB_PASS, DB_NAME);
  // Test if connection succeeded
  if(mysqli_connect_errno()) {
    die("Database connection failed: " .
         mysqli_connect_error() .
         " (" . mysqli_connect_errno() . ")"
    );
  }
?>
