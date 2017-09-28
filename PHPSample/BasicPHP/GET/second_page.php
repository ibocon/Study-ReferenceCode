<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN"
   "http://www.w3.org/TR/html4/loose.dtd">

<html lang="en">
  <head>
    <title>Second Page</title>
  </head>
  <body>

    <pre>
      <?php
      //print out super global variable
        print_r($_GET);
      ?>
    </pre>

    <?php
      //get super global variable 'id'
      $id = $_GET['id'];
      echo $id;
    ?>
    <br />
    <?php
      //get super global variable 'company'
      $company = $_GET['company'];
      echo $company;
    ?>

  </body>
</html>
